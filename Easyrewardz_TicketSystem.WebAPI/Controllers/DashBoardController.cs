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
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string _profileImg_Resources;
        private readonly string _profileImg_Image;
        private readonly string _API_Url;

        #endregion

        #region Constructor
        public DashBoardController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _API_Url = configuration.GetValue<string>("APIURL");
            _profileImg_Resources = configuration.GetValue<string>("ProfileImg_Resources");
            _profileImg_Image = configuration.GetValue<string>("ProfileImg_Image");

        }
        #endregion

        #region custom Methods

        /// <summary>
        /// Dashboard count data
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="UserIds"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("DashBoardCountData")]
        public ResponseModel DashBoardCountData(string BrandID, string UserIds, string fromdate, string todate)
        {
            ResponseModel objResponseModel = new ResponseModel();
            DashBoardDataModel db = new DashBoardDataModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                DashBoardCaller dcaller = new DashBoardCaller();
                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                db = dcaller.GetDashBoardCountData(new DashBoardService(_connectioSting), BrandID, UserIds, fromdate, todate, authenticate.TenantId);

                statusCode = db == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = db;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
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
            ResponseModel objResponseModel = new ResponseModel();
            DashBoardGraphModel db = new DashBoardGraphModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                DashBoardCaller dcaller = new DashBoardCaller();
                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                db = dcaller.GetDashBoardGraphdata(new DashBoardService(_connectioSting), BrandID, UserIds, fromdate, todate, authenticate.TenantId);

                statusCode = db == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = db;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
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
            List<SearchResponseDashBoard> searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            DashBoardCaller dbsearchMaster = new DashBoardCaller();
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                searchparams.TenantID = authenticate.TenantId; // add tenantID to request
                searchparams.curentUserId = authenticate.UserMasterID; // add currentUserID to request

                searchResult = dbsearchMaster.GetDashboardTicketsOnSearch(new DashBoardService(_connectioSting), searchparams);

                statusCode = searchResult.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResult.Count > 0 ? searchResult : null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
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
            string strCSV = string.Empty;
            ResponseModel objResponseModel = new ResponseModel();
            DashBoardCaller dashBoardCaller = new DashBoardCaller();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                searchparams.TenantID = authenticate.TenantId; // add tenantID to request
                searchparams.curentUserId = authenticate.UserMasterID; // add currentUserID to request
                strCSV = dashBoardCaller.DashBoardSearchDataToCSV(new DashBoardService(_connectioSting), searchparams);
                statusCode = !string.IsNullOrEmpty(strCSV) ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = !string.IsNullOrEmpty(strCSV) ? File(new System.Text.UTF8Encoding().GetBytes(strCSV), "text/csv", "ABC.csv") : null;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// LoggedIn Account details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("LoggedInAccountDetails")]
        public ResponseModel LoggedInAccountDetails()
        {
            LoggedInAgentModel  loggedinAccInfo = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0; string statusMessage = "";
            DashBoardCaller _dbsearchMaster = new DashBoardCaller();  
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                var folderName = Path.Combine(_profileImg_Resources, _profileImg_Image);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                loggedinAccInfo = _dbsearchMaster.GetLogginAccountInfo(new DashBoardService(_connectioSting),
                    authenticate.TenantId, authenticate.UserMasterID, pathToSave);

                statusCode = loggedinAccInfo != null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = loggedinAccInfo != null ? loggedinAccInfo : null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// DashBoard Save Search
        /// </summary>
        /// <param name="SearchSaveName"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DashBoardSaveSearch")]
        public ResponseModel DashBoardSaveSearch(string SearchSaveName, string parameter)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;

            string statusMessage = "";
            try
            {
                DashBoardCaller dcaller = new DashBoardCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = dcaller.AddDashBoardSearch(new DashBoardService(_connectioSting), authenticate.UserMasterID, SearchSaveName, parameter, authenticate.TenantId);
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
        /// Delete DashBoard Saved Search
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteDashBoardSavedSearch")]
        public ResponseModel DeleteDashBoardSavedSearch(int SearchParamID)
        {
            ResponseModel  objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                DashBoardCaller dcaller = new DashBoardCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = dcaller.DeleteDashBoardSavedSearch(new DashBoardService(_connectioSting), SearchParamID, authenticate.UserMasterID);
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
        /// Get DashBoard Saved Search
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDashBoardSavedSearch")]
        public ResponseModel GetDashBoardSavedSearch()
        {
            List<UserTicketSearchMaster> objSavedSearch = new List<UserTicketSearchMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                DashBoardCaller dcaller = new DashBoardCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objSavedSearch = dcaller.ListSavedDashBoardSearch(new DashBoardService(_connectioSting), authenticate.UserMasterID);
                statusCode =
                objSavedSearch.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSavedSearch;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
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
            DashBoardSavedSearch searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            DashBoardCaller dcaller = new DashBoardCaller();
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                searchResult = dcaller.GetDashBoardTicketsOnSavedSearch(new DashBoardService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, SearchParamID);

                statusCode = searchResult.DashboardTicketList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = searchResult;
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
