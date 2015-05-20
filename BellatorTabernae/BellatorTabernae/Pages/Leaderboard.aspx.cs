using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BellatorTabernae.Pages
{
    public partial class Leaderboard : System.Web.UI.Page
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

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

        public IEnumerable<Model.Leaderboard> LeaderboardListView_GetLeaderBoard(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            try
            {
                return Service.GetLeaderboard(maximumRows, startRowIndex, out totalRowCount, LeaderboardType.SelectedIndex);
            }
            catch (SqlException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch (ApplicationException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade.");
            }
            totalRowCount = 0;
            return null;
        }
    }
}