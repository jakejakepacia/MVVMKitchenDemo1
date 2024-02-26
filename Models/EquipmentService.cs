using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMKitchenDemo1.Models
{
    public class EquipmentService
    {
        SqlConnection ObjSqlConnection;
        SqlCommand ObjSqlCommand;

        public EquipmentService()
        {
            ObjSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["KEConnection"].ConnectionString);
            ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandType = CommandType.StoredProcedure;
        }

        public List<Equipment> GetEquipmentByUserId(int userId)
        {
            List<Equipment> ObjEquipmentList = new List<Equipment>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "GetEquipmentByUserId";
                ObjSqlCommand.Parameters.AddWithValue("@UserId", userId);

                ObjSqlConnection.Open();

                var objSqlDataReader = ObjSqlCommand.ExecuteReader();

                if (objSqlDataReader.HasRows)
                {
                    Equipment ObjEquipment = null;
                    while (objSqlDataReader.Read())
                    {
                        ObjEquipment = new Equipment();
                        ObjEquipment.EquipmentId = objSqlDataReader.GetInt32(2);
                        ObjEquipment.SerialNumber = objSqlDataReader.GetInt32(3);
                        ObjEquipment.Description = objSqlDataReader.GetString(4);
                        ObjEquipment.Condition = objSqlDataReader.GetString(5);
                        ObjEquipment.UserId = objSqlDataReader.GetInt32(6);
                        ObjEquipment.SiteId = objSqlDataReader.GetInt32(0);
                        ObjEquipment.SiteDescription = objSqlDataReader.GetString(1);
                        ObjEquipmentList.Add(ObjEquipment);
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

            return ObjEquipmentList;
        }

        public List<Equipment> GetEquipmentBySite(Site site)
        {
            List<Equipment> ObjEquipmentList = new List<Equipment>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "GetEquipmentBySiteId";
                ObjSqlCommand.Parameters.AddWithValue("@UserId", site.UserId);
                ObjSqlCommand.Parameters.AddWithValue("@SiteId", site.SiteId);

                ObjSqlConnection.Open();

                var objSqlDataReader = ObjSqlCommand.ExecuteReader();

                if (objSqlDataReader.HasRows)
                {
                    Equipment ObjUser = null;
                    while (objSqlDataReader.Read())
                    {
                        ObjUser = new Equipment();
                        ObjUser.EquipmentId = objSqlDataReader.GetInt32(0);
                        ObjUser.SerialNumber = objSqlDataReader.GetInt32(1);
                        ObjUser.Description = objSqlDataReader.GetString(2);
                        ObjUser.Condition = objSqlDataReader.GetString(3);
                        ObjUser.UserId = objSqlDataReader.GetInt32(4);


                        ObjEquipmentList.Add(ObjUser);
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

            return ObjEquipmentList;
        }

        public bool Add(Equipment equipment)
        {
            bool IsAdded = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "AddEquipment";
                ObjSqlCommand.Parameters.AddWithValue("@SerialNumber", equipment.SerialNumber);
                ObjSqlCommand.Parameters.AddWithValue("@Description", equipment.Description);
                ObjSqlCommand.Parameters.AddWithValue("@Condition", equipment.Condition);
                ObjSqlCommand.Parameters.AddWithValue("@UserId", equipment.UserId);
                ObjSqlCommand.Parameters.AddWithValue("@SiteId", equipment.SiteId);

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

        public bool Update(Equipment equipment)
        {
            bool IsUpdated = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "UpdateEquipment";
                ObjSqlCommand.Parameters.AddWithValue("@EquipmentId", equipment.EquipmentId);
                ObjSqlCommand.Parameters.AddWithValue("@SerialNumber", equipment.SerialNumber);
                ObjSqlCommand.Parameters.AddWithValue("@Description", equipment.Description);
                ObjSqlCommand.Parameters.AddWithValue("@Condition", equipment.Condition);
                ObjSqlCommand.Parameters.AddWithValue("@UserId", equipment.UserId);


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

        public bool Delete(int equipmentId)
        {
            bool IsDeleted = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "DeleteEquipment";
                ObjSqlCommand.Parameters.AddWithValue("@EquipmentId", equipmentId);

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
