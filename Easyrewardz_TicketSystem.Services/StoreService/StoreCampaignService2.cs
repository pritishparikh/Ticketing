using Easyrewardz_TicketSystem.CustomModel;
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
                            SmsFlag = Convert.ToBoolean(ds.Tables[0].Rows[i]["SmsFlag"]),
                            EmailFlag = Convert.ToBoolean(ds.Tables[0].Rows[i]["EmailFlag"]),
                            MessengerFlag = Convert.ToBoolean(ds.Tables[0].Rows[i]["MessengerFlag"]),
                            BotFlag = Convert.ToBoolean(ds.Tables[0].Rows[i]["BotFlag"]),
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


        ///// <summary>
        /////Get Customer popup Details List
        ///// </summary>
        ///// <param name="tenantID"></param>
        ///// <param name="userID"></param>
        ///// <param name="mobileNumber"></param>
        ///// <param name="programCode"></param>
        ///// <returns></returns>
        //public CampaignStatusResponse1 GetCustomerpopupDetailsList(string mobileNumber, string programCode, int tenantID, int userID)
        //{
        //    CampaignStatusResponse1 obj = new CampaignStatusResponse1();
        //    StoreCampaignSearchOrder objOrderSearch = new StoreCampaignSearchOrder();
        //    List<CustomerpopupDetails> lstpopupDetails = new List<CustomerpopupDetails>();
        //    CustomerpopupDetails objpopupDetails = new CustomerpopupDetails();
        //    StoreCampaignKeyInsight objkeyinsight = new StoreCampaignKeyInsight();
        //    List<StoreCampaignKeyInsight> lstkeyinsightDetails = new List<StoreCampaignKeyInsight>();
        //    List<StoreCampaignRecommended> objrecommended = new List<StoreCampaignRecommended>();
        //    List<StoreCampaignRecommended> objrecommendedDetails = new List<StoreCampaignRecommended>();


        //    try
        //    {
        //        apisecurityToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJQcm9ncmFtQ29kZSI6IkJhdGEiLCJVc2VySUQiOiIzIiwiQXBwSUQiOiI3IiwiRGF5IjoiMjgiLCJNb250aCI6IjMiLCJZZWFyIjoiMjAyMSIsIlJvbGUiOiJBZG1pbiIsImlzcyI6IkF1dGhTZWN1cml0eUlzc3VlciIsImF1ZCI6IkF1dGhTZWN1cml0eUF1ZGllbmNlIn0.0XeF7V5LWfQn0NlSlG7Rb-Qq1hUCtUYRDg6dMGIMvg0";
        //        objOrderSearch.mobileNumber = mobileNumber;
        //        objOrderSearch.programCode = programCode;
        //        objOrderSearch.securityToken = apisecurityToken;


        //        string apiReq = JsonConvert.SerializeObject(objOrderSearch);
        //        apiResponse = CommonService.SendApiRequest("http://chatbotbellapiqa.ercx.co/api/BellChatBotIntegration/GetUserATVDetails", apiReq);

        //        if (!string.IsNullOrEmpty(apiResponse))
        //        {
        //            ApiResponse = JsonConvert.DeserializeObject<CustomResponse>(apiResponse);

        //            if (apiResponse != null)
        //            {
        //                objpopupDetails = JsonConvert.DeserializeObject<CustomerpopupDetails>(((apiResponse)));

        //                if (objpopupDetails != null)
        //                {
        //                    CustomerpopupDetails popupDetail = new CustomerpopupDetails();
        //                    popupDetail.name = objpopupDetails.name;
        //                    popupDetail.mobileNumber = objpopupDetails.mobileNumber;
        //                    popupDetail.tiername = objpopupDetails.tiername;
        //                    popupDetail.lifeTimeValue = objpopupDetails.lifeTimeValue;
        //                    popupDetail.visitCount = objpopupDetails.visitCount;
        //                    lstpopupDetails.Add(popupDetail);

        //                }

        //            }

        //        }

        //        apiResponse = CommonService.SendApiRequest("http://chatbotbellapiqa.ercx.co/api/BellChatBotIntegration/GetKeyInsight", apiReq);

        //        if (!string.IsNullOrEmpty(apiResponse))
        //        {
        //            ApiResponse = JsonConvert.DeserializeObject<CustomResponse>(apiResponse);

        //            if (ApiResponse != null)
        //            {
        //                objkeyinsight = JsonConvert.DeserializeObject<StoreCampaignKeyInsight>(Convert.ToString((ApiResponse.Responce)));

        //                if (objkeyinsight != null)
        //                {
        //                    StoreCampaignKeyInsight popupDetail = new StoreCampaignKeyInsight();

        //                    popupDetail.mobileNumber = objkeyinsight.mobileNumber;
        //                    popupDetail.insightText = objkeyinsight.insightText;
        //                    lstkeyinsightDetails.Add(popupDetail);

        //                }

        //            }

        //        }

        //        apiResponse = CommonService.SendApiRequest("http://chatbotbellapiqa.ercx.co/api/BellChatBotIntegration/GetRecommendedList", apiReq);

        //        if (!string.IsNullOrEmpty(apiResponse))
        //        {
        //            ApiResponse = JsonConvert.DeserializeObject<CustomResponse>(apiResponse);

        //            if (ApiResponse != null)
        //            {
        //                objrecommendedDetails = JsonConvert.DeserializeObject<List<StoreCampaignRecommended>>(Convert.ToString((ApiResponse.Responce)));

        //                if (objrecommendedDetails != null)
        //                {
        //                    if (objrecommendedDetails.Count > 0)
        //                    {
        //                        StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended();

        //                        RecommendedDetail.mobileNumber = "1234567890";
        //                        RecommendedDetail.itemCode = "For Test";
        //                        RecommendedDetail.category = "For Test";
        //                        RecommendedDetail.subCategory = "For Test";
        //                        RecommendedDetail.brand = "For Test";
        //                        RecommendedDetail.color = "For Test";
        //                        RecommendedDetail.size = "For Test";
        //                        RecommendedDetail.price = "For Test";
        //                        RecommendedDetail.url = "For Test";
        //                        RecommendedDetail.imageURL = "For Test";
        //                        objrecommended.Add(RecommendedDetail);
        //                    }
        //                }

        //            }

        //        }



        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return obj;
        //}

        /// <summary>
        ///Get Customer popup Details List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public CampaignStatusResponse1 GetCustomerpopupDetailsList(string mobileNumber, string programCode, int tenantID, int userID)
        {
            CampaignStatusResponse1 obj = new CampaignStatusResponse1();
            StoreCampaignSearchOrder objOrderSearch = new StoreCampaignSearchOrder();
            List<CustomerpopupDetails> objpopupdetails = new List<CustomerpopupDetails>();
            CustomerpopupDetails objOrderDetails = new CustomerpopupDetails();
            List<StoreCampaignKeyInsight> objkeyinsight = new List<StoreCampaignKeyInsight>();
            List<StoreCampaignKeyInsight> objkeyinsightDetails = new List<StoreCampaignKeyInsight>();
            List<StoreCampaignRecommended> objrecommended = new List<StoreCampaignRecommended>();
            List<StoreCampaignRecommended> objrecommendedDetails = new List<StoreCampaignRecommended>();

            try
            {
                CustomerpopupDetails popupDetail = new CustomerpopupDetails();
                popupDetail.name = "Dharmendra";
                popupDetail.mobileNumber = "9923165567";
                popupDetail.tiername = "";
                popupDetail.lifeTimeValue = "4568.45";
                popupDetail.visitCount = "6";
               // objpopupdetails.Add(popupDetail);
                obj.useratvdetails = popupDetail;
                StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight();

                KeyInsight.mobileNumber = "9923165567";
                KeyInsight.insightText = "Lorem Ipsum";
               // objkeyinsight.Add(KeyInsight);
                obj.campaignkeyinsight = KeyInsight;

                StoreCampaignRecommended RecommendedDetail = new StoreCampaignRecommended();

                RecommendedDetail.mobileNumber = "9923165567";
                RecommendedDetail.itemCode = "F808920000";
                RecommendedDetail.category = "Shoes";
                RecommendedDetail.subCategory = "Casual";
                RecommendedDetail.brand = "";
                RecommendedDetail.color = "Black";
                RecommendedDetail.size = "7";
                RecommendedDetail.price = "3499";
                RecommendedDetail.url = "https://www.bata.in/bataindia/e-124_c-262/blacks-and-browns/men.html";
                RecommendedDetail.imageURL = "https://img2.bata.in/0/images/product/854-6523_300x300_1.jpeg";
                objrecommended.Add(RecommendedDetail);
                obj.campaignrecommended = objrecommended;

                StoreCampaignLastTransactionDetails LastTransactionDetails = new StoreCampaignLastTransactionDetails();

                     LastTransactionDetails.billNo = "BB332398";
                     LastTransactionDetails.billDate = "12 Jan 2020";
                     LastTransactionDetails.storeName = "Bata-Rajouri Garden";
                     LastTransactionDetails.amount = "1,499";
                     LastTransactionDetails.itemDetails = "";
                     obj.lasttransactiondetails = LastTransactionDetails;

            }
            catch (Exception)
            {
                throw;
            }
            return obj;
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
