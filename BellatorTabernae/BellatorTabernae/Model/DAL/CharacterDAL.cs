using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BellatorTabernae.Model.DAL
{
    public class CharacterDAL : DALBase
    {
        public Character GetCharacter(int? charID = null, int? userID = null)
        {
            if (charID != null || userID != null)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_GetCharacter", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (charID != null)
                        {
                            cmd.Parameters.AddWithValue("@CharID", charID);
                        }
                        if (userID != null)
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID);
                        }

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var charIDIndex = reader.GetOrdinal("CharID");
                                var userIDIndex = reader.GetOrdinal("UserID");
                                var raceIndex = reader.GetOrdinal("RaceName");
                                var nameIndex = reader.GetOrdinal("Name");
                                var levelIndex = reader.GetOrdinal("Level");
                                var experienceIndex = reader.GetOrdinal("Experience");
                                var healthIndex = reader.GetOrdinal("Health");
                                var maxHealthIndex = reader.GetOrdinal("MaxHealth");
                                var stanimaIndex = reader.GetOrdinal("Stanima");
                                var maxStanimaIndex = reader.GetOrdinal("MaxStanima");
                                var strengthIndex = reader.GetOrdinal("Strength");
                                var speedIndex = reader.GetOrdinal("Speed");
                                var dexterityIndex = reader.GetOrdinal("Dexterity");
                                var agilityIndex = reader.GetOrdinal("Agility");
                                var weaponIDIndex = reader.GetOrdinal("WeaponID");
                                var shieldIDIndex = reader.GetOrdinal("ShieldID");
                                var armorIDIndex = reader.GetOrdinal("ArmorID");
                                var biografyIndex = reader.GetOrdinal("Biografy");
                                var createdOnIndex = reader.GetOrdinal("CreatedOn");

                                int? weaponID, shieldID, armorID;
                                string biografy;

                                if (!reader.IsDBNull(weaponIDIndex))
                                {
                                    weaponID = reader.GetInt32(weaponIDIndex);
                                }
                                else
                                {
                                    weaponID = null;
                                }

                                if (!reader.IsDBNull(shieldIDIndex))
                                {
                                    shieldID = reader.GetInt32(shieldIDIndex);
                                }
                                else
                                {
                                    shieldID = null;
                                }

                                if (!reader.IsDBNull(armorIDIndex))
                                {
                                    armorID = reader.GetInt32(armorIDIndex);
                                }
                                else
                                {
                                    armorID = null;
                                }

                                if (!reader.IsDBNull(biografyIndex))
                                {
                                    biografy = reader.GetString(biografyIndex);
                                }
                                else
                                {
                                    biografy = null;
                                }

                                return new Character
                                {
                                    CharID = reader.GetInt32(charIDIndex),
                                    UserID = reader.GetInt32(userIDIndex),
                                    Race = reader.GetString(raceIndex),
                                    Name = reader.GetString(nameIndex),
                                    Level = reader.GetByte(levelIndex),
                                    Experience = reader.GetInt32(experienceIndex),
                                    Health = reader.GetInt16(healthIndex),
                                    MaxHealth = reader.GetInt16(maxHealthIndex),
                                    Stanima = reader.GetInt16(stanimaIndex),
                                    MaxStanima = reader.GetInt16(maxStanimaIndex),
                                    Strength = reader.GetByte(strengthIndex),
                                    Speed = reader.GetByte(speedIndex),
                                    Dexterity = reader.GetByte(dexterityIndex),
                                    Agility = reader.GetByte(agilityIndex),
                                    WeaponID = weaponID,
                                    ShieldID = shieldID,
                                    ArmorID = armorID,
                                    Biografy = biografy,
                                    CreatedOn = reader.GetDateTime(createdOnIndex)
                                };
                            }
                        }
                        return new Character();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    catch
                    {
                        throw new ApplicationException("Ett fel inträffade när en karaktär skulle hämtas från databasen.");
                    }
                }
            }
            throw new ArgumentException("Ett argument fel inträffade när en karaktär skull hämtas från databasen.");
        }

        public Character GetMonster(int charID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetMonster", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CharID", charID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var charIDIndex = reader.GetOrdinal("CharID");
                            var userIDIndex = reader.GetOrdinal("UserID");
                            var raceIndex = reader.GetOrdinal("RaceName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var levelIndex = reader.GetOrdinal("Level");
                            var experienceIndex = reader.GetOrdinal("Experience");
                            var healthIndex = reader.GetOrdinal("Health");
                            var maxHealthIndex = reader.GetOrdinal("MaxHealth");
                            var stanimaIndex = reader.GetOrdinal("Stanima");
                            var maxStanimaIndex = reader.GetOrdinal("MaxStanima");
                            var strengthIndex = reader.GetOrdinal("Strength");
                            var speedIndex = reader.GetOrdinal("Speed");
                            var dexterityIndex = reader.GetOrdinal("Dexterity");
                            var agilityIndex = reader.GetOrdinal("Agility");
                            var weaponIDIndex = reader.GetOrdinal("WeaponID");
                            var shieldIDIndex = reader.GetOrdinal("ShieldID");
                            var armorIDIndex = reader.GetOrdinal("ArmorID");
                            var biografyIndex = reader.GetOrdinal("Biografy");
                            var createdOnIndex = reader.GetOrdinal("CreatedOn");

                            int? weaponID, shieldID, armorID;
                            string biografy;

                            if (!reader.IsDBNull(weaponIDIndex))
                            {
                                weaponID = reader.GetInt32(weaponIDIndex);
                            }
                            else
                            {
                                weaponID = null;
                            }

                            if (!reader.IsDBNull(shieldIDIndex))
                            {
                                shieldID = reader.GetInt32(shieldIDIndex);
                            }
                            else
                            {
                                shieldID = null;
                            }

                            if (!reader.IsDBNull(armorIDIndex))
                            {
                                armorID = reader.GetInt32(armorIDIndex);
                            }
                            else
                            {
                                armorID = null;
                            }

                            if (!reader.IsDBNull(biografyIndex))
                            {
                                biografy = reader.GetString(biografyIndex);
                            }
                            else
                            {
                                biografy = null;
                            }

                            if (!reader.IsDBNull(userIDIndex))
                            {
                                throw new ApplicationException("Monstret som hämtades från databasen var egentligen inget monster!");
                            }

                            return new Character
                            {
                                CharID = reader.GetInt32(charIDIndex),
                                UserID = null,
                                Race = reader.GetString(raceIndex),
                                Name = reader.GetString(nameIndex),
                                Level = reader.GetByte(levelIndex),
                                Experience = reader.GetInt32(experienceIndex),
                                Health = reader.GetInt16(healthIndex),
                                MaxHealth = reader.GetInt16(maxHealthIndex),
                                Stanima = reader.GetInt16(stanimaIndex),
                                MaxStanima = reader.GetInt16(maxStanimaIndex),
                                Strength = reader.GetByte(strengthIndex),
                                Speed = reader.GetByte(speedIndex),
                                Dexterity = reader.GetByte(dexterityIndex),
                                Agility = reader.GetByte(agilityIndex),
                                WeaponID = weaponID,
                                ShieldID = shieldID,
                                ArmorID = armorID,
                                Biografy = biografy,
                                CreatedOn = reader.GetDateTime(createdOnIndex)
                            };
                        }
                    }
                    return new Character();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när ett monster skulle hämtas från databasen.");
                }
            }
        }

        public bool UserHasCharacter(int userID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_UserHasCharacter", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
                    cmd.Parameters.Add("@HasCharacter", SqlDbType.Bit, 1).Direction = ParameterDirection.Output;

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    return (bool) cmd.Parameters["@HasCharacter"].Value;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när en koll skulle göras om användaren hade en karaktär");
                }
            }
        }

        public bool IsCharacterUsers(int charID, int userID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_IsCharacterUsers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Value = charID;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
                    cmd.Parameters.Add("@IsUsersCharacter", SqlDbType.Bit, 1).Direction = ParameterDirection.Output;

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    return (bool)cmd.Parameters["@IsUsersCharacter"].Value;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när det skulle kontrolleras om användaren ägde en viss karaktär");
                }
            }
        }

        public Character GetCharacterH(int charID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetCharacterH", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CharID", charID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var charIDIndex = reader.GetOrdinal("CharID");
                            var userIDIndex = reader.GetOrdinal("UserID");
                            var raceIndex = reader.GetOrdinal("RaceName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var levelIndex = reader.GetOrdinal("Level");
                            var experienceIndex = reader.GetOrdinal("Experience");
                            var healthIndex = reader.GetOrdinal("Health");
                            var maxHealthIndex = reader.GetOrdinal("MaxHealth");
                            var stanimaIndex = reader.GetOrdinal("Stanima");
                            var maxStanimaIndex = reader.GetOrdinal("MaxStanima");
                            var strengthIndex = reader.GetOrdinal("Strength");
                            var speedIndex = reader.GetOrdinal("Speed");
                            var dexterityIndex = reader.GetOrdinal("Dexterity");
                            var agilityIndex = reader.GetOrdinal("Agility");
                            var weaponIDIndex = reader.GetOrdinal("WeaponID");
                            var shieldIDIndex = reader.GetOrdinal("ShieldID");
                            var armorIDIndex = reader.GetOrdinal("ArmorID");
                            var biografyIndex = reader.GetOrdinal("Biografy");
                            var createdOnIndex = reader.GetOrdinal("CreatedOn");

                            return new Character
                            {
                                CharID = reader.GetInt32(charIDIndex),
                                UserID = reader.GetInt32(userIDIndex),
                                Race = reader.GetString(raceIndex),
                                Name = reader.GetString(nameIndex),
                                Level = reader.GetByte(levelIndex),
                                Experience = reader.GetInt32(experienceIndex),
                                Health = reader.GetInt16(healthIndex),
                                MaxHealth = reader.GetInt16(maxHealthIndex),
                                Stanima = reader.GetInt16(stanimaIndex),
                                MaxStanima = reader.GetInt16(maxStanimaIndex),
                                Strength = reader.GetByte(strengthIndex),
                                Speed = reader.GetByte(speedIndex),
                                Dexterity = reader.GetByte(dexterityIndex),
                                Agility = reader.GetByte(agilityIndex),
                                WeaponID = reader.GetInt32(weaponIDIndex),
                                ShieldID = reader.GetInt32(shieldIDIndex),
                                ArmorID = reader.GetInt32(armorIDIndex),
                                Biografy = reader.GetString(biografyIndex),
                                CreatedOn = reader.GetDateTime(createdOnIndex)
                            };
                        }
                    }
                    return new Character();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när en gammal karaktär skulle hämtas från databasen.");
                }
            }
        }

        public IEnumerable<Character> GetCharactersH(int userID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var characters = new List<Character>(100);

                    SqlCommand cmd = new SqlCommand("dbo.usp_GetCharacterH", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var charIDIndex = reader.GetOrdinal("CharID");
                            var userIDIndex = reader.GetOrdinal("UserID");
                            var raceIndex = reader.GetOrdinal("RaceName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var levelIndex = reader.GetOrdinal("Level");
                            var experienceIndex = reader.GetOrdinal("Experience");
                            var healthIndex = reader.GetOrdinal("Health");
                            var maxHealthIndex = reader.GetOrdinal("MaxHealth");
                            var stanimaIndex = reader.GetOrdinal("Stanima");
                            var maxStanimaIndex = reader.GetOrdinal("MaxStanima");
                            var strengthIndex = reader.GetOrdinal("Strength");
                            var speedIndex = reader.GetOrdinal("Speed");
                            var dexterityIndex = reader.GetOrdinal("Dexterity");
                            var agilityIndex = reader.GetOrdinal("Agility");
                            var weaponIDIndex = reader.GetOrdinal("WeaponID");
                            var shieldIDIndex = reader.GetOrdinal("ShieldID");
                            var armorIDIndex = reader.GetOrdinal("ArmorID");
                            var biografyIndex = reader.GetOrdinal("Biografy");
                            var createdOnIndex = reader.GetOrdinal("CreatedOn");

                            while (reader.Read())
                            {
                                int? weaponID, shieldID, armorID;
                                string biografy;

                                if (!reader.IsDBNull(weaponIDIndex))
                                {
                                    weaponID = reader.GetInt32(weaponIDIndex);
                                }
                                else
                                {
                                    weaponID = null;
                                }

                                if (!reader.IsDBNull(shieldIDIndex))
                                {
                                    shieldID = reader.GetInt32(shieldIDIndex);
                                }
                                else
                                {
                                    shieldID = null;
                                }

                                if (!reader.IsDBNull(armorIDIndex))
                                {
                                    armorID = reader.GetInt32(armorIDIndex);
                                }
                                else
                                {
                                    armorID = null;
                                }

                                if (!reader.IsDBNull(biografyIndex))
                                {
                                    biografy = reader.GetString(biografyIndex);
                                }
                                else
                                {
                                    biografy = null;
                                }

                                characters.Add(new Character
                                {
                                    CharID = reader.GetInt32(charIDIndex),
                                    UserID = reader.GetInt32(userIDIndex),
                                    Race = reader.GetString(raceIndex),
                                    Name = reader.GetString(nameIndex),
                                    Level = reader.GetByte(levelIndex),
                                    Experience = reader.GetInt32(experienceIndex),
                                    Health = reader.GetInt16(healthIndex),
                                    MaxHealth = reader.GetInt16(maxHealthIndex),
                                    Stanima = reader.GetInt16(stanimaIndex),
                                    MaxStanima = reader.GetInt16(maxStanimaIndex),
                                    Strength = reader.GetByte(strengthIndex),
                                    Speed = reader.GetByte(speedIndex),
                                    Dexterity = reader.GetByte(dexterityIndex),
                                    Agility = reader.GetByte(agilityIndex),
                                    WeaponID = weaponID,
                                    ShieldID = shieldID,
                                    ArmorID = armorID,
                                    Biografy = biografy,
                                    CreatedOn = reader.GetDateTime(createdOnIndex)
                                });
                            }
                            characters.TrimExcess();

                            return characters;
                        }
                        return new List<Character>();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när gammla karaktärer skulle hämtas från databasen.");
                }
            }
        }

        public IEnumerable<Character> GetMonsters()
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var characters = new List<Character>(100);

                    SqlCommand cmd = new SqlCommand("dbo.usp_GetMonster", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var charIDIndex = reader.GetOrdinal("CharID");
                            var userIDIndex = reader.GetOrdinal("UserID");
                            var raceIndex = reader.GetOrdinal("RaceName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var levelIndex = reader.GetOrdinal("Level");
                            var experienceIndex = reader.GetOrdinal("Experience");
                            var healthIndex = reader.GetOrdinal("Health");
                            var maxHealthIndex = reader.GetOrdinal("MaxHealth");
                            var stanimaIndex = reader.GetOrdinal("Stanima");
                            var maxStanimaIndex = reader.GetOrdinal("MaxStanima");
                            var strengthIndex = reader.GetOrdinal("Strength");
                            var speedIndex = reader.GetOrdinal("Speed");
                            var dexterityIndex = reader.GetOrdinal("Dexterity");
                            var agilityIndex = reader.GetOrdinal("Agility");
                            var weaponIDIndex = reader.GetOrdinal("WeaponID");
                            var shieldIDIndex = reader.GetOrdinal("ShieldID");
                            var armorIDIndex = reader.GetOrdinal("ArmorID");
                            var biografyIndex = reader.GetOrdinal("Biografy");
                            var createdOnIndex = reader.GetOrdinal("CreatedOn");

                            while (reader.Read())
                            {
                                int? weaponID, shieldID, armorID;
                                string biografy;

                                if (!reader.IsDBNull(weaponIDIndex))
                                {
                                    weaponID = reader.GetInt32(weaponIDIndex);
                                }
                                else
                                {
                                    weaponID = null;
                                }

                                if (!reader.IsDBNull(shieldIDIndex))
                                {
                                    shieldID = reader.GetInt32(shieldIDIndex);
                                }
                                else
                                {
                                    shieldID = null;
                                }

                                if (!reader.IsDBNull(armorIDIndex))
                                {
                                    armorID = reader.GetInt32(armorIDIndex);
                                }
                                else
                                {
                                    armorID = null;
                                }

                                if (!reader.IsDBNull(biografyIndex))
                                {
                                    biografy = reader.GetString(biografyIndex);
                                }
                                else
                                {
                                    biografy = null;
                                }

                                if (!reader.IsDBNull(userIDIndex))
                                {
                                    throw new ApplicationException("Monstret som hämtades från databasen var egentligen inget monster!");
                                }

                                characters.Add(new Character
                                {
                                    CharID = reader.GetInt32(charIDIndex),
                                    UserID = null,
                                    Race = reader.GetString(raceIndex),
                                    Name = reader.GetString(nameIndex),
                                    Level = reader.GetByte(levelIndex),
                                    Experience = reader.GetInt32(experienceIndex),
                                    Health = reader.GetInt16(healthIndex),
                                    MaxHealth = reader.GetInt16(maxHealthIndex),
                                    Stanima = reader.GetInt16(stanimaIndex),
                                    MaxStanima = reader.GetInt16(maxStanimaIndex),
                                    Strength = reader.GetByte(strengthIndex),
                                    Speed = reader.GetByte(speedIndex),
                                    Dexterity = reader.GetByte(dexterityIndex),
                                    Agility = reader.GetByte(agilityIndex),
                                    WeaponID = weaponID,
                                    ShieldID = shieldID,
                                    ArmorID = armorID,
                                    Biografy = biografy,
                                    CreatedOn = reader.GetDateTime(createdOnIndex)
                                });
                            }
                            characters.TrimExcess();

                            return characters;
                        }
                        return new List<Character>();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när monster skulle hämtas från databasen.");
                }
            }
        }

        public void DeleteCharacter(int? charID = null, int? userID = null)
        {
            if (charID != null || userID != null)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_DeleteCharacter", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (charID != null)
                        {
                            cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Value = charID;
                        }
                        if (userID != null)
                        {
                            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
                        }

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    catch
                    {
                        throw new ApplicationException("Ett fel inträffade när en karaktär skulle raderas från databasen.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argument fel inträffade när en karaktär skulle raderas från databasen.");
            }
        }

        public void CreateCharacter(Character character, int raceID)
        {
            using (var conn = CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("dbo.usp_CreateCharacter", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = character.UserID;
                    cmd.Parameters.Add("@RaceID", SqlDbType.TinyInt, 1).Value = raceID;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = character.Name;
                    cmd.Parameters.Add("@MaxHealth", SqlDbType.SmallInt, 2).Value = character.MaxHealth;
                    cmd.Parameters.Add("@MaxStanima", SqlDbType.SmallInt, 2).Value = character.MaxStanima;
                    cmd.Parameters.Add("@Strength", SqlDbType.TinyInt, 1).Value = character.Strength;
                    cmd.Parameters.Add("@Speed", SqlDbType.TinyInt, 1).Value = character.Speed;
                    cmd.Parameters.Add("@Dexterity", SqlDbType.TinyInt, 1).Value = character.Dexterity;
                    cmd.Parameters.Add("@Agility", SqlDbType.TinyInt, 1).Value = character.Agility;
                    cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    character.CharID = (int)cmd.Parameters["@CharID"].Value;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när en karaktär skulle läggas till i databasen.");
                }
            }
        }

        public void UpdateCharacterStats(int? userID = null, int? charID = null, int? level = null, int? experience = null,
                                         int? health = null, int? maxHealth = null, int? stanima = null, int? maxStanima = null,
                                         int? strength = null, int? speed = null, int? dexterity = null, int? agility = null)
        {
            if(userID != null || charID != null)
            {
                using (var conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_UpdateCharacterStats", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (userID != null)
                        {
                            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
                        }
                        if (charID != null)
                        {
                            cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Value = charID;
                        }
                        cmd.Parameters.Add("@Level", SqlDbType.TinyInt, 1).Value = level;
                        cmd.Parameters.Add("@Experience", SqlDbType.Int, 4).Value = experience;
                        cmd.Parameters.Add("@Health", SqlDbType.SmallInt, 2).Value = health;
                        cmd.Parameters.Add("@MaxHealth", SqlDbType.SmallInt, 2).Value = maxHealth;
                        cmd.Parameters.Add("@Stanima", SqlDbType.SmallInt, 2).Value = stanima;
                        cmd.Parameters.Add("@MaxStanima", SqlDbType.SmallInt, 2).Value = maxStanima;
                        cmd.Parameters.Add("@Strength", SqlDbType.TinyInt, 1).Value = strength;
                        cmd.Parameters.Add("@Speed", SqlDbType.TinyInt, 1).Value = speed;
                        cmd.Parameters.Add("@Dexterity", SqlDbType.TinyInt, 1).Value = dexterity;
                        cmd.Parameters.Add("@Agility", SqlDbType.TinyInt, 1).Value = agility;

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    catch
                    {
                        throw new ApplicationException("Ett fel inträffade när en karaktärs stats skulle uppdateras.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argument fel inträffade när en karaktärs stats skulle uppdateras.");
            }
        }

        public void UpdateCharacterBiografy(int? userID = null, int? charID = null, string biografy = null)
        {
            if (userID != null || charID != null)
            {
                using (var conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_UpdateCharacterBiografy", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (userID != null)
                        {
                            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
                        }
                        if (charID != null)
                        {
                            cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Value = charID;
                        }
                        cmd.Parameters.Add("@Biografy", SqlDbType.NVarChar, 2000).Value = biografy;

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    catch
                    {
                        throw new ApplicationException("Ett fel inträffade när en karaktärs biografi skulle uppdateras.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argument fel inträffade när en karaktärs biografi skulle uppdateras.");
            }
        }

        public IEnumerable<Race> GetRaces()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var races = new List<Race>(20);

                    SqlCommand cmd = new SqlCommand("dbo.usp_GetRace", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var raceIDIndex = reader.GetOrdinal("RaceID");
                            var raceNameIndex = reader.GetOrdinal("RaceName");
                            var raceDescIndex = reader.GetOrdinal("RaceDesc");
                            var raceHealthIndex = reader.GetOrdinal("Health");
                            var raceStanimaIndex = reader.GetOrdinal("Stanima");
                            var raceStrengthIndex = reader.GetOrdinal("Strength");
                            var raceSpeedIndex = reader.GetOrdinal("Speed");
                            var raceAgilityIndex = reader.GetOrdinal("Agility");
                            var raceDexterityIndex = reader.GetOrdinal("Dexterity");

                            while (reader.Read())
                            {
                                races.Add(new Race
                                {
                                    RaceID = reader.GetByte(raceIDIndex),
                                    RaceName = reader.GetString(raceNameIndex),
                                    RaceDesc = reader.GetString(raceDescIndex),
                                    Health = reader.GetInt16(raceHealthIndex),
                                    Stanima = reader.GetInt16(raceStanimaIndex),
                                    Strength = reader.GetByte(raceStrengthIndex),
                                    Speed = reader.GetByte(raceSpeedIndex),
                                    Agility = reader.GetByte(raceAgilityIndex),
                                    Dexterity = reader.GetByte(raceDexterityIndex)
                                });
                            }
                            races.TrimExcess();

                            return races;
                        }
                    }
                    return new List<Race>();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när raser skulle hämtas från databasen.");
                }
            }
        }

        public Race GetRace(int raceID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetRace", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RaceID", raceID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var raceIDIndex = reader.GetOrdinal("RaceID");
                            var raceNameIndex = reader.GetOrdinal("RaceName");
                            var raceDescIndex = reader.GetOrdinal("RaceDesc");
                            var raceHealthIndex = reader.GetOrdinal("Health");
                            var raceStanimaIndex = reader.GetOrdinal("Stanima");
                            var raceStrengthIndex = reader.GetOrdinal("Strength");
                            var raceSpeedIndex = reader.GetOrdinal("Speed");
                            var raceAgilityIndex = reader.GetOrdinal("Agility");
                            var raceDexterityIndex = reader.GetOrdinal("Dexterity");

                            return new Race
                            {
                                RaceID = reader.GetByte(raceIDIndex),
                                RaceName = reader.GetString(raceNameIndex),
                                RaceDesc = reader.GetString(raceDescIndex),
                                Health = reader.GetInt16(raceHealthIndex),
                                Stanima = reader.GetInt16(raceStanimaIndex),
                                Strength = reader.GetByte(raceStrengthIndex),
                                Speed = reader.GetByte(raceSpeedIndex),
                                Agility = reader.GetByte(raceAgilityIndex),
                                Dexterity = reader.GetByte(raceDexterityIndex)
                            };
                        }
                    }
                    return new Race();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när en ras skulle hämtas från databasen.");
                }
            }
        }
    }
}