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
                        return null;
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
                    return null;
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

                            while (reader.Read())
                            {
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
                                    WeaponID = reader.GetInt32(weaponIDIndex),
                                    ShieldID = reader.GetInt32(shieldIDIndex),
                                    ArmorID = reader.GetInt32(armorIDIndex),
                                    Biografy = reader.GetString(biografyIndex),
                                    CreatedOn = reader.GetDateTime(createdOnIndex)
                                });
                            }
                            characters.TrimExcess();

                            return characters;
                        }
                    }
                    return null;
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
            throw new ArgumentException("Ett argument fel inträffade när en karaktär skulle raderas från databasen.");
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
    }
}