using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public partial class CustomerChatService: ICustomerChat
    {
        /// <summary>
        /// update customer chat session
        /// </summary>
        /// <param name="ChatSessionValue"></param>
        /// <param name="ChatSessionDuration"></param>
        /// <param name="ChatDisplayValue"></param>
        /// <param name="ChatDisplayDuration"></param>
        /// <returns></returns>
        public int UpdateChatSession(int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue, string ChatDisplayDuration, int ModifiedBy)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_UpdateChatSessionMaster"
                };
                cmd.Parameters.AddWithValue("@_ChatSessionValue", ChatSessionValue);
                cmd.Parameters.AddWithValue("@_ChatSessionDuration", ChatSessionDuration);
                cmd.Parameters.AddWithValue("@_ChatDisplayValue", ChatDisplayValue);
                cmd.Parameters.AddWithValue("@_ChatDisplayDuration", ChatDisplayDuration);
                cmd.Parameters.AddWithValue("@_ModifiedBy", ModifiedBy);

                success = Convert.ToInt32(cmd.ExecuteScalar());
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
        /// <returns></returns>
        public ChatSessionModel GetChatSession()
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            ChatSessionModel ChatSession = new ChatSessionModel();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_GetChatSession", conn);
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {


                        ChatSession.ProgramCode = ds.Tables[0].Rows[0]["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ProgramCode"]);
                        ChatSession.TenantID = ds.Tables[0].Rows[0]["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["TenantID"]);
                        ChatSession.ChatSessionValue = ds.Tables[0].Rows[0]["ChatSessionValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ChatSessionValue"]);
                        ChatSession.ChatSessionDuration = ds.Tables[0].Rows[0]["ChatSessionDuration"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ChatSessionDuration"]);
                        ChatSession.ChatDisplayValue = ds.Tables[0].Rows[0]["ChatDisplayValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ChatDisplayValue"]);
                        ChatSession.ChatDisplayDuration = ds.Tables[0].Rows[0]["ChatDisplayDuration"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ChatDisplayDuration"]);

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

            return ChatSession;
        }


        /// <summary>
        /// get recent chat history of agents
        /// </summary>
        /// 
        public List<AgentRecentChatHistory> GetAgentRecentChat()
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<AgentRecentChatHistory> RecentChatsList = new List<AgentRecentChatHistory>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetAgentRecentChat", conn);
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            AgentRecentChatHistory obj = new AgentRecentChatHistory()
                            {
                                ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                                Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),
                                ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                                TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),

                            };

                            RecentChatsList.Add(obj);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return RecentChatsList;
        }


        /// <summary>
        /// Get Agent Chat History
        /// <param name="TenantId"></param>
        /// <param name="StoreManagerID"></param>
        /// <param name="ProgramCode"></param>
        /// </summary>
        /// 
        public List<AgentCustomerChatHistory> GetAgentChatHistory(int TenantId, int StoreManagerID, string ProgramCode)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<AgentCustomerChatHistory> ChatsList = new List<AgentCustomerChatHistory>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_GetAgentChatHistory", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_programCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_StoreManagerID", StoreManagerID);

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            AgentCustomerChatHistory obj = new AgentCustomerChatHistory()
                            {
                                ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                                Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),

                                CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                                CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]).Trim(),
                                ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                                TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),

                            };

                            ChatsList.Add(obj);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return ChatsList;
        }


        /// <summary>
        /// Get Agent List For Ongoin Chat
        /// </summary>
        /// 
        public List<AgentRecentChatHistory> GetAgentList(int TenantID)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<AgentRecentChatHistory> AgentList = new List<AgentRecentChatHistory>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetStoreManagerList", conn);
                cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            AgentRecentChatHistory obj = new AgentRecentChatHistory()
                            {
                              
                                StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                AgentName = dr["StoreManagerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreManagerName"]),
                               

                            };

                            AgentList.Add(obj);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return AgentList;
        }

        /// <summary>
        /// Insert Card Image Upload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        ///  <param name="ItemID"></param>
        ///   <param name="ImageUrl"></param>
        ///    <param name="CreatedBy"></param>
        /// <returns></returns>
        /// 
        public int InsertCardImageUpload(int TenantID, string ProgramCode, string ItemID, string ImageUrl, int CreatedBy)
        {
            int success = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_HSInsertCardImageUpload"
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_ItemID", ItemID);
                cmd.Parameters.AddWithValue("@_ImageUrl",string.IsNullOrEmpty(ImageUrl) ? "" : ImageUrl);
                cmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);

                success = Convert.ToInt32(cmd.ExecuteScalar());
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
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        ///  <param name="ItemID"></param>
        ///   <param name="AddToLibrary"></param>
        ///   <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int ApproveRejectCardImage(int ID,int TenantID, string ProgramCode, string ItemID, bool AddToLibrary, int ModifiedBy)
        {
            int success = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_HSApproveRejectCardImage"
                };
                cmd.Parameters.AddWithValue("@_ID", ID);
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_ItemID", ItemID);
                cmd.Parameters.AddWithValue("@_IsAddToLibrary", Convert.ToInt16(AddToLibrary));
                cmd.Parameters.AddWithValue("@_ModifiedBy", ModifiedBy);

                success = Convert.ToInt32(cmd.ExecuteScalar());
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
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// </summary>
        /// <returns></returns>
        public List<ChatCardImageUploadModel> GetCardImageUploadlog(int ListingFor, int TenantID, string ProgramCode)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<ChatCardImageUploadModel> CardImageLog = new List<ChatCardImageUploadModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetCardImageUploadlog", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_ListingFor", ListingFor);

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ChatCardImageUploadModel obj = new ChatCardImageUploadModel()
                            {
                                ImageUploadLogID = dr["ImageUploadLogID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ImageUploadLogID"]),
                                TenantID = dr["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TenantID"]),
                                ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                ItemID = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                ImageURL = dr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ImageURL"]),

                                StoreID = dr["StoreID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreID"]),
                                StoreCode = dr["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreCode"]),
                                StoreName = dr["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreName"]),
                                StoreAddress = dr["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreAddress"]),

                                IsAddedToLibrary = dr["IsAddedToLibrary"] == DBNull.Value ?false: Convert.ToBoolean(dr["IsAddedToLibrary"]),
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

            return CardImageLog;
        }

    }
}
