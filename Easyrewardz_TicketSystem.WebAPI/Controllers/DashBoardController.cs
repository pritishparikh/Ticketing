using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class DashBoardController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string ProfileImg_Resources;
        private readonly string ProfileImg_Image;
        private readonly string API_Url;

        #endregion

        #region Cunstructor
        public DashBoardController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            _Cache = cache;
            Db = db;
            API_Url = configuration.GetValue<string>("APIURL");
            ProfileImg_Resources = configuration.GetValue<string>("ProfileImg_Resources");
            ProfileImg_Image = configuration.GetValue<string>("ProfileImg_Image");

        }
        #endregion

        /// <summary>
        /// Dashboard count data
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="UserIds"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        #region custom Methods
        [HttpPost]
        [Route("DashBoardCountData")]
        public ResponseModel DashBoardCountData(string BrandID, string UserIds, string fromdate, string todate)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            DashBoardDataModel db = new DashBoardDataModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                DashBoardCaller dcaller = new DashBoardCaller();
                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                db = dcaller.GetDashBoardCountData(new DashBoardService(_Cache, Db), BrandID, UserIds, fromdate, todate, authenticate.TenantId);

                StatusCode = db == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = db;
            }
            catch (Exception)
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
        /// Dashboard graph data
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="UserIds"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DashBoardGraphData")]
        public ResponseModel DashBoardGraphData(string BrandID, string UserIds, string fromdate, string todate)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            DashBoardGraphModel db = new DashBoardGraphModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                DashBoardCaller dcaller = new DashBoardCaller();
                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                db = dcaller.GetDashBoardGraphdata(new DashBoardService(_Cache, Db), BrandID, UserIds, fromdate, todate, authenticate.TenantId);

                StatusCode = db == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = db;
            }
            catch (Exception)
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
        /// Dashboard search ticket
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DashBoardSearchTicket")]
        public ResponseModel DashBoardSearchTicket([FromBody]SearchModelDashBoard searchparams)
        {
            List<SearchResponseDashBoard> _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            DashBoardCaller _dbsearchMaster = new DashBoardCaller();
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                searchparams.TenantID = authenticate.TenantId; // add tenantID to request
                searchparams.curentUserId = authenticate.UserMasterID; // add currentUserID to request


                //searchparams.TenantID = 1; // add tenantID to request
                //searchparams.curentUserId = 9; // add currentUserID to request
                _searchResult = _dbsearchMaster.GetDashboardTicketsOnSearch(new DashBoardService(_Cache, Db), searchparams);

                StatusCode = _searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _searchResult.Count > 0 ? _searchResult : null;
            }
            catch (Exception)
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
        /// Export Dashboard search to CSV
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExportDashBoardSearchToCSV")]
        public ResponseModel ExportDashBoardSearchToCSV([FromBody] SearchModelDashBoard searchparams)
        {
           
            string strcsv = string.Empty;
            ResponseModel _objResponseModel = new ResponseModel();
            DashBoardCaller _dbsearchMaster = new DashBoardCaller();
           
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                searchparams.TenantID = authenticate.TenantId; // add tenantID to request
                searchparams.curentUserId = authenticate.UserMasterID; // add currentUserID to request
                strcsv = _dbsearchMaster.DashBoardSearchDataToCSV(new DashBoardService(_Cache, Db), searchparams);

                StatusCode = !string.IsNullOrEmpty(strcsv) ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = !string.IsNullOrEmpty(strcsv) ? File(new System.Text.UTF8Encoding().GetBytes(strcsv), "text/csv", "ABC.csv") : null;


            }
            catch (Exception)
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
        /// LoggedIn Account details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("LoggedInAccountDetails")]
        public ResponseModel LoggedInAccountDetails()
        {
            LoggedInAgentModel  _loggedinAccInfo = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0; string statusMessage = "";
            DashBoardCaller _dbsearchMaster = new DashBoardCaller();  
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                var folderName = Path.Combine(ProfileImg_Resources, ProfileImg_Image);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                _loggedinAccInfo = _dbsearchMaster.GetLogginAccountInfo(new DashBoardService(_Cache, Db),
                    authenticate.TenantId, authenticate.UserMasterID, pathToSave);

               

                StatusCode = _loggedinAccInfo != null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _loggedinAccInfo != null ? _loggedinAccInfo : null;
            }
            catch (Exception)
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
        /// Saved Search
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        [Route("DashBoardSaveSearch")]
        public ResponseModel DashBoardSaveSearch(string SearchSaveName, string parameter)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;

            string statusMessage = "";
            try
            {
                DashBoardCaller dcaller = new DashBoardCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                int result = dcaller.AddDashBoardSearch(new DashBoardService(_Cache, Db), authenticate.UserMasterID, SearchSaveName, parameter, authenticate.TenantId);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
        }

        /// <summary>
        /// Delete Saved Searcht
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("DeleteDashBoardSavedSearch")]
        public ResponseModel DeleteDashBoardSavedSearch(int SearchParamID)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                DashBoardCaller dcaller = new DashBoardCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));


                int result = dcaller.DeleteDashBoardSavedSearch(new DashBoardService(_Cache, Db), SearchParamID, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
        }

        /// <summary>
        /// List of Saved Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDashBoardSavedSearch")]
        public ResponseModel GetDashBoardSavedSearch()
        {
            List<UserTicketSearchMaster> objSavedSearch = new List<UserTicketSearchMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                DashBoardCaller dcaller = new DashBoardCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                objSavedSearch = dcaller.ListSavedDashBoardSearch(new DashBoardService(_Cache, Db), authenticate.UserMasterID);
                StatusCode =
                objSavedSearch.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objSavedSearch;
            }
            catch (Exception)
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
        /// Get Dashboard tickets saved search
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDashBoardTicketsOnSavedSearch")]
        public ResponseModel GetDashBoardTicketsOnSavedSearch(int SearchParamID)
        {
            DashBoardSavedSearch _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            DashBoardCaller dcaller = new DashBoardCaller();
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                _searchResult = dcaller.GetDashBoardTicketsOnSavedSearch(new DashBoardService(_Cache, Db), authenticate.TenantId, authenticate.UserMasterID, SearchParamID);

                StatusCode = _searchResult.DashboardTicketList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _searchResult;
            }
            catch (Exception)
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
