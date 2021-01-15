using ClientAPIServiceCall;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public class HSWebBotService : IWebBot
    {
        #region variable
        MySqlConnection conn = new MySqlConnection();
        ChatbotBellHttpClientService _APICall = null;
        WebBotHttpClientService _WebBot = null;
        MaxWebBotHttpClientService _MaxHSM = null;

        ILogger<object> _logger;
        #endregion

        #region Constructor
        public HSWebBotService(string _connectionString, ChatbotBellHttpClientService APICall = null, WebBotHttpClientService WebBot=null, MaxWebBotHttpClientService MaxHSM=null,
            ILogger<object> logger = null)
        {
            conn.ConnectionString = _connectionString;
            _APICall = APICall;
            _logger = logger;
            _WebBot = WebBot;
            _MaxHSM = MaxHSM;
        }

        #endregion

        public async  Task<List<HSWebBotModel>> GetWebBotOption()
        {
            List<HSWebBotModel> WebBotOption = new List<HSWebBotModel>();

            DataTable schemaTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetHSWebBotOption", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                HSWebBotModel obj = new HSWebBotModel()
                                {
                                    ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                    Option = dr["Option"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Option"]),
                                    IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),

                                };
                                WebBotOption.Add(obj);
                            }

                        }
                    }
                }
            }
            catch(Exception)
            {
                throw;
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

            return WebBotOption;
        }


        public async Task<WebBotFilterByOptionID> GetWebBotFilterByOptionID(int TenantID, int UserID, int OptionID)
        {
            List<object> WebBotFilter = new List<object>();
            WebBotFilterByOptionID webBotFilterByOptionID = new WebBotFilterByOptionID();
            List<WebBotFilterData> WebBotFilterDataList = new List<WebBotFilterData>();
            DataTable schemaTable = new DataTable();
            DataTable schemaWabaTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetFilterDataByOptionID", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                    cmd.Parameters.AddWithValue("@_UserID", UserID);
                    cmd.Parameters.AddWithValue("@_OptionID", OptionID);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);

                            if (OptionID.Equals(1) || OptionID.Equals(2) || OptionID.Equals(5))
                            {
                                schemaWabaTable.Load(reader);
                            }

                            if(schemaTable.Rows.Count > 0)
                            {
                                if(OptionID.Equals(1) || OptionID.Equals(2))
                                {

                                    foreach (DataRow dr in schemaTable.Rows)
                                    {
                                        CountryCodeModel obj = new CountryCodeModel()
                                        {
                                            ISO = dr["ISO"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ISO"]),
                                            PhoneCode = dr["PhoneCode"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PhoneCode"]),
                                            CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CountryName"]),

                                        };

                                        WebBotFilter.Add(obj);
                                    }
                                }

                                else if(OptionID.Equals(3) || OptionID.Equals(4))
                                {

                                    foreach (DataRow dr in schemaTable.Rows)
                                    {
                                        ShoppingBag obj = new ShoppingBag()
                                        {
                                            ShoppingID = dr["ShoppingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ShoppingID"]),
                                            ShoppingBagNo = dr["ShoppingBagNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ShoppingBagNo"]),
                                            CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                            MobileNumber = dr["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MobileNumber"]),
                                            WabaNumber = dr["WabaNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["WabaNumber"]),
                                        };
                                        WebBotFilter.Add(obj);
                                    }
                                }

                                else
                                {
                                    foreach (DataRow dr in schemaTable.Rows)
                                    {
                                        OrderReturns obj = new OrderReturns()
                                        {
                                            OrderID = dr["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrderID"]),
                                            CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                            MobileNumber = dr["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MobileNumber"]),
                                            WabaNumber = dr["WabaNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["WabaNumber"])
                                        };

                                        WebBotFilter.Add(obj);
                                    }
                                }
                                webBotFilterByOptionID.WebBotFilter = WebBotFilter;
                            }

                            if (OptionID.Equals(1) || OptionID.Equals(2))
                            {
                                if(schemaWabaTable.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in schemaWabaTable.Rows)
                                    {
                                        WebBotFilterData obj = new WebBotFilterData()
                                        {
                                            WABAId = dr["WABAId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["WABAId"]),
                                            WABANo = dr["WABANo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["WABANo"])
                                        };
                                        WebBotFilterDataList.Add(obj);
                                    }
                                }
                            }
                            webBotFilterByOptionID.WebBotFilterDataList = WebBotFilterDataList;
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }

            return webBotFilterByOptionID;
        }

        public async Task<WebContentDetails> SendWebBotHSM(WebBotContentRequest webBotcontentRequest)
        {
            DataTable schemaTable = new DataTable();
            WebContentDetails WebBotDetails = new WebContentDetails();
            WebBotLinkRequest webBotRequest = new WebBotLinkRequest();
            WebBotLinkResponse webBotResponse = new WebBotLinkResponse();
            MaxWebBotHSMResponse MaxResponse = new MaxWebBotHSMResponse();
            string JsonRequest = string.Empty;
            string ClientAPIResponse = string.Empty;
            bool HSMFlag = false;
            int ChatID = 0;
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                #region DB Call for webBot Content

                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSWebBotHSMContent", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", webBotcontentRequest.TenantID);
                    cmd.Parameters.AddWithValue("@_UserID", webBotcontentRequest.UserID);
                    cmd.Parameters.AddWithValue("@_OptionId", webBotcontentRequest.OptionID);
                    cmd.Parameters.AddWithValue("@_CustomerName", string.IsNullOrEmpty(webBotcontentRequest.CustomerName) ? "" : webBotcontentRequest.CustomerName);
                    cmd.Parameters.AddWithValue("@_MobileNumber", string.IsNullOrEmpty(webBotcontentRequest.MobileNo) ? "" : webBotcontentRequest.MobileNo);
                    cmd.Parameters.AddWithValue("@_ShopingBagId", webBotcontentRequest.ShopingBagNo);
                    cmd.Parameters.AddWithValue("@_OrderId",  webBotcontentRequest.OrderID);
                    cmd.Parameters.AddWithValue("@_WebBotlink", string.IsNullOrEmpty(webBotcontentRequest.WebBotLink) ? "" : webBotcontentRequest.WebBotLink);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                        }

                        if (schemaTable.Rows.Count > 0)
                        {
                            WebBotDetails.MobileNo = schemaTable.Rows[0]["to"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["to"]);
                            WebBotDetails.TemplateName = schemaTable.Rows[0]["TemplateName"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["TemplateName"]);
                            WebBotDetails.AdditionalInfo = schemaTable.Rows[0]["AdditionalInfo"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["AdditionalInfo"]);
                            WebBotDetails.HSMText = schemaTable.Rows[0]["HSMText"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["HSMText"]);
                            WebBotDetails.MakebellActive = schemaTable.Rows[0]["MakebellActive"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["MakebellActive"]);
                            WebBotDetails.ChatActive = schemaTable.Rows[0]["ChatActive"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["ChatActive"]);
                            WebBotDetails.StoreCode = schemaTable.Rows[0]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["StoreCode"]);
                            WebBotDetails.IsMaxClickCount = schemaTable.Rows[0]["IsHSMMaxClick"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["IsHSMMaxClick"]);
                            WebBotDetails.ShoppingBagNo = schemaTable.Rows[0]["ShoppingBagNo"] == DBNull.Value ? "" : Convert.ToString(schemaTable.Rows[0]["ShoppingBagNo"]);
                            WebBotDetails.TemplateLanguage = schemaTable.Rows[0]["TemplateLanguage"] == DBNull.Value ? "" : Convert.ToString(schemaTable.Rows[0]["TemplateLanguage"]);
                        }
                    }

                    #endregion

                    #region check if max click count is reached

                    if (!WebBotDetails.IsMaxClickCount)
                    {
                        if (webBotcontentRequest.OptionID.Equals(1) || webBotcontentRequest.OptionID.Equals(3))
                        {
                            #region send webbot api call for webbot link

                            webBotRequest.programCode =  webBotcontentRequest.ProgramCode;
                            webBotRequest.storeCode = WebBotDetails.StoreCode;
                            webBotRequest.mobile = WebBotDetails.MobileNo;
                            webBotRequest.wabaNumber = webBotcontentRequest.WABANo;
                            webBotRequest.shoppingBagNumber = WebBotDetails.ShoppingBagNo;

                            JsonRequest = JsonConvert.SerializeObject(webBotRequest);

                            ClientAPIResponse = await _WebBot.SendApiRequest(webBotcontentRequest.WeBBotGenerationLink, JsonRequest);

                            if (!string.IsNullOrEmpty(ClientAPIResponse))
                            {
                                webBotResponse = JsonConvert.DeserializeObject<WebBotLinkResponse>(ClientAPIResponse);

                                if (webBotResponse != null)
                                {
                                    if (webBotResponse.isSuccess)
                                    {
                                        webBotcontentRequest.WebBotLink = webBotResponse.data.shortUrl;
                                        WebBotDetails.AdditionalInfo = WebBotDetails.AdditionalInfo.Replace("@WebBotlink", webBotResponse.data.shortUrl);
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(webBotcontentRequest.WebBotLink))
                            {
                                HSMFlag = true;
                            }
                            else
                            {
                                WebBotDetails.ErrorMessage = "WebBot Link is empty";
                            }
                            #endregion
                        }

                        if (webBotcontentRequest.OptionID.Equals(2) || webBotcontentRequest.OptionID.Equals(4) || webBotcontentRequest.OptionID.Equals(5))
                        {
                            if (WebBotDetails.ChatActive)
                            {
                                WebBotDetails.ErrorMessage = "Chat is Already active";
                            }
                            else
                            {
                                HSMFlag = true;
                            }
                        }
                    }
                    else
                    {
                        HSMFlag = false;
                        WebBotDetails.ErrorMessage = "Maximum Click Count Reached";
                    }

                    #endregion

                    if (HSMFlag)
                    {

                        #region HSM API Call

                        if (webBotcontentRequest.ProgramCode.ToLower().Equals("max") || webBotcontentRequest.ProgramCode.ToLower().Equals("lifestyle") || webBotcontentRequest.ProgramCode.ToLower().Equals("homecentre"))
                        {
                            webBotcontentRequest.MaxHSMRequest.body.to = WebBotDetails.MobileNo;
                            webBotcontentRequest.MaxHSMRequest.body.hsm.element_name = WebBotDetails.TemplateName;

                            List<string> MaxAdditionalList = new List<string>();
                            MaxAdditionalList = WebBotDetails.AdditionalInfo.Split('|').ToList();

                            if (MaxAdditionalList.Count > 0)
                            {
                                List<LocalizableParam> list = new List<LocalizableParam>();

                                foreach (string str in MaxAdditionalList)
                                {
                                    list.Add(new LocalizableParam() { @default = str });
                                }
                                webBotcontentRequest.MaxHSMRequest.body.hsm.localizable_params = list;
                            }

                            JsonRequest = JsonConvert.SerializeObject(webBotcontentRequest.MaxHSMRequest);
                            ClientAPIResponse = await _MaxHSM.SendApiRequest(webBotcontentRequest.MaxWebBotHSMURL, JsonRequest);

                            if (!string.IsNullOrEmpty(ClientAPIResponse))
                            {
                                MaxResponse = JsonConvert.DeserializeObject<MaxWebBotHSMResponse>(ClientAPIResponse);

                                ClientAPIResponse = MaxResponse.success ? "true" : "false";
                                WebBotDetails.IsHSMSent = MaxResponse.success;
                            }
                            else
                            {
                                WebBotDetails.IsHSMSent = false;
                                WebBotDetails.ErrorMessage = "HSM sending failed ";
                            }
                        }
                        else
                        {
                            SendFreeTextRequest SendHSM = new SendFreeTextRequest
                            {
                                To = WebBotDetails.MobileNo,
                                ProgramCode = webBotcontentRequest.ProgramCode,
                                TemplateName = WebBotDetails.TemplateName,
                                AdditionalInfo = WebBotDetails.AdditionalInfo.Split('|').ToList(),
                                language = WebBotDetails.TemplateLanguage,
                                whatsAppNumber = webBotcontentRequest.WABANo
                            };

                            JsonRequest = JsonConvert.SerializeObject(SendHSM);
                            ClientAPIResponse = await _APICall.SendApiRequest(webBotcontentRequest.ClientAPIUrl, JsonRequest);

                            WebBotDetails.IsHSMSent = ClientAPIResponse.Equals("true");
                            WebBotDetails.ErrorMessage = string.IsNullOrEmpty(ClientAPIResponse) || ClientAPIResponse.Equals("false") ? "HSM sending failed " : null;
                        }

                        #endregion

                        #region Start New Chat

                        if (webBotcontentRequest.OptionID.Equals(2) || webBotcontentRequest.OptionID.Equals(4) || webBotcontentRequest.OptionID.Equals(5))
                        {
                            if (!string.IsNullOrEmpty(ClientAPIResponse))
                            {

                                if (ClientAPIResponse.Equals("true"))
                                {
                                    #region  make bell active

                                    ChatID = await WebBotActivateChat(webBotcontentRequest.TenantID,
                                        webBotcontentRequest.CustomerName, WebBotDetails.MobileNo, webBotcontentRequest.UserID, WebBotDetails.HSMText);

                                    WebBotDetails.ChatID = ChatID;

                                    if (ChatID > 0)
                                    {
                                        Dictionary<string, string> Params = new Dictionary<string, string>
                                        {
                                            { "Mobilenumber",  WebBotDetails.MobileNo },
                                            { "ProgramCode", webBotcontentRequest.ProgramCode }
                                        };

                                        try
                                        {
                                            ClientAPIResponse = await _APICall.SendApiRequestParams(webBotcontentRequest.MakeBellActiveUrl, Params);
                                        }

                                        catch (Exception) { }

                                        if (!string.IsNullOrEmpty(ClientAPIResponse))
                                        {
                                            WebBotDetails.MakebellActive = ClientAPIResponse.Equals("true");
                                        }
                                    }

                                    #endregion
                                }
                            }
                        }

                        #endregion
                    }

                    #region Insert HSM Log

                    try
                    {
                        await InsertWebotHSMLog(webBotcontentRequest, WebBotDetails);
                    }
                    catch (Exception)
                    {

                    }

                    #endregion
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }

            return WebBotDetails;
        }

        public async Task<int> WebBotActivateChat(int TenantID, string CustomerName,string CustomerMobileNo,int UserID,string Message)
        {
            int ChatID = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_HSWebBotActivateChat", conn))
                    {

                        if (conn != null && conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }

                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_FirstName", CustomerName);
                        cmd.Parameters.AddWithValue("@_CustomerMobileNumber", CustomerMobileNo);
                        cmd.Parameters.AddWithValue("@_UserID", UserID);
                        cmd.Parameters.AddWithValue("@_Message", Message);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ChatID = reader["Chat_ID"] == DBNull.Value ? 0 : reader.GetInt32(reader.GetOrdinal("Chat_ID"));
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
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }

            return ChatID;
        }


        public async Task<int> InsertWebotHSMLog(WebBotContentRequest HSMReqest, WebContentDetails HSMDetails)
        {
            int LogID = 0;
            try
            {

                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_HSInsertWebBotHSMLog", conn))
                    {

                        if (conn != null && conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }

                        cmd.Parameters.AddWithValue("@_TenantID", HSMReqest.TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", HSMReqest.ProgramCode);
                        cmd.Parameters.AddWithValue("@_OptionID", HSMReqest.OptionID);
                        cmd.Parameters.AddWithValue("@_StoreCode", HSMDetails.StoreCode);
                        cmd.Parameters.AddWithValue("@_CustomerName", string.IsNullOrEmpty(HSMReqest.CustomerName) ? "" : HSMReqest.CustomerName);
                        cmd.Parameters.AddWithValue("@_CustomerMobileNo", string.IsNullOrEmpty(HSMDetails.MobileNo) ? "" : HSMDetails.MobileNo);
                        cmd.Parameters.AddWithValue("@_ShoppingBagID", HSMReqest.ShopingBagNo);
                        cmd.Parameters.AddWithValue("@_OrderNo", HSMReqest.OrderID);
                        cmd.Parameters.AddWithValue("@_ChatID", HSMDetails.ChatID);
                        cmd.Parameters.AddWithValue("@_TemplateName", string.IsNullOrEmpty(HSMDetails.TemplateName) ? "" : HSMDetails.TemplateName);
                        cmd.Parameters.AddWithValue("@_AdditionalInfo", string.IsNullOrEmpty(HSMDetails.AdditionalInfo) ? "" : HSMDetails.AdditionalInfo);
                        cmd.Parameters.AddWithValue("@_HSMText", string.IsNullOrEmpty(HSMDetails.HSMText) ? "" : HSMDetails.HSMText);
                        cmd.Parameters.AddWithValue("@_WeBotLink", string.IsNullOrEmpty(HSMReqest.WebBotLink) ? "" : HSMReqest.WebBotLink);
                        cmd.Parameters.AddWithValue("@_IsChatActive", Convert.ToInt16(HSMDetails.ChatActive));
                        cmd.Parameters.AddWithValue("@_IsBellActive", Convert.ToInt16(HSMDetails.MakebellActive));
                        cmd.Parameters.AddWithValue("@_IsHSMSent", Convert.ToInt16(HSMDetails.IsHSMSent));
                        cmd.Parameters.AddWithValue("@_ErrorMessage", string.IsNullOrEmpty(HSMDetails.ErrorMessage) ? "" : HSMDetails.ErrorMessage);
                        cmd.Parameters.AddWithValue("@_CreatedBy", HSMReqest.UserID);

                        cmd.CommandType = CommandType.StoredProcedure;
                        LogID = Convert.ToInt32(cmd.ExecuteScalar());

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
            return LogID;
        }

        public async Task<int> UpdateHSMOptions(string ActiveOptions, string InActiveOptions)
        {
            int Result = 0;
            try
            {

                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_HSUpdateHSMOption", conn))
                    {

                        if (conn != null && conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }

                        cmd.Parameters.AddWithValue("@_ActiveOptions", string.IsNullOrEmpty(ActiveOptions)?"": ActiveOptions.Trim(','));
                        cmd.Parameters.AddWithValue("@_InActiveOptions", string.IsNullOrEmpty(InActiveOptions) ? "" : InActiveOptions.Trim(',')) ;

                        cmd.CommandType = CommandType.StoredProcedure;
                        Result = Convert.ToInt32(cmd.ExecuteScalar());

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
            return Result;
        }
    }
}
