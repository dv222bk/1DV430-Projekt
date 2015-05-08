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
    public partial class Character : System.Web.UI.Page
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Session["SiteMsg"] = "Du måste vara inloggad för att titta på karaktärsidor!";
                Response.RedirectToRoute("Default");
            }

            if (Context.User.Identity.IsAuthenticated && Service.UserHasCharacter(int.Parse(Context.User.Identity.Name)))
            {
                GetCharacter();
            }
            else
            {
                CreateNewCharacter();
            }
        }

        protected void GetCharacter()
        {
            try
            {
                Model.Character character = Service.GetCharacter(null, int.Parse(Context.User.Identity.Name));
                CharacterPanel.Visible = true;
                CharacterName.Text = character.Name;
                CharacterRace.Text = String.Format("Ras: {0}", character.Race);
                CharacterLevel.Text = String.Format("Level: {0}", character.Level);
                CharacterExperience.Text = String.Format("XP: {0}", character.Experience);
                CharacterHealth.Text = String.Format("Livspoäng: {0}/{1}", character.Health, character.MaxHealth);
                CharacterStanima.Text = String.Format("Uthållighetspoäng: {0}/{1}", character.Stanima, character.MaxStanima);
                CharacterStrength.Text = String.Format("Styrka: {0}", character.Strength);
                CharacterSpeed.Text = String.Format("Speed: {0}", character.Speed);
                CharacterAgility.Text = String.Format("Undvika: {0}", character.Agility);
                CharacterDexterity.Text = String.Format("Träffsäkerhet: {0}", character.Dexterity);
                if (character.Biografy != null)
                {
                    CharacterBiografy.Text = character.Biografy;
                }
                else
                {
                    CharacterBiografy.Text = "Du har inte skrivt någon biografi till din karaktär ännu!";
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

        protected void CreateNewCharacter()
        {
            if (!IsPostBack && ViewState["Races"] == null)
            {
                try
                {
                    NewCharacterPanel.Visible = true;
                    IEnumerable<Race> Races = Service.GetRaces();
                    ViewState["Races"] = Races;

                    ListItem listItem;

                    foreach (Race race in Races)
                    {
                        listItem = new ListItem(race.RaceName, race.RaceID.ToString());
                        RaceList.Items.Add(listItem);
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

        protected void CreateCharacter_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (CheckNewCharacterPoints())
                {

                }
                else
                {
                    Page.ModelState.AddModelError(String.Empty, "Du har inte satt ut alla poäng!");
                }
            }
        }

        protected bool CheckNewCharacterPoints()
        {
            if(((int)ViewState["Health"] / 5) + ((int)ViewState["Stanima"] / 5) + (int)ViewState["Strength"] +
               (int)ViewState["Speed"] + (int)ViewState["Dexterity"] + (int)ViewState["Agility"] == 60)
            {
                return true;
            }
            return false;
        }

        protected void GetRaceEffect(object sender, EventArgs e)
        {
            Race selectedRace = new Race();

            foreach (Race race in (IEnumerable<Race>)ViewState["Races"])
            {
                if(race.RaceID == int.Parse(RaceList.SelectedValue))
                {
                    selectedRace = race;
                    break;
                }
            }

            ViewState["Health"] = ViewState["RaceHealth"] = Health.Text = selectedRace.Health.ToString();
            ViewState["Stanima"] = ViewState["RaceStanima"] = Stanima.Text = selectedRace.Stanima.ToString();
            ViewState["Strength"] = ViewState["RaceStrength"] = Strength.Text = selectedRace.Strength.ToString();
            ViewState["Speed"] = ViewState["RaceSpeed"] = Speed.Text = selectedRace.Speed.ToString();
            ViewState["Agility"] = ViewState["RaceAgility"] = Agility.Text = selectedRace.Agility.ToString();
            ViewState["Dexterity"] = ViewState["RaceDexterity"] = Dexterity.Text = selectedRace.Dexterity.ToString();
            ViewState["PointsLeft"] = 10;
        }

        protected void UpdatePointsLeft()
        {
            PointsLeft.Text = String.Format("Poäng kvar: {0}", ViewState["PointsLeft"]);
        }

        protected void DexterityPlus_Click(object sender, EventArgs e)
        {
            int currentDexterity = (int)(ViewState["Dexterity"] ?? int.Parse(Dexterity.Text));
            if ((int)ViewState["PointsLeft"] > 0)
            {
                currentDexterity++;
                ViewState["Dexterity"] = currentDexterity;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] - 1;
                Dexterity.Text = (int.Parse(Dexterity.Text) + 1).ToString();
                UpdatePointsLeft();
            }
        }

        protected void DexterityMinus_Click(object sender, EventArgs e)
        {
            int currentDexterity = (int)(ViewState["Dexterity"] ?? int.Parse(Dexterity.Text));
            if (currentDexterity > (int)ViewState["RaceDexterity"])
            {
                currentDexterity--;
                ViewState["Dexterity"] = currentDexterity;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] + 1;
                Dexterity.Text = currentDexterity.ToString();
                UpdatePointsLeft();
            }
        }

        protected void AgilityPlus_Click(object sender, EventArgs e)
        {
            int currentAgility = (int)(ViewState["Agility"] ?? int.Parse(Agility.Text));
            if ((int)ViewState["PointsLeft"] > 0)
            {
                currentAgility++;
                ViewState["Agility"] = currentAgility;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] - 1;
                Agility.Text = (int.Parse(Agility.Text) + 1).ToString();
                UpdatePointsLeft();
            }
        }

        protected void AgilityMinus_Click(object sender, EventArgs e)
        {
            int currentAgility = (int)(ViewState["Agility"] ?? int.Parse(Agility.Text));
            if (currentAgility > (int)ViewState["RaceAgility"])
            {
                currentAgility--;
                ViewState["Agility"] = currentAgility;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] + 1;
                Agility.Text = currentAgility.ToString();
                UpdatePointsLeft();
            }
        }

        protected void SpeedPlus_Click(object sender, EventArgs e)
        {
            int currentSpeed = (int)(ViewState["Speed"] ?? int.Parse(Speed.Text));
            if ((int)ViewState["PointsLeft"] > 0)
            {
                currentSpeed++;
                ViewState["Speed"] = currentSpeed;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] - 1;
                Speed.Text = (int.Parse(Speed.Text) + 1).ToString();
                UpdatePointsLeft();
            }
        }

        protected void SpeedMinus_Click(object sender, EventArgs e)
        {
            int currentSpeed = (int)(ViewState["Speed"] ?? int.Parse(Speed.Text));
            if (currentSpeed > (int)ViewState["RaceSpeed"])
            {
                currentSpeed--;
                ViewState["Speed"] = currentSpeed;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] + 1;
                Speed.Text = currentSpeed.ToString();
                UpdatePointsLeft();
            }
        }

        protected void StrengthPlus_Click(object sender, EventArgs e)
        {
            int currentStrength = (int)(ViewState["Strength"] ?? int.Parse(Strength.Text));
            if ((int)ViewState["PointsLeft"] > 0)
            {
                currentStrength++;
                ViewState["Strength"] = currentStrength;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] - 1;
                Strength.Text = (int.Parse(Strength.Text) + 1).ToString();
                UpdatePointsLeft();
            }
        }

        protected void StrengthMinus_Click(object sender, EventArgs e)
        {
            int currentStrength = (int)(ViewState["Strength"] ?? int.Parse(Strength.Text));
            if (currentStrength > (int)ViewState["RaceStrength"])
            {
                currentStrength--;
                ViewState["Speed"] = currentStrength;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] + 1;
                Strength.Text = currentStrength.ToString();
                UpdatePointsLeft();
            }
        }

        protected void StanimaPlus_Click(object sender, EventArgs e)
        {
            int currentStanima = (int)(ViewState["Stanima"] ?? int.Parse(Stanima.Text));
            if ((int)ViewState["PointsLeft"] > 0)
            {
                currentStanima += 5;
                ViewState["Stanima"] = currentStanima;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] - 1;
                Stanima.Text = (int.Parse(Stanima.Text) + 5).ToString();
                UpdatePointsLeft();
            }
        }

        protected void StanimaMinus_Click(object sender, EventArgs e)
        {
            int currentStanima = (int)(ViewState["Stanima"] ?? int.Parse(Stanima.Text));
            if (currentStanima > (int)ViewState["RaceStanima"] + 5)
            {
                currentStanima -= 5;
                ViewState["Stanima"] = currentStanima;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] + 1;
                Stanima.Text = currentStanima.ToString();
                UpdatePointsLeft();
            }
        }

        protected void HealthPlus_Click(object sender, EventArgs e)
        {
            int currentHealth = (int)(ViewState["Health"] ?? int.Parse(Health.Text));
            if ((int)ViewState["PointsLeft"] > 0)
            {
                currentHealth += 5;
                ViewState["Health"] = currentHealth;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] - 1;
                Health.Text = (int.Parse(Health.Text) + 5).ToString();
                UpdatePointsLeft();
            }
        }

        protected void HealthMinus_Click(object sender, EventArgs e)
        {
            int currentHealth = (int)(ViewState["Health"] ?? int.Parse(Health.Text));
            if (currentHealth > (int)ViewState["RaceHealth"] + 5)
            {
                currentHealth -= 5;
                ViewState["Stanima"] = currentHealth;
                ViewState["PointsLeft"] = (int)ViewState["PointsLeft"] + 1;
                Health.Text = currentHealth.ToString();
                UpdatePointsLeft();
            }
        }
    }
}