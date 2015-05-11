using System;
using BellatorTabernae.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace BattleUnitTest
{
    [TestClass]
    public class BattleTest 
    {
        //Combatants
        private Combatant combatant1 = new Combatant
        {
            CharID = 1,
            UserID = 1,
            Race = "Dvärg",
            Name = "Jollepoker",
            Level = 1,
            Experience = 0,
            Health = 25,
            MaxHealth = 25,
            Stanima = 25,
            MaxStanima = 25,
            Strength = 10,
            Speed = 10,
            Dexterity = 10,
            Agility = 10,
            WeaponID = null,
            ShieldID = null,
            ArmorID = null,
            GiveUpPercent = 0.20,
            TeamNumber = 1,
            CombatantID = 1
        };

        private Combatant combatant2 = new Combatant
        {
            CharID = 2,
            UserID = 2,
            Race = "Människa",
            Name = "Åke",
            Level = 1,
            Experience = 0,
            Health = 25,
            MaxHealth = 25,
            Stanima = 25,
            MaxStanima = 25,
            Strength = 10,
            Speed = 10,
            Dexterity = 10,
            Agility = 10,
            WeaponID = null,
            ShieldID = null,
            ArmorID = null,
            GiveUpPercent = 0.20,
            TeamNumber = 2,
            CombatantID = 2
        };

        private Combatant combatant3 = new Combatant
        {
            CharID = 3,
            UserID = 3,
            Race = "Alv",
            Name = "AkNar",
            Level = 1,
            Experience = 0,
            Health = 25,
            MaxHealth = 25,
            Stanima = 25,
            MaxStanima = 25,
            Strength = 10,
            Speed = 10,
            Dexterity = 10,
            Agility = 10,
            WeaponID = null,
            ShieldID = null,
            ArmorID = null,
            GiveUpPercent = 0.20,
            TeamNumber = 2,
            CombatantID = 3
        };

        private Combatant deadCombatant1 = new Combatant
        {
            CharID = 4,
            UserID = 4,
            Race = "Människa",
            Name = "Sture",
            Level = 1,
            Experience = 0,
            Health = 0,
            MaxHealth = 25,
            Stanima = 25,
            MaxStanima = 25,
            Strength = 10,
            Speed = 10,
            Dexterity = 10,
            Agility = 10,
            WeaponID = null,
            ShieldID = null,
            ArmorID = null,
            GiveUpPercent = 0.20,
            TeamNumber = 1,
            CombatantID = 4
        };

        private Combatant tiredCombatant1 = new Combatant
        {
            CharID = 4,
            UserID = 4,
            Race = "Människa",
            Name = "Sture",
            Level = 1,
            Experience = 0,
            Health = 25,
            MaxHealth = 25,
            Stanima = 0,
            MaxStanima = 25,
            Strength = 10,
            Speed = 10,
            Dexterity = 10,
            Agility = 10,
            WeaponID = null,
            ShieldID = null,
            ArmorID = null,
            GiveUpPercent = 0.20,
            TeamNumber = 1,
            CombatantID = 4
        };

        public Combatant CopyCombatant(Combatant combatantToCopy)
        {
            return new Combatant
            {
                CharID = combatantToCopy.CharID,
                UserID = combatantToCopy.UserID,
                Race = combatantToCopy.Race,
                Name = combatantToCopy.Name,
                Level = combatantToCopy.Level,
                Experience = combatantToCopy.Experience,
                Health = combatantToCopy.Health,
                MaxHealth = combatantToCopy.MaxHealth,
                Stanima = combatantToCopy.Stanima,
                MaxStanima = combatantToCopy.MaxStanima,
                Strength = combatantToCopy.Strength,
                Speed = combatantToCopy.Speed,
                Dexterity = combatantToCopy.Dexterity,
                Agility = combatantToCopy.Agility,
                WeaponID = combatantToCopy.WeaponID,
                ShieldID = combatantToCopy.ShieldID,
                ArmorID = combatantToCopy.ArmorID,
                GiveUpPercent = combatantToCopy.GiveUpPercent,
                TeamNumber = combatantToCopy.TeamNumber,
                CombatantID = combatantToCopy.CombatantID
            };
        }

        [TestMethod]
        public void ContinueBattle_ShouldContinue()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);
            combatants.Add(combatant2);
            combatants.Add(combatant3);

            Battle battleClass = new Battle();

            // act
            bool continueBattle = battleClass.ContinueBattle(combatants);

            // assert
            Assert.IsTrue(continueBattle, "ContinueBattle did not continue battle when it was supposed to");
        }

        [TestMethod]
        public void ContinueBattle_ShouldNotContinue()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant2);
            combatants.Add(combatant3);

            Battle battleClass = new Battle();

            // act
            bool continueBattle = battleClass.ContinueBattle(combatants);

            // assert
            Assert.IsFalse(continueBattle, "ContinueBattle continued even when it should not have");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ContinueBattle_InvalidValues_EmptyCombatant()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);
            combatants.Add(new Combatant());

            Battle battleClass = new Battle();

            // act
            bool continueBattle = battleClass.ContinueBattle(combatants);
        }

        [TestMethod]
        public void ContinueBattle_InvalidValues_EmptyCombatantList()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();

            Battle battleClass = new Battle();

            // act
            bool continueBattle = battleClass.ContinueBattle(combatants);

            // assert
            Assert.IsFalse(continueBattle, "ContinueBattle continued even though there were not combatants in the list");
        }

        [TestMethod]
        public void ValidateCombatants_ValidValues()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);
            combatants.Add(combatant2);
            combatants.Add(combatant3);

            Battle battleClass = new Battle();

            // act
            bool validateCombatants = battleClass.ValidateCombatants(combatants);

            // assert
            Assert.IsTrue(validateCombatants, "ValidateCombatants did not validate the valid combatants correctly");
        }

        [TestMethod]
        public void ValidateCombatants_InvalidValues_DeadCharacter()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);
            combatants.Add(combatant2);
            combatants.Add(combatant3);
            combatants.Add(deadCombatant1);

            Battle battleClass = new Battle();

            // act
            bool validateCombatants = battleClass.ValidateCombatants(combatants);

            // assert
            Assert.IsFalse(validateCombatants, "ValidateCombatants did not validate the invalid dead combatant correctly");
        }

        [TestMethod]
        public void ValidateCombatants_InvalidValues_TiredCharacter()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);
            combatants.Add(combatant2);
            combatants.Add(combatant3);
            combatants.Add(tiredCombatant1);

            Battle battleClass = new Battle();

            // act
            bool validateCombatants = battleClass.ValidateCombatants(combatants);

            // assert
            Assert.IsFalse(validateCombatants, "ValidateCombatants did not validate the invalid tired combatant correctly");
        }

        [TestMethod]
        public void ValidateCombatants_InvalidValues_EmptyCombatant()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);
            combatants.Add(combatant2);
            combatants.Add(combatant3);
            combatants.Add(new Combatant());

            Battle battleClass = new Battle();

            // act
            bool validateCombatants = battleClass.ValidateCombatants(combatants);

            // assert
            Assert.IsFalse(validateCombatants, "ValidateCombatants did not validate the invalid empty combatant correctly");
        }

        [TestMethod]
        public void ValidateCombatants_InvalidValues_EmptyCombatantList()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();

            Battle battleClass = new Battle();

            // act
            bool validateCombatants = battleClass.ValidateCombatants(combatants);

            // assert
            Assert.IsFalse(validateCombatants, "ValidateCombatants did not validate the invalid empty combatantlist correctly");
        }

        [TestMethod]
        public void AssignCombatantIDs_CheckValidInput()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            Combatant combatant1Copy = CopyCombatant(combatant1);
            Combatant combatant2Copy = CopyCombatant(combatant2);
            combatant1Copy.CombatantID = 0;
            combatant2Copy.CombatantID = 0;
            combatants.Add(combatant1Copy);
            combatants.Add(combatant2Copy);

            Battle battleClass = new Battle();

            // act
            battleClass.AssignCombatantIDs(ref combatants);

            // assert
            List<Combatant> expected = new List<Combatant>();
            expected.Add(combatant1);
            expected.Add(combatant2);
            if (expected[0].CombatantID == combatants[0].CombatantID)
            {
                Assert.AreEqual(expected[1].CombatantID, combatants[1].CombatantID, "AssignCombatantIDs did not assign combatant IDs as expected");
                return;
            }
            Assert.Fail("AssignCombatantIDs did not assign combatant IDs as expected");
        }

        [TestMethod]
        public void CreateTurnOrder_CheckSortedTurnOrder()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);
            combatants.Add(combatant2);
            combatants.Add(combatant3);

            Battle battleClass = new Battle();

            // act
            var turnOrder = battleClass.CreateTurnOrder(combatants);
            int[] turnOrderInt = new int[3];
            int i = 0;

            // assert
            foreach (KeyValuePair<int, Combatant> pair in turnOrder)
            {
                turnOrderInt[i] = pair.Key;
                i++;
            }

            if (turnOrderInt[0] > turnOrderInt[1] && turnOrderInt[1] > turnOrderInt[2])
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail("CreateTurnOrder does not order the list correctly. Order: {0}, {1}, {2}", turnOrderInt[0], turnOrderInt[1], turnOrderInt[2]);
        }

        [TestMethod]
        public void CreateTurnOrder_CheckForMultipleAttacks()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Speed = 60;
            combatants.Add(combatant1Copy);
            combatants.Add(combatant2);
            combatants.Add(combatant3);

            Battle battleClass = new Battle();

            // act
            var turnOrder = battleClass.CreateTurnOrder(combatants);

            int turnOrderCount = 0;
            foreach (KeyValuePair<int, Combatant> pair in turnOrder)
            {
                turnOrderCount++;
            }

            // assert
            Assert.AreEqual(4, turnOrderCount, "CreateTurnOrder does not let combatant do multiple attacks");
        }

        [TestMethod]
        public void Turn_CheckCombatLogEntries()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            Combatant combatant1Copy = CopyCombatant(combatant1);
            Combatant combatant2Copy = CopyCombatant(combatant2);
            combatants.Add(combatant1Copy);
            combatants.Add(combatant2Copy);

            Battle battleClass = new Battle();

            // act
            battleClass.Turn(ref combatants);

            // assert

            // print the combatlog to test output
            for (int i = 0; i < battleClass.GetCombatLog().Count; i++)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("LogEntry: {0} DefenderDamage: {1} AttackerDamage: {2}", battleClass.GetCombatLog()[i].Text, battleClass.GetCombatLog()[i].DefenderDamage, battleClass.GetCombatLog()[i].AttackerDamage));
            }

            // If a combatant died before he had a chance to do something, the combatlog will have one less entry
            // Due to the random nature of the turnorder, one can not be sure which combatant attacks first.
            if (combatants[0].Health <= 0 || combatants[1].Health <= 0)
            {
                if (battleClass.GetCombatLog().Count == 1 || battleClass.GetCombatLog().Count == 2)
                {
                    Assert.IsTrue(true);
                    return;
                }
            }
            else
            {
                Assert.AreEqual(2, battleClass.GetCombatLog().Count, "The turn method does not fill the combatlog with information correctly");
                return;
            }
            Assert.Fail("The turn method does not fill the combatlog with information correctly");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CombatCleanUp_CheckWithMoreThanOneTeamLeft()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);
            combatants.Add(combatant2);

            Battle battleClass = new Battle();

            // act
            battleClass.CombatCleanUp(combatants);
        }

        [TestMethod]
        public void GetGoldRewards_CheckAmountInSpan()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            combatants.Add(combatant1);

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int goldReward;
            for(int i = 0; i < 10000; i++) {
                goldReward = battleClass.GetGoldRewards(combatants);

                // assert
                if(goldReward < 4 || goldReward > 13) {
                    Assert.Fail("GetGoldRewards awards the wrong amount of gold");
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AttackCleanUp_NoRemoval()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            Combatant combatant1Copy = CopyCombatant(combatant1);
            Combatant combatant2Copy = CopyCombatant(combatant2);
            combatants.Add(combatant1Copy);
            combatants.Add(combatant2Copy);

            Battle battleClass = new Battle();

            // act
            battleClass.AttackCleanUp(ref combatant1Copy, ref combatant2Copy, ref combatants);

            // assert
            if(combatant1Copy.Stanima == combatant1Copy.MaxStanima - 1 &&
                combatant2Copy.Stanima == combatant2Copy.MaxStanima - 1 &&
                combatants.Exists(x => x.CombatantID == combatant1Copy.CombatantID) &&
                combatants.Exists(x => x.CombatantID == combatant2Copy.CombatantID))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("AttackCleanUp removes combatants it's not supposed to remove");
            }
        }

        [TestMethod]
        public void AttackCleanUp_RemoveLowStanima()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Stanima = 1;
            Combatant combatant2Copy = CopyCombatant(combatant2);
            combatant2Copy.Stanima = 1;
            combatants.Add(combatant1Copy);
            combatants.Add(combatant2Copy);

            Battle battleClass = new Battle();

            // act
            battleClass.AttackCleanUp(ref combatant1Copy, ref combatant2Copy, ref combatants);

            // assert
            if(combatants.Exists(x => x.CombatantID == combatant1Copy.CombatantID))
            {
                Assert.Fail("AttackCleanUp failed to remove the attacker with low stanima");
                return;
            }
            if (combatants.Exists(x => x.CombatantID == combatant2Copy.CombatantID))
            {
                Assert.Fail("AttackCleanUp failed to remvoe the defender with low stanima");
                return;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AttackCleanUp_RemoveLowHealth()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Health = 0;
            Combatant combatant2Copy = CopyCombatant(combatant2);
            combatant2Copy.Health = 0;
            combatants.Add(combatant1Copy);
            combatants.Add(combatant2Copy);

            Battle battleClass = new Battle();

            // act
            battleClass.AttackCleanUp(ref combatant1Copy, ref combatant2Copy, ref combatants);

            // assert
            if (combatants.Exists(x => x.CombatantID == combatant1Copy.CombatantID))
            {
                Assert.Fail("AttackCleanUp failed to remove the attacker with low stanima");
                return;
            }
            if (combatants.Exists(x => x.CombatantID == combatant2Copy.CombatantID))
            {
                Assert.Fail("AttackCleanUp failed to remvoe the defender with low stanima");
                return;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AttackChance_CheckNumberSameValue()
        {
            // arrange
            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.AttackChance(combatant1);

                // assert
                if (randomNumber < combatant1.Speed / 2 || randomNumber > combatant1.Agility + combatant1.Speed)
                {
                    Assert.Fail("AttackChance has a chance of giving the wrong number if the combatants speed and agility values match");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AttackChance_CheckNumberHighAgility()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Agility = 100;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.AttackChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Speed / 2 || randomNumber > ((int)combatant1Copy.Speed * 1.5) + combatant1Copy.Speed)
                {
                    Assert.Fail("AttackChance has a chance of giving the wrong number if the combatants agility is 1.5 times the his/her speed");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AttackChance_CheckNumberSlightlyHigherSpeed()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Speed += 1;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.AttackChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Agility / 2 || randomNumber > combatant1Copy.Agility + combatant1Copy.Speed)
                {
                    Assert.Fail("AttackChance has a chance of giving the wrong number if the combatants speed value is slightly higher than the agility value");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AttackChance_CheckNumberHighSpeed()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Speed = 100;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.AttackChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Agility / 2 || randomNumber > ((int)combatant1Copy.Agility * 1.5) + combatant1Copy.Agility)
                {
                    Assert.Fail("AttackChance has a chance of giving the wrong number if the combatants speed is 1.5 times the his/her agility");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void EvadeChance_CheckNumberSameValue()
        {
            // arrange
            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.EvadeChance(combatant1);

                // assert
                if (randomNumber < combatant1.Dexterity / 2 || randomNumber > combatant1.Speed + combatant1.Dexterity)
                {
                    Assert.Fail("EvadeChance has a chance of giving the wrong number if the combatants speed and dexterity values match");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void EvadeChance_CheckNumberHighSpeed()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Speed = 100;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.EvadeChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Dexterity / 2 || randomNumber > ((int)combatant1Copy.Dexterity * 1.5) + combatant1Copy.Dexterity)
                {
                    Assert.Fail("EvadeChance has a chance of giving the wrong number if the combatants speed is 1.5 times the his/her dexterity");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void EvadeChance_CheckNumberSlightlyHigherDexterity()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Dexterity += 1;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.EvadeChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Speed / 2 || randomNumber > combatant1Copy.Speed + combatant1Copy.Dexterity)
                {
                    Assert.Fail("EvadeChance has a chance of giving the wrong number if the combatants dexterity value is slightly higher than the speed value");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void EvadeChance_CheckNumberHighDexterity()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Dexterity = 100;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.EvadeChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Speed / 2 || randomNumber > ((int)combatant1Copy.Speed * 1.5) + combatant1Copy.Speed)
                {
                    Assert.Fail("EvadeChance has a chance of giving the wrong number if the combatants dexterity is 1.5 times the his/her speed");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BlockAndCounterAttackChance_CheckNumberSameValue()
        {
            // arrange
            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.BlockAndCounterAttackChance(combatant1);

                // assert
                if (randomNumber < combatant1.Dexterity / 2 || randomNumber > combatant1.Agility + combatant1.Dexterity)
                {
                    Assert.Fail("BlockAndCounterAttackChance has a chance of giving the wrong number if the combatants agility and dexterity values match");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BlockAndCounterAttackChance_CheckNumberHighAgility()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Agility = 100;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.BlockAndCounterAttackChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Dexterity / 2 || randomNumber > ((int)combatant1Copy.Dexterity * 1.5) + combatant1Copy.Dexterity)
                {
                    Assert.Fail("BlockAndCounterAttackChance has a chance of giving the wrong number if the combatants agility is 1.5 times the his/her dexterity");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BlockAndCounterAttackChance_CheckNumberSlightlyHigherDexterity()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Dexterity += 1;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.BlockAndCounterAttackChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Agility / 2 || randomNumber > combatant1Copy.Agility + combatant1Copy.Dexterity)
                {
                    Assert.Fail("BlockAndCounterAttackChance has a chance of giving the wrong number if the combatants dexterity value is slightly higher than the agility value");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BlockAndCounterAttackChance_CheckNumberHighDexterity()
        {
            // arrange
            Combatant combatant1Copy = CopyCombatant(combatant1);
            combatant1Copy.Dexterity = 100;

            Battle battleClass = new Battle();

            // act
            // loop through the method 10000 times to get a good span
            int randomNumber;
            for (int i = 0; i < 10000; i++)
            {
                randomNumber = battleClass.BlockAndCounterAttackChance(combatant1Copy);

                // assert
                if (randomNumber < combatant1Copy.Agility / 2 || randomNumber > ((int)combatant1Copy.Agility * 1.5) + combatant1Copy.Agility)
                {
                    Assert.Fail("BlockAndCounterAttackChance has a chance of giving the wrong number if the combatants dexterity is 1.5 times the his/her agility");
                    return;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitBattle_InvalidCombatants()
        {
            // arrange
            List<Combatant> combatants = new List<Combatant>();
            Combatant combatant1Copy = CopyCombatant(combatant1);
            Combatant deadCombatant1Copy = CopyCombatant(deadCombatant1);
            combatants.Add(combatant1Copy);
            combatants.Add(deadCombatant1Copy);

            Battle battleClass = new Battle();

            // act
            List<CombatLog> combatLog = battleClass.InitBattle(combatants);
        }

        [TestMethod]
        public void InitBattle_TestBattleOutputCombatLog()
        {
            // NOTICE: This test only tests the output of a battle
            // For each individual part of the battle, check the other tests!

            // arrange
            List<Combatant> combatants = new List<Combatant>();
            Combatant combatant1Copy = CopyCombatant(combatant1);
            Combatant combatant2Copy = CopyCombatant(combatant2);

            // to prevent the battlesystem to search the database for the combatants
            combatant1Copy.UserID = null;
            combatant2Copy.UserID = null;
            combatant1Copy.CombatantID = 0;
            combatant2Copy.CombatantID = 0;

            combatants.Add(combatant1Copy);
            combatants.Add(combatant2Copy);

            Battle battleClass = new Battle();

            // act
            try
            {
                List<CombatLog> combatLog = battleClass.InitBattle(combatants);

                // print the combatlog to test output
                for (int i = 0; i < combatLog.Count; i++)
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("LogEntry: {0} DefenderDamage: {1} AttackerDamage: {2}", combatLog[i].Text, combatLog[i].DefenderDamage, combatLog[i].AttackerDamage));
                }
            }
            catch (Exception ex)
            {
                // assert
                Assert.Fail("Something prohibited the battle from finishing. ErrorMsg: {0}", ex.Message);
                return;
            }
            Assert.IsTrue(true);
        }
    }
}