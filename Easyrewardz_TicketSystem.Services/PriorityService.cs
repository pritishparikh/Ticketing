using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
  public class PriorityService :IPriority
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public PriorityService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        /// <summary>
        ///  Add Priority
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public int AddPriority(string PriorityName, int status,int tenantID,int UserID)
        {

            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd  = new MySqlCommand("SP_InsertPriority", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Priority_Name", PriorityName);
                cmd.Parameters.AddWithValue("@Is_status", status);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public int DeletePriority(int PriorityID, int tenantID, int UserID)
        {

            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeletePriority", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Priority_ID", PriorityID);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
        /// <returns></returns>
        public List<Priority> GetPriorityList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Priority> objPriority = new List<Priority>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetPriorityList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Priority priority = new Priority();
                        priority.PriorityID = Convert.ToInt32(ds.Tables[0].Rows[i]["PriorityID"]);
                        priority.PriortyName = Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]);
                        priority.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

                        priority.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]);
                        priority.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                        priority.CreatedDateFormated = priority.CreatedDate.ToString("dd/MMM/yyyy");
                        priority.ModifiedByName= Convert.ToString(ds.Tables[0].Rows[i]["ModifiedBy"]);
                        priority.ModifiedDate= Convert.ToDateTime(ds.Tables[0].Rows[i]["ModifiedDate"]);
                        priority.ModifiedDateFormated = priority.ModifiedDate.ToString("dd/MMM/yyyy");
                        priority.PriortyStatus = Convert.ToString(ds.Tables[0].Rows[i]["PriortyStatus"]);  
                        objPriority.Add(priority);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
                MySqlCommand cmd = new MySqlCommand("SP_UpdatePriority", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Priority_ID", PriorityID);
                cmd.Parameters.AddWithValue("@Priority_Name", PriorityName);
                cmd.Parameters.AddWithValue("@Is_status", status);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
        #endregion
    }
}
