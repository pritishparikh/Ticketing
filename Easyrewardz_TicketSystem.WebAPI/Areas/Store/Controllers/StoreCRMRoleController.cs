using Easyrewardz_TicketSystem.CustomModel;
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
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreCRMRoleController : ControllerBase
    {

        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string _UploadedBulkFile;
        #endregion

        #region Cunstructor
        public StoreCRMRoleController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
            _UploadedBulkFile = configuration.GetValue<string>("FileUploadLocation");
        }

        #endregion

        #region custom Methods

        /// <summary>
        /// Create /Update CRMRole
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateUpdateStoreCRMRole")]
        public ResponseModel CreateUpdateStoreCRMRole(int CRMRoleID, string RoleName, bool RoleisActive, string ModulesEnabled, string ModulesDisabled)
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreCRMRoleCaller newCRM = new StoreCRMRoleCaller();
                count = newCRM.InsertUpdateCRMRole(new StoreCRMRoleService(_connectioSting), CRMRoleID, authenticate.TenantId, RoleName, RoleisActive, authenticate.UserMasterID, ModulesEnabled, ModulesDisabled);

                if (CRMRoleID.Equals(0))
                {
                    statusCode = count == 0 ? (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
                }
                else
                {
                    statusCode = count < 0 ? (int)EnumMaster.StatusCode.RecordAlreadyExists : count ==0 ? (int)EnumMaster.StatusCode.InternalServerError :(int)EnumMaster.StatusCode.Success;
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
        [Route("DeleteStoreCRMRole")]
        public ResponseModel DeleteStoreCRMRole(int CRMRoleID)
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreCRMRoleCaller newCRM = new StoreCRMRoleCaller();
                deleteCount = newCRM.DeleteCRMRole(new StoreCRMRoleService(_connectioSting), authenticate.TenantId, CRMRoleID);

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
        [Route("GetStoreCRMRoles")]
        public ResponseModel GetStoreCRMRoles()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<StoreCRMRoleModel> listCRMRole = new List<StoreCRMRoleModel>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreCRMRoleCaller newCRM = new StoreCRMRoleCaller();
                listCRMRole = newCRM.CRMRoleList(new StoreCRMRoleService(_connectioSting), authenticate.TenantId);

                statusCode = listCRMRole.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = listCRMRole;

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
        [Route("GetStoreRolesByUserID")]
        public ResponseModel GetStoreRolesByUserID()
        {

            ResponseModel objResponseModel = new ResponseModel();
            StoreCRMRoleModel objCRMRoleModel = new StoreCRMRoleModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreCRMRoleCaller newCRM = new StoreCRMRoleCaller();
                objCRMRoleModel = newCRM.GetCRMRoleByUserID(new StoreCRMRoleService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID);

                statusCode = objCRMRoleModel == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCRMRoleModel;

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
        [Route("GetStoreCRMRoleDropdown")]
        public ResponseModel GetStoreCRMRoleDropdown()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<StoreCRMRoleModel> objCRMRoleModel = new List<StoreCRMRoleModel>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreCRMRoleCaller newCRM = new StoreCRMRoleCaller();
                objCRMRoleModel = newCRM.CRMRoleDropdown(new StoreCRMRoleService(_connectioSting), authenticate.TenantId);

                statusCode = objCRMRoleModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCRMRoleModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Bullk Upload  SLA
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadStoreCRMRole")]
        public ResponseModel BulkUploadStoreCRMRole(int RoleFor = 3)
        {
            string downloadFilePath = string.Empty;
            string bulkUploadFilesPath = string.Empty;
            bool errorFileSaved = false;
            bool successFileSaved = false;
            int count = 0;
            List<string> CSVlist = new List<string>();
            StoreCRMRoleCaller newCRM = new StoreCRMRoleCaller();
            StoreFileUploadCaller fileU = new StoreFileUploadCaller();
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

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
                bulkUploadFilesPath = Folderpath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor);
                downloadFilePath = Folderpath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor);

                #endregion
                dataSetCSV = CommonService.csvToDataSet(Folderpath + "\\" + finalAttchment);
                CSVlist = newCRM.CRMRoleBulkUpload(new StoreCRMRoleService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, RoleFor, dataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successFileSaved = CommonService.SaveFile(downloadFilePath + "\\CRMRole\\ Success" + "\\" + "CRMRoleSuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorFileSaved = CommonService.SaveFile(downloadFilePath + "\\CRMRole\\Error" + "\\" + "CRMRoleErrorFile.csv", CSVlist[1]);

                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectioSting), authenticate.TenantId, finalAttchment, errorFileSaved,
                                   "CRMRoleErrorFile.csv", "CRMRoleSuccessFile.csv", authenticate.UserMasterID, "CRMRole",
                                   downloadFilePath + "\\CRMRole\\Error" + "\\" + "CRMRoleErrorFile.csv",
                                   downloadFilePath + "\\CRMRole\\ Success" + "\\" + "CRMRoleSuccessFile.csv", RoleFor
                                   );
                #endregion

                statusCode = successFileSaved ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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
