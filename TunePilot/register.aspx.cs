using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace TunePilot
{
    public partial class register : Page
    {
        string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] != null)
                Response.Redirect("home.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Check if username or email already exists
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username OR email = @email";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    checkCmd.Parameters.AddWithValue("@email", email);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        lblMessage.Text = "Username or email already exists. Please choose another.";
                        lblMessage.CssClass = "message-label error";
                        lblMessage.Visible = true;
                        return;
                    }

                    // Insert new user
                    string insertQuery = @"INSERT INTO users (first_name, last_name, username, email, password, role, active, created_at)
                                          VALUES (@firstName, @lastName, @username, @email, @password, 'student', 1, @createdAt)";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@firstName", firstName);
                    insertCmd.Parameters.AddWithValue("@lastName", lastName);
                    insertCmd.Parameters.AddWithValue("@username", username);
                    insertCmd.Parameters.AddWithValue("@email", email);
                    insertCmd.Parameters.AddWithValue("@password", password);
                    insertCmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
                    insertCmd.ExecuteNonQuery();

                    // Success — redirect to login
                    Response.Redirect("login.aspx?registered=1");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.CssClass = "message-label error";
                lblMessage.Visible = true;
            }
        }
    }
}
