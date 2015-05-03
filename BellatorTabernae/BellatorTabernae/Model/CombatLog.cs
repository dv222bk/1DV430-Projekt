using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellatorTabernae.Model
{
    public class CombatLog
    {
        public int CombatLogID { get; set; }

        public string Text { get; set; }

        public string Attacker { get; set; }

        public string Defender { get; set; }

        public int? AttackerDamage { get; set; }

        public int? DefenderDamage { get; set; }

        public CombatLog(int combatLogID, string text, string attacker = null, string defender = null, int? attackerDamage = null, int? defenderDamage = null)
        {
            CombatLogID = combatLogID;
            Text = text;
            Attacker = attacker;
            Defender = defender;
            AttackerDamage = attackerDamage;
            DefenderDamage = defenderDamage;
        }
    }
}