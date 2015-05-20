using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BellatorTabernae.Pages
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SiteMsg"] != null)
            {
                Panel MsgPanel = (Panel)Master.FindControl("MsgPanel");
                MsgPanel.Visible = true;
                Literal SiteMsg = (Literal)Master.FindControl("SiteMsg");
                SiteMsg.Text = Session["SiteMsg"].ToString();
                Session["SiteMsg"] = null;
            }
        }
    }
}