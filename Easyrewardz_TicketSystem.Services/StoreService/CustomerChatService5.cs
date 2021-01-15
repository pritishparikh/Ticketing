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
            //ClientCustomSendTextModel SendTextRequest = new ClientCustomSendTextModel();
            ClientCustomSendTextModelNew SendTextRequest = new ClientCustomSendTextModelNew();
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

                                SendTextRequest.To = customerChatMaster.MobileNo;// customerChatMaster.MobileNo.Length.Equals(10) ? "91" + customerChatMaster.MobileNo : customerChatMaster.MobileNo;
                                SendTextRequest.textToReply = ReInitiateMessage;
                                SendTextRequest.programCode = customerChatMaster.ProgramCode;
                                SendTextRequest.source = "cb";
                                string JsonRequest = JsonConvert.SerializeObject(SendTextRequest);

                                //ClientAPIResponse = CommonService.SendApiRequest(ClientAPIUrl + "api/ChatbotBell/SendText", JsonRequest);
                                ClientAPIResponse = await APICall.SendApiRequest(ClientAPIUrl + sendText, JsonRequest);

                                #endregion

                                #region call client api for making bell active

                                Dictionary<string, string> Params = new Dictionary<string, string>
                                {
                                   // { "Mobilenumber", customerChatMaster.MobileNo.Length.Equals(10) ? "91" + customerChatMaster.MobileNo : customerChatMaster.MobileNo },
                                    { "Mobilenumber",  customerChatMaster.MobileNo },
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


        /// <summary>
        /// Get Chat Notifications Details
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <param name="PageNo"></param>
        /// <returns></returns>
        public async Task<MobileNotificationModel> GetMobileNotificationsDetails(int TenantID, string ProgramCode, int UserID, int PageNo)
        {
            MobileNotificationModel Notifications = new MobileNotificationModel();
            List<MobileNotificationDetails> NotificationDetails = new List<MobileNotificationDetails>();
            DataTable schemaTable = new DataTable();
            DataTable schemaTable1 = new DataTable();
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSGetMobileNotificationsDetails", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_UserID", UserID);
                        cmd.Parameters.AddWithValue("@_PageNo", PageNo);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                schemaTable.Load(reader);
                                schemaTable1.Load(reader);

                                if (schemaTable.Rows.Count > 0)
                                {
                                    foreach(DataRow dr in schemaTable.Rows)
                                    {
                                        MobileNotificationDetails obj = new MobileNotificationDetails() {

                                            IndexID = dr["IndexID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IndexID"]),
                                            NotificationFor = dr["NotificationFor"] == DBNull.Value ? string.Empty : Convert.ToString(dr["NotificationFor"]),
                                            OrderID = dr["OrderID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrderID"]),
                                            AppointmentID = dr["AppointmentID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AppointmentID"]),
                                            ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                                            CustomerID = dr["CustomerID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerID"]),
                                            CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                            CustomerMobileNumber = dr["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerMobileNumber"]),
                                            Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                            IsRead = dr["IsRead"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsRead"]),
                                            CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),

                                        };

                                        NotificationDetails.Add(obj);
                                    }

                                   if(schemaTable1.Rows.Count > 0)
                                    {
                                        Notifications.UnReadNotiCount= schemaTable1.Rows[0]["UnReadNotiCount"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable1.Rows[0]["UnReadNotiCount"]);
                                        Notifications.TotalNotiCount = schemaTable1.Rows[0]["TotalNotiCount"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable1.Rows[0]["TotalNotiCount"]);
                                    }

                                    Notifications.ChatNotification = NotificationDetails;
                                }

                            }
                            else
                            {
                                Notifications.ChatNotification = new List<MobileNotificationDetails>();
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
                if (schemaTable1 != null)
                {
                    schemaTable.Dispose();
                }
            }

            return Notifications;
        }


        /// <summary>
        /// Update Chat sNotification
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <param name="IndexID"></param>
        /// <returns></returns>
        public async Task<int> UpdateMobileNotification(int TenantID, string ProgramCode, int UserID, string IndexID)
        {
            int Result = 0;

            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                       ("SP_UpdateHSMobileNotification", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_IndexID",string.IsNullOrEmpty(IndexID) ? "" : IndexID.TrimEnd(','));
                        cmd.Parameters.AddWithValue("@_UserID", UserID);
                       

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
