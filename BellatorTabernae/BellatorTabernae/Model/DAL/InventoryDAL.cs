﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BellatorTabernae.Model.DAL
{
    public class InventoryDAL : DALBase
    {
        public IEnumerable<Inventory> GetInventory(int? charID, int? userID)
        {
            if(charID != null || userID != null)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        var inventory = new List<Inventory>(100);

                        SqlCommand cmd = new SqlCommand("dbo.usp_GetInventory", conn);
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
                            if (reader.HasRows)
                            {
                                var inventoryIDIndex = reader.GetOrdinal("InventoryID");
                                var equipIDIndex = reader.GetOrdinal("EquipID");
                                var equipStatsIDIndex = reader.GetOrdinal("EquipStatsID");
                                var charIDIndex = reader.GetOrdinal("CharID");
                                var equipTypeIndex = reader.GetOrdinal("EquipTypeName");
                                var nameIndex = reader.GetOrdinal("Name");
                                var valueIndex = reader.GetOrdinal("Value");
                                var numberIndex = reader.GetOrdinal("Number");

                                while (reader.Read())
                                {
                                    inventory.Add(new Inventory
                                    {
                                        InventoryID = reader.GetInt32(inventoryIDIndex),
                                        EquipID = reader.GetInt16(equipIDIndex),
                                        EquipStatsID = reader.GetInt16(equipStatsIDIndex),
                                        CharID = reader.GetInt32(charIDIndex),
                                        EquipType = reader.GetString(equipTypeIndex),
                                        Name = reader.GetString(nameIndex),
                                        Value = reader.GetInt32(valueIndex),
                                        Number = reader.GetInt32(numberIndex)
                                    });
                                }
                                inventory.TrimExcess();

                                return inventory;
                            }
                        }
                        return new List<Inventory>();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    catch
                    {
                        throw new ApplicationException("Ett fel inträffade när ägodelsplatser skulle hämtas från databasen.");
                    }
                }
            }
            else 
            {
                throw new ArgumentException("Ett argumentfel inträffade när ägodelsplatser skulle hämtas från databasen.");
            }
        }

        public Inventory GetInventory(int inventoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetInventory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var inventoryIDIndex = reader.GetOrdinal("InventoryID");
                            var equipIDIndex = reader.GetOrdinal("EquipID");
                            var equipStatsIDIndex = reader.GetOrdinal("EquipStatsID");
                            var charIDIndex = reader.GetOrdinal("CharID");
                            var equipTypeIndex = reader.GetOrdinal("EquipTypeName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var valueIndex = reader.GetOrdinal("Value");
                            var numberIndex = reader.GetOrdinal("Number");

                            return new Inventory
                            {
                                InventoryID = reader.GetInt32(inventoryIDIndex),
                                EquipID = reader.GetInt16(equipIDIndex),
                                EquipStatsID = reader.GetInt16(equipStatsIDIndex),
                                CharID = reader.GetInt32(charIDIndex),
                                EquipType = reader.GetString(equipTypeIndex),
                                Name = reader.GetString(nameIndex),
                                Value = reader.GetInt32(valueIndex),
                                Number = reader.GetInt32(numberIndex)
                            };
                        }
                    }
                    return new Inventory();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när en ägodelsplats skulle hämtas från databasen.");
                }
            }
        }

        public IEnumerable<Inventory> GetInventoryH(int charID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var inventory = new List<Inventory>(100);

                    SqlCommand cmd = new SqlCommand("dbo.usp_GetInventoryH", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CharID", charID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var inventoryIDIndex = reader.GetOrdinal("InventoryID");
                            var equipIDIndex = reader.GetOrdinal("EquipID");
                            var equipStatsIDIndex = reader.GetOrdinal("EquipStatsID");
                            var charIDIndex = reader.GetOrdinal("CharID");
                            var equipTypeIndex = reader.GetOrdinal("EquipTypeName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var valueIndex = reader.GetOrdinal("Value");
                            var numberIndex = reader.GetOrdinal("Number");

                            while (reader.Read())
                            {
                                inventory.Add(new Inventory
                                {
                                    InventoryID = reader.GetInt32(inventoryIDIndex),
                                    EquipID = reader.GetInt16(equipIDIndex),
                                    EquipStatsID = reader.GetInt16(equipStatsIDIndex),
                                    CharID = reader.GetInt32(charIDIndex),
                                    EquipType = reader.GetString(equipTypeIndex),
                                    Name = reader.GetString(nameIndex),
                                    Value = reader.GetInt32(valueIndex),
                                    Number = reader.GetInt32(numberIndex)
                                });
                            }
                            inventory.TrimExcess();

                            return inventory;
                        }
                    }
                    return new List<Inventory>();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när en gammal ägodelsplats skulle hämtas från databasen.");
                }
            }
        }

        public void AddGoldToInventory(int charID, int number)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_AddGoldToInventory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Value = charID;
                    cmd.Parameters.Add("@Number", SqlDbType.Int, 4).Value = number;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när guld skulle läggas i en ägodelsplats.");
                }
            }
        }

        public void AddEquipmentToInventory(int? charID, int? userID, int equipID, int? number = null)
        {
            if (charID != null || userID != null)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_AddEquipmentToInventory", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (charID != null)
                        {
                            cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Value = charID;
                        }

                        if (userID != null)
                        {
                            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
                        }

                        cmd.Parameters.Add("@EquipID", SqlDbType.SmallInt, 2).Value = equipID;

                        if (number != null)
                        {
                            cmd.Parameters.Add("@Number", SqlDbType.Int, 4).Value = number;
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
                        throw new ApplicationException("Ett fel inträffade när en utrustning skulle läggas i en ägodelsplats.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argumentfel inträffade när en utrustning skulle läggas i en ägodelsplats.");
            }
        }

        public void AddEquipmentToInventory(int inventoryID, int? number = null)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_AddEquipmentToInventory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@InventoryID", SqlDbType.Int, 4).Value = inventoryID;
                    if (number != null)
                    {
                        cmd.Parameters.Add("@Number", SqlDbType.Int, 4).Value = number;
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
                    throw new ApplicationException("Ett fel inträffade när en utrustning skulle läggas i en ägodelsplats.");
                }
            }
        }

        public void RemoveGoldFromInventory(int? charID, int? userID, int? inventoryID, int? number)
        {
            if (charID != null || userID != null || inventoryID != null)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_RemoveGoldFromInventory", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (charID != null)
                        {
                            cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Value = charID;
                        }
                        if (userID != null)
                        {
                            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
                        }
                        if (inventoryID != null)
                        {
                            cmd.Parameters.Add("@InventoryID", SqlDbType.Int, 4).Value = inventoryID;
                        }
                        if (number != null)
                        {
                            cmd.Parameters.Add("@Number", SqlDbType.Int, 4).Value = number;
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
                        throw new ApplicationException("Ett fel inträffade när en utrustning skulle läggas i en ägodelsplats.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argumentfel inträffade när guld skulle tas bort från karaktärens ägodelar.");
            }
        }

        public void DeleteEquipmentFromInventory(int charID, int equipID, int? number = null)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_DeleteEquipmentFromInventory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CharID", SqlDbType.Int, 4).Value = charID;
                    cmd.Parameters.Add("@EquipID", SqlDbType.SmallInt, 2).Value = equipID;
                    if (number != null)
                    {
                        cmd.Parameters.Add("@Number", SqlDbType.Int, 4).Value = number;
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
                    throw new ApplicationException("Ett fel inträffade när en utrustning tas bort från en ägodelsplats.");
                }
            }
        }

        public void DeleteEquipmentFromInventory(int inventoryID, int? number = null)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_DeleteEquipmentFromInventory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@InventoryID", SqlDbType.Int, 4).Value = inventoryID;
                    if (number != null)
                    {
                        cmd.Parameters.Add("@Number", SqlDbType.Int, 4).Value = number;
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
                    throw new ApplicationException("Ett fel inträffade när en utrustning tas bort från en ägodelsplats.");
                }
            }
        }

        public void EquipWeapon(int? inventoryID, int? charID = null, int? userID = null)
        {
            if (charID != null || userID != null)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_EquipWeapon", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (inventoryID != null)
                        {
                            cmd.Parameters.Add("@WeaponID", SqlDbType.Int, 4).Value = inventoryID;
                        }
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
                        throw new ApplicationException("Ett fel inträffade när en karaktär skulle utrusta sig med vapen.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argument fel inträffade när en karaktär skulle utrusta sig med vapen.");
            }
        }

        public void EquipShield(int? inventoryID, int? charID = null, int? userID = null)
        {
            if (charID != null || userID != null)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_EquipShield", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (inventoryID != null)
                        {
                            cmd.Parameters.Add("@ShieldID", SqlDbType.Int, 4).Value = inventoryID;
                        }
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
                        throw new ApplicationException("Ett fel inträffade när en karaktär skulle utrusta sig med sköld.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argument fel inträffade när en karaktär skulle utrusta sig med sköld.");
            }
        }

        public void EquipArmor(int? inventoryID, int? charID = null, int? userID = null)
        {
            if (charID != null || userID != null)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.usp_EquipArmor", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (inventoryID != null)
                        {
                            cmd.Parameters.Add("@ArmorID", SqlDbType.Int, 4).Value = inventoryID;
                        }
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
                        throw new ApplicationException("Ett fel inträffade när en karaktär skulle utrusta sig med rustning.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ett argument fel inträffade när en karaktär skulle utrusta sig med rustning.");
            }
        }
    }
}