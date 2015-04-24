using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    public class Race
    {
        public int RaceID { get; set; }

        [Required(ErrorMessage = "Rasen måste ha ett namn!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Rasens namn får inte innehålla mer än 50 tecken!")]
        public string RaceName { get; set; }

        [Required(ErrorMessage = "Rasen måste ha en beskrivning!")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Rasens beskrivning får inte innehålla mer än 500 tecken!")]
        public string RaceDesc { get; set; }
    }
}