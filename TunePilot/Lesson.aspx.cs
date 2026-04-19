using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Drawing;

namespace TunePilot
{
    public partial class Lesson : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["course"] == null)
            {
                Response.Redirect("StudentDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadCourseInfo();
                LoadLessonButtons();

                if (Session["lesson"] == null)
                {
                    Session["lesson"] = GetLessonIdByOrder(1);
                }

                LoadLesson();
            }
        }

        void LoadCourseInfo()
        {
            int courseId = Convert.ToInt32(Session["course"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"SELECT i.name, c.difficulty_level
                             FROM courses c
                             JOIN instruments i ON c.instrument_id = i.instrument_id
                             WHERE c.course_id=@id";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", courseId);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    LabelInstrument.Text = r["name"].ToString();
                    LabelInstrumentDifficult.Text = r["difficulty_level"].ToString();
                }
            }
        }


        void LoadLessonButtons()
        {
            int courseId = Convert.ToInt32(Session["course"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"SELECT lesson_id, title, lesson_order
                             FROM lessons
                             WHERE course_id=@id
                             ORDER BY lesson_order";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", courseId);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int order = Convert.ToInt32(r["lesson_order"]);
                    string title = r["title"].ToString();
                    int lessonId = Convert.ToInt32(r["lesson_id"]);

                    if (order == 1)
                    {
                        Tutorial1.Text = title;
                        Tutorial1.CommandArgument = lessonId.ToString();
                    }
                    else if (order == 2)
                    {
                        Tutorial2.Text = title;
                        Tutorial2.CommandArgument = lessonId.ToString();
                    }
                    else if (order == 3)
                    {
                        Tutorial3.Text = title;
                        Tutorial3.CommandArgument = lessonId.ToString();
                    }
                }
            }
        }

        void LoadLesson()
        {
            if (Session["lesson"] == null) return;

            int lessonId = Convert.ToInt32(Session["lesson"]);

            LoadLessonInfo(lessonId);
            LoadContents(lessonId);
            LoadVideo(lessonId);
            SetCompleteButton();
            HighlightSelectedLesson();
        }


        void LoadLessonInfo(int lessonId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"SELECT title, summary, duration_minutes
                             FROM lessons
                             WHERE lesson_id=@id";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", lessonId);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    LabelLessonIndex.Text = r["title"].ToString();
                    LabelLessonDescription.Text = r["summary"].ToString();
                    LabelDuration.Text = r["duration_minutes"] + " min";
                }
            }
        }


        void LoadContents(int lessonId)
        {
            HyperLink[] links = { Label1, Label2, Label3, Label4, Label5 };

            foreach (var l in links)
                l.Text = "";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"SELECT title, media_url
                             FROM lesson_contents
                             WHERE lesson_id=@id
                             AND content_type NOT IN ('text','video')
                             ORDER BY content_order";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", lessonId);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                int i = 0;
                while (r.Read() && i < links.Length)
                {
                    links[i].Text = r["title"].ToString()+" "+r["media_url"].ToString();
                    links[i].NavigateUrl = r["media_url"].ToString();
                    links[i].Target = "_blank";
                    i++;
                }
            }
        }


        void LoadVideo(int lessonId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"SELECT media_url
                             FROM lesson_contents
                             WHERE lesson_id=@id
                             AND content_type='video'";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", lessonId);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    VideoPlayer.InnerHtml = $"<source src='{result}' type='video/mp4' />";
                }
            }
        }


        protected void ToggleComplete(object sender, EventArgs e)
        {
            if (Session["user_id"] == null || Session["lesson"] == null)
                return;

            int userId = Convert.ToInt32(Session["user_id"]);
            int lessonId = Convert.ToInt32(Session["lesson"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string check = @"SELECT COUNT(*) FROM progress
                                 WHERE user_id=@u AND lesson_id=@l";

                SqlCommand cmd = new SqlCommand(check, con);
                cmd.Parameters.AddWithValue("@u", userId);
                cmd.Parameters.AddWithValue("@l", lessonId);

                int exists = (int)cmd.ExecuteScalar();

                if (exists == 0)
                {
                    SqlCommand ins = new SqlCommand(
                        "INSERT INTO progress (user_id, lesson_id, status) VALUES (@u,@l,'completed')", con);

                    ins.Parameters.AddWithValue("@u", userId);
                    ins.Parameters.AddWithValue("@l", lessonId);
                    ins.ExecuteNonQuery();

                    Complete.ForeColor = Color.Green;
                }
                else
                {
                    SqlCommand del = new SqlCommand(
                        "DELETE FROM progress WHERE user_id=@u AND lesson_id=@l", con);

                    del.Parameters.AddWithValue("@u", userId);
                    del.Parameters.AddWithValue("@l", lessonId);
                    del.ExecuteNonQuery();

                    Complete.ForeColor = Color.Black;
                }
            }
        }

        void SetCompleteButton()
        {
            if (Session["user_id"] == null || Session["lesson"] == null)
                return;

            int userId = Convert.ToInt32(Session["user_id"]);
            int lessonId = Convert.ToInt32(Session["lesson"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM progress WHERE user_id=@u AND lesson_id=@l", con);

                cmd.Parameters.AddWithValue("@u", userId);
                cmd.Parameters.AddWithValue("@l", lessonId);

                con.Open();

                bool completed = (int)cmd.ExecuteScalar() > 0;

                Complete.Text = "Complete"; 
                Complete.ForeColor = completed ? Color.Green : Color.Black;
            }
        }

        protected void SelectLesson(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Session["lesson"] = Convert.ToInt32(b.CommandArgument);
            LoadLesson();
        }

        protected void PrevLesson(object sender, EventArgs e)
        {
            int current = GetLessonOrder((int)Session["lesson"]);
            if (current > 1)
                Session["lesson"] = GetLessonIdByOrder(current - 1);

            LoadLesson();
        }

        protected void NextLesson(object sender, EventArgs e)
        {
            int current = GetLessonOrder((int)Session["lesson"]);
            if (current < 3)
                Session["lesson"] = GetLessonIdByOrder(current + 1);

            LoadLesson();
        }

        void HighlightSelectedLesson()
        {
            if (Session["lesson"] == null) return;

            int currentLessonId = Convert.ToInt32(Session["lesson"]);

            Button[] buttons = { Tutorial1, Tutorial2, Tutorial3 };

            foreach (var btn in buttons)
            {
                if (btn.CommandArgument == currentLessonId.ToString())
                {
                    btn.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    btn.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        int GetLessonOrder(int lessonId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT lesson_order FROM lessons WHERE lesson_id=@id", con);

                cmd.Parameters.AddWithValue("@id", lessonId);
                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        int GetLessonIdByOrder(int order)
        {
            int courseId = Convert.ToInt32(Session["course"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT lesson_id FROM lessons WHERE course_id=@c AND lesson_order=@o", con);

                cmd.Parameters.AddWithValue("@c", courseId);
                cmd.Parameters.AddWithValue("@o", order);

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}