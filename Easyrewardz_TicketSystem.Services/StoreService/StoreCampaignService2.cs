﻿using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class StoreCampaignService : IStoreCampaign
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        CustomResponse ApiResponse = null;
        string apiResponse = string.Empty;
        string apisecurityToken = string.Empty;
        string apiURL = string.Empty;
        public StoreCampaignService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Get Store Task By Ticket
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<StoreCampaignModel2> GetStoreCampaign(int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            List<StoreCampaignModel2> lstCampaign = new List<StoreCampaignModel2>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreCampaignModel2 storecampaign = new StoreCampaignModel2
                        {
                            CampaignID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            CampaignName = ds.Tables[0].Rows[i]["CampaignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignName"]),
                            CustomerCount = ds.Tables[0].Rows[i]["CustomerCount"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerCount"]),
                            ChatbotScript = ds.Tables[0].Rows[i]["ChatbotScript"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ChatbotScript"]),
                            SmsScript = ds.Tables[0].Rows[i]["SmsScript"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SmsScript"]),
                            CampaingPeriod = ds.Tables[0].Rows[i]["CampaingPeriod"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaingPeriod"]),
                            Status = Convert.ToString((StoreCampaignStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"])),
                        };
                        lstCampaign.Add(storecampaign);
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
            return lstCampaign;
        }


        /// <summary>
        /// Get Store Task By Ticket
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<CustomerpopupDetails> GetCustomerpopupDetailsList(string mobileNumber, string programCode, int tenantID, int userID)
        {
            List<CustomerpopupDetails> objorderMaster = new List<CustomerpopupDetails>();
            StoreCampaignSearchOrder objOrderSearch = new StoreCampaignSearchOrder();
            List<CustomerpopupDetails> objOrderDetails = new List<CustomerpopupDetails>();

            try
            {
             
                objOrderSearch.mobileNumber = mobileNumber;
                objOrderSearch.programCode = programCode;

                string apiReq = JsonConvert.SerializeObject(objOrderSearch);
                apiResponse = CommonService.SendApiRequest(apiURL + "CustomerOrderDetails", apiReq);

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    ApiResponse = JsonConvert.DeserializeObject<CustomResponse>(apiResponse);

                    if (ApiResponse != null)
                    {
                        objOrderDetails = JsonConvert.DeserializeObject<List<CustomerpopupDetails>>(Convert.ToString((ApiResponse.Responce)));

                        if (objOrderDetails != null)
                        {
                            if (objOrderDetails.Count > 0)
                            {
                                for (int k = 0; k < objOrderDetails.Count; k++)
                                {
                                    CustomerpopupDetails orderDetails = new CustomerpopupDetails();
                                    
                                    objorderMaster.Add(orderDetails);
                                }
                            }
                        }

                    }

                }



            }
            catch (Exception)
            {
                throw;
            }
            return objorderMaster;
        }

        /// <summary>
        /// Get Store Task By Ticket
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<StoreCampaignLogo> GetCampaignDetailsLogo(int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            List<StoreCampaignLogo> lstCampaignLogo = new List<StoreCampaignLogo>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignLogoList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreCampaignLogo storecampaign = new StoreCampaignLogo
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["IdCampaignLogo"]),
                            name = ds.Tables[0].Rows[i]["Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Name"]),
                        };
                        lstCampaignLogo.Add(storecampaign);
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
            return lstCampaignLogo;
        }

    }

}
