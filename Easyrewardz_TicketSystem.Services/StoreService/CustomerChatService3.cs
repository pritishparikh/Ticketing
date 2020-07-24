using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        public int UpdateChatSession(ChatSessionModel Chat)
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

                cmd.Parameters.AddWithValue("@_TenantID", Chat.TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", Chat.ProgramCode);
                cmd.Parameters.AddWithValue("@_ChatSessionValue", Chat.ChatSessionValue);
                cmd.Parameters.AddWithValue("@_ChatSessionDuration", Chat.ChatSessionDuration);
                cmd.Parameters.AddWithValue("@_ChatDisplayValue", Chat.ChatDisplayValue);
                cmd.Parameters.AddWithValue("@_ChatDisplayDuration", Chat.ChatDisplayDuration); 
                cmd.Parameters.AddWithValue("@_ChatCharLimit", Chat.ChatCharLimit);

                cmd.Parameters.AddWithValue("@_Message",Convert.ToInt16(Chat.Message));
                cmd.Parameters.AddWithValue("@_Card", Convert.ToInt16(Chat.Card));
                cmd.Parameters.AddWithValue("@_RecommendedList", Convert.ToInt16(Chat.RecommendedList));
                cmd.Parameters.AddWithValue("@_ScheduleVisit", Convert.ToInt16(Chat.ScheduleVisit));
                cmd.Parameters.AddWithValue("@_PaymentLink", Convert.ToInt16(Chat.PaymentLink));
                cmd.Parameters.AddWithValue("@_CustomerProfile", Convert.ToInt16(Chat.CustomerProfile));
                cmd.Parameters.AddWithValue("@_CustomerProduct", Convert.ToInt16(Chat.CustomerProduct));
                cmd.Parameters.AddWithValue("@_ModifiedBy", Chat.ModifiedBy);

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
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public ChatSessionModel GetChatSession(int TenantId, string ProgramCode)
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
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
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
                        ChatSession.ChatCharLimit= ds.Tables[0].Rows[0]["ChatTextLimit"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ChatTextLimit"]);

                        ChatSession.Message = ds.Tables[0].Rows[0]["Message"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Message"]);
                        ChatSession.Card = ds.Tables[0].Rows[0]["Card"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Card"]);
                        ChatSession.RecommendedList = ds.Tables[0].Rows[0]["RecommendedList"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["RecommendedList"]);
                        ChatSession.ScheduleVisit = ds.Tables[0].Rows[0]["ScheduleVisit"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["ScheduleVisit"]);
                        ChatSession.PaymentLink = ds.Tables[0].Rows[0]["PaymentLink"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["PaymentLink"]);
                        ChatSession.CustomerProfile = ds.Tables[0].Rows[0]["CustomerProfile"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["CustomerProfile"]);
                        ChatSession.CustomerProduct = ds.Tables[0].Rows[0]["CustomerProduct"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["CustomerProduct"]);

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
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public List<AgentRecentChatHistory> GetAgentRecentChat(int TenantId,string ProgramCode, int CustomerID)
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
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_programCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
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
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="StoreManagerID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>        
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
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<AgentRecentChatHistory> GetAgentList(int TenantID, int UserID)
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
                cmd.Parameters.AddWithValue("@_UserID", UserID);
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
                                AgentName = dr["StoreManagerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreManagerName"]).Trim(),
                               

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
        /// <param name="ClientAPIUrl"></param>
        /// <param name="SearchText"></param>
        /// <param name="ItemID"></param>
        /// <param name="ImageUrl"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int InsertCardImageUpload(int TenantID, string ProgramCode,string ClientAPIUrl, string SearchText, string ItemID, string ImageUrl, int CreatedBy)
        {
            int success = 0;
            List<CustomItemSearchResponseModel> ItemList = new List<CustomItemSearchResponseModel>();
            CustomItemSearchResponseModel CardItemDetails = new CustomItemSearchResponseModel();
            string ClientAPIResponse = string.Empty;

            //string FTPUrl = string.Empty;
            try
            {


                #region call cardimage API for getting ftp link

                //string JsonRequest = JsonConvert.SerializeObject(new { programCode = ProgramCode });

                //ClientAPIResponse = CommonService.SendApiRequest(ClientAPIUrl + "api/ChatbotBell/GetItemImageUrl", JsonRequest);

                //if (!string.IsNullOrEmpty(ClientAPIResponse))
                //{
                //    DictFtp = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClientAPIResponse);

                //    if (DictFtp.Count > 0)
                //    {
                //        FTPUrl = DictFtp["imageUrl"];
                //        if (!string.IsNullOrEmpty(FTPUrl))
                //        {
                //            ImageUrl = FTPUrl + Path.GetFileName(ImageFilePath);

                //        }
                //    }
                //}

                #endregion

                #region call client api for getting item list

                string JsonRequest = JsonConvert.SerializeObject(new ClientCustomGetItemOnSearch() { programcode= ProgramCode , searchCriteria= SearchText });


                ClientAPIResponse = CommonService.SendApiRequest(ClientAPIUrl + "api/ChatbotBell/GetItemsByArticlesSKUID", JsonRequest);

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


                    success = Convert.ToInt32(cmd.ExecuteScalar());

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
        public int ApproveRejectCardImage(int ID,int TenantID, string ProgramCode, string ItemID, bool AddToLibrary,string RejectionReason, int ModifiedBy)
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
                cmd.Parameters.AddWithValue("@_RejectionReason", RejectionReason);
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
        /// </summary>
        /// <param name="ListingFor"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
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
                                RejectionReason = dr["RejectionReason"] == DBNull.Value ? string.Empty : Convert.ToString(dr["RejectionReason"]),
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



        /// <summary>
        /// Insert New CardItem Configuration
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CardItem"></param>
        /// <param name="IsEnabled"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int InsertNewCardItemConfiguration(int TenantID, string ProgramCode, string CardItem, bool IsEnabled, int CreatedBy)
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
                    CommandText = "SP_HSInsertCardItemConfiguration"
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CardItem", CardItem);
                cmd.Parameters.AddWithValue("@_IsEnabled", Convert.ToInt16(IsEnabled));
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
        /// Update Card Item Configuration
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="EnabledCardItems"></param>
        /// <param name="DisabledCardItems"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int UpdateCardItemConfiguration(int TenantID, string ProgramCode, string EnabledCardItems, string DisabledCardItems, int ModifiedBy)
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
                    CommandText = "SP_HSUpdateCardItemConfiguration"
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_EnabledCardItems", string.IsNullOrEmpty(EnabledCardItems) ? "" : EnabledCardItems.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_DisabledCardItems", string.IsNullOrEmpty(DisabledCardItems) ? "" : DisabledCardItems.TrimEnd(','));
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
        /// Get Card Configuration List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public List<ChatCardConfigurationModel> GetCardConfiguration(int TenantID, string ProgramCode)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<ChatCardConfigurationModel> CardConfigList = new List<ChatCardConfigurationModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetCardConfigurationList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
              

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
        public int UpdateStoreManagerChatStatus(int TenantID, string ProgramCode, int ChatID, int ChatStatusID, int StoreManagerID)
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
                    CommandText = "SP_UpdateStoreManagerChatStatus"
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_ChatID", ChatID);
                cmd.Parameters.AddWithValue("@_ChatStatus", ChatStatusID);
                cmd.Parameters.AddWithValue("@_StoreManagerID", StoreManagerID);

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
        /// Update Card Image Approval
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int UpdateCardImageApproval(int TenantID, string ProgramCode, int ID, int ModifiedBy)
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
                    CommandText = "SP_HSUpdateCardImageApproval"
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_ID", ID);
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
        /// Get Card Image Approval List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public List<CardImageApprovalModel> GetCardImageApprovalList(int TenantID, string ProgramCode)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<CardImageApprovalModel> CardApprovalist = new List<CardImageApprovalModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetCardImageApproval", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);


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
        public int EndCustomerChat(int TenantID, string ProgramCode, int ChatID, string EndChatMessage,int UserID)
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
                    CommandText = "SP_HSEndCustomerChat"
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_ChatID", ChatID);
                cmd.Parameters.AddWithValue("@_EndChatMessage", string.IsNullOrEmpty(EndChatMessage) ? "" : EndChatMessage);
                cmd.Parameters.AddWithValue("@_UserID", UserID);

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

    }
}
