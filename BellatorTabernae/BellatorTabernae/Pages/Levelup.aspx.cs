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
    public partial class Levelup : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    GetCharacter();

                    // If the character has no points to spend
                    if (GetPointsLeft() <= 0)
                    {
                        ResetSessions();
                        Session["SiteMsg"] = "Du har inga poäng att sätta ut, så du kan inte levela upp!";
                        Response.RedirectToRoute("Character");
                        return;
                    }
                }
                InsertPageInfo();
            }
            else if (Context.User.Identity.IsAuthenticated)
            {
                Session["SiteMsg"] = "Du har ingen karaktär, så du kan inte levela upp!";
                Response.RedirectToRoute("Character");
            }
            else
            {
                Session["SiteMsg"] = "Du måste vara inloggad för att levela upp karaktärer!";
                Response.RedirectToRoute("Default");
            }
        }

        protected void GetCharacter()
        {
            Model.Character character = Service.GetCharacter(null, int.Parse(Context.User.Identity.Name));
            Session["OriginalStrength"] = Session["Strength"] = character.Strength;
            Session["OriginalSpeed"] = Session["Speed"] = character.Speed;
            Session["OriginalAgility"] = Session["Agility"] = character.Agility;
            Session["OriginalDexterity"] = Session["Dexterity"] = character.Dexterity;
            Session["OriginalHealth"] = Session["Health"] = character.MaxHealth;
            Session["OriginalStanima"] = Session["Stanima"] = character.MaxStanima;
            Session["Level"] = character.Level;
            Session["PointsLeft"] = GetPointsLeft();
        }

        protected void InsertPageInfo()
        {
            Strength.Text = Session["Strength"].ToString();
            Speed.Text = Session["Speed"].ToString();
            Agility.Text = Session["Agility"].ToString();
            Dexterity.Text = Session["Dexterity"].ToString();
            Health.Text = Session["Health"].ToString();
            Stanima.Text = Session["Stanima"].ToString();
            StrengthLabel.Text = String.Format("Styrka ({0}): ", Session["OriginalStrength"].ToString());
            SpeedLabel.Text = String.Format("Snabbhet ({0}): ", Session["OriginalSpeed"].ToString());
            AgilityLabel.Text = String.Format("Träffsäkerhet ({0}): ", Session["OriginalAgility"].ToString());
            DexterityLabel.Text = String.Format("Undvika ({0}): ", Session["OriginalDexterity"].ToString());
            HealthLabel.Text = String.Format("Livspoäng ({0}): ", Session["OriginalHealth"].ToString());
            StanimaLabel.Text = String.Format("Uthållighetspoäng ({0}): ", Session["OriginalStanima"].ToString());
            UpdatePointsLeft();
        }

        protected void LevelUp_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (GetPointsLeft() >= 0)
                {
                    try
                    {
                        Service.LevelUpCharacter(int.Parse(Context.User.Identity.Name), null,
                            int.Parse(Session["Health"].ToString()), int.Parse(Session["Stanima"].ToString()),
                            int.Parse(Session["Strength"].ToString()), int.Parse(Session["Speed"].ToString()),
                            int.Parse(Session["Dexterity"].ToString()), int.Parse(Session["Agility"].ToString()));

                        // Reset all sessions
                        ResetSessions();

                        Session["SiteMsg"] = "Du levelade upp din karaktär!";
                        Response.RedirectToRoute("Character");
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
                }
                else
                {
                    Page.ModelState.AddModelError(String.Empty, "Din karaktär är inte tillräckligt hög level för att spendera så många poäng!");
                }
            }
        }

        protected void ResetSessions()
        {
            // Reset all sessions
            Session["Health"] = Session["Stanima"] = Session["Strength"] = Session["Speed"] =
                Session["Agility"] = Session["Dexterity"] = Session["RaceHealth"] = Session["Level"] =
                Session["OriginalStanima"] = Session["OriginalStrength"] = Session["OriginalSpeed"] =
                Session["OriginalAgility"] = Session["OriginalDexterity"] = Session["PointsLeft"] = null;
        }

        protected int GetPointsLeft()
        {
            return ((int.Parse(Session["Level"].ToString()) * 10) + 50) - ((int.Parse(Session["Health"].ToString()) / 5) +
                (int.Parse(Session["Stanima"].ToString()) / 5) + int.Parse(Session["Strength"].ToString()) +
                int.Parse(Session["Speed"].ToString()) + int.Parse(Session["Agility"].ToString()) +
                int.Parse(Session["Dexterity"].ToString()));
        }

        protected void DexterityPlus_Click(object sender, EventArgs e)
        {
            int currentDexterity = int.Parse(Session["Dexterity"].ToString());
            if (int.Parse(Session["PointsLeft"].ToString()) > 0)
            {
                currentDexterity++;
                Session["Dexterity"] = currentDexterity;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) - 1;
                Dexterity.Text = (int.Parse(Dexterity.Text) + 1).ToString();
                UpdatePointsLeft();
            }
        }

        protected void DexterityMinus_Click(object sender, EventArgs e)
        {
            int currentDexterity = int.Parse(Session["Dexterity"].ToString());
            if (currentDexterity > int.Parse(Session["OriginalDexterity"].ToString()))
            {
                currentDexterity--;
                Session["Dexterity"] = currentDexterity;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) + 1;
                Dexterity.Text = currentDexterity.ToString();
                UpdatePointsLeft();
            }
        }

        protected void AgilityPlus_Click(object sender, EventArgs e)
        {
            int currentAgility = int.Parse(Session["Agility"].ToString());
            if (int.Parse(Session["PointsLeft"].ToString()) > 0)
            {
                currentAgility++;
                Session["Agility"] = currentAgility;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) - 1;
                Agility.Text = (int.Parse(Agility.Text) + 1).ToString();
                UpdatePointsLeft();
            }
        }

        protected void AgilityMinus_Click(object sender, EventArgs e)
        {
            int currentAgility = int.Parse(Session["Agility"].ToString());
            if (currentAgility > int.Parse(Session["OriginalAgility"].ToString()))
            {
                currentAgility--;
                Session["Agility"] = currentAgility;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) + 1;
                Agility.Text = currentAgility.ToString();
                UpdatePointsLeft();
            }
        }

        protected void SpeedPlus_Click(object sender, EventArgs e)
        {
            int currentSpeed = int.Parse(Session["Speed"].ToString());
            if (int.Parse(Session["PointsLeft"].ToString()) > 0)
            {
                currentSpeed++;
                Session["Speed"] = currentSpeed;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) - 1;
                Speed.Text = (int.Parse(Speed.Text) + 1).ToString();
                UpdatePointsLeft();
            }
        }

        protected void SpeedMinus_Click(object sender, EventArgs e)
        {
            int currentSpeed = int.Parse(Session["Speed"].ToString());
            if (currentSpeed > int.Parse(Session["OriginalSpeed"].ToString()))
            {
                currentSpeed--;
                Session["Speed"] = currentSpeed;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) + 1;
                Speed.Text = currentSpeed.ToString();
                UpdatePointsLeft();
            }
        }

        protected void StrengthPlus_Click(object sender, EventArgs e)
        {
            int currentStrength = int.Parse(Session["Strength"].ToString());
            if (int.Parse(Session["PointsLeft"].ToString()) > 0)
            {
                currentStrength++;
                Session["Strength"] = currentStrength;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) - 1;
                Strength.Text = (int.Parse(Strength.Text) + 1).ToString();
                UpdatePointsLeft();
            }
        }

        protected void StrengthMinus_Click(object sender, EventArgs e)
        {
            int currentStrength = int.Parse(Session["Strength"].ToString());
            if (currentStrength > int.Parse(Session["OriginalStrength"].ToString()))
            {
                currentStrength--;
                Session["Strength"] = currentStrength;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) + 1;
                Strength.Text = currentStrength.ToString();
                UpdatePointsLeft();
            }
        }

        protected void StanimaPlus_Click(object sender, EventArgs e)
        {
            int currentStanima = int.Parse(Session["Stanima"].ToString());
            if (int.Parse(Session["PointsLeft"].ToString()) > 0)
            {
                currentStanima += 5;
                Session["Stanima"] = currentStanima;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) - 1;
                Stanima.Text = (int.Parse(Stanima.Text) + 5).ToString();
                UpdatePointsLeft();
            }
        }

        protected void StanimaMinus_Click(object sender, EventArgs e)
        {
            int currentStanima = int.Parse(Session["Stanima"].ToString());
            if (currentStanima > int.Parse(Session["OriginalStanima"].ToString()))
            {
                currentStanima -= 5;
                Session["Stanima"] = currentStanima;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) + 1;
                Stanima.Text = currentStanima.ToString();
                UpdatePointsLeft();
            }
        }

        protected void HealthPlus_Click(object sender, EventArgs e)
        {
            int currentHealth = int.Parse(Session["Health"].ToString());
            if (int.Parse(Session["PointsLeft"].ToString()) > 0)
            {
                currentHealth += 5;
                Session["Health"] = currentHealth;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) - 1;
                Health.Text = (int.Parse(Health.Text) + 5).ToString();
                UpdatePointsLeft();
            }
        }

        protected void HealthMinus_Click(object sender, EventArgs e)
        {
            int currentHealth = int.Parse(Session["Health"].ToString());
            if (currentHealth > int.Parse(Session["OriginalHealth"].ToString()))
            {
                currentHealth -= 5;
                Session["Health"] = currentHealth;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) + 1;
                Health.Text = currentHealth.ToString();
                UpdatePointsLeft();
            }
        }

        protected void UpdatePointsLeft()
        {
            PointsLeft.Text = String.Format("Poäng kvar: {0}", Session["PointsLeft"]);
        }
    }
}