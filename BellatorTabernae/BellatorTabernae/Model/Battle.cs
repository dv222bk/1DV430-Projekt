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

        // The randomgenerator used to get random numbers
        private Random randomGenerator = new Random();

        // Holder for the combatlog which will be filled by this class and returned.
        private List<CombatLog> combatLog = new List<CombatLog>();

        // List containing the original combatants before the battle
        private List<Combatant> originalCombatants;

        // List containging all combatants which have given up the battle
        private List<Combatant> combatantsGiveUp = new List<Combatant>();

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
            var teamsLeft = combatants.Select(s => s.TeamNumber).Distinct();
            if (teamsLeft.Count() > 1)
            {
                return true;
            }
            return false;
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
                // This check is done since you can't remove elements from an IENumerable (turnOrder)
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
                    if (defender.Stanima > 0 && ((double)defender.Health / defender.MaxHealth) > defender.GiveUpPercent)
                    {
                        break;
                    }
                }

                // Randomize heavy or light attack
                bool heavyAttack = (randomGenerator.NextDouble() >= 0.5) ? true : false;

                // If heavy attack, the defender will evade. If light, the defender will block (but only if a shield/weapon is equipped or the attacker isn't using a weapon)
                int attackChance = AttackChance(attacker);
                int defenseChance = 0;

                if (heavyAttack)
                {
                    // Evade
                    defenseChance = EvadeChance(defender);
                }
                else
                {
                    if (defender.WeaponID != null || defender.ShieldID != null || attacker.WeaponID == null)
                    {
                        // Block
                        defenseChance = BlockAndCounterAttackChance(defender);
                    }
                    else
                    {
                        // Evade
                        defenseChance = EvadeChance(defender);
                    }
                }

                if (attackChance * 1.5 <= defenseChance)
                {
                    // Miss
                    // message miss

                    // Counterattack chance for the defender
                    attackChance = BlockAndCounterAttackChance(defender);
                    defenseChance = BlockAndCounterAttackChance(attacker);

                    if (attackChance > defenseChance)
                    {
                        // Hit

                        // Count the damage the attacker takes
                        int damage = (AttackDamage(defender) - DefenseValue(attacker)) / 2;

                        if (damage <= 0)
                        {
                            damage = 1;
                        }

                        // Update the attacker
                        attacker.Health -= damage;
                    }
                }
                else if (attackChance <= defenseChance)
                {
                    // Evade or block
                    if (heavyAttack)
                    {
                        // message evading
                    }
                    else
                    {
                        if (defender.WeaponID != null || defender.ShieldID != null || attacker.WeaponID == null)
                        {
                            // message blocking
                        }
                        else
                        {
                            // message evading
                        }
                    }

                    // Counterattack chance for the attacker
                    attackChance = BlockAndCounterAttackChance(attacker);
                    defenseChance = EvadeChance(defender);

                    if (attackChance > defenseChance)
                    {
                        // Hit

                        // Count the damage the defender takes
                        int damage = (AttackDamage(attacker) - DefenseValue(defender)) / 2;

                        if (damage <= 0)
                        {
                            damage = 1;
                        }

                        // Update the defender
                        defender.Health -= damage;
                    }
                }
                else
                {
                    // Hit

                    // Count the damage the defender takes
                    int damage = AttackDamage(attacker) - DefenseValue(defender);

                    if (damage <= 0)
                    {
                        damage = 1;
                    }

                    // Update the defender
                    defender.Health -= damage;
                }

                // Cleanup step
                AttackCleanUp(attacker, defender, ref combatants);
            }
        }

        public void CombatCleanUp(List<Combatant> combatants)
        {
            // Check so there's only one team left alive
            if (!ContinueBattle(combatants))
            {
                // Give XP and gold to all team members of the winning team
                int winningTeamNumber = combatants.First().TeamNumber;
                List<Combatant> finalResults = new List<Combatant>();
                finalResults.AddRange(combatants);
                finalResults.AddRange(combatantsGiveUp);

                List<Combatant> winningTeam = finalResults.FindAll(x => x.TeamNumber == winningTeamNumber);
                List<Combatant> losers = finalResults.FindAll(x => x.TeamNumber != winningTeamNumber);

                int goldReward = GetGoldRewards(losers);
                int survivingWinners = winningTeam.FindAll(x => x.Health != 0).Count();

                foreach (Combatant winner in winningTeam)
                {
                    if(winner.Health > 0)
                    {
                        int xpToNextLevel = (int)((winner.Level * 25) + (winner.Level * 25) * 1.1) - winner.Experience;
                        int gainedXP = 0;

                        foreach (Combatant loser in losers)
                        {
                            gainedXP += (int)(((20 + loser.Level * loser.Level * loser.Level) / 2) * (loser.Level / winner.Level));

                            if (gainedXP > xpToNextLevel)
                            {
                                gainedXP = xpToNextLevel;
                                winner.Level++;
                                break;
                            }
                        }
                        winner.Experience += gainedXP;
                        Service.AddGoldToInventory(winner.CharID, goldReward / survivingWinners);
                    }
                }

                // Update each character in the battle
                foreach (Combatant combatant in finalResults)
                {
                    if(combatant.UserID != null)
                    {
                        Service.UpdateCharacterStats(combatant.CharID, combatant.Level, combatant.Experience, combatant.Health, combatant.MaxHealth);
                    }
                }
            }
            else
            {
                throw new ApplicationException("Ett kritiskt fel inträffade under striden och den avbröts.");
            }
        }

        public int GetGoldRewards(List<Combatant> losers)
        {
            int goldRewards = 0;

            foreach (Combatant loser in losers)
            {
                goldRewards += 3 + (loser.Level * randomGenerator.Next(1, 10));
            }

            return goldRewards;
        }

        public void AttackCleanUp(Combatant attacker, Combatant defender, ref List<Combatant> combatants)
        {
            // Both combatants lose stanima for their participation in the attack.
            attacker.Stanima -= 1;
            defender.Stanima -= 1;

            if (defender.Stanima <= 0 || ((double)defender.Health / defender.MaxHealth) < defender.GiveUpPercent)
            {
                combatantsGiveUp.Add(defender);
                combatants.Remove(combatants.Find(x => x.CombatantID == defender.CombatantID));
            }

            if (attacker.Stanima <= 0 || ((double)attacker.Health / attacker.MaxHealth) < attacker.GiveUpPercent)
            {
                combatantsGiveUp.Add(attacker);
                combatants.Remove(combatants.Find(x => x.CombatantID == attacker.CombatantID));
            }
        }

        public int AttackChance(Combatant combatant)
        {
            int agility = combatant.Agility;
            int speed = combatant.Speed;

            if (agility < speed)
            {
                if (agility * 1.5 < speed)
                {
                    speed = (int)(agility * 1.5);
                }

                return randomGenerator.Next(agility / 2, agility + speed);
            }
            else
            {
                if (speed * 1.5 < agility)
                {
                    agility = (int)(speed * 1.5);
                }

                return randomGenerator.Next(speed / 2, agility + speed);
            }
        }

        public int EvadeChance(Combatant combatant)
        {
            int speed = combatant.Speed;
            int dexterity = combatant.Dexterity;

            if (speed < dexterity)
            {
                if (speed * 1.5 < dexterity)
                {
                    dexterity = (int)(speed * 1.5);
                }

                return randomGenerator.Next(speed / 2, speed + dexterity);
            }
            else
            {
                if (dexterity * 1.5 < speed)
                {
                    speed = (int)(dexterity * 1.5);
                }

                return randomGenerator.Next(dexterity / 2, speed + dexterity);
            }
        }

        public int BlockAndCounterAttackChance(Combatant combatant)
        {
            int agility = combatant.Speed;
            int dexterity = combatant.Dexterity;

            if (agility < dexterity)
            {
                if (agility * 1.5 < dexterity)
                {
                    dexterity = (int)(agility * 1.5);
                }

                return randomGenerator.Next(agility / 2, agility + dexterity);
            }
            else
            {
                if (dexterity * 1.5 < agility)
                {
                    agility = (int)(dexterity * 1.5);
                }

                return randomGenerator.Next(dexterity / 2, agility + dexterity);
            }
        }

        public int AttackDamage(Combatant combatant)
        {
            int strength = combatant.Strength;

            if (combatant.WeaponID != null)
            {
                int weaponDamage = Service.GetEquipmentStats(null, combatant.WeaponID).Damage;

                if (weaponDamage < strength) 
                {
                    return randomGenerator.Next(weaponDamage, weaponDamage + (strength / 2));
                }
                else
                {
                    return randomGenerator.Next(strength, strength + (weaponDamage / 2));
                }
            }
            else
            {
                return randomGenerator.Next(0, strength / 2);
            }
        }

        public int DefenseValue(Combatant combatant)
        {
            int strength = combatant.Strength;

            if (combatant.ArmorID != null)
            {
                int armorDefense = Service.GetEquipmentStats(null, combatant.ArmorID).Defense;

                if (armorDefense < strength)
                {
                    return randomGenerator.Next(armorDefense, armorDefense + (strength / 4));
                }
                else
                {
                    return randomGenerator.Next(strength, strength + (armorDefense / 4));
                }
            }
            else
            {
                return randomGenerator.Next(0, strength / 4);
            }
        }
    }
}