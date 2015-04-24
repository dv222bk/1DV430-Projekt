using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
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
    }
}