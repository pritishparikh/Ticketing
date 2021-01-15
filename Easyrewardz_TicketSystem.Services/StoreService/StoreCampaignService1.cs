using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<CampaignCustomerDetails> GetCampaignCustomer(int tenantID, int userID, CampaingCustomerFilterRequest campaingCustomerFilterRequest)
        {
            CampaignCustomerDetails objdetails = new CampaignCustomerDetails();
            List<CampaignCustomerModel> objList = new List<CampaignCustomerModel>();
            int CustomerCount = 0;
            DataTable campaignTable = new DataTable();
            DataTable responseListTable = new DataTable();
            DataTable customerCountTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignCustomer", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_CampaignScriptID", campaingCustomerFilterRequest.CampaignScriptID);
                    cmd.Parameters.AddWithValue("@_pageno", campaingCustomerFilterRequest.PageNo);
                    cmd.Parameters.AddWithValue("@_pagesize", campaingCustomerFilterRequest.PageSize);
                    cmd.Parameters.AddWithValue("@_FilterStatus", campaingCustomerFilterRequest.FilterStatus);
                    cmd.Parameters.AddWithValue("@_MobileNumber", campaingCustomerFilterRequest.MobileNumber);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            try
                            {
                                ds.Tables.Add(campaignTable);
                                ds.Tables.Add(responseListTable);
                                ds.Tables.Add(customerCountTable);
                                ds.EnforceConstraints = false;
                                campaignTable.Load(reader);
                                responseListTable.Load(reader);
                                customerCountTable.Load(reader);
                            }
                            catch
                            {
                                var error = campaignTable.GetErrors();
                            }
                        }
                    }
                    if (campaignTable != null)
                    {
                        for (int i = 0; i < campaignTable.Rows.Count; i++)
                        {
                            CampaignCustomerModel obj = new CampaignCustomerModel
                            {
                                ID = campaignTable.Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(campaignTable.Rows[i]["ID"]),
                                //CampaignScriptID = campaignTable.Rows[i]["CampaignScriptID"] == DBNull.Value ? 0 : Convert.ToInt32(campaignTable.Rows[i]["CampaignScriptID"]),
                                CampaignScriptID = campaingCustomerFilterRequest.CampaignScriptID,
                                CustomerName = campaignTable.Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(campaignTable.Rows[i]["CustomerName"]),
                                CustomerNumber = campaignTable.Rows[i]["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(campaignTable.Rows[i]["CustomerNumber"]),
                                CustomerEmailID = campaignTable.Rows[i]["CustomerEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(campaignTable.Rows[i]["CustomerEmailID"]),
                                DOB = campaignTable.Rows[i]["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(campaignTable.Rows[i]["DOB"]),
                                CampaignDate = campaignTable.Rows[i]["CampaignDate"] == DBNull.Value ? string.Empty : Convert.ToString(campaignTable.Rows[i]["CampaignDate"]),
                                ResponseID = campaignTable.Rows[i]["ResponseID"] == DBNull.Value ? 0 : Convert.ToInt32(campaignTable.Rows[i]["ResponseID"]),
                                CallRescheduledTo = campaignTable.Rows[i]["CallRescheduledTo"] == DBNull.Value ? string.Empty : ConvertDatetimeToString(Convert.ToString(campaignTable.Rows[i]["CallRescheduledTo"])),
                                DoesTicketRaised = campaignTable.Rows[i]["DoesTicketRaised"] == DBNull.Value ? 0 : Convert.ToInt32(campaignTable.Rows[i]["DoesTicketRaised"]),
                                StatusName = campaignTable.Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(campaignTable.Rows[i]["StatusName"]),
                                StatusID = campaignTable.Rows[i]["StatusID"] == DBNull.Value ? 0 : Convert.ToInt32(campaignTable.Rows[i]["StatusID"]),
                                Programcode = campaignTable.Rows[i]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(campaignTable.Rows[i]["Programcode"]),
                                Storecode = campaignTable.Rows[i]["Storecode"] == DBNull.Value ? string.Empty : Convert.ToString(campaignTable.Rows[i]["Storecode"]),
                                StoreManagerId = campaignTable.Rows[i]["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(campaignTable.Rows[i]["StoreManagerId"]),
                                SmsFlag = campaignTable.Rows[i]["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(campaignTable.Rows[i]["SmsFlag"]),
                                EmailFlag = campaignTable.Rows[i]["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(campaignTable.Rows[i]["EmailFlag"]),
                                MessengerFlag = campaignTable.Rows[i]["MessengerFlag"] == DBNull.Value ? false : Convert.ToBoolean(campaignTable.Rows[i]["MessengerFlag"]),
                                BotFlag = campaignTable.Rows[i]["BotFlag"] == DBNull.Value ? false : Convert.ToBoolean(campaignTable.Rows[i]["BotFlag"]),
                                IsCustomerChating = campaignTable.Rows[i]["IsCustomerChating"] == DBNull.Value ? false : Convert.ToBoolean(campaignTable.Rows[i]["IsCustomerChating"]),
                                CustomerChatingID = campaignTable.Rows[i]["CustomerChatingID"] == DBNull.Value ? 0 : Convert.ToInt32(campaignTable.Rows[i]["CustomerChatingID"]),
                                HSCampaignResponseList = responseListTable.AsEnumerable().Select(r => new HSCampaignResponse()
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
                    if (customerCountTable != null)
                    {
                        if (customerCountTable.Rows.Count > 0)
                        {
                            CustomerCount = customerCountTable.Rows[0]["TotalCustomerCount"] == DBNull.Value ? 0 : Convert.ToInt32(customerCountTable.Rows[0]["TotalCustomerCount"]);
                        }
                    }
                    objdetails.CampaignCustomerModel = objList;
                    objdetails.CampaignCustomerCount = CustomerCount;
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
                if (campaignTable != null)
                {
                    campaignTable.Dispose();
                }
                if (responseListTable != null)
                {
                    responseListTable.Dispose();
                }
                if (customerCountTable != null)
                {
                    customerCountTable.Dispose();
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
        public async Task<int> UpdateCampaignStatusResponse(CampaignResponseInput objRequest, int TenantID, int UserID)
        {

            int result = 0;
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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

                    result = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
                throw;
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
        public async Task<int> CampaignShareChatbot(ShareChatbotModel objRequest, string ClientAPIURL, int TenantID, int UserID, string ProgramCode)
        {
            DataSet ds = new DataSet();
            int result = 0;
            string Message = "";
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                List<GetWhatsappMessageDetailsResponse> getWhatsappMessageDetailsResponseList = new List<GetWhatsappMessageDetailsResponse>();
                GetWhatsappMessageDetailsResponse getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();

                string whatsapptemplate = await GetWhatsupTemplateName(TenantID, UserID, "Campaign");

                string strpostionNumber = "";
                string strpostionName = "";
                string additionalInfo = "";
                string wABANo = "";
                try
                {
                    GetWhatsappMessageDetailsModal getWhatsappMessageDetailsModal = new GetWhatsappMessageDetailsModal()
                    {
                        ProgramCode = ProgramCode
                    };

                    string apiBotReq = JsonConvert.SerializeObject(getWhatsappMessageDetailsModal);
                    //string apiBotResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetWhatsappMessageDetails", apiBotReq);
                    string apiBotResponse = await APICall.SendApiRequest(ClientAPIURL + _campaingURLList.Getwhatsappmessagedetails, apiBotReq);



                    if (!string.IsNullOrEmpty(apiBotResponse.Replace("[]", "").Replace("[", "").Replace("]", "")))
                    {
                        getWhatsappMessageDetailsResponseList = JsonConvert.DeserializeObject<List<GetWhatsappMessageDetailsResponse>>(apiBotResponse);
                    }


                    if (getWhatsappMessageDetailsResponseList != null)
                    {
                        if (getWhatsappMessageDetailsResponseList.Count > 0)
                        {
                            getWhatsappMessageDetailsResponse = getWhatsappMessageDetailsResponseList.Where(x => x.TemplateName == whatsapptemplate).FirstOrDefault();
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
                    else
                    {
                        getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
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
                    cmd.Parameters.AddWithValue("@_strpostionNumber", strpostionNumber);
                    cmd.Parameters.AddWithValue("@_strpostionName", strpostionName);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            result = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]);
                            Message = dr["Message"] == DBNull.Value ? String.Empty : Convert.ToString(dr["Message"]);
                            additionalInfo = dr["additionalInfo"] == DBNull.Value ? String.Empty : Convert.ToString(dr["additionalInfo"]);
                            wABANo = dr["wABANo"] == DBNull.Value ? String.Empty : Convert.ToString(dr["wABANo"]);
                        }
                    }
                }                

                if (!String.IsNullOrEmpty(Message))
                {
                    try
                    {
                        List<string> additionalList = new List<string>();
                        if (!string.IsNullOrEmpty(additionalInfo))
                        {
                            additionalList = additionalInfo.Split(",").ToList();
                        }
                        SendFreeTextRequest sendFreeTextRequest = new SendFreeTextRequest
                        {
                            //To = objRequest.CustomerMobileNumber.TrimStart('0').Length > 10 ? objRequest.CustomerMobileNumber : "91" + objRequest.CustomerMobileNumber.TrimStart('0'),
                            To = objRequest.CustomerMobileNumber.TrimStart('0'),
                            ProgramCode = ProgramCode,
                            TemplateName = getWhatsappMessageDetailsResponse.TemplateName,
                            AdditionalInfo = additionalList,
                            language= getWhatsappMessageDetailsResponse.TemplateLanguage,
                            whatsAppNumber = wABANo
                        };

                        string apiReq = JsonConvert.SerializeObject(sendFreeTextRequest);
                        //string apiResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendCampaign", apiReq);
                        string apiResponse = await APICall.SendApiRequest(ClientAPIURL + _campaingURLList.Sendcampaign, apiReq);

                        if (apiResponse.Equals("true"))
                        {
                            await UpdateResponseShare(objRequest.CustomerID, "Contacted Via Chatbot");
                            //string bellMobileNumber = objRequest.CustomerMobileNumber.TrimStart('0').Length > 10 ? objRequest.CustomerMobileNumber : "91" + objRequest.CustomerMobileNumber.TrimStart('0');
                            string bellMobileNumber = objRequest.CustomerMobileNumber.TrimStart('0');
                            await MakeBellActive(bellMobileNumber, objRequest.ProgramCode, ClientAPIURL, TenantID, UserID, _campaingURLList);
                        }
                    }
                    catch (Exception)
                    {

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
            return result;
        }

        /// <summary>
        /// GetWhatsupTemplateName
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="MessageName"></param>
        /// <returns></returns>
        public async Task<string> GetWhatsupTemplateName(int TenantID, int UserID, string MessageName)
        {
            string WhatsupTemplateName = "";
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetWhatsupTemplate", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                    cmd.Parameters.AddWithValue("@_UserID", UserID);
                    cmd.Parameters.AddWithValue("@_MessageName", MessageName);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            WhatsupTemplateName = dr["TemplateName"] == DBNull.Value ? String.Empty : Convert.ToString(dr["TemplateName"]);
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

            return WhatsupTemplateName;
        }

        /// <summary>
        /// Campaign Share Massanger
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<string> CampaignShareMassanger(ShareChatbotModel objRequest, int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            string Message = "";
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            Message = dr["Message"] == DBNull.Value ? String.Empty : Convert.ToString(dr["Message"]);
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
        public async Task<int> CampaignShareSMS(ShareChatbotModel objRequest, string ClientAPIURL, string SMSsenderId, int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            int result = 0;
            string Message = "";
            CampaignStatusResponse obj = new CampaignStatusResponse();
            #region
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            Message = dr["Message"] == DBNull.Value ? String.Empty : Convert.ToString(dr["Message"]);
                            SMSsenderId = dr["SmsSenderID"] == DBNull.Value ? String.Empty : Convert.ToString(dr["SmsSenderID"]);
                        }
                        if (!String.IsNullOrEmpty(Message))
                        {
                            ChatSendSMS chatSendSMS = new ChatSendSMS
                            {
                                //MobileNumber = objRequest.CustomerMobileNumber.TrimStart('0').Length > 10 ? objRequest.CustomerMobileNumber : "91" + objRequest.CustomerMobileNumber.TrimStart('0'),
                                MobileNumber = objRequest.CustomerMobileNumber.TrimStart('0'),
                                SenderId = SMSsenderId,
                                SmsText = Message
                            };

                            string apiReq = JsonConvert.SerializeObject(chatSendSMS);
                            //string apiResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendSMS", apiReq);
                            string apiResponse = await APICall.SendApiRequest(ClientAPIURL + _campaingURLList.Sendsms, apiReq);

                            ChatSendSMSResponse chatSendSMSResponse = new ChatSendSMSResponse();

                            chatSendSMSResponse = JsonConvert.DeserializeObject<ChatSendSMSResponse>(apiResponse);

                            if (chatSendSMSResponse != null)
                            {
                                if (string.IsNullOrEmpty(chatSendSMSResponse.ErrorCODE))
                                {
                                    result = chatSendSMSResponse.Id;
                                }
                            }
                        }
                    }
                }

                if (result > 0)
                {
                    await UpdateResponseShare(objRequest.CustomerID, "Contacted Via SMS");
                    //string bellMobileNumber = objRequest.CustomerMobileNumber.TrimStart('0').Length > 10 ? objRequest.CustomerMobileNumber : "91" + objRequest.CustomerMobileNumber.TrimStart('0');
                    string bellMobileNumber = objRequest.CustomerMobileNumber.TrimStart('0');
                    await MakeBellActive(bellMobileNumber, objRequest.ProgramCode, ClientAPIURL, TenantID, UserID, _campaingURLList);
                }
            }
            #endregion
            //#region
            //try
            //{
            //    conn.Open();

            //    MySqlCommand cmd = new MySqlCommand("SP_HSCampaignShareSMS", conn)
            //    {
            //        CommandType = CommandType.StoredProcedure
            //    };
            //    cmd.Parameters.AddWithValue("@_TenantID", TenantID);
            //    cmd.Parameters.AddWithValue("@_StoreID", objRequest.StoreID);
            //    cmd.Parameters.AddWithValue("@_ProgramCode", objRequest.ProgramCode);
            //    cmd.Parameters.AddWithValue("@_CustomerID", objRequest.CustomerID);
            //    cmd.Parameters.AddWithValue("@_CustomerMobileNumber", objRequest.CustomerMobileNumber);
            //    cmd.Parameters.AddWithValue("@_StoreManagerId", objRequest.StoreManagerId);
            //    cmd.Parameters.AddWithValue("@_CampaignScriptID", objRequest.CampaignScriptID);
            //    cmd.Parameters.AddWithValue("@_CreatedBy", UserID);

            //    MySqlDataAdapter da = new MySqlDataAdapter
            //    {
            //        SelectCommand = cmd
            //    };
            //    da.Fill(ds);
            //    if (ds != null && ds.Tables[0] != null)
            //    {
            //        Message = ds.Tables[0].Rows[0]["Message"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
            //        SMSsenderId = ds.Tables[0].Rows[0]["SmsSenderID"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["SmsSenderID"]);
            //    }

            //    if (!String.IsNullOrEmpty(Message))
            //    {
            //        ChatSendSMS chatSendSMS = new ChatSendSMS
            //        {
            //            MobileNumber = objRequest.CustomerMobileNumber.TrimStart('0').Length > 10 ? objRequest.CustomerMobileNumber : "91" + objRequest.CustomerMobileNumber.TrimStart('0'),
            //            SenderId = SMSsenderId,
            //            SmsText = Message
            //        };

            //        string apiReq = JsonConvert.SerializeObject(chatSendSMS);
            //        apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendSMS", apiReq);

            //        ChatSendSMSResponse chatSendSMSResponse = new ChatSendSMSResponse();

            //        chatSendSMSResponse = JsonConvert.DeserializeObject<ChatSendSMSResponse>(apiResponse);

            //        if (chatSendSMSResponse != null)
            //        {
            //            if (string.IsNullOrEmpty(chatSendSMSResponse.ErrorCODE))
            //            {
            //                result = chatSendSMSResponse.Id;
            //            }
            //        }

            //        if (result > 0)
            //        {
            //            UpdateResponseShare(objRequest.CustomerID, "Contacted Via SMS");
            //            string bellMobileNumber = objRequest.CustomerMobileNumber.TrimStart('0').Length > 10 ? objRequest.CustomerMobileNumber : "91" + objRequest.CustomerMobileNumber.TrimStart('0');
            //            MakeBellActive(bellMobileNumber, objRequest.ProgramCode, ClientAPIURL, TenantID, UserID);
            //        }
            //    }

            //}
            //#endregion
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
            return result;
        }

        /// <summary>
        /// Update Response Share
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="ResponseName"></param>
        /// <returns></returns>
        public async Task<int> UpdateResponseShare(string CustomerID, string ResponseName)
        {
            int result = 0;
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSUpdateResponseShare", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                    cmd.Parameters.AddWithValue("@_ResponseName", ResponseName);

                    result = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
        /// GetBroadcastConfigurationResponses
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <param name="campaignCode"></param>
        /// <returns></returns>
        public async Task<BroadcastDetails> GetBroadcastConfigurationResponses(int tenantID, int userID, string programcode, string storeCode, string campaignCode)
        {
            DataSet ds = new DataSet();
            BroadcastDetails broadcastDetails = new BroadcastDetails();
            List<CampaignExecutionDetailsResponse> objList = new List<CampaignExecutionDetailsResponse>();
            BroadcastConfigurationResponse BroadcastConfigurationResponse = new BroadcastConfigurationResponse();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetBroadcastConfiguration", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_Programcode", programcode);
                    cmd.Parameters.AddWithValue("@_StoreCode", storeCode);
                    cmd.Parameters.AddWithValue("@_CampaignCode", campaignCode);
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            CampaignExecutionDetailsResponse obj = new CampaignExecutionDetailsResponse
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                Programcode = dr["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Programcode"]),
                                StoreCode = dr["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreCode"]),
                                CampaignCode = dr["CampaignCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignCode"]),
                                ChannelType = dr["ChannelType"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChannelType"]),
                                ExecutionDate = dr["ExecutionDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ExecutionDate"]),
                            };
                            objList.Add(obj);
                        }
                       if( dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                BroadcastConfigurationResponse.ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]);
                                BroadcastConfigurationResponse.MaxClickAllowed = dr["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxClickAllowed"]);
                                BroadcastConfigurationResponse.SmsFlag = dr["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["SmsFlag"]);
                                BroadcastConfigurationResponse.EmailFlag = dr["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["EmailFlag"]);
                                BroadcastConfigurationResponse.WhatsappFlag = dr["WhatsappFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["WhatsappFlag"]);
                                BroadcastConfigurationResponse.DisableSMS = dr["HideSMS"] == DBNull.Value ? false : Convert.ToBoolean(dr["HideSMS"]);
                                BroadcastConfigurationResponse.DisableEmail = dr["HideEmail"] == DBNull.Value ? false : Convert.ToBoolean(dr["HideEmail"]);
                                BroadcastConfigurationResponse.DisableWhatsapp = dr["HideWhatsapp"] == DBNull.Value ? false : Convert.ToBoolean(dr["HideWhatsapp"]);
                                BroadcastConfigurationResponse.SMSClickCount = dr["SMSClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SMSClickCount"]);
                                BroadcastConfigurationResponse.EmailClickCount = dr["EmailClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EmailClickCount"]);
                                BroadcastConfigurationResponse.WhatsappClickCount = dr["WhatsappClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["WhatsappClickCount"]);
                            }
                        }
                        

                        broadcastDetails.CampaignExecutionDetailsResponse = objList;
                        broadcastDetails.BroadcastConfigurationResponse = BroadcastConfigurationResponse;
                    }

                }
            }
            //#region 
            //DataSet ds = new DataSet();
            //BroadcastDetails broadcastDetails = new BroadcastDetails();
            //List<CampaignExecutionDetailsResponse> objList = new List<CampaignExecutionDetailsResponse>();
            //BroadcastConfigurationResponse BroadcastConfigurationResponse = new BroadcastConfigurationResponse();
            //try
            //{
            //    conn.Open();
            //    MySqlCommand cmd = new MySqlCommand("SP_HSGetBroadcastConfiguration", conn)
            //    {
            //        CommandType = CommandType.StoredProcedure
            //    };
            //    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
            //    cmd.Parameters.AddWithValue("@_UserID", userID);
            //    cmd.Parameters.AddWithValue("@_Programcode", programcode);
            //    cmd.Parameters.AddWithValue("@_StoreCode", storeCode);
            //    cmd.Parameters.AddWithValue("@_CampaignCode", campaignCode);

            //    MySqlDataAdapter da = new MySqlDataAdapter
            //    {
            //        SelectCommand = cmd
            //    };
            //    da.Fill(ds);
            //    if (ds != null && ds.Tables[0] != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //            {
            //                CampaignExecutionDetailsResponse obj = new CampaignExecutionDetailsResponse
            //                {
            //                    ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
            //                    Programcode = ds.Tables[0].Rows[i]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Programcode"]),
            //                    StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]),
            //                    CampaignCode = ds.Tables[0].Rows[i]["CampaignCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignCode"]),
            //                    ChannelType = ds.Tables[0].Rows[i]["ChannelType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ChannelType"]),
            //                    ExecutionDate = ds.Tables[0].Rows[i]["ExecutionDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ExecutionDate"]),
            //                };
            //                objList.Add(obj);
            //            }
            //        }
            //    }
            //    if (ds != null && ds.Tables[1] != null)
            //    {
            //        BroadcastConfigurationResponse.ID = ds.Tables[1].Rows[0]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["ID"]);
            //        BroadcastConfigurationResponse.MaxClickAllowed = ds.Tables[1].Rows[0]["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["MaxClickAllowed"]);
            //        BroadcastConfigurationResponse.SmsFlag = ds.Tables[1].Rows[0]["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[1].Rows[0]["SmsFlag"]);
            //        BroadcastConfigurationResponse.EmailFlag = ds.Tables[1].Rows[0]["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[1].Rows[0]["EmailFlag"]);
            //        BroadcastConfigurationResponse.WhatsappFlag = ds.Tables[1].Rows[0]["WhatsappFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[1].Rows[0]["WhatsappFlag"]);
            //        BroadcastConfigurationResponse.DisableSMS = ds.Tables[1].Rows[0]["HideSMS"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[1].Rows[0]["HideSMS"]);
            //        BroadcastConfigurationResponse.DisableEmail = ds.Tables[1].Rows[0]["HideEmail"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[1].Rows[0]["HideEmail"]);
            //        BroadcastConfigurationResponse.DisableWhatsapp = ds.Tables[1].Rows[0]["HideWhatsapp"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[1].Rows[0]["HideWhatsapp"]);
            //        BroadcastConfigurationResponse.SMSClickCount = ds.Tables[1].Rows[0]["SMSClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["SMSClickCount"]);
            //        BroadcastConfigurationResponse.EmailClickCount = ds.Tables[1].Rows[0]["EmailClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["EmailClickCount"]);
            //        BroadcastConfigurationResponse.WhatsappClickCount = ds.Tables[1].Rows[0]["WhatsappClickCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["WhatsappClickCount"]);
            //    }
            //    broadcastDetails.CampaignExecutionDetailsResponse = objList;
            //    broadcastDetails.BroadcastConfigurationResponse = BroadcastConfigurationResponse;
            //}
            //#endregion
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
            return broadcastDetails;

        }

        /// <summary>
        /// InsertBroadCastDetails
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <param name="campaignCode"></param>
        /// <param name="channelType"></param>
        /// <returns></returns>
        public async Task<int> InsertBroadCastDetails(int tenantID, int userID, string programcode, string storeCode, string campaignCode, string channelType, string ClientAPIURL)
        {

            int result = 0;
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                string strpostionNumber = "";
                string strpostionName = "";
                GetWhatsappMessageDetailsResponse getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
                List<GetWhatsappMessageDetailsResponse> getWhatsappMessageDetailsResponseList = new List<GetWhatsappMessageDetailsResponse>();

                try
                {
                    if (channelType.Equals("Whatsapp"))
                    {
                        string whatsapptemplate = await GetWhatsupTemplateName(tenantID, userID, "Campaign");

                        GetWhatsappMessageDetailsModal getWhatsappMessageDetailsModal = new GetWhatsappMessageDetailsModal()
                        {
                            ProgramCode = programcode
                        };

                        string apiBotReq = JsonConvert.SerializeObject(getWhatsappMessageDetailsModal);
                        //string apiBotResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetWhatsappMessageDetails", apiBotReq);
                        string apiBotResponse = await APICall.SendApiRequest(ClientAPIURL + _campaingURLList.Getwhatsappmessagedetails, apiBotReq);



                        if (!string.IsNullOrEmpty(apiBotResponse.Replace("[]", "").Replace("[", "").Replace("]", "")))
                        {
                            getWhatsappMessageDetailsResponseList = JsonConvert.DeserializeObject<List<GetWhatsappMessageDetailsResponse>>(apiBotResponse);
                        }


                        if (getWhatsappMessageDetailsResponseList != null)
                        {
                            if (getWhatsappMessageDetailsResponseList.Count > 0)
                            {
                                getWhatsappMessageDetailsResponse = getWhatsappMessageDetailsResponseList.Where(x => x.TemplateName == whatsapptemplate).FirstOrDefault();
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
                        else
                        {
                            getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
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
                    MySqlCommand cmd = new MySqlCommand("SP_HSInsertBroadCastDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_Programcode", programcode);
                    cmd.Parameters.AddWithValue("@_StoreCode", storeCode);
                    cmd.Parameters.AddWithValue("@_CampaignCode", campaignCode);
                    cmd.Parameters.AddWithValue("@_ChannelType", channelType);
                    cmd.Parameters.AddWithValue("@_strpostionNumber", strpostionNumber);
                    cmd.Parameters.AddWithValue("@_strpostionName", strpostionName);
                    cmd.Parameters.AddWithValue("@_TemplateName", getWhatsappMessageDetailsResponse.TemplateName);
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);

                    result = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
        /// MakeBellActive
        /// </summary>
        /// <param name="Mobilenumber"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        public async Task<string> MakeBellActive(string Mobilenumber, string ProgramCode, string ClientAPIURL, int TenantID, int UserID, CampaingURLList _campaingURLList)
        {
            string apiResponsechatbotBellMakeBellActive = "";
            try
            {
               // NameValueCollection Params = new NameValueCollection
                Dictionary<string, string> Params = new Dictionary<string, string>()
                {
                    { "Mobilenumber", Mobilenumber },
                    { "ProgramCode", ProgramCode }
                };
                //string apiResponsechatbotBellMakeBellActive = CommonService.SendParamsApiRequest(ClientAPIURL + "api/ChatbotBell/MakeBellActive", Params);
                apiResponsechatbotBellMakeBellActive = await APICall.SendApiRequestParams(ClientAPIURL + _campaingURLList.MakeBellActive, Params);
            }
            catch (Exception)
            {
                throw;
            }
            return apiResponsechatbotBellMakeBellActive;
        }
    }
}
