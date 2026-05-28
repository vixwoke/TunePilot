using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null) Session["role"] = "fallback_role";
            if (Session["instrument"] == null) Session["instrument"] = 1;

            RoleLabel.Text = Session["role"].ToString();

            if (Session["first_name"] != null)
                lblGreeting.Text = "Welcome back, " + Session["first_name"].ToString() + " " + Session["last_name"].ToString()+"!";
            else
                lblGreeting.Text = "Welcome, [if u see this, session is empty. check it pls/]";

            CardGuitar.ServerClick += InstrumentBtn_Click;
            CardDrum.ServerClick += InstrumentBtn_Click;
            CardTrumpet.ServerClick += InstrumentBtn_Click;

            SetActiveCard();

            LoadDashboard();
        }

        void SetActiveCard()
        {
            int id = Convert.ToInt32(Session["instrument"]);
            CardGuitar.Attributes["class"] = "instrument-card";
            CardDrum.Attributes["class"] = "instrument-card";
            CardTrumpet.Attributes["class"] = "instrument-card";
            switch (id)
            {
                case 1: CardGuitar.Attributes["class"] = "instrument-card active"; break;
                case 2: CardDrum.Attributes["class"] = "instrument-card active"; break;
                case 3: CardTrumpet.Attributes["class"] = "instrument-card active"; break;
            }
        }

        void LoadDashboard()
        {
            LoadInstrumentInfo();
            LoadLessons();
            LoadQuizzes();
            LoadExams();
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
                int lessonNumber = 0;

                while (r.Read())
                {
                    int courseId = Convert.ToInt32(r["course_id"]);

                    if (currentCourse != courseId)
                    {
                        currentCourse = courseId;
                        lessonNumber = 1;

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
                        btn.Text = lessonNumber + ". " + r["lesson_title"].ToString();
                        btn.CommandArgument = r["lesson_id"].ToString();
                        btn.CssClass = "lesson-link";

                        btn.Click += SelectCourse;

                        LessonContainer.Controls.Add(btn);
                    }
                    else
                    {
                        Label lbl = new Label();

                        lbl.Text = lessonNumber + ". " + r["lesson_title"].ToString() + " (Locked)";
                        lbl.CssClass = "lesson-locked";

                        LessonContainer.Controls.Add(lbl);
                    }

                    lessonNumber++;

                    string status =
                        r["status"] == DBNull.Value ? "" : r["status"].ToString().ToLower();

                    string dotClass = "empty";
                    if (status == "in progress") dotClass = "progress";
                    if (status == "completed") dotClass = "completed";

                    LessonProgressContainer.Controls.Add(
                        new Literal { Text = "<span class=\"progress-dot progress-dot--" + dotClass + "\"></span>" }
                    );
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
    c.course_id,
    c.title AS course_title,

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

ORDER BY c.course_id, q.quiz_id;";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@instrument", Session["instrument"]);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);

                con.Open();

                SqlDataReader r = cmd.ExecuteReader();

                int currentCourse = -1;
                int quizNumber = 0;

                while (r.Read())
                {
                    int courseId = Convert.ToInt32(r["course_id"]);

                    if (currentCourse != courseId)
                    {
                        currentCourse = courseId;
                        quizNumber = 1;

                        Literal title = new Literal();
                        title.Text = "<div class='course-title'>" + r["course_title"].ToString() + "</div>";

                        QuizContainer.Controls.Add(title);
                    }

                    LinkButton btn = new LinkButton();

                    btn.ID = "Quiz_" + r["quiz_id"].ToString();
                    btn.Text = quizNumber + ". " + r["title"].ToString();
                    btn.CommandArgument = r["quiz_id"].ToString();
                    btn.CssClass = "quiz-link";

                    btn.Click += SelectQuiz;

                    QuizContainer.Controls.Add(btn);

                    quizNumber++;

                    string dotClass = "empty";

                    if (r["attempt_id"] != DBNull.Value)
                    {
                        if (r["latest_passed"] == DBNull.Value)
                            dotClass = "progress";
                        else if (Convert.ToInt32(r["ever_passed"]) == 1)
                            dotClass = "completed";
                        else
                            dotClass = "failed";
                    }

                    QuizProgressContainer.Controls.Add(
                        new Literal { Text = "<span class=\"progress-dot progress-dot--" + dotClass + "\"></span>" }
                    );
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

                int examNumber = 0;

                while (r.Read())
                {
                    examNumber++;

                    int attempts = Convert.ToInt32(r["attempts"]);
                    int bestScore = Convert.ToInt32(r["best_score"]);
                    string result = r["result"].ToString();

                    string statusLabel = "";
                    string statusClass = "";
                    if (result == "pass") { statusLabel = "Passed"; statusClass = "exam-status passed"; }
                    else if (result == "fail") { statusLabel = bestScore + "%"; statusClass = "exam-status failed"; }
                    else { statusLabel = "Not attempted"; statusClass = "exam-status empty"; }

                    Panel item = new Panel { CssClass = "exam-item" };
                    Panel row = new Panel { CssClass = "exam-row" };

                    LinkButton btn = new LinkButton();
                    btn.ID = "Exam_" + r["exam_id"].ToString();
                    btn.Text = examNumber + ". " + r["title"].ToString();
                    btn.CssClass = "exam-link";
                    btn.CommandArgument = r["exam_id"].ToString();
                    btn.Click += SelectExam;

                    Literal badge = new Literal();
                    badge.Text = "<span class='" + statusClass + "'>" + statusLabel + "</span>";

                    Literal meta = new Literal();
                    meta.Text = "<div class='exam-meta'>" + attempts + " attempt" + (attempts != 1 ? "s" : "") + " &middot; Best: " + bestScore + "%</div>";

                    row.Controls.Add(btn);
                    row.Controls.Add(badge);
                    item.Controls.Add(row);
                    item.Controls.Add(meta);
                    ExamContainer.Controls.Add(item);

                    string dotClass = "empty";
                    if (result == "pass") dotClass = "completed";
                    else if (result == "fail") dotClass = "failed";

                    ExamProgressContainer.Controls.Add(
                        new Literal { Text = "<span class=\"progress-dot progress-dot--" + dotClass + "\"></span>" }
                    );
                }
            }
        }

        protected void InstrumentBtn_Click(object sender, EventArgs e)
        {
            HtmlButton btn = (HtmlButton)sender;
            if (btn.ID == "CardGuitar") Session["instrument"] = 1;
            else if (btn.ID == "CardDrum") Session["instrument"] = 2;
            else if (btn.ID == "CardTrumpet") Session["instrument"] = 3;
            SetActiveCard();
            LoadDashboard();
        }

        protected void SelectExam(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Session["exam"] = btn.CommandArgument;
            Response.Redirect("Exam.aspx");
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

    }
}