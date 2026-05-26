using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class AddStudent : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null || Session["role"].ToString() != "admin")
                Response.Redirect("~/login.aspx");
        }

        protected void savebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(firstNametb.Text.Trim()) ||
                string.IsNullOrEmpty(lastNametb.Text.Trim()) ||
                string.IsNullOrEmpty(usernametb.Text.Trim()) ||
                string.IsNullOrEmpty(emailtb.Text.Trim()) ||
                string.IsNullOrEmpty(passwordtb.Text.Trim()))
            {
                messagelbl.Text = "All fields are required.";
                messagelbl.Visible = true;
                return;
            }

            // Check duplicate username
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand check = new SqlCommand(
                    "SELECT COUNT(*) FROM users WHERE username = @username OR email = @email", conn);
                check.Parameters.AddWithValue("@username", usernametb.Text.Trim());
                check.Parameters.AddWithValue("@email", emailtb.Text.Trim());
                int exists = Convert.ToInt32(check.ExecuteScalar());
                if (exists > 0)
                {
                    messagelbl.Text = "Username or email already exists.";
                    messagelbl.Visible = true;
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO users (first_name, last_name, username, email, password, role, active, created_at)
                    VALUES (@fname, @lname, @username, @email, @password, 'student', 1, @created)", conn);
                cmd.Parameters.AddWithValue("@fname", firstNametb.Text.Trim());
                cmd.Parameters.AddWithValue("@lname", lastNametb.Text.Trim());
                cmd.Parameters.AddWithValue("@username", usernametb.Text.Trim());
                cmd.Parameters.AddWithValue("@email", emailtb.Text.Trim());
                cmd.Parameters.AddWithValue("@password", passwordtb.Text.Trim());
                cmd.Parameters.AddWithValue("@created", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("~/AdminDashboard.aspx");
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminDashboard.aspx");
        }
    }
}