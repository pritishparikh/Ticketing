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
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class ReportController: ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public ReportController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// View  Modules Items
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateReport")]
        public ResponseModel InserCreateReporttReport( string ReportName, bool isReportActive, string TicketReportParams,
            bool IsDaily, bool IsDailyForMonth, bool IsWeekly, bool IsWeeklyForMonth, bool IsDailyForYear, bool IsWeeklyForYear)

        {

            int insertcount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newReport= new SettingsCaller();

                insertcount = _newReport.InsertReport(new ReportService(_connectioSting), authenticate.TenantId,
                     ReportName,  isReportActive,  TicketReportParams, IsDaily,  IsDailyForMonth,  IsWeekly,  IsWeeklyForMonth,
                     IsDailyForYear,  IsWeeklyForYear, authenticate.UserMasterID);

                StatusCode =
                insertcount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = insertcount;

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
        /// Delete Report
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteReport")]
        public ResponseModel DeleteAlert(int ReportID)
        {
            int Deletecount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newReport = new SettingsCaller();

                Deletecount = _newReport.DeleteReport(new ReportService(_connectioSting), authenticate.TenantId, ReportID);

                StatusCode =
                Deletecount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Deletecount;

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
        /// Save Report for download.Update Isdownload flag in ReportMaster
        /// </summary>
        /// <param name="ScheduleID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveReportForDownload")]
        public ResponseModel SaveReportForDownload(int ScheduleID)
        {
            int saveCount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newReport = new SettingsCaller();

                saveCount = _newReport.SaveReportForDownload(new ReportService(_connectioSting), authenticate.TenantId,authenticate.UserMasterID, ScheduleID);

                StatusCode =
                saveCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = saveCount;

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
        /// Delete Report
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReports")]
        public ResponseModel GetReports()
        {
            int Deletecount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            List<ReportModel> _objresponseModel = new List<ReportModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newReport = new SettingsCaller();

                _objresponseModel = _newReport.GetReportList(new ReportService(_connectioSting), authenticate.TenantId);

                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

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
        [HttpPost]
        [Route("ReportSearch")]
        public ResponseModel ReportSearch([FromBody]ReportSearchModel searchparams)
        {
            List<SearchResponseReport> _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int resultCount = 0;
            SettingsCaller _dbsearchMaster = new SettingsCaller();
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                searchparams.TenantID = authenticate.TenantId; // add tenantID to request
                searchparams.curentUserId = authenticate.UserMasterID; // add currentUserID to request


                //searchparams.TenantID = 1; // add tenantID to request
                //searchparams.curentUserId = 9; // add currentUserID to request
                resultCount = _dbsearchMaster.GetReportSearch(new ReportService(_connectioSting), searchparams);

                StatusCode = resultCount > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData =resultCount;
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
        /// DownloadDefaultReportAPI
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownloadDefaultReport")]
        public ResponseModel DownloadDefaultReport([FromBody] DefaultReportRequestModel objRequest)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            SettingsCaller _dbsearchMaster = new SettingsCaller();
            string strcsv = string.Empty;
            try
            {              
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                strcsv = _dbsearchMaster.DownloadDefaultReport(new ReportService(_connectioSting), objRequest, authenticate.UserMasterID, authenticate.TenantId);

                string dateformat = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string csvname = "DefaultReport" + "_" + dateformat + ".csv";

                var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;

                string Folderpath = Path.Combine(appRoot, "ReportDownload");
                string URLFolderpath = Path.Combine(rootPath, "ReportDownload");

                if (!Directory.Exists(Folderpath))
                {
                    Directory.CreateDirectory(Folderpath);
                }

                Folderpath = Path.Combine(Folderpath, csvname);
                URLFolderpath = URLFolderpath + @"/" + csvname;
                CommonService.SaveFile(Folderpath, strcsv);

                StatusCode = (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;

                if (System.IO.File.Exists(Folderpath))
                {
                    FileInfo fileInfo = new FileInfo(Folderpath);
                    float sizeInMB =  (fileInfo.Length / 1024f) / 1024f;                   
                    if (sizeInMB > 5)
                    {
                        _objResponseModel.ResponseData = !string.IsNullOrEmpty(URLFolderpath) ? URLFolderpath + "@mail" : null;
                    }
                    else
                    {
                        _objResponseModel.ResponseData = URLFolderpath;
                    }
                }

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

        [HttpPost]
        [Route("DownloadReportSearch")]
        public ResponseModel DownloadReportSearch(int SchedulerID)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            SettingsCaller _dbsearchMaster = new SettingsCaller();
            string strcsv = string.Empty;
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(_token);
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                strcsv = _dbsearchMaster.DownloadReportSearch(new ReportService(_connectioSting), SchedulerID, authenticate.UserMasterID, authenticate.TenantId);

                string dateformat = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string csvname = "Repost" + SchedulerID + "_" + dateformat + ".csv";

                var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;

                string Folderpath = Path.Combine(appRoot , "ReportDownload");
                string URLFolderpath = Path.Combine(rootPath, "ReportDownload");

                if (!Directory.Exists(Folderpath))
                {
                    Directory.CreateDirectory(Folderpath);
                }

                Folderpath = Path.Combine(Folderpath , csvname);
                URLFolderpath = URLFolderpath + @"/" + csvname ;
                CommonService.SaveFile(Folderpath, strcsv);

                StatusCode =  (int)EnumMaster.StatusCode.Success ;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                if (System.IO.File.Exists(Folderpath))
                {
                    FileInfo fileInfo = new FileInfo(Folderpath);
                    float sizeInMB = (fileInfo.Length / 1024f) / 1024f;
                    if (sizeInMB > 5)
                    {
                        _objResponseModel.ResponseData = !string.IsNullOrEmpty(URLFolderpath) ? URLFolderpath + "@mail" : null;
                    }
                    else
                    {
                        _objResponseModel.ResponseData = URLFolderpath;
                    }
                }
              //  _objResponseModel.ResponseData = !string.IsNullOrEmpty(URLFolderpath) ? URLFolderpath : null;
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

        [HttpPost]
        [Route("SendReportMail")]
        public ResponseModel SendReportMail([FromBody] ReportMailModel reportmailmodel)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            TicketingCaller _ticketingCaller = new TicketingCaller();
            MasterCaller masterCaller = new MasterCaller();

            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _dbsearchMaster = new SettingsCaller();

                bool IsSent = _dbsearchMaster.SendReportMail(new ReportService(_connectioSting), reportmailmodel.EmailID, reportmailmodel.FilePath, authenticate.TenantId, authenticate.UserMasterID);

                if (IsSent)
                {
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                    _objResponseModel.ResponseData = "Mail sent successfully.";
                }
                else
                {
                    _objResponseModel.Status = false;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                    _objResponseModel.ResponseData = "Mail sent failure.";
                }


            }
            catch (Exception ex)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                _objResponseModel.ResponseData = "We had an error! Sorry about that.";
            }
            return _objResponseModel;
        }

        #endregion
    }
}
