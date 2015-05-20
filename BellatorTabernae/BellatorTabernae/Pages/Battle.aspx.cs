using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BellatorTabernae.Pages
{
    public partial class Battle : System.Web.UI.Page
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

            if (Context.User.Identity.IsAuthenticated && Service.UserHasCharacter(int.Parse(Context.User.Identity.Name)))
            {
                MonsterPanel.Visible = true;
            }
            else if (Context.User.Identity.IsAuthenticated)
            {
                NoCharacterPanel.Visible = true;
            } 
            else 
            {
                Session["SiteMsg"] = "Du måste vara inloggad för att strida!";
                Response.RedirectToRoute("Default");
            }
        }

        public IEnumerable<Model.Character> MonstersListView_GetMonsters()
        {
            try
            {
                return Service.GetMonsters();
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
            return null;
        }

        public void MonstersListView_ChallangeMonster(int charID)
        {
            try
            {
                Session["CombatLog"] = Service.InitiateMonsterBattle(int.Parse(Context.User.Identity.Name), charID);

                Response.RedirectToRoute("BattleResult");
            }
            catch (SqlException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch (ApplicationException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch (ArgumentException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade.");
            }
        }
    }
}