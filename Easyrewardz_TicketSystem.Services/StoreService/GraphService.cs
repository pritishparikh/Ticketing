using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

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

        /// <summary>
        /// Get GraphCountData
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="UserIds"></param>
        /// <param name="BrandIDs"></param>
        /// <returns></returns>
        public GraphModal GetGraphCountData(int TenantID, int UserID, GraphCountDataRequest GraphCountData)
        {
            DataSet ds = new DataSet();
            GraphModal obj = new GraphModal();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Sp_GetGraphCountData", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_UserID", UserID);
                cmd.Parameters.AddWithValue("@UserIds", GraphCountData.UserIds);
                cmd.Parameters.AddWithValue("@BrandIDs", GraphCountData.BrandIDs);
                cmd.Parameters.AddWithValue("@DateFrom", GraphCountData.DateFrom);
                cmd.Parameters.AddWithValue("@DateEnd", GraphCountData.DateEnd);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        obj = new GraphModal
                        {
                            TaskOpen = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskOpen"]),
                            TaskOverDue = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskOverDue"]),
                            TaskDueToday = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskDueToday"]),
                            CampaingnOpen = Convert.ToInt32(ds.Tables[0].Rows[0]["CampaingOpen"]),
                            ClaimOpen = Convert.ToInt32(ds.Tables[0].Rows[0]["ClaimOpen"]),
                            ClaimDueToday = Convert.ToInt32(ds.Tables[0].Rows[0]["ClaimDueToday"]),
                            ClaimOverDue = Convert.ToInt32(ds.Tables[0].Rows[0]["ClaimOverDue"])
                        };
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

            return obj;
        }

        /// <summary>
        /// Get Graph Data
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="GraphCountData"></param>
        /// <returns></returns>
        public GraphData GetGraphData(int TenantID, int UserID, GraphCountDataRequest GraphCountData)
        {
            DataSet ds = new DataSet();
            GraphData obj = new GraphData();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Sp_GetStoreGraphData", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_UserID", UserID);
                cmd.Parameters.AddWithValue("@UserIds", GraphCountData.UserIds);
                cmd.Parameters.AddWithValue("@BrandIDs", GraphCountData.BrandIDs);
                cmd.Parameters.AddWithValue("@DateFrom", GraphCountData.DateFrom);
                cmd.Parameters.AddWithValue("@DateEnd", GraphCountData.DateEnd);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //obj = new GraphModal
                        //{
                        //    TaskOpen = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskOpen"]),
                        //    TaskOverDue = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskOverDue"]),
                        //    TaskDueToday = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskDueToday"]),
                        //    CampaingnOpen = Convert.ToInt32(ds.Tables[0].Rows[0]["CampaingOpen"]),
                        //    ClaimOpen = Convert.ToInt32(ds.Tables[0].Rows[0]["ClaimOpen"]),
                        //    ClaimDueToday = Convert.ToInt32(ds.Tables[0].Rows[0]["ClaimDueToday"]),
                        //    ClaimOverDue = Convert.ToInt32(ds.Tables[0].Rows[0]["ClaimOverDue"])
                        //};
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

            return obj;
        }

    }
}
