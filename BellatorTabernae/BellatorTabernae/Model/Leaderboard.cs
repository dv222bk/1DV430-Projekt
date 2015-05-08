using System;
using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    [Serializable]
    public class Leaderboard
    {
        [Required(ErrorMessage = "Karaktären måste ha ett radnummer i leaderboarden")]
        public int RowNumber { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha en leaderboard ranking!")]
        public int Rank { get; set; }

        [Required(ErrorMessage = "Det måste finnas en karaktär!")]
        public int CharID { get; set; }

        [Required(ErrorMessage = "Karaktären måste tillhöra en ras!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Karaktärens ras får max innehålla 50 tecken.")]
        public string Race { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett namn!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Karaktärens namn får högst bestå av 50 tecken.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha en level!")]
        [Range(1, 255, ErrorMessage = "Karaktärens level måste vara mellan 1 och 255")]
        public int Level { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett max antal livspoäng!")]
        [Range(1, 32767, ErrorMessage = "Karaktärens maximala hälsa kan inte ligga över 32767!")]
        public int MaxHealth { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha ett max antal uthållighetspoäng!")]
        [Range(1, 32767, ErrorMessage = "Karaktärens maximala uthålighet kan inte ligga över 32767!")]
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
    }
}