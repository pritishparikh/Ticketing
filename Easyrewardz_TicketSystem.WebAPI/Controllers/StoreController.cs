using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    public class StoreController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionString;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string _UploadedBulkFile;
        #endregion

        #region Cunstructor
        public StoreController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
            _UploadedBulkFile = configuration.GetValue<string>("FileUploadLocation");
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
        [Route("SearchStoreDetail")]
        public ResponseModel SearchStoreDetail(string SearchText)
        {
            List<StoreMaster> objstoreList = new List<StoreMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                StoreCaller newStore = new StoreCaller();

                objstoreList = newStore.getStoreDetailbyNameAndPincode(new StoreService(_connectionString), SearchText, authenticate.TenantId);
                StatusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objstoreList;
            }
            catch (Exception )
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Stores on basis of text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getStores")]
        public ResponseModel GetStores(string searchText)
        {
            List<StoreMaster> storeMasters = new List<StoreMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreCaller newMasterBrand = new StoreCaller();

                storeMasters = newMasterBrand.getStores(new StoreService(_connectionString), searchText, authenticate.TenantId);

                StatusCode =
                storeMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = storeMasters;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Add Store
        /// </summary>
        /// <param name="storeMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createstore")]
        public ResponseModel Createstore([FromBody]StoreMaster storeMaster)
        {
            StoreCaller newStore = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = newStore.AddStore(new StoreService(_connectionString), storeMaster, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Edit Store
        /// </summary>
        /// <param name="storeMaster"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editstore")]
        public ResponseModel Editstore([FromBody]StoreMaster storeMaster, int StoreID)
        {
            StoreCaller newStore = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = newStore.EditStore(new StoreService(_connectionString), storeMaster, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
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
        public ResponseModel DeleteStore(int StoreID)
        {
            StoreCaller newStore = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = newStore.DeleteStore(new StoreService(_connectionString), StoreID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                StoreCaller newStore = new StoreCaller();

                objstoreList = newStore.StoreList(new StoreService(_connectionString), authenticate.TenantId);
                StatusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objstoreList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Search Store
        /// </summary>
        /// <param name="StateID"></param>
        /// <param name="PinCode"></param>
        /// <param name="Area"></param>
        /// <param name="IsCountry"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchStore")]
        public ResponseModel SearchStore(int StateID, int PinCode, string Area, bool IsCountry)
        {
            List<StoreMaster> objstoreList = new List<StoreMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                StoreCaller newStore = new StoreCaller();

                objstoreList = newStore.SearchStore(new StoreService(_connectionString), StateID, PinCode, Area, IsCountry);
                StatusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objstoreList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        #region attach store old approach 
        /// <summary>
        /// attach store
        /// </summary>
        /// <param name="StoreId"></param>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("attachstore")]
        //public ResponseModel Attachstore(string StoreId, int TicketId)
        //{
        //    StoreCaller newStore = new StoreCaller();
        //    ResponseModel objResponseModel = new ResponseModel();
        //    int StatusCode = 0;
        //    string statusMessage = "";
        //    try
        //    {
        //        string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
        //        Authenticate authenticate = new Authenticate();
        //        authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

        //        int result = newStore.AttachStore(new StoreService(_connectionString), StoreId, TicketId, authenticate.UserMasterID);
        //        StatusCode =
        //        result == 0 ?
        //               (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
        //        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
        //        objResponseModel.Status = true;
        //        objResponseModel.StatusCode = StatusCode;
        //        objResponseModel.Message = statusMessage;
        //        objResponseModel.ResponseData = result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return objResponseModel;
        //}

        #endregion

        /// <summary>
        /// attach store
        /// </summary>
        /// <param name="StoreId"></param>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("attachstore")]
        public ResponseModel Attachstore(IFormFile File)
        {
            StoreCaller newStore = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            List<StoreMaster> storeMaster = new List < StoreMaster > ();
            int StatusCode = 0;
            int TicketId = 0; string StoreId = string.Empty;
            int result = 0;
            List<string> ListStoreDetails = new List<string>();
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                var Keys = Request.Form;
                StoreId = Convert.ToString(Keys["StoreId"]);
                TicketId = Convert.ToInt32(Keys["TicketId"]);


                if (!string.IsNullOrEmpty(StoreId) && TicketId > 0)
                {
                    #region check for store is from LPASS 


                    storeMaster = JsonConvert.DeserializeObject<List<StoreMaster>>(Keys["storeDetails"]);

                    if (storeMaster != null)
                    {
                        if (storeMaster.Count > 0)
                        {
                            foreach (var store in storeMaster)
                            {
                                if (store.StoreID.Equals(0))
                                {

                                    int InsertedStoreID = 0;
                                    InsertedStoreID = newStore.AddStore(new StoreService(_connectionString), store, authenticate.TenantId, authenticate.UserMasterID);

                                    if (InsertedStoreID > 0)
                                    {
                                        store.StoreVisitDate = string.IsNullOrEmpty(store.StoreVisitDate) ? "" : store.StoreVisitDate; 
                                        ListStoreDetails.Add(Convert.ToString(InsertedStoreID) + "|" + store.StoreVisitDate + "|" + store.Purpose);
                                    }

                                }
                            }

                            StoreId = ListStoreDetails.Count > 0 ? string.Join(',', ListStoreDetails) : "";

                        }

                    }


                    #endregion

                    result = newStore.AttachStore(new StoreService(_connectionString), StoreId, TicketId, authenticate.UserMasterID);
                }
                   
                StatusCode = result.Equals(0)? (int)EnumMaster.StatusCode.InternalServerError : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Search selected Store by ticket Id
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getSelectedStores")]
        public ResponseModel GetSelectedStores(int TicketID)
        {
            List<StoreMaster> storeMasters = new List<StoreMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreCaller newMasterStore = new StoreCaller();

                storeMasters = newMasterStore.getSelectedStores(new StoreService(_connectionString), TicketID);

                StatusCode =
                storeMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = storeMasters;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Bulk Upload Store
        /// </summary>
        /// <param name="File"></param>
        /// <param name="StoreFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadStore")]
        public ResponseModel BulkUploadStore(int StoreFor = 1)
        {
            StoreCaller newMasterStore = new StoreCaller();

            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            int count = 0;

            List<string> CSVlist = new List<string>();
            SettingsCaller newFile = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            DataSet DataSetCSV = new DataSet();
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            string[] filesName = null;


            try
            {
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


                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                #region FilePath
                BulkUploadFilesPath = appRoot + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)StoreFor);
                DownloadFilePath = appRoot + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)StoreFor);

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

                DataSetCSV = CommonService.csvToDataSet(Folderpath + "\\" + finalAttchment);
                CSVlist = newMasterStore.StoreBulkUpload(new StoreService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, DataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog
                
                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Store\\ Success" + "\\" + "StoreSuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Store\\Error" + "\\" + "StoreErrorFile.csv", CSVlist[1]);

                count = newFile.CreateFileUploadLog(new FileUploadService(_connectionString), authenticate.TenantId, finalAttchment, errorfilesaved,
                                   "StoreErrorFile.csv", "StoreSuccessFile.csv", authenticate.UserMasterID, "Store",
                                   DownloadFilePath + "\\Store\\Error" + "\\" + "StoreErrorFile.csv",
                                   DownloadFilePath + "\\Store\\ Success" + "\\" + "StoreSuccessFile.csv", 1
                                   );
                #endregion

                StatusCode = successfilesaved ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

            }
            catch (Exception )
            {
                throw;
            }
            return objResponseModel;


        }
        #endregion  

    }
}
