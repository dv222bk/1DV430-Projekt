using System.ComponentModel.DataAnnotations;
using System;

namespace BellatorTabernae.Model
{
    public class Combatant
    {
        public int CharID { get; set; }

        [Required(ErrorMessage = "Karaktären måste tillhöra en användare!")]
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Karaktären måste tillhöra en ras!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Karaktärens ras får max innehålla 50 tecken.")]
        public string Race { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett namn!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Karaktärens namn får högst bestå av 50 tecken.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha en level!")]
        [Range(1, 255, ErrorMessage = "Karaktärens level måste vara mellan 1 och 255")]
        public int Level { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett experience värde!")]
        [Range(0, int.MaxValue, ErrorMessage = "Karaktären har ett felaktigt experience värde!")]
        public int Experience { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha livspoäng!")]
        [Range(-32767, 32767, ErrorMessage = "Karaktärens hälsa kan inte ligga över 32767!")]
        public int Health { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett max antal livspoäng!")]
        [Range(1, 32767, ErrorMessage = "Karaktärens maximala hälsa kan inte ligga över 32767!")]
        public int MaxHealth { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha uthållighetspoäng!")]
        [Range(-32767, 32767, ErrorMessage = "Karaktärens uthållighet kan inte ligga över 32767!")]
        public int Stanima { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett max antal uthållighetspoäng!")]
        [Range(1, 32767, ErrorMessage = "Karaktärens maximala uthållighet kan inte ligga över 32767!")]
        public int MaxStanima { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett värde i styrka!")]
        [Range(1, 255, ErrorMessage = "Karaktärens styrka kan inte ligga över 255!")]
        public int Strength { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett värde i snabbhet!")]
        [Range(1, 255, ErrorMessage = "Karaktärens snabbhet kan inte ligga över 255!")]
        public int Speed { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett värde i undvika!")]
        [Range(1, 255, ErrorMessage = "Karaktärens undvika kan inte ligga över 255!")]
        public int Dexterity { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett värde i träffsäkerhet!")]
        [Range(1, 255, ErrorMessage = "Karaktärens träffsäkerhet kan inte ligga över 255!")]
        public int Agility { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Karaktärens vapen har ett felaktigt ID!")]
        public int? WeaponID { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Karaktärens sköld har ett felaktigt ID!")]
        public int? ShieldID { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Karaktärens rustning har ett felaktigt ID!")]
        public int? ArmorID { get; set; }

        [Required(ErrorMessage = "Karaktären måste ge upp vid en viss procent!")]
        [Range(0, 1, ErrorMessage = "Felaktigt ge upp procent värde!")]
        public double GiveUpPercent { get; set; }

        [Required(ErrorMessage = "Karaktären måste tillhöra ett lag!")]
        [Range(1, int.MaxValue, ErrorMessage = "Karaktären befinner sig i ett felaktigt lag!")]
        public int TeamNumber { get; set; }

        public int CombatantID { get; set; }

        public Combatant()
        {
            GiveUpPercent = 0.20;
        }
    }
}