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
    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public partial class CustomerChatController : ControllerBase
    {
        /// <summary>
        /// update customer chat session
        /// </summary>
        /// <param name="ChatSessionValue"></param>
        /// <param name="ChatSessionDuration"></param>
        /// <param name="ChatDisplayValue"></param>
        /// <param name="ChatDisplayDuration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateChatSession")]
        public ResponseModel UpdateChatSession(int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue, string ChatDisplayDuration)
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

                result = customerChatCaller.UpdateChatSession(new CustomerChatService(_connectionString), ChatSessionValue,  ChatSessionDuration,  ChatDisplayValue, 
                    ChatDisplayDuration, authenticate.UserMasterID);

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
        /// get customer chat session
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatSession")]
        public ResponseModel GetChatSession()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            ChatSessionModel ChatSession = new ChatSessionModel();
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                //Authenticate authenticate = new Authenticate();
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                ChatSession = customerChatCaller.GetChatSession(new CustomerChatService(_connectionString) );

                statusCode = ChatSession!=null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ChatSession;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Agent Recent Chat List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAgentRecentChat")]
        public ResponseModel GetAgentRecentChat()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<AgentRecentChatHistory> RecentChatsList = new List<AgentRecentChatHistory>();
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                //Authenticate authenticate = new Authenticate();
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                RecentChatsList = customerChatCaller.GetAgentRecentChat(new CustomerChatService(_connectionString));

                statusCode = RecentChatsList.Count > 0? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = RecentChatsList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Agent Chat History
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAgentChatHistory")]
        public ResponseModel GetAgentChatHistory()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<AgentCustomerChatHistory> ChatsList = new List<AgentCustomerChatHistory>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                ChatsList = customerChatCaller.GetAgentChatHistory(new CustomerChatService(_connectionString),authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);

                statusCode = ChatsList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ChatsList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Agent List For Ongoin Chat
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAgentList")]
        public ResponseModel GetAgentList()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<AgentRecentChatHistory> AgentList = new List<AgentRecentChatHistory>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                AgentList = customerChatCaller.GetAgentList(new CustomerChatService(_connectionString),authenticate.TenantId);

                statusCode = AgentList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = AgentList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Get Card Image Upload log
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCardImageUploadlog")]
        public ResponseModel GetCardImageUploadlog(int ListingFor=1) //1=asset Approval listing 2=upload log

        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<ChatCardImageUploadModel> CardImageLog = new List<ChatCardImageUploadModel>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                CardImageLog = customerChatCaller.GetCardImageUploadlog(new CustomerChatService(_connectionString), ListingFor, authenticate.TenantId,authenticate.ProgramCode);

                statusCode = CardImageLog.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CardImageLog;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Insert Card Image Upload
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertCardImageUpload")]
        public ResponseModel InsertCardImageUpload(string ItemID, string ImageUrl)
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

                result = customerChatCaller.InsertCardImageUpload
                    (new CustomerChatService(_connectionString), authenticate.TenantId,authenticate.ProgramCode, ItemID, ImageUrl,authenticate.UserMasterID);

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
        /// Approve Reject Card Image
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ApproveRejectCardImage")]
        public ResponseModel ApproveRejectCardImage(int ID,string ItemID, bool AddToLibrary)
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

                result = customerChatCaller.ApproveRejectCardImage
                    (new CustomerChatService(_connectionString), ID, authenticate.TenantId, authenticate.ProgramCode, ItemID, AddToLibrary, authenticate.UserMasterID);

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


    }
}