using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CRMRoleController: ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string rootPath;
        private readonly string _UploadedBulkFile;
        #endregion

        #region Cunstructor
        public CRMRoleController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
            rootPath = Configuration.GetValue<string>("APIURL");
            _UploadedBulkFile = Configuration.GetValue<string>("FileUploadLocation");
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
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);


                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newCRM = new SettingsCaller();

                count = newCRM.InsertUpdateCRMRole(new CRMRoleService(Cache, Db), CRMRoleID, authenticate.TenantId, RoleName, RoleisActive, authenticate.UserMasterID, ModulesEnabled, ModulesDisabled);

                if (CRMRoleID.Equals(0))
                {
                    statusCode = count == 0 ? (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
                }
                else
                {
                    statusCode = count == 0 ? (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;
                }

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// Delete CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteCRMRole")]
        public ResponseModel DeleteCRMRole(int CRMRoleID)
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

                SettingsCaller newCRM = new SettingsCaller();
                deleteCount = newCRM.DeleteCRMRole(new CRMRoleService(Cache, Db), authenticate.TenantId, CRMRoleID);

                statusCode =
                deleteCount == 0 ? (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = deleteCount;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// View  CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCRMRoles")]
        public ResponseModel GetCRMRoles()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<CRMRoleModel> objresponseModel = new List<CRMRoleModel>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newCRM = new SettingsCaller();
                objresponseModel = newCRM.CRMRoleList(new CRMRoleService(Cache, Db), authenticate.TenantId);

                statusCode = objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// View  CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRolesByUserID")]
        public ResponseModel GetRolesByUserID()
        {

            ResponseModel objResponseModel = new ResponseModel();
            CRMRoleModel objCRMRoleResponseModel = new CRMRoleModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newCRM = new SettingsCaller();
                objCRMRoleResponseModel = newCRM.GetCRMRoleByUserID(new CRMRoleService(Cache, Db), authenticate.TenantId, authenticate.UserMasterID);

                statusCode = objCRMRoleResponseModel == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCRMRoleResponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// GetCRMRoleDropdown
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCRMRoleDropdown")]
        public ResponseModel GetCRMRoleDropdown()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<CRMRoleModel> objCRMResponseModel = new List<CRMRoleModel>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newCRM = new SettingsCaller();
                objCRMResponseModel = newCRM.CRMRoleDropdown(new CRMRoleService(Cache, Db), authenticate.TenantId);

                statusCode = objCRMResponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCRMResponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Bullk Upload  SLA Role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadCRMRole")]
        public ResponseModel BulkUploadCRMRole(IFormFile File, int RoleFor = 1)
        {

            string downloadFilePath = string.Empty;
            string bulkUploadFilesPath = string.Empty;
            bool errorFilesaved = false;
            bool successFilesaved = false;
            int count = 0;
            List<string> CSVlist = new List<string>();
            SettingsCaller _newCRM = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            DataSet dataSetCSV = new DataSet();
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            string[] filesName = null;

            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                var files = Request.Form.Files;

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }
                var Keys = Request.Form;

                var exePath = Path.GetDirectoryName(System.Reflection
                     .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string Folderpath = appRoot + "\\" + _UploadedBulkFile;
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
                            string s = Convert.ToBase64String(fileBytes);
                            byte[] a = Convert.FromBase64String(s);
                            // act on the Base64 data

                        }
                    }
                }
                #region FilePath
                bulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor);
                downloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor);

                #endregion
                dataSetCSV = CommonService.csvToDataSet(Folderpath + "\\" + finalAttchment);
                CSVlist = _newCRM.CRMRoleBulkUpload(new CRMRoleService(Cache, Db), authenticate.TenantId, authenticate.UserMasterID, RoleFor, dataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successFilesaved = CommonService.SaveFile(downloadFilePath + "\\CRMRole\\ Success" + "\\" + "CRMRoleSuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorFilesaved = CommonService.SaveFile(downloadFilePath + "\\CRMRole\\Error" + "\\" + "CRMRoleErrorFile.csv", CSVlist[1]);

                count = _newCRM.CreateFileUploadLog(new FileUploadService(Cache, Db), authenticate.TenantId, "CRMRolesBulk.csv", errorFilesaved,
                                   "HierarchyErrorFile.csv", "HierarchySuccessFile.csv", authenticate.UserMasterID, "CRMRole",
                                   downloadFilePath + "\\Hierarchy\\Error" + "\\" + "CRMRoleErrorFile.csv",
                                   downloadFilePath + "\\Hierarchy\\ Success" + "\\" + "CRMRoleSuccessFile.csv", RoleFor
                                   );
                #endregion

                statusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CSVlist.Count;

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
