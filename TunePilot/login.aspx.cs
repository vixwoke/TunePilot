using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace TunePilot
{
    public partial class login : Page
    {
        string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] != null)
                Response.Redirect("home.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string emailOrUsername = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT user_id, first_name, username, role FROM users WHERE (email = @input OR username = @input) AND password = @password AND active = 1";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@input", emailOrUsername);
                    cmd.Parameters.AddWithValue("@password", password);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Session["user_id"] = reader["user_id"].ToString();
                        Session["username"] = reader["username"].ToString();
                        Session["first_name"] = reader["first_name"].ToString();
                        Session["role"] = reader["role"].ToString();
                        Response.Redirect("home.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid email/username or password.";
                        lblMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.Visible = true;
            }
        }
    }
}