using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class CustomerChatService : ICustomerChat
    {

        /// <summary>
        /// Get Customer Chat Details
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="ChatID"></param>
        /// <returns></returns>
        /// 
        public async Task<List<CustomerChatMessages>> GetChatMessageDetails(int tenantId, int ChatID, int ForRecentChat)
        {
            MySqlCommand cmd = new MySqlCommand();
            List<CustomerChatMessages> ChatList = new List<CustomerChatMessages>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_HSGetChatDetails", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_chatID", ChatID);
                    cmd.Parameters.AddWithValue("@_ForRecentChat", ForRecentChat);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {

                        while (dr.Read())
                        {

                            CustomerChatMessages obj = new CustomerChatMessages()
                            {
                                ChatID = Convert.ToInt32(dr["ChatID"]),
                                Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                Attachment = dr["Attachment"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Attachment"]),
                                CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                ByCustomer = dr["ByCustomer"] == DBNull.Value ? false : Convert.ToBoolean(dr["ByCustomer"]),
                                ChatStatus = dr["ChatStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatStatus"]),
                                StoreManagerId = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                CreateByName = dr["CreateByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreateByName"]),
                                ModifyBy = dr["ModifyBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                ChatDate = dr["ChatDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatDate"]),
                                ChatTime = dr["ChatTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatTime"]),
                                IsBotReply = dr["IsBotReply"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsBotReply"]),
                                IsAttachment = dr["IsAttachment"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsAttachment"])
                            };
                            ChatList.Add(obj);
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

            return ChatList;
        }

        /// <summary>
        /// Get Customer Chat Details
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="ChatID"></param>
        /// <returns></returns>
        /// 
        public async Task<ChatMessageDetails> GetChatMessageDetailsNew(int tenantId, int ChatID, int ForRecentChat, int PageNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            ChatMessageDetails ChatMessages = new ChatMessageDetails();
            List<CustomerChatMessages> ChatList = new List<CustomerChatMessages>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_HSGetChatDetailsNew", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_chatID", ChatID);
                    cmd.Parameters.AddWithValue("@_ForRecentChat", ForRecentChat);
                    cmd.Parameters.AddWithValue("@_pageNo", PageNo);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            CustomerChatMessages obj = new CustomerChatMessages()
                            {
                                ChatID = Convert.ToInt32(dr["ChatID"]),
                                Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                Attachment = dr["Attachment"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Attachment"]),
                                CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                ByCustomer = dr["ByCustomer"] == DBNull.Value ? false : Convert.ToBoolean(dr["ByCustomer"]),
                                ChatStatus = dr["ChatStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatStatus"]),
                                StoreManagerId = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                CreateByName = dr["CreateByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreateByName"]),
                                ModifyBy = dr["ModifyBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                ChatDate = dr["ChatDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatDate"]),
                                ChatTime = dr["ChatTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatTime"]),
                                IsBotReply = dr["IsBotReply"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsBotReply"]),
                                IsAttachment = dr["IsAttachment"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsAttachment"])
                            };
                            ChatList.Add(obj);
                        }
                        if (ChatList.Count > 0)
                        {
                            ChatMessages.ChatMessages = ChatList;
                        }
                        if(dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                ChatMessages.RecentChatCount = dr["RecentChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["RecentChatCount"]);
                                ChatMessages.PrevPage = dr["PrevPage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PrevPage"]);
                                ChatMessages.NextPage = dr["NextPage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NextPage"]);
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

            return ChatMessages;
        }


        /// <summary>
        /// Save Chat messages
        /// </summary>
        /// <param name="CustomerChatModel"></param>
        /// <returns></returns>
        public int SaveChatMessages(CustomerChatModel ChatMessageDetails)
        {

            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_HSInsertChatDetails", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_ChatID", ChatMessageDetails.ChatID);
                    cmd.Parameters.AddWithValue("@_Message", string.IsNullOrEmpty(ChatMessageDetails.Message) ? "" : ChatMessageDetails.Message);
                    cmd.Parameters.AddWithValue("@_ByCustomer", ChatMessageDetails.ByCustomer ? 1 : 2);
                    cmd.Parameters.AddWithValue("@_Status", !ChatMessageDetails.ByCustomer ? 0 : 1);
                    cmd.Parameters.AddWithValue("@_StoreManagerId", ChatMessageDetails.StoreManagerId);
                    cmd.Parameters.AddWithValue("@_Attachment", string.IsNullOrEmpty(ChatMessageDetails.Attachment) ? "" : ChatMessageDetails.Attachment);
                    cmd.Parameters.AddWithValue("@_CreatedBy", ChatMessageDetails.CreatedBy);
                    cmd.CommandType = CommandType.StoredProcedure;

                    resultCount = Convert.ToInt32(cmd.ExecuteScalar());
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return resultCount;
        }


        /// <summary>
        /// Search Item Details in Card Tab of Chat
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public List<CustomItemSearchResponseModel> ChatItemDetailsSearch(int TenantID, string Programcode, string ClientAPIURL, string SearchText)
        {

            List<CustomItemSearchResponseModel> ItemList = new List<CustomItemSearchResponseModel>();

            List<CustomItemSearchResponseModel> ApprovedImageItemList = new List<CustomItemSearchResponseModel>();
            ClientCustomGetItemOnSearch SearchItemRequest = new ClientCustomGetItemOnSearch();

            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            string ClientAPIResponse = string.Empty;
            string CardItemsIds = string.Empty;
            string SKUItemCodes = string.Empty;
            try
            {

                #region call client api for getting item list

                SearchItemRequest.programcode = Programcode;
                SearchItemRequest.searchCriteria = SearchText;

                try
                {
                    string JsonRequest = JsonConvert.SerializeObject(SearchItemRequest);

                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetItemsByArticlesSKUID", JsonRequest);

                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                    {
                        ItemList = JsonConvert.DeserializeObject<List<CustomItemSearchResponseModel>>(ClientAPIResponse);
                    }

                    if (ItemList.Count > 0)
                    {
                        SKUItemCodes = string.Join(',', ItemList.Where(x => string.IsNullOrEmpty(x.imageURL) && !string.IsNullOrEmpty(x.uniqueItemCode)).
                            Select(x => x.uniqueItemCode).ToList());

                        if (conn != null && conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (conn)
                        {
                            cmd = new MySqlCommand("SP_GetDisabledCardItemsConfiguration", conn);
                            cmd.Connection = conn;
                            cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                            cmd.Parameters.AddWithValue("@_ProgramCode", Programcode);
                            cmd.Parameters.AddWithValue("@_SKUItemCodes", string.IsNullOrEmpty(SKUItemCodes) ? "" : SKUItemCodes);
                            cmd.CommandType = CommandType.StoredProcedure;
                            MySqlDataAdapter da = new MySqlDataAdapter();
                            da.SelectCommand = cmd;
                            da.Fill(ds);

                            if (ds != null && ds.Tables != null)
                            {

                                #region disable card items from the API response

                                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    CardItemsIds = ds.Tables[0].Rows[0]["CardItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CardItemID"]);

                                    if (!string.IsNullOrEmpty(CardItemsIds))
                                    {
                                        string[] CardItemsIDArr = CardItemsIds.Split(new char[] { ',' });

                                        if (CardItemsIDArr.Contains("1"))
                                            ItemList = ItemList.Select(x => { x.uniqueItemCode = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("2"))
                                            ItemList = ItemList.Select(x => { x.categoryName = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("3"))
                                            ItemList = ItemList.Select(x => { x.subCategoryName = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("4"))
                                            ItemList = ItemList.Select(x => { x.brandName = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("5"))
                                            ItemList = ItemList.Select(x => { x.color = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("6"))
                                            ItemList = ItemList.Select(x => { x.size = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("7"))
                                            ItemList = ItemList.Select(x => { x.price = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("8"))
                                            ItemList = ItemList.Select(x => { x.url = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("9"))
                                            ItemList = ItemList.Select(x => { x.imageURL = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("10"))
                                            ItemList = ItemList.Select(x => { x.productName = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("11"))
                                            ItemList = ItemList.Select(x => { x.discount = ""; return x; }).ToList();

                                        if (CardItemsIDArr.Contains("12"))
                                            ItemList = ItemList.Select(x => { x.colorCode = ""; return x; }).ToList();


                                    }
                                }

                                #endregion

                                #region update imageUrl of the items whose card iamge upload has been approved

                                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                                {

                                    foreach (DataRow dr in ds.Tables[1].Rows)
                                    {
                                        ApprovedImageItemList.Add(new CustomItemSearchResponseModel()
                                        {

                                            uniqueItemCode = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                            imageURL = dr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ImageURL"]),

                                        });
                                    }

                                    if (ApprovedImageItemList.Count > 0)
                                    {
                                        var ApprovedList = ApprovedImageItemList.Select(x => x.uniqueItemCode).ToList();

                                        foreach (CustomItemSearchResponseModel carditem in ItemList)
                                        {
                                            if (ApprovedList.Contains(carditem.uniqueItemCode))
                                            {
                                                carditem.imageURL = ApprovedImageItemList.Where(x => x.uniqueItemCode.Equals(carditem.uniqueItemCode)).Select(x => x.imageURL).FirstOrDefault();
                                            }

                                        }
                                    }

                                }


                                #endregion

                            }
                        }



                    }

                }
                catch (Exception)
                {
                    throw;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return ItemList;
        }


        /// <summary>
        /// Search Item Details in Card Tab of Chat
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="SearchText"></param>
        ///  <param name="StoreCode"></param>
        /// <returns></returns>
        public async Task<List<CustomItemSearchResponseModel>> ChatItemDetailsSearchNew(int TenantID, string Programcode, string ClientAPIURL, string SearchText,string StoreCode)
        {
            ClientCustomGetItemOnSearchnew SearchItemRequest = new ClientCustomGetItemOnSearchnew();
            List<CustomItemSearchResponseModel> ItemList = new List<CustomItemSearchResponseModel>();
            string ClientAPIResponse = string.Empty;

            List<CustomItemSearchResponseModel> ApprovedImageItemList = new List<CustomItemSearchResponseModel>();
            string CardItemsIds = string.Empty;
            string SKUItemCodes = string.Empty;
            //DataTable DisabledCardDetailsDt = new DataTable();
            DataTable ApprovedCardImageDt = new DataTable();
            try
            {



                SearchItemRequest.programCode = Programcode;
                SearchItemRequest.storeCode = string.IsNullOrEmpty(StoreCode) ? "" : StoreCode;
                SearchItemRequest.searchtext = string.IsNullOrEmpty(SearchText) ? "" : SearchText;
                  string JsonRequest = JsonConvert.SerializeObject(SearchItemRequest);

                try
                {
                    ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL, JsonRequest);
                    //ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ElasticSearch/SearchItemDetails", JsonRequest);
                }
                catch(Exception)
                {
                    ClientAPIResponse = string.Empty;
                }

                // ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ElasticSearch/SearchItemDetails", JsonRequest);

                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                    {
                        ItemList = JsonConvert.DeserializeObject<List<CustomItemSearchResponseModel>>(ClientAPIResponse);
                    }

                #region commented code for image moderation and item enable/disable requirement removal


                if (ItemList.Count > 0)
                    {
                        SKUItemCodes = string.Join(',', ItemList.Where(x => string.IsNullOrEmpty(x.imageURL) && !string.IsNullOrEmpty(x.uniqueItemCode)).
                            Select(x => x.uniqueItemCode).ToList());

                        if (conn != null && conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        using (conn)
                        {
                            MySqlCommand cmd = new MySqlCommand("SP_GetDisabledCardItemsConfiguration", conn)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                            cmd.Parameters.AddWithValue("@_ProgramCode", Programcode);
                            cmd.Parameters.AddWithValue("@_SKUItemCodes", string.IsNullOrEmpty(SKUItemCodes) ? "" : SKUItemCodes);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (reader.HasRows)
                                {
                                    //DisabledCardDetailsDt.Load(reader);
                                    ApprovedCardImageDt.Load(reader);
                                }

                            #region disable card items from the API response


                            /*

                                            if (DisabledCardDetailsDt != null && DisabledCardDetailsDt.Rows.Count > 0)
                                            {
                                                CardItemsIds = DisabledCardDetailsDt.Rows[0]["CardItemID"] == DBNull.Value ? string.Empty : Convert.ToString(DisabledCardDetailsDt.Rows[0]["CardItemID"]);

                                                if (!string.IsNullOrEmpty(CardItemsIds))
                                                {
                                                    string[] CardItemsIDArr = CardItemsIds.Split(new char[] { ',' });

                                                    if (CardItemsIDArr.Contains("1"))
                                                        ItemList = ItemList.Select(x => { x.uniqueItemCode = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("2"))
                                                        ItemList = ItemList.Select(x => { x.categoryName = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("3"))
                                                        ItemList = ItemList.Select(x => { x.subCategoryName = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("4"))
                                                        ItemList = ItemList.Select(x => { x.brandName = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("5"))
                                                        ItemList = ItemList.Select(x => { x.color = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("6"))
                                                        ItemList = ItemList.Select(x => { x.size = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("7"))
                                                        ItemList = ItemList.Select(x => { x.price = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("8"))
                                                        ItemList = ItemList.Select(x => { x.url = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("9"))
                                                        ItemList = ItemList.Select(x => { x.imageURL = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("10"))
                                                        ItemList = ItemList.Select(x => { x.productName = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("11"))
                                                        ItemList = ItemList.Select(x => { x.discount = ""; return x; }).ToList();

                                                    if (CardItemsIDArr.Contains("12"))
                                                        ItemList = ItemList.Select(x => { x.colorCode = ""; return x; }).ToList();


                                                }
                                            }
                                             */
                            #endregion

                            #region update imageUrl of the items whose card iamge upload has been approved

                            if (ApprovedCardImageDt != null && ApprovedCardImageDt.Rows.Count > 0)
                                {

                                    foreach (DataRow dr in ApprovedCardImageDt.Rows)
                                    {
                                        ApprovedImageItemList.Add(new CustomItemSearchResponseModel()
                                        {

                                            uniqueItemCode = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                            imageURL = dr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ImageURL"]),

                                        });
                                    }

                                    if (ApprovedImageItemList.Count > 0)
                                    {
                                        var ApprovedList = ApprovedImageItemList.Select(x => x.uniqueItemCode).ToList();

                                        foreach (CustomItemSearchResponseModel carditem in ItemList)
                                        {
                                            if (ApprovedList.Contains(carditem.uniqueItemCode))
                                            {
                                                carditem.imageURL = ApprovedImageItemList.Where(x => x.uniqueItemCode.Equals(carditem.uniqueItemCode)).Select(x => x.imageURL).FirstOrDefault();
                                            }

                                        }
                                    }

                                }

                                #endregion

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

                //if (DisabledCardDetailsDt != null)
                //{
                //    DisabledCardDetailsDt.Dispose();
                //}

                if (ApprovedCardImageDt != null)
                {
                    ApprovedCardImageDt.Dispose();
                }
            }

            return ItemList;
        }


        /// <summary>
        /// Search Item Details in Card Tab of Chat WebBot
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="SearchText"></param>
        ///  <param name="StoreCode"></param>
        /// <returns></returns>
        public async Task<CustomItemSearchWBResponseModel> ChatItemDetailsSearchWB(int TenantID, string Programcode, string ClientAPIURL, string SearchText, string StoreCode)
        {
            ClientCustomGetItemOnSearchnew SearchItemRequest = new ClientCustomGetItemOnSearchnew();
            CustomItemSearchWBResponseModel ItemList = new CustomItemSearchWBResponseModel();
            string ClientAPIResponse = string.Empty;

            List<CustomItemSearchResponseModel> ApprovedImageItemList = new List<CustomItemSearchResponseModel>();
            string SKUItemCodes = string.Empty;
            //DataTable DisabledCardDetailsDt = new DataTable();
            DataTable ApprovedCardImageDt = new DataTable();

            try
            {
                SearchItemRequest.programCode = Programcode;
                SearchItemRequest.storeCode = string.IsNullOrEmpty(StoreCode) ? "" : StoreCode;
                SearchItemRequest.searchtext = string.IsNullOrEmpty(SearchText) ? "" : SearchText;
                string JsonRequest = JsonConvert.SerializeObject(SearchItemRequest);

                try
                {
                    ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL, JsonRequest);
                   
                }
                catch (Exception)
                {
                    ClientAPIResponse = string.Empty;
                }

                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    ItemList = JsonConvert.DeserializeObject<CustomItemSearchWBResponseModel>(ClientAPIResponse);
                }


                #region commented code for image moderation and item enable/disable requirement removal


                if (ItemList.items.Count > 0)
                {
                    SKUItemCodes = string.Join(',', ItemList.items.Where(x => string.IsNullOrEmpty(x.imageUrl) && !string.IsNullOrEmpty(x.itemCode)).
                        Select(x => x.itemCode).ToList());

                    if (conn != null && conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }
                    using (conn)
                    {
                        MySqlCommand cmd = new MySqlCommand("SP_GetDisabledCardItemsConfiguration", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", Programcode);
                        cmd.Parameters.AddWithValue("@_SKUItemCodes", string.IsNullOrEmpty(SKUItemCodes) ? "" : SKUItemCodes);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                ApprovedCardImageDt.Load(reader);
                            }

                           

                            #region update imageUrl of the items whose card iamge upload has been approved

                            if (ApprovedCardImageDt != null && ApprovedCardImageDt.Rows.Count > 0)
                            {

                                foreach (DataRow dr in ApprovedCardImageDt.Rows)
                                {
                                    ApprovedImageItemList.Add(new CustomItemSearchResponseModel()
                                    {

                                        uniqueItemCode = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                        imageURL = dr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ImageURL"]),

                                    });
                                }

                                if (ApprovedImageItemList.Count > 0)
                                {
                                    var ApprovedList = ApprovedImageItemList.Select(x => x.uniqueItemCode).ToList();

                                     foreach(Items carditem in  ItemList.items)
                                    {
                                        if (ApprovedList.Contains(carditem.itemCode))
                                        {
                                            carditem.imageUrl = ApprovedImageItemList.Where(x => x.uniqueItemCode.Equals(carditem.itemCode)).Select(x => x.imageURL).FirstOrDefault();
                                        }

                                    }
                                }

                            }

                            #endregion

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
            { }

            return ItemList;
        }

        /// <summary>
        /// Get Chat Suggestions
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public async Task<List<object>> GetChatSuggestions(string SearchText)
        {
            List<object> SuggestionsDetails = new List<object>();
            List<CustomerChatSuggestionModel> SuggestionList = new List<CustomerChatSuggestionModel>();
            List<ChatSuggestionTags> ChatSuggestionTagList = new List<ChatSuggestionTags>();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_HSGetChatSuggestions", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@SearchText", string.IsNullOrEmpty(SearchText) ? "" : SearchText.ToLower());
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            ChatSuggestionTags Tags = new ChatSuggestionTags()
                            {

                                TagID = dr["TagID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TagID"]),
                                TagName = dr["TagName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TagName"]),
                            };
                            ChatSuggestionTagList.Add(Tags);
                        }
                        if (ChatSuggestionTagList.Count > 0)
                        {
                            if(dr.NextResult())
                            {
                                while (dr.Read())
                                {
                                    CustomerChatSuggestionModel obj = new CustomerChatSuggestionModel()
                                    {
                                        SuggestionID = Convert.ToInt32(dr["SuggestionID"]),
                                        SuggestionText = dr["SuggestionText"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SuggestionText"]),
                                        TagID = dr["TagID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TagID"]),
                                        TagName = dr["Tags"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Tags"]),
                                    };
                                    SuggestionList.Add(obj);
                                }
                            }
              
                        }
                        SuggestionsDetails.Add(ChatSuggestionTagList);
                        SuggestionsDetails.Add(SuggestionList);
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
            return SuggestionsDetails;
        }


        /// <summary>
        /// Save Customer Chat reply 
        /// </summary>
        /// <param name="ChatMessageReply"></param>
        /// <returns></returns>
        public async Task<int> SaveCustomerChatMessageReply(CustomerChatReplyModel ChatReply)
        {
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;

            try
            {

                DateTime Now = !string.IsNullOrEmpty(ChatReply.DateTime) ? DateTime.ParseExact(ChatReply.DateTime, "dd MMM yyyy hh:mm:ss tt", CultureInfo.InvariantCulture) : DateTime.Now;

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_InsertCustomerChatReplyMessage", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_ProgramCode", ChatReply.ProgramCode);
                    cmd.Parameters.AddWithValue("@_StoreCode", ChatReply.StoreCode);
                    cmd.Parameters.AddWithValue("@_Mobile", ChatReply.Mobile);
                    cmd.Parameters.AddWithValue("@_Message", string.IsNullOrEmpty(ChatReply.Message) ? "" : ChatReply.Message);
                    cmd.Parameters.AddWithValue("@_ChatID", ChatReply.ChatID);
                    cmd.Parameters.AddWithValue("@_DateTime", Now);

                    cmd.CommandType = CommandType.StoredProcedure;

                    resultCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return resultCount;
        }

        /// <summary>
        /// send Recommendations To Customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        //public int SendRecommendationsToCustomer(int TenantID, string Programcode, int CustomerID, string MobileNo, string ClientAPIURL, int CreatedBy)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    int resultCount = 0; int Chat_ID = 0;
        //    string ProgramCode = string.Empty;
        //    List<CustomerRecommendatonModel> RecommendationsList = new List<CustomerRecommendatonModel>();
        //    DataSet ds = new DataSet();
        //    ClientCustomSendProductModel Details = new ClientCustomSendProductModel();
        //    string HtmlMessageContent = "<div class=\"card-body position-relative\"><div class=\"row\" style=\"margin: 0px; align-items: flex-end;\"><div class=\"col-md-2\"><img class=\"chat-product-img\" src=\"{0}\" alt=\"Product Image\" ></div><div class=\"col-md-10 bkcprdt\"><div><label class=\"chat-product-name\">Brand :{1}</label></div><div><label class=\"chat-product-code\">Category: {2}</label></div><div><label class=\"chat-product-code\">SubCategory: {3}</label></div><div><label class=\"chat-product-code\">Color: {4}</label></div><div><label class=\"chat-product-code\">Size: {5}</label></div><div><label class=\"chat-product-code\">Item Code: {6}</label></div><div><label class=\"chat-product-prize\"> Price : {7}</label></div><div><a href=\"{8}\" target=\"_blank\" class=\"chat-product-url\">{9}</a></div></div></div></div>";
        //    string ClientAPIResponse = string.Empty;
        //    string whatsAppContent = string.Empty;
        //    try
        //    {

        //        if (conn != null && conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }

        //        cmd = new MySqlCommand("SP_HSGetRecomendationsByCustomerID", conn);
        //        cmd.Connection = conn;
        //        cmd.Parameters.AddWithValue("@_TenantID", TenantID);
        //        cmd.Parameters.AddWithValue("@_prgCode", Programcode);
        //        cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
        //        cmd.Parameters.AddWithValue("@_MobileNo", MobileNo);

        //        cmd.CommandType = CommandType.StoredProcedure;

        //        MySqlDataAdapter da = new MySqlDataAdapter();
        //        da.SelectCommand = cmd;
        //        da.Fill(ds);

        //        if (ds != null && ds.Tables != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    CustomerRecommendatonModel obj = new CustomerRecommendatonModel()
        //                    {
        //                        Id = Convert.ToInt32(dr["Id"]),
        //                        ItemCode = dr["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemCode"]),
        //                        Category = dr["Category"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Category"]),
        //                        SubCategory = dr["SubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubCategory"]),
        //                        Brand = dr["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Brand"]),
        //                        Color = dr["Color"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Color"]),
        //                        Size = dr["Size"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Size"]),
        //                        Price = dr["Price"] == DBNull.Value ? "0" : Convert.ToString(dr["Price"]),
        //                        Url = dr["Url"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Url"]),
        //                        ImageURL = dr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ImageURL"]),
        //                        ItemName = dr["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemName"]),
        //                        IsShoppingBag = dr["IsShoppingBag"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsShoppingBag"]),
        //                        IsWishList = dr["IsWishList"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsWishList"]),
        //                        IsRecommended = dr["IsRecommended"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsRecommended"]),
        //                    };

        //                    RecommendationsList.Add(obj);
        //                }
        //            }

        //            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        //            {
        //                Chat_ID = ds.Tables[1].Rows[0]["Chat_ID"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["Chat_ID"]);
        //                ProgramCode = ds.Tables[1].Rows[0]["prgCode"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["prgCode"]);
        //            }
        //        }

        //        if (RecommendationsList.Count > 0 && Chat_ID > 0 && !string.IsNullOrEmpty(ProgramCode))
        //        {

        //            #region call client send text api for sending message to customer

        //            foreach (CustomerRecommendatonModel RecObj in RecommendationsList)
        //            {
        //                /*
        //                string whatsAppContent = "Brand: " + RecObj.Brand + ", Category: " + RecObj.Category + ", Sub Category: " + RecObj.SubCategory + ", Color: " + RecObj.Color + ", Size: " +
        //                                         RecObj.Size + ", Item Code: " + RecObj.ItemCode + ", Price: " + RecObj.Price + "  " + RecObj.Url;
        //                resultCount = resultCount + SendMessageToCustomer(Chat_ID, MobileNo.Length > 10 ? MobileNo : "91" + MobileNo, ProgramCode, RecObj.Url, whatsAppContent, RecObj.ImageURL, ClientAPIURL, CreatedBy, 0);
        //                */

        //                #region call client api for sending message to customer

        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.ItemCode) ? RecObj.ItemCode : " " + ",";
        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.ItemName) ? RecObj.ItemName : " " + ",";
        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.Category) ? RecObj.Category : " " + ",";
        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.SubCategory) ? RecObj.SubCategory : " " + ",";
        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.Brand) ? RecObj.Brand : " " + ",";
        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.Price) ? RecObj.Price : " " + ",";
        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.Color) ? RecObj.Color : " " + ",";
        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.Size) ? RecObj.Size : " " + ",";
        //                whatsAppContent += !string.IsNullOrEmpty(RecObj.ImageURL) ? RecObj.ImageURL : " " + ",";


        //                Details.to = MobileNo.Length > 10 ? MobileNo : "91" + MobileNo;
        //                Details.textToReply = whatsAppContent;
        //                Details.programCode = Programcode;
        //                Details.imageUrl = RecObj.ImageURL;
        //                Details.shoppingBag = RecObj.IsShoppingBag ? "1" : "0";
        //                Details.like = RecObj.IsWishList ? "1" : "0";

        //                string JsonRequest = JsonConvert.SerializeObject(Details);

        //                ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendCtaImage", JsonRequest);

        //                #endregion
        //            }

        //            #endregion



        //            foreach (CustomerRecommendatonModel RecObj in RecommendationsList)
        //            {

        //                string messagecontent = string.Format(HtmlMessageContent, RecObj.ImageURL, RecObj.Brand, RecObj.Category, RecObj.SubCategory, RecObj.Color
        //                                        , RecObj.Size, RecObj.ItemCode, RecObj.Price, RecObj.Url, RecObj.Url);

        //                CustomerChatModel ChatMessageDetails = new CustomerChatModel();
        //                ChatMessageDetails.ChatID = Chat_ID;
        //                ChatMessageDetails.Message = messagecontent;
        //                ChatMessageDetails.ByCustomer = false;
        //                ChatMessageDetails.ChatStatus = 0;
        //                ChatMessageDetails.StoreManagerId = CreatedBy;
        //                ChatMessageDetails.CreatedBy = CreatedBy;

        //                resultCount = resultCount + SaveChatMessages(ChatMessageDetails);

        //            }


        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //        }
        //    }
        //    return resultCount;
        //}


        /// <summary>
        /// send Recommendations To Customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        public int SendRecommendationsToCustomer(int ChatID, int TenantID, string Programcode, int CustomerID, string MobileNo, string ClientAPIURL, int CreatedBy)
        {
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;
            List<CustomerChatProductModel> CustomerProducts = new List<CustomerChatProductModel>();

            ClientCustomSendProductModel Details = new ClientCustomSendProductModel();
            string HtmlMessageContent = "<div class=\"card-body position-relative\"><div class=\"row\" style=\"margin: 0px; align-items: flex-end;\"><div class=\"col-md-2\"><img class=\"chat-product-img\" src=\"{0}\" alt=\"Product Image\" ></div><div class=\"col-md-10 bkcprdt\"><div><label class=\"chat-product-name\">Brand :{1}</label></div><div><label class=\"chat-product-code\">Category: {2}</label></div><div><label class=\"chat-product-code\">SubCategory: {3}</label></div><div><label class=\"chat-product-code\">Color: {4}</label></div><div><label class=\"chat-product-code\">Size: {5}</label></div><div><label class=\"chat-product-code\">Item Code: {6}</label></div><div><label class=\"chat-product-prize\"> Price : {7}</label></div><div><a href=\"{8}\" target=\"_blank\" class=\"chat-product-url\">{9}</a></div></div></div></div>";
            string ClientAPIResponse = string.Empty;
            string whatsAppContent = string.Empty;
            try
            {

                //CustomerProducts = GetChatCustomerProducts(TenantID, Programcode, CustomerID, MobileNo, ClientAPIURL);
                //CustomerProducts= CustomerProducts.Where(x => x.IsRecommended.Equals(true)).Take(5).ToList();

                if (CustomerProducts.Count > 0)
                {

                    #region call client send text api for sending message to customer

                    foreach (CustomerChatProductModel RecObj in CustomerProducts)
                    {


                        #region call client api for sending message to customer
                        whatsAppContent = string.Empty;
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.uniqueItemCode) ? RecObj.uniqueItemCode : " " + ",";
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.productName) ? RecObj.productName : " " + ",";
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.categoryName) ? RecObj.categoryName : " " + ",";
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.subCategoryName) ? RecObj.subCategoryName : " " + ",";
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.brandName) ? RecObj.brandName : " " + ",";
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.price) ? RecObj.price : " " + ",";
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.color) ? RecObj.color : " " + ",";
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.size) ? RecObj.size : " " + ",";
                        whatsAppContent += !string.IsNullOrEmpty(RecObj.imageURL) ? RecObj.imageURL : " " + ",";


                        Details.to = MobileNo.Length > 10 ? MobileNo : "91" + MobileNo;
                        Details.textToReply = whatsAppContent;
                        Details.programCode = Programcode;
                        Details.imageUrl = RecObj.imageURL;
                        Details.shoppingBag = RecObj.IsShoppingBag ? "1" : "0";
                        Details.like = RecObj.IsWishList ? "1" : "0";

                        string JsonRequest = JsonConvert.SerializeObject(Details);

                        ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendCtaImage", JsonRequest);

                        #endregion
                    }

                    #endregion


                    foreach (CustomerChatProductModel RecObj in CustomerProducts)
                    {

                        string messagecontent = string.Format(HtmlMessageContent, RecObj.imageURL, RecObj.brandName, RecObj.categoryName, RecObj.subCategoryName, RecObj.color
                                                , RecObj.size, RecObj.uniqueItemCode, RecObj.price, RecObj.url, RecObj.url);

                        CustomerChatModel ChatMessageDetails = new CustomerChatModel();
                        ChatMessageDetails.ChatID = ChatID;
                        ChatMessageDetails.Message = messagecontent;
                        ChatMessageDetails.ByCustomer = false;
                        ChatMessageDetails.ChatStatus = 0;
                        ChatMessageDetails.StoreManagerId = CreatedBy;
                        ChatMessageDetails.CreatedBy = CreatedBy;

                        resultCount = resultCount + SaveChatMessages(ChatMessageDetails);

                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();

            }
            return resultCount;
        }

        public async Task<int> SendRecommendationsToCustomerNew(int ChatID, int TenantID, string Programcode, int CustomerID, string MobileNo,
            string ClientAPIURL, string SendImageMessage, string Recommended, int CreatedBy, string Source)
        {
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;
            List<CustomerChatProductModel> CustomerProducts = new List<CustomerChatProductModel>();

            ClientCustomSendProductModelNew Details = new ClientCustomSendProductModelNew();
            string HtmlMessageContent = "<div class=\"card-body position-relative\"><div class=\"row\" style=\"margin: 0px; align-items: flex-end;\"><div class=\"col-md-2\"><img class=\"chat-product-img\" src=\"{0}\" alt=\"Product Image\" ></div><div class=\"col-md-10 bkcprdt\"><div><label class=\"chat-product-name\">Brand :{1}</label></div><div><label class=\"chat-product-code\">Category: {2}</label></div><div><label class=\"chat-product-code\">SubCategory: {3}</label></div><div><label class=\"chat-product-code\">Color: {4}</label></div><div><label class=\"chat-product-code\">Size: {5}</label></div><div><label class=\"chat-product-code\">Item Code: {6}</label></div><div><label class=\"chat-product-prize\"> Price : {7}</label></div><div><a href=\"{8}\" target=\"_blank\" class=\"chat-product-url\">{9}</a></div></div></div></div>";
            string ClientAPIResponse = string.Empty;
            string whatsAppContent = string.Empty;
            try
            {

                CustomerProducts = await GetChatCustomerProducts(TenantID, Programcode, CustomerID, MobileNo, ClientAPIURL);
                CustomerProducts= CustomerProducts.Where(x => x.IsRecommended.Equals(true)).Take(5).ToList();


                if (CustomerProducts.Count > 0)
                {

                    #region call client send text api for sending message to customer

                    foreach (CustomerChatProductModel RecObj in CustomerProducts)
                    {


                        #region call client api for sending message to customer
                        whatsAppContent = string.Empty;

                        if (Source == "wb")
                        {
                            whatsAppContent = RecObj.uniqueItemCode;
                        }
                        else
                        {

                            whatsAppContent += !string.IsNullOrEmpty(RecObj.uniqueItemCode) ? RecObj.uniqueItemCode : " " + ",";
                            whatsAppContent += !string.IsNullOrEmpty(RecObj.productName) ? RecObj.productName : " " + ",";
                            whatsAppContent += !string.IsNullOrEmpty(RecObj.categoryName) ? RecObj.categoryName : " " + ",";
                            whatsAppContent += !string.IsNullOrEmpty(RecObj.subCategoryName) ? RecObj.subCategoryName : " " + ",";
                            whatsAppContent += !string.IsNullOrEmpty(RecObj.brandName) ? RecObj.brandName : " " + ",";
                            whatsAppContent += !string.IsNullOrEmpty(RecObj.price) ? RecObj.price : " " + ",";
                            whatsAppContent += !string.IsNullOrEmpty(RecObj.color) ? RecObj.color : " " + ",";
                            whatsAppContent += !string.IsNullOrEmpty(RecObj.size) ? RecObj.size : " " + ",";
                            whatsAppContent += !string.IsNullOrEmpty(RecObj.imageURL) ? RecObj.imageURL : " " + ",";
                        }


                        Details.to = MobileNo; // MobileNo.Length > 10 ? MobileNo : "91" + MobileNo;
                        Details.textToReply = whatsAppContent;
                        Details.programCode = Programcode;
                        Details.imageUrl = RecObj.imageURL;
                        Details.shoppingBag = RecObj.IsShoppingBag ? "1" : "0";
                        Details.like = RecObj.IsWishList ? "1" : "0";
                        Details.source = Source;

                        string JsonRequest = JsonConvert.SerializeObject(Details);

                        ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL , JsonRequest);
                      //  ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendImageMessage", JsonRequest);

                        #endregion
                    }

                    #endregion


                    foreach (CustomerChatProductModel RecObj in CustomerProducts)
                    {

                        string messagecontent = string.Format(HtmlMessageContent, RecObj.imageURL, RecObj.brandName, RecObj.categoryName, RecObj.subCategoryName, RecObj.color
                                                , RecObj.size, RecObj.uniqueItemCode, RecObj.price, RecObj.url, RecObj.url);

                        CustomerChatModel ChatMessageDetails = new CustomerChatModel();
                        ChatMessageDetails.ChatID = ChatID;
                        ChatMessageDetails.Message = messagecontent;
                        ChatMessageDetails.ByCustomer = false;
                        ChatMessageDetails.ChatStatus = 0;
                        ChatMessageDetails.StoreManagerId = CreatedBy;
                        ChatMessageDetails.CreatedBy = CreatedBy;

                        resultCount = resultCount + SaveChatMessages(ChatMessageDetails);

                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();

            }
            return resultCount;
        }

        /// <summary>
        /// send Message To Customer
        /// </summary>
        /// <param name="ChatID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="Messsage"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int SendMessageToCustomer(int ChatID, string MobileNo, string ProgramCode, string Message, string WhatsAppMessage, string ImageURL, string ClientAPIURL, int CreatedBy, int InsertChat)
        {
            CustomerChatModel ChatMessageDetails = new CustomerChatModel();
            ClientCustomSendTextModel SendTextRequest = new ClientCustomSendTextModel();
            ClientCustomSendImageModel SendImageRequest = new ClientCustomSendImageModel();
            string ClientAPIResponse = string.Empty;
            string ClientImageAPIResponse = string.Empty;
            //bool isMessageSent = false;

            try
            {

                #region call client api for sending message to customer
                if (string.IsNullOrEmpty(ImageURL))
                {
                    SendTextRequest.To = MobileNo;
                    SendTextRequest.textToReply = Message;
                    SendTextRequest.programCode = ProgramCode;


                    string JsonRequest = JsonConvert.SerializeObject(SendTextRequest);

                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendText", JsonRequest);
                }

                if (!string.IsNullOrEmpty(ImageURL))
                {
                    SendImageRequest.To = MobileNo;
                    SendImageRequest.textToReply = WhatsAppMessage;
                    SendImageRequest.programCode = ProgramCode;
                    SendImageRequest.imageUrl = ImageURL;

                    string JsonRequests = JsonConvert.SerializeObject(SendImageRequest);

                    ClientImageAPIResponse = CommonService.SendImageApiRequest(ClientAPIURL + "api/ChatbotBell/SendImage", JsonRequests);
                }

                //if (!string.IsNullOrEmpty(ClientAPIResponse))
                //{
                //    isMessageSent = Convert.ToBoolean(ClientAPIResponse);

                //    if (isMessageSent && ChatID > 0 && InsertChat.Equals(1))
                //    {
                //        ChatMessageDetails.ChatID = ChatID;
                //        ChatMessageDetails.Message = Message;
                //        ChatMessageDetails.ByCustomer = false;
                //        ChatMessageDetails.ChatStatus = 1;
                //        ChatMessageDetails.StoreManagerId = CreatedBy;
                //        ChatMessageDetails.CreatedBy = CreatedBy;

                //        resultCount = SaveChatMessages(ChatMessageDetails);

                //    }
                //}

                #endregion



            }
            catch (Exception)
            {
                throw;
            }

            return string.IsNullOrEmpty(ClientImageAPIResponse) ? 0 : 1;
        }

        public int sendMessageToCustomerNew(int ChatID, string MobileNo, string ProgramCode, string Message, string WhatsAppMessage, string ImageURL, string ClientAPIURL, int CreatedBy, int InsertChat, string Source)
        {
            CustomerChatModel ChatMessageDetails = new CustomerChatModel();
            ClientCustomSendTextModelNew SendTextRequest = new ClientCustomSendTextModelNew();
            ClientCustomSendImageModelNew SendImageRequest = new ClientCustomSendImageModelNew();
            string ClientAPIResponse = string.Empty;
            string ClientImageAPIResponse = string.Empty;
            //bool isMessageSent = false;

            try
            {

                #region call client api for sending message to customer
                if (string.IsNullOrEmpty(ImageURL))
                {
                    SendTextRequest.To = MobileNo;
                    SendTextRequest.textToReply = Message;
                    SendTextRequest.programCode = ProgramCode;
                    SendTextRequest.source = Source;

                    string JsonRequest = JsonConvert.SerializeObject(SendTextRequest);

                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendTextMessage", JsonRequest);
                }

                //if (!string.IsNullOrEmpty(ClientAPIResponse))
                //{
                //    isMessageSent = Convert.ToBoolean(ClientAPIResponse);

                //    if (isMessageSent && ChatID > 0 && InsertChat.Equals(1))
                //    {
                //        ChatMessageDetails.ChatID = ChatID;
                //        ChatMessageDetails.Message = Message;
                //        ChatMessageDetails.ByCustomer = false;
                //        ChatMessageDetails.ChatStatus = 1;
                //        ChatMessageDetails.StoreManagerId = CreatedBy;
                //        ChatMessageDetails.CreatedBy = CreatedBy;

                //        resultCount = SaveChatMessages(ChatMessageDetails);

                //    }
                //}

                #endregion



            }
            catch (Exception)
            {
                throw;
            }

            return ClientAPIResponse.ToLower().Equals("true") ? 1 : 0;
        }

        #region Chat Sound Notification Setting

        /// <summary>
        ///  Get Chat Sound List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <returns></returns>
        /// 
        public async Task<List<ChatSoundModel>> GetChatSoundList(int TenantID, string Programcode, string SoundFilePath)
        {
            DataTable schemaTable = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            List<ChatSoundModel> SoundList = new List<ChatSoundModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_HSGetChatSoundList", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                    cmd.Parameters.AddWithValue("@_ProgramCode", Programcode);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                ChatSoundModel obj = new ChatSoundModel()
                                {
                                    SoundID = Convert.ToInt32(dr["SoundID"]),
                                    TenantID = dr["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TenantID"]),
                                    ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                    SoundFileName = dr["SoundFileName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SoundFileName"]),
                                    SoundFileUrl = dr["SoundFileName"] == DBNull.Value ? string.Empty : SoundFilePath + Convert.ToString(dr["SoundFileName"]),
                                    CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                    CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                    CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                    ModifyBy = dr["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifiedBy"]),
                                    ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                    ModifyDate = dr["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyDate"]),
                                };
                                SoundList.Add(obj);
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

            return SoundList;
        }


        /// <summary>
        /// Get Chat Sound Notification Setting
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <returns></returns>
        /// 
        public async Task<ChatSoundNotificationModel> GetChatSoundNotificationSetting(int TenantID, string Programcode, string SoundFilePath)
        {
            DataTable schemaTable = new DataTable();
            ChatSoundNotificationModel SoundSetting = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_HSGetChatSoundNotificationSetting", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                    cmd.Parameters.AddWithValue("@_ProgramCode", Programcode);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                SoundSetting = new ChatSoundNotificationModel()
                                {
                                    ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                    TenantID = dr["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TenantID"]),
                                    ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                    NewChatSoundID = dr["NewChatSoundID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NewChatSoundID"]),
                                    NewChatSoundFile = dr["NewChatSoundFile"] == DBNull.Value ? string.Empty : SoundFilePath + Convert.ToString(dr["NewChatSoundFile"]),
                                    NewChatSoundVolume = dr["NewChatVolume"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NewChatVolume"]),
                                    NewMessageSoundID = dr["NewMessageSoundID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NewMessageSoundID"]),
                                    NewMessageSoundFile = dr["NewMessageSoundFile"] == DBNull.Value ? string.Empty : SoundFilePath + Convert.ToString(dr["NewMessageSoundFile"]),
                                    NewMessageSoundVolume = dr["NewMessageVolume"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NewMessageVolume"]),
                                    IsNotiNewChat = dr["IsNotiNewChat"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsNotiNewChat"]),
                                    IsNotiNewMessage = dr["IsNotiNewMessage"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsNotiNewMessage"]),
                                    NotificationTime = dr["NotificationTime"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NotificationTime"]),
                                    IsDefault = dr["IsDefault"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsDefault"]),
                                    CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                    CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                    CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                    ModifyBy = dr["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifiedBy"]),
                                    ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                    ModifyDate = dr["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyDate"]),
                                };
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

            return SoundSetting;



        }


        /// <summary>
        /// Update Chat Sound Notification Setting
        /// </summary>
        /// <param name="ChatSoundNotificationModel"></param>
        /// <returns></returns>
        ///
        public async Task<int> UpdateChatSoundNotificationSetting(ChatSoundNotificationModel Setting)
        {
            int success = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_HSUpdateChatSoundNotificationSetting"
                    };
                    cmd.Parameters.AddWithValue("@_ID", Setting.ID);
                    cmd.Parameters.AddWithValue("@_TenantID", Setting.TenantID);
                    cmd.Parameters.AddWithValue("@_ProgramCode", Setting.ProgramCode);
                    cmd.Parameters.AddWithValue("@_NewChatSoundID", Setting.NewChatSoundID);
                    cmd.Parameters.AddWithValue("@_NewChatVolume", Setting.NewChatSoundVolume);
                    cmd.Parameters.AddWithValue("@_NewMessageSoundID", Setting.NewMessageSoundID);
                    cmd.Parameters.AddWithValue("@_NewMessageVolume", Setting.NewMessageSoundVolume);
                    cmd.Parameters.AddWithValue("@_IsNotiNewChat", Convert.ToInt16(Setting.IsNotiNewChat));
                    cmd.Parameters.AddWithValue("@_IsNotiNewMessage", Convert.ToInt16(Setting.IsNotiNewMessage));
                    cmd.Parameters.AddWithValue("@_NotificationTime", Convert.ToInt16(Setting.NotificationTime));
                    cmd.Parameters.AddWithValue("@_IsDefault", Convert.ToInt16(Setting.IsDefault));
                    cmd.Parameters.AddWithValue("@_ModifiedBy", Setting.ModifyBy);

                    success = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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

        #endregion
    }
}
