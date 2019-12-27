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
        #endregion

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
                        priority.TenantID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        priority.PriortyName = Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]);
                        priority.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

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
    }
}
