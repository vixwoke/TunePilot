using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections.Generic;

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

            if (Session["instrument"] == null)
            {
                Session["instrument"] = 1;
            }

            if (!IsPostBack)
            {
                LoadAll();
                ApplyGuestRestrictions();
                SetActiveIcon(Convert.ToInt32(Session["instrument"]));
            }

            if (Request["__EVENTTARGET"] == "InstrumentSelect")
            {
                int instrumentId = Convert.ToInt32(Request["__EVENTARGUMENT"]);

                Session["instrument"] = instrumentId;
                Session["course"] = null;
                Session["lesson"] = null;

                LoadAll();
                ApplyGuestRestrictions();
                SetActiveIcon(instrumentId);
            }
        }

        void LoadAll()
        {
            LoadInstrumentInfo();
            LoadCourses();
            LoadQuizzes();
            LoadExams();
            LoadProgress();
        }

        void LoadInstrumentInfo()
        {
            int instrument = Convert.ToInt32(Session["instrument"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = "SELECT name, description,category FROM instruments WHERE instrument_id=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", instrument);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    LabelInstrumentName.Text = r["name"].ToString();
                    LabelCategory.Text = " ~ " + r["category"].ToString();
                    LabelDescription.Text = r["description"].ToString();
                }
            }
        }

        void LoadCourses()
        {
            int instrument = Convert.ToInt32(Session["instrument"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = "SELECT course_id, title, difficulty_level FROM courses WHERE instrument_id=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", instrument);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int courseId = Convert.ToInt32(r["course_id"]);
                    string level = r["difficulty_level"].ToString().ToLower();

                    if (level == "beginner")
                    {
                        Media1.Text = r["title"].ToString();
                        Difficulty1.Text = " " + r["difficulty_level"].ToString();
                        MediaBtn1.CommandArgument = courseId.ToString(); // ✅ FIX
                    }
                    else if (level == "intermediate")
                    {
                        Media2.Text = r["title"].ToString();
                        Difficulty2.Text = " " + r["difficulty_level"].ToString();
                        MediaBtn2.CommandArgument = courseId.ToString(); // ✅ FIX
                    }
                    else if (level == "advanced")
                    {
                        Media3.Text = r["title"].ToString();
                        Difficulty3.Text = " " + r["difficulty_level"].ToString();
                        MediaBtn3.CommandArgument = courseId.ToString(); // ✅ FIX
                    }
                }
            }
        }

        void LoadProgress()
        {
            if (Session["role"] == null || Session["role"].ToString().ToLower() != "student")
                return;

            int userId = Convert.ToInt32(Session["user_id"]);
            int instrumentId = Convert.ToInt32(Session["instrument"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"
        SELECT c.course_id, COUNT(p.progress_id) AS completedLessons
        FROM courses c
        LEFT JOIN lessons l ON c.course_id = l.course_id
        LEFT JOIN progress p 
            ON p.lesson_id = l.lesson_id 
            AND p.user_id = @user_id
            AND p.status = 'completed'
        WHERE c.instrument_id = @instrument
        GROUP BY c.course_id
        ORDER BY c.course_id";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@instrument", instrumentId);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                Dictionary<int, int> courseProgress = new Dictionary<int, int>();

                while (r.Read())
                {
                    int courseId = Convert.ToInt32(r["course_id"]);
                    int completed = Convert.ToInt32(r["completedLessons"]);

                    courseProgress[courseId] = completed;
                }

                ApplyProgressToUI(courseProgress);
            }
        }

        void ApplyProgressToUI(Dictionary<int, int> data)
        {
            Image[] squares =
            {
                MediaProgress1, MediaProgress2, MediaProgress3,
                MediaProgress4, MediaProgress5, MediaProgress6,
                MediaProgress7, MediaProgress8, MediaProgress9
            };

            foreach (var sq in squares)
                sq.ImageUrl = "~/resources/studentDashboard/square.png";

            int instrumentId = Convert.ToInt32(Session["instrument"]);

            List<int> courseOrder = new List<int>();

            if (instrumentId == 1)
                courseOrder = new List<int> { 1, 2, 3 };
            else if (instrumentId == 2)
                courseOrder = new List<int> { 4, 5, 6 };
            else if (instrumentId == 3)
                courseOrder = new List<int> { 7, 8, 9 };

            for (int i = 0; i < courseOrder.Count; i++)
            {
                int courseId = courseOrder[i];

                if (!data.ContainsKey(courseId))
                    continue;

                int completed = data[courseId];

                for (int j = 0; j < completed; j++)
                {
                    int index = (i * 3) + j;
                    squares[index].ImageUrl = "~/resources/studentDashboard/square1.png";
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
        SELECT 
            e.exam_id,
            e.title,
            c.difficulty_level,
            COUNT(a.attempt_id) AS attempts,
            ISNULL(MAX(a.score), 0) AS best_score
        FROM exams e
        JOIN courses c ON e.course_id = c.course_id
        LEFT JOIN exam_attempts a 
            ON e.exam_id = a.exam_id
            AND a.user_id = @user_id
        WHERE c.instrument_id = @instrument
        GROUP BY e.exam_id, e.title, c.difficulty_level";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@instrument", instrument);

                if (Session["user_id"] != null)
                    cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);
                else
                    cmd.Parameters.AddWithValue("@user_id", DBNull.Value);

                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string level = r["difficulty_level"].ToString().ToLower();
                    string title = r["title"].ToString();
                    int attempts = Convert.ToInt32(r["attempts"]);
                    int score = Convert.ToInt32(r["best_score"]);

                    if (level == "beginner")
                    {
                        Exam1.Text = title;
                        LabelExamAttempt1.Text = "Attempts: " + attempts;
                        LabelExamScore1.Text = score + "%";
                    }
                    else if (level == "intermediate")
                    {
                        Exam2.Text = title;
                        LabelExamAttempt2.Text = "Attempts: " + attempts;
                        LabelExamScore2.Text = score + "%";
                    }
                    else if (level == "advanced")
                    {
                        Exam3.Text = title;
                        LabelExamAttempt3.Text = "Attempts: " + attempts;
                        LabelExamScore3.Text = score + "%";
                    }
                }
            }
        }

        protected void SelectCourse(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Session["course"] = btn.CommandArgument;
            SetEnrollment();
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

        void SetEnrollment()
        {
            if (Session["role"] == null || Session["role"].ToString() == "guest")
                return;

            int userId = Convert.ToInt32(Session["user_id"]);
            int courseId = Convert.ToInt32(Session["course"]);

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string checkQuery = @"
            SELECT COUNT(*) 
            FROM enrollments 
            WHERE user_id = @user_id AND course_id = @course_id";

                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@user_id", userId);
                checkCmd.Parameters.AddWithValue("@course_id", courseId);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    string insertQuery = @"
                INSERT INTO enrollments (user_id, course_id, status)
                VALUES (@user_id, @course_id, 'active')";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                    insertCmd.Parameters.AddWithValue("@user_id", userId);
                    insertCmd.Parameters.AddWithValue("@course_id", courseId);

                    insertCmd.ExecuteNonQuery();
                }
            }
        }

        void SetActiveIcon(int id)
        {
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

        void ApplyGuestRestrictions()
        {
            string role = Session["role"].ToString().ToLower();

            if (role == "guest")
            {
                QuizBtn1.Enabled = false;
                QuizBtn2.Enabled = false;
                QuizBtn3.Enabled = false;
                QuizBtn4.Enabled = false;
                QuizBtn5.Enabled = false;
                QuizBtn6.Enabled = false;
                QuizBtn7.Enabled = false;
                QuizBtn8.Enabled = false;
                QuizBtn9.Enabled = false;

                ExamBtn1.Enabled = false;
                ExamBtn2.Enabled = false;
                ExamBtn3.Enabled = false;

                MediaBtn2.Enabled = false;
                MediaBtn3.Enabled = false;

                LoginUnlock.Text = "Login required to access quizzes and exams.";
            }
        }
    }
}