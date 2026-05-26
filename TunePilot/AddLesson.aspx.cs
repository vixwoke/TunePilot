using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class AddLesson : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;
        int courseId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null || Session["role"].ToString() != "admin")
                Response.Redirect("~/login.aspx");

            if (Request.QueryString["course_id"] == null)
                Response.Redirect("~/AdminDashboard.aspx");

            courseId = Convert.ToInt32(Request.QueryString["course_id"]);

            if (!IsPostBack)
            {
                string query = "SELECT title FROM courses WHERE course_id = @id";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", courseId);
                    courseTitlelbl.Text = "Course: " + cmd.ExecuteScalar().ToString();
                }

                if (Session["new_lesson_id"] != null)
                {
                    quizpnl.Visible = true;
                    saveLessonbtn.Enabled = false;
                }
                if (Session["new_quiz_id"] != null)
                {
                    questionspnl.Visible = true;
                    saveQuizbtn.Enabled = false;
                    BindQuestions();
                }
                if (Session["new_question_id"] != null)
                {
                    optionspnl.Visible = true;
                    BindOptions(Convert.ToInt32(Session["new_question_id"]));
                }
            }
        }

        protected void saveLessonbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lessonTitletb.Text.Trim()))
            {
                messagelbl.Text = "Lesson title is required.";
                messagelbl.Visible = true;
                return;
            }

            int order = string.IsNullOrEmpty(ordertb.Text) ? 0 : Convert.ToInt32(ordertb.Text);
            int duration = string.IsNullOrEmpty(durationtb.Text) ? 0 : Convert.ToInt32(durationtb.Text);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO lessons (course_id, title, summary, lesson_order, duration_minutes)
                    VALUES (@course, @title, @summary, @order, @duration);
                    SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@course", courseId);
                cmd.Parameters.AddWithValue("@title", lessonTitletb.Text.Trim());
                cmd.Parameters.AddWithValue("@summary", summarytb.Text.Trim());
                cmd.Parameters.AddWithValue("@order", order);
                cmd.Parameters.AddWithValue("@duration", duration);

                int newLessonId = Convert.ToInt32(cmd.ExecuteScalar());
                Session["new_lesson_id"] = newLessonId;
            }

            saveLessonbtn.Enabled = false;
            quizpnl.Visible = true;
            messagelbl.Visible = false;
        }

        protected void saveQuizbtn_Click(object sender, EventArgs e)
        {
            int lessonId = Convert.ToInt32(Session["new_lesson_id"]);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand check = new SqlCommand(
                    "SELECT COUNT(*) FROM quizzes WHERE lesson_id = @id", conn);
                check.Parameters.AddWithValue("@id", lessonId);
                int exists = Convert.ToInt32(check.ExecuteScalar());
                if (exists > 0)
                {
                    messagelbl.Text = "A quiz already exists for this lesson.";
                    messagelbl.Visible = true;
                    return;
                }
            }

            if (string.IsNullOrEmpty(quizTitletb.Text.Trim()))
            {
                messagelbl.Text = "Quiz title is required.";
                messagelbl.Visible = true;
                return;
            }

            int passingScore = string.IsNullOrEmpty(passingStoretb.Text) ? 80 : Convert.ToInt32(passingStoretb.Text);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO quizzes (lesson_id, title, description, passing_score, created_at)
                                                  VALUES (@lesson, @title, @desc, @score, @created);
                                                  SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@lesson", lessonId);
                cmd.Parameters.AddWithValue("@title", quizTitletb.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", quizDesctb.Text.Trim());
                cmd.Parameters.AddWithValue("@score", passingScore);
                cmd.Parameters.AddWithValue("@created", DateTime.Now);

                int newQuizId = Convert.ToInt32(cmd.ExecuteScalar());
                Session["new_quiz_id"] = newQuizId;
            }

            saveQuizbtn.Enabled = false;
            questionspnl.Visible = true;
            messagelbl.Visible = false;
            BindQuestions();
        }

        protected void addQuestionbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(questionTexttb.Text.Trim()))
            {
                messagelbl.Text = "Question text is required.";
                messagelbl.Visible = true;
                return;
            }

            int quizId = Convert.ToInt32(Session["new_quiz_id"]);
            int order = string.IsNullOrEmpty(questionOrdertb.Text) ? 0 : Convert.ToInt32(questionOrdertb.Text);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO quiz_questions (quiz_id, question_text, question_order)
                                                  VALUES (@quiz, @text, @order)", conn);
                cmd.Parameters.AddWithValue("@quiz", quizId);
                cmd.Parameters.AddWithValue("@text", questionTexttb.Text.Trim());
                cmd.Parameters.AddWithValue("@order", order);
                cmd.ExecuteNonQuery();
            }

            questionTexttb.Text = "";
            questionOrdertb.Text = "";
            messagelbl.Visible = false;
            BindQuestions();
        }

        protected void questionsgv_SelectedIndexChanged(object sender, EventArgs e)
        {
            int questionId = (int)questionsgv.SelectedDataKey.Value;
            Session["new_question_id"] = questionId;
            BindOptions(questionId);
            optionspnl.Visible = true;
        }

        protected void addOptionbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(optionTexttb.Text.Trim()))
            {
                messagelbl.Text = "Option text is required.";
                messagelbl.Visible = true;
                return;
            }

            int questionId = Convert.ToInt32(Session["new_question_id"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO quiz_options (question_id, option_text, is_correct)
                                                  VALUES (@question, @text, @correct)", conn);
                cmd.Parameters.AddWithValue("@question", questionId);
                cmd.Parameters.AddWithValue("@text", optionTexttb.Text.Trim());
                cmd.Parameters.AddWithValue("@correct", isCorrectcb.Checked ? 1 : 0);
                cmd.ExecuteNonQuery();
            }

            optionTexttb.Text = "";
            isCorrectcb.Checked = false;
            messagelbl.Visible = false;
            BindOptions(questionId);
        }

        protected void optionsgv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            optionsgv.EditIndex = e.NewEditIndex;
            BindOptions(Convert.ToInt32(Session["new_question_id"]));
        }

        protected void optionsgv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            optionsgv.EditIndex = -1;
            BindOptions(Convert.ToInt32(Session["new_question_id"]));
        }

        protected void optionsgv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int optionId = (int)optionsgv.DataKeys[e.RowIndex].Value;
            string text = e.NewValues["option_text"].ToString();
            bool isCorrect = Convert.ToBoolean(e.NewValues["is_correct"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE quiz_options
                    SET option_text = @text, is_correct = @correct
                    WHERE option_id = @id", conn);
                cmd.Parameters.AddWithValue("@text", text);
                cmd.Parameters.AddWithValue("@correct", isCorrect);
                cmd.Parameters.AddWithValue("@id", optionId);
                cmd.ExecuteNonQuery();
            }

            optionsgv.EditIndex = -1;
            BindOptions(Convert.ToInt32(Session["new_question_id"]));
        }

        private void BindQuestions()
        {
            int quizId = Convert.ToInt32(Session["new_quiz_id"]);

            string query = @"
                SELECT question_id, question_text, question_order
                FROM quiz_questions
                WHERE quiz_id = @id
                ORDER BY question_order";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", quizId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                questionsgv.DataSource = dt;
                questionsgv.DataBind();
            }
        }

        private void BindOptions(int questionId)
        {
            string query = @"
                SELECT option_id, option_text, is_correct
                FROM quiz_options
                WHERE question_id = @id";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", questionId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                optionsgv.DataSource = dt;
                optionsgv.DataBind();
            }
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Session.Remove("new_lesson_id");
            Session.Remove("new_quiz_id");
            Session.Remove("new_question_id");
            Response.Redirect("~/AdminDashboard.aspx");
        }
    }
}