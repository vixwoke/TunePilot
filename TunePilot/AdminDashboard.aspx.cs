using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TunePilot
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourseGrid("", "c.title");
                BindStudentList("");
            }
        }
        protected void sortCourseddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCourseGrid(searchCoursetb.Text.Trim(), GetSortBy());
        }

        protected void searchCoursebtn_Click(object sender, EventArgs e)
        {
            BindCourseGrid(searchCoursetb.Text.Trim(), GetSortBy());
        }

        protected void addCoursebtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddCourse.aspx");
        }

        protected void coursesgv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            coursesgv.PageIndex = e.NewPageIndex;
            BindCourseGrid(searchCoursetb.Text.Trim(), GetSortBy());
        }

        protected void coursesgv_SelectedIndexChanged(object sender, EventArgs e)
        {
            int courseId = (int)coursesgv.SelectedDataKey.Value;

            string queryCourse = @"
        SELECT c.course_id, c.title, c.description, c.difficulty_level, i.name AS instrument, c.created_at
        FROM courses c
        JOIN instruments i ON c.instrument_id = i.instrument_id
        WHERE c.course_id = @id";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(queryCourse, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", courseId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                coursedv.DataSource = dt;
                coursedv.DataBind();
            }

            string queryLessons = @"
        SELECT lesson_id, lesson_order, title, duration_minutes
        FROM lessons
        WHERE course_id = @id
        ORDER BY lesson_order";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(queryLessons, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", courseId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lessonsgv.DataSource = dt;
                lessonsgv.DataBind();
            }

            courseDetailpnl.Visible = true;
        }

        protected void coursesgv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int courseId = (int)coursesgv.DataKeys[e.RowIndex].Value;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM courses WHERE course_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", courseId);
                cmd.ExecuteNonQuery();
            }

            BindCourseGrid("", "c.title");
            courseDetailpnl.Visible = false;
        }

        protected void coursedv_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            int courseId = (int)coursedv.DataKey.Value;
            string title = e.NewValues["title"].ToString();
            string desc = e.NewValues["description"].ToString();
            string diff = e.NewValues["difficulty_level"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            UPDATE courses 
            SET title = @title, description = @desc, difficulty_level = @diff
            WHERE course_id = @id", conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@diff", diff);
                cmd.Parameters.AddWithValue("@id", courseId);
                cmd.ExecuteNonQuery();
            }

            coursedv.ChangeMode(DetailsViewMode.ReadOnly);
            coursesgv_SelectedIndexChanged(sender, e);
            BindCourseGrid(searchCoursetb.Text.Trim(), "c.title");
        }

        protected void searchStudentbtn_Click(object sender, EventArgs e)
        {
            BindStudentList(searchStudenttb.Text.Trim());
        }

        protected void addStudentbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/register.aspx");
        }

        protected void studentslv_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int userId = Convert.ToInt32(e.CommandArgument);

                string queryStudent = @"
            SELECT user_id, first_name, last_name, username, email, active, created_at
            FROM users
            WHERE user_id = @id";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlDataAdapter da = new SqlDataAdapter(queryStudent, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@id", userId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    studentdv.DataSource = dt;
                    studentdv.DataBind();
                }

                string queryEnrollments = @"
            SELECT e.enrollment_id, c.title, e.enrolled_at, e.status
            FROM enrollments e JOIN courses c ON e.course_id = c.course_id
            WHERE e.user_id = @id";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlDataAdapter da = new SqlDataAdapter(queryEnrollments, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@id", userId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    enrollmentsgv.DataSource = dt;
                    enrollmentsgv.DataBind();
                }

                studentDetailpnl.Visible = true;
            }
        }
        protected void studentdv_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            int userId = (int)studentdv.DataKey.Value;
            string fname = e.NewValues["first_name"].ToString();
            string lname = e.NewValues["last_name"].ToString();
            string username = e.NewValues["username"].ToString();
            string email = e.NewValues["email"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            UPDATE users
            SET first_name = @fname, last_name = @lname, username = @username, email = @email
            WHERE user_id = @id", conn);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();
            }

            string queryStudent = @"
        SELECT user_id, first_name, last_name, username, email, active, created_at
        FROM users 
        WHERE user_id = @id";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(queryStudent, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", userId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentdv.ChangeMode(DetailsViewMode.ReadOnly);
                studentdv.DataSource = dt;
                studentdv.DataBind();
            }

            BindStudentList(searchStudenttb.Text.Trim());
        }

        private void BindStudentList(string search)
        {
            string query = @"
        SELECT user_id, first_name, last_name, username, email, active
        FROM users
        WHERE role = 'student'
        AND (first_name + ' ' + last_name LIKE @search OR CAST(user_id AS NVARCHAR) LIKE @search)
        ORDER BY last_name";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@search", "%" + search + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentslv.DataSource = dt;
                studentslv.DataBind();
            }
        }
        protected void coursedv_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            coursedv.ChangeMode(e.NewMode);
            int courseId = (int)coursesgv.SelectedDataKey.Value;
            BindCourseDetail(courseId);
        }
        private void BindCourseDetail(int courseId)
        {
            string query = @"
        SELECT c.course_id, c.title, c.description, c.difficulty_level, i.name AS instrument, c.created_at 
        FROM courses c JOIN instruments i ON c.instrument_id = i.instrument_id 
        WHERE c.course_id = @id";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", courseId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                coursedv.DataSource = dt;
                coursedv.DataBind();
            }
        }
        private void BindCourseGrid(string search, string sortBy)
        {
            string query = @"
                SELECT c.course_id, c.title, i.name AS instrument, c.difficulty_level, 
                (SELECT COUNT(*) FROM lessons l  WHERE l.course_id = c.course_id) AS lesson_count, (SELECT COUNT(*) 
                FROM quizzes q JOIN lessons l ON q.lesson_id = l.lesson_id 
                WHERE l.course_id = c.course_id) AS quiz_count
                FROM courses c JOIN instruments i ON c.instrument_id = i.instrument_id
                WHERE c.title LIKE @search OR CAST(c.course_id AS NVARCHAR) LIKE @search
                ORDER BY " + sortBy;

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@search", "%" + search + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                coursesgv.DataSource = dt;
                coursesgv.DataBind();
            }
        }

        private string GetSortBy()
        {
            switch (sortCourseddl.SelectedValue)
            {
                case "instrument": return "i.name";
                case "difficulty_level": return "c.difficulty_level";
                default: return "c.title";
            }
        }
    }
}