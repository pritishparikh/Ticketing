using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

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
        public ResponseModel getChatMessagesList(int ChatID, int ForRecentChat=0)
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

                ChatList = customerChatCaller.GetChatmessageDetails(new CustomerChatService(_connectionString), authenticate.TenantId,ChatID, ForRecentChat);

                statusCode = ChatList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound  ;
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
        /// Get Chat Suggestions
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getChatSuggestions")]
        public ResponseModel getChatSuggestions(string SearchText)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<CustomerChatSuggestionModel> SuggestionList = new List<CustomerChatSuggestionModel>();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                SuggestionList = customerChatCaller.GetChatSuggestions(new CustomerChatService(_connectionString), SearchText);
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
        public ResponseModel saveCustomerChatReply([FromBody]  CustomerChatReplyModel ChatMessageReply)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0; int statusCode = 0; 
            string statusMessage = "";
            try
            {


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.SaveCustomerChatMessageReply(new CustomerChatService(_connectionString), ChatMessageReply);

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
        public ResponseModel sendRecommendationsToCustomer(int CustomerID, string MobileNumber)
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

                result = customerChatCaller.SendRecommendationsToCustomer(new CustomerChatService(_connectionString), authenticate.TenantId,authenticate.ProgramCode,
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

        #endregion
    }
}