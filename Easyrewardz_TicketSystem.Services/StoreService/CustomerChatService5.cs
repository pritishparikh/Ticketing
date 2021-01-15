using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
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
    public partial class CustomerChatService : ICustomerChat
    {

        /// <summary>
        /// Reinitiate Chats
        /// </summary>
        /// <param name="CustomerChatMaster"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        public async Task<int> SaveReInitiateChatMessages(ReinitiateChatModel customerChatMaster, string ClientAPIUrl, string sendText, string MakeBellActive)
        {
            //MySqlCommand cmd = new MySqlCommand();
            int ChatID = 0;
            ClientCustomSendTextModel SendTextRequest = new ClientCustomSendTextModel();
            string ClientAPIResponse = string.Empty;
            string ReInitiateMessage = string.Empty;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_HSInsertReInitiateChat", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", customerChatMaster.TenantID);
                        cmd.Parameters.AddWithValue("@_StoreID", string.IsNullOrEmpty(customerChatMaster.StoreID) ? "" : customerChatMaster.StoreID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", customerChatMaster.ProgramCode);
                        cmd.Parameters.AddWithValue("@_CustomerID", string.IsNullOrEmpty(customerChatMaster.CustomerID) ? "" : customerChatMaster.CustomerID);
                        cmd.Parameters.AddWithValue("@_FirstName", string.IsNullOrEmpty(customerChatMaster.FirstName) ? "" : customerChatMaster.FirstName);
                        cmd.Parameters.AddWithValue("@_LastName", string.IsNullOrEmpty(customerChatMaster.LastName) ? "" : customerChatMaster.LastName);
                        cmd.Parameters.AddWithValue("@_CustomerMobileNumber", string.IsNullOrEmpty(customerChatMaster.MobileNo) ? "" : customerChatMaster.MobileNo);
                        cmd.Parameters.AddWithValue("@_StoreManagerId", customerChatMaster.CreatedBy);
                        cmd.Parameters.AddWithValue("@_ChatTicketID", customerChatMaster.ChatTicketID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ChatID = reader["Chat_ID"] == DBNull.Value ? 0 : reader.GetInt32(reader.GetOrdinal("Chat_ID"));
                                    ReInitiateMessage = reader["ReinitiateMsg"] == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("ReinitiateMsg")); 
                                }
                            }

                            if (ChatID > 0 && !string.IsNullOrEmpty(ReInitiateMessage))
                            {

                                #region call client api for sending message to customer

                                SendTextRequest.To = customerChatMaster.MobileNo.Length.Equals(10) ? "91" + customerChatMaster.MobileNo : customerChatMaster.MobileNo;
                                SendTextRequest.textToReply = ReInitiateMessage;
                                SendTextRequest.programCode = customerChatMaster.ProgramCode;
                                string JsonRequest = JsonConvert.SerializeObject(SendTextRequest);

                                //ClientAPIResponse = CommonService.SendApiRequest(ClientAPIUrl + "api/ChatbotBell/SendText", JsonRequest);
                                ClientAPIResponse = await APICall.SendApiRequest(ClientAPIUrl + sendText, JsonRequest);

                                #endregion

                                #region call client api for making bell active

                                Dictionary<string, string> Params = new Dictionary<string, string>
                                {
                                    { "Mobilenumber", customerChatMaster.MobileNo.Length.Equals(10) ? "91" + customerChatMaster.MobileNo : customerChatMaster.MobileNo },
                                    { "ProgramCode", customerChatMaster.ProgramCode }
                                };

                                try
                                {

                                    string ActiveBell  = await APICall.SendApiRequestParams(ClientAPIUrl + MakeBellActive, Params);

                                }
                                catch (Exception) { }

                                //string ActiveBell = CommonService.SendParamsApiRequest(ClientAPIUrl + MakeBellActive, Params);
                                #endregion
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
    }
}
