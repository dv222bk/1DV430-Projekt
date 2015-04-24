using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    public class Equipment
    {
        public int EquipID { get; set; }

        [Range(1, 32767, ErrorMessage = "Utrustningens stats ID måste vara inom den positiva smallint skalan!")]
        public int EquipStatsID { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha en typ!")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Utrustningens typ får högst bestå av 10 tecken.")]
        public string EquipType { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett namn!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Utrustningens namn får högst bestå av 50 tecken.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Utrustningen måste ha ett värde!")]
        [Range(0, int.MaxValue, ErrorMessage = "Utrustningen måste ha ett positivt värde!")]
        public int Value { get; set; }
    }
}