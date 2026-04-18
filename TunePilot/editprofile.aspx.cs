using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace TunePilot
{
    public partial class editprofile : Page
    {
        string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Must be logged in
            if (Session["user_id"] == null)
                Response.Redirect("login.aspx");

            if (!IsPostBack)
                LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT first_name, last_name, username, email FROM users WHERE user_id = @userId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", Session["user_id"].ToString());

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFirstName.Text = reader["first_name"].ToString();
                        txtLastName.Text = reader["last_name"].ToString();
                        txtUsername.Text = reader["username"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading profile: " + ex.Message;
                lblMessage.CssClass = "message-label error";
                lblMessage.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string userId = Session["user_id"].ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Check if username/email taken by another user
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE (username = @username OR email = @email) AND user_id != @userId";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    checkCmd.Parameters.AddWithValue("@email", email);
                    checkCmd.Parameters.AddWithValue("@userId", userId);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        lblMessage.Text = "Username or email is already taken by another account.";
                        lblMessage.CssClass = "message-label error";
                        lblMessage.Visible = true;
                        return;
                    }

                    // Build update query — only update password if provided
                    string updateQuery;
                    SqlCommand updateCmd;

                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        updateQuery = @"UPDATE users 
                                        SET first_name = @firstName, last_name = @lastName, 
                                            username = @username, email = @email, password = @password
                                        WHERE user_id = @userId";
                        updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@password", newPassword);
                    }
                    else
                    {
                        updateQuery = @"UPDATE users 
                                        SET first_name = @firstName, last_name = @lastName, 
                                            username = @username, email = @email
                                        WHERE user_id = @userId";
                        updateCmd = new SqlCommand(updateQuery, conn);
                    }

                    updateCmd.Parameters.AddWithValue("@firstName", firstName);
                    updateCmd.Parameters.AddWithValue("@lastName", lastName);
                    updateCmd.Parameters.AddWithValue("@username", username);
                    updateCmd.Parameters.AddWithValue("@email", email);
                    updateCmd.Parameters.AddWithValue("@userId", userId);
                    updateCmd.ExecuteNonQuery();

                    // Update session
                    Session["username"] = username;
                    Session["first_name"] = firstName;
                    Session["last_name"] = lastName;
                    Session["email"] = email;

                    lblMessage.Text = "Profile updated successfully!";
                    lblMessage.CssClass = "message-label success";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.CssClass = "message-label error";
                lblMessage.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
}
