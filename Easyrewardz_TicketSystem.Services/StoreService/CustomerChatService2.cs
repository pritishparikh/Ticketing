using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Linq;

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
        public List<CustomerChatMessages> GetChatMessageDetails(int tenantId, int ChatID)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<CustomerChatMessages> ChatList = new List<CustomerChatMessages>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetChatDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@_chatID", ChatID);

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
                                //AgentProfilePic = dr["AgentProfilePic"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AgentProfilePic"]),
                                //CustomerProfilePic = dr["CustomerProfilePic"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerProfilePic"]),
                                IsBotReply = dr["IsBotReply"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsBotReply"])


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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return ChatList;
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

                cmd = new MySqlCommand("SP_HSInsertChatDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_ChatID", ChatMessageDetails.ChatID);
                cmd.Parameters.AddWithValue("@_Message", string.IsNullOrEmpty(ChatMessageDetails.Message) ? "" : ChatMessageDetails.Message);
                cmd.Parameters.AddWithValue("@_ByCustomer", ChatMessageDetails.ByCustomer ? 1 : 2);
                cmd.Parameters.AddWithValue("@_Status",!ChatMessageDetails.ByCustomer ? 0 : 1);
                cmd.Parameters.AddWithValue("@_StoreManagerId", ChatMessageDetails.StoreManagerId);
                cmd.Parameters.AddWithValue("@_CreatedBy", ChatMessageDetails.CreatedBy);




                cmd.CommandType = CommandType.StoredProcedure;

                resultCount = Convert.ToInt32(cmd.ExecuteScalar());

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
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public List<CustomItemSearchResponseModel> ChatItemDetailsSearch(int TenantID, string Programcode, string ClientAPIURL, string SearchText, string ProgramCode)
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

                SearchItemRequest.programcode = ProgramCode;
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
                                CardItemsIds= ds.Tables[0].Rows[0]["CardItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CardItemID"]);

                                if(!string.IsNullOrEmpty(CardItemsIds))
                                {
                                    string[] CardItemsIDArr = CardItemsIds.Split(new char[] { ',' });

                                    if(CardItemsIDArr.Contains("1"))
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
                                    foreach (CustomItemSearchResponseModel carditem  in ItemList)
                                    {
                                            carditem.imageURL = ApprovedImageItemList.Where(x => x.uniqueItemCode.Equals(carditem.uniqueItemCode)).Select(x => x.imageURL).FirstOrDefault();
                                    }

                                }

                            }


                            #endregion

                        }


                    }

                }
                catch(Exception )
                {
                    throw;
                }
                


                #endregion

            }
            catch (Exception)
            {
                throw;
            }
            
            return ItemList;
        }

        /// <summary>
        /// Get Chat Suggestions
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public List<CustomerChatSuggestionModel> GetChatSuggestions(string SearchText)
        {

            List<CustomerChatSuggestionModel> SuggestionList = new List<CustomerChatSuggestionModel>();
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetChatSuggestions", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@SearchText", string.IsNullOrEmpty(SearchText) ? "" : SearchText.ToLower());


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
                            CustomerChatSuggestionModel obj = new CustomerChatSuggestionModel()
                            {
                                SuggestionID = Convert.ToInt32(dr["SuggestionID"]),
                                SuggestionText = dr["SuggestionText"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SuggestionText"]),
                                //CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0: Convert.ToInt32(dr["CreatedBy"]),
                                //CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                //ModifyBy = dr["ModifyBy"] == DBNull.Value ? 0: Convert.ToInt32(dr["ModifyBy"]),
                                //ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedDate"]),

                            };

                            SuggestionList.Add(obj);
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
            return SuggestionList;
        }


        /// <summary>
        /// Save Customer Chat reply 
        /// </summary>
        /// <param name="ChatMessageReply"></param>
        /// <returns></returns>
        public int SaveCustomerChatMessageReply(CustomerChatReplyModel ChatReply)
        {
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;

            try
            {

                DateTime Now = !string.IsNullOrEmpty(ChatReply.DateTime) ? DateTime.ParseExact(ChatReply.DateTime, "dd MMM yyyy hh:mm:ss tt", CultureInfo.InvariantCulture) : DateTime.Now;

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_InsertCustomerChatReplyMessage", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_ProgramCode", ChatReply.ProgramCode);
                cmd.Parameters.AddWithValue("@_StoreCode", ChatReply.StoreCode);
                cmd.Parameters.AddWithValue("@_Mobile", ChatReply.Mobile);
                cmd.Parameters.AddWithValue("@_Message",string.IsNullOrEmpty(ChatReply.Message) ? "" :  ChatReply.Message);
                cmd.Parameters.AddWithValue("@_ChatID", ChatReply.ChatID);
                cmd.Parameters.AddWithValue("@_DateTime", Now);

                cmd.CommandType = CommandType.StoredProcedure;

                resultCount = Convert.ToInt32(cmd.ExecuteScalar());
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
        public int SendRecommendationsToCustomer(int TenantID, string Programcode, int CustomerID, string MobileNo, string ClientAPIURL, int CreatedBy)
        {
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0; int Chat_ID = 0;
            string ProgramCode = string.Empty;
            List<CustomerRecommendatonModel> RecommendationsList = new List<CustomerRecommendatonModel>();
            DataSet ds = new DataSet();
            string HtmlMessageContent = "<div class=\"card-body position-relative\"><div class=\"row\" style=\"margin: 0px; align-items: flex-end;\"><div class=\"col-md-2\"><img class=\"chat-product-img\" src=\"{0}\" alt=\"Product Image\" ></div><div class=\"col-md-10 bkcprdt\"><div><label class=\"chat-product-name\">Brand :{1}</label></div><div><label class=\"chat-product-code\">Category: {2}</label></div><div><label class=\"chat-product-code\">SubCategory: {3}</label></div><div><label class=\"chat-product-code\">Color: {4}</label></div><div><label class=\"chat-product-code\">Size: {5}</label></div><div><label class=\"chat-product-code\">Item Code: {6}</label></div><div><label class=\"chat-product-prize\"> Price : {7}</label></div><div><a href=\"{8}\" target=\"_blank\" class=\"chat-product-url\">{9}</a></div></div></div></div>";

            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetRecomendationsByCustomerID", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_prgCode", Programcode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_MobileNo", MobileNo);
           
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
                            CustomerRecommendatonModel obj = new CustomerRecommendatonModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                ItemCode = dr["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemCode"]),
                                Category = dr["Category"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Category"]),
                                SubCategory = dr["SubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubCategory"]),
                                Brand = dr["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Brand"]),
                                Color = dr["Color"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Color"]),
                                Size = dr["Size"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Size"]),
                                Price = dr["Price"] == DBNull.Value ? "0" : Convert.ToString(dr["Price"]),
                                Url = dr["Url"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Url"]),
                                ImageURL = dr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ImageURL"]),

                           };

                            RecommendationsList.Add(obj);
                        }
                    }

                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        Chat_ID= ds.Tables[1].Rows[0]["Chat_ID"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["Chat_ID"]);
                        ProgramCode = ds.Tables[1].Rows[0]["prgCode"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["prgCode"]);
                    }
                }

                if(RecommendationsList.Count > 0 && Chat_ID > 0 && !string.IsNullOrEmpty(ProgramCode))
                {

                    #region call client send text api for sending message to customer

                    foreach (CustomerRecommendatonModel RecObj in RecommendationsList)
                    {
                        string whatsAppContent = "Brand: " + RecObj.Brand + ", Category: " + RecObj.Category + ", Sub Category: " + RecObj.SubCategory + ", Color: " + RecObj.Color + ", Size: " +
                                                 RecObj.Size + ", Item Code: " + RecObj.ItemCode + ", Price: " + RecObj.Price + "  " + RecObj.Url;
                        resultCount = resultCount + SendMessageToCustomer(Chat_ID, MobileNo.Length>10?MobileNo:"91"+MobileNo, ProgramCode, RecObj.Url, whatsAppContent, RecObj.ImageURL, ClientAPIURL, CreatedBy,0);
                    }

                    #endregion



                    foreach (CustomerRecommendatonModel RecObj in RecommendationsList)
                    {

                        string messagecontent = string.Format(HtmlMessageContent, RecObj.ImageURL, RecObj.Brand, RecObj.Category, RecObj.SubCategory, RecObj.Color
                                                , RecObj.Size, RecObj.ItemCode, RecObj.Price, RecObj.Url, RecObj.Url);

                        CustomerChatModel ChatMessageDetails = new CustomerChatModel();
                        ChatMessageDetails.ChatID = Chat_ID;
                        ChatMessageDetails.Message = messagecontent;
                        ChatMessageDetails.ByCustomer =false;
                        ChatMessageDetails.ChatStatus = 0;
                        ChatMessageDetails.StoreManagerId = CreatedBy;
                        ChatMessageDetails.CreatedBy = CreatedBy;

                        resultCount  = resultCount + SaveChatMessages(ChatMessageDetails);

                    }

                     
                }

            }
            catch (Exception )
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
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;
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
            catch (Exception )
            {
                throw;
            }
            
            return resultCount;
        }

    }

}
