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
            GraphData obj = new GraphData
            {
                OpenTaskDepartmentWise = new List<OpenTaskDepartmentWise>(),
                TaskByPriority = new List<TaskByPriority>(),
                OpenCampaignByType = new List<OpenCampaignByType>(),
                ClaimVsInvoiceArticle = new List<ClaimVsInvoiceArticle>(),
                OpenClaimStatus = new List<OpenClaimStatus>(),
                ClaimVsInvoiceAmount = new List<ClaimVsInvoiceAmount>()
            };
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
                if (ds != null)
                {
                    if (ds.Tables[0] != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                OpenTaskDepartmentWise objOpenTaskDepartmentWise = new OpenTaskDepartmentWise()
                                {
                                    ID = ds.Tables[0].Rows[i]["DepartmentID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentID"]),
                                    Name = ds.Tables[0].Rows[i]["DepartmentName"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]),
                                    Value = ds.Tables[0].Rows[i]["deptcount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["deptcount"]),
                                };
                                obj.OpenTaskDepartmentWise.Add(objOpenTaskDepartmentWise);
                            }
                        }
                    }

                    if (ds.Tables[1] != null)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                TaskByPriority objTaskByPriority = new TaskByPriority()
                                {
                                    ID = ds.Tables[1].Rows[i]["PriorityID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["PriorityID"]),
                                    Name = ds.Tables[1].Rows[i]["PriortyName"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[1].Rows[i]["PriortyName"]),
                                    Value = ds.Tables[1].Rows[i]["PriorityCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["PriorityCount"]),
                                };
                                obj.TaskByPriority.Add(objTaskByPriority);
                            }
                        }
                    }

                    if (ds.Tables[2] != null)
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                OpenCampaignByType objOpenCampaignByType = new OpenCampaignByType()
                                {
                                    ID = ds.Tables[2].Rows[i]["CampaignTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[2].Rows[i]["CampaignTypeID"]),
                                    Name = ds.Tables[2].Rows[i]["CampaignName"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[2].Rows[i]["CampaignName"]),
                                    Value = ds.Tables[2].Rows[i]["CampaignTypeCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[2].Rows[i]["CampaignTypeCount"]),
                                };
                                obj.OpenCampaignByType.Add(objOpenCampaignByType);
                            }
                        }
                    }

                    if (ds.Tables[3] != null)
                    {
                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                            {
                                OpenClaimStatus objOpenClaimStatus = new OpenClaimStatus()
                                {
                                    ID = 0,
                                    Name = ds.Tables[3].Rows[i]["ClaimTypeName"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[3].Rows[i]["ClaimTypeName"]),
                                    Value = ds.Tables[3].Rows[i]["ClaimTypeCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[3].Rows[i]["ClaimTypeCount"]),
                                };
                                obj.OpenClaimStatus.Add(objOpenClaimStatus);
                            }
                        }
                    }

                    if (ds.Tables[4] != null)
                    {
                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                            {
                                ClaimVsInvoiceAmount objClaimVsInvoiceAmount = new ClaimVsInvoiceAmount()
                                {
                                    ID = 0,
                                    Name = ds.Tables[4].Rows[i]["totalName"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[4].Rows[i]["totalName"]),
                                    Value = ds.Tables[4].Rows[i]["totalInvoce"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[4].Rows[i]["totalInvoce"]),
                                };
                                obj.ClaimVsInvoiceAmount.Add(objClaimVsInvoiceAmount);
                            }
                        }
                    }

                    if (ds.Tables[5] != null)
                    {
                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                            {
                                ClaimVsInvoiceArticle objClaimVsInvoiceArticle = new ClaimVsInvoiceArticle()
                                {
                                    ID = 0,
                                    Name = ds.Tables[5].Rows[i]["totalName"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[5].Rows[i]["totalName"]),
                                    Value = ds.Tables[5].Rows[i]["TotalCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[5].Rows[i]["TotalCount"]),
                                };
                                obj.ClaimVsInvoiceArticle.Add(objClaimVsInvoiceArticle);
                            }
                        }
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
