using System;
using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    public class Chat
    {
        public int MsgID { get; set; }

        [Required(ErrorMessage = "Det måste finnas en författare till chatinlägget!")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Chatinlägget måste innehålla ett meddelande!")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Meddelandet kan högst innehålla 250 tecken!")]
        public string Msg { get; set; }

        [Required(ErrorMessage = "Ett datum när inlägget skrev måste finnas med!")]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
    }
}