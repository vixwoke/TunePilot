using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class AddExam : System.Web.UI.Page
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

                if (Session["new_exam_id"] != null)
                {
                    questionspnl.Visible = true;
                    saveExambtn.Enabled = false;
                    BindQuestions();
                }
            }
        }

        protected void saveExambtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(examTitletb.Text.Trim()))
            {
                messagelbl.Text = "Exam title is required.";
                messagelbl.Visible = true;
                return;
            }

            int passingScore = string.IsNullOrEmpty(passingStoretb.Text) ? 80 : Convert.ToInt32(passingStoretb.Text);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO exams (course_id, title, description, passing_score, created_at)
                    VALUES (@course, @title, @desc, @score, @created);
                    SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@course", courseId);
                cmd.Parameters.AddWithValue("@title", examTitletb.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", examDesctb.Text.Trim());
                cmd.Parameters.AddWithValue("@score", passingScore);
                cmd.Parameters.AddWithValue("@created", DateTime.Now);

                int newExamId = Convert.ToInt32(cmd.ExecuteScalar());
                Session["new_exam_id"] = newExamId;
            }

            saveExambtn.Enabled = false;
            questionspnl.Visible = true;
            messagelbl.Visible = false;
        }

        protected void addQuestionbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(instructiontb.Text.Trim()) ||
                string.IsNullOrEmpty(expectedNotestb.Text.Trim()))
            {
                messagelbl.Text = "Instruction and expected notes are required.";
                messagelbl.Visible = true;
                return;
            }

            int examId = Convert.ToInt32(Session["new_exam_id"]);
            int order = string.IsNullOrEmpty(questionOrdertb.Text) ? 0 : Convert.ToInt32(questionOrdertb.Text);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO exam_questions (exam_id, instruction, expected_notes, question_order)
                    VALUES (@exam, @instruction, @notes, @order)", conn);
                cmd.Parameters.AddWithValue("@exam", examId);
                cmd.Parameters.AddWithValue("@instruction", instructiontb.Text.Trim());
                cmd.Parameters.AddWithValue("@notes", expectedNotestb.Text.Trim());
                cmd.Parameters.AddWithValue("@order", order);
                cmd.ExecuteNonQuery();
            }

            instructiontb.Text = "";
            expectedNotestb.Text = "";
            questionOrdertb.Text = "";
            messagelbl.Visible = false;
            BindQuestions();
        }

        private void BindQuestions()
        {
            int examId = Convert.ToInt32(Session["new_exam_id"]);

            string query = @"
                SELECT question_id, instruction, expected_notes, question_order
                FROM exam_questions
                WHERE exam_id = @id
                ORDER BY question_order";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", examId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                questionsgv.DataSource = dt;
                questionsgv.DataBind();
            }
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Session.Remove("new_exam_id");
            Response.Redirect("~/AdminDashboard.aspx");
        }
    }
}