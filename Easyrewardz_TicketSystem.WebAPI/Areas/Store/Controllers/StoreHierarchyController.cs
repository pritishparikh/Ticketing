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
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreHierarchyController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        private readonly string rootPath;
        private readonly string _UploadedBulkFile;
        #endregion
        #region Constructor
        public StoreHierarchyController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
            _UploadedBulkFile = configuration.GetValue<string>("FileUploadLocation");
        }
        #endregion
        #region Custom Methods
        /// <summary>
        /// CreateStoreHierarchy
        /// </summary>
        /// <param name="CustomHierarchymodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateStoreHierarchy")]
        public ResponseModel CreateStoreHierarchy([FromBody] CustomHierarchymodel customHierarchymodel)
        {
            StoreHierarchyCaller storeHierarchyCaller = new StoreHierarchyCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                customHierarchymodel.TenantID = authenticate.TenantId;
                customHierarchymodel.CreatedBy = authenticate.UserMasterID;
                int result = storeHierarchyCaller.CreateStoreHierarchy(new StoreHierarchyService(_connectionSting), customHierarchymodel);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
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
        /// UpdateStoreHierarchy
        /// </summary>
        /// <param name="CustomHierarchymodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateStoreHierarchy")]
        public ResponseModel UpdateStoreHierarchy([FromBody] CustomHierarchymodel customHierarchymodel)
        {
            StoreHierarchyCaller storeHierarchyCaller = new StoreHierarchyCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                customHierarchymodel.TenantID = authenticate.TenantId;
                customHierarchymodel.CreatedBy = authenticate.UserMasterID;
                int result = storeHierarchyCaller.UpdateStoreHierarchy(new StoreHierarchyService(_connectionSting), customHierarchymodel);
                statusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
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
        /// DeleteStoreHierarchy
        /// </summary>
        /// <param name="designationID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteStoreHierarchy")]
        public ResponseModel DeleteStoreHierarchy(int designationID)
        {
            StoreHierarchyCaller storeHierarchyCaller = new StoreHierarchyCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = storeHierarchyCaller.DeleteStoreHierarchy(new StoreHierarchyService(_connectionSting), designationID, authenticate.UserMasterID, authenticate.TenantId);
                statusCode =
                 result == 0 ?
                        (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.Success;
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
        /// List Store Hierarchy
        /// </summary>
        /// <returns></returns>
        [Route("ListStoreHierarchy")]
        public ResponseModel ListStoreHierarchy()
        {
            List<CustomHierarchymodel> customHierarchymodels = new List<CustomHierarchymodel>();
            StoreHierarchyCaller storeHierarchyCaller = new StoreHierarchyCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                customHierarchymodels = storeHierarchyCaller.ListStoreHierarchy(new StoreHierarchyService(_connectionSting), authenticate.TenantId);
                statusCode =
                   customHierarchymodels.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customHierarchymodels;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Bulk Upload StoreHierarchy
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadStoreHierarchy")]
        public ResponseModel BulkUploadStoreHierarchy()
        {
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            int count = 0;
            StoreHierarchyCaller storeHierarchyCaller = new StoreHierarchyCaller();
            StoreFileUploadCaller fileU = new StoreFileUploadCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            DataSet DataSetCSV = new DataSet();
            List<string> CSVlist = new List<string>();
            string fileName = "";
            string finalAttchment = "";


            try
            {
                var files = Request.Form.Files;
                string timeStamp = DateTime.Now.ToString("ddMMyyyyhhmm");

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
                    string[] filesName = finalAttchment.Split(",");
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



                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                #region FilePath
                BulkUploadFilesPath = Folderpath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)3);
                DownloadFilePath = Folderpath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)3);

                #endregion             

                DataSetCSV = CommonService.csvToDataSet(Folderpath + "\\" + finalAttchment);

                CSVlist = storeHierarchyCaller.StoreHierarchyBulkUpload(new StoreHierarchyService(_connectionSting),
                    authenticate.TenantId, authenticate.UserMasterID, DataSetCSV);
                #region Create Error and Success files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Hierarchy\\ Success" + "\\" + "HierarchySuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Hierarchy\\Error" + "\\" + "HierarchyErrorFile.csv", CSVlist[1]);

                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectionSting), authenticate.TenantId, finalAttchment, errorfilesaved,
                                   "HierarchyErrorFile.csv", "HierarchySuccessFile.csv", authenticate.UserMasterID, "Hierarchy",
                                   DownloadFilePath + "\\Hierarchy\\Error" + "\\" + "HierarchyErrorFile.csv",
                                   DownloadFilePath + "\\Hierarchy\\ Success" + "\\" + "HierarchySuccessFile.csv", 3
                                   );
                #endregion

                StatusCode = successfilesaved ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CSVlist.Count;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;


        }


        /// <summary>
        /// Get designation list for the Designation dropdown
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreDesignationList")]
        public ResponseModel GetStoreDesignationList()
        {
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                StoreHierarchyCaller storeHierarchyCaller = new StoreHierarchyCaller();
                designationMasters = storeHierarchyCaller.GetDesignations(new StoreHierarchyService(_connectionSting), authenticate.TenantId);

                statusCode =
                designationMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = designationMasters;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
            #endregion
        }
    }

}