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

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController :ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public SearchController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
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
            List<SearchResponse> searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            SearchCaller newSearchMaster = new SearchCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                searchparams.tenantID = authenticate.TenantId; // add tenantID to request
                searchparams.assignedTo = authenticate.UserMasterID; // add assignedID to request
                searchResult = newSearchMaster.GetSearchResults(new SearchService(Cache, Db), searchparams);

                 statusCode = searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResult.Count > 0 ? searchResult : null;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
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
            List<TicketStatusModel> searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            SearchCaller newSearchMaster = new SearchCaller();
            try
            {
                SearchRequest searchparams = new SearchRequest();
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                searchparams.tenantID = authenticate.TenantId; // add tenantID to request
                searchparams.assignedTo = authenticate.UserMasterID;// add assignedID to request

                searchResult = newSearchMaster.GetStatusCount(new SearchService(Cache, Db), searchparams);

                statusCode = searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResult.Count > 0 ? searchResult : null;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
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
            List<SearchResponse> searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            SearchCaller newSearchMaster = new SearchCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                searchResult = newSearchMaster.GetTicketsOnLoad(new SearchService(Cache, Db), HeaderStatusID,authenticate.TenantId,authenticate.UserMasterID);

                statusCode = searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResult.Count > 0 ? searchResult : null;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = false;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
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
            List<SearchResponse> searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            SearchCaller newSearchMaster = new SearchCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                //authenticate.UserMasterID = 6;
                //authenticate.TenantId = 1;

                /*
                 SearchModel 
                 */
                searchModel.AssigntoId = authenticate.UserMasterID;
                searchModel.TenantID = authenticate.TenantId;
                

                searchResult = newSearchMaster.GetTicketsOnSearch(new SearchService(Cache, Db), searchModel);

                statusCode = searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResult.Count > 0 ? searchResult : null;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = false;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
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
            TicketSaveSearch searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            SearchCaller newSearchMaster = new SearchCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                searchResult = newSearchMaster.GetTicketsOnSavedSearch(new SearchService(Cache, Db),authenticate.TenantId,authenticate.UserMasterID, SearchParamID);

                statusCode = searchResult.ticketList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResult;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = false;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }
        #endregion
    }
}
