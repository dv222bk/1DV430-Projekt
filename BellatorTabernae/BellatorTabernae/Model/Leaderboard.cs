using System;
using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    [Serializable]
    public class Leaderboard
    {
        [Required(ErrorMessage = "Karaktären måste ha ett radnummer i leaderboarden")]
        public long RowNumber { get; set; }

        [Required(ErrorMessage = "Karaktären måste ha en leaderboard ranking!")]
        public long Rank { get; set; }

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
    }
}