using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class HSChatTicketingController : ControllerBase
    {
        #region  Variable Declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor
        public HSChatTicketingController(IConfiguration iConfig)
        {
            configuration = iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get Chat Tickets
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatTickets")]
        public ResponseModel GetTicketsOnPageLoad(int statusID)
        {
            List<CustomGetChatTickets> customGetChatTickets = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                customGetChatTickets = chatTicketingCaller.GetTicketsOnLoad(new HSChatTicketingService(_connectioSting), statusID, authenticate.TenantId, authenticate.UserMasterID,authenticate.ProgramCode);

                statusCode = customGetChatTickets.Count.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customGetChatTickets.Count > 0 ? customGetChatTickets : null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Chat Ticket Status Count
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChatTicketStatusCount")]
        public ResponseModel ChatTicketStatusCount()
        {
            List<TicketStatusModel> searchResultList = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                searchResultList = chatTicketingCaller.GetStatusCount(new HSChatTicketingService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);

                statusCode = searchResultList.Count.Equals(0)? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResultList.Count > 0 ? searchResultList : null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Chat Category
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatCategory")]
        public ResponseModel GetChatCategory()
        {
            List<Category> objCategoryList = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
                objCategoryList = chatTicketingCaller.GetCategoryList(new HSChatTicketingService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);

                statusCode = objCategoryList.Count.Equals(0)? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCategoryList.Count > 0 ? objCategoryList : null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get SubCategory By CategoryID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatSubCategoryByCategoryID")]
        public ResponseModel GetChatSubCategoryByCategoryID(int categoryID)
        {
            List<SubCategory> objSubCategory = new List<SubCategory>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();

                objSubCategory = chatTicketingCaller.GetChatSubCategoryByCategoryID(new HSChatTicketingService(_connectioSting), categoryID);

                statusCode =
                objSubCategory.Count.Equals(0) ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSubCategory;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get Chat Issue Type By Subcategory
        /// </summary>
        /// <param name="subCategoryID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatIssueTypeBySubcategory")]
        public ResponseModel GetChatIssueTypeBySubcategory(int subCategoryID)
        {
            List<IssueType> objIssueTypeList = new List<IssueType>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();

                objIssueTypeList = chatTicketingCaller.GetIssueTypeList(new HSChatTicketingService(_connectioSting), authenticate.TenantId, subCategoryID);

                statusCode =
                objIssueTypeList.Count.Equals(0) ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objIssueTypeList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get Chat Tickets By ID
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatTicketsByID")]
        public ResponseModel GetChatTicketsByID(int ticketID)
        {
            GetChatTicketsByID customGetChatTickets = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                customGetChatTickets = chatTicketingCaller.GetTicketsByID(new HSChatTicketingService(_connectioSting), ticketID, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);

                statusCode = customGetChatTickets !=null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customGetChatTickets != null ? customGetChatTickets : null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Add Chat Ticket Notes
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddChatTicketNotes")]
        public ResponseModel AddChatTicketNotes(int ticketID, string comment)
        {
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = chatTicketingCaller.AddChatTicketNotes(new HSChatTicketingService(_connectioSting), ticketID, comment, authenticate.UserMasterID, authenticate.TenantId, authenticate.ProgramCode);
                statusCode =
                result.Equals(0)?
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
        /// Get Chat Ticket Notes
        /// </summary>
        /// <param name="ticketID"></param>
        [HttpPost]
        [Route("GetChatTicketNotes")]
        public ResponseModel GetChatTicketNotes(int ticketID)
        {
            List<ChatTicketNotes> chatTicketNotes = new List<ChatTicketNotes>();
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                chatTicketNotes = chatTicketingCaller.GetChatticketNotes(new HSChatTicketingService(_connectioSting), ticketID);
                statusCode =
                   chatTicketNotes.Count.Equals(0)?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = chatTicketNotes;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Chat Ticket Status
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="statusID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateChatTicketStatus")]
        public ResponseModel UpdateChatTicketStatus(int ticketID,int statusID)
        {
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = chatTicketingCaller.SubmitChatTicket(new HSChatTicketingService(_connectioSting), ticketID, statusID, authenticate.UserMasterID);
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
        /// Get tickets On View Search click
        /// </summary>
        /// <param name="ChatTicketSearch"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatTicketsOnSearch")]
        public ResponseModel GetChatTicketsOnSearch([FromBody]ChatTicketSearch searchModel)
        {
            List<CustomGetChatTickets> searchResultList = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                searchModel.UserID = authenticate.UserMasterID;
                searchModel.TenantID = authenticate.TenantId;
                searchResultList = chatTicketingCaller.GetChatTicketsOnSearch(new HSChatTicketingService(_connectioSting), searchModel);

                statusCode = searchResultList.Count.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResultList.Count > 0 ? searchResultList : null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Chat Ticket History
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatTicketHistory")]
        public ResponseModel GetChatTicketHistory(int ticketID)
        {
            List<CustomTicketHistory> objTicketHistory = new List<CustomTicketHistory>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objTicketHistory = chatTicketingCaller.GetChatTickethistory(new HSChatTicketingService(_connectioSting), ticketID);
                statusCode =
               objTicketHistory.Count.Equals(0) ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTicketHistory;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Create Chat Ticket
        /// </summary>
        /// <param name="createChatTickets"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateChatTicket")]
        public ResponseModel CreateChatTicket([FromBody]CreateChatTickets createChatTickets)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                createChatTickets.CreatedBy = authenticate.UserMasterID;
                createChatTickets.TenantID = authenticate.TenantId;
                //createChatTickets.Brand = authenticate.ProgramCode;
                int TicketID = chatTicketingCaller.CreateChatTicket(new HSChatTicketingService(_connectioSting), createChatTickets);

                statusCode = TicketID.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = TicketID;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        [HttpPost]
        [Route("GetChatTicketsByCustomer")]
        public ResponseModel GetTicketsByCustomerOnPageLoad(int statusID)
        {
            List<CustomGetChatTickets> customGetChatTickets = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                customGetChatTickets = chatTicketingCaller.GetTicketsByCustomerOnLoad(new HSChatTicketingService(_connectioSting), statusID, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);

                statusCode = customGetChatTickets.Count.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customGetChatTickets.Count > 0 ? customGetChatTickets : null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        [HttpPost]
        [Route("GetChatTicketsByCustomerOnSearch")]
        public ResponseModel GetChatTicketsByCustomerOnSearch([FromBody]ChatTicketSearch searchModel)
        {
            List<CustomGetChatTickets> searchResultList = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                searchModel.UserID = authenticate.UserMasterID;
                searchModel.TenantID = authenticate.TenantId;
                searchResultList = chatTicketingCaller.GetChatTicketsByCustomerOnSearch(new HSChatTicketingService(_connectioSting), searchModel);

                statusCode = searchResultList.Count.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResultList.Count > 0 ? searchResultList : null;
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