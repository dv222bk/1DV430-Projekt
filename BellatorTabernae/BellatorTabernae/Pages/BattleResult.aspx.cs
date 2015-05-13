﻿using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BellatorTabernae.Pages
{
    public partial class BattleResult : System.Web.UI.Page
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated && Session["CombatLog"] != null)
            {
                CombatLogPanel.Visible = true;
            }
            else if (Context.User.Identity.IsAuthenticated)
            {
                NoCombatLogPanel.Visible = true;
            }
            else
            {
                Session["SiteMsg"] = "Du måste vara inloggad för att titta på stridsrapporter!";
                Response.RedirectToRoute("Default");
            }
        }

        public IEnumerable<CombatLog> CombatLogListView_ShowCombatLog()
        {
            return (IEnumerable<CombatLog>)Session["CombatLog"]; 
        }
    }
}