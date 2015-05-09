using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BellatorTabernae.Model.DAL
{
    public class LeaderboardDAL : DALBase
    {
        public IEnumerable<Leaderboard> GetLeaderboard(int maximumRows, int startIndexRow, out int totalRowCount, int type)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var leaderboard = new List<Leaderboard>(30);

                    SqlCommand cmd = new SqlCommand("dbo.usp_GetLeaderboard", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@StartPosition", SqlDbType.Int, 4).Value = startIndexRow + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@TotalEntries", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    cmd.Parameters.AddWithValue("@Type", type);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var rowNumberIndex = reader.GetOrdinal("RowNumber");
                            var rankIndex = reader.GetOrdinal("Rank");
                            var charIDIndex = reader.GetOrdinal("CharID");
                            var raceIndex = reader.GetOrdinal("RaceName");
                            var nameIndex = reader.GetOrdinal("Name");
                            var levelIndex = reader.GetOrdinal("Level");
                            var maxHealthIndex = reader.GetOrdinal("MaxHealth");
                            var maxStanimaIndex = reader.GetOrdinal("MaxStanima");
                            var strengthIndex = reader.GetOrdinal("Strength");
                            var speedIndex = reader.GetOrdinal("Speed");
                            var dexterityIndex = reader.GetOrdinal("Dexterity");
                            var agilityIndex = reader.GetOrdinal("Agility");

                            while (reader.Read())
                            {
                                leaderboard.Add(new Leaderboard
                                {
                                    RowNumber = reader.GetInt32(rowNumberIndex),
                                    Rank = reader.GetInt32(rankIndex),
                                    CharID = reader.GetInt32(charIDIndex),
                                    Race = reader.GetString(raceIndex),
                                    Name = reader.GetString(nameIndex),
                                    Level = reader.GetByte(levelIndex),
                                    MaxHealth = reader.GetInt16(maxHealthIndex),
                                    MaxStanima = reader.GetInt16(maxStanimaIndex),
                                    Strength = reader.GetByte(strengthIndex),
                                    Speed = reader.GetByte(speedIndex),
                                    Dexterity = reader.GetByte(dexterityIndex),
                                    Agility = reader.GetByte(agilityIndex)
                                });
                            }
                            totalRowCount = (int)cmd.Parameters["@TotalEntries"].Value;

                            leaderboard.TrimExcess();

                            return leaderboard;
                        }
                        totalRowCount = 0;

                        return null;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när topplistan skulle hämtas från databasen.");
                }
            }
        }
    }
}