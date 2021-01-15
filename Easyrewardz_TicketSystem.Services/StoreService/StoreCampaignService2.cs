using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class StoreCampaignService : IStoreCampaign
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
       // CustomResponse ApiResponse = null;
        string apiURL = string.Empty;
        string apiURLGetUserATVDetails = string.Empty;
        ChatbotBellHttpClientService APICall = null;

        public StoreCampaignService(string _connectionString, ChatbotBellHttpClientService _APICall = null)
        {
            conn.ConnectionString = _connectionString;
            APICall = _APICall;
        }
        #endregion

        /// <summary>
        /// Get Store Task By Ticket
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<StoreCampaignModel2>> GetStoreCampaign(int tenantID, int userID, string campaignName, string statusId)
        {
            List<StoreCampaignModel2> lstCampaign = new List<StoreCampaignModel2>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                  await  conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignList", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@User_ID", userID);
                    cmd.Parameters.AddWithValue("@_campaignName", campaignName);
                    cmd.Parameters.AddWithValue("@_statusId", statusId);
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            StoreCampaignModel2 storecampaign = new StoreCampaignModel2
                            {
                                CampaignID = Convert.ToInt32(dr["ID"]),
                                CampaignName = dr["CampaignName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignName"]),
                                CustomerCount = dr["CustomerCount"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerCount"]),
                                ChatbotScript = dr["ChatbotScript"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatbotScript"]),
                                SmsScript = dr["SmsScript"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SmsScript"]),
                                CampaingPeriod = dr["CampaingPeriod"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaingPeriod"]),
                                SmsFlag = Convert.ToBoolean(dr["SmsFlag"]),
                                EmailFlag = Convert.ToBoolean(dr["EmailFlag"]),
                                MessengerFlag = Convert.ToBoolean(dr["MessengerFlag"]),
                                BotFlag = Convert.ToBoolean(dr["BotFlag"]),
                                Status = Convert.ToString((StoreCampaignStatus)Convert.ToInt32(dr["Status"])),
                                MaxClickAllowed = dr["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxClickAllowed"]),
                                StoreCode = dr["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreCode"]),
                                CampaignCode = dr["CampaignCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignCode"])
                            };
                            lstCampaign.Add(storecampaign);
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
            return lstCampaign;
        }

        #region Campaign Customer Pop up  APIs

        /// <summary>
        ///Get Customer popup Details List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public async Task<StoresCampaignStatusResponse> GetCustomerpopupDetailsList(string mobileNumber, string programCode, string campaignID, int tenantID, int userID, string ClientAPIURL)
        {
            StoresCampaignStatusResponse obj = new StoresCampaignStatusResponse();
            StoreCampaignSearchOrder objOrderSearch = new StoreCampaignSearchOrder();
            CustomerpopupDetails objpopupDetails = new CustomerpopupDetails();
          // StoreCampaignLastTransactionDetails objLastTransactionDetails = new StoreCampaignLastTransactionDetails();
            //StoreCampaignKeyInsight objkeyinsight = new StoreCampaignKeyInsight();
           // List<StoreCampaignRecommended> objrecommended = new List<StoreCampaignRecommended>();
           // List<StoreCampaignRecommended> objrecommendedDetails = new List<StoreCampaignRecommended>();

            string apiReq = string.Empty;
            DataSet ds = new DataSet();

                #region ATVDetails 

            try
            {
                objOrderSearch.mobileNumber = mobileNumber;
                objOrderSearch.programCode = programCode;
                try
                {
                    apiReq = JsonConvert.SerializeObject(objOrderSearch);
                   // string apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetUserATVDetails", apiReq);
                    string apiResponse =await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetUserATVDetails", apiReq);

                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        if (apiResponse != null)
                        {
                            objpopupDetails = JsonConvert.DeserializeObject<CustomerpopupDetails>(apiResponse);

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


                #endregion

                #region Commented code,PS: moved to seperate APIS 
                /*
                #region KeyInsight 

                try
                {
                    string apiResponse = string.Empty;
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
                                    insightText = ""
                                };
                                obj.campaignkeyinsight = KeyInsight;
                            }
                        }
                        else
                        {
                            StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight
                            {
                                mobileNumber = "",
                                insightText = ""
                            };
                            obj.campaignkeyinsight = KeyInsight;
                        }
                    }
                    else
                    {
                        StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight
                        {
                            mobileNumber = "",
                            insightText = ""
                        };
                        obj.campaignkeyinsight = KeyInsight;
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
                    bool lifeTimeValuehaszero = false;
                    bool visitCounthaszero = false;

                    if (!string.IsNullOrEmpty(obj.useratvdetails.lifeTimeValue))
                    {
                        if(decimal.TryParse(obj.useratvdetails.lifeTimeValue, out decimal result))
                        {
                            if(Convert.ToDouble(obj.useratvdetails.lifeTimeValue).Equals(0))
                            {
                                lifeTimeValuehaszero = true;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(obj.useratvdetails.visitCount))
                    {
                        if (decimal.TryParse(obj.useratvdetails.visitCount, out decimal result))
                        {
                            if (Convert.ToDouble(obj.useratvdetails.visitCount).Equals(0))
                            {
                                visitCounthaszero = true;
                            }
                        }
                    }

                    if ((string.IsNullOrEmpty(obj.useratvdetails.lifeTimeValue) && string.IsNullOrEmpty(obj.useratvdetails.visitCount) || (lifeTimeValuehaszero && visitCounthaszero)))
                    {
                        if (obj.campaignkeyinsight != null)
                        {
                            if (string.IsNullOrEmpty(obj.campaignkeyinsight.insightText))
                            {
                                StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight
                                {
                                    mobileNumber = mobileNumber,
                                    insightText = await GetKeyInsightAsChatBot(mobileNumber, programCode, campaignID, tenantID, userID, ClientAPIURL),
                                    ShowKeyInsights = false
                                };
                                obj.campaignkeyinsight = KeyInsight;
                            }
                        }
                    }
                }

                #endregion

                #region campaign recommendation list
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
                            StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended
                            {
                                mobileNumber = ds.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]),
                                itemCode = ds.Tables[0].Rows[i]["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]),
                                category = ds.Tables[0].Rows[i]["Category"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Category"]),
                                subCategory = ds.Tables[0].Rows[i]["SubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategory"]),
                                brand = ds.Tables[0].Rows[i]["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Brand"]),
                                color = ds.Tables[0].Rows[i]["Color"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Color"]),
                                size = ds.Tables[0].Rows[i]["Size"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Size"]),
                                price = ds.Tables[0].Rows[i]["Price"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Price"]),
                                url = ds.Tables[0].Rows[i]["Url"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Url"]),
                                imageURL = ds.Tables[0].Rows[i]["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageURL"])
                            };
                            objrecommended.Add(RecommendedDetail);
                        }
                        obj.campaignrecommended = objrecommended;
                    }
                    else
                    {
                        StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended
                        {
                            mobileNumber = "",
                            itemCode = "",
                            category = "",
                            subCategory = "",
                            brand = "",
                            color = "",
                            size = "",
                            price = "",
                            url = "",
                            imageURL = ""
                        };
                        objrecommended.Add(RecommendedDetail);
                        obj.campaignrecommended = objrecommended;
                    }
                }
                catch (Exception)
                {
                    StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended
                    {
                        mobileNumber = "",
                        itemCode = "",
                        category = "",
                        subCategory = "",
                        brand = "",
                        color = "",
                        size = "",
                        price = "",
                        url = "",
                        imageURL = ""
                    };
                    objrecommended.Add(RecommendedDetail);
                    obj.campaignrecommended = objrecommended;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }

                #endregion

                #region Last Transaction Details

                try
                {
                    string apiResponse = string.Empty;
                    apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetLastTransactionDetails", apiReq);

                    if (!string.IsNullOrEmpty(apiResponse))
                    {

                        if (apiResponse != null)
                        {
                            objLastTransactionDetails = JsonConvert.DeserializeObject<StoreCampaignLastTransactionDetails>(apiResponse);

                            if (objLastTransactionDetails != null)
                            {
                                
                                obj.lasttransactiondetails = objLastTransactionDetails;
                            }
                            else
                            {
                                StoreCampaignLastTransactionDetails LastTransactionDetails = new StoreCampaignLastTransactionDetails
                                {
                                    billNo = "",
                                    billDate = "",
                                    storeName = "",
                                    amount = ""
                                };
                                obj.lasttransactiondetails = LastTransactionDetails;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    if (obj.lasttransactiondetails == null)
                    {
                        StoreCampaignLastTransactionDetails LastTransactionDetails = new StoreCampaignLastTransactionDetails
                        {
                            billNo = "",
                            billDate = "",
                            storeName = "",
                            amount = ""
                        };
                        obj.lasttransactiondetails = LastTransactionDetails;
                    }
                }

                #endregion

                #region Share Campaign Via Setting

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
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }

                #endregion
                */
                #endregion
            }
            catch (Exception)
            {

            }
            return obj;
        }

        /// <summary>
        /// Get Store Campaign Key Insights
        /// </summary>
        /// <param name="lifetimeValue"></param>
        /// <param name="VisitCount"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="campaignID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<StoreCampaignKeyInsight> GetStoreCampaignKeyInsight(string lifetimeValue, string VisitCount, string mobileNumber, string programCode, string campaignID, int tenantID, int userID, string ClientAPIURL)
        {
            StoreCampaignKeyInsight objkeyinsight = new StoreCampaignKeyInsight();
            string apiResponse = string.Empty;
            string apiReq = string.Empty;
            StoreCampaignSearchOrder objOrderSearch = new StoreCampaignSearchOrder();
            bool lifeTimeValuehaszero = false;
            bool visitCounthaszero = false;
            try
            {


                try
                {

                    objOrderSearch.mobileNumber = mobileNumber;
                    objOrderSearch.programCode = programCode;
                    apiReq = JsonConvert.SerializeObject(objOrderSearch);

                    apiResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetKeyInsight", apiReq);

                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        if (!string.IsNullOrEmpty(apiResponse.Replace("[]", "")))
                        {
                            objkeyinsight = JsonConvert.DeserializeObject<StoreCampaignKeyInsight>(((apiResponse)));

                            if (objkeyinsight == null)
                            {


                                objkeyinsight.mobileNumber = "";
                                objkeyinsight.insightText = "";

                            }
                        }
                        else
                        {
                            objkeyinsight.mobileNumber = "";
                            objkeyinsight.insightText = "";

                        }
                    }
                    else
                    {
                        objkeyinsight.mobileNumber = "";
                        objkeyinsight.insightText = "";

                    }
                }
                catch (Exception)
                {
                    if (objkeyinsight == null)
                    {
                        objkeyinsight.mobileNumber = "";
                        objkeyinsight.insightText = "";
                    }
                }


                    if (!string.IsNullOrEmpty(lifetimeValue))
                    {
                        if (decimal.TryParse(lifetimeValue, out decimal result))
                        {
                            if (Convert.ToDouble(lifetimeValue).Equals(0))
                            {
                                lifeTimeValuehaszero = true;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(VisitCount))
                    {
                        if (decimal.TryParse(VisitCount, out decimal result))
                        {
                            if (Convert.ToDouble(VisitCount).Equals(0))
                            {
                                visitCounthaszero = true;
                            }
                        }
                    }

                    if ((string.IsNullOrEmpty(VisitCount) && string.IsNullOrEmpty(VisitCount) || (lifeTimeValuehaszero && visitCounthaszero)))
                    {
                        if (objkeyinsight != null)
                        {
                            if (string.IsNullOrEmpty(objkeyinsight.insightText))
                            {

                                objkeyinsight.mobileNumber = mobileNumber;
                                objkeyinsight.insightText = await GetKeyInsightAsChatBot(mobileNumber, programCode, campaignID, tenantID, userID, ClientAPIURL);
                                objkeyinsight.ShowKeyInsights = false;

                            }
                        }
                    }
                
            }
            catch (Exception)
            {
                objkeyinsight.mobileNumber = "";
                objkeyinsight.insightText = "";
            }

                       
            return objkeyinsight;
        }

        /// <summary>
        /// Get Campaign Recommendation List
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async  Task<List<StoreCampaignRecommended>> GetCampaignRecommendationList( string mobileNumber, string programCode, int tenantID, int userID)
        {

            List<StoreCampaignRecommended> objrecommendedDetails = new List<StoreCampaignRecommended>();
            DataTable schemaTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {

                    MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignRecommendedList", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@User_ID", userID);
                    cmd.Parameters.AddWithValue("@mobile_Number", mobileNumber);
                    cmd.Parameters.AddWithValue("@program_Code", programCode);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {

                        if (dr.HasRows)
                        {
                            schemaTable.Load(dr);

                            foreach (DataRow dtr in schemaTable.Rows)
                            {
                                StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended
                                {
                                    mobileNumber = dtr["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["MobileNumber"]),
                                    itemCode = dtr["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["ItemCode"]),
                                    category = dtr["Category"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["Category"]),
                                    subCategory = dtr["SubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["SubCategory"]),
                                    brand = dtr["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["Brand"]),
                                    color = dtr["Color"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["Color"]),
                                    size = dtr["Size"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["Size"]),
                                    price = dtr["Price"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["Price"]),
                                    url = dtr["Url"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["Url"]),
                                    imageURL = dtr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dtr["ImageURL"])
                                };
                                objrecommendedDetails.Add(RecommendedDetail);
                            }

                        }
                        else
                        {
                            objrecommendedDetails.Add(new StoreCampaignRecommended
                            {
                                mobileNumber = "",
                                itemCode = "",
                                category = "",
                                subCategory = "",
                                brand = "",
                                color = "",
                                size = "",
                                price = "",
                                url = "",
                                imageURL = ""
                            });

                        }

                    }

                }

            }
            catch (Exception)
            {

                objrecommendedDetails.Add(new StoreCampaignRecommended
                {
                    mobileNumber = "",
                    itemCode = "",
                    category = "",
                    subCategory = "",
                    brand = "",
                    color = "",
                    size = "",
                    price = "",
                    url = "",
                    imageURL = ""
                });
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }

            return objrecommendedDetails;
        }

        /// <summary>
        /// Get Store Campaign Last Transaction Details
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<StoreCampaignLastTransactionDetails> GetStoreCampaignLastTransactionDetails( string mobileNumber, string programCode, string ClientAPIURL)
        {
            StoreCampaignLastTransactionDetails objLastTransactionDetails = new StoreCampaignLastTransactionDetails();
            StoreCampaignSearchOrder objOrderSearch = new StoreCampaignSearchOrder();
            string apiReq = string.Empty;
            string apiResponse = string.Empty;
            try
            {

                objOrderSearch.mobileNumber = mobileNumber;
                objOrderSearch.programCode = programCode;
                apiReq = JsonConvert.SerializeObject(objOrderSearch);
                apiResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetLastTransactionDetails", apiReq);

                if (!string.IsNullOrEmpty(apiResponse))
                {

                        objLastTransactionDetails = JsonConvert.DeserializeObject<StoreCampaignLastTransactionDetails>(apiResponse);

                        if (objLastTransactionDetails == null)
                        {
                            objLastTransactionDetails.billNo = "";
                            objLastTransactionDetails.billDate = "";
                            objLastTransactionDetails.storeName = "";
                            objLastTransactionDetails.amount = "";
                        }

                }
            }
            catch (Exception)
            {
                objLastTransactionDetails.billNo = "";
                objLastTransactionDetails.billDate = "";
                objLastTransactionDetails.storeName = "";
                objLastTransactionDetails.amount = "";
            }

            return objLastTransactionDetails;

        }

        /// <summary>
        /// Get Share Campaign Via Setting
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<ShareCampaignViaSettingModal> GetShareCampaignViaSetting( string mobileNumber, string programCode, int tenantID, int userID)
        {
            ShareCampaignViaSettingModal shareCampaignViaSettingModal = new ShareCampaignViaSettingModal();
            MySqlCommand cmd = null;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_HSGetShareCampaignViaSetting", conn);
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@User_ID", userID);
                    cmd.Parameters.AddWithValue("@mobile_Number", mobileNumber);
                    cmd.Parameters.AddWithValue("@program_Code", programCode);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        if(dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                shareCampaignViaSettingModal.CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]);
                                shareCampaignViaSettingModal.CustomerNumber = dr["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerNumber"]);
                                shareCampaignViaSettingModal.SmsFlag = dr["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["SmsFlag"]);
                                shareCampaignViaSettingModal.SmsClickCount = dr["SmsClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SmsClickCount"]);
                                shareCampaignViaSettingModal.SmsClickable = dr["SmsClickable"] == DBNull.Value ? false : Convert.ToBoolean(dr["SmsClickable"]);
                                shareCampaignViaSettingModal.EmailFlag = dr["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["EmailFlag"]);
                                shareCampaignViaSettingModal.EmailClickCount = dr["EmailClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EmailClickCount"]);
                                shareCampaignViaSettingModal.EmailClickable = dr["EmailClickable"] == DBNull.Value ? false : Convert.ToBoolean(dr["EmailClickable"]);
                                shareCampaignViaSettingModal.MessengerFlag = dr["MessengerFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["MessengerFlag"]);
                                shareCampaignViaSettingModal.MessengerClickCount = dr["MessengerClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MessengerClickCount"]);
                                shareCampaignViaSettingModal.MessengerClickable = dr["MessengerClickable"] == DBNull.Value ? false : Convert.ToBoolean(dr["MessengerClickable"]);
                                shareCampaignViaSettingModal.BotFlag = dr["BotFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["BotFlag"]);
                                shareCampaignViaSettingModal.BotClickCount = dr["BotClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BotClickCount"]);
                                shareCampaignViaSettingModal.BotClickable = dr["BotClickable"] == DBNull.Value ? false : Convert.ToBoolean(dr["BotClickable"]);
                                shareCampaignViaSettingModal.MaxClickAllowed = dr["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxClickAllowed"]);
                            }
                        }
                        else
                        {
                            shareCampaignViaSettingModal.CustomerName = "";
                            shareCampaignViaSettingModal.CustomerNumber = "";
                            shareCampaignViaSettingModal.SmsFlag = false;
                            shareCampaignViaSettingModal.SmsClickCount = 0;
                            shareCampaignViaSettingModal.SmsClickable = false;
                            shareCampaignViaSettingModal.EmailFlag = false;
                            shareCampaignViaSettingModal.EmailClickCount = 0;
                            shareCampaignViaSettingModal.EmailClickable = false;
                            shareCampaignViaSettingModal.MessengerFlag = false;
                            shareCampaignViaSettingModal.MessengerClickCount = 0;
                            shareCampaignViaSettingModal.MessengerClickable = false;
                            shareCampaignViaSettingModal.BotFlag = false;
                            shareCampaignViaSettingModal.BotClickCount = 0;
                            shareCampaignViaSettingModal.BotClickable = false;
                            shareCampaignViaSettingModal.MaxClickAllowed = 0;
                        }

                       
                    }
                }

            }
            catch(Exception )
            {
                shareCampaignViaSettingModal.CustomerName = "";
                shareCampaignViaSettingModal.CustomerNumber = "";
                shareCampaignViaSettingModal.SmsFlag = false;
                shareCampaignViaSettingModal.SmsClickCount = 0;
                shareCampaignViaSettingModal.SmsClickable = false;
                shareCampaignViaSettingModal.EmailFlag = false;
                shareCampaignViaSettingModal.EmailClickCount = 0;
                shareCampaignViaSettingModal.EmailClickable = false;
                shareCampaignViaSettingModal.MessengerFlag = false;
                shareCampaignViaSettingModal.MessengerClickCount = 0;
                shareCampaignViaSettingModal.MessengerClickable = false;
                shareCampaignViaSettingModal.BotFlag = false;
                shareCampaignViaSettingModal.BotClickCount = 0;
                shareCampaignViaSettingModal.BotClickable = false;
                shareCampaignViaSettingModal.MaxClickAllowed = 0;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return shareCampaignViaSettingModal;
        }

        #endregion

        /// <summary>
        /// Get Key Insight As ChatBot
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<string> GetKeyInsightAsChatBot(string mobileNumber, string programCode, string campaignID, int tenantID, int userID, string ClientAPIURL)
        {
            string obj = "";
            try
            {
                GetWhatsappMessageDetailsResponse getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
                List<GetWhatsappMessageDetailsResponse> getWhatsappMessageDetailsResponseList = new List<GetWhatsappMessageDetailsResponse>();
                string strpostionNumber = "";
                string strpostionName = "";
                string whatsapptemplate = await GetWhatsupTemplateName(tenantID, userID, "Campaign");
                try
                {
                    GetWhatsappMessageDetailsModal getWhatsappMessageDetailsModal = new GetWhatsappMessageDetailsModal()
                    {
                        ProgramCode = programCode
                    };

                    string apiBotReq = JsonConvert.SerializeObject(getWhatsappMessageDetailsModal);
                   // string apiBotResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetWhatsappMessageDetails", apiBotReq);
                    string apiBotResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetWhatsappMessageDetails", apiBotReq);

                    //if (!string.IsNullOrEmpty(apiBotResponse.Replace("[]", "").Replace("[", "").Replace("]", "")))
                    //{
                    //    getWhatsappMessageDetailsResponse = JsonConvert.DeserializeObject<GetWhatsappMessageDetailsResponse>(apiBotResponse.Replace("[", "").Replace("]", ""));
                    //}

                    if (!string.IsNullOrEmpty(apiBotResponse.Replace("[]", "").Replace("[", "").Replace("]", "")))
                    {
                        getWhatsappMessageDetailsResponseList = JsonConvert.DeserializeObject<List<GetWhatsappMessageDetailsResponse>>(apiBotResponse);
                    }

                    if (getWhatsappMessageDetailsResponseList != null)
                    {
                        if (getWhatsappMessageDetailsResponseList.Count > 0)
                        {
                            getWhatsappMessageDetailsResponse = getWhatsappMessageDetailsResponseList.Where(x => x.TemplateName == whatsapptemplate).First();
                        }
                    }

                    if (getWhatsappMessageDetailsResponse != null)
                    {
                        if (getWhatsappMessageDetailsResponse.Remarks != null)
                        {
                            string ObjRemark = getWhatsappMessageDetailsResponse.Remarks.Replace("\r\n", "");
                            string[] ObjSplitComma = ObjRemark.Split(',');

                            if (ObjSplitComma.Length > 0)
                            {
                                for (int i = 0; i < ObjSplitComma.Length; i++)
                                {
                                    strpostionNumber += ObjSplitComma[i].Split('-')[0].Trim().Replace("{", "").Replace("}", "") + ",";
                                    strpostionName += ObjSplitComma[i].Split('-')[1].Trim() + ",";
                                }
                            }
                            strpostionNumber = strpostionNumber.TrimEnd(',');
                            strpostionName = strpostionName.TrimEnd(',');
                        }
                    }
                }
                catch (Exception)
                {
                    getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
                }

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetKeyInsightAsChatBot", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_ProgramCode", programCode);
                    cmd.Parameters.AddWithValue("@_MobileNumber", mobileNumber);
                    cmd.Parameters.AddWithValue("@_CampaignID", campaignID);
                    cmd.Parameters.AddWithValue("@_strpostionNumber", strpostionNumber);
                    cmd.Parameters.AddWithValue("@_strpostionName", strpostionName);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            obj = dr["Message"] == DBNull.Value ? String.Empty : Convert.ToString(dr["Message"]);
                        }
                    }
                }
            }
            catch(Exception)
            {
                obj = "";
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
        /// Get Store Task By Ticket
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<StoreCampaignLogo>> GetCampaignDetailsLogo(int tenantID, int userID)
        {
            List<StoreCampaignLogo> lstCampaignLogo = new List<StoreCampaignLogo>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                   await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignLogoList", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@User_ID", userID);
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            StoreCampaignLogo storecampaign = new StoreCampaignLogo
                            {
                                Id = Convert.ToInt32(dr["IdCampaignLogo"]),
                                name = dr["Name"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Name"]),
                            };
                            lstCampaignLogo.Add(storecampaign);
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
            return lstCampaignLogo;
        }


        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<int> InsertApiResponseData(StoresCampaignStatusResponse obj, int userID)
        {
            int result = 0;
            int TransactionID = 0;
            DataSet ds = new DataSet();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    result = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());

                    MySqlCommand cmd1 = new MySqlCommand("SP_HScreateKeyInsight", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd1.Parameters.AddWithValue("@_mobileNumber", obj.campaignkeyinsight.mobileNumber);
                    cmd1.Parameters.AddWithValue("@_insightText", obj.campaignkeyinsight.insightText);
                    cmd1.Parameters.AddWithValue("@_UserID", userID);
                    result = Convert.ToInt32(await cmd1.ExecuteNonQueryAsync());

                    MySqlCommand cmd2 = new MySqlCommand("SP_HScreateLastTransactionDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd2.Parameters.AddWithValue("@_billNo", obj.lasttransactiondetails.billNo);
                    cmd2.Parameters.AddWithValue("@_billDate", obj.lasttransactiondetails.billDate);
                    cmd2.Parameters.AddWithValue("@_storeName", obj.lasttransactiondetails.storeName);
                    cmd2.Parameters.AddWithValue("@_amount", obj.lasttransactiondetails.amount);
                    cmd2.Parameters.AddWithValue("@_UserID", userID);

                    using (var dr = await cmd2.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            TransactionID = dr["TransactionID"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["TransactionID"]);
                        }
                    }

                    if (obj.lasttransactiondetails.itemDetails.Count > 0)
                    {
                        for (int i = 0; i < obj.lasttransactiondetails.itemDetails.Count; i++)
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
                            result = Convert.ToInt32(await cmd3.ExecuteNonQueryAsync());
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
            return result;
        }

    }

}
