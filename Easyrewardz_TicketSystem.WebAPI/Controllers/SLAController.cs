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
        private readonly string rootPath;

        private readonly string BulkUpload;
        private readonly string UploadFiles;
        private readonly string DownloadFile;

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

            rootPath = configuration.GetValue<string>("APIURL");
            BulkUpload = configuration.GetValue<string>("BulkUpload");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            DownloadFile = configuration.GetValue<string>("Downloadfile");
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

            //#region Old Code
            //string DownloadFilePath = string.Empty;
            //string BulkUploadFilesPath = string.Empty;
            //bool errorfilesaved = false;
            //bool successfilesaved = false;
            //int count = 0;
            //List<string> CSVlist = new List<string>();
            //SettingsCaller newSLA = new SettingsCaller();
            //ResponseModel objResponseModel = new ResponseModel();
            //int StatusCode = 0;
            //string statusMessage = "";
            //string fileName = "";
            //string finalAttchment = "";
            //string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            //DataSet DataSetCSV = new DataSet();
            //string[] filesName = null;


            //try
            //{
            //    var files = Request.Form.Files;

            //    string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
            //    Authenticate authenticate = new Authenticate();
            //    authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));



            //    if (files.Count > 0)
            //    {
            //        for (int i = 0; i < files.Count; i++)
            //        {
            //            fileName += files[i].FileName.Replace(".", "_" + authenticate.UserMasterID + "_" + timeStamp + ".") + ",";
            //        }
            //        finalAttchment = fileName.TrimEnd(',');
            //    }

            //    #region FilePath
            //    string Folderpath = Directory.GetCurrentDirectory();
            //    filesName = finalAttchment.Split(",");


            //    BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor));
            //    DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor));


            //    if (!Directory.Exists(BulkUploadFilesPath))
            //    {
            //        Directory.CreateDirectory(BulkUploadFilesPath);
            //    }



            //    if (files.Count > 0)
            //    {

            //        for (int i = 0; i < files.Count; i++)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                files[i].CopyTo(ms);
            //                var fileBytes = ms.ToArray();
            //                MemoryStream msfile = new MemoryStream(fileBytes);
            //                FileStream docFile = new FileStream(Path.Combine(BulkUploadFilesPath, filesName[i]), FileMode.Create, FileAccess.Write);
            //                msfile.WriteTo(docFile);
            //                docFile.Close();
            //                ms.Close();
            //                msfile.Close();
            //                string s = Convert.ToBase64String(fileBytes);
            //                byte[] a = Convert.FromBase64String(s);
            //                // act on the Base64 data

            //            }
            //        }
            //    }

            //    #endregion



            //    DataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));
            //    CSVlist = newSLA.SLABulkUpload(new SLAServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, SLAFor, DataSetCSV);


            //    #region Create Error and Success files and  Insert in FileUploadLog

            //    string SuccessFileName = "SLASuccessFile_" + timeStamp + ".csv";
            //    string ErrorFileName = "SLAErrorFile_" + timeStamp + ".csv";

            //    string SuccessFileUrl = rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor) + "/Success/" + SuccessFileName;
            //    string ErrorFileUrl = rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor) + "/Error/" + ErrorFileName;

            //    if (!string.IsNullOrEmpty(CSVlist[0]))
            //        successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);

            //    if (!string.IsNullOrEmpty(CSVlist[1]))
            //        errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);

            //    count = newSLA.CreateFileUploadLog(new FileUploadService(_connectioSting), authenticate.TenantId, filesName[0], errorfilesaved,
            //                     ErrorFileName, SuccessFileName, authenticate.UserMasterID, "SLA", SuccessFileUrl, ErrorFileUrl, SLAFor);
            //    #endregion

            //    StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
            //    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
            //    objResponseModel.Status = true;
            //    objResponseModel.StatusCode = StatusCode;
            //    objResponseModel.Message = statusMessage;
            //    objResponseModel.ResponseData = count;
            //    #endregion Old Code

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



                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", "_" + authenticate.UserMasterID + "_" + timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }

                #region FilePath
                string Folderpath = Directory.GetCurrentDirectory();
                filesName = finalAttchment.Split(",");


                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor));
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor));


                if (!Directory.Exists(BulkUploadFilesPath))
                {
                    Directory.CreateDirectory(BulkUploadFilesPath);
                }



                if (files.Count > 0)
                {

                    for (int i = 0; i < files.Count; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(Path.Combine(BulkUploadFilesPath, filesName[i]), FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();
                            string s = Convert.ToBase64String(fileBytes);
                            byte[] a = Convert.FromBase64String(s);
                            // act on the Base64 data

                        }
                    }
                }

                #endregion



                DataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));
                CSVlist = newSLA.SLABulkUpload(new SLAServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, SLAFor, DataSetCSV);


                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "SLASuccessFile_" + timeStamp + ".csv";
                string ErrorFileName = "SLAErrorFile_" + timeStamp + ".csv";

                string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                  rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor) + "/Error/" + ErrorFileName : string.Empty;

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);

                count = newSLA.CreateFileUploadLog(new FileUploadService(_connectioSting), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "SLA", SuccessFileUrl, ErrorFileUrl, SLAFor);
                #endregion

                StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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