using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    public class Inventory
    {
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "Det måste finnas en utrustning i ägodelsplatsen!")]
        public int EquipID { get; set; }

        [Required(ErrorMessage = "Ägodelsplatsen måste ägas av en karaktär!")]
        public int CharID { get; set; }

        [Required(ErrorMessage = "Antalet utrustningar på ägodelsplatsen måste anges!")]
        [Range(0, int.MaxValue, ErrorMessage = "Antalet utrustningar på ägodelsplatsen måste vara positivt!")]
        public int Number { get; set; }
    }
}