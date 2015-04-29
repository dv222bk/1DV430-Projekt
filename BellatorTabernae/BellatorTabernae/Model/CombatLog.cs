using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellatorTabernae.Model
{
    public class CombatLog
    {
        public int TextNumber { get; set; }

        public string Text { get; set; }

        public Character Attacker { get; set; }

        public Character Defender { get; set; }

        public int? AttackerDamage { get; set; }

        public int? DefenderDamage { get; set; }
    }
}