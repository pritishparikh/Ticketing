using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class CustomerChatService : ICustomerChat
    {
        /// <summary>
        /// update customer chat session
        /// </summary>
        /// <param name="ChatSessionValue"></param>
        /// <param name="ChatSessionDuration"></param>
        /// <param name="ChatDisplayValue"></param>
        /// <param name="ChatDisplayDuration"></param>
        /// <returns></returns>
        public async Task<int> UpdateChatSession(ChatSessionModel Chat)
        {
            int success = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_UpdateChatSessionMaster", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", Chat.TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", Chat.ProgramCode);
                        cmd.Parameters.AddWithValue("@_AgentChatSessionValue", Chat.AgentChatSessionValue);
                        cmd.Parameters.AddWithValue("@_AgentChatSessionDuration", Chat.AgentChatSessionDuration);
                        cmd.Parameters.AddWithValue("@_CustomerChatSessionValue", Chat.CustomerChatSessionValue);
                        cmd.Parameters.AddWithValue("@_CustomerChatSessionDuration", Chat.CustomerChatSessionDuration);
                        cmd.Parameters.AddWithValue("@_ChatDisplayValue", Chat.ChatDisplayValue);
                        cmd.Parameters.AddWithValue("@_ChatDisplayDuration", Chat.ChatDisplayDuration);
                        cmd.Parameters.AddWithValue("@_ChatCharLimit", Chat.ChatCharLimit);
                        cmd.Parameters.AddWithValue("@_Message", Convert.ToInt16(Chat.Message));
                        cmd.Parameters.AddWithValue("@_Card", Convert.ToInt16(Chat.Card));
                        cmd.Parameters.AddWithValue("@_RecommendedList", Convert.ToInt16(Chat.RecommendedList));
                        cmd.Parameters.AddWithValue("@_ScheduleVisit", Convert.ToInt16(Chat.ScheduleVisit));
                        cmd.Parameters.AddWithValue("@_PaymentLink", Convert.ToInt16(Chat.PaymentLink));
                        cmd.Parameters.AddWithValue("@_CustomerProfile", Convert.ToInt16(Chat.CustomerProfile));
                        cmd.Parameters.AddWithValue("@_CustomerProduct", Convert.ToInt16(Chat.CustomerProduct));
                        cmd.Parameters.AddWithValue("@_ModifiedBy", Chat.ModifiedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }


        /// <summary>
        /// get customer chat session
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public async Task<ChatSessionModel> GetChatSession(int TenantId, string ProgramCode)
        {
            ChatSessionModel ChatSession = new ChatSessionModel();
            DataTable dt = new DataTable();
            try
            {
                using (conn)
                {
                    using (MySqlCommand command = new MySqlCommand("SP_GetChatSession", conn))
                    {
                        await conn.OpenAsync();
                        command.Parameters.AddWithValue("@_TenantID", TenantId);
                        command.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = await command.ExecuteReaderAsync())
                        {

                            if (reader.HasRows)
                            {
                                dt.Load(reader);

                                if(dt.Rows.Count > 0)
                                {

                                    ChatSession.ProgramCode = dt.Rows[0]["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[0]["ProgramCode"]);
                                    ChatSession.TenantID = dt.Rows[0]["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["TenantID"]);
                                    ChatSession.CustomerChatSessionValue = dt.Rows[0]["ChatSessionValue"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ChatSessionValue"]);
                                    ChatSession.CustomerChatSessionDuration = dt.Rows[0]["ChatSessionDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[0]["ChatSessionDuration"]);
                                    ChatSession.AgentChatSessionValue = dt.Rows[1]["ChatSessionValue"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[1]["ChatSessionValue"]);
                                    ChatSession.AgentChatSessionDuration = dt.Rows[1]["ChatSessionDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[1]["ChatSessionDuration"]);
                                    ChatSession.ChatDisplayValue = dt.Rows[0]["ChatDisplayValue"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ChatDisplayValue"]);
                                    ChatSession.ChatDisplayDuration = dt.Rows[0]["ChatDisplayDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[0]["ChatDisplayDuration"]);
                                    ChatSession.ChatCharLimit = dt.Rows[0]["ChatTextLimit"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ChatTextLimit"]);

                                    ChatSession.Message = dt.Rows[0]["Message"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["Message"]);
                                    ChatSession.Card = dt.Rows[0]["Card"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["Card"]);
                                    ChatSession.RecommendedList = dt.Rows[0]["RecommendedList"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["RecommendedList"]);
                                    ChatSession.ScheduleVisit = dt.Rows[0]["ScheduleVisit"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["ScheduleVisit"]);
                                    ChatSession.PaymentLink = dt.Rows[0]["PaymentLink"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["PaymentLink"]);
                                    ChatSession.CustomerProfile = dt.Rows[0]["CustomerProfile"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["CustomerProfile"]);
                                    ChatSession.CustomerProduct = dt.Rows[0]["CustomerProduct"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["CustomerProduct"]);
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
                if(dt!=null)
                {
                    dt.Dispose();
                }
            }

            return ChatSession;
        }

        /// <summary>
        /// update customer chat session
        /// </summary>
        /// <param name="ChatSessionValue"></param>
        /// <param name="ChatSessionDuration"></param>
        /// <param name="ChatDisplayValue"></param>
        /// <param name="ChatDisplayDuration"></param>
        /// <returns></returns>
        public async Task<int> UpdateChatSessionNew(ChatSessionModel Chat)
        {
            int success = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_UpdateChatSessionMaster_New", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", Chat.TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", Chat.ProgramCode);
                        cmd.Parameters.AddWithValue("@_AgentChatSessionValue", Chat.AgentChatSessionValue);
                        cmd.Parameters.AddWithValue("@_AgentChatSessionDuration", Chat.AgentChatSessionDuration);
                        cmd.Parameters.AddWithValue("@_CustomerChatSessionValue", Chat.CustomerChatSessionValue);
                        cmd.Parameters.AddWithValue("@_CustomerChatSessionDuration", Chat.CustomerChatSessionDuration);
                        cmd.Parameters.AddWithValue("@_ChatDisplayValue", Chat.ChatDisplayValue);
                        cmd.Parameters.AddWithValue("@_ChatDisplayDuration", Chat.ChatDisplayDuration);
                        cmd.Parameters.AddWithValue("@_ChatCharLimit", Chat.ChatCharLimit);
                        cmd.Parameters.AddWithValue("@_Message", Convert.ToInt16(Chat.Message));
                        cmd.Parameters.AddWithValue("@_Card", Convert.ToInt16(Chat.Card));
                        cmd.Parameters.AddWithValue("@_CardWithStoreCode", Convert.ToInt16(Chat.CardSearchStoreCode));
                        cmd.Parameters.AddWithValue("@_RecommendedList", Convert.ToInt16(Chat.RecommendedList));
                        cmd.Parameters.AddWithValue("@_ScheduleVisit", Convert.ToInt16(Chat.ScheduleVisit));
                        cmd.Parameters.AddWithValue("@_PaymentLink", Convert.ToInt16(Chat.PaymentLink));
                        cmd.Parameters.AddWithValue("@_CustomerProfile", Convert.ToInt16(Chat.CustomerProfile));
                        cmd.Parameters.AddWithValue("@_CustomerProduct", Convert.ToInt16(Chat.CustomerProduct));
                        cmd.Parameters.AddWithValue("@_ModifiedBy", Chat.ModifiedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }


        /// <summary>
        /// get customer chat session
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public async Task<ChatSessionModel> GetChatSessionNew(int TenantId, string ProgramCode)
        {
            ChatSessionModel ChatSession = new ChatSessionModel();
            DataTable dt = new DataTable();
            try
            {
                using (conn)
                {
                    using (MySqlCommand command = new MySqlCommand("SP_GetChatSession_New", conn))
                    {
                        await conn.OpenAsync();
                        command.Parameters.AddWithValue("@_TenantID", TenantId);
                        command.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = await command.ExecuteReaderAsync())
                        {

                            if (reader.HasRows)
                            {
                                dt.Load(reader);

                                if (dt.Rows.Count > 0)
                                {

                                    ChatSession.ProgramCode = dt.Rows[0]["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[0]["ProgramCode"]);
                                    ChatSession.TenantID = dt.Rows[0]["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["TenantID"]);
                                    ChatSession.CustomerChatSessionValue = dt.Rows[0]["ChatSessionValue"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ChatSessionValue"]);
                                    ChatSession.CustomerChatSessionDuration = dt.Rows[0]["ChatSessionDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[0]["ChatSessionDuration"]);
                                    ChatSession.AgentChatSessionValue = dt.Rows[1]["ChatSessionValue"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[1]["ChatSessionValue"]);
                                    ChatSession.AgentChatSessionDuration = dt.Rows[1]["ChatSessionDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[1]["ChatSessionDuration"]);
                                    ChatSession.ChatDisplayValue = dt.Rows[0]["ChatDisplayValue"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ChatDisplayValue"]);
                                    ChatSession.ChatDisplayDuration = dt.Rows[0]["ChatDisplayDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[0]["ChatDisplayDuration"]);
                                    ChatSession.ChatCharLimit = dt.Rows[0]["ChatTextLimit"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ChatTextLimit"]);

                                    ChatSession.Message = dt.Rows[0]["Message"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["Message"]);
                                    ChatSession.Card = dt.Rows[0]["Card"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["Card"]);
                                    ChatSession.CardSearchStoreCode = dt.Rows[0]["CardSearchWithStoreCode"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["CardSearchWithStoreCode"]);
                                    ChatSession.RecommendedList = dt.Rows[0]["RecommendedList"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["RecommendedList"]);
                                    ChatSession.ScheduleVisit = dt.Rows[0]["ScheduleVisit"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["ScheduleVisit"]);
                                    ChatSession.PaymentLink = dt.Rows[0]["PaymentLink"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["PaymentLink"]);
                                    ChatSession.CustomerProfile = dt.Rows[0]["CustomerProfile"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["CustomerProfile"]);
                                    ChatSession.CustomerProduct = dt.Rows[0]["CustomerProduct"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["CustomerProduct"]);
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

            return ChatSession;
        }



        /// <summary>
        /// get recent chat history of agents
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public async Task<List<AgentRecentChatHistory>> GetAgentRecentChat(int TenantId, string ProgramCode, int CustomerID)
        {
            DataTable schemaTable = new DataTable();
            List<AgentRecentChatHistory> RecentChatsList = new List<AgentRecentChatHistory>();
            try
            {
                using (conn)
                {
                    using (MySqlCommand command = new MySqlCommand
                           ("SP_HSGetAgentRecentChat", conn))
                    {
                        await conn.OpenAsync();
                        command.Parameters.AddWithValue("@_TenantID", TenantId);
                        command.Parameters.AddWithValue("@_programCode", ProgramCode);
                        command.Parameters.AddWithValue("@_CustomerID", CustomerID);
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                schemaTable.Load(reader);
                                foreach (DataRow dr in schemaTable.Rows)
                                {
                                    AgentRecentChatHistory obj = new AgentRecentChatHistory()
                                    {
                                        ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                                        Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                        StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                        AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),
                                        CustomerMobile = dr["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerMobileNumber"]),
                                        ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                                        TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),
                                        ChatStatus = dr["ChatStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatStatus"]),
                                    };
                                    RecentChatsList.Add(obj);
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }

            return RecentChatsList;
        }

        /// <summary>
        /// get recent chat history of agents
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public async Task<List<AgentRecentChatHistory>> GetAgentRecentChatNew(int TenantId, string ProgramCode, int CustomerID, int PageNo)
        {
            DataTable schemaTable = new DataTable();
            List<AgentRecentChatHistory> RecentChatsList = new List<AgentRecentChatHistory>();
            try
            {
                using (conn)
                {
                    using (MySqlCommand command = new MySqlCommand
                           ("SP_HSGetAgentRecentChatNew", conn))
                    {
                        await conn.OpenAsync();
                        command.Parameters.AddWithValue("@_TenantID", TenantId);
                        command.Parameters.AddWithValue("@_programCode", ProgramCode);
                        command.Parameters.AddWithValue("@_CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@_pageNo", PageNo);
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                schemaTable.Load(reader);
                                foreach (DataRow dr in schemaTable.Rows)
                                {
                                    AgentRecentChatHistory obj = new AgentRecentChatHistory()
                                    {
                                        ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                                        Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                        StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                        AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),
                                        CustomerMobile = dr["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerMobileNumber"]),
                                        ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                                        TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),
                                        ChatStatus = dr["ChatStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatStatus"]),
                                    };
                                    RecentChatsList.Add(obj);
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }

            return RecentChatsList;
        }

        /// <summary>
        /// Get Agent Chat History
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="StoreManagerID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>        
        public async Task<List<AgentCustomerChatHistory>> GetAgentChatHistory(int TenantId, int StoreManagerID, string ProgramCode)
        {
            DataTable dt = new DataTable();
            List<AgentCustomerChatHistory> ChatsList = new List<AgentCustomerChatHistory>();
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_GetAgentChatHistory", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                        cmd.Parameters.AddWithValue("@_programCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_StoreManagerID", StoreManagerID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    AgentCustomerChatHistory obj = new AgentCustomerChatHistory()
                                    {
                                        ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                                        Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                        StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                        AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),

                                        CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                                        CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]).Trim(),
                                        CustomerMobile = dr["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerMobileNumber"]),
                                        ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                                        TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),
                                        ChatStatus = dr["ChatStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatStatus"]),

                                        ChatSourceID = dr["SourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SourceID"]),
                                        SourceName = dr["SourceName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceName"]),
                                        SourceAbbr = dr["SourceAbbr"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceAbbr"]),
                                        SourceIconUrl = dr["SourceIconUrl"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceIconUrl"]),
                                        ChatSourceIsActive = dr["SourceIsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["SourceIsActive"]),
                                    };

                                    ChatsList.Add(obj);
                                }
                            }
                        }
                    }
                }







                //if (conn != null && conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //using (conn)
                //{
                //    cmd = new MySqlCommand("SP_GetAgentChatHistory", conn);
                //    cmd.Connection = conn;
                //    cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                //    cmd.Parameters.AddWithValue("@_programCode", ProgramCode);
                //    cmd.Parameters.AddWithValue("@_StoreManagerID", StoreManagerID);
                //    cmd.Parameters.AddWithValue("@_pageNo", PageNo);

                //    cmd.CommandType = CommandType.StoredProcedure;

                //    MySqlDataAdapter da = new MySqlDataAdapter();
                //    da.SelectCommand = cmd;
                //    da.Fill(ds);

                //    if (ds != null && ds.Tables != null)
                //    {
                //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                //        {
                //            foreach (DataRow dr in ds.Tables[0].Rows)
                //            {
                //                AgentCustomerChatHistory obj = new AgentCustomerChatHistory()
                //                {
                //                    ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                //                    Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                //                    StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                //                    AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),

                //                    CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                //                    CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]).Trim(),
                //                    CustomerMobile = dr["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerMobileNumber"]),
                //                    ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                //                    TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),
                //                    ChatStatus = dr["ChatStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatStatus"]),
                //                };

                //                ChatsList.Add(obj);
                //            }
                //        }
                //    }
                //}
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

            return ChatsList;
        }

        /// <summary>
        /// Get Agent Chat History
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="StoreManagerID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>        
        public async Task<List<AgentCustomerChatHistory>> GetAgentChatHistoryNew(int TenantId, int StoreManagerID, string ProgramCode, int PageNo)
        {
            DataTable dt = new DataTable();
            List<AgentCustomerChatHistory> ChatsList = new List<AgentCustomerChatHistory>();
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_GetAgentChatHistoryNew", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                        cmd.Parameters.AddWithValue("@_programCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_StoreManagerID", StoreManagerID);
                        cmd.Parameters.AddWithValue("@_pageNo", PageNo);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    AgentCustomerChatHistory obj = new AgentCustomerChatHistory()
                                    {
                                        ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                                        Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                        StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                        AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),

                                        CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                                        CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]).Trim(),
                                        CustomerMobile = dr["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerMobileNumber"]),
                                        ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                                        TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),
                                        ChatStatus = dr["ChatStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatStatus"]),
                                    };

                                    ChatsList.Add(obj);
                                }
                            }
                        }
                    }
                }







                //if (conn != null && conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //using (conn)
                //{
                //    cmd = new MySqlCommand("SP_GetAgentChatHistory", conn);
                //    cmd.Connection = conn;
                //    cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                //    cmd.Parameters.AddWithValue("@_programCode", ProgramCode);
                //    cmd.Parameters.AddWithValue("@_StoreManagerID", StoreManagerID);
                //    cmd.Parameters.AddWithValue("@_pageNo", PageNo);

                //    cmd.CommandType = CommandType.StoredProcedure;

                //    MySqlDataAdapter da = new MySqlDataAdapter();
                //    da.SelectCommand = cmd;
                //    da.Fill(ds);

                //    if (ds != null && ds.Tables != null)
                //    {
                //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                //        {
                //            foreach (DataRow dr in ds.Tables[0].Rows)
                //            {
                //                AgentCustomerChatHistory obj = new AgentCustomerChatHistory()
                //                {
                //                    ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                //                    Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                //                    StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                //                    AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),

                //                    CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                //                    CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]).Trim(),
                //                    CustomerMobile = dr["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerMobileNumber"]),
                //                    ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                //                    TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),
                //                    ChatStatus = dr["ChatStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatStatus"]),
                //                };

                //                ChatsList.Add(obj);
                //            }
                //        }
                //    }
                //}
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

            return ChatsList;
        }


        /// <summary>
        /// Get Agent List For Ongoin Chat
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public async Task<List<AgentRecentChatHistory>> GetAgentList(int TenantID, int UserID)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<AgentRecentChatHistory> AgentList = new List<AgentRecentChatHistory>();
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSGetStoreManagerList", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_UserID", UserID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    AgentRecentChatHistory obj = new AgentRecentChatHistory()
                                    {
                                        StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                        AgentName = dr["StoreManagerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreManagerName"]).Trim(),
                                    };
                                    AgentList.Add(obj);
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

            return AgentList;
        }

        /// <summary>
        /// Insert Card Image Upload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <param name="SearchText"></param>
        /// <param name="ItemID"></param>
        /// <param name="ImageUrl"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public async Task<int> InsertCardImageUpload(int TenantID, string ProgramCode,string StoreCode, string ClientAPIUrl, string SearchText, string ItemID, string ImageUrl, int CreatedBy)
        {
            int success = 0;
            List<CustomItemSearchResponseModel> ItemList = new List<CustomItemSearchResponseModel>();
            CustomItemSearchResponseModel CardItemDetails = new CustomItemSearchResponseModel();
            string ClientAPIResponse = string.Empty;
            ClientCustomGetItemOnSearchnew SearchItemRequest = new ClientCustomGetItemOnSearchnew();

            //string FTPUrl = string.Empty;
            try
            {


                #region call client api for getting item list


                SearchItemRequest.programCode = ProgramCode;
                SearchItemRequest.storeCode = string.IsNullOrEmpty(StoreCode) ? "" : StoreCode;
                SearchItemRequest.searchtext = string.IsNullOrEmpty(SearchText) ? "" : SearchText;


                string JsonRequest = JsonConvert.SerializeObject(SearchItemRequest);

                //ClientAPIResponse = await APICall.SendApiRequest(ClientAPIUrl + "api/ChatbotBell/GetItemsByArticlesSKUID", JsonRequest);
                ClientAPIResponse = await APICall.SendApiRequest(ClientAPIUrl, JsonRequest);

                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    ItemList = JsonConvert.DeserializeObject<List<CustomItemSearchResponseModel>>(ClientAPIResponse);

                    if (ItemList.Count > 0)
                    {
                        CardItemDetails = ItemList.Where(x => x.uniqueItemCode.Equals(ItemID)).ToList().FirstOrDefault();
                    }
                }

                #endregion


                #region insert image log in DB

                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    using (conn)
                    {
                        using (MySqlCommand cmd = new MySqlCommand
                               ("SP_HSInsertCardImageUpload", conn))
                        {
                            await conn.OpenAsync();
                            cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                            cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                            cmd.Parameters.AddWithValue("@_ItemID", ItemID);
                            cmd.Parameters.AddWithValue("@_ImageUrl", string.IsNullOrEmpty(ImageUrl) ? "" : ImageUrl);
                            cmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);
                            //the params below are for inserting the details in item_master_inventory table (client table)
                            cmd.Parameters.AddWithValue("@_Category", string.IsNullOrEmpty(CardItemDetails.categoryName) ? "" : CardItemDetails.categoryName);
                            cmd.Parameters.AddWithValue("@_SubCategoryName", string.IsNullOrEmpty(CardItemDetails.subCategoryName) ? "" : CardItemDetails.subCategoryName);
                            cmd.Parameters.AddWithValue("@_BrandName", string.IsNullOrEmpty(CardItemDetails.brandName) ? "" : CardItemDetails.brandName);
                            cmd.Parameters.AddWithValue("@_Colour", string.IsNullOrEmpty(CardItemDetails.color) ? "" : CardItemDetails.color);
                            cmd.Parameters.AddWithValue("@_ColourCode", string.IsNullOrEmpty(CardItemDetails.colorCode) ? "" : CardItemDetails.colorCode);
                            cmd.Parameters.AddWithValue("@_Price", string.IsNullOrEmpty(CardItemDetails.price) ? "" : CardItemDetails.price);
                            cmd.Parameters.AddWithValue("@_Discount", string.IsNullOrEmpty(CardItemDetails.discount) ? "" : CardItemDetails.discount);
                            cmd.Parameters.AddWithValue("@_Size", string.IsNullOrEmpty(CardItemDetails.size) ? "" : CardItemDetails.size);
                            cmd.Parameters.AddWithValue("@_ItemName", string.IsNullOrEmpty(CardItemDetails.productName) ? "" : CardItemDetails.productName);
                            cmd.Parameters.AddWithValue("@_ProductURL", string.IsNullOrEmpty(CardItemDetails.url) ? "" : CardItemDetails.url);
                            cmd.CommandType = CommandType.StoredProcedure;
                            success = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                }
                #endregion
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
            return success;
        }

        /// <summary>
        /// Approve Reject Card Image
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ItemID"></param>
        /// <param name="AddToLibrary"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public async Task<int> ApproveRejectCardImage(int ID, int TenantID, string ProgramCode, string ItemID, bool AddToLibrary, string RejectionReason, int ModifiedBy)
        {
            int success = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSApproveRejectCardImage", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_ID", ID);
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@_IsAddToLibrary", Convert.ToInt16(AddToLibrary));
                        cmd.Parameters.AddWithValue("@_RejectionReason", RejectionReason);
                        cmd.Parameters.AddWithValue("@_ModifiedBy", ModifiedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }

        /// <summary>
        /// Get Card Image Upload log
        /// </summary>
        /// <param name="ListingFor"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public async Task<List<ChatCardImageUploadModel>> GetCardImageUploadlog(int ListingFor, int TenantID, string ProgramCode)
        {
            //MySqlCommand cmd = new MySqlCommand();
            DataTable dt = new DataTable();
            List<ChatCardImageUploadModel> CardImageLog = new List<ChatCardImageUploadModel>();
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSGetCardImageUploadlog", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_ListingFor", ListingFor);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    ChatCardImageUploadModel obj = new ChatCardImageUploadModel()
                                    {
                                        ImageUploadLogID = dr["ImageUploadLogID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ImageUploadLogID"]),
                                        TenantID = dr["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TenantID"]),
                                        ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                        ItemID = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                        ImageURL = dr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ImageURL"]),
                                        RejectionReason = dr["RejectionReason"] == DBNull.Value ? string.Empty : Convert.ToString(dr["RejectionReason"]),
                                        StoreID = dr["StoreID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreID"]),
                                        StoreCode = dr["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreCode"]),
                                        StoreName = dr["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreName"]),
                                        StoreAddress = dr["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreAddress"]),

                                        IsAddedToLibrary = dr["IsAddedToLibrary"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsAddedToLibrary"]),
                                        CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                        CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                        CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),

                                        ModifyBy = dr["ModifyBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                        ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                        ModifyDate = dr["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyDate"]),
                                    };
                                    CardImageLog.Add(obj);
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

            return CardImageLog;
        }



        /// <summary>
        /// Insert New CardItem Configuration
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CardItem"></param>
        /// <param name="IsEnabled"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public async Task<int> InsertNewCardItemConfiguration(int TenantID, string ProgramCode, string CardItem, bool IsEnabled, int CreatedBy)
        {
            int success = 0;
            try
            {

                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSInsertCardItemConfiguration", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_CardItem", CardItem);
                        cmd.Parameters.AddWithValue("@_IsEnabled", Convert.ToInt16(IsEnabled));
                        cmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }


        /// <summary>
        /// Update Card Item Configuration
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="EnabledCardItems"></param>
        /// <param name="DisabledCardItems"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateCardItemConfiguration(int TenantID, string ProgramCode, string EnabledCardItems, string DisabledCardItems, int ModifiedBy)
        {
            int success = 0;
            try
            { 
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSUpdateCardItemConfiguration", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_EnabledCardItems", string.IsNullOrEmpty(EnabledCardItems) ? "" : EnabledCardItems.TrimEnd(','));
                        cmd.Parameters.AddWithValue("@_DisabledCardItems", string.IsNullOrEmpty(DisabledCardItems) ? "" : DisabledCardItems.TrimEnd(','));
                        cmd.Parameters.AddWithValue("@_ModifiedBy", ModifiedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }

        /// <summary>
        /// Get Card Configuration List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public async Task<List<ChatCardConfigurationModel>> GetCardConfiguration(int TenantID, string ProgramCode)
        {
           // MySqlCommand cmd = new MySqlCommand();
            DataTable dt = new DataTable();
            List<ChatCardConfigurationModel> CardConfigList = new List<ChatCardConfigurationModel>();
            try
            {


                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSGetCardConfigurationList", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    ChatCardConfigurationModel obj = new ChatCardConfigurationModel()
                                    {
                                        CardItemID = dr["CardItemID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CardItemID"]),
                                        TenantID = dr["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TenantID"]),
                                        ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                        CardItem = dr["CardItem"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CardItem"]),
                                        IsEnabled = dr["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsEnabled"]),
                                        CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                        CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                        CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                        ModifyBy = dr["ModifyBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                        ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                        ModifyDate = dr["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyDate"]),
                                    };

                                    CardConfigList.Add(obj);
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

            return CardConfigList;
        }


        /// <summary>
        /// Update StoreManager chat status
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ChatID"></param>
        /// <param name="ChatStatusID"></param>
        /// <param name="StoreManagerID"></param>
        /// <returns></returns>
        public async Task<int> UpdateStoreManagerChatStatus(int TenantID, string ProgramCode, int ChatID, int ChatStatusID, int StoreManagerID)
        {
            int success = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_UpdateStoreManagerChatStatus", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_ChatID", ChatID);
                        cmd.Parameters.AddWithValue("@_ChatStatus", ChatStatusID);
                        cmd.Parameters.AddWithValue("@_StoreManagerID", StoreManagerID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }


        /// <summary>
        /// Update Card Image Approval
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateCardImageApproval(int TenantID, string ProgramCode, int ID, int ModifiedBy)
        {
            int success = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSUpdateCardImageApproval", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_ID", ID);
                        cmd.Parameters.AddWithValue("@_ModifiedBy", ModifiedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }

        /// <summary>
        /// Get Card Image Approval List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public async Task<List<CardImageApprovalModel>> GetCardImageApprovalList(int TenantID, string ProgramCode)
        {
            //MySqlCommand cmd = new MySqlCommand();
            DataTable dt = new DataTable();
            List<CardImageApprovalModel> CardApprovalist = new List<CardImageApprovalModel>();
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSGetCardImageApproval", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    CardImageApprovalModel obj = new CardImageApprovalModel()
                                    {
                                        ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                        TenantID = dr["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TenantID"]),
                                        ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                        ApprovalType = dr["ApprovalType"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ApprovalType"]),
                                        IsEnabled = dr["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsEnabled"]),
                                        CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                        CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                        CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                        ModifyBy = dr["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifiedBy"]),
                                        ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                        ModifyDate = dr["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedDate"]),
                                    };

                                    CardApprovalist.Add(obj);
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

            return CardApprovalist;
        }


        /// <summary>
        /// End Chat Form Customer
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ChatID"></param>
        /// <param name="EndChatMessage"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async  Task<int> EndCustomerChat(int TenantID, string ProgramCode, int ChatID, string EndChatMessage, int UserID)
        {
            int success = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSEndCustomerChat", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_ChatID", ChatID);
                        cmd.Parameters.AddWithValue("@_EndChatMessage", string.IsNullOrEmpty(EndChatMessage) ? "" : EndChatMessage);
                        cmd.Parameters.AddWithValue("@_UserID", UserID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }

    }
}
