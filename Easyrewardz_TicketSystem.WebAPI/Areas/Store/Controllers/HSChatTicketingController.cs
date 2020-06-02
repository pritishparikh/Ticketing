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
            int StatusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                customGetChatTickets = chatTicketingCaller.GetTicketsOnLoad(new HSChatTicketingService(_connectioSting), statusID, authenticate.TenantId, authenticate.UserMasterID,authenticate.ProgramCode);

                StatusCode = customGetChatTickets.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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

                statusCode = searchResultList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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
        /// Get CategoryList
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

                statusCode = objCategoryList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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
        /// Get SubCategoryBy CategoryID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatSubCategoryByCategoryID")]
        public ResponseModel GetChatSubCategoryByCategoryID(int categoryID)
        {
            List<SubCategory> objSubCategory = new List<SubCategory>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();

                objSubCategory = chatTicketingCaller.GetChatSubCategoryByCategoryID(new HSChatTicketingService(_connectioSting), categoryID);

                StatusCode =
                objSubCategory.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatIssueTypeBySubcategory")]
        public ResponseModel GetChatIssueTypeBySubcategory(int subCategoryID)
        {
            List<IssueType> objIssueTypeList = new List<IssueType>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();

                objIssueTypeList = chatTicketingCaller.GetIssueTypeList(new HSChatTicketingService(_connectioSting), authenticate.TenantId, subCategoryID);

                StatusCode =
                objIssueTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// <param name="statusID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatTicketsByID")]
        public ResponseModel GetChatTicketsByID(int ticketID)
        {
            GetChatTicketsByID customGetChatTickets = null;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            HSChatTicketingCaller chatTicketingCaller = new HSChatTicketingCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                customGetChatTickets = chatTicketingCaller.GetTicketsByID(new HSChatTicketingService(_connectioSting), ticketID, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);

                StatusCode = customGetChatTickets !=null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;

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
                   chatTicketNotes.Count == 0 ?
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
        #endregion
    }
}