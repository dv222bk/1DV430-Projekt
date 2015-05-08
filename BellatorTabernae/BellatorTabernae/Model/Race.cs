using System;
using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    [Serializable]
    public class Race
    {
        public int RaceID { get; set; }

        [Required(ErrorMessage = "Rasen måste ha ett namn!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Rasens namn får inte innehålla mer än 50 tecken!")]
        public string RaceName { get; set; }

        [Required(ErrorMessage = "Rasen måste ha en beskrivning!")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Rasens beskrivning får inte innehålla mer än 500 tecken!")]
        public string RaceDesc { get; set; }

        [Required(ErrorMessage = "Rasen måste ha en bashälsa!")]
        [Range(1, 32767, ErrorMessage = "Rasens bashälsa måste ligga mellan 1 och 32767")]
        public int Health { get; set; }

        [Required(ErrorMessage = "Rasen måste ha en basuthållighet!")]
        [Range(1, 32767, ErrorMessage = "Rasens basuthållighet måste ligga mellan 1 och 32767")]
        public int Stanima { get; set; }

        [Required(ErrorMessage = "Rasen måste ha en basstyrka!")]
        [Range(1, 255, ErrorMessage = "Rasens basstyrka måste ligga mellan 1 och 255")]
        public int Strength { get; set; }

        [Range(1, 255, ErrorMessage = "Rasens bassnabbhet måste ligga mellan 1 och 255")]
        [Required(ErrorMessage = "Rasen måste ha en bassnabbhet!")]
        public int Speed { get; set; }

        [Required(ErrorMessage = "Rasen måste ha en basträffsäkerhet!")]
        [Range(1, 255, ErrorMessage = "Rasens basträffsäkerhet måste ligga mellan 1 och 255")]
        public int Agility { get; set; }

        [Required(ErrorMessage = "Rasen måste ha en basundvika!")]
        [Range(1, 255, ErrorMessage = "Rasens basundvika måste ligga mellan 1 och 255")]
        public int Dexterity { get; set; }
    }
}