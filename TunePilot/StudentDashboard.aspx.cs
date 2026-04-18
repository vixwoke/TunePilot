using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null)
            {
                Session["role"] = "guest";
            }
            RoleLabel.Text = Session["role"].ToString();
            if (!IsPostBack)
            {
                Session["instrument"] = 1;
                LoadAll();
                SetActiveIcon(1);
                return;
            }

            if (Request["__EVENTTARGET"] == "InstrumentSelect")
            {
                int instrumentId = Convert.ToInt32(Request["__EVENTARGUMENT"]);
                Session["instrument"] = instrumentId;

                LoadAll();
                SetActiveIcon(instrumentId);
            }
        }

        void LoadAll()
        {
            LoadInstrumentInfo();
            LoadCourses();
            LoadQuizzes();
            LoadExams();
        }

        void LoadInstrumentInfo()
        {
            int instrument = Convert.ToInt32(Session["instrument"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = "SELECT name, description FROM instruments WHERE instrument_id=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", instrument);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    LabelInstrumentName.Text = r["name"].ToString();
                    LabelDescription.Text = r["description"].ToString();
                }
            }
        }

        void LoadCourses()
        {
            int instrument = Convert.ToInt32(Session["instrument"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = "SELECT title, difficulty_level FROM courses WHERE instrument_id=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", instrument);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string level = r["difficulty_level"].ToString().ToLower();

                    if (level == "beginner")
                        Media1.Text = r["title"].ToString();
                    else if (level == "intermediate")
                        Media2.Text = r["title"].ToString();
                    else if (level == "advanced")
                        Media3.Text = r["title"].ToString();
                }
            }
        }

        void LoadQuizzes()
        {
            int instrument = Convert.ToInt32(Session["instrument"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"
                    SELECT q.title
                    FROM quizzes q
                    JOIN lessons l ON q.lesson_id = l.lesson_id
                    JOIN courses c ON l.course_id = c.course_id
                    WHERE c.instrument_id=@id
                    ORDER BY q.quiz_id";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", instrument);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                Label[] quizLabels =
                {
                    Quiz1, Quiz2, Quiz3,
                    Quiz4, Quiz5, Quiz6,
                    Quiz7, Quiz8, Quiz9
                };

                int i = 0;
                while (r.Read() && i < quizLabels.Length)
                {
                    quizLabels[i].Text = r["title"].ToString();
                    i++;
                }
            }
        }

        void LoadExams()
        {
            int instrument = Convert.ToInt32(Session["instrument"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"
                    SELECT e.title, c.difficulty_level
                    FROM exams e
                    JOIN courses c ON e.course_id = c.course_id
                    WHERE c.instrument_id=@id";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", instrument);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string level = r["difficulty_level"].ToString().ToLower();

                    if (level == "beginner")
                        Exam1.Text = r["title"].ToString();
                    else if (level == "intermediate")
                        Exam2.Text = r["title"].ToString();
                    else if (level == "advanced")
                        Exam3.Text = r["title"].ToString();
                }
            }
        }

        protected void SelectCourse(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Session["course"] = btn.CommandArgument;
            Response.Redirect("Lesson.aspx");
        }

        protected void SelectQuiz(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Session["quiz"] = btn.CommandArgument;
            Response.Redirect("Quiz.aspx");
        }

        protected void SelectExam(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Session["exam"] = btn.CommandArgument;
            Response.Redirect("Exam.aspx");
        }

        void SetActiveIcon(int id)
        {
            // Reset all first
            GuitarIcon.ImageUrl = ResolveUrl("~/resources/studentDashboard/guitar.jpg");
            DrumIcon.ImageUrl = ResolveUrl("~/resources/studentDashboard/drum.png");
            TrumpetIcon.ImageUrl = ResolveUrl("~/resources/studentDashboard/trumpet.jpg");

            GuitarIcon.Style["border"] = "2px solid gray";
            DrumIcon.Style["border"] = "2px solid gray";
            TrumpetIcon.Style["border"] = "2px solid gray";

            if (id == 1)
            {
                GuitarIcon.ImageUrl = ResolveUrl("~/resources/studentDashboard/guitar1.png");
                GuitarIcon.Style["border"] = "2px solid red";
            }
            else if (id == 2)
            {
                DrumIcon.ImageUrl = ResolveUrl("~/resources/studentDashboard/drum1.png");
                DrumIcon.Style["border"] = "2px solid red";
            }
            else if (id == 3)
            {
                TrumpetIcon.ImageUrl = ResolveUrl("~/resources/studentDashboard/trumpet1.png");
                TrumpetIcon.Style["border"] = "2px solid red";
            }
        }
    }
}