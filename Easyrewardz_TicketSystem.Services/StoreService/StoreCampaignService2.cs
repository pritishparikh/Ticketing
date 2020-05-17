using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.Extensions.Configuration;
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
        private IConfiguration configuration;
        CustomResponse ApiResponse = null;
        string apiResponse = string.Empty;
        string apiResponse1 = string.Empty;
        string apisecurityToken = string.Empty;
        string apiURL = string.Empty;
        string apiURLGetUserATVDetails = string.Empty;

        public StoreCampaignService(string _connectionString)
        {
          
            conn.ConnectionString = _connectionString;
            apisecurityToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJQcm9ncmFtQ29kZSI6IkJhdGEiLCJVc2VySUQiOiIzIiwiQXBwSUQiOiI3IiwiRGF5IjoiMjgiLCJNb250aCI6IjMiLCJZZWFyIjoiMjAyMSIsIlJvbGUiOiJBZG1pbiIsImlzcyI6IkF1dGhTZWN1cml0eUlzc3VlciIsImF1ZCI6IkF1dGhTZWN1cml0eUF1ZGllbmNlIn0.0XeF7V5LWfQn0NlSlG7Rb-Qq1hUCtUYRDg6dMGIMvg0";
            //apiURLGetUserATVDetails = configuration.GetValue<string>("apiURLGetUserATVDetails");
        }
        #endregion

        /// <summary>
        /// Get Store Task By Ticket
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<StoreCampaignModel2> GetStoreCampaign(int tenantID, int userID, string campaignName, string statusId)
        {
            DataSet ds = new DataSet();
            List<StoreCampaignModel2> lstCampaign = new List<StoreCampaignModel2>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", userID);
                cmd.Parameters.AddWithValue("@_campaignName", campaignName);
                cmd.Parameters.AddWithValue("@_statusId", statusId);

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
                            SmsFlag = Convert.ToBoolean(ds.Tables[0].Rows[i]["SmsFlag"]),
                            EmailFlag = Convert.ToBoolean(ds.Tables[0].Rows[i]["EmailFlag"]),
                            MessengerFlag = Convert.ToBoolean(ds.Tables[0].Rows[i]["MessengerFlag"]),
                            BotFlag = Convert.ToBoolean(ds.Tables[0].Rows[i]["BotFlag"]),
                            Status = Convert.ToString((StoreCampaignStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"])),
                            MaxClickAllowed = ds.Tables[0].Rows[i]["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["MaxClickAllowed"]),
                          // SmsClickCount = ds.Tables[0].Rows[i]["SmsClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SmsClickCount"]),
                          //  EmailClickCount = ds.Tables[0].Rows[i]["EmailClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["EmailClickCount"]),
                          //  MessengerClickCount = ds.Tables[0].Rows[i]["MessengerClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["MessengerClickCount"]),
                          //  BotClickCount = ds.Tables[0].Rows[i]["BotClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BotClickCount"]),
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
        ///Get Customer popup Details List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public StoresCampaignStatusResponse GetCustomerpopupDetailsList(string mobileNumber, string programCode, int tenantID, int userID, string ClientAPIURL)
        {
            StoresCampaignStatusResponse obj = new StoresCampaignStatusResponse();
            StoreCampaignSearchOrder objOrderSearch = new StoreCampaignSearchOrder();
            CustomerpopupDetails objpopupDetails = new CustomerpopupDetails();
            StoreCampaignLastTransactionDetails objLastTransactionDetails = new StoreCampaignLastTransactionDetails();
            StoreCampaignKeyInsight objkeyinsight = new StoreCampaignKeyInsight();
            List<StoreCampaignRecommended> objrecommended = new List<StoreCampaignRecommended>();
            List<StoreCampaignRecommended> objrecommendedDetails = new List<StoreCampaignRecommended>();

            string apiReq = string.Empty;
            DataSet ds = new DataSet();

            try
            {

                objOrderSearch.mobileNumber = mobileNumber;
                objOrderSearch.programCode = programCode;
                objOrderSearch.securityToken = apisecurityToken;

                try
                {
                    apiReq = JsonConvert.SerializeObject(objOrderSearch);
                    apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetUserATVDetails", apiReq);

                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        ApiResponse = JsonConvert.DeserializeObject<CustomResponse>(apiResponse);

                        if (apiResponse != null)
                        {
                            objpopupDetails = JsonConvert.DeserializeObject<CustomerpopupDetails>(((apiResponse)));

                            if (objpopupDetails != null)
                            {
                                CustomerpopupDetails popupDetail = new CustomerpopupDetails
                                {
                                    name = objpopupDetails.name,
                                    mobileNumber = objpopupDetails.mobileNumber,
                                    tiername = objpopupDetails.tiername,
                                    lifeTimeValue = objpopupDetails.lifeTimeValue,
                                    visitCount = objpopupDetails.visitCount
                                };
                                obj.useratvdetails = popupDetail;

                            }
                            else
                            {
                                CustomerpopupDetails popupDetail = new CustomerpopupDetails
                                {
                                    name = "",
                                    mobileNumber = "",
                                    tiername = "",
                                    lifeTimeValue = "",
                                    visitCount = ""
                                };
                                obj.useratvdetails = popupDetail;
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    if (obj.useratvdetails == null)
                    {
                        CustomerpopupDetails popupDetail = new CustomerpopupDetails
                        {
                            name = "",
                            mobileNumber = "",
                            tiername = "",
                            lifeTimeValue = "",
                            visitCount = ""
                        };
                        obj.useratvdetails = popupDetail;

                    }
                }

                try
                {
                    apiResponse = string.Empty;
                    apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetKeyInsight", apiReq);

                    if (!string.IsNullOrEmpty(apiResponse))
                    {

                        if (!string.IsNullOrEmpty(apiResponse.Replace("[]", "")))
                        {
                            objkeyinsight = JsonConvert.DeserializeObject<StoreCampaignKeyInsight>(((apiResponse)));

                            if (objkeyinsight != null)
                            {
                                StoreCampaignKeyInsight popupDetail = new StoreCampaignKeyInsight
                                {
                                    mobileNumber = objkeyinsight.mobileNumber,
                                    insightText = objkeyinsight.insightText
                                };
                                obj.campaignkeyinsight = popupDetail;
                            }
                            else
                            {
                                StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight
                                {
                                    mobileNumber = "",
                                    insightText = ""//GetKeyInsightAsChatBot(mobileNumber, programCode, tenantID, userID)
                                };
                                obj.campaignkeyinsight = KeyInsight;
                            }
                        }
                        else
                        {
                            StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight
                            {
                                mobileNumber = "",
                                insightText = ""//GetKeyInsightAsChatBot(mobileNumber, programCode, tenantID, userID)
                            };
                            obj.campaignkeyinsight = KeyInsight;
                        }
                    }
                }
                catch (Exception)
                {
                    if (obj.campaignkeyinsight == null)
                    {
                        StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight
                        {
                            mobileNumber = "",
                            insightText = ""
                        };
                        obj.campaignkeyinsight = KeyInsight;
                    }
                }

                if (obj.useratvdetails != null)
                {
                    if (string.IsNullOrEmpty(obj.useratvdetails.lifeTimeValue) && string.IsNullOrEmpty(obj.useratvdetails.visitCount))
                    {
                        if (obj.campaignkeyinsight != null)
                        {
                            if (string.IsNullOrEmpty(obj.campaignkeyinsight.insightText))
                            {
                                StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight
                                {
                                    mobileNumber = mobileNumber,
                                    insightText = GetKeyInsightAsChatBot(mobileNumber, programCode, tenantID, userID),
                                    ShowKeyInsights = false
                                };
                                obj.campaignkeyinsight = KeyInsight;
                            }
                        }
                    }
                }

                try
                {
                    if (conn != null && conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignRecommendedList", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@User_ID", userID);
                    cmd.Parameters.AddWithValue("@mobile_Number", mobileNumber);
                    cmd.Parameters.AddWithValue("@program_Code", programCode);

                    MySqlDataAdapter da = new MySqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                    da.Fill(ds);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended();
                            RecommendedDetail.mobileNumber = ds.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]);
                            RecommendedDetail.itemCode = ds.Tables[0].Rows[i]["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
                            RecommendedDetail.category = ds.Tables[0].Rows[i]["Category"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
                            RecommendedDetail.subCategory = ds.Tables[0].Rows[i]["SubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategory"]);
                            RecommendedDetail.brand = ds.Tables[0].Rows[i]["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Brand"]);
                            RecommendedDetail.color = ds.Tables[0].Rows[i]["Color"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Color"]);
                            RecommendedDetail.size = ds.Tables[0].Rows[i]["Size"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Size"]);
                            RecommendedDetail.price = ds.Tables[0].Rows[i]["Price"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Price"]);
                            RecommendedDetail.url = ds.Tables[0].Rows[i]["Url"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Url"]);
                            RecommendedDetail.imageURL = ds.Tables[0].Rows[i]["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageURL"]);
                            objrecommended.Add(RecommendedDetail);

                        }
                        obj.campaignrecommended = objrecommended;
                    }
                    else
                    {
                        StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended();

                        RecommendedDetail.mobileNumber = "";
                        RecommendedDetail.itemCode = "";
                        RecommendedDetail.category = "";
                        RecommendedDetail.subCategory = "";
                        RecommendedDetail.brand = "";
                        RecommendedDetail.color = "";
                        RecommendedDetail.size = "";
                        RecommendedDetail.price = "";
                        RecommendedDetail.url = "";
                        RecommendedDetail.imageURL = "";
                        objrecommended.Add(RecommendedDetail);
                        obj.campaignrecommended = objrecommended;
                    }
                }
                catch (Exception)
                {
                    StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended();

                    RecommendedDetail.mobileNumber = "";
                    RecommendedDetail.itemCode = "";
                    RecommendedDetail.category = "";
                    RecommendedDetail.subCategory = "";
                    RecommendedDetail.brand = "";
                    RecommendedDetail.color = "";
                    RecommendedDetail.size = "";
                    RecommendedDetail.price = "";
                    RecommendedDetail.url = "";
                    RecommendedDetail.imageURL = "";
                    objrecommended.Add(RecommendedDetail);
                    obj.campaignrecommended = objrecommended;
                }

                try
                {
                    apiResponse = string.Empty;
                    apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetLastTransactionDetails", apiReq);

                    if (!string.IsNullOrEmpty(apiResponse))
                    {

                        if (apiResponse != null)
                        {
                            objLastTransactionDetails = JsonConvert.DeserializeObject<StoreCampaignLastTransactionDetails>(((apiResponse)));

                            if (objLastTransactionDetails != null)
                            {
                                //if (objrecommendedDetails.Count > 0)
                                //{
                                //    StoreCampaignLastTransactionDetails LastTransactionDetails = new StoreCampaignLastTransactionDetails();

                                //    LastTransactionDetails.billNo = LastTransactionDetails.billNo;
                                //    LastTransactionDetails.billDate = LastTransactionDetails.billDate; ;
                                //    LastTransactionDetails.storeName = LastTransactionDetails.storeName;
                                //    LastTransactionDetails.amount = LastTransactionDetails.amount;
                                //    //LastTransactionDetails.itemDetails = LastTransactionDetails.itemDetails;
                                //    obj.lasttransactiondetails = LastTransactionDetails;
                                //}
                                obj.lasttransactiondetails = objLastTransactionDetails;
                            }
                            else
                            {
                                StoreCampaignLastTransactionDetails LastTransactionDetails = new StoreCampaignLastTransactionDetails();

                                LastTransactionDetails.billNo = "";
                                LastTransactionDetails.billDate = "";
                                LastTransactionDetails.storeName = "";
                                LastTransactionDetails.amount = "";
                                //  LastTransactionDetails.itemDetails = "";
                                obj.lasttransactiondetails = LastTransactionDetails;
                            }

                        }
                    }
                }
                catch (Exception)
                {
                    if (obj.lasttransactiondetails == null)
                    {
                        StoreCampaignLastTransactionDetails LastTransactionDetails = new StoreCampaignLastTransactionDetails();

                        LastTransactionDetails.billNo = "";
                        LastTransactionDetails.billDate = "";
                        LastTransactionDetails.storeName = "";
                        LastTransactionDetails.amount = "";
                        //  LastTransactionDetails.itemDetails = "";
                        obj.lasttransactiondetails = LastTransactionDetails;
                    }
                }

                try
                {
                    if (conn != null && conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetShareCampaignViaSetting", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@User_ID", userID);
                    cmd.Parameters.AddWithValue("@mobile_Number", mobileNumber);
                    cmd.Parameters.AddWithValue("@program_Code", programCode);
                    ds = new DataSet();
                    MySqlDataAdapter da = new MySqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                    da.Fill(ds);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        ShareCampaignViaSettingModal shareCampaignViaSettingModal = new ShareCampaignViaSettingModal
                        {
                            CustomerName = ds.Tables[0].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]),
                            CustomerNumber = ds.Tables[0].Rows[0]["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CustomerNumber"]),
                            SmsFlag = ds.Tables[0].Rows[0]["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["SmsFlag"]),
                            SmsClickCount = ds.Tables[0].Rows[0]["SmsClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["SmsClickCount"]),
                            SmsClickable = ds.Tables[0].Rows[0]["SmsClickable"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["SmsClickable"]),
                            EmailFlag = ds.Tables[0].Rows[0]["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["EmailFlag"]),
                            EmailClickCount = ds.Tables[0].Rows[0]["EmailClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["EmailClickCount"]),
                            EmailClickable = ds.Tables[0].Rows[0]["EmailClickable"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["EmailClickable"]),
                            MessengerFlag = ds.Tables[0].Rows[0]["MessengerFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["MessengerFlag"]),
                            MessengerClickCount = ds.Tables[0].Rows[0]["MessengerClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["MessengerClickCount"]),
                            MessengerClickable = ds.Tables[0].Rows[0]["MessengerClickable"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["MessengerClickable"]),
                            BotFlag = ds.Tables[0].Rows[0]["BotFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["BotFlag"]),
                            BotClickCount = ds.Tables[0].Rows[0]["BotClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["BotClickCount"]),
                            BotClickable = ds.Tables[0].Rows[0]["BotClickable"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["BotClickable"]),
                            MaxClickAllowed = ds.Tables[0].Rows[0]["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["MaxClickAllowed"]),
                        };

                        obj.ShareCampaignViaSettingModal = shareCampaignViaSettingModal;
                    }
                    else
                    {
                        ShareCampaignViaSettingModal shareCampaignViaSettingModal = new ShareCampaignViaSettingModal
                        {
                            CustomerName = "",
                            CustomerNumber = "",
                            SmsFlag = false,
                            SmsClickCount = 0,
                            SmsClickable = false,
                            EmailFlag = false,
                            EmailClickCount = 0,
                            EmailClickable = false,
                            MessengerFlag = false,
                            MessengerClickCount = 0,
                            MessengerClickable = false,
                            BotFlag = false,
                            BotClickCount = 0,
                            BotClickable = false,
                            MaxClickAllowed = 0,
                        };

                        obj.ShareCampaignViaSettingModal = shareCampaignViaSettingModal;
                    }
                }
                catch (Exception)
                {
                    ShareCampaignViaSettingModal shareCampaignViaSettingModal = new ShareCampaignViaSettingModal
                    {
                        CustomerName = "",
                        CustomerNumber = "",
                        SmsFlag = false,
                        SmsClickCount = 0,
                        SmsClickable = false,
                        EmailFlag = false,
                        EmailClickCount = 0,
                        EmailClickable = false,
                        MessengerFlag = false,
                        MessengerClickCount = 0,
                        MessengerClickable = false,
                        BotFlag = false,
                        BotClickCount = 0,
                        BotClickable = false,
                        MaxClickAllowed = 0,
                    };

                    obj.ShareCampaignViaSettingModal = shareCampaignViaSettingModal;
                }
            }
            catch (Exception)
            {

            }
            // InsertApiResponseData(obj, userID);
            return obj;
        }

        /// <summary>
        /// Get Key Insight As ChatBot
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetKeyInsightAsChatBot(string mobileNumber, string programCode, int tenantID, int userID)
        {
            string obj = "";
            DataSet ds = new DataSet();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand("SP_HSGetKeyInsightAsChatBot", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                cmd.Parameters.AddWithValue("@_UserID", userID);
                cmd.Parameters.AddWithValue("@_ProgramCode", programCode);
                cmd.Parameters.AddWithValue("@_MobileNumber", mobileNumber);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    obj = ds.Tables[0].Rows[0]["Message"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                }
            }
            catch(Exception ex)
            {
                obj = "";
            }

            return obj;
        }

        ///// <summary>
        /////Get Customer popup Details List
        ///// </summary>
        ///// <param name="tenantID"></param>
        ///// <param name="userID"></param>
        ///// <param name="mobileNumber"></param>
        ///// <param name="programCode"></param>
        ///// <returns></returns>
        //public StoresCampaignStatusResponse GetCustomerpopupDetailsList(string mobileNumber, string programCode, int tenantID, int userID)
        //{
        //    StoresCampaignStatusResponse obj = new StoresCampaignStatusResponse();
        //    StoreCampaignSearchOrder objOrderSearch = new StoreCampaignSearchOrder();
        //    List<CustomerpopupDetails> objpopupdetails = new List<CustomerpopupDetails>();
        //    CustomerpopupDetails objOrderDetails = new CustomerpopupDetails();
        //    List<StoreCampaignKeyInsight> objkeyinsight = new List<StoreCampaignKeyInsight>();
        //    List<StoreCampaignKeyInsight> objkeyinsightDetails = new List<StoreCampaignKeyInsight>();
        //    List<StoreCampaignRecommended> objrecommended = new List<StoreCampaignRecommended>();
        //    List<StoreCampaignRecommended> objrecommendedDetails = new List<StoreCampaignRecommended>();
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        CustomerpopupDetails popupDetail = new CustomerpopupDetails();
        //        popupDetail.name = "Dharmendra";
        //        popupDetail.mobileNumber = "9923165567";
        //        popupDetail.tiername = "";
        //        popupDetail.lifeTimeValue = "4568.45";
        //        popupDetail.visitCount = "6";
        //        obj.useratvdetails = popupDetail;
        //        StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight();

        //        KeyInsight.mobileNumber = "9923165567";
        //        KeyInsight.insightText = "Lorem Ipsum";
        //        obj.campaignkeyinsight = KeyInsight;

        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignRecommendedList", conn)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
        //        cmd.Parameters.AddWithValue("@User_ID", userID);
        //        cmd.Parameters.AddWithValue("@mobile_Number", mobileNumber);
        //        cmd.Parameters.AddWithValue("@program_Code", programCode);

        //        MySqlDataAdapter da = new MySqlDataAdapter
        //        {
        //            SelectCommand = cmd
        //        };
        //        da.Fill(ds);
        //        if (ds != null && ds.Tables[0] != null)
        //        {
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended();
        //                RecommendedDetail.mobileNumber = ds.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]);
        //                RecommendedDetail.itemCode = ds.Tables[0].Rows[i]["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
        //                RecommendedDetail.category = ds.Tables[0].Rows[i]["Category"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
        //                RecommendedDetail.subCategory = ds.Tables[0].Rows[i]["SubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategory"]);
        //                RecommendedDetail.brand = ds.Tables[0].Rows[i]["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Brand"]);
        //                RecommendedDetail.color = ds.Tables[0].Rows[i]["Color"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Color"]);
        //                RecommendedDetail.size = ds.Tables[0].Rows[i]["Size"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Size"]);
        //                RecommendedDetail.price = ds.Tables[0].Rows[i]["Price"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Price"]);
        //                RecommendedDetail.url = ds.Tables[0].Rows[i]["Url"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Url"]);
        //                RecommendedDetail.imageURL = ds.Tables[0].Rows[i]["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageURL"]);
        //                objrecommended.Add(RecommendedDetail);
        //                obj.campaignrecommended = objrecommended;
        //            }
        //        }
        //        //StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended();

        //        //RecommendedDetail.mobileNumber = "9923165567";
        //        //RecommendedDetail.itemCode = "F808920000";
        //        //RecommendedDetail.category = "Shoes";
        //        //RecommendedDetail.subCategory = "Casual";
        //        //RecommendedDetail.brand = "";
        //        //RecommendedDetail.color = "Black";
        //        //RecommendedDetail.size = "7";
        //        //RecommendedDetail.price = "3499";
        //        //RecommendedDetail.url = "https://www.bata.in/bataindia/e-124_c-262/blacks-and-browns/men.html";
        //        //RecommendedDetail.imageURL = "https://img2.bata.in/0/images/product/854-6523_300x300_1.jpeg";
        //        //objrecommended.Add(RecommendedDetail);
        //        //obj.campaignrecommended = objrecommended;

        //        StoreCampaignLastTransactionDetails LastTransactionDetails = new StoreCampaignLastTransactionDetails();

        //        LastTransactionDetails.billNo = "BB332398";
        //        LastTransactionDetails.billDate = "12 Jan 2020";
        //        LastTransactionDetails.storeName = "Bata-Rajouri Garden";
        //        LastTransactionDetails.amount = "1,499";
        //        LastTransactionDetails.itemDetails = "";
        //        obj.lasttransactiondetails = LastTransactionDetails;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return obj;
        //}

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
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
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


        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int InsertApiResponseData(StoresCampaignStatusResponse obj, int userID)
        {
            int result = 0;
            int TransactionID = 0;
            DataSet ds = new DataSet();
            try
            {
                conn.Close();
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand("SP_HScreateUserAtvDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_name", obj.useratvdetails.name);
                cmd.Parameters.AddWithValue("@_mobileNumber", obj.useratvdetails.mobileNumber);
                cmd.Parameters.AddWithValue("@_tiername", obj.useratvdetails.tiername);
                cmd.Parameters.AddWithValue("@_visitCount", obj.useratvdetails.visitCount);
                cmd.Parameters.AddWithValue("@_lifeTimeValue", obj.useratvdetails.lifeTimeValue);
                cmd.Parameters.AddWithValue("@_UserID", userID);
                result = Convert.ToInt32(cmd.ExecuteNonQuery());

                MySqlCommand cmd1 = new MySqlCommand("SP_HScreateKeyInsight", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@_mobileNumber", obj.campaignkeyinsight.mobileNumber);
                cmd1.Parameters.AddWithValue("@_insightText", obj.campaignkeyinsight.insightText);
                cmd1.Parameters.AddWithValue("@_UserID", userID);
                result = Convert.ToInt32(cmd1.ExecuteNonQuery());

                MySqlCommand cmd2 = new MySqlCommand("SP_HScreateLastTransactionDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd2.Parameters.AddWithValue("@_billNo", obj.lasttransactiondetails.billNo);
                cmd2.Parameters.AddWithValue("@_billDate", obj.lasttransactiondetails.billDate);
                cmd2.Parameters.AddWithValue("@_storeName", obj.lasttransactiondetails.storeName);
                cmd2.Parameters.AddWithValue("@_amount", obj.lasttransactiondetails.amount);
                cmd2.Parameters.AddWithValue("@_UserID", userID);
               // cmd2.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd2;

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        TransactionID = ds.Tables[0].Rows[0]["TransactionID"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["TransactionID"]);
                    }

                }

                if (obj.lasttransactiondetails.itemDetails.Count>0)
                {
                    for(int i=0;i< obj.lasttransactiondetails.itemDetails.Count;i++)
                    {
                        MySqlCommand cmd3 = new MySqlCommand("SP_HScreateLastTransactionItemDetails", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd3.Parameters.AddWithValue("@_mobileNo", obj.lasttransactiondetails.itemDetails[i].mobileNo);
                        cmd3.Parameters.AddWithValue("@_article", obj.lasttransactiondetails.itemDetails[i].article);
                        cmd3.Parameters.AddWithValue("@_quantity", obj.lasttransactiondetails.itemDetails[i].quantity);
                        cmd3.Parameters.AddWithValue("@_amount", obj.lasttransactiondetails.itemDetails[i].amount);
                        cmd3.Parameters.AddWithValue("@_UserID", userID);
                        cmd3.Parameters.AddWithValue("@_TransactionID", TransactionID);
                        result = Convert.ToInt32(cmd3.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;
        }

    }

}
