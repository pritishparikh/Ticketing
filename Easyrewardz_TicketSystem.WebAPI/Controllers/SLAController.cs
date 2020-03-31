using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class SLAController : ControllerBase
    {

        #region Variable Declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor

        /// <summary>
        /// Brand Controller
        /// </summary>
        /// <param name="_iConfig"></param>
        public SLAController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }

        #endregion

        #region Custom Methods 

        /// <summary>
        /// Get SLA Status list for the SLA dropdown
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSLAStatusList")]
        public ResponseModel GetSLAStatusList()
        {
            List<SLAStatus> objSLAStatusList = new List<SLAStatus>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SLACaller _newSLA = new SLACaller();

                objSLAStatusList = _newSLA.GetSLAStatusList(new SLAServices(_connectioSting), authenticate.TenantId);

                StatusCode =
                objSLAStatusList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objSLAStatusList;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        ///Bind Issuetype for SLA Creation DropDown
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetIssueType")]
        public ResponseModel GetIssueType(string SearchText)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<IssueTypeList> _objresponseModel = new List<IssueTypeList>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();

                _objresponseModel = _newCRM.BindIssueTypeList(new SLAServices(_connectioSting), authenticate.TenantId, SearchText);
                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Create SLA
        /// </summary>
        /// <param name="insertSLA"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSLA")]
        public ResponseModel CreateSLA([FromBody]SLAModel insertSLA)
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

                SettingsCaller _newSLA = new SettingsCaller();

                insertSLA.TenantID = authenticate.TenantId;
                insertSLA.CreatedBy = authenticate.UserMasterID;
                insertcount = _newSLA.InsertSLA(new SLAServices(_connectioSting), insertSLA);

                StatusCode =
                insertcount == 0 ?
                     (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = insertcount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        /// Update SLA
        /// </summary>
        /// <param name="SLAID"></param>
        /// <param name="IssueTypeID"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifySLA")]
        public ResponseModel ModifySLA(int SLAID, int IssueTypeID, bool isActive)
        {
            int updatecount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newSLA = new SettingsCaller();

                updatecount = _newSLA.UpdateSLA(new SLAServices(_connectioSting), authenticate.TenantId, SLAID, IssueTypeID, isActive, authenticate.UserMasterID);

                StatusCode =
                updatecount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = updatecount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        /// Delete SLA
        /// </summary>
        /// <param name="SLAID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSLA")]
        public ResponseModel DeleteSLA(int SLAID)
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

                SettingsCaller _newSLA = new SettingsCaller();

                Deletecount = _newSLA.DeleteSLA(new SLAServices(_connectioSting), authenticate.TenantId, SLAID);

                StatusCode =
                Deletecount == 0 ?
                     (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Deletecount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        /// Get SLA
        /// </summary>
        /// <param name="SLAFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSLA")]
        public ResponseModel GetSLA(int SLAFor = 1)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<SLAResponseModel> _objresponseModel = new List<SLAResponseModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();
                //_objresponseModel = _newCRM.SLAList(new SLAServices(_connectioSting), authenticate.TenantId,  pageNo,  PageSize);
                _objresponseModel = _newCRM.SLAList(new SLAServices(_connectioSting), authenticate.TenantId, SLAFor);
                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        /// Bullk Upload  SLA
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadSLA")]
        public ResponseModel BulkUploadSLA(int SLAFor=1)
        {


            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            int count = 0;
            List<string> CSVlist = new List<string>();
            SettingsCaller newSLA = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet DataSetCSV = new DataSet();
            string[] filesName = null;


            try
            {
                var files = Request.Form.Files;

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                #region Read from Form

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", "_" + authenticate.UserMasterID + "_" + timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }

                var exePath = Path.GetDirectoryName(System.Reflection
                     .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string Folderpath = appRoot ;



                if (files.Count > 0)
                {
                    filesName = finalAttchment.Split(",");
                    for (int i = 0; i < files.Count; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(Folderpath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();

                        }
                    }
                }

                
                BulkUploadFilesPath = Folderpath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor);
                DownloadFilePath = Folderpath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor);

              

                DataSetCSV = CommonService.csvToDataSet(Folderpath + "\\" + filesName[0]);
                CSVlist = newSLA.SLABulkUpload(new SLAServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, SLAFor, DataSetCSV);

                #endregion


                #region Create Error and Succes files

                
                successfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Success" + "\\" + "SLAErrorFile.csv", CSVlist[0]);
                errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Error" + "\\" + "SLASuccessFile.csv", CSVlist[0]);

                #endregion

                #region Insert in FileUploadLog

               
                count = newSLA.CreateFileUploadLog(new FileUploadService(_connectioSting), authenticate.TenantId, finalAttchment, errorfilesaved,
                                   "SLAErrorFile.csv", "SLASuccessFile.csv", authenticate.UserMasterID, "SLA",
                                   DownloadFilePath + "\\SLA\\Error" + "\\" + "StoreErrorFile.csv",
                                   DownloadFilePath + "\\SLA\\ Success" + "\\" + "StoreSuccessFile.csv", 1
                                   );
                #endregion

                StatusCode = successfilesaved  ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;


        }

        /// <summary>
        /// Search Issue Type
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SearchIssueType")]
        public ResponseModel SearchIssueType(string SearchText)
        {
            List<IssueTypeList> objIssueTypeList = new List<IssueTypeList>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SLACaller _newSLA = new SLACaller();

                objIssueTypeList = _newSLA.SearchIssueType(new SLAServices(_connectioSting), authenticate.TenantId, SearchText);

                StatusCode =
                objIssueTypeList.Count == 0 ?
                (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objIssueTypeList;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Get SLA Detail
        /// </summary>
        /// <param name="SLAId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSLADetail")]
        public ResponseModel GetSLADetail(int SLAId)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            SLADetail _objresponseModel = new SLADetail();
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                SLACaller _newCRM = new SLACaller();
                _objresponseModel = _newCRM.GetSLADetail(new SLAServices(_connectioSting), authenticate.TenantId, SLAId);
                StatusCode = (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;
            }
            catch (Exception)
            {
                throw;
            }
            return _objResponseModel;
        }


        /// <summary>
        /// Updare SLA Details
        /// </summary>
        /// <param name="sladetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdareSLADetails")]
        public ResponseModel UpdareSLADetails([FromBody]SLADetail sladetails)
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

                SLACaller _newSLA = new SLACaller();
                bool isUdpated = _newSLA.UpdateSLADetails(new SLAServices(_connectioSting), sladetails, authenticate.TenantId, authenticate.UserMasterID);

                StatusCode =
                isUdpated ?
                     (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = insertcount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }



        #endregion
    }
}