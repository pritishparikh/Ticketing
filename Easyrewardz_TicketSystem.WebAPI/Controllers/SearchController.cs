using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController :ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public SearchController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
        }
        #endregion

        #region custom Methods
        /// <summary>
        /// Search ticket
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SearchTicket")]
      //  [AllowAnonymous]
        public ResponseModel SearchTicket([FromBody]SearchRequest searchparams )
        {
            List<SearchResponse> _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            SearchCaller _newsearchMaster = new SearchCaller();
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                searchparams.tenantID = authenticate.TenantId; // add tenantID to request
                searchparams.assignedTo = authenticate.UserMasterID; // add assignedID to request
                _searchResult = _newsearchMaster.GetSearchResults(new SearchService(_Cache, Db), searchparams);

                 StatusCode = _searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _searchResult.Count > 0 ? _searchResult : null;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }

        /// <summary>
        /// Ticket status count
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("TicketStatusCount")]
       // [AllowAnonymous]
        public ResponseModel TicketStatusCount()
        {
            List<TicketStatusModel> _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            SearchCaller _newsearchMaster = new SearchCaller();
            try
            {
                SearchRequest searchparams = new SearchRequest();
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                searchparams.tenantID = authenticate.TenantId; // add tenantID to request
                searchparams.assignedTo = authenticate.UserMasterID;// add assignedID to request

                _searchResult = _newsearchMaster.GetStatusCount(new SearchService(_Cache, Db), searchparams);

                StatusCode = _searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _searchResult.Count > 0 ? _searchResult : null;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }

        /// <summary>
        /// Get tickets On page load
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTicketsOnPageLoad")]
        [AllowAnonymous]
        public ResponseModel GetTicketsOnPageLoad(int HeaderStatusID)
        {
            List<SearchResponse> _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            SearchCaller _newsearchMaster = new SearchCaller();
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                _searchResult = _newsearchMaster.GetTicketsOnLoad(new SearchService(_Cache, Db), HeaderStatusID,authenticate.TenantId,authenticate.UserMasterID);

                StatusCode = _searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _searchResult.Count > 0 ? _searchResult : null;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = false;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }


        /// <summary>
        /// Get tickets On View Search click
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTicketsOnSearch")]
        //[AllowAnonymous]
        public ResponseModel GetTicketsOnSearch([FromBody]SearchModel searchModel)
        {
            List<SearchResponse> _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            SearchCaller _newsearchMaster = new SearchCaller();
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                //authenticate.UserMasterID = 6;
                //authenticate.TenantId = 1;

                /*
                 SearchModel 
                 */
                searchModel.AssigntoId = authenticate.UserMasterID;
                searchModel.TenantID = authenticate.TenantId;
                

                _searchResult = _newsearchMaster.GetTicketsOnSearch(new SearchService(_Cache, Db), searchModel);

                StatusCode = _searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _searchResult.Count > 0 ? _searchResult : null;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = false;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }


        /// <summary>
        /// Get tickets On page load
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTicketsOnSavedSearch")]
        public ResponseModel GetTicketsOnSavedSearch(int SearchParamID)
        {
            TicketSaveSearch _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            SearchCaller _newsearchMaster = new SearchCaller();
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                _searchResult = _newsearchMaster.GetTicketsOnSavedSearch(new SearchService(_Cache, Db),authenticate.TenantId,authenticate.UserMasterID, SearchParamID);

                StatusCode = _searchResult.ticketList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _searchResult;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = false;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }
        #endregion
    }
}
