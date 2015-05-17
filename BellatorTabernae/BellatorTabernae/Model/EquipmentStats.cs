using System;
using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    [Serializable]
    public class EquipmentStats
    {
        public int EquipStatsID { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett livspoängvärde")]
        public int Health { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett uthållighetspoängvärde!")]
        public int Stanima { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett värde i styrka!")]
        public int Strength { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett värde i snabbhet!")]
        public int Speed { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett värde i undvika!")]
        public int Dexterity { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett värde i träffsäkerhet!")]
        public int Agility { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett värde i skada!")]
        public int Damage { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett värde i skydd!")]
        public int Defense { get; set; }

        public string EffectString
        {
            get
            {
                string returnString = "";
                if (Health != 0)
                {
                    returnString = String.Concat(returnString, "<span class=\"EquipmentStats\">Hälsa: ", Health, "</span>");
                }
                if (Stanima != 0)
                {
                    returnString = String.Concat(returnString, "<span class=\"EquipmentStats\">Uthållighet: ", Stanima, "</span>");
                }
                if (Strength != 0)
                {
                    returnString = String.Concat(returnString, "<span class=\"EquipmentStats\">Styrka: ", Strength, "</span>");
                }
                if (Speed != 0)
                {
                    returnString = String.Concat(returnString, "<span class=\"EquipmentStats\">Snabbhet: ", Speed, "</span>");
                }
                if (Dexterity != 0)
                {
                    returnString = String.Concat(returnString, "<span class=\"EquipmentStats\">Undvika: ", Dexterity, "</span>");
                }
                if (Agility != 0)
                {
                    returnString = String.Concat(returnString, "<span class=\"EquipmentStats\">Träffsäkerhet: ", Agility, "</span>");
                }
                if (Damage != 0)
                {
                    returnString = String.Concat(returnString, "<span class=\"EquipmentStats\">Skada: ", Damage, "</span>");
                }
                if (Defense != 0)
                {
                    returnString = String.Concat(returnString, "<span class=\"EquipmentStats\">Skydd: ", Defense, "</span>");
                }
                return returnString;
            }
        }
    }
}