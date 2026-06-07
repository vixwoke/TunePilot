using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class Lesson : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        // =====================================================
        // PAGE LOAD
        // =====================================================

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                ValidateCourseLesson();

                LoadLessonData();

                ViewState["CurrentLessonIndex"] = GetLessonIndex();

                LoadCourseInfo();

                LoadLessonDetail();

                EnsureProgress();

                UpdateCompleteButton();

                UpdateNavigationButtons();
            }

            LoadLessons();
        }

        // =====================================================
        // SECURITY CHECK
        // =====================================================

        void ValidateCourseLesson()
        {
            int courseId = Convert.ToInt32(Session["course"]);

            int lessonId = Convert.ToInt32(Session["lesson"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM lessons
                WHERE lesson_id=@l
                AND course_id=@c", con);

                cmd.Parameters.AddWithValue("@l", lessonId);

                cmd.Parameters.AddWithValue("@c", courseId);

                con.Open();

                int ok = (int)cmd.ExecuteScalar();

                if (ok == 0)
                {
                    Session["lesson"] = GetFirstLesson(courseId);
                }
            }
        }

        int GetFirstLesson(int courseId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT TOP 1 lesson_id
                FROM lessons
                WHERE course_id=@c
                ORDER BY lesson_order", con);

                cmd.Parameters.AddWithValue("@c", courseId);

                con.Open();

                object r = cmd.ExecuteScalar();

                return Convert.ToInt32(r);
            }
        }

        // =====================================================
        // LOAD PAGE
        // =====================================================

        void LoadPage()
        {
            LoadCourseInfo();

            LoadLessons();

            LoadLessonDetail();

            UpdateCompleteButton();

            UpdateNavigationButtons();
        }

        // =====================================================
        // LOAD LESSON DATA
        // =====================================================

        void LoadLessonData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT
                    lesson_id,
                    title,
                    lesson_order
                FROM lessons
                WHERE course_id=@c
                ORDER BY lesson_order", con);

                da.SelectCommand.Parameters.AddWithValue("@c", Session["course"]);

                da.Fill(dt);
            }

            ViewState["Lessons"] = dt;
        }

        DataTable GetLessons()
        {
            return (DataTable)ViewState["Lessons"];
        }

        int GetLessonIndex()
        {
            DataTable lessons = GetLessons();

            int currentLesson = Convert.ToInt32(Session["lesson"]);

            for (int i = 0; i < lessons.Rows.Count; i++)
            {
                if (Convert.ToInt32(lessons.Rows[i]["lesson_id"]) == currentLesson)
                {
                    return i;
                }
            }

            return 0;
        }

        // =====================================================
        // COURSE INFO
        // =====================================================

        void LoadCourseInfo()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    i.name,
                    c.difficulty_level
                FROM courses c
                JOIN instruments i
                    ON c.instrument_id=i.instrument_id
                WHERE c.course_id=@id", con);

                cmd.Parameters.AddWithValue("@id", Session["course"]);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    LabelInstrument.Text = r["name"].ToString();

                    LabelLevel.Text = r["difficulty_level"].ToString();
                }
            }
        }

        // =====================================================
        // LESSON LIST
        // =====================================================

        void LoadLessons()
        {
            LessonContainer.Controls.Clear();

            int currentLesson = Convert.ToInt32(Session["lesson"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    l.lesson_id,
                    l.title,
                    ISNULL(p.status, '') AS status
                FROM lessons l
                LEFT JOIN progress p
                    ON p.lesson_id=l.lesson_id
                    AND p.user_id=@u
                WHERE l.course_id=@c
                ORDER BY l.lesson_order", con);

                cmd.Parameters.AddWithValue("@c", Session["course"]);

                cmd.Parameters.AddWithValue("@u", Session["user_id"] ?? 0);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int lessonId = Convert.ToInt32(r["lesson_id"]);

                    string title = r["title"].ToString();

                    string status = r["status"].ToString();

                    Label lbl = new Label();

                    lbl.Text = title;

                    string cls = "lesson-btn ";

                    if (lessonId == currentLesson)
                    {
                        cls += "current";
                    }
                    else if (status == "completed")
                    {
                        cls += "completed";
                    }
                    else
                    {
                        cls += "normal";
                    }

                    lbl.CssClass = cls;

                    LessonContainer.Controls.Add(lbl);

                    LessonContainer.Controls.Add(new LiteralControl("<br/>"));
                }
            }
        }

        // =====================================================
        // LESSON DETAIL
        // =====================================================

        void LoadLessonDetail()
        {
            int lessonId = Convert.ToInt32(Session["lesson"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    title,
                    summary,
                    duration_minutes
                FROM lessons
                WHERE lesson_id=@id", con);

                cmd.Parameters.AddWithValue("@id", lessonId);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    LessonTitle.Text = r["title"].ToString();

                    LessonDesc.Text = r["summary"].ToString();

                    LessonDuration.Text = r["duration_minutes"] + " min";
                }
            }

            LoadLessonContents();

            LoadVideo(lessonId);
        }

        // =====================================================
        // LESSON CONTENTS
        // =====================================================

        void LoadLessonContents()
        {
            ContentContainer.Controls.Clear();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    title,
                    media_url,
                    content_type
                FROM lesson_contents
                WHERE lesson_id=@id
                ORDER BY content_id", con);

                cmd.Parameters.AddWithValue("@id", Session["lesson"]);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string type = r["content_type"].ToString();

                    if (type == "video")
                        continue;

                    HyperLink link = new HyperLink();

                    link.Text = r["title"].ToString();

                    link.NavigateUrl = r["media_url"].ToString();

                    link.Target = "_blank";

                    ContentContainer.Controls.Add(link);

                    ContentContainer.Controls.Add(new LiteralControl("<br/>"));
                }
            }
        }

        // =====================================================
        // VIDEO
        // =====================================================

        void LoadVideo(int lessonId)
        {
            VideoPlayer.Controls.Clear();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT TOP 1 media_url
                FROM lesson_contents
                WHERE lesson_id=@id
                AND content_type='video'", con);

                cmd.Parameters.AddWithValue("@id", lessonId);

                con.Open();

                object url = cmd.ExecuteScalar();

                if (url != null)
                {
                    VideoPlayer.Controls.Add(
                        new LiteralControl(
                            "<source src='"
                            + url.ToString()
                            + "' type='video/mp4' />"));
                }
            }
        }

        // =====================================================
        // PROGRESS
        // =====================================================

        void EnsureProgress()
        {
            if (Session["role"].ToString() == "guest")
            {
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"
                IF NOT EXISTS
                (
                    SELECT 1
                    FROM progress
                    WHERE user_id=@u
                    AND lesson_id=@l
                )

                INSERT INTO progress
                (
                    user_id,
                    lesson_id,
                    status
                )
                VALUES
                (
                    @u,
                    @l,
                    'in progress'
                )", con);

                cmd.Parameters.AddWithValue("@u", Session["user_id"]);

                cmd.Parameters.AddWithValue("@l", Session["lesson"]);

                cmd.ExecuteNonQuery();
            }
        }

        // =====================================================
        // COMPLETE BUTTON
        // =====================================================

        void UpdateCompleteButton()
        {
            if (Session["role"].ToString() == "guest")
            {
                CompleteBtn.Visible = false;
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT status
                FROM progress
                WHERE user_id=@u
                AND lesson_id=@l", con);

                cmd.Parameters.AddWithValue("@u", Session["user_id"]);

                cmd.Parameters.AddWithValue("@l", Session["lesson"]);

                con.Open();

                object r = cmd.ExecuteScalar();

                if (r != null && r.ToString() == "completed")
                {
                    CompleteBtn.Text = "Completed";
                    CompleteBtn.CssClass = "btn-complete completed";
                }
                else
                {
                    CompleteBtn.Text = "Complete";
                    CompleteBtn.CssClass = "btn-complete";
                }
            }
        }

        // =====================================================
        // NAVIGATION
        // =====================================================

        protected void Next_Click(object sender, EventArgs e)
        {
            int current = Convert.ToInt32(ViewState["CurrentLessonIndex"]);

            DataTable lessons = GetLessons();

            if (current < lessons.Rows.Count - 1)
            {
                current++;

                ViewState["CurrentLessonIndex"] = current;

                Session["lesson"] = lessons.Rows[current]["lesson_id"];

                EnsureProgress();

                LoadPage();
            }
        }

        protected void Prev_Click(object sender, EventArgs e)
        {
            int current = Convert.ToInt32(ViewState["CurrentLessonIndex"]);

            if (current > 0)
            {
                current--;

                ViewState["CurrentLessonIndex"] = current;

                DataTable lessons = GetLessons();

                Session["lesson"] = lessons.Rows[current]["lesson_id"];

                EnsureProgress();

                LoadPage();
            }
        }

        void UpdateNavigationButtons()
        {
            int current = Convert.ToInt32(ViewState["CurrentLessonIndex"]);

            int total = GetLessons().Rows.Count;

            PrevBtn.Enabled = current > 0;

            NextBtn.Enabled = current < total - 1;
        }

        // =====================================================
        // COMPLETE
        // =====================================================

        protected void Complete_Click(object sender, EventArgs e)
        {
            if (Session["role"].ToString() == "guest")
            {
                return;
            }

            string currentStatus = "";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand check = new SqlCommand(@"
                    SELECT status
                    FROM progress
                    WHERE user_id=@u
                    AND lesson_id=@l", con);

                check.Parameters.AddWithValue("@u", Session["user_id"]);

                check.Parameters.AddWithValue("@l", Session["lesson"]);

                object r = check.ExecuteScalar();

                if (r != null)
                {
                    currentStatus = r.ToString();
                }

                string newStatus =
                    currentStatus == "completed"
                    ? "in progress"
                    : "completed";

                SqlCommand cmd = new SqlCommand(@"
                    UPDATE progress
                    SET status=@s
                    WHERE user_id=@u
                    AND lesson_id=@l", con);

                cmd.Parameters.AddWithValue("@s", newStatus);

                cmd.Parameters.AddWithValue("@u", Session["user_id"]);

                cmd.Parameters.AddWithValue("@l", Session["lesson"]);

                cmd.ExecuteNonQuery();
            }

            LoadLessons();

            UpdateCompleteButton();

            string lessonTitle = LessonTitle.Text;

            string message =
                currentStatus == "completed"
                ? "Lesson marked as In Progress: "
                : "Thank you for completing lesson: ";

            string script =
                "alert('"
                + message
                + lessonTitle.Replace("'", "")
                + "');"
                + "window.location='StudentDashboard.aspx';";

            ClientScript.RegisterStartupScript(
                this.GetType(),
                "done",
                script,
                true);
        }

        // =====================================================
        // BACK
        // =====================================================

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentDashboard.aspx");
        }
    }
}