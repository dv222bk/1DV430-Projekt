﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BellatorTabernae.Model.DAL
{
    public class EquipmentDAL : DALBase
    {
        public Equipment GetEquipment(int equipID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetEquipment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EquipID", equipID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var equipStatsIDIndex = reader.GetOrdinal("EquipStatsID");
                            var equipTypeIndex = reader.GetOrdinal("EquipTypeName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var valueIndex = reader.GetOrdinal("Value");

                            return new Equipment
                            {
                                EquipID = equipID,
                                EquipStatsID = reader.GetInt16(equipStatsIDIndex),
                                EquipType = reader.GetString(equipTypeIndex),
                                Name = reader.GetString(nameIndex),
                                Value = reader.GetByte(valueIndex)
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
                    throw new ApplicationException("Ett fel inträffade när en utrustningen skulle hämtas från databasen.");
                }
            }
        }

        public IEnumerable<Equipment> GetEquipments()
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var equipment = new List<Equipment>(200);

                    SqlCommand cmd = new SqlCommand("dbo.usp_GetEquipment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var equipIDIndex = reader.GetOrdinal("EquipID");
                            var equipStatsIDIndex = reader.GetOrdinal("EquipStatsID");
                            var equipTypeIndex = reader.GetOrdinal("EquipTypeName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var valueIndex = reader.GetOrdinal("Value");

                            while (reader.Read())
                            {
                                equipment.Add(new Equipment
                                {
                                    EquipID = reader.GetInt32(equipIDIndex),
                                    EquipStatsID = reader.GetInt16(equipStatsIDIndex),
                                    EquipType = reader.GetString(equipTypeIndex),
                                    Name = reader.GetString(nameIndex),
                                    Value = reader.GetByte(valueIndex)
                                });
                            }
                            equipment.TrimExcess();

                            return equipment;
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
                    throw new ApplicationException("Ett fel inträffade när utrustningar skulle hämtas från databasen.");
                }
            }
        }

        public EquipmentStats GetEquipmentStats(int? equipStatsID, int? equipID, int? inventoryID)
        {
            if(equipStatsID != null || equipID != null || inventoryID != null) 
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_GetEquipmentStats", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (equipStatsID != null)
                        {
                            cmd.Parameters.AddWithValue("@EquipStatsID", equipStatsID);
                        }
                        if (equipID != null)
                        {
                            cmd.Parameters.AddWithValue("@EquipID", equipID);
                        }
                        if (inventoryID != null)
                        {
                            cmd.Parameters.AddWithValue("@InventoryID", inventoryID);
                        }
                       
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var equipStatsIDIndex = reader.GetOrdinal("EquipStatsID");
                                var healthIndex = reader.GetOrdinal("Health");
                                var stanimaIndex = reader.GetOrdinal("Stanima");
                                var strengthIndex = reader.GetOrdinal("Strength");
                                var speedIndex = reader.GetOrdinal("Speed");
                                var dexterityIndex = reader.GetOrdinal("Dexterity");
                                var agilityIndex = reader.GetOrdinal("Agility");
                                var damageIndex = reader.GetOrdinal("Damage");
                                var defenseIndex = reader.GetOrdinal("Defense");

                                return new EquipmentStats
                                {
                                    EquipStatsID = reader.GetInt16(equipStatsIDIndex),
                                    Health = reader.GetInt16(healthIndex),
                                    Stanima = reader.GetInt16(stanimaIndex),
                                    Strength = reader.GetInt16(strengthIndex),
                                    Speed = reader.GetInt16(speedIndex),
                                    Dexterity = reader.GetInt16(dexterityIndex),
                                    Agility = reader.GetInt16(agilityIndex),
                                    Damage = reader.GetInt16(damageIndex),
                                    Defense = reader.GetInt16(defenseIndex),
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
                        throw new ApplicationException("Ett fel inträffade när utrustnings egenskaper skulle hämtas från databasen.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argument fel inträffade när utrustnings egenskaper skulle hämtas från databasen.");
            }
        }

        public IEnumerable<EquipmentStats> GetEquipmentStats()
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var equipmentStats = new List<EquipmentStats>(200);

                    SqlCommand cmd = new SqlCommand("dbo.usp_GetEquipment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var equipStatsIDIndex = reader.GetOrdinal("EquipStatsID");
                            var healthIndex = reader.GetOrdinal("Health");
                            var stanimaIndex = reader.GetOrdinal("Stanima");
                            var strengthIndex = reader.GetOrdinal("Strength");
                            var speedIndex = reader.GetOrdinal("Speed");
                            var dexterityIndex = reader.GetOrdinal("Dexterity");
                            var agilityIndex = reader.GetOrdinal("Agility");
                            var damageIndex = reader.GetOrdinal("Damage");
                            var defenseIndex = reader.GetOrdinal("Defense");

                            while (reader.Read())
                            {
                                equipmentStats.Add(new EquipmentStats
                                {
                                    EquipStatsID = reader.GetInt16(equipStatsIDIndex),
                                    Health = reader.GetInt16(healthIndex),
                                    Stanima = reader.GetInt16(stanimaIndex),
                                    Strength = reader.GetInt16(strengthIndex),
                                    Speed = reader.GetInt16(speedIndex),
                                    Dexterity = reader.GetInt16(dexterityIndex),
                                    Agility = reader.GetInt16(agilityIndex),
                                    Damage = reader.GetInt16(damageIndex),
                                    Defense = reader.GetInt16(defenseIndex),
                                });
                            }
                            equipmentStats.TrimExcess();

                            return equipmentStats;
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
                    throw new ApplicationException("Ett fel inträffade när en utrustningens egenskaper skulle hämtas från databasen.");
                }
            }
        }
    }
}