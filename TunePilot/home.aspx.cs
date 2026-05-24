using System;
using System.Web.UI;

namespace TunePilot
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "TunePilot";
        }

        protected void buttonGetStart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StudentDashboard.aspx");
        }

        protected void buttonAboutUs_Click(object sender, EventArgs e)
        {
            Response.Redirect("aboutus.aspx");
        }
    }
}