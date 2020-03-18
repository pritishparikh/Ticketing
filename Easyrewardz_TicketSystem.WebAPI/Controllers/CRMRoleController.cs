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
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CRMRoleController: ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public CRMRoleController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
            rootPath = configuration.GetValue<string>("APIURL");
        }
        #endregion

        #region custom Methods

        /// <summary>
        /// Create /Update CRMRole
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateUpdateCRMRole")]
        public ResponseModel CreateUpdateCRMRole(int CRMRoleID, string RoleName, bool RoleisActive,string ModulesEnabled,string ModulesDisabled)
        {
            int count = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
               string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);


                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();

                   count = _newCRM.InsertUpdateCRMRole(new CRMRoleService(_Cache, Db), CRMRoleID, authenticate.TenantId, RoleName, RoleisActive, authenticate.UserMasterID, ModulesEnabled, ModulesDisabled);

                StatusCode = count == 0 ?(int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = count;

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
        /// Delete CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteCRMRole")]
        public ResponseModel CRMRol(int CRMRoleID)
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
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();
                Deletecount = _newCRM.DeleteCRMRole(new CRMRoleService(_Cache, Db), authenticate.TenantId, CRMRoleID);

                StatusCode =
                Deletecount == 0 ? (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

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
        /// View  CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCRMRoles")]
        public ResponseModel GetCRMRoles()
        {
          
            ResponseModel _objResponseModel = new ResponseModel();
            List<CRMRoleModel> _objresponseModel = new List<CRMRoleModel>();
           int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();
                _objresponseModel = _newCRM.CRMRoleList(new CRMRoleService(_Cache, Db), authenticate.TenantId);

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


        /// <summary>
        /// View  CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRolesByUserID")]
        public ResponseModel GetRolesByUserID(int UserId)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            CRMRoleModel _objresponseModel = new CRMRoleModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();
                _objresponseModel = _newCRM.GetCRMRoleByUserID(new CRMRoleService(_Cache, Db), authenticate.TenantId, UserId);

                StatusCode = _objresponseModel == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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

        /// <summary>
        /// GetCRMRoleDropdown
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCRMRoleDropdown")]
        public ResponseModel GetCRMRoleDropdown()
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<CRMRoleModel> _objresponseModel = new List<CRMRoleModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();
                _objresponseModel = _newCRM.CRMRoleDropdown(new CRMRoleService(_Cache, Db), authenticate.TenantId);

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


        /// <summary>
        /// Bullk Upload  SLA
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadCRMRole")]
        public ResponseModel BulkUploadCRMRole(int RoleFor)
        {

            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            int count = 0;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            List<string> CSVlist = new List<string>();
            SettingsCaller _newCRM = new SettingsCaller();
            ResponseModel _objResponseModel = new ResponseModel();
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
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                #region FilePath
                BulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor);
                DownloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor);

                #endregion

                #region Read from Form

                //if (files.Count > 0)
                //{
                //    for (int i = 0; i < files.Count; i++)
                //    {
                //        fileName += files[i].FileName.Replace(".", "_" + authenticate.UserMasterID + "_" + timeStamp + ".") + ",";
                //    }
                //    finalAttchment = fileName.TrimEnd(',');
                //}

                ////var exePath = Path.GetDirectoryName(System.Reflection
                ////     .Assembly.GetExecutingAssembly().CodeBase);
                ////Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                ////var appRoot = appPathMatcher.Match(exePath).Value;
                ////string Folderpath = appRoot + "\\" + "BulkUpload\\CRMRole";



                //if (files.Count > 0)
                //{
                //    filesName = finalAttchment.Split(",");
                //    for (int i = 0; i < files.Count; i++)
                //    {
                //        using (var ms = new MemoryStream())
                //        {
                //            files[i].CopyTo(ms);
                //            var fileBytes = ms.ToArray();
                //            MemoryStream msfile = new MemoryStream(fileBytes);
                //            FileStream docFile = new FileStream(BulkUploadFilesPath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                //            msfile.WriteTo(docFile);
                //            docFile.Close();
                //            ms.Close();
                //            msfile.Close();

                //        }
                //    }
                //}

                //DataSetCSV = CommonService.csvToDataSet(BulkUploadFilesPath + "\\" + filesName[0]);

                #endregion

                DataSetCSV = CommonService.csvToDataSet(BulkUploadFilesPath + "\\CRMRolesBulk.csv" );
                CSVlist = _newCRM.CRMRoleBulkUpload(new CRMRoleService(_Cache, Db), authenticate.TenantId, authenticate.UserMasterID, RoleFor, DataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\CRMRole\\ Success" + "\\" + "CRMRoleSuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    successfilesaved = CommonService.SaveFile(DownloadFilePath + "\\CRMRole\\Error" + "\\" + "CRMRoleErrorFile.csv", CSVlist[1]);

                count = _newCRM.CreateFileUploadLog(new FileUploadService(_Cache, Db), authenticate.TenantId, "CRMRolesBulk.csv", errorfilesaved,
                                   "HierarchyErrorFile.csv", "HierarchySuccessFile.csv", authenticate.UserMasterID, "CRMRole",
                                   DownloadFilePath + "\\Hierarchy\\Error" + "\\" + "CRMRoleErrorFile.csv",
                                   DownloadFilePath + "\\Hierarchy\\ Success" + "\\" + "CRMRoleSuccessFile.csv", RoleFor
                                   );
                #endregion

                StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = CSVlist.Count;

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


        #endregion

        /// <summary>
        /// View  CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRolesByUserID")]
        public ResponseModel GetRolesByUserID()
        {

            ResponseModel _objResponseModel = new ResponseModel();
            CRMRoleModel _objresponseModel = new CRMRoleModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();
                _objresponseModel = _newCRM.GetCRMRoleByUserID(new CRMRoleService(_Cache, Db), authenticate.TenantId, authenticate.UserMasterID);

                StatusCode = _objresponseModel == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
    }
}
