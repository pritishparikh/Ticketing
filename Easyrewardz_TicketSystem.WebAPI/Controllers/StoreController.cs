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
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public StoreController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
            rootPath =  configuration.GetValue<string>("APIURL");
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                StoreCaller _newStore = new StoreCaller();

                objstoreList = _newStore.getStoreDetailbyNameAndPincode(new StoreService(_Cache, Db), SearchText, authenticate.TenantId);
                StatusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objstoreList;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                StoreCaller _newMasterBrand = new StoreCaller();

                storeMasters = _newMasterBrand.getStores(new StoreService(_Cache, Db), searchText, authenticate.TenantId);

                StatusCode =
                storeMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = storeMasters;

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
        /// Add Store
        /// </summary>
        /// <param name="TaskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createstore")]
        public ResponseModel createstore([FromBody]StoreMaster storeMaster)
        {
            StoreCaller _newStore = new StoreCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int result = _newStore.AddStore(new StoreService(_Cache, Db), storeMaster, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;

            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = false;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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
            StoreCaller _newStore = new StoreCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int result = _newStore.EditStore(new StoreService(_Cache, Db), storeMaster, StoreID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;

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
        ///delete Store
        /// </summary>
        /// <param name="StoreID"></param> soft delete 
        /// <returns></returns>
        [HttpPost]
        [Route("deleteStore")]
        public ResponseModel deleteStore(int StoreID)
        {
            StoreCaller _newStore = new StoreCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int result = _newStore.DeleteStore(new StoreService(_Cache, Db), StoreID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;

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
        /// Store List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StoreList")]
        public ResponseModel StoreList()
        {
            List<CustomStoreList> objstoreList = new List<CustomStoreList>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                StoreCaller _newStore = new StoreCaller();

                objstoreList = _newStore.StoreList(new StoreService(_Cache, Db), authenticate.TenantId);
                StatusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objstoreList;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                StoreCaller _newStore = new StoreCaller();

                objstoreList = _newStore.SearchStore(new StoreService(_Cache, Db), StateID, PinCode, Area, IsCountry);
                StatusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objstoreList;
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
        /// attach store
        /// </summary>
        /// <param name="OrderitemID"></param>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("attachstore")]
        public ResponseModel attachstore(string StoreId, int TicketId)
        {
            StoreCaller _newStore = new StoreCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                int result = _newStore.AttachStore(new StoreService(_Cache, Db), StoreId, TicketId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
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
        /// Search selected Store by ticket Id
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getSelectedStores")]
        public ResponseModel getSelectedStores(int TicketID)
        {
            List<StoreMaster> storeMasters = new List<StoreMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                StoreCaller _newMasterStore = new StoreCaller();

                storeMasters = _newMasterStore.getSelectedStores(new StoreService(_Cache, Db), TicketID);

                StatusCode =
                storeMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = storeMasters;

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
        /// Bulk Upload Store
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadStore")]
        public ResponseModel BulkUploadStore(int StoreFor=1)
        {
            StoreCaller _newMasterStore = new StoreCaller();
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            int count = 0;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            List<string> CSVlist = new List<string>();
            SettingsCaller _newFile = new SettingsCaller();
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
                BulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)StoreFor);
                DownloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)StoreFor);

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

                DataSetCSV = CommonService.csvToDataSet(BulkUploadFilesPath + "\\StoreBulkUpload.csv");
                CSVlist = _newMasterStore.StoreBulkUpload(new StoreService(_Cache, Db), authenticate.TenantId, authenticate.UserMasterID, DataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Store\\ Success" + "\\" + "StoreSuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    successfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Store\\Error" + "\\" + "StoreErrorFile.csv", CSVlist[1]);

                count = _newFile.CreateFileUploadLog(new FileUploadService(_Cache, Db), authenticate.TenantId, "StoreBulkUpload.csv", errorfilesaved,
                                   "StoreErrorFile.csv", "StoreSuccessFile.csv", authenticate.UserMasterID, "Store",
                                   DownloadFilePath + "\\Store\\Error" + "\\" + "StoreErrorFile.csv",
                                   DownloadFilePath + "\\Store\\ Success" + "\\" + "StoreSuccessFile.csv", 1
                                   );
                #endregion

                StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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
        #endregion  

    }
}
