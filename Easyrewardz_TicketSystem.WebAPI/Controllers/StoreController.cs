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
    public class StoreController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public StoreController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
            rootPath =  Configuration.GetValue<string>("APIURL");
        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// Search Store details
        /// </summary>
        /// <param name="Storename"></param>
        /// <param name="Storecode"></param>
        /// <param name="Pincode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchStoreDetail")]
        public ResponseModel searchStoreDetail(string SearchText)
        {
            List<StoreMaster> objstoreList = new List<StoreMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                StoreCaller newStore = new StoreCaller();

                objstoreList = newStore.getStoreDetailbyNameAndPincode(new StoreService(Cache, Db), SearchText, authenticate.TenantId);
                statusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objstoreList;
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
        /// Search Store
        /// </summary>
        /// <param name="store"></param>
        /// <param name="searchText"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getStores")]
        public ResponseModel getStores(string searchText)
        {
            List<StoreMaster> storeMasters = new List<StoreMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                StoreCaller _newMasterBrand = new StoreCaller();

                storeMasters = _newMasterBrand.getStores(new StoreService(Cache, Db), searchText, authenticate.TenantId);

                statusCode =
                storeMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = storeMasters;

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
        /// Add Store
        /// </summary>
        /// <param name="TaskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createstore")]
        public ResponseModel createstore([FromBody]StoreMaster storeMaster)
        {
            StoreCaller newStore = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int result = newStore.AddStore(new StoreService(Cache, Db), storeMaster, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = false;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Edit Store
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editstore")]
        public ResponseModel editstore([FromBody]StoreMaster storeMaster, int StoreID)
        {
            StoreCaller newStore = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int result = newStore.EditStore(new StoreService(Cache, Db), storeMaster, StoreID, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;

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
        ///delete Store
        /// </summary>
        /// <param name="StoreID"></param> soft delete 
        /// <returns></returns>
        [HttpPost]
        [Route("deleteStore")]
        public ResponseModel deleteStore(int StoreID)
        {
            StoreCaller newStore = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int result = newStore.DeleteStore(new StoreService(Cache, Db), StoreID, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;

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
        /// Store List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StoreList")]
        public ResponseModel StoreList()
        {
            List<CustomStoreList> objstoreList = new List<CustomStoreList>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                StoreCaller newStore = new StoreCaller();

                objstoreList = newStore.StoreList(new StoreService(Cache, Db), authenticate.TenantId);
                statusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objstoreList;
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
        /// Search Store
        /// </summary>
        /// <param name="Storename"></param>
        /// <param name="Storecode"></param>
        /// <param name="Pincode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchStore")]
        public ResponseModel searchStore(int StateID, int PinCode, string Area, bool IsCountry)
        {
            List<StoreMaster> objstoreList = new List<StoreMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                StoreCaller newStore = new StoreCaller();

                objstoreList = newStore.SearchStore(new StoreService(Cache, Db), StateID, PinCode, Area, IsCountry);
                statusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objstoreList;
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
        /// attach store
        /// </summary>
        /// <param name="OrderitemID"></param>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("attachstore")]
        public ResponseModel attachstore(string StoreId, int TicketId)
        {
            StoreCaller newStore = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = newStore.AttachStore(new StoreService(Cache, Db), StoreId, TicketId, authenticate.UserMasterID);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
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
        /// Search selected Store by ticket Id
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getSelectedStores")]
        public ResponseModel getSelectedStores(int TicketID)
        {
            List<StoreMaster> storeMasters = new List<StoreMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                StoreCaller newMasterStore = new StoreCaller();

                storeMasters = newMasterStore.getSelectedStores(new StoreService(Cache, Db), TicketID);

                statusCode =
                storeMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = storeMasters;

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
        /// Bulk Upload Store
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadStore")]
        public ResponseModel BulkUploadStore(int StoreFor=1)
        {
            StoreCaller newMasterStore = new StoreCaller();
            string downloadFilePath = string.Empty;
            string bulkUploadFilesPath = string.Empty;
            int count = 0;
            bool errorFileSaved = false;
            bool successFileSaved = false;
            List<string> CSVlist = new List<string>();
            SettingsCaller newFile = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet dataSetCSV = new DataSet();
            string[] filesName = null;


            try
            {
                var files = Request.Form.Files;

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                #region FilePath
                bulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)StoreFor);
                downloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)StoreFor);

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

                dataSetCSV = CommonService.csvToDataSet(bulkUploadFilesPath + "\\StoreBulkUpload.csv");
                CSVlist = newMasterStore.StoreBulkUpload(new StoreService(Cache, Db), authenticate.TenantId, authenticate.UserMasterID, dataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    errorFileSaved = CommonService.SaveFile(downloadFilePath + "\\Store\\ Success" + "\\" + "StoreSuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    successFileSaved = CommonService.SaveFile(downloadFilePath + "\\Store\\Error" + "\\" + "StoreErrorFile.csv", CSVlist[1]);

                count = newFile.CreateFileUploadLog(new FileUploadService(Cache, Db), authenticate.TenantId, "StoreBulkUpload.csv", errorFileSaved,
                                   "StoreErrorFile.csv", "StoreSuccessFile.csv", authenticate.UserMasterID, "Store",
                                   downloadFilePath + "\\Store\\Error" + "\\" + "StoreErrorFile.csv",
                                   downloadFilePath + "\\Store\\ Success" + "\\" + "StoreSuccessFile.csv", 1
                                   );
                #endregion

                statusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

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
        #endregion  

    }
}
