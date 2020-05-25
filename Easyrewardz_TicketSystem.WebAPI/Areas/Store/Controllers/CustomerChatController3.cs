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
        /// get customer chat session
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
    }
}