using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BellatorTabernae.Model;

namespace BellatorTabernae.Model
{
    public class Battle
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        private Random randomGenerator = new Random();
        private List<CombatLog> combatLog = new List<CombatLog>();
        private List<Combatant> originalCombatants;

        public List<CombatLog> InitBattle(List<Combatant> combatants)
        {
            if (ValidateCombatants(combatants))
            {
                AssignCombatantIDs(ref combatants);
                originalCombatants = combatants;
                while (ContinueBattle(combatants))
                {
                    Turn(ref combatants);
                }
                CombatCleanUp(combatants);
                return combatLog;
            }
            else
            {
                throw new ArgumentException("Kombatanterna i striden är ogiltliga");
            }
        }

        public bool ContinueBattle(List<Combatant> combatants)
        {
            return true;
        }

        public bool ValidateCombatants(List<Combatant> combatants)
        {
            int numberOfCombatants = combatants.Count;
            if(numberOfCombatants > 1) 
            {
                foreach(Combatant combatant in combatants) 
                {
                    ICollection<ValidationResult> validationResults;
                    if (!combatant.Validate(out validationResults))
                    {
                        return false;
                    }
                }
                if (combatants.Select(x => x.TeamNumber).Distinct().Count() >= 2)
                {
                    return true;
                }
            }
            return false;
        }

        public void AssignCombatantIDs(ref List<Combatant> combatants)
        {
            int numberCombatants = combatants.Count();

            for (int i = 0; i < combatants.Count(); i += 1)
            {
                combatants[i].CombatantID = i;
            }
        }

        public IEnumerable<KeyValuePair<int, Combatant>> CreateTurnOrder(List<Combatant> combatants) {
            var turnOrder = new Dictionary<int, Combatant>();

            /*
             * This line might need some explanation.
             * Basically, this puts each combatant in a random order in the list. (as random as it gets anyway)
             * This is done to make the attack order more fair.
             * The first combatant to get a specific speed value always has an advantage and if the list wasn't random
             * the same combatant would always get this edge.
             */
            combatants.OrderBy(item => randomGenerator.Next());

            foreach (Combatant combatant in combatants)
            {
                // Get the combatants speed and the speed from it's equipment
                int totalSpeed = combatant.Speed;
                if (combatant.ArmorID != null)
                {
                    totalSpeed += Service.GetEquipmentStats(null, combatant.ArmorID).Speed;
                }
                if (combatant.WeaponID != null)
                {
                    totalSpeed += Service.GetEquipmentStats(null, combatant.WeaponID).Speed;
                }
                if (combatant.ShieldID != null)
                {
                    totalSpeed += Service.GetEquipmentStats(null, combatant.ShieldID).Speed;
                }

                // Check how many times the combatant attacks
                int numberOfAttacks = totalSpeed / 30;
                if (numberOfAttacks < 1)
                {
                    numberOfAttacks = 1;
                }

                // For each attack, asign the combatant a random number and put it in the speedList dictionary.
                for (int attack = 0; attack < numberOfAttacks; attack += 1)
                {
                    int randomNumber = randomGenerator.Next(0, totalSpeed + 1);
                    while (true)
                    {
                        if (turnOrder.ContainsKey(randomNumber))
                        {
                            randomNumber--;
                        }
                        else
                        {
                            break;
                        }
                    }
                    turnOrder.Add(randomNumber, combatant);
                }
            }

            // sort the speedList dictionary based on the randomNumber
            var sortedTurnOrder = from pair in turnOrder
                    orderby pair.Key descending
                    select pair;

            return sortedTurnOrder;
        }

        public void Turn(ref List<Combatant> combatants)
        {
            var turnOrder = CreateTurnOrder(combatants);
            foreach (KeyValuePair<int, Combatant> pair in turnOrder)
            {
                Combatant attacker = pair.Value; // For easier more logical use in the code to come

                // If the attackers stanima is 0 or less, or if the attackers health is below the giveuppercent of it's max health, the attacker won't attack.
                if (attacker.Stanima <= 0 || ((double)attacker.Health / attacker.MaxHealth) < attacker.GiveUpPercent)
                {
                    continue;
                }

                // Fill a list with combatants from other teams
                List<Combatant> otherTeamCombatants = combatants.FindAll(s => s.TeamNumber != attacker.TeamNumber);

                // If there are no combatants in other teams
                if(otherTeamCombatants.Count() < 1) 
                {
                    break;
                }

                // Find a random defender
                Combatant defender;
                while (true)
                {
                    defender = otherTeamCombatants[randomGenerator.Next(0, otherTeamCombatants.Count())];
                    if (defender.Stanima <= 0 || ((double)defender.Health / defender.MaxHealth) < defender.GiveUpPercent)
                    {
                        break;
                    }
                }
            }
        }

        public void CombatCleanUp(List<Combatant> combatants)
        {

        }
    }
}