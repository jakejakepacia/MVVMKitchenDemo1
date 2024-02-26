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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MVVMKitchenDemo1.Models
{
    public class SiteService
    {
        SqlConnection ObjSqlConnection;
        SqlCommand ObjSqlCommand;

        public SiteService()
        {
            ObjSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["KEConnection"].ConnectionString);
            ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandType = CommandType.StoredProcedure;
        }

        public List<Site> GetSitesByUser(int userId)
        {
            List<Site> ObjUserList = new List<Site>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "GetSiteByUserId";
                ObjSqlCommand.Parameters.AddWithValue("@UserId", userId);

                ObjSqlConnection.Open();

                var objSqlDataReader = ObjSqlCommand.ExecuteReader();

                if (objSqlDataReader.HasRows)
                {
                    Site ObjUser = null;
                    while (objSqlDataReader.Read())
                    {
                        ObjUser = new Site();
                        ObjUser.SiteId = objSqlDataReader.GetInt32(0);
                        ObjUser.UserId = objSqlDataReader.GetInt32(1);
                        ObjUser.Description = objSqlDataReader.GetString(2);
                        ObjUser.IsActive = objSqlDataReader.GetBoolean(3);

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

        public bool Add(Site site)
        {
            bool IsAdded = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "AddSite";
                ObjSqlCommand.Parameters.AddWithValue("@Description", site.Description);
                ObjSqlCommand.Parameters.AddWithValue("@Active", site.IsActive);
                ObjSqlCommand.Parameters.AddWithValue("@UserId", site.UserId);

                ObjSqlConnection.Open();

                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                if (NoOfRowsAffected > 0)
                {
                    IsAdded = true;
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
            return IsAdded;
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "[DeletSite]";
                ObjSqlCommand.Parameters.AddWithValue("@SiteId", id);

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

        public bool Update(Site site)
        {
            bool IsUpdated = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "[UpdateSite]";
                ObjSqlCommand.Parameters.AddWithValue("@Description", site.Description);
                ObjSqlCommand.Parameters.AddWithValue("@Active", site.IsActive);
                ObjSqlCommand.Parameters.AddWithValue("@SiteId", site.SiteId);
            

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
    }
}
