using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BellatorTabernae.Model.DAL
{
    public class UserDAL : DALBase
    {
        public User GetUser(int userID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var usernameIndex = reader.GetOrdinal("Username");

                            return new User
                            {
                                UserID = userID,
                                Username = reader.GetString(usernameIndex)
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
                    throw new ApplicationException("Ett fel inträffade när en en användare skulle hämtas från databasen.");
                }
            }
        }

        public void CreateUser(string username, string password, string email)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password + "J~O?L?L3@P034~E5", 22);

                    SqlCommand cmd = new SqlCommand("dbo.usp_CreateUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.NChar, 60).Value = hashedPassword;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 254).Value = email;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när en en användare skulle skapas.");
                }
            }
        }

        public bool CheckLogin(string username, string password)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetLogin", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var dbPasswordIndex = reader.GetOrdinal("Password");
                            string dbPassword = reader.GetString(dbPasswordIndex);

                            return BCrypt.Net.BCrypt.Verify(password + "J~O?L?L3@P034~E5", dbPassword);
                        }
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade när ett login skulle kontrolleras.");
                }
            }
        }
    }
}