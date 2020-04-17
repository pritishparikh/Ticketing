using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class GraphService : IGraph
    {
        MySqlConnection conn = new MySqlConnection();

        #region Constructor
        public GraphService(string connectionString)
        {
            conn.ConnectionString = connectionString;
        }
        #endregion


        /// <summary>
        /// GetUserList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        public List<User> GetUserList(int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            List<User> users = new List<User>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreDasboardUserFullName", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        User user = new User
                        {
                            UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]),
                            FullName = Convert.ToString(ds.Tables[0].Rows[i]["FullName"]),
                            ReporteeID = Convert.ToInt32(ds.Tables[0].Rows[i]["ReporteeID"]),
                            RoleID = Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"]),
                            RoleName = Convert.ToString(ds.Tables[0].Rows[i]["RoleName"])
                        };



                        users.Add(user);
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

            return users;
        }
    }
}
