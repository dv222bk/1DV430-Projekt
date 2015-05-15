using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Routing;
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
            int charID;
            try
            {
                if (int.TryParse(Page.RouteData.Values["charID"].ToString(), out charID))
                {
                    if (!Service.IsCharacterUsers(charID, int.Parse(Context.User.Identity.Name)))
                    {
                        EditCharacterBiografyButton.Visible = false;
                        RemoveCharacter.Visible = false;
                        GetCharacter(false);
                        return; // If this path is choosen, nothing else should happen in this method.
                    }
                }
            }
            catch 
            {
                EditCharacterBiografyButton.Visible = true;
                RemoveCharacter.Visible = true;
            }
            if (Context.User.Identity.IsAuthenticated && Service.UserHasCharacter(int.Parse(Context.User.Identity.Name)))
            {
                GetCharacter(true);
            }
            else if (Context.User.Identity.IsAuthenticated)
            {
                CreateNewCharacter();
            }
            else
            {
                Session["SiteMsg"] = "Du måste vara inloggad för att titta på karaktärsidor!";
                Response.RedirectToRoute("Default");
            }
        }

        protected void GetCharacter(bool ownCharacter)
        {
            try
            {
                Model.Character character = new Model.Character();
                if (!ownCharacter)
                {
                    int charID;
                    if (int.TryParse(Page.RouteData.Values["charID"].ToString(), out charID))
                    {
                        character = Service.GetCharacter(charID);
                    }
                    else
                    {
                        throw new ArgumentException("Felaktigt karaktärsID!");
                    }
                } 
                else 
                {
                    character = Service.GetCharacter(null, int.Parse(Context.User.Identity.Name));
                }
                CharacterPanel.Visible = true;
                CharacterName.Text = character.Name;
                CharacterRace.Text = String.Format("Ras: {0}", character.Race);
                CharacterLevel.Text = String.Format("Level: {0}", character.Level);
                CharacterExperience.Text = String.Format("XP: {0}", character.Experience);
                CharacterHealth.Text = String.Format("Livspoäng: {0}/{1}", character.Health, character.MaxHealth);
                CharacterStanima.Text = String.Format("Uthållighetspoäng: {0}/{1}", character.Stanima, character.MaxStanima);
                CharacterStrength.Text = String.Format("Styrka: {0}", character.Strength);
                CharacterSpeed.Text = String.Format("Snabbhet: {0}", character.Speed);
                CharacterAgility.Text = String.Format("Undvika: {0}", character.Agility);
                CharacterDexterity.Text = String.Format("Träffsäkerhet: {0}", character.Dexterity);
                if (character.Biografy != null)
                {
                    CharacterBiografyLiteral.Text = character.Biografy;
                }
                else
                {
                    CharacterBiografyLiteral.Text = "Du har inte skrivt någon biografi till din karaktär ännu!";
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
            NewCharacterPanel.Visible = true;
            try
            {
                if (Session["Races"] == null)
                {
                    IEnumerable<Race> Races = Service.GetRaces();
                    Session["Races"] = Races;
                    Session["NewRace"] = true;
                }

                if (!IsPostBack)
                {
                    ListItem listItem;

                    foreach (Race race in (IEnumerable<Race>)Session["Races"])
                    {
                        listItem = new ListItem(race.RaceName, race.RaceID.ToString());
                        RaceList.Items.Add(listItem);
                    }
                }

                if (bool.Parse(Session["NewRace"].ToString()) == true)
                {
                    GetRaceEffect();
                    Session["NewRace"] = false;
                }
                else
                {
                    InsertPoints();
                    UpdateRaceInfo();
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

        protected void CreateCharacter_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (CheckNewCharacterPoints())
                {
                    try
                    {
                        Model.Character newCharacter = new Model.Character
                        {
                            CharID = 0,
                            UserID = int.Parse(Context.User.Identity.Name),
                            Race = RaceList.SelectedItem.Text,
                            Name = Name.Text,
                            Level = 1,
                            Experience = 0,
                            Health = int.Parse(Session["Health"].ToString()),
                            MaxHealth = int.Parse(Session["Health"].ToString()),
                            Stanima = int.Parse(Session["Stanima"].ToString()),
                            MaxStanima = int.Parse(Session["Stanima"].ToString()),
                            Strength = int.Parse(Session["Strength"].ToString()),
                            Speed = int.Parse(Session["Speed"].ToString()),
                            Agility = int.Parse(Session["Agility"].ToString()),
                            Dexterity = int.Parse(Session["Dexterity"].ToString()),
                            WeaponID = null,
                            ShieldID = null,
                            ArmorID = null,
                            Biografy = null,
                            CreatedOn = new DateTime()
                        };

                        Service.CreateCharacter(newCharacter, int.Parse(RaceList.SelectedValue));

                        // Reset all sessions
                        Session["Health"] = Session["Stanima"] = Session["Strength"] = Session["Speed"] =
                            Session["Agility"] = Session["Dexterity"] = Session["RaceHealth"] =
                            Session["RaceStanima"] = Session["RaceStrength"] = Session["RaceSpeed"] =
                            Session["RaceAgility"] = Session["RaceDexterity"] = Session["Races"] =
                            Session["NewRace"] = Session["PointsLeft"] = null;

                        Response.RedirectToRoute("Character");
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
                else
                {
                    Page.ModelState.AddModelError(String.Empty, "Du har inte satt ut alla poäng!");
                }
            }
        }

        protected bool CheckNewCharacterPoints()
        {
            if (Session["Health"] != null && Session["Stanima"] != null && Session["Strength"] != null &&
                Session["Speed"] != null && Session["Dexterity"] != null && Session["Agility"] != null)
            {
                if ((int.Parse(Session["Health"].ToString()) / 5) + (int.Parse(Session["Stanima"].ToString()) / 5) + int.Parse(Session["Strength"].ToString()) +
                   int.Parse(Session["Speed"].ToString()) + int.Parse(Session["Dexterity"].ToString()) + int.Parse(Session["Agility"].ToString()) == 60)
                {
                    return true;
                }
            }
            return false;
        }

        protected void GetRaceEffect()
        {
            Race selectedRace = new Race();

            foreach (Race race in (IEnumerable<Race>)Session["Races"])
            {
                if(race.RaceID == int.Parse(RaceList.SelectedValue))
                {
                    selectedRace = race;
                    break;
                }
            }

            Session["Health"] = Session["RaceHealth"] = Health.Text = selectedRace.Health.ToString();
            Session["Stanima"] = Session["RaceStanima"] = Stanima.Text = selectedRace.Stanima.ToString();
            Session["Strength"] = Session["RaceStrength"] = Strength.Text = selectedRace.Strength.ToString();
            Session["Speed"] = Session["RaceSpeed"] = Speed.Text = selectedRace.Speed.ToString();
            Session["Agility"] = Session["RaceAgility"] = Agility.Text = selectedRace.Agility.ToString();
            Session["Dexterity"] = Session["RaceDexterity"] = Dexterity.Text = selectedRace.Dexterity.ToString();
            Session["RaceDesc"] = selectedRace.RaceDesc;
            Session["PointsLeft"] = 10;

            UpdateRaceInfo();

            UpdatePointsLeft();
        }

        protected void InsertPoints()
        {
            Health.Text = Session["Health"].ToString();
            Stanima.Text = Session["Stanima"].ToString();
            Strength.Text = Session["Strength"].ToString();
            Speed.Text = Session["Speed"].ToString();
            Agility.Text = Session["Agility"].ToString();
            Dexterity.Text = Session["Dexterity"].ToString();

            UpdatePointsLeft();
        }

        protected void UpdateRaceInfo()
        {
            HealthLabel.Text = String.Format("Livspoäng ({0}): ", Session["RaceHealth"].ToString());
            StanimaLabel.Text = String.Format("Uthållighetspoäng ({0}): ", Session["RaceStanima"].ToString());
            StrengthLabel.Text = String.Format("Styrka ({0}): ", Session["RaceStrength"].ToString());
            SpeedLabel.Text = String.Format("Snabbhet ({0}): ", Session["RaceSpeed"].ToString());
            AgilityLabel.Text = String.Format("Träffsäkerhet ({0}): ", Session["RaceAgility"].ToString());
            DexterityLabel.Text = String.Format("Undvika ({0}): ", Session["RaceDexterity"].ToString());
            RaceDesc.Text = Session["RaceDesc"].ToString();
        }

        protected void UpdatePointsLeft()
        {
            PointsLeft.Text = String.Format("Poäng kvar: {0}", Session["PointsLeft"]);
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
            if (currentDexterity > int.Parse(Session["RaceDexterity"].ToString()))
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
            if (currentAgility > int.Parse(Session["RaceAgility"].ToString()))
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
            if (currentSpeed > int.Parse(Session["RaceSpeed"].ToString()))
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
            if (currentStrength > int.Parse(Session["RaceStrength"].ToString()))
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
            if (currentStanima > int.Parse(Session["RaceStanima"].ToString()))
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
            if (currentHealth > int.Parse(Session["RaceHealth"].ToString()))
            {
                currentHealth -= 5;
                Session["Health"] = currentHealth;
                Session["PointsLeft"] = int.Parse(Session["PointsLeft"].ToString()) + 1;
                Health.Text = currentHealth.ToString();
                UpdatePointsLeft();
            }
        }

        protected void RemoveCharacter_Click(object sender, EventArgs e)
        {
            try
            {
                Service.DeleteCharacter(null, int.Parse(Context.User.Identity.Name));
                Response.RedirectToRoute("Character");
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

        protected void ChangeRace(object sender, EventArgs e)
        {
            Session["NewRace"] = true;
        }

        protected void EditCharacterBiografyButton_Click(object sender, EventArgs e)
        {
            EditCharacterBiografyButton.Visible = false;
            CharacterBiografyLiteral.Visible = false;
            EditCharacterBiografy.Visible = true;
            SubmitBiografy.Visible = true;
            EditCharacterBiografy.Text = CharacterBiografyLiteral.Text;
        }

        protected void SubmitBiografy_Click(object sender, EventArgs e)
        {
            try
            {
                Service.UpdateCharacterBiografy(int.Parse(Context.User.Identity.Name), null, EditCharacterBiografy.Text);
                EditCharacterBiografyButton.Visible = true;
                CharacterBiografyLiteral.Visible = true;
                EditCharacterBiografy.Visible = false;
                SubmitBiografy.Visible = false;
                CharacterBiografyLiteral.Text = EditCharacterBiografy.Text;
            }
            catch (SqlException ex)
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