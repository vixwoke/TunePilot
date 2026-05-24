using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TunePilot
{
    public partial class AddCourse : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TunePilotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null || Session["role"].ToString() != "admin")
                Response.Redirect("~/login.aspx");

            if (!IsPostBack)
                BindInstruments();
        }

        private void BindInstruments()
        {
            string query = "SELECT instrument_id, name FROM instruments ORDER BY name";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                instrumentddl.DataSource = dt;
                instrumentddl.DataTextField = "name";
                instrumentddl.DataValueField = "instrument_id";
                instrumentddl.DataBind();
                instrumentddl.Items.Insert(0, new ListItem("Select...", ""));
            }
        }

        protected void savebtn_Click(object sender, EventArgs e)
        {
            if (instrumentddl.SelectedValue == "" || difficultyDdl.SelectedValue == "" || titletb.Text.Trim() == "")
            {
                messagelbl.Text = "Please fill in all fields.";
                messagelbl.Visible = true;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO courses (instrument_id, title, description, difficulty_level, created_at)
                    VALUES (@instrument, @title, @desc, @diff, @created)", conn);
                cmd.Parameters.AddWithValue("@instrument", Convert.ToInt32(instrumentddl.SelectedValue));
                cmd.Parameters.AddWithValue("@title", titletb.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", descriptiontb.Text.Trim());
                cmd.Parameters.AddWithValue("@diff", difficultyDdl.SelectedValue);
                cmd.Parameters.AddWithValue("@created", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("~/AdminDashboard.aspx");
        }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminDashboard.aspx");
        }
    }
}