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
                    cmd.Parameters.Add("@Type", SqlDbType.Int, 4).Value = type;

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

                            while (reader.Read())
                            {
                                leaderboard.Add(new Leaderboard
                                {
                                    RowNumber = reader.GetInt64(rowNumberIndex),
                                    Rank = reader.GetInt64(rankIndex),
                                    CharID = reader.GetInt32(charIDIndex),
                                    Race = reader.GetString(raceIndex),
                                    Name = reader.GetString(nameIndex),
                                    Level = reader.GetByte(levelIndex)
                                });
                            }
                        }
                    }
                    totalRowCount = (int)cmd.Parameters["@TotalEntries"].Value;

                    leaderboard.TrimExcess();

                    return leaderboard;
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