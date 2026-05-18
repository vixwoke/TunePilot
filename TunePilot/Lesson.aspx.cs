using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TunePilot
{
    
    public partial class Lesson : System.Web.UI.Page
    {
        string connStr =
        ConfigurationManager
        .ConnectionStrings["TunePilotDB"]
        .ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["course"] == null
                || Session["lesson"] == null)
            {
                Response.Redirect(
                    "StudentDashboard.aspx");

                return;
            }


            if (!IsPostBack)
            {
                ValidateCourseLesson();

                LoadCourseInfo();

                LoadLessonDetail();

                EnsureProgress();

                UpdateCompleteButton();
            }
            LoadLessons();
        }

        // =====================================================
        // SECURITY CHECK
        // =====================================================

        void ValidateCourseLesson()
        {
            int courseId =
                Convert.ToInt32(
                    Session["course"]);

            int lessonId =
                Convert.ToInt32(
                    Session["lesson"]);

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                SqlCommand cmd =
                    new SqlCommand(@"

                SELECT COUNT(*)

                FROM lessons

                WHERE lesson_id=@l
                AND course_id=@c", con);

                cmd.Parameters.AddWithValue(
                    "@l",
                    lessonId);

                cmd.Parameters.AddWithValue(
                    "@c",
                    courseId);

                con.Open();

                int ok =
                    (int)cmd.ExecuteScalar();

                if (ok == 0)
                {
                    Session["lesson"] =
                        GetFirstLesson(courseId);
                }
            }
        }

        int GetFirstLesson(int courseId)
        {
            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                SqlCommand cmd =
                    new SqlCommand(@"

                SELECT TOP 1 lesson_id

                FROM lessons

                WHERE course_id=@c

                ORDER BY lesson_order", con);

                cmd.Parameters.AddWithValue(
                    "@c",
                    courseId);

                con.Open();

                object r =
                    cmd.ExecuteScalar();

                return Convert.ToInt32(r);
            }
        }

        // =====================================================
        // LOAD WHOLE PAGE
        // =====================================================

        void LoadPage()
        {
            LoadCourseInfo();

            LoadLessons();

            LoadLessonDetail();

            UpdateCompleteButton();
        }

        // =====================================================
        // COURSE INFO
        // =====================================================

        void LoadCourseInfo()
        {
            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                SqlCommand cmd =
                    new SqlCommand(@"

                SELECT
                    i.name,
                    c.difficulty_level

                FROM courses c

                JOIN instruments i
                    ON c.instrument_id=i.instrument_id

                WHERE c.course_id=@id", con);

                cmd.Parameters.AddWithValue(
                    "@id",
                    Session["course"]);

                con.Open();

                SqlDataReader r =
                    cmd.ExecuteReader();

                if (r.Read())
                {
                    LabelInstrument.Text =
                        r["name"].ToString();

                    LabelLevel.Text =
                        r["difficulty_level"].ToString();
                }
            }
        }

        // =====================================================
        // LESSON LIST
        // =====================================================

        void LoadLessons()
        {
            LessonContainer.Controls.Clear();

            int courseId =
                Convert.ToInt32(
                    Session["course"]);

            int currentLesson =
                Convert.ToInt32(
                    Session["lesson"]);

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                SqlCommand cmd =
                    new SqlCommand(@"

                SELECT
                    l.lesson_id,
                    l.title,
                    l.lesson_order,

                    ISNULL(
                        p.status,
                        ''
                    ) AS status

                FROM lessons l

                LEFT JOIN progress p
                    ON p.lesson_id=l.lesson_id
                    AND p.user_id=@u

                WHERE l.course_id=@c

                ORDER BY l.lesson_order", con);

                cmd.Parameters.AddWithValue(
                    "@c",
                    courseId);

                cmd.Parameters.AddWithValue(
                    "@u",
                    Session["user_id"] ?? 0);

                con.Open();

                SqlDataReader r =
                    cmd.ExecuteReader();

                while (r.Read())
                {
                    int lessonId =
                        Convert.ToInt32(
                            r["lesson_id"]);

                    string title =
                        r["title"].ToString();

                    string status =
                        r["status"].ToString();

                    LinkButton btn =
                        new LinkButton();

                    btn.Text = title;

                    btn.CommandArgument =
                        lessonId.ToString();

                    btn.Click += SelectLesson;

                    string cls =
                        "lesson-btn ";

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

                    btn.CssClass = cls;

                    LessonContainer.Controls.Add(btn);

                    LessonContainer.Controls.Add(
                        new LiteralControl("<br/>"));

                }
            }
        }

        // =====================================================
        // LOAD CURRENT LESSON
        // =====================================================

        void LoadLessonDetail()
        {
            int lessonId =
                Convert.ToInt32(
                    Session["lesson"]);

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                SqlCommand cmd =
                    new SqlCommand(@"

                SELECT
                    title,
                    summary,
                    duration_minutes

                FROM lessons

                WHERE lesson_id=@id", con);

                cmd.Parameters.AddWithValue(
                    "@id",
                    lessonId);

                con.Open();

                SqlDataReader r =
                    cmd.ExecuteReader();

                if (r.Read())
                {
                    LessonTitle.Text =
                        r["title"].ToString();

                    LessonDesc.Text =
                        r["summary"].ToString();

                    LessonDuration.Text =
                        r["duration_minutes"]
                        + " min";
                }
            }

            LoadLessonContents();

            LoadVideo(lessonId);
        }

        // =====================================================
        // LESSON RESOURCES
        // =====================================================

        void LoadLessonContents()
        {
            ContentContainer.Controls.Clear();

            int lessonId =
                Convert.ToInt32(
                    Session["lesson"]);

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                SqlCommand cmd =
                    new SqlCommand(@"

                SELECT
                    title,
                    media_url,
                    content_type

                FROM lesson_contents

                WHERE lesson_id=@id

                ORDER BY content_id", con);

                cmd.Parameters.AddWithValue(
                    "@id",
                    lessonId);

                con.Open();

                SqlDataReader r =
                    cmd.ExecuteReader();

                while (r.Read())
                {
                    string title =
                        r["title"].ToString();

                    string url =
                        r["media_url"].ToString();

                    string type =
                        r["content_type"].ToString();

                    // skip mp4 because shown below
                    if (type == "video")
                        continue;

                    HyperLink link =
                        new HyperLink();

                    link.Text =
                        title
                        + " ("
                        + type
                        + ")"
                        +url;

                    link.NavigateUrl = url;

                    link.Target = "_blank";

                    ContentContainer.Controls.Add(link);

                    ContentContainer.Controls.Add(
                        new LiteralControl("<br/>"));
                }
            }
        }

        // =====================================================
        // VIDEO
        // =====================================================

        void LoadVideo(int lessonId)
        {
            VideoPlayer.Controls.Clear();

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                SqlCommand cmd =
                    new SqlCommand(@"

                SELECT TOP 1 media_url

                FROM lesson_contents

                WHERE lesson_id=@id
                AND content_type='video'", con);

                cmd.Parameters.AddWithValue(
                    "@id",
                    lessonId);

                con.Open();

                object url =
                    cmd.ExecuteScalar();

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
        // ENSURE PROGRESS
        // =====================================================

        void EnsureProgress()
        {
            if (Session["role"].ToString()
                == "guest")
            {
                return;
            }

            int userId =
                Convert.ToInt32(
                    Session["user_id"]);

            int lessonId =
                Convert.ToInt32(
                    Session["lesson"]);

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd =
                    new SqlCommand(@"

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

                cmd.Parameters.AddWithValue(
                    "@u",
                    userId);

                cmd.Parameters.AddWithValue(
                    "@l",
                    lessonId);

                cmd.ExecuteNonQuery();
            }
        }

        // =====================================================
        // COMPLETE BUTTON
        // =====================================================

        void UpdateCompleteButton()
        {
            if (Session["role"].ToString()
                == "guest")
            {
                CompleteBtn.Visible = false;

                return;
            }

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                SqlCommand cmd =
                    new SqlCommand(@"

                SELECT status

                FROM progress

                WHERE user_id=@u
                AND lesson_id=@l", con);

                cmd.Parameters.AddWithValue(
                    "@u",
                    Session["user_id"]);

                cmd.Parameters.AddWithValue(
                    "@l",
                    Session["lesson"]);

                con.Open();

                object r =
                    cmd.ExecuteScalar();

                if (r != null
                    &&
                    r.ToString() == "completed")
                {
                    CompleteBtn.Text =
                        "Completed";

                    CompleteBtn.ForeColor =
                        Color.Green;
                }
                else
                {
                    CompleteBtn.Text =
                        "Complete";

                    CompleteBtn.ForeColor =
                        Color.Black;
                }
            }
        }

        // =====================================================
        // SELECT LESSON
        // =====================================================

        protected void SelectLesson(
            object sender,
            EventArgs e)
        {
            LinkButton btn =
                (LinkButton)sender;

            Session["lesson"] =
                btn.CommandArgument;

            EnsureProgress();

            LoadPage();
        }

        // =====================================================
        // NEXT / PREV
        // =====================================================

        protected void Next_Click(
            object sender,
            EventArgs e)
        {
            Session["lesson"] =
                GetNextLesson();

            EnsureProgress();

            LoadPage();
        }

        protected void Prev_Click(
            object sender,
            EventArgs e)
        {
            Session["lesson"] =
                GetPrevLesson();

            EnsureProgress();

            LoadPage();
        }

        int GetNextLesson()
        {
            return GetAdjacentLesson(">");
        }

        int GetPrevLesson()
        {
            return GetAdjacentLesson("<");
        }

        int GetAdjacentLesson(string dir)
        {
            int courseId =
                Convert.ToInt32(
                    Session["course"]);

            int lessonId =
                Convert.ToInt32(
                    Session["lesson"]);

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                string op =
                    dir == ">"
                    ? ">"
                    : "<";

                string order =
                    dir == ">"
                    ? "ASC"
                    : "DESC";

                SqlCommand cmd =
                    new SqlCommand($@"

                SELECT TOP 1 lesson_id

                FROM lessons

                WHERE course_id=@c

                AND lesson_order {op}
                (
                    SELECT lesson_order
                    FROM lessons
                    WHERE lesson_id=@l
                )

                ORDER BY lesson_order {order}", con);

                cmd.Parameters.AddWithValue(
                    "@c",
                    courseId);

                cmd.Parameters.AddWithValue(
                    "@l",
                    lessonId);

                con.Open();

                object r =
                    cmd.ExecuteScalar();

                return r == null
                    ? lessonId
                    : Convert.ToInt32(r);
            }
        }

        // =====================================================
        // COMPLETE LESSON
        // =====================================================

        protected void Complete_Click(
            object sender,
            EventArgs e)
        {
            if (Session["role"].ToString()
                == "guest")
            {
                return;
            }

            int userId =
                Convert.ToInt32(
                    Session["user_id"]);

            int lessonId =
                Convert.ToInt32(
                    Session["lesson"]);

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd =
                    new SqlCommand(@"

                UPDATE progress

                SET status='completed'

                WHERE user_id=@u
                AND lesson_id=@l", con);

                cmd.Parameters.AddWithValue(
                    "@u",
                    userId);

                cmd.Parameters.AddWithValue(
                    "@l",
                    lessonId);

                cmd.ExecuteNonQuery();
            }

            LoadLessons();

            UpdateCompleteButton();
        }

        // =====================================================
        // BACK
        // =====================================================

        protected void Back_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "StudentDashboard.aspx");
        }
    }


}
