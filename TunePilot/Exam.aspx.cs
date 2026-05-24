using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class Exam : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        // Load page

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["exam"] == null || Session["user_id"] == null)
            {
                Response.Redirect("StudentDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                ViewState["CurrentQuestionIndex"] = 0;

                LoadOrCreateAttempt();
                LoadExam();
                LoadQuestions();
                DisplayCurrentQuestion();
                UpdateNavigationButtons();
            }

            LoadAttempts();
        }

        // Attempt

        void LoadOrCreateAttempt()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT TOP 1 attempt_id
                    FROM exam_attempts
                    WHERE exam_id = @exam
                    AND user_id = @user
                    AND submitted_at IS NULL", con);

                cmd.Parameters.AddWithValue("@exam", Session["exam"]);
                cmd.Parameters.AddWithValue("@user", Session["user_id"]);

                object r = cmd.ExecuteScalar();

                int attemptId;

                if (r != null)
                {
                    attemptId = Convert.ToInt32(r);
                }
                else
                {
                    cmd = new SqlCommand(@"
                        INSERT INTO exam_attempts (exam_id, user_id, started_at)
                        OUTPUT INSERTED.attempt_id
                        VALUES (@exam, @user, GETDATE())", con);

                    cmd.Parameters.AddWithValue("@exam", Session["exam"]);
                    cmd.Parameters.AddWithValue("@user", Session["user_id"]);

                    attemptId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                ViewState["AttemptId"] = attemptId;
            }
        }

        int GetAttemptId()
        {
            return Convert.ToInt32(ViewState["AttemptId"]);
        }

        // Exam info

        void LoadExam()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT title, description, passing_score
                    FROM exams
                    WHERE exam_id = @id", con);

                cmd.Parameters.AddWithValue("@id", Session["exam"]);
                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    ExamTitle.Text = r["title"].ToString();
                    ExamDescription.Text = r["description"].ToString();
                    PassingScore.Text = "Passing Score: " + r["passing_score"].ToString() + "%";

                    ViewState["PassingScore"] = Convert.ToInt32(r["passing_score"]);
                }
            }
        }

        // Questions

        void LoadQuestions()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT question_id, instruction, expected_notes, question_order
                    FROM exam_questions
                    WHERE exam_id = @id
                    ORDER BY question_order", con);

                da.SelectCommand.Parameters.AddWithValue("@id", Session["exam"]);
                da.Fill(dt);
            }

            ViewState["Questions"] = dt;
        }

        DataTable GetQuestions()
        {
            return (DataTable)ViewState["Questions"];
        }

        // Display counter

        void DisplayCurrentQuestion()
        {
            DataTable questions = GetQuestions();
            int index = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            if (index < 0 || index >= questions.Rows.Count) return;

            DataRow row = questions.Rows[index];
            int questionId = Convert.ToInt32(row["question_id"]);

            QuestionInstruction.Text = row["instruction"].ToString();
            ExpectedNotesField.Value = row["expected_notes"].ToString();
            ExpectedNotesDisplay.Text = row["expected_notes"].ToString();
            QuestionCounter.Text = "Question " + (index + 1) + " of " + questions.Rows.Count;

            // Load previously saved answer for this question if it exists
            LoadSavedAnswer(questionId);

            // Render status dots
            RenderStatusDots(questions, index);

            // Show submit only on last question
            SubmitBtn.Visible = (index == questions.Rows.Count - 1);
        }

        void LoadSavedAnswer(int questionId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT detected_notes, accuracy_score
                    FROM exam_answers
                    WHERE attempt_id = @a
                    AND question_id  = @q", con);

                cmd.Parameters.AddWithValue("@a", GetAttemptId());
                cmd.Parameters.AddWithValue("@q", questionId);
                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string detected = r["detected_notes"] == DBNull.Value
                        ? "—" : r["detected_notes"].ToString();

                    string accuracy = r["accuracy_score"] == DBNull.Value
                        ? "—" : r["accuracy_score"].ToString() + "%";

                    DetectedNotesDisplay.Text = detected;
                    AccuracyDisplay.Text = accuracy;
                    DetectedNotesField.Value = detected == "—" ? "" : detected;
                }
                else
                {
                    DetectedNotesDisplay.Text = "—";
                    AccuracyDisplay.Text = "—";
                    DetectedNotesField.Value = "";
                    AccuracyField.Value = "";
                }
            }
        }

        void RenderStatusDots(DataTable questions, int currentIndex)
        {
            QuestionStatusContainer.Controls.Clear();

            for (int i = 0; i < questions.Rows.Count; i++)
            {
                int questionId = Convert.ToInt32(questions.Rows[i]["question_id"]);
                bool answered = IsAnswered(questionId);

                Label dot = new Label();
                dot.Text = " ● ";

                if (i == currentIndex)
                    dot.ForeColor = System.Drawing.Color.DarkGreen;
                else if (answered)
                    dot.ForeColor = System.Drawing.Color.Green;
                else
                    dot.ForeColor = System.Drawing.Color.Gray;

                QuestionStatusContainer.Controls.Add(dot);
            }
        }

        bool IsAnswered(int questionId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT COUNT(*)
                    FROM exam_answers
                    WHERE attempt_id = @a
                    AND question_id  = @q", con);

                cmd.Parameters.AddWithValue("@a", GetAttemptId());
                cmd.Parameters.AddWithValue("@q", questionId);
                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Save answer

        void SaveCurrentAnswer()
        {
            DataTable questions = GetQuestions();
            int index = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            if (index < 0 || index >= questions.Rows.Count) return;

            int questionId = Convert.ToInt32(questions.Rows[index]["question_id"]);
            string detected = DetectedNotesField.Value.Trim();
            string accuracy = AccuracyField.Value.Trim();

            if (string.IsNullOrEmpty(detected)) return;

            int accuracyScore = 0;
            int.TryParse(accuracy, out accuracyScore);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"
                    IF EXISTS (
                        SELECT 1 FROM exam_answers
                        WHERE attempt_id = @a AND question_id = @q
                    )
                        UPDATE exam_answers
                        SET detected_notes = @d,
                            accuracy_score = @s
                        WHERE attempt_id = @a AND question_id = @q
                    ELSE
                        INSERT INTO exam_answers
                            (attempt_id, question_id, detected_notes, accuracy_score)
                        VALUES (@a, @q, @d, @s)", con);

                cmd.Parameters.AddWithValue("@a", GetAttemptId());
                cmd.Parameters.AddWithValue("@q", questionId);
                cmd.Parameters.AddWithValue("@d", detected);
                cmd.Parameters.AddWithValue("@s", accuracyScore);
                cmd.ExecuteNonQuery();
            }
        }

        // Navigation

        protected void Next_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();

            int index = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);
            int total = GetQuestions().Rows.Count;

            if (index < total - 1)
            {
                ViewState["CurrentQuestionIndex"] = index + 1;
                DisplayCurrentQuestion();
                UpdateNavigationButtons();
            }
        }

        protected void Prev_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();

            int index = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            if (index > 0)
            {
                ViewState["CurrentQuestionIndex"] = index - 1;
                DisplayCurrentQuestion();
                UpdateNavigationButtons();
            }
        }

        void UpdateNavigationButtons()
        {
            int index = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);
            int total = GetQuestions().Rows.Count;

            PrevBtn.Enabled = index > 0;
            NextBtn.Enabled = index < total - 1;
        }

        // Submit

        protected void Submit_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();

            int totalScore = 0;
            int answered = 0;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT accuracy_score
                    FROM exam_answers
                    WHERE attempt_id = @a", con);

                cmd.Parameters.AddWithValue("@a", GetAttemptId());
                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    if (r["accuracy_score"] != DBNull.Value)
                    {
                        totalScore += Convert.ToInt32(r["accuracy_score"]);
                        answered++;
                    }
                }
            }

            int finalScore = answered > 0 ? totalScore / answered : 0;
            int passing = Convert.ToInt32(ViewState["PassingScore"]);
            bool passed = finalScore >= passing;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE exam_attempts
                    SET score        = @s,
                        passed       = @p,
                        submitted_at = GETDATE()
                    WHERE attempt_id = @a", con);

                cmd.Parameters.AddWithValue("@s", finalScore);
                cmd.Parameters.AddWithValue("@p", passed);
                cmd.Parameters.AddWithValue("@a", GetAttemptId());
                con.Open();
                cmd.ExecuteNonQuery();
            }

            string result = passed ? "Passed" : "Not passed";

            ClientScript.RegisterStartupScript(
                this.GetType(),
                "examDone",
                "alert('Exam submitted! Score: " + finalScore + "% — " + result + "');" +
                "window.location='StudentDashboard.aspx';",
                true);
        }

        // Back

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentDashboard.aspx");
        }

        // Previous attempts

        void LoadAttempts()
        {
            AttemptsContainer.Controls.Clear();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT attempt_id, score, passed, submitted_at
                    FROM exam_attempts
                    WHERE exam_id      = @exam
                    AND user_id        = @user
                    AND submitted_at   IS NOT NULL
                    ORDER BY submitted_at DESC", con);

                cmd.Parameters.AddWithValue("@exam", Session["exam"]);
                cmd.Parameters.AddWithValue("@user", Session["user_id"]);
                con.Open();

                SqlDataReader r = cmd.ExecuteReader();
                int count = 1;

                while (r.Read())
                {
                    string score = r["score"] == DBNull.Value
                        ? "—" : r["score"].ToString() + "%";

                    string result = r["passed"] == DBNull.Value
                        ? "—"
                        : (Convert.ToBoolean(r["passed"]) ? "Passed" : "Not passed");

                    string date = Convert.ToDateTime(r["submitted_at"])
                        .ToString("dd MMM yyyy, hh:mm tt");

                    Label lbl = new Label();
                    lbl.Text = "Attempt " + count + " | Score: " + score +
                               " | " + result + " | " + date;

                    AttemptsContainer.Controls.Add(lbl);
                    AttemptsContainer.Controls.Add(new LiteralControl("<br/><br/>"));

                    count++;
                }
            }
        }
    }
}