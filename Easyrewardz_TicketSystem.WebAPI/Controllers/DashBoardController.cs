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
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string ProfileImg_Resources;
        private readonly string ProfileImg_Image;
        private readonly string API_Url;

        #endregion

        #region Cunstructor
        public DashBoardController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Cache = cache;
            Db = db;
            API_Url = Configuration.GetValue<string>("APIURL");
            ProfileImg_Resources = Configuration.GetValue<string>("ProfileImg_Resources");
            ProfileImg_Image = Configuration.GetValue<string>("ProfileImg_Image");

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
            ResponseModel objResponseModel = new ResponseModel();
            DashBoardDataModel db = new DashBoardDataModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                DashBoardCaller dCaller = new DashBoardCaller();
                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                db = dCaller.GetDashBoardCountData(new DashBoardService(Cache, Db), BrandID, UserIds, fromdate, todate, authenticate.TenantId);

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
                DashBoardCaller dCaller = new DashBoardCaller();
                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                db = dCaller.GetDashBoardGraphdata(new DashBoardService(Cache, Db), BrandID, UserIds, fromdate, todate, authenticate.TenantId);

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
            DashBoardCaller dbSearchMaster = new DashBoardCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                searchparams.TenantID = authenticate.TenantId; // add tenantID to request
                searchparams.curentUserId = authenticate.UserMasterID; // add currentUserID to request


                //searchparams.TenantID = 1; // add tenantID to request
                //searchparams.curentUserId = 9; // add currentUserID to request
                searchResult = dbSearchMaster.GetDashboardTicketsOnSearch(new DashBoardService(Cache, Db), searchparams);

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
            DashBoardCaller dbSearchMaster = new DashBoardCaller();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                searchparams.TenantID = authenticate.TenantId; // add tenantID to request
                searchparams.curentUserId = authenticate.UserMasterID; // add currentUserID to request
                strCSV = dbSearchMaster.DashBoardSearchDataToCSV(new DashBoardService(Cache, Db), searchparams);

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
            LoggedInAgentModel loggedinAccInfo = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            DashBoardCaller dbSearchMaster = new DashBoardCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                var folderName = Path.Combine(ProfileImg_Resources, ProfileImg_Image);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                loggedinAccInfo = dbSearchMaster.GetLogginAccountInfo(new DashBoardService(Cache, Db),
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
        /// Saved Search
        /// </summary>
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
                DashBoardCaller dCaller = new DashBoardCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = dCaller.AddDashBoardSearch(new DashBoardService(Cache, Db), authenticate.UserMasterID, SearchSaveName, parameter, authenticate.TenantId);
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
        /// Delete Saved Searcht
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("DeleteDashBoardSavedSearch")]
        public ResponseModel DeleteDashBoardSavedSearch(int SearchParamID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                DashBoardCaller dCaller = new DashBoardCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));


                int result = dCaller.DeleteDashBoardSavedSearch(new DashBoardService(Cache, Db), SearchParamID, authenticate.UserMasterID);
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
        /// List of Saved Search
        /// </summary>
        /// <param name="UserID"></param>
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
                DashBoardCaller dCaller = new DashBoardCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objSavedSearch = dCaller.ListSavedDashBoardSearch(new DashBoardService(Cache, Db), authenticate.UserMasterID);
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
            DashBoardCaller dCaller = new DashBoardCaller();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                searchResult = dCaller.GetDashBoardTicketsOnSavedSearch(new DashBoardService(Cache, Db), authenticate.TenantId, authenticate.UserMasterID, SearchParamID);

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
