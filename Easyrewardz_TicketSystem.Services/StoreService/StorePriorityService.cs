using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class StorePriorityService : IStorePriority
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();

        public StorePriorityService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        #endregion
        /// <summary>
        ///  Add Priority
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public int AddPriority(string PriorityName, int status, int tenantID, int UserID)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertStorePriority", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Priority_Name", PriorityName);
                cmd.Parameters.AddWithValue("@Is_status", status);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return success;
        }
        /// <summary>
        ///  Delete Priority
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        public int DeletePriority(int PriorityID, int tenantID, int UserID)
        {

            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteStorePriority", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Priority_ID", PriorityID);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return success;
        }
        /// <summary>
        /// Get Priority List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        public List<Priority> GetPriorityList(int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Priority> objPriority = new List<Priority>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStorePriorityList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Priority priority = new Priority();
                        priority.PriorityID = Convert.ToInt32(ds.Tables[0].Rows[i]["PriorityID"]);
                        priority.PriortyName = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]);
                        objPriority.Add(priority);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return objPriority;
        }
        /// <summary>
        /// Get list of the priority
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        public List<Priority> PriorityList(int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Priority> objPriority = new List<Priority>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_StorePriorityList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Priority priority = new Priority();
                        priority.PriorityID = Convert.ToInt32(ds.Tables[0].Rows[i]["PriorityID"]);
                        priority.PriortyName = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]);
                        priority.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        priority.CreatedByName = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]);
                        priority.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                        priority.CreatedDateFormated = priority.CreatedDate.ToString("dd/MMM/yyyy");
                        priority.ModifiedByName = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedBy"]);
                        priority.ModifiedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ModifiedDate"]);
                        priority.ModifiedDateFormated = priority.ModifiedDate.ToString("dd/MMM/yyyy");
                        priority.PriortyStatus = ds.Tables[0].Rows[i]["PriortyStatus"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyStatus"]);
                        objPriority.Add(priority);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return objPriority;
        }
        /// <summary>
        ///  Update Priority
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>

        public int UpdatePriority(int PriorityID, string PriorityName, int status, int tenantID, int UserID)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateStorePriority", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Priority_ID", PriorityID);
                cmd.Parameters.AddWithValue("@Priority_Name", PriorityName);
                cmd.Parameters.AddWithValue("@Is_status", status);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return success;
        }
        /// <summary>
        /// Update priority order
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="selectedPriorityID"></param>
        /// <param name="currentPriorityID"></param>
        /// <param name="PriorityFor"></param>        
        /// <returns></returns>
        public bool UpdatePriorityOrder(int TenantID, int selectedPriorityID, int currentPriorityID)
        {
            bool isUpdate = false;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateStorePriorityOrder", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@selectedPriorityID", selectedPriorityID);
                cmd.Parameters.AddWithValue("@currentPriorityID", currentPriorityID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteScalar();
                isUpdate = true;
            }
            catch (Exception)
            {
                isUpdate = false;
            }
            finally
            {
            }

            return isUpdate;
        }
    }
}
