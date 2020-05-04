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
                cmd.Parameters.AddWithValue("@_Status", ChatMessageDetails.ChatStatus);
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
        public List<CustomItemSearchResponseModel> ChatItemDetailsSearch(string SearchText)
        {

            List<CustomItemSearchResponseModel> ItemList = new List<CustomItemSearchResponseModel>();

            try
            {

                for(int i=0; i< 6; i++)
                {
                    ItemList.Add(new
                 CustomItemSearchResponseModel
                    {
                        ItemID = i + 1,
                        ImageURL = "https://img2.bata.in/0/images/product/854-6523_700x650_1.jpeg",
                        Label = "Black Slipons for Men " + i.ToString(),
                        AlternativeText = "Product Code: F854652300" + i.ToString(),
                        RedirectionUrl = "https://www.bata.in/bataindia/pr-1463307_c-262/black-slipons-for-men.html"
                    });
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
        public int SendRecommendationsToCustomer(int CustomerID, string MobileNo, int CreatedBy)
        {
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0; int Chat_ID = 0;
          
            List<CustomerRecommendatonModel> RecommendationsList = new List<CustomerRecommendatonModel>();
            DataSet ds = new DataSet();

            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("GetRecomendationsByCustomer", conn);
                cmd.Connection = conn;
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
                    }
                }

                if(RecommendationsList.Count > 0 && Chat_ID > 0)
                {

                    foreach(CustomerRecommendatonModel RecObj in RecommendationsList)
                    {
                        CustomerChatModel ChatMessageDetails = new CustomerChatModel();
                        ChatMessageDetails.ChatID = Chat_ID;
                        ChatMessageDetails.Message = JsonConvert.SerializeObject(RecObj);
                        ChatMessageDetails.ByCustomer =false;
                        ChatMessageDetails.ChatStatus = 1;
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
        public int SendMessageToCustomer(int ChatID, string MobileNo, string ProgramCode, string Message, string ClientAPIURL, int CreatedBy)
        {
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;
            CustomerChatModel ChatMessageDetails = new CustomerChatModel();
            ClientCustomSendTextModel SendTextRequest = new ClientCustomSendTextModel();
            string ClientAPIResponse = string.Empty;

            try
            {

                #region call client api for sending message to customer

                SendTextRequest.To = MobileNo;
                SendTextRequest.textToReply = Message;
                SendTextRequest.programCode = ProgramCode;

                string JsonRequest = JsonConvert.SerializeObject(SendTextRequest);

                ClientAPIResponse = CommonService.SendApiRequest(ClientAPIResponse + "api/BellChatBotIntegration/SendText", JsonRequest);


                // response binding pending as no response structure is provided yet from client------

                //--------

                #endregion

                if (ChatID > 0)
                {
                        ChatMessageDetails.ChatID = ChatID;
                        ChatMessageDetails.Message = Message;
                        ChatMessageDetails.ByCustomer = false;
                        ChatMessageDetails.ChatStatus = 1;
                        ChatMessageDetails.StoreManagerId = CreatedBy;
                        ChatMessageDetails.CreatedBy = CreatedBy;

                        resultCount = SaveChatMessages(ChatMessageDetails);

                }

            }
            catch (Exception )
            {
                throw;
            }
            
            return resultCount;
        }

    }

}
