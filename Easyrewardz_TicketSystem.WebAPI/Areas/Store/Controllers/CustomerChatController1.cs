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
using System.Linq;
using System.Threading.Tasks;

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
        private static string _ClientAPIUrl;
        private readonly string UploadFiles;
        private readonly string rootPath;

        #region API url variable declaration

        private readonly string _getUserAtvDetails;
        private readonly string _getKeyInsight;
        private readonly string _getRecommendedList;
        private readonly string _getLastTransactionDetails;
        private readonly string _sendText;
        private readonly string _sendTextMessage;
        private readonly string _sendImageMessage;
        private readonly string _sendImage;
        private readonly string _sendCtaImage;
        private readonly string _sendSms;
        private readonly string _makeBellActive;
        private readonly string _getCustomerLastUpdatedTime;
        private readonly string _searchItemDetails;
        private readonly string _searchItemDetailsWB;
        private readonly string _addOrderinPhygital;

        #endregion

        private readonly ChatbotBellHttpClientService _chatbotBellHttpClientService;
        private readonly ILogger<CustomerChatController> _logger;   
        #endregion

        #region Constructor
        public CustomerChatController(IConfiguration  iConfig, ChatbotBellHttpClientService chatbotBellHttpClientService, ILogger<CustomerChatController> logger)
        {
            configuration = iConfig;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _ClientAPIUrl = configuration.GetValue<string>("ClientAPIURL");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            rootPath = configuration.GetValue<string>("APIURL");
           _chatbotBellHttpClientService = chatbotBellHttpClientService;
            _logger = logger;

            _getUserAtvDetails = configuration.GetValue<string>("ClientAPI:GetUserATVDetails");
            _getKeyInsight = configuration.GetValue<string>("ClientAPI:GetKeyInsight");
            _getRecommendedList = configuration.GetValue<string>("ClientAPI:GetRecommendedList");
            _getLastTransactionDetails = configuration.GetValue<string>("ClientAPI:GetLastTransactionDetails");
            _sendText = configuration.GetValue<string>("ClientAPI:SendText");
            _sendTextMessage = configuration.GetValue<string>("ClientAPI:SendTextMessage");
            _sendImageMessage = configuration.GetValue<string>("ClientAPI:SendImageMessage");
            _sendImage = configuration.GetValue<string>("ClientAPI:SendImage");
            _sendCtaImage = configuration.GetValue<string>("ClientAPI:SendCtaImage");
            _sendSms = configuration.GetValue<string>("ClientAPI:SendSMS");
            _makeBellActive = configuration.GetValue<string>("ClientAPI:MakeBellActive");
            _getCustomerLastUpdatedTime = configuration.GetValue<string>("ClientAPI:GetCustomerLastUpdatedTime");
            _searchItemDetails = configuration.GetValue<string>("ClientAPI:SearchItemDetails");
            _searchItemDetailsWB = configuration.GetValue<string>("ClientAPI:SearchItemDetailsWB");
            _addOrderinPhygital = configuration.GetValue<string>("ClientAPI:AddOrderinPhygital");
                           
             
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get Ongoing Chat
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="StoreManagerID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOngoingChat")]
        public async Task<ResponseModel> GetOngoingChat(string Search, int StoreManagerID)
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

                customerChatMaster =await customerChatCaller.OngoingChat(new CustomerChatService(_connectionString), authenticate.UserMasterID, authenticate.TenantId, Search, StoreManagerID);

                statusCode = customerChatMaster.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

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
        public async Task<ResponseModel> GetNewChat()
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

                customerChatMaster = await customerChatCaller.NewChat(new CustomerChatService(_connectionString), authenticate.UserMasterID, authenticate.TenantId);

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
        public async Task<ResponseModel> ReadOnGoingMessage(int chatID)
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

                int result =await customerChatCaller.MarkAsReadMessage(new CustomerChatService(_connectionString), chatID);

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
        /// Update Customer Chat Status
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCustomerChatStatus")]
        public async Task<ResponseModel> UpdateCustomerChatStatus(int chatID)
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

                int result =await customerChatCaller.UpdateCustomerChatIdStatus(new CustomerChatService(_connectionString), chatID, authenticate.TenantId,authenticate.UserMasterID);

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
        public async Task<ResponseModel> ScheduleVisit([FromBody]AppointmentMaster appointmentMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<AppointmentDetails> appointmentDetails = new List<AppointmentDetails>();
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

                appointmentDetails = await customerChatCaller.ScheduleVisit(new CustomerChatService(_connectionString), appointmentMaster);

                statusCode =
              appointmentDetails.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = appointmentDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Customer Chat History
        /// </summary>
        /// <param name="chatHistory"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustomerChatHistory")]
        public async Task<ResponseModel> GetCustomerChatHistory([FromBody] GetChatHistoryModel chatHistory)
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

                customerChatHistory =await customerChatCaller.CustomerChatHistory(new CustomerChatService(_connectionString), chatHistory.ChatId);

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
        public async Task<ResponseModel> GetChatNotificationCount()
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
                int counts =await customerChatCaller.GetChatCount(new CustomerChatService(_connectionString),authenticate.TenantId, authenticate.UserMasterID);

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
        public ResponseModel GetTimeSlot()
        {
            List<DateofSchedule> dateOfSchedule = new List<DateofSchedule>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                dateOfSchedule = customerChatCaller.GetTimeSlot(new CustomerChatService(_connectionString), 
                    authenticate.TenantId, authenticate.ProgramCode,authenticate.UserMasterID);

                statusCode = dateOfSchedule.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

               
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = dateOfSchedule;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// send Message To Customer For Schedule Visit
        /// </summary>
        /// <param name="appointmentMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sendMessageToCustomerForScheduleVisit")]
        public async Task<ResponseModel> SendMessageToCustomerForScheduleVisit([FromBody]AppointmentMaster appointmentMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();

            ClientCustomSendTextModel SendTextRequest = new ClientCustomSendTextModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            string ClientAPIResponse = string.Empty;
            string textToReply = string.Empty;
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                //Authenticate authenticate = new Authenticate();
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                // CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                //  result = customerChatCaller.SendMessageToCustomerForVisit(new CustomerChatService(_connectionString), appointmentMaster, _ClientAPIUrl, authenticate.UserMasterID);


                #region call client api for sending message to customer

                 textToReply = "Dear" + appointmentMaster.CustomerName + ",Your Visit for Our Store is schedule On" + appointmentMaster.AppointmentDate +
                  "On Time Between" + appointmentMaster.TimeSlot;

                SendTextRequest.To = appointmentMaster.MobileNo.Length.Equals(12) ? appointmentMaster.MobileNo : "91"+ appointmentMaster.MobileNo;
                SendTextRequest.textToReply = textToReply;
                SendTextRequest.programCode = appointmentMaster.ProgramCode;

                string JsonRequest = JsonConvert.SerializeObject(SendTextRequest);

                _ClientAPIUrl =  _ClientAPIUrl + _sendText;
                ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl, JsonRequest);
               // ClientAPIResponse =await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + "api/ChatbotBell/SendText",JsonRequest);

                #endregion

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

        #endregion
    }
}