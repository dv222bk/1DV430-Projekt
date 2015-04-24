using System.ComponentModel.DataAnnotations;
using System;

namespace BellatorTabernae.Model
{
    public class Character
    {
        public int CharID { get; set; }

        [Required(ErrorMessage = "Karaktären måste tillhöra en användare!")]
        public int UserID { get; set; }

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
        public int Health { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett max antal livspoäng!")]
        public int MaxHealth { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha uthållighetspoäng!")]
        public int Stanima { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett max antal uthållighetspoäng!")]
        public int MaxStanima { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett värde i styrka!")]
        public int Strength { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett värde i snabbhet!")]
        public int Speed { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett värde i undvika!")]
        public int Dexterity { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett värde i agility!")]
        public int Agility { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Karaktärens vapen har ett felaktigt ID!")]
        public int? WeaponID { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Karaktärens sköld har ett felaktigt ID!")]
        public int? ShieldID { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Karaktärens rustning har ett felaktigt ID!")]
        public int? ArmorID { get; set; }

        [StringLength(2000, MinimumLength = 0, ErrorMessage = "Karaktärens biografi får högst innehålla 2000 tecken!")]
        public string? Biografy { get; set; }

        [Required(ErrorMessage = "Det är viktigt att veta när karaktären skapades!")]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
    }
}