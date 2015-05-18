using BellatorTabernae.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BellatorTabernae.Model
{
    public class Service
    {
        private CharacterDAL _characterDAL;
        private ChatDAL _chatDAL;
        private EquipmentDAL _equipmentDAL;
        private InventoryDAL _inventoryDAL;
        private LeaderboardDAL _leaderboardDAL;
        private UserDAL _userDAL;
        private Battle _battle;

        public CharacterDAL CharacterDAL
        {
            get
            {
                return _characterDAL ?? (_characterDAL = new CharacterDAL());
            }
        }

        public ChatDAL ChatDAL
        {
            get
            {
                return _chatDAL ?? (_chatDAL = new ChatDAL());
            }
        }

        public EquipmentDAL EquipmentDAL
        {
            get
            {
                return _equipmentDAL ?? (_equipmentDAL = new EquipmentDAL());
            }
        }

        public InventoryDAL InventoryDAL
        {
            get
            {
                return _inventoryDAL ?? (_inventoryDAL = new InventoryDAL());
            }
        }

        public LeaderboardDAL LeaderboardDAL
        {
            get
            {
                return _leaderboardDAL ?? (_leaderboardDAL = new LeaderboardDAL());
            }
        }

        public UserDAL UserDAL
        {
            get
            {
                return _userDAL ?? (_userDAL = new UserDAL());
            }
        }

        public Battle Battle
        {
            get
            {
                return _battle ?? (_battle = new Battle());
            }
        }

        /* Validation */

        public void Validate(Object validationObject)
        {
            ICollection<ValidationResult> validationResults;
            if (!validationObject.Validate(out validationResults))
            {
                ValidationResult[] validationException = new ValidationResult[1];
                validationResults.CopyTo(validationException, 0);
                var ex = new ValidationException(validationException[0].ErrorMessage);
                throw ex;
            }
        }

        /* CharacterDAL */

        public Character GetCharacter(int? charID = null, int? userID = null)
        {
            return CharacterDAL.GetCharacter(charID, userID);
        }

        public bool UserHasCharacter(int userID)
        {
            return CharacterDAL.UserHasCharacter(userID);
        }

        public bool IsCharacterUsers(int charID, int userID)
        {
            return CharacterDAL.IsCharacterUsers(charID, userID);
        }

        public Character GetCharacterH(int charID)
        {
            return CharacterDAL.GetCharacterH(charID);
        }

        public IEnumerable<Character> GetCharactersH(int userID)
        {
            return CharacterDAL.GetCharactersH(userID);
        }

        public void DeleteCharacter(Character character)
        {
            Validate(character);
            CharacterDAL.DeleteCharacter(character.CharID, character.UserID);
        }

        public void DeleteCharacter(int? charID = null, int? userID = null)
        {
            CharacterDAL.DeleteCharacter(charID, userID);
        }

        public void CreateCharacter(Character character, int raceID)
        {
            Validate(character);
            CharacterDAL.CreateCharacter(character, raceID);
        }

        public void UpdateCharacterStats(int? userID = null, int? charID = null, int? level = null, int? experience = null,
                                         int? health = null, int? maxHealth = null, int? stanima = null, int? maxStanima = null,
                                         int? strength = null, int? speed = null, int? dexterity = null, int? agility = null)
        {
            CharacterDAL.UpdateCharacterStats(userID, charID, level, experience, health, maxHealth, stanima, maxStanima, strength, speed, dexterity, agility);
        }

        public void UpdateCharacterStats(Character character, int? level = null, int? experience = null,
                                         int? health = null, int? maxHealth = null, int? stanima = null, int? maxStanima = null,
                                         int? strength = null, int? speed = null, int? dexterity = null, int? agility = null)
        {
            Validate(character);
            CharacterDAL.UpdateCharacterStats(character.UserID, character.CharID, level, experience, health, maxHealth, stanima, maxStanima, strength, speed, dexterity, agility);
        }

        public void UpdateCharacterStats(Character character)
        {
            Validate(character);
            CharacterDAL.UpdateCharacterStats(character.UserID, character.CharID, character.Level, character.Experience, character.Health, character.MaxHealth, character.Stanima,
                character.MaxStanima, character.Strength, character.Speed, character.Dexterity, character.Agility);
        }

        public void LevelUpCharacter(int? userID, int? charID, int maxHealth, int maxStanima, int strength,
                                    int speed, int dexterity, int agility)
        {
            CharacterDAL.UpdateCharacterStats(userID, charID, null, null, maxHealth, maxHealth, maxStanima, maxStanima, strength, speed, dexterity, agility);
        }

        public void UpdateCharacterAfterCombat(int charID, int level, int experience, int health, int stanima) {
            CharacterDAL.UpdateCharacterStats(null, charID, level, experience, health, null, stanima);
        }

        public void UpdateCharacterBiografy(Character character, string biografy = null)
        {
            Validate(character);
            CharacterDAL.UpdateCharacterBiografy(character.UserID, character.CharID, biografy);
        }

        public void UpdateCharacterBiografy(int? userID = null, int? charID = null, string biografy = null)
        {
            CharacterDAL.UpdateCharacterBiografy(userID, charID, biografy);
        }

        public IEnumerable<Race> GetRaces()
        {
            return CharacterDAL.GetRaces();
        }

        public Race GetRace(int raceID)
        {
            return CharacterDAL.GetRace(raceID);
        }

        public Character GetMonster(int charID)
        {
            return CharacterDAL.GetMonster(charID);
        }

        public IEnumerable<Character> GetMonsters()
        {
            return CharacterDAL.GetMonsters();
        }

        /* ChatDAL */

        public IEnumerable<Chat> GetChat()
        {
            return ChatDAL.GetChat();
        }

        public Chat GetChatMessage(int msgID)
        {
            return ChatDAL.GetChatMessage(msgID);
        }

        public void PostChatMessage(int userID, string msg)
        {
            ChatDAL.PostChatMessage(userID, msg);
        }

        /* EquipmentDAL */

        public Equipment GetEquipment(int equipID)
        {
            return EquipmentDAL.GetEquipment(equipID);
        }

        public IEnumerable<Equipment> GetEquipments()
        {
            return EquipmentDAL.GetEquipments();
        }

        public IEnumerable<Equipment> GetMarketInventory(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return EquipmentDAL.GetMarketInventory(maximumRows, startRowIndex, out totalRowCount);
        }

        public int GetCharacterGold(int? charID, int? userID)
        {
            return EquipmentDAL.GetCharacterGold(charID, userID);
        }

        public int GetCharacterGold(Character character)
        {
            Validate(character);
            return EquipmentDAL.GetCharacterGold(character.CharID, null);
        }

        public EquipmentStats GetEquipmentStats(int? equipStatsID, int? equipID, int? inventoryID)
        {
            return EquipmentDAL.GetEquipmentStats(equipStatsID, equipID, inventoryID);
        }

        public IEnumerable<EquipmentStats> GetEquipmentStats()
        {
            return EquipmentDAL.GetEquipmentStats();
        }

        /* InventoryDAL */

        public IEnumerable<Inventory> GetInventory(Character character)
        {
            Validate(character);
            return InventoryDAL.GetInventory(character.CharID, null);
        }

        public IEnumerable<Inventory> GetInventory(int? charID, int? userID)
        {
            return InventoryDAL.GetInventory(charID, userID);
        }

        public Inventory GetInventory(int inventoryID)
        {
            return InventoryDAL.GetInventory(inventoryID);
        }

        public IEnumerable<Inventory> GetInventoryH(Character character)
        {
            Validate(character);
            return InventoryDAL.GetInventoryH(character.CharID);
        }

        public IEnumerable<Inventory> GetInventoryH(int charID)
        {
            return InventoryDAL.GetInventoryH(charID);
        }

        public void AddGoldToInventory(int charID, int number)
        {
            InventoryDAL.AddGoldToInventory(charID, number);
        }

        public void RemoveGoldFromInventory(int? charID, int? userID, int? inventoryID, int? number)
        {
            InventoryDAL.RemoveGoldFromInventory(charID, userID, inventoryID, number);
        }

        public void AddEquipmentToInventory(Character character, Equipment equipment, int? number = null)
        {
            Validate(character);
            Validate(equipment);
            InventoryDAL.AddEquipmentToInventory(character.CharID, null, equipment.EquipID, number);
        }

        public void AddEquipmentToInventory(int charID, Equipment equipment, int? number = null)
        {
            Validate(equipment);
            InventoryDAL.AddEquipmentToInventory(charID, null, equipment.EquipID, number);
        }

        public void AddEquipmentToInventory(Character character, int equipID, int? number = null)
        {
            Validate(character);
            InventoryDAL.AddEquipmentToInventory(character.CharID, null, equipID, number);
        }

        public void AddEquipmentToInventory(int? charID, int? userID, int equipID, int? number = null)
        {
            InventoryDAL.AddEquipmentToInventory(charID, userID, equipID, number);
        }

        public void AddEquipmentToInventory(Inventory inventory, int? number = null)
        {
            Validate(inventory);
            InventoryDAL.AddEquipmentToInventory(inventory.InventoryID, number);
        }

        public void AddEquipmentToInventory(int inventoryID, int? number = null)
        {
            InventoryDAL.AddEquipmentToInventory(inventoryID, number);
        }

        public void DeleteEquipmentFromInventory(Character character, Equipment equipment, int? number = null)
        {
            Validate(character);
            Validate(equipment);
            InventoryDAL.DeleteEquipmentFromInventory(character.CharID, equipment.EquipID, number);
        }

        public void DeleteEquipmentFromInventory(int charID, Equipment equipment, int? number = null) 
        {
            Validate(equipment);
            InventoryDAL.DeleteEquipmentFromInventory(charID, equipment.EquipID, number);
        }

        public void DeleteEquipmentFromInventory(Character character, int equipID, int? number = null)
        {
            Validate(character);
            InventoryDAL.DeleteEquipmentFromInventory(character.CharID, equipID, number);
        }

        public void DeleteEquipmentFromInventory(int charID, int equipID, int? number = null)
        {
            InventoryDAL.DeleteEquipmentFromInventory(charID, equipID, number);
        }

        public void DeleteEquipmentFromInventory(Inventory inventory, int? number = null)
        {
            Validate(inventory);
            InventoryDAL.DeleteEquipmentFromInventory(inventory.InventoryID, number);
        }

        public void DeleteEquipmentFromInventory(int inventoryID, int? number = null)
        {
            InventoryDAL.DeleteEquipmentFromInventory(inventoryID, number);
        }

        public void EquipWeapon(int inventoryID, int? charID = null, int? userID = null)
        {
            InventoryDAL.EquipWeapon(inventoryID, charID, userID);
        }

        public void EquipWeapon(Inventory inventory, int? userID = null)
        {
            Validate(inventory);
            InventoryDAL.EquipWeapon(inventory.InventoryID, inventory.CharID, userID);
        }

        public void EquipWeapon(int? inventoryID, Character character)
        {
            Validate(character);
            InventoryDAL.EquipWeapon(inventoryID, character.CharID, character.UserID);
        }

        public void EquipShield(int inventoryID, int? charID = null, int? userID = null)
        {
            InventoryDAL.EquipShield(inventoryID, charID, userID);
        }

        public void EquipShield(Inventory inventory, int? userID = null)
        {
            Validate(inventory);
            InventoryDAL.EquipShield(inventory.InventoryID, inventory.CharID, userID);
        }

        public void EquipShield(int? inventoryID, Character character)
        {
            Validate(character);
            InventoryDAL.EquipShield(inventoryID, character.CharID, character.UserID);
        }

        public void EquipArmor(int inventoryID, int? charID = null, int? userID = null)
        {
            InventoryDAL.EquipArmor(inventoryID, charID, userID);
        }

        public void EquipArmor(Inventory inventory, int? userID = null)
        {
            Validate(inventory);
            InventoryDAL.EquipArmor(inventory.InventoryID, inventory.CharID, userID);
        }

        public void EquipArmor(int? inventoryID, Character character)
        {
            Validate(character);
            InventoryDAL.EquipArmor(inventoryID, character.CharID, character.UserID);
        }

        /* LeaderboardDAL */

        public IEnumerable<Leaderboard> GetLeaderboard(int maximumRows, int startRowIndex, out int totalRowCount, int type)
        {
            return LeaderboardDAL.GetLeaderboard(maximumRows, startRowIndex, out totalRowCount, type);
        }

        /* UserDAL */

        public User GetUser(int userID)
        {
            return UserDAL.GetUser(userID);
        }

        public User GetUser(Character character)
        {
            Validate(character);
            return UserDAL.GetUser((int)character.UserID);
        }

        public void CreateUser(string username, string password, string email)
        {
            UserDAL.CreateUser(username, password, email);
        }

        public int CheckLogin(string username, string password)
        {
            return UserDAL.CheckLogin(username, password);
        }

        /* Battle */

        public List<CombatLog> InitiateMonsterBattle(int userID, int monsterID)
        {
            if (UserHasCharacter(userID))
            {
                List<Combatant> combatants = new List<Combatant>();
                combatants.Add(CreateCombatantFromCharacter(GetCharacter(null, userID), 1));
                combatants.Add(CreateCombatantFromCharacter(GetMonster(monsterID), 2));

                return Battle.InitBattle(combatants);
            }
            else
            {
                throw new ApplicationException("Användaren hade ingen karaktär!");
            }
        }

        public Combatant CreateCombatantFromCharacter(Character character, int teamNumber)
        {
            return new Combatant
            {
                CharID = character.CharID,
                UserID = character.UserID,
                Race = character.Race,
                Name = character.Name,
                Level = character.Level,
                Experience = character.Experience,
                Health = character.Health,
                MaxHealth = character.MaxHealth,
                Stanima = character.Stanima,
                MaxStanima = character.MaxStanima,
                Strength = character.Strength,
                Speed = character.Speed,
                Agility = character.Agility,
                Dexterity = character.Dexterity,
                WeaponID = character.WeaponID,
                ShieldID = character.ShieldID,
                ArmorID = character.ArmorID,
                GiveUpPercent = 0.2,
                TeamNumber = teamNumber
            };
        }
    }
}