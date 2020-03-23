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
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Distributed;
using Easyrewardz_TicketSystem.MySqlDBContext;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class ReportController: ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public ReportController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
            rootPath = Configuration.GetValue<string>("APIURL");
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

            int insertCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newReport= new SettingsCaller();

                insertCount = newReport.InsertReport(new ReportService(Cache, Db), authenticate.TenantId,
                     ReportName,  isReportActive,  TicketReportParams, IsDaily,  IsDailyForMonth,  IsWeekly,  IsWeeklyForMonth,
                     IsDailyForYear,  IsWeeklyForYear, authenticate.UserMasterID);

                statusCode =
                insertCount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = insertCount;

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
        /// Delete alert
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteReport")]
        public ResponseModel DeleteAlert(int ReportID)
        {
            int deleteCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newReport = new SettingsCaller();

                deleteCount = newReport.DeleteReport(new ReportService(Cache, Db), authenticate.TenantId, ReportID);

                statusCode =
                deleteCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = deleteCount;

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
        /// Save report for download.Update Isdownload flag in ReportMaster
        /// </summary>
        /// <param name="ScheduleID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveReportForDownload")]
        public ResponseModel SaveReportForDownload(int ScheduleID)
        {
            int saveCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newReport = new SettingsCaller();

                saveCount = newReport.SaveReportForDownload(new ReportService(Cache, Db), authenticate.TenantId,authenticate.UserMasterID, ScheduleID);

                statusCode =
                saveCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = saveCount;

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
        /// Delete report
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReports")]
        public ResponseModel GetReports()
        {
            int deleteCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            List<ReportModel> obReportResponseModel = new List<ReportModel>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newReport = new SettingsCaller();

                obReportResponseModel = newReport.GetReportList(new ReportService(Cache, Db), authenticate.TenantId);

                statusCode = obReportResponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obReportResponseModel;

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
        /// Search report
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReportSearch")]
        public ResponseModel ReportSearch([FromBody]ReportSearchModel searchparams)
        {
            List<SearchResponseReport> searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int resultCount = 0;
            SettingsCaller dbSearchMaster = new SettingsCaller();
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
                resultCount = dbSearchMaster.GetReportSearch(new ReportService(Cache, Db), searchparams);

                statusCode = resultCount > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData =resultCount;
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
        /// Download Default ReportAPI
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownloadDefaultReport")]
        public ResponseModel DownloadDefaultReport([FromBody] DefaultReportRequestModel objRequest)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            SettingsCaller dbSearchMaster = new SettingsCaller();
            string strCSV = string.Empty;
            try
            {              
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                strCSV = dbSearchMaster.DownloadDefaultReport(new ReportService(Cache, Db), objRequest, authenticate.UserMasterID, authenticate.TenantId);

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
                CommonService.SaveFile(Folderpath, strCSV);

                statusCode = (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;

                if (System.IO.File.Exists(Folderpath))
                {
                    FileInfo fileInfo = new FileInfo(Folderpath);
                    float sizeInMB =  (fileInfo.Length / 1024f) / 1024f;                   
                    if (sizeInMB > 5)
                    {
                        objResponseModel.ResponseData = !string.IsNullOrEmpty(URLFolderpath) ? URLFolderpath + "@mail" : null;
                    }
                    else
                    {
                        objResponseModel.ResponseData = URLFolderpath;
                    }
                }

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
        /// Download report search
        /// </summary>
        /// <param name="SchedulerID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownloadReportSearch")]
        public ResponseModel DownloadReportSearch(int SchedulerID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            SettingsCaller dbSearchMaster = new SettingsCaller();
            string strCSV = string.Empty;
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                var temp = SecurityService.DecryptStringAES(token);
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                strCSV = dbSearchMaster.DownloadReportSearch(new ReportService(Cache, Db), SchedulerID, authenticate.UserMasterID, authenticate.TenantId);

                string dateformat = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string csvname = "Repost" + SchedulerID + "_" + dateformat + ".csv";

                var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;

                string folderPath = Path.Combine(appRoot , "ReportDownload");
                string URLFolderpath = Path.Combine(rootPath, "ReportDownload");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                folderPath = Path.Combine(folderPath , csvname);
                URLFolderpath = URLFolderpath + @"/" + csvname ;
                CommonService.SaveFile(folderPath, strCSV);

                statusCode =  (int)EnumMaster.StatusCode.Success ;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                if (System.IO.File.Exists(folderPath))
                {
                    FileInfo fileInfo = new FileInfo(folderPath);
                    float sizeInMB = (fileInfo.Length / 1024f) / 1024f;
                    if (sizeInMB > 5)
                    {
                        objResponseModel.ResponseData = !string.IsNullOrEmpty(URLFolderpath) ? URLFolderpath + "@mail" : null;
                    }
                    else
                    {
                        objResponseModel.ResponseData = URLFolderpath;
                    }
                }
              //  objResponseModel.ResponseData = !string.IsNullOrEmpty(URLFolderpath) ? URLFolderpath : null;
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
        /// Send report mail
        /// </summary>
        /// <param name="reportmailmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendReportMail")]
        public ResponseModel SendReportMail([FromBody] ReportMailModel reportmailmodel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            TicketingCaller ticketingCaller = new TicketingCaller();
            MasterCaller masterCaller = new MasterCaller();

            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller _dbsearchMaster = new SettingsCaller();

                bool IsSent = _dbsearchMaster.SendReportMail(new ReportService(Cache, Db), reportmailmodel.EmailID, reportmailmodel.FilePath, authenticate.TenantId, authenticate.UserMasterID);

                if (IsSent)
                {
                    objResponseModel.Status = true;
                    objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                    objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                    objResponseModel.ResponseData = "Mail sent successfully.";
                }
                else
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                    objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                    objResponseModel.ResponseData = "Mail sent failure.";
                }


            }
            catch (Exception ex)
            {
                objResponseModel.Status = true;
                objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                objResponseModel.ResponseData = "We had an error! Sorry about that.";
            }
            return objResponseModel;
        }

        #endregion
    }
}
