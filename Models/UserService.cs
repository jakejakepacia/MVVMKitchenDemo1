using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace MVVMKitchenDemo1.Models
{
    public class UserService
    {
        SqlConnection ObjSqlConnection;
        SqlCommand ObjSqlCommand;

        public UserService()
        {
            ObjSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["KEConnection"].ConnectionString);
            ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandType = CommandType.StoredProcedure;
        }

        public List<User> GetAll()
        {
            List<User> ObjUserList = new List<User>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectAllUsers";

                ObjSqlConnection.Open();

                var objSqlDataReader = ObjSqlCommand.ExecuteReader();

                if (objSqlDataReader.HasRows)
                {
                    User ObjUser = null;
                    while (objSqlDataReader.Read())
                    {
                        ObjUser = new User();
                        ObjUser.UserId = objSqlDataReader.GetInt32(0);
                        ObjUser.UserType = objSqlDataReader.GetString(1);
                        ObjUser.FirstName = objSqlDataReader.GetString(2);
                        ObjUser.LastName = objSqlDataReader.GetString(3);
                        ObjUser.EmailAddress = objSqlDataReader.GetString(4);
                        ObjUser.Username = objSqlDataReader.GetString(5);
                        ObjUser.Password = objSqlDataReader.GetString(6);

                        ObjUserList.Add(ObjUser);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            return ObjUserList;
        }

        public List<User> GetUserByCredentials(string username, string password)
        {
            List<User> ObjUserList = new List<User>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_GetUserByCredentials";
                ObjSqlCommand.Parameters.AddWithValue("@Username", username);
                ObjSqlCommand.Parameters.AddWithValue("@Password", password);

                ObjSqlConnection.Open();

                var objSqlDataReader = ObjSqlCommand.ExecuteReader();

                if (objSqlDataReader.HasRows)
                {
                    User ObjUser = null;
                    while (objSqlDataReader.Read())
                    {
                        ObjUser = new User();
                        ObjUser.UserId = objSqlDataReader.GetInt32(0);
                        ObjUser.UserType = objSqlDataReader.GetString(1);
                        ObjUser.FirstName = objSqlDataReader.GetString(2);
                        ObjUser.LastName = objSqlDataReader.GetString(3);
                        ObjUser.EmailAddress = objSqlDataReader.GetString(4);
                        ObjUser.Username = objSqlDataReader.GetString(5);
                        ObjUser.Password = objSqlDataReader.GetString(6);

                        ObjUserList.Add(ObjUser);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            return ObjUserList;
        }

        public User GetUserByUsername(string username)
        {
            List<User> ObjUserList = new List<User>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_GetUserByUsername";
                ObjSqlCommand.Parameters.AddWithValue("@Username", username);

                ObjSqlConnection.Open();

                var objSqlDataReader = ObjSqlCommand.ExecuteReader();

                if (objSqlDataReader.HasRows)
                {
                    User ObjUser = null;
                    while (objSqlDataReader.Read())
                    {
                        ObjUser = new User();
                        ObjUser.UserId = objSqlDataReader.GetInt32(0);
                        ObjUser.UserType = objSqlDataReader.GetString(1);
                        ObjUser.FirstName = objSqlDataReader.GetString(2);
                        ObjUser.LastName = objSqlDataReader.GetString(3);
                        ObjUser.EmailAddress = objSqlDataReader.GetString(4);
                        ObjUser.Username = objSqlDataReader.GetString(5);
                        ObjUser.Password = objSqlDataReader.GetString(6);

                        ObjUserList.Add(ObjUser);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            return ObjUserList.ElementAt(0);
        }


        public bool Add(User user)
        {
            bool IsAdded = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_InsertUser";
                ObjSqlCommand.Parameters.AddWithValue("@UserType", user.UserType);
                ObjSqlCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                ObjSqlCommand.Parameters.AddWithValue("@LastName", user.LastName);
                ObjSqlCommand.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                ObjSqlCommand.Parameters.AddWithValue("@Username", user.Username);
                ObjSqlCommand.Parameters.AddWithValue("@Password", user.Password);

                ObjSqlConnection.Open();

                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                if(NoOfRowsAffected > 0)
                {
                    IsAdded = true;
                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return IsAdded;
        }

        public bool Update(User user)
        {
            bool IsUpdated = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_UpdateUser";
                ObjSqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                ObjSqlCommand.Parameters.AddWithValue("@UserType", user.UserType);
                ObjSqlCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                ObjSqlCommand.Parameters.AddWithValue("@LastName", user.LastName);
                ObjSqlCommand.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                ObjSqlCommand.Parameters.AddWithValue("@Username", user.Username);
                ObjSqlCommand.Parameters.AddWithValue("@Password", user.Password);

                ObjSqlConnection.Open();

                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                if (NoOfRowsAffected > 0)
                {
                    IsUpdated = true;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_DeleteUserByUserId";
                ObjSqlCommand.Parameters.AddWithValue("@UserId", id);

                ObjSqlConnection.Open();

                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                if (NoOfRowsAffected > 0)
                {
                    IsDeleted = true;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            return IsDeleted;

        }
    }
}
