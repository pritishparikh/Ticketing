using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public partial class CustomerChatController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionString;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor
        public CustomerChatController(IConfiguration  iConfig)
        {
            configuration = iConfig;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get Ongoing Chat
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOngoingChat")]
        public ResponseModel GetOngoingChat()
        {
            List<CustomerChatMaster> customerChatMaster = new List<CustomerChatMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                customerChatMaster = customerChatCaller.OngoingChat(new CustomerChatService(_connectionString), authenticate.UserMasterID, authenticate.TenantId);

                statusCode =
               customerChatMaster.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customerChatMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get New Chat
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetNewChat")]
        public ResponseModel GetNewChat()
        {
            List<CustomerChatMaster> customerChatMaster = new List<CustomerChatMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                customerChatMaster = customerChatCaller.NewChat(new CustomerChatService(_connectionString), authenticate.UserMasterID, authenticate.TenantId);

                statusCode =
               customerChatMaster.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customerChatMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Read On Going Message
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        [HttpPost]     
        [Route("MarkAsReadOnGoingChat")]
        public ResponseModel ReadOnGoingMessage(int chatID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                int result = customerChatCaller.MarkAsReadMessage(new CustomerChatService(_connectionString), chatID);

                statusCode =
               result.Equals( 0) ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// UpdateCustomerChatStatus
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCustomerChatStatus")]
        public ResponseModel UpdateCustomerChatStatus(int chatID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                int result = customerChatCaller.UpdateCustomerChatIdStatus(new CustomerChatService(_connectionString), chatID, authenticate.TenantId);

                statusCode =
               result == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// Schedule Visit 
        /// </summary>
        /// <param name="AppointmentMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ScheduleVisit")]
        public ResponseModel ScheduleVisit([FromBody]AppointmentMaster appointmentMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                appointmentMaster.CreatedBy = authenticate.UserMasterID;
                appointmentMaster.TenantID=authenticate.TenantId;
                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                int result = customerChatCaller.ScheduleVisit(new CustomerChatService(_connectionString), appointmentMaster);

                statusCode =
               result.Equals(0) ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// CustomerChatHistory
        /// </summary>
        /// <param name="GetChatHistoryModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustomerChatHistory")]
        public ResponseModel GetCustomerChatHistory([FromBody] GetChatHistoryModel chatHistory)
        {
            List<CustomerChatHistory> customerChatHistory = new List<CustomerChatHistory>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                customerChatHistory = customerChatCaller.CustomerChatHistory(new CustomerChatService(_connectionString), chatHistory.ChatId);

                //////////////////////////Paging//////////////////////

                // Get's No of Rows Count   
                int count = customerChatHistory.Count;

                // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
                int CurrentPage = chatHistory.pageNumber;

                // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
                int PageSize = chatHistory.pageSize;

                // Display TotalCount to Records to User  
                int TotalCount = count;

                // Calculating Totalpage by Dividing (No of Records / Pagesize)  
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                // Returns List of Customer after applying Paging   
                var customerChats = customerChatHistory.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                // if CurrentPage is greater than 1 means it has previousPage  
                var previousPage = CurrentPage > 1 ? "Yes" : "No";

                // if TotalPages is greater than CurrentPage means it has nextPage  
                var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                // Object which we are going to send in header   
                var paginationMetadata = new
                {
                    totalCount = TotalCount,
                    pageSize = PageSize,
                    currentPage = CurrentPage,
                    totalPages = TotalPages,
                    previousPage,
                    nextPage
                };

                //////////////////////////Paging End////////////////////////

                statusCode =
                customerChats.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customerChats;

                HttpContext.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Chat Notification Count
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatNotificationCount")]
        public ResponseModel GetChatNotificationCount()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();
                int counts = customerChatCaller.GetChatCount(new CustomerChatService(_connectionString),authenticate.TenantId);

                statusCode =
               counts== 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = counts;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Time Slot
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTimeSlot")]
        public ResponseModel GetTimeSlot(int storeID)
        {
            List<TimeSlotModel> timeSlotModel = new List<TimeSlotModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                timeSlotModel = customerChatCaller.GetTimeSlot(new CustomerChatService(_connectionString), storeID, authenticate.UserMasterID, authenticate.TenantId);

                statusCode =
               timeSlotModel.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = timeSlotModel;
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