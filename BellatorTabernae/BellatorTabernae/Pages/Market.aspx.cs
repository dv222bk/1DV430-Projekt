using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BellatorTabernae.Pages
{
    public partial class Market : System.Web.UI.Page
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated && Service.UserHasCharacter(int.Parse(Context.User.Identity.Name)))
            {
                CharacterGoldLiteral.Text = String.Format("Ditt guld: {0}", Service.GetCharacterGold(null, int.Parse(Context.User.Identity.Name.ToString())));
            }
            else if (Context.User.Identity.IsAuthenticated)
            {
                NoCharacterPanel.Visible = true;
                MarketPanel.Visible = false;
            }
            else
            {
                Session["SiteMsg"] = "Du måste vara inloggad för att besöka torget!";
                Response.RedirectToRoute("Default");
            }
        }

        protected string GetEquipEffects(int equipStatsID)
        {
            try
            {
                EquipmentStats equipStats = Service.GetEquipmentStats(equipStatsID, null, null);
                return equipStats.EffectString;
            }
            catch (SqlException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch (ValidationException ex)
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

        public IEnumerable<Equipment> MarketListView_GetEquipments(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            try
            {
                return Service.GetMarketInventory(maximumRows, startRowIndex, out totalRowCount);
            }
            catch (SqlException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch (ValidationException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch (ArgumentException ex)
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

        public void MarketListView_BuyEquipment(int EquipID)
        {
            try
            {
                Equipment equipment = Service.GetEquipment(EquipID);
                if (Service.GetCharacterGold(null, int.Parse(Context.User.Identity.Name.ToString())) >= equipment.Value)
                {
                    Service.RemoveGoldFromInventory(null, int.Parse(Context.User.Identity.Name.ToString()), null, equipment.Value);
                    Service.AddEquipmentToInventory(null, int.Parse(Context.User.Identity.Name.ToString()), EquipID, 1);

                    Session["SiteMsg"] = String.Format("Grattis! Du köpte {0} för {1} guld!", equipment.Name, equipment.Value);
                    Response.RedirectToRoute("Market");
                }
                else
                {
                    Page.ModelState.AddModelError(String.Empty, String.Format("Karaktären har inte tillräckligt med guld för att köpa {0}!", equipment.Name));
                }
            }
            catch (SqlException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch (ValidationException ex)
            {
                Page.ModelState.AddModelError(String.Empty, ex.Message);
            }
            catch (ArgumentException ex)
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
        }
    }
}