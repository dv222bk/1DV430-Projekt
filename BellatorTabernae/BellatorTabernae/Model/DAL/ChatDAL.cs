using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BellatorTabernae.Model.DAL
{
    public class ChatDAL : DALBase
    {
        public IEnumerable<Chat> GetChat()
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var chat = new List<Chat>(30);

                    SqlCommand cmd = new SqlCommand("dbo.usp_GetChat", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var msgIDIndex = reader.GetOrdinal("MsgID");
                            var usernameIndex = reader.GetOrdinal("Username");
                            var msgIndex = reader.GetOrdinal("Msg");
                            var timeIndex = reader.GetOrdinal("Time");

                            while (reader.Read())
                            {
                                chat.Add(new Chat
                                {
                                    MsgID = reader.GetInt32(msgIDIndex),
                                    Username = reader.GetString(usernameIndex),
                                    Msg = reader.GetString(msgIndex),
                                    Time = reader.GetDateTime(timeIndex)
                                });
                            }
                            chat.TrimExcess();

                            return chat;
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
                    throw new ApplicationException("Ett fel inträffade när chatmeddelanden skulle hämtas från databasen.");
                }
            }
        }

        public Chat GetChatMessage(int msgID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetChat", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MsgID", msgID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var usernameIndex = reader.GetOrdinal("Username");
                            var msgIndex = reader.GetOrdinal("Msg");
                            var timeIndex = reader.GetOrdinal("Time");

                            return new Chat
                            {
                                MsgID = msgID,
                                Username = reader.GetString(usernameIndex),
                                Msg = reader.GetString(msgIndex),
                                Time = reader.GetDateTime(timeIndex)
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
                    throw new ApplicationException("Ett fel inträffade när ett chatmeddelanden skulle hämtas från databasen.");
                }
            }
        }

        public void PostChatMessage(int userID, string msg)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_PostChatMessage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
                    cmd.Parameters.Add("@Msg", SqlDbType.NVarChar, 250).Value = msg;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när ett chatmeddelanden skulle skickas.");
                }
            }
        }
    }
}