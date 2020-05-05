using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        public CampaignCustomerDetails GetCampaignCustomer(int tenantID, int userID, int campaignScriptID, int pageNo, int pageSize, string FilterStatus)
        {
            DataSet ds = new DataSet();
            CampaignCustomerDetails objdetails = new CampaignCustomerDetails();

            List<CampaignCustomerModel> objList = new List<CampaignCustomerModel>();
            int CustomerCount = 0;
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
                cmd.Parameters.AddWithValue("@_FilterStatus", FilterStatus);

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
                            CallRescheduledTo = ds.Tables[0].Rows[i]["CallRescheduledTo"] == DBNull.Value ? string.Empty : ConvertDatetimeToString(Convert.ToString(ds.Tables[0].Rows[i]["CallRescheduledTo"])),
                            DoesTicketRaised = ds.Tables[0].Rows[i]["DoesTicketRaised"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DoesTicketRaised"]),
                            StatusName = ds.Tables[0].Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StatusName"]),
                            StatusID = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]),
                            Programcode = ds.Tables[0].Rows[i]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Programcode"]),
                            Storecode = ds.Tables[0].Rows[i]["Storecode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Storecode"]),
                            StoreManagerId = ds.Tables[0].Rows[i]["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StoreManagerId"]),
                            SmsFlag = ds.Tables[0].Rows[i]["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["SmsFlag"]),
                            EmailFlag = ds.Tables[0].Rows[i]["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["EmailFlag"]),
                            MessengerFlag = ds.Tables[0].Rows[i]["MessengerFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["MessengerFlag"]),
                            BotFlag = ds.Tables[0].Rows[i]["BotFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["BotFlag"]),
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
                if (ds != null && ds.Tables[2] != null)
                {
                    CustomerCount = ds.Tables[2].Rows[0]["TotalCustomerCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[2].Rows[0]["TotalCustomerCount"]);
                }
                objdetails.CampaignCustomerModel = objList;
                objdetails.CampaignCustomerCount = CustomerCount;
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
            return objdetails;
        }

        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UpdateCampaignStatusResponse(CampaignResponseInput objRequest, int TenantID, int UserID)
        {

            int result = 0;
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_HSUpdateCampaignCustomer", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_CampaignCustomerID", objRequest.CampaignCustomerID);
                cmd.Parameters.AddWithValue("@_ResponseID", objRequest.ResponseID);

                if (!string.IsNullOrEmpty(objRequest.CallReScheduledTo))
                {
                    objRequest.CallReScheduledToDate = Convert.ToDateTime(objRequest.CallReScheduledTo);
                }
                cmd.Parameters.AddWithValue("@_CallReScheduledTo", objRequest.CallReScheduledToDate);
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_UserID", UserID);

                result = Convert.ToInt32(cmd.ExecuteNonQuery());
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

        /// <summary>
        /// Convert Datetime ToString
        /// </summary>
        /// <param name="DateInString"></param>
        /// <returns></returns>
        public string ConvertDatetimeToString(string DateInString)
        {
            string result = "";
            string GMT = " GMT+05:30 (" + TimeZoneInfo.Local.StandardName + ")";
            try
            {
                if (!String.IsNullOrEmpty(DateInString))
                {
                    result = DateInString + GMT;
                }

            }
            catch (Exception)
            {

            }

            return result;
        }

        /// <summary>
        /// Campaign Share Chat bot
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int CampaignShareChatbot(ShareChatbotModel objRequest, string ClientAPIURL, int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            int result = 0;
            int resultApi = 0;
            string Message = "";
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_HSCampaignShareChatbot", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_StoreID", objRequest.StoreID);
                cmd.Parameters.AddWithValue("@_ProgramCode", objRequest.ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", objRequest.CustomerID);
                cmd.Parameters.AddWithValue("@_CustomerMobileNumber", objRequest.CustomerMobileNumber);
                cmd.Parameters.AddWithValue("@_StoreManagerId", objRequest.StoreManagerId);
                cmd.Parameters.AddWithValue("@_CampaignScriptID", objRequest.CampaignScriptID);
                cmd.Parameters.AddWithValue("@_CreatedBy", UserID);

                //result = Convert.ToInt32(cmd.ExecuteNonQuery());
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    result = ds.Tables[0].Rows[0]["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ChatID"]);
                    Message = ds.Tables[0].Rows[0]["Message"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                }

                if (!String.IsNullOrEmpty(Message))
                {
                    SendFreeTextRequest sendFreeTextRequest = new SendFreeTextRequest
                    {
                        ProgramCode = objRequest.ProgramCode,
                        PhoneNumber = objRequest.CustomerMobileNumber.Length > 10 ? objRequest.CustomerMobileNumber : "91" + objRequest.CustomerMobileNumber,
                        Text = Message
                    };

                    string apiReq = JsonConvert.SerializeObject(sendFreeTextRequest);
                    apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendFreeText", apiReq);

                    //ChatSendSMSResponse chatSendSMSResponse = new ChatSendSMSResponse();

                    //chatSendSMSResponse = JsonConvert.DeserializeObject<ChatSendSMSResponse>(apiResponse);

                    //if (chatSendSMSResponse != null)
                    //{
                    //    resultApi = chatSendSMSResponse.Id;
                    //}

                    if (resultApi > 0)
                    {
                        UpdateResponseShare(objRequest.CustomerID, "Contacted Via Chatbot");
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

        /// <summary>
        /// Campaign Share Massanger
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string CampaignShareMassanger(ShareChatbotModel objRequest, int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            string Message = "";
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_HSCampaignShareMassanger", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_StoreID", objRequest.StoreID);
                cmd.Parameters.AddWithValue("@_ProgramCode", objRequest.ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", objRequest.CustomerID);
                cmd.Parameters.AddWithValue("@_CustomerMobileNumber", objRequest.CustomerMobileNumber);
                cmd.Parameters.AddWithValue("@_StoreManagerId", objRequest.StoreManagerId);
                cmd.Parameters.AddWithValue("@_CampaignScriptID", objRequest.CampaignScriptID);
                cmd.Parameters.AddWithValue("@_CreatedBy", UserID);

                //result = Convert.ToInt32(cmd.ExecuteNonQuery());
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    Message = ds.Tables[0].Rows[0]["Message"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
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
            return Message;
        }

        /// <summary>
        /// Campaign Share SMS
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int CampaignShareSMS(ShareChatbotModel objRequest, string ClientAPIURL, string SMSsenderId, int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            int result = 0;
            string Message = "";
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_HSCampaignShareSMS", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_StoreID", objRequest.StoreID);
                cmd.Parameters.AddWithValue("@_ProgramCode", objRequest.ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", objRequest.CustomerID);
                cmd.Parameters.AddWithValue("@_CustomerMobileNumber", objRequest.CustomerMobileNumber);
                cmd.Parameters.AddWithValue("@_StoreManagerId", objRequest.StoreManagerId);
                cmd.Parameters.AddWithValue("@_CampaignScriptID", objRequest.CampaignScriptID);
                cmd.Parameters.AddWithValue("@_CreatedBy", UserID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    Message = ds.Tables[0].Rows[0]["Message"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                }

                if(!String.IsNullOrEmpty(Message))
                {
                    ChatSendSMS chatSendSMS = new ChatSendSMS
                    {
                        MobileNumber = objRequest.CustomerMobileNumber.Length > 10 ? objRequest.CustomerMobileNumber : "91" + objRequest.CustomerMobileNumber,
                        SenderId = SMSsenderId,
                        SmsText = Message
                    };
                    
                    string apiReq = JsonConvert.SerializeObject(chatSendSMS);
                    apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendSMS", apiReq);

                    ChatSendSMSResponse chatSendSMSResponse = new ChatSendSMSResponse();

                    chatSendSMSResponse = JsonConvert.DeserializeObject<ChatSendSMSResponse>(apiResponse);

                    if(chatSendSMSResponse != null)
                    {
                        result = chatSendSMSResponse.Id;
                    }

                    if(result > 0)
                    {
                        UpdateResponseShare(objRequest.CustomerID, "Contacted Via SMS");
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

        /// <summary>
        /// Update Response Share
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="ResponseName"></param>
        /// <returns></returns>
        public int UpdateResponseShare(string CustomerID, string ResponseName)
        {

            int result = 0;
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                if (conn == null)
                {
                    conn.Open();
                }
                

                MySqlCommand cmd = new MySqlCommand("SP_HSUpdateResponseShare", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_ResponseName", ResponseName);

                result = Convert.ToInt32(cmd.ExecuteNonQuery());
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
