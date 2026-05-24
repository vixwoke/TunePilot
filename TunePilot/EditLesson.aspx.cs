using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class EditLesson : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;
        int lessonId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("~/AdminDashboard.aspx");

            lessonId = Convert.ToInt32(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                BindLessonDetail();
                BindLessonContents();
                BindQuizDetail();
                BindQuestions();
            }
        }

        private void BindLessonDetail()
        {
            string query = @"
                SELECT lesson_id, title, summary, lesson_order, duration_minutes
                FROM lessons
                WHERE lesson_id = @id";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", lessonId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lessondv.DataSource = dt;
                lessondv.DataBind();
            }
        }

        private void BindLessonContents()
        {
            string query = @"
                SELECT content_id, content_type, title, body, media_url, content_order
                FROM lesson_contents
                WHERE lesson_id = @id
                ORDER BY content_order";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", lessonId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lessonContentsgv.DataSource = dt;
                lessonContentsgv.DataBind();
            }
        }

        protected void lessondv_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            lessondv.ChangeMode(e.NewMode);
            BindLessonDetail();
        }

        protected void lessondv_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            string title = e.NewValues["title"].ToString();
            string summary = e.NewValues["summary"].ToString();
            int order = Convert.ToInt32(e.NewValues["lesson_order"]);
            int duration = Convert.ToInt32(e.NewValues["duration_minutes"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE lessons
                    SET title = @title, summary = @summary, lesson_order = @order, duration_minutes = @duration
                    WHERE lesson_id = @id", conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@summary", summary);
                cmd.Parameters.AddWithValue("@order", order);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@id", lessonId);
                cmd.ExecuteNonQuery();
            }

            lessondv.ChangeMode(DetailsViewMode.ReadOnly);
            BindLessonDetail();
        }

        protected void lessonContentsgv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lessonContentsgv.EditIndex = e.NewEditIndex;
            BindLessonContents();
        }

        protected void lessonContentsgv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lessonContentsgv.EditIndex = -1;
            BindLessonContents();
        }

        protected void lessonContentsgv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int contentId = (int)lessonContentsgv.DataKeys[e.RowIndex].Value;
            string title = e.NewValues["title"].ToString();
            string body = e.NewValues["body"] != null ? e.NewValues["body"].ToString() : null;
            string mediaUrl = e.NewValues["media_url"] != null ? e.NewValues["media_url"].ToString() : null;
            int order = Convert.ToInt32(e.NewValues["content_order"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE lesson_contents
                    SET title = @title, body = @body, media_url = @mediaUrl, content_order = @order
                    WHERE content_id = @id", conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@body", (object)body ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@mediaUrl", (object)mediaUrl ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@order", order);
                cmd.Parameters.AddWithValue("@id", contentId);
                cmd.ExecuteNonQuery();
            }

            lessonContentsgv.EditIndex = -1;
            BindLessonContents();
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminDashboard.aspx");
        }

        private void BindQuizDetail()
        {
            string query = @"
        SELECT quiz_id, title, description, passing_score
        FROM quizzes
        WHERE lesson_id = @id";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", lessonId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                quizdv.DataSource = dt;
                quizdv.DataBind();
            }
        }

        private void BindQuestions()
        {
            string query = @"
        SELECT q.question_id, q.question_text, q.question_order
        FROM quiz_questions q JOIN quizzes qz ON q.quiz_id = qz.quiz_id
        WHERE qz.lesson_id = @id
        ORDER BY q.question_order";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", lessonId);
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

        protected void quizdv_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            quizdv.ChangeMode(e.NewMode);
            BindQuizDetail();
        }

        protected void quizdv_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            int quizId = (int)quizdv.DataKey.Value;
            string title = e.NewValues["title"].ToString();
            string desc = e.NewValues["description"].ToString();
            int passingScore = Convert.ToInt32(e.NewValues["passing_score"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            UPDATE quizzes
            SET title = @title, description = @desc, passing_score = @score
            WHERE quiz_id = @id", conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@score", passingScore);
                cmd.Parameters.AddWithValue("@id", quizId);
                cmd.ExecuteNonQuery();
            }

            quizdv.ChangeMode(DetailsViewMode.ReadOnly);
            BindQuizDetail();
        }

        protected void questionsgv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            questionsgv.EditIndex = e.NewEditIndex;
            BindQuestions();
        }

        protected void questionsgv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            questionsgv.EditIndex = -1;
            BindQuestions();
        }

        protected void questionsgv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int questionId = (int)questionsgv.DataKeys[e.RowIndex].Value;
            string text = e.NewValues["question_text"].ToString();
            int order = Convert.ToInt32(e.NewValues["question_order"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            UPDATE quiz_questions
            SET question_text = @text, question_order = @order
            WHERE question_id = @id", conn);
                cmd.Parameters.AddWithValue("@text", text);
                cmd.Parameters.AddWithValue("@order", order);
                cmd.Parameters.AddWithValue("@id", questionId);
                cmd.ExecuteNonQuery();
            }

            questionsgv.EditIndex = -1;
            BindQuestions();
        }

        protected void questionsgv_SelectedIndexChanged(object sender, EventArgs e)
        {
            int questionId = (int)questionsgv.SelectedDataKey.Value;
            BindOptions(questionId);
            optionspnl.Visible = true;
        }

        protected void optionsgv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            optionsgv.EditIndex = e.NewEditIndex;
            int questionId = (int)questionsgv.SelectedDataKey.Value;
            BindOptions(questionId);
        }

        protected void optionsgv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            optionsgv.EditIndex = -1;
            int questionId = (int)questionsgv.SelectedDataKey.Value;
            BindOptions(questionId);
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
            int questionId = (int)questionsgv.SelectedDataKey.Value;
            BindOptions(questionId);
        }
    }
}