using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Web.UI;

namespace TunePilot
{
    public partial class navbar : MasterPage
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null)
            {
                chatbot_container.Visible = false;
                navphLogin.Visible = true;
                navphRegister.Visible = true;
                navAccCornerContainer.Visible = false;
            }
            else if (Session["role"].ToString() == "student")
            {
                chatbot_container.Visible = true;
                lblName.Text = Session["first_name"]?.ToString();
                lblUserame.Text = "@" + Session["username"]?.ToString();
                navphLogin.Visible = false;
                navphRegister.Visible = false;
                navAccCornerContainer.Visible = true;
            }
            else if (Session["role"].ToString() == "admin")
            {
                chatbot_container.Visible = false;
                lblName.Text = Session["first_name"]?.ToString();
                lblUserame.Text = "Admin @" + Session["username"]?.ToString();
                navphLogin.Visible = false;
                navphRegister.Visible = false;
                navAccCornerContainer.Visible = true;
            }
            else
            {
                Response.Write("<script>alert('fall into else error. when the navigation bar load, session [role] is not NULL, student, or admin. File affected: navbar.Master.cs');</script>");
            }

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/home.aspx");
        }

        protected void chatSendbtn_Click(object sender, EventArgs e)
        {
            if (Session["user_id"] == null) return;

            string userMessage = chatInputtb.Text.Trim();
            if (string.IsNullOrEmpty(userMessage)) return;

            int userId = Convert.ToInt32(Session["user_id"]);
            string context = BuildContext(userId);
            string response = CallGroq(userMessage, context);

            string safeMessage = userMessage.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");
            string safeResponse = response.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");

            string script = $"document.getElementById('chatbot-messages').innerHTML += '<div class=\"user-msg\">You: {safeMessage}</div><div class=\"bot-msg\">TunePilot: {safeResponse}</div>'; toggleChatbot(true);";

            Page.ClientScript.RegisterStartupScript(GetType(), "chatResponse", script, true);
            chatInputtb.Text = "";
        }

        private string BuildContext(int userId)
        {
            StringBuilder sb = new StringBuilder();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT first_name, last_name, role 
                    FROM users WHERE user_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", userId);
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                    sb.AppendLine($"User: {r["first_name"]} {r["last_name"]} (Role: {r["role"]})");
                r.Close();

                cmd = new SqlCommand(@"
                    SELECT c.title, e.status,
                        (
                         SELECT COUNT(*) FROM progress p JOIN lessons l ON p.lesson_id = l.lesson_id
                         WHERE p.user_id = @id AND l.course_id = c.course_id AND p.status = 'completed') AS completed_lessons,
                         (SELECT COUNT(*) FROM lessons l 
                         WHERE l.course_id = c.course_id
                        ) AS total_lessons
                    FROM enrollments e
                    JOIN courses c ON e.course_id = c.course_id
                    WHERE e.user_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", userId);
                r = cmd.ExecuteReader();
                sb.AppendLine("Enrolled courses:");
                while (r.Read())
                    sb.AppendLine($"- {r["title"]} | Status: {r["status"]} | Progress: {r["completed_lessons"]}/{r["total_lessons"]} lessons");
                r.Close();

                cmd = new SqlCommand(@"
                    SELECT c.title, i.name AS instrument, c.difficulty_level,
                        (SELECT COUNT(*) FROM lessons l WHERE l.course_id = c.course_id) AS lesson_count
                    FROM courses c
                    JOIN instruments i ON c.instrument_id = i.instrument_id", conn);
                r = cmd.ExecuteReader();
                sb.AppendLine("Available courses:");
                while (r.Read())
                    sb.AppendLine($"- {r["title"]} ({r["instrument"]}, {r["difficulty_level"]}, {r["lesson_count"]} lessons)");
                r.Close();
            }

            return sb.ToString();
        }

        private string CallGroq(string userMessage, string context)
        {
            string apiKey = ConfigurationManager.AppSettings["GroqApiKey"];

            string systemPrompt = $@"You are TunePilot Assistant, a helpful music learning assistant. 
                                     Only answer questions about TunePilot courses, lessons, quizzes, exams, and the user's progress. 
                                     If asked anything unrelated to TunePilot or music learning, politely decline. 
                                     Keep answers concise and friendly. Here is the current data: {context}";

            string json = $@"{{
                ""model"": ""llama-3.3-70b-versatile"",
                ""max_tokens"": 1000,
                ""messages"": [
                    {{""role"": ""system"", ""content"": {Newtonsoft.Json.JsonConvert.SerializeObject(systemPrompt)}}},
                    {{""role"": ""user"", ""content"": {Newtonsoft.Json.JsonConvert.SerializeObject(userMessage)}}}
                ]
            }}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = client.PostAsync("https://api.groq.com/openai/v1/chat/completions", content).Result;
                string responseJson = result.Content.ReadAsStringAsync().Result;

                dynamic parsed = Newtonsoft.Json.JsonConvert.DeserializeObject(responseJson);
                return parsed.choices[0].message.content.ToString();
            }
        }
    }
}
