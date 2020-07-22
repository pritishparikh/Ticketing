using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.WebAPI.Provider.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreReportController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        #endregion


        #region Cunstructor
        public StoreReportController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
        }
        #endregion


        /// <summary>
        /// Search the Report
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StoreReportSearch")]
        public ResponseModel ReportSearch([FromBody]StoreReportModel SearchParams)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int resultCount = 0;
            StoreReportCaller dbsearchMaster = new StoreReportCaller();
            List<StoreUserListing> StoreUserList = new List<StoreUserListing>();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                SearchParams.TenantID = authenticate.TenantId; // add tenantID to request
                                                               // searchparams.curentUserId = authenticate.UserMasterID; // add currentUserID to request

                StoreUserList = new StoreUserService(_connectioSting).GetStoreUserList(authenticate.TenantId, authenticate.UserMasterID);

                resultCount = dbsearchMaster.StoreReportSearch(new StoreReportService(_connectioSting), SearchParams, StoreUserList);

                StatusCode = resultCount > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = resultCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Search the Report
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownloadStoreReport")]
        public ResponseModel DownloadStoreReportSearch(int ReportID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            string CSVReport = string.Empty;
            string appRoot = string.Empty;
            string Folderpath = string.Empty;
            string URLPath = string.Empty;
            StoreReportCaller dbsearchMaster = new StoreReportCaller();
            List<StoreUserListing> StoreUserList = new List<StoreUserListing>();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                StoreUserList = new StoreUserService(_connectioSting).GetStoreUserList(authenticate.TenantId,authenticate.UserMasterID);
                CSVReport = dbsearchMaster.DownloadStoreReportSearch(new StoreReportService(_connectioSting), ReportID, authenticate.UserMasterID, authenticate.TenantId, StoreUserList);

                appRoot = Directory.GetCurrentDirectory();

                string CSVFileName = "StoreReport_" + ReportID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".csv";

                Folderpath = Path.Combine(appRoot, "ReportDownload");
                if (!Directory.Exists(Folderpath))
                {
                    Directory.CreateDirectory(Folderpath);
                }


                if (!string.IsNullOrEmpty(CSVReport))
                {
                    URLPath = rootPath + "ReportDownload" + "/" + CSVFileName;
                    Folderpath = Path.Combine(Folderpath, CSVFileName);
                    CommonService.SaveFile(Folderpath, CSVReport);
                }


                StatusCode = !string.IsNullOrEmpty(CSVReport) ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = !string.IsNullOrEmpty(CSVReport) ? URLPath : string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Check If Report Name Exists
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="ReportName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckIfReportNameExists")]
        public ResponseModel CheckIfReportNameExists(int ReportID, string ReportName)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            bool IsExists = false;
            StoreReportCaller dbsearchMaster = new StoreReportCaller();
            List<StoreUserListing> StoreUserList = new List<StoreUserListing>();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                StoreUserList = new StoreUserService(_connectioSting).GetStoreUserList(authenticate.TenantId, authenticate.UserMasterID);
                IsExists = dbsearchMaster.CheckIfReportNameExists(new StoreReportService(_connectioSting), ReportID, ReportName, authenticate.TenantId);

                StatusCode = IsExists ? (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = IsExists;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Schedule Store Report
        /// </summary>
        /// <param name="scheduleMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ScheduleStoreReport")]
        public ResponseModel Schedule([FromBody]ScheduleMaster scheduleMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreReportCaller reportCaller = new StoreReportCaller();

                int result = reportCaller.ScheduleStoreReport(new StoreReportService(_connectioSting), scheduleMaster, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result > 0 ?
                       (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Get the Store Reports
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreReports")]
        public ResponseModel GetStoreReports()
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<ReportModel> objReportList = new List<ReportModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreReportCaller newReport = new StoreReportCaller();

                objReportList = newReport.GetStoreReportList(new StoreReportService(_connectioSting), authenticate.TenantId);

                StatusCode = objReportList.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objReportList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Delete Store Report
        /// </summary>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteStoreReport")]
        public ResponseModel DeleteStoreReport(int ReportID)
        {
            int Deletecount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreReportCaller newReport = new StoreReportCaller();

                Deletecount = newReport.DeleteStoreReport(new StoreReportService(_connectioSting), authenticate.TenantId, ReportID);

                StatusCode =
                Deletecount.Equals(0) ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Deletecount;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Insert New report
        /// </summary>
        /// <param name="scheduleMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveStoreReport")]
        public ResponseModel SaveStoreReport([FromBody]StoreReportRequest ReportMaster )
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreReportCaller reportCaller = new StoreReportCaller();
                ReportMaster.CreatedBy = authenticate.UserMasterID;
                ReportMaster.ModifyBy = authenticate.UserMasterID;
                ReportMaster.TenantID = authenticate.TenantId;

                int result = reportCaller.InsertStoreReport(new StoreReportService(_connectioSting), ReportMaster);
                StatusCode =
                result >= 0 ?
                       (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Get CampaignNames
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignNames")]
        public ResponseModel GetCampaignNames()
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<CampaignScriptName> objCampaignList = new List<CampaignScriptName>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreReportCaller newReport = new StoreReportCaller();

                objCampaignList = newReport.GetCampaignNames(new StoreReportService(_connectioSting));

                StatusCode = objCampaignList.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCampaignList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// User Wise Login Report
        /// </summary>
        /// <param name="scheduleMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserWiseLoginReport")]
        public ResponseModel UserWiseLoginReport([FromBody]LoginReportRequest loginReportRequest)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<LoginReportResponse> LoginReportResponse = new List<LoginReportResponse>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                loginReportRequest.TenantID = authenticate.TenantId;
                StoreReportCaller reportCaller = new StoreReportCaller();

                LoginReportResponse = reportCaller.UserLoginReport(new StoreReportService(_connectioSting), loginReportRequest);
                statusCode =
                LoginReportResponse.Count > 0 ?
                       (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = LoginReportResponse;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

    }
}
