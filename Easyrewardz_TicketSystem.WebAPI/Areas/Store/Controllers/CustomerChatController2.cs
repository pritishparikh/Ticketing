using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{

    public partial class CustomerChatController : ControllerBase
    {
        #region Custom Methods


        /// <summary>
        /// Get Customer Chat messages list
        /// </summary>
        /// <param name="ChatID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getChatMessagesList")]
        public async Task<ResponseModel> getChatMessagesList(int ChatID, int ForRecentChat=0)
        {
            ResponseModel objResponseModel = new ResponseModel();
           
            List<CustomerChatMessages> ChatList = new List<CustomerChatMessages>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                ChatList =await customerChatCaller.GetChatmessageDetails(new CustomerChatService(_connectionString), authenticate.TenantId,ChatID, ForRecentChat);

                statusCode = ChatList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ChatList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("getChatMessagesListNew")]
        public async Task<ResponseModel> getChatMessagesListNew(int ChatID, int PageNo, int ForRecentChat = 0)
        {
            ResponseModel objResponseModel = new ResponseModel();
            ChatMessageDetails ChatList = new ChatMessageDetails();
            // List<CustomerChatMessages> ChatList = new List<CustomerChatMessages>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                ChatList = await customerChatCaller.GetChatmessageDetailsNew(new CustomerChatService(_connectionString), authenticate.TenantId, ChatID, ForRecentChat, PageNo);


                if (ChatList.ChatMessages != null)
                {
                    if (ChatList.ChatMessages.Count > 0 || ChatList.RecentChatCount > 0)
                    {
                        statusCode = (int)EnumMaster.StatusCode.Success;
                    }
                    else
                    {
                        statusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                    }

                }
                else
                {
                    statusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                }

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ChatList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Customer Chat messages list
        /// </summary>
        /// <param name="ChatID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveChatMessages")]
        public ResponseModel saveChatMessages([FromBody]  CustomerChatModel ChatMessageDetails)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();
                ChatMessageDetails.CreatedBy = !ChatMessageDetails.ByCustomer ? authenticate.UserMasterID : 0;
                result = customerChatCaller.SaveChatMessages(new CustomerChatService(_connectionString), ChatMessageDetails);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Search Item Details in Card Tab of Chat
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchChatItemDetails")]
        public ResponseModel searchChatItemDetails(string SearchText, string ProgramCode)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<CustomItemSearchResponseModel> ItemList = new List<CustomItemSearchResponseModel>();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                ItemList = customerChatCaller.ChatItemSearch(new CustomerChatService(_connectionString),authenticate.TenantId, ProgramCode,
                    _ClientAPIUrl, SearchText);
                statusCode = ItemList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ItemList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Search Item Details in Card Tab of Chat
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchChatItemDetailsNew")]
        public async Task<ResponseModel> searchChatItemDetailsNew(string SearchText, string StoreCode)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<CustomItemSearchResponseModel> ItemList = new List<CustomItemSearchResponseModel>();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();
                _ClientAPIUrl = _ClientAPIUrl + _searchItemDetails;
                ItemList = await customerChatCaller.ChatItemSearchNew(new CustomerChatService(_connectionString, _chatbotBellHttpClientService,_logger), authenticate.TenantId, authenticate.ProgramCode,
                    _ClientAPIUrl, SearchText, StoreCode);

                statusCode = ItemList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ItemList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Search Item Details in Card Tab of Chat WebBot
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchChatItemDetailsWB")]
        public async Task<ResponseModel> searchChatItemDetailsWB(string SearchText, string StoreCode)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<CustomItemSearchWBResponseModel> ItemList = new List<CustomItemSearchWBResponseModel>();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();
                _ClientAPIUrl = _ClientAPIUrl + _searchItemDetailsWB;
                ItemList = await customerChatCaller.ChatItemDetailsSearchWB(new CustomerChatService(_connectionString, _chatbotBellHttpClientService, _logger), authenticate.TenantId, authenticate.ProgramCode,
                    _ClientAPIUrl, SearchText, StoreCode);

                statusCode = ItemList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ItemList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Get Chat Suggestions
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getChatSuggestions")]
        public async Task<ResponseModel> getChatSuggestions(string SearchText)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<object> SuggestionList = new List<object>();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                SuggestionList =await customerChatCaller.GetChatSuggestions(new CustomerChatService(_connectionString), SearchText);
                statusCode = SuggestionList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = SuggestionList;  
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Save Customer Chat reply 
        /// </summary>
        /// <param name="ChatMessageReply"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveCustomerChatReply")]
        public async Task<ResponseModel> saveCustomerChatReply([FromBody]  CustomerChatReplyModel ChatMessageReply)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0; int statusCode = 0; 
            string statusMessage = "";
            try
            {


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = await customerChatCaller.SaveCustomerChatMessageReply(new CustomerChatService(_connectionString), ChatMessageReply);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// send Recommendations To Customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sendRecommendationsToCustomer")]
        public ResponseModel sendRecommendationsToCustomer(int ChatID,int CustomerID, string MobileNumber)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.SendRecommendationsToCustomer(new CustomerChatService(_connectionString),  ChatID, authenticate.TenantId,authenticate.ProgramCode,
                    CustomerID, MobileNumber,_ClientAPIUrl,authenticate.UserMasterID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("sendRecommendationsToCustomerNew")]
        public async Task<ResponseModel> sendRecommendationsToCustomerNew(int ChatID, int CustomerID, string MobileNumber, string Source)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();


               

                result = await customerChatCaller.SendRecommendationsToCustomerNew(new CustomerChatService(_connectionString, _chatbotBellHttpClientService), ChatID, authenticate.TenantId, authenticate.ProgramCode,
                    CustomerID, MobileNumber, _ClientAPIUrl,_sendImageMessage,_getRecommendedList, authenticate.UserMasterID, Source);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// send Message To Customer
        /// </summary>
        /// <param name="ChatID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="Messsage"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sendMessageToCustomer")]
        public ResponseModel sendMessageToCustomer(int ChatID, string MobileNo, string ProgramCode, string Message, string WhatsAppMessage, string ImageURL, int InsertChat=1)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.SendMessageToCustomer(new CustomerChatService(_connectionString),  ChatID,  MobileNo,  ProgramCode, 
                          Message, WhatsAppMessage, ImageURL, _ClientAPIUrl, authenticate.UserMasterID, InsertChat);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        [HttpPost]
        [Route("sendMessageToCustomerNew")]
        public async Task<ResponseModel> sendMessageToCustomerNew(int ChatID, string MobileNo, string ProgramCode, string Message, string WhatsAppMessage, string ImageURL, string Source, int InsertChat = 1)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            string ClientAPIResponse = string.Empty;
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]); 
                //Authenticate authenticate = new Authenticate();
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                ClientCustomSendTextModelNew SendTextRequest = new ClientCustomSendTextModelNew();

                SendTextRequest.To = MobileNo;
                SendTextRequest.textToReply = Message;
                SendTextRequest.programCode = ProgramCode;
                SendTextRequest.source = Source;
                string JsonRequest = JsonConvert.SerializeObject(SendTextRequest);

                //var Response = _chatbotBellHttpClientService.SendAPIRequest(JsonRequest, _ClientAPIUrl + "api/ChatbotBell/SendTextMessage");

                _ClientAPIUrl = _ClientAPIUrl + _sendTextMessage;
                ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl, JsonRequest);
              //  ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + "api/ChatbotBell/SendTextMessage",JsonRequest );

                //result =  customerChatCaller.sendMessageToCustomerNew(new CustomerChatService(_connectionString), ChatID, MobileNo, ProgramCode,
                //          Message, WhatsAppMessage, ImageURL, _ClientAPIUrl, authenticate.UserMasterID, InsertChat, Source);

                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    statusCode = ClientAPIResponse.ToLower().Equals("true") ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                }
                else
                {
                    statusCode = (int)EnumMaster.StatusCode.InternalServerError; ;
                }

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        #region Chat Sound Notification Setting

        /// <summary>
        /// Get Chat Sound List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatSoundList")]
        public async Task<ResponseModel> GetChatSoundList()
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<ChatSoundModel> SoundList = new List<ChatSoundModel>();

            int statusCode = 0;
            string statusMessage = "";
            string SoundPhysicalFilePath = string.Empty;
            string SoundFilePath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                SoundPhysicalFilePath = Path.Combine(Directory.GetCurrentDirectory(), UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)4), "ChatBotSoundFiles");

                if (!Directory.Exists(SoundPhysicalFilePath))
                {
                    Directory.CreateDirectory(SoundPhysicalFilePath);
                }

                SoundFilePath = rootPath + UploadFiles + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)4) + "/ChatBotSoundFiles/";

                SoundList = await customerChatCaller.GetChatSoundList(new CustomerChatService(_connectionString), authenticate.TenantId,authenticate.ProgramCode, SoundFilePath);
                statusCode = SoundList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = SoundList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Get Chat Sound Notification Setting
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatSoundNotiSetting")]
        public async Task<ResponseModel> GetChatSoundNotiSetting()
        {
            ResponseModel objResponseModel = new ResponseModel();
            ChatSoundNotificationModel SoundSetting = new ChatSoundNotificationModel();

            int statusCode = 0;
            string statusMessage = "";
            string SoundPhysicalFilePath = string.Empty;
            string SoundFilePath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                SoundPhysicalFilePath = Path.Combine(Directory.GetCurrentDirectory(), UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)4), "ChatBotSoundFiles");

                if (!Directory.Exists(SoundPhysicalFilePath))
                {
                    Directory.CreateDirectory(SoundPhysicalFilePath);
                }

                SoundFilePath = rootPath + UploadFiles + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)4) + "/ChatBotSoundFiles/";

                SoundSetting = await customerChatCaller.GetChatSoundNotificationSetting(new CustomerChatService(_connectionString), authenticate.TenantId, authenticate.ProgramCode, SoundFilePath);
                statusCode = SoundSetting.ID > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = SoundSetting;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Update Chat Sound Notification Setting
        /// </summary>
        /// <param name="ChatSoundNotificationModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateChatSoundNotiSetting")]
        public async  Task<ResponseModel> UpdateChatSoundNotiSetting([FromBody]  ChatSoundNotificationModel Setting)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
          
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Setting.TenantID = authenticate.TenantId;
                Setting.ProgramCode = authenticate.ProgramCode;
                Setting.ModifyBy = authenticate.UserMasterID;

               


                result =await customerChatCaller.UpdateChatSoundNotificationSetting(new CustomerChatService(_connectionString), Setting);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #endregion

        #endregion
    }
}