using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class Quiz : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        // =====================================================
        // PAGE LOAD
        // =====================================================

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["quiz"] == null || Session["user_id"] == null)
            {
                Response.Redirect("StudentDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                ViewState["CurrentQuestionIndex"] = 0;

                LoadOrCreateAttempt();

                LoadQuiz();

                LoadQuestions();

                RenderQuestionStatus();

                DisplayCurrentQuestion();

                UpdateNavigationButtons();
            }

            LoadAttempts();
        }

        // =====================================================
        // ATTEMPT
        // =====================================================

        void LoadOrCreateAttempt()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"
                SELECT TOP 1 attempt_id
                FROM quiz_attempts
                WHERE quiz_id=@q
                AND user_id=@u
                AND score IS NULL", con);

                cmd.Parameters.AddWithValue("@q", Session["quiz"]);

                cmd.Parameters.AddWithValue("@u", Session["user_id"]);

                object r = cmd.ExecuteScalar();

                int attemptId;

                if (r != null)
                {
                    attemptId = Convert.ToInt32(r);
                }
                else
                {
                    cmd = new SqlCommand(@"
                    INSERT INTO quiz_attempts
                    (
                        quiz_id,
                        user_id
                    )

                    OUTPUT INSERTED.attempt_id

                    VALUES
                    (
                        @q,
                        @u
                    )", con);

                    cmd.Parameters.AddWithValue("@q", Session["quiz"]);

                    cmd.Parameters.AddWithValue("@u", Session["user_id"]);

                    attemptId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                ViewState["AttemptId"] = attemptId;
            }
        }

        int GetAttemptId()
        {
            return Convert.ToInt32(ViewState["AttemptId"]);
        }

        // =====================================================
        // QUIZ INFO
        // =====================================================

        void LoadQuiz()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    title,
                    description,
                    passing_score
                FROM quizzes
                WHERE quiz_id=@id", con);

                cmd.Parameters.AddWithValue("@id", Session["quiz"]);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    QuizTitle.Text = r["title"].ToString();

                    QuizDecription.Text = r["description"].ToString();

                    PassingScore.Text = "Passing Score: " + r["passing_score"];
                }
            }
        }

        // =====================================================
        // QUESTIONS
        // =====================================================

        void LoadQuestions()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT
                    question_id,
                    question_text
                FROM quiz_questions
                WHERE quiz_id=@q
                ORDER BY question_order", con);

                da.SelectCommand.Parameters.AddWithValue("@q", Session["quiz"]);

                da.Fill(dt);
            }

            ViewState["Questions"] = dt;
        }

        DataTable GetQuestions()
        {
            return (DataTable)ViewState["Questions"];
        }

        // =====================================================
        // DISPLAY QUESTION
        // =====================================================

        void DisplayCurrentQuestion()
        {
            DataTable questions = GetQuestions();

            int currentIndex = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            if (currentIndex < 0 || currentIndex >= questions.Rows.Count)
            {
                return;
            }

            int questionId = Convert.ToInt32(questions.Rows[currentIndex]["question_id"]);

            QuizQuestion.Text = questions.Rows[currentIndex]["question_text"].ToString();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    option_id,
                    option_text
                FROM quiz_options
                WHERE question_id=@q
                ORDER BY option_id", con);

                cmd.Parameters.AddWithValue("@q", questionId);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                Button[] btns =
                {
                    QuizOption1,
                    QuizOption2,
                    QuizOption3,
                    QuizOption4
                };

                int i = 0;

                while (r.Read())
                {
                    btns[i].Visible = true;

                    btns[i].Text = r["option_text"].ToString();

                    btns[i].CommandArgument = r["option_id"].ToString();

                    btns[i].BackColor = Color.White;

                    btns[i].ForeColor = Color.Black;

                    i++;
                }
            }

            HighlightSelectedAnswer(questionId);
        }

        // =====================================================
        // QUESTION STATUS
        // =====================================================

        void RenderQuestionStatus()
        {
            QuestionContainer.Controls.Clear();

            DataTable questions = GetQuestions();

            int currentIndex = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            for (int i = 0; i < questions.Rows.Count; i++)
            {
                int questionId = Convert.ToInt32(questions.Rows[i]["question_id"]);

                Label lbl = new Label();

                lbl.Text = (i + 1).ToString();

                lbl.BorderStyle = BorderStyle.Solid;

                lbl.BorderWidth = 1;

                lbl.Style["margin-right"] = "5px";

                if (IsAnswered(questionId))
                {
                    lbl.BackColor = Color.LightGreen;
                }
                else
                {
                    lbl.BackColor = Color.Black;

                    lbl.ForeColor = Color.White;
                }

                if (i == currentIndex)
                {
                    lbl.BackColor = Color.DarkGreen;

                    lbl.ForeColor = Color.White;
                }

                QuestionContainer.Controls.Add(lbl);
            }
        }

        bool IsAnswered(int questionId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM quiz_answers
                WHERE attempt_id=@a
                AND question_id=@q", con);

                cmd.Parameters.AddWithValue("@a", GetAttemptId());

                cmd.Parameters.AddWithValue("@q", questionId);

                con.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0;
            }
        }

        // =====================================================
        // SELECT OPTION
        // =====================================================

        protected void Option_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int optionId = Convert.ToInt32(btn.CommandArgument);

            DataTable questions = GetQuestions();

            int currentIndex = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            int questionId = Convert.ToInt32(questions.Rows[currentIndex]["question_id"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"
                IF EXISTS
                (
                    SELECT 1
                    FROM quiz_answers
                    WHERE attempt_id=@a
                    AND question_id=@q
                )

                UPDATE quiz_answers

                SET selected_option_id=@o

                WHERE attempt_id=@a
                AND question_id=@q

                ELSE

                INSERT INTO quiz_answers
                (
                    attempt_id,
                    question_id,
                    selected_option_id
                )

                VALUES
                (
                    @a,
                    @q,
                    @o
                )", con);

                cmd.Parameters.AddWithValue("@a", GetAttemptId());

                cmd.Parameters.AddWithValue("@q", questionId);

                cmd.Parameters.AddWithValue("@o", optionId);

                cmd.ExecuteNonQuery();
            }

            RenderQuestionStatus();

            HighlightSelectedAnswer(questionId);

            Next_Click(this, EventArgs.Empty);
        }

        // =====================================================
        // HIGHLIGHT ANSWER
        // =====================================================

        void HighlightSelectedAnswer(int questionId)
        {
            int selectedOptionId = -1;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT selected_option_id
                FROM quiz_answers
                WHERE attempt_id=@a
                AND question_id=@q", con);

                cmd.Parameters.AddWithValue("@a", GetAttemptId());

                cmd.Parameters.AddWithValue("@q", questionId);

                con.Open();

                object r = cmd.ExecuteScalar();

                if (r != null)
                {
                    selectedOptionId = Convert.ToInt32(r);
                }
            }

            Button[] btns =
            {
                QuizOption1,
                QuizOption2,
                QuizOption3,
                QuizOption4
            };

            foreach (Button btn in btns)
            {
                btn.BackColor = Color.White;

                btn.ForeColor = Color.Black;

                if (btn.CommandArgument == selectedOptionId.ToString())
                {
                    btn.BackColor = Color.DarkBlue;

                    btn.ForeColor = Color.White;
                }
            }
        }

        // =====================================================
        // NAVIGATION
        // =====================================================

        protected void Next_Click(object sender, EventArgs e)
        {
            int current = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            if (current < GetQuestions().Rows.Count - 1)
            {
                current++;

                ViewState["CurrentQuestionIndex"] = current;

                DisplayCurrentQuestion();

                RenderQuestionStatus();

                UpdateNavigationButtons();
            }
        }

        protected void Prev_Click(object sender, EventArgs e)
        {
            int current = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            if (current > 0)
            {
                current--;

                ViewState["CurrentQuestionIndex"] = current;

                DisplayCurrentQuestion();

                RenderQuestionStatus();

                UpdateNavigationButtons();
            }
        }

        void UpdateNavigationButtons()
        {
            int current = Convert.ToInt32(ViewState["CurrentQuestionIndex"]);

            int total = GetQuestions().Rows.Count;

            PrevBtn.Enabled = current > 0;

            NextBtn.Enabled = current < total - 1;
        }

        // =====================================================
        // COMPLETE QUIZ
        // =====================================================

        protected void Complete_Click(object sender, EventArgs e)
        {
            int correct = 0;

            DataTable questions = GetQuestions();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                foreach (DataRow row in questions.Rows)
                {
                    int questionId = Convert.ToInt32(row["question_id"]);

                    SqlCommand cmd = new SqlCommand(@"
                    SELECT o.is_correct
                    FROM quiz_answers a
                    JOIN quiz_options o
                    ON a.selected_option_id=o.option_id
                    WHERE a.attempt_id=@a
                    AND a.question_id=@q", con);

                    cmd.Parameters.AddWithValue("@a", GetAttemptId());

                    cmd.Parameters.AddWithValue("@q", questionId);

                    object r = cmd.ExecuteScalar();

                    if (r != null && Convert.ToBoolean(r))
                    {
                        correct++;
                    }
                }

                int total = questions.Rows.Count;

                int score = (correct * 100) / total;

                int passing = Convert.ToInt32(
                    PassingScore.Text.Replace("Passing Score: ", ""));

                bool passed = score >= passing;

                SqlCommand update = new SqlCommand(@"
                UPDATE quiz_attempts
                SET
                    score=@s,
                    passed=@p
                WHERE attempt_id=@a", con);

                update.Parameters.AddWithValue("@s", score);

                update.Parameters.AddWithValue("@p", passed);

                update.Parameters.AddWithValue("@a", GetAttemptId());

                update.ExecuteNonQuery();

                Response.Write(
                    "<script>" +
                    "alert('Quiz Completed! Score: "
                    + score +
                    "%');" +
                    "window.location='StudentDashboard.aspx';" +
                    "</script>");
            }
        }

        // =====================================================
        // BACK
        // =====================================================

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentDashboard.aspx");
        }

        void LoadAttempts()
        {
            Attempt.Controls.Clear();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"
                SELECT
                    attempt_id,
                    score,
                    attempted_at
                FROM quiz_attempts
                WHERE user_id=@user_id
                AND quiz_id=@quiz_id
                AND score IS NOT NULL
                ORDER BY attempted_at DESC";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);

                cmd.Parameters.AddWithValue("@quiz_id", Session["quiz"]);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                int count = 1;

                while (r.Read())
                {
                    Label lbl = new Label();

                    lbl.Text =
                        "Attempt "
                        + count
                        + " | Score: "
                        + r["score"].ToString()
                        + "%";

                    Attempt.Controls.Add(lbl);

                    Attempt.Controls.Add(
                        new Literal()
                        {
                            Text = "<br/><br/>"
                        });

                    count++;
                }
            }
        }
    }
}