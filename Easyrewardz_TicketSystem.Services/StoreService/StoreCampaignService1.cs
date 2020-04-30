using Easyrewardz_TicketSystem.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class StoreCampaignService : IStoreCampaign
    {
        /// <summary>
        /// Get Campaign Customer
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="campaignScriptID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<CampaignCustomerModel> GetCampaignCustomer(int tenantID, int userID, int campaignScriptID, int pageNo, int pageSize)
        {
            DataSet ds = new DataSet();
            List<CampaignCustomerModel> objList = new List<CampaignCustomerModel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignCustomer", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                cmd.Parameters.AddWithValue("@_UserID", userID);
                cmd.Parameters.AddWithValue("@_CampaignScriptID", campaignScriptID);
                cmd.Parameters.AddWithValue("@_pageno", pageNo);
                cmd.Parameters.AddWithValue("@_pagesize", pageSize);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CampaignCustomerModel obj = new CampaignCustomerModel
                        {

                            ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            CampaignScriptID = ds.Tables[0].Rows[i]["CampaignScriptID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CampaignScriptID"]),
                            CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]),
                            CustomerNumber = ds.Tables[0].Rows[i]["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerNumber"]),
                            CustomerEmailID = ds.Tables[0].Rows[i]["CustomerEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerEmailID"]),
                            DOB = ds.Tables[0].Rows[i]["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DOB"]),
                            CampaignDate = ds.Tables[0].Rows[i]["CampaignDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignDate"]),
                            ResponseID = ds.Tables[0].Rows[i]["ResponseID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ResponseID"]),
                            CallRescheduledTo = ds.Tables[0].Rows[i]["CallRescheduledTo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CallRescheduledTo"]),
                            DoesTicketRaised = ds.Tables[0].Rows[i]["DoesTicketRaised"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DoesTicketRaised"]),
                            StatusName = ds.Tables[0].Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StatusName"]),
                            StatusID = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]),
                            HSCampaignResponseList = ds.Tables[1].AsEnumerable().Select(r => new HSCampaignResponse()
                            {
                                ResponseID = r.Field<object>("ResponseID") == DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("ResponseID")),
                                Response = r.Field<object>("Response") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("Response")),
                                Status = r.Field<object>("Status") == DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("Status")),
                                StatusName = r.Field<object>("StatusName") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("StatusName"))

                            }).ToList()
                        };
                        objList.Add(obj);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return objList;
        }
    }
}
