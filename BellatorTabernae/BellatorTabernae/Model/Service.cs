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

        public EquipmentStats GetEquipmentStats(int? equipStatsID, int? equipID)
        {
            return EquipmentDAL.GetEquipmentStats(equipStatsID, equipID);
        }

        public IEnumerable<EquipmentStats> GetEquipmentStats()
        {
            return EquipmentDAL.GetEquipmentStats();
        }

        /* InventoryDAL */

        public IEnumerable<Inventory> GetInventory(Character character)
        {
            Validate(character);
            return InventoryDAL.GetInventory(character.CharID);
        }

        public IEnumerable<Inventory> GetInventory(int charID)
        {
            return InventoryDAL.GetInventory(charID);
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

        public void AddEquipmentToInventory(Character character, Equipment equipment, int? number = null)
        {
            Validate(character);
            Validate(equipment);
            InventoryDAL.AddEquipmentToInventory(character.CharID, equipment.EquipID, number);
        }

        public void AddEquipmentToInventory(int charID, Equipment equipment, int? number = null)
        {
            Validate(equipment);
            InventoryDAL.AddEquipmentToInventory(charID, equipment.EquipID, number);
        }

        public void AddEquipmentToInventory(Character character, int equipID, int? number = null)
        {
            Validate(character);
            InventoryDAL.AddEquipmentToInventory(character.CharID, equipID, number);
        }

        public void AddEquipmentToInventory(int charID, int equipID, int? number = null)
        {
            InventoryDAL.AddEquipmentToInventory(charID, equipID, number);
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

        public void EquipWeapon(int inventoryID, Character character)
        {
            Validate(character);
            InventoryDAL.EquipWeapon(inventoryID, character.CharID, character.UserID);
        }

        public void EquipWeapon(Inventory inventory, Character character)
        {
            Validate(inventory);
            Validate(character);
            InventoryDAL.EquipWeapon(inventory.InventoryID, character.CharID, character.UserID);
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

        public void EquipShield(int inventoryID, Character character)
        {
            Validate(character);
            InventoryDAL.EquipShield(inventoryID, character.CharID, character.UserID);
        }

        public void EquipArmor(Inventory inventory, Character character)
        {
            Validate(inventory);
            Validate(character);
            InventoryDAL.EquipArmor(inventory.InventoryID, character.CharID, character.UserID);
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

        public void EquipArmor(int inventoryID, Character character)
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
    }
}