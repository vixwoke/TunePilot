using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null) Session["role"] = "guest";
            if (Session["instrument"] == null) Session["instrument"] = 1;

            RoleLabel.Text = Session["role"].ToString();

            if (Request["__EVENTTARGET"] == "InstrumentSelect")
                Session["instrument"] = Convert.ToInt32(Request["__EVENTARGUMENT"]);

            LoadDashboard();

            SetActiveIcon(Convert.ToInt32(Session["instrument"]));
        }

        void LoadDashboard()
        {
            LoadInstrumentInfo();
            LoadLessons();
            LoadQuizzes();
            LoadExams();
            ApplyGuestRestrictions();
        }

        void LoadInstrumentInfo()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"SELECT name, category, description
                             FROM instruments
                             WHERE instrument_id=@id";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@id", Session["instrument"]);

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

        void LoadLessons()
        {
            LessonContainer.Controls.Clear();
            LessonProgressContainer.Controls.Clear();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"
                SELECT c.course_id, c.title AS course_title,
                       l.lesson_id, l.title AS lesson_title, l.lesson_order,
                       p.status
                FROM courses c
                JOIN lessons l
                    ON c.course_id = l.course_id
                LEFT JOIN progress p
                    ON p.lesson_id = l.lesson_id
                    AND p.user_id = @user_id
                WHERE c.instrument_id=@instrument
                ORDER BY c.course_id, l.lesson_order";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@instrument", Session["instrument"]);

                if (Session["user_id"] != null)
                    cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);
                else
                    cmd.Parameters.AddWithValue("@user_id", DBNull.Value);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                int currentCourse = -1;

                while (r.Read())
                {
                    int courseId = Convert.ToInt32(r["course_id"]);

                    if (currentCourse != courseId)
                    {
                        currentCourse = courseId;

                        Literal title = new Literal();
                        title.Text = "<div class='course-title'>" + r["course_title"].ToString() + "</div>";

                        LessonContainer.Controls.Add(title);
                    }

                    bool guestLocked =
                        Session["role"].ToString() == "guest" &&
                        !(courseId == 1 || courseId == 4 || courseId == 7);

                    if (!guestLocked)
                    {
                        LinkButton btn = new LinkButton();

                        btn.ID = "Lesson_" + r["lesson_id"].ToString();
                        btn.Text = r["lesson_title"].ToString();
                        btn.CommandArgument = r["lesson_id"].ToString();

                        btn.Click += SelectCourse;

                        LessonContainer.Controls.Add(btn);
                    }
                    else
                    {
                        Label lbl = new Label();

                        lbl.Text = r["lesson_title"].ToString() + " (Locked)";

                        LessonContainer.Controls.Add(lbl);
                    }

                    LessonContainer.Controls.Add(new Literal() { Text = "<br/>" });

                    string status =
                        r["status"] == DBNull.Value ? "" : r["status"].ToString().ToLower();

                    string image = "~/resources/studentDashboard/square.png";

                    if (status == "in progress") image = "~/resources/studentDashboard/square3.png";
                    if (status == "completed") image = "~/resources/studentDashboard/square1.png";

                    Image img = new Image();

                    img.ImageUrl = image;
                    img.CssClass = "progress-img";

                    LessonProgressContainer.Controls.Add(img);
                }
            }
        }

        void LoadQuizzes()
        {
            QuizContainer.Controls.Clear();
            QuizProgressContainer.Controls.Clear();

            if (Session["role"].ToString() == "guest") return;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"
WITH LatestAttempt AS
(
    SELECT 
        qa.quiz_id,
        qa.user_id,
        qa.passed,
        qa.attempt_id,

        ROW_NUMBER() OVER
        (
            PARTITION BY qa.quiz_id, qa.user_id
            ORDER BY qa.attempt_id DESC
        ) AS rn

    FROM quiz_attempts qa
)

SELECT 
    q.quiz_id,
    q.title,

    la.attempt_id,
    la.passed AS latest_passed,

    CASE
        WHEN EXISTS
        (
            SELECT 1
            FROM quiz_attempts x
            WHERE x.quiz_id = q.quiz_id
            AND x.user_id = @user_id
            AND x.passed = 1
        )
        THEN 1
        ELSE 0
    END AS ever_passed

FROM quizzes q

JOIN lessons l
    ON q.lesson_id = l.lesson_id

JOIN courses c
    ON l.course_id = c.course_id
    AND c.instrument_id = @instrument

LEFT JOIN LatestAttempt la
    ON q.quiz_id = la.quiz_id
    AND la.user_id = @user_id
    AND la.rn = 1

ORDER BY q.quiz_id;";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@instrument", Session["instrument"]);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    LinkButton btn = new LinkButton();

                    btn.ID = "Quiz_" + r["quiz_id"].ToString();
                    btn.Text = r["title"].ToString();
                    btn.CommandArgument = r["quiz_id"].ToString();

                    btn.Click += SelectQuiz;

                    QuizContainer.Controls.Add(btn);

                    QuizContainer.Controls.Add(new Literal() { Text = "<br/>" });


                    // never attempt
                    string image = "~/resources/studentDashboard/square.png";

                    // no attempt record
                    if (r["attempt_id"] == DBNull.Value)
                    {
                        image = "~/resources/studentDashboard/square.png";
                    }
                    // latest attempt still in progress
                    else if (r["latest_passed"] == DBNull.Value)
                    {
                        image = "~/resources/studentDashboard/square3.png";
                    }
                    else
                    {
                        bool everPassed = Convert.ToInt32(r["ever_passed"]) == 1;

                        // passed before
                        if (everPassed)
                        {
                            image = "~/resources/studentDashboard/square1.png";
                        }
                        // attempted but never passed
                        else
                        {
                            image = "~/resources/studentDashboard/square2.png";
                        }
                    }

                    Image img = new Image();

                    img.ImageUrl = image;
                    img.CssClass = "progress-img";

                    QuizProgressContainer.Controls.Add(img);
                }
            }
        }

        void LoadExams()
        {
            ExamContainer.Controls.Clear();
            ExamProgressContainer.Controls.Clear();

            if (Session["role"].ToString() == "guest") return;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"
                SELECT e.exam_id, e.title,
                       COUNT(a.attempt_id) AS attempts,
                       ISNULL(MAX(a.score), 0) AS best_score,
                       CASE
                           WHEN MAX(CASE WHEN a.passed = 1 THEN 1 ELSE 0 END) = 1 THEN 'pass'
                           WHEN COUNT(a.attempt_id) > 0 THEN 'fail'
                           ELSE 'none'
                       END AS result
                FROM exams e
                JOIN courses c
                    ON e.course_id = c.course_id
                    AND c.instrument_id = @instrument
                LEFT JOIN exam_attempts a
                    ON e.exam_id = a.exam_id
                    AND a.user_id = @user_id
                GROUP BY e.exam_id, e.title
                ORDER BY e.exam_id;";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@instrument", Session["instrument"]);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    LinkButton btn = new LinkButton();

                    btn.ID = "Exam_" + r["exam_id"].ToString();

                    btn.Text = r["title"].ToString() + " | Attempts: " + r["attempts"].ToString() + " | Best Score: " + r["best_score"].ToString() + "%";

                    btn.CommandArgument = r["exam_id"].ToString();

                    btn.Click += SelectExam;

                    ExamContainer.Controls.Add(btn);

                    ExamContainer.Controls.Add(new Literal() { Text = "<br/>" });

                    string result = r["result"].ToString();

                    string image = "~/resources/studentDashboard/square.png";

                    if (result == "fail") image = "~/resources/studentDashboard/square2.png";
                    if (result == "pass") image = "~/resources/studentDashboard/square1.png";

                    Image img = new Image();

                    img.ImageUrl = image;
                    img.CssClass = "progress-img";

                    ExamProgressContainer.Controls.Add(img);
                }
            }
        }

        protected void SelectCourse(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            Session["lesson"] = btn.CommandArgument;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"SELECT course_id
                             FROM lessons
                             WHERE lesson_id=@lesson_id";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@lesson_id", Session["lesson"]);

                con.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                    Session["course"] = result.ToString();
            }

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
            if (Session["role"].ToString() == "guest") return;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"
                IF NOT EXISTS (
                    SELECT *
                    FROM enrollments
                    WHERE user_id=@user_id
                    AND course_id=@course_id
                )
                BEGIN
                    INSERT INTO enrollments(user_id, course_id, status)
                    VALUES(@user_id, @course_id, 'active')
                END";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);
                cmd.Parameters.AddWithValue("@course_id", Session["course"]);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        void ApplyGuestRestrictions()
        {
            if (Session["role"].ToString() == "guest")
                LoginUnlock.Text = "Login to unlock more lessons, quizzes and exams.";
        }

        void SetActiveIcon(int id)
        {
            GuitarIcon.Style["border"] = "2px solid gray";
            DrumIcon.Style["border"] = "2px solid gray";
            TrumpetIcon.Style["border"] = "2px solid gray";

            GuitarIcon.ImageUrl = "/resources/studentDashboard/guitar.jpg";
            DrumIcon.ImageUrl = "/resources/studentDashboard/drum.png";
            TrumpetIcon.ImageUrl = "/resources/studentDashboard/trumpet.jpg";

            switch (id)
            {
                case 1:
                    GuitarIcon.Style["border"] = "2px solid red";
                    GuitarIcon.ImageUrl = "/resources/studentDashboard/guitar1.png";
                    break;

                case 2:
                    DrumIcon.Style["border"] = "2px solid red";
                    DrumIcon.ImageUrl = "/resources/studentDashboard/drum1.png";
                    break;

                case 3:
                    TrumpetIcon.Style["border"] = "2px solid red";
                    TrumpetIcon.ImageUrl = "/resources/studentDashboard/trumpet1.png";
                    break;
            }
        }
    }
}