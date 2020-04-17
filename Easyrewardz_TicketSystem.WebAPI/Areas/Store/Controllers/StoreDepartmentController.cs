using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
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
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreDepartmentController : ControllerBase
    {


        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string _UploadedBulkFile;
        #endregion

        #region Constructor
        public StoreDepartmentController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _UploadedBulkFile = configuration.GetValue<string>("FileUploadLocation");
            rootPath = configuration.GetValue<string>("APIURL");
        }
        #endregion


        #region Custom Methods

        /// <summary>
        /// Get Store Details By BrandID
        /// </summary>
        /// <param name="BrandIDs"></param>
        ///  /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreCodeByBrandID")]
        public ResponseModel GetStoreCodeByBrandID(string BrandIDs)
        {
            List<StoreCodeModel> objStoreCodeModel = new List<StoreCodeModel>();
            ResponseModel objResponseModel = new ResponseModel();
            StoreDepartmentCaller newStore = new StoreDepartmentCaller();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));



                objStoreCodeModel = newStore.getStoreByBrandID(new StoreDepartmentService(_connectioSting), BrandIDs, authenticate.TenantId);
                statusCode =
               objStoreCodeModel.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreCodeModel;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Delete department Brand Mapping 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="DepartmentBrandMappingID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteBrandDepartmentMapping")]
        public ResponseModel DeleteBrandDepartmentMapping(int DepartmentBrandMappingID)
        {

            ResponseModel objResponseModel = new ResponseModel();
            StoreDepartmentCaller newDept = new StoreDepartmentCaller();
            int statusCode = 0;
            string statusMessage = "";
            int DeleteCount = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));



                DeleteCount = newDept.DeleteDepartmentMapping(new StoreDepartmentService(_connectioSting), authenticate.TenantId, DepartmentBrandMappingID);
                statusCode =
                DeleteCount == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = DeleteCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Update department Brand Mapping 
        /// </summary>
        /// <param name="DepartmentBrandID"></param>
        /// <param name="BrandID"></param>
        /// <param name="StoreID"></param>
        /// <param name="DepartmentID"></param>
        /// <param name="FunctionID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateBrandDepartmentMapping")]
        public ResponseModel UpdateBrandDepartmentMapping([FromBody] CreateStoreDepartmentModel updateDepartmentModel)
        {

            ResponseModel objResponseModel = new ResponseModel();
            StoreDepartmentCaller newDept = new StoreDepartmentCaller();
            int statusCode = 0;
            string statusMessage = "";
            int UpdateCount = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                updateDepartmentModel.TenantID = authenticate.TenantId;
                updateDepartmentModel.CreatedBy = authenticate.UserMasterID;
                UpdateCount = newDept.UpdateDepartmentMapping(new StoreDepartmentService(_connectioSting), updateDepartmentModel);
                statusCode =
                UpdateCount == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Add Department  
        /// </summary>
        /// <param name="DepartmentName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStoreDepartment")]
        public ResponseModel AddStoreDepartment(string DepartmentName)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            int result = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreDepartmentCaller newMasterBrand = new StoreDepartmentCaller();

                result = newMasterBrand.AddDepartment(new StoreDepartmentService(_connectioSting), DepartmentName, authenticate.TenantId, authenticate.UserMasterID);

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
        /// Add Function   
        /// </summary>
        /// <param name="DepartmentID"></param>
        ///  <param name="FunctionName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStoreFunction")]
        public ResponseModel AddStoreFunction(int DepartmentID, string FunctionName)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            int result = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreDepartmentCaller newMasterBrand = new StoreDepartmentCaller();

                result = newMasterBrand.AddFunction(new StoreDepartmentService(_connectioSting), DepartmentID, FunctionName, authenticate.TenantId, authenticate.UserMasterID);

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
        /// Get Department List 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDepartmentList")]
        public ResponseModel getDepartmentList()
        {

            List<StoreDepartmentModel> objDepartmentList = new List<StoreDepartmentModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreDepartmentCaller newMasterBrand = new StoreDepartmentCaller();

                objDepartmentList = newMasterBrand.GetDepartmentListDetails(new StoreDepartmentService(_connectioSting), authenticate.TenantId);

                StatusCode =
                objDepartmentList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objDepartmentList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Function Name By Department ID
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getFunctionNameByDepartmentId")]
        public ResponseModel getFunctionNameByDepartmentId(int DepartmentId)
        {
            List<StoreFunctionModel> objFunctionList = new List<StoreFunctionModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreDepartmentCaller newMasterChannel = new StoreDepartmentCaller();
                objFunctionList = newMasterChannel.GetStoreFunctionbyDepartment(new StoreDepartmentService(_connectioSting), DepartmentId, authenticate.TenantId);
                StatusCode =
                objFunctionList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objFunctionList;
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get Function Name By Department ID
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getFunctionNameByMultipleDepartmentIds")]
        public ResponseModel getFunctionNameByMultipleDepartmentIds(string DepartmentIds)
        {
            List<StoreFunctionModel> objFunctionList = new List<StoreFunctionModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreDepartmentCaller newMasterChannel = new StoreDepartmentCaller();
                objFunctionList = newMasterChannel.GetStoreFunctionbyMultiDepartment(new StoreDepartmentService(_connectioSting), DepartmentIds, authenticate.TenantId);
                StatusCode =
                objFunctionList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objFunctionList;
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        [HttpPost]
        [Route("CreateDepartment")]
        public ResponseModel CreateDepartment([FromBody] CreateStoreDepartmentModel createDepartmentModel)
        {
            StoreDepartmentCaller newCreatDept = new StoreDepartmentCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
           
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                createDepartmentModel.CreatedBy = authenticate.UserMasterID;
                createDepartmentModel.TenantID = authenticate.TenantId;

                int result = newCreatDept.CreateStoreDepartment(new StoreDepartmentService(_connectioSting), createDepartmentModel);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// Get DeparmentBrandMapping List 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDeparmentBrandMappingList")]
        public ResponseModel GetDeparmentBrandMappingList()
        {

            List<DepartmentListingModel> objDepartmentList = new List<DepartmentListingModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreDepartmentCaller newMasterBrand = new StoreDepartmentCaller();

                objDepartmentList = newMasterBrand.GetBrandDepartmenMappingtList(new StoreDepartmentService(_connectioSting), authenticate.TenantId);

                StatusCode =
                objDepartmentList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objDepartmentList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("BulkUploadDepartment")]
        public ResponseModel BulkUploadDepartment()
        {
            string downloadFilePath = string.Empty;
            string bulkUploadFilesPath = string.Empty;
            bool errorFileSaved = false;
            bool successFileSaved = false;
            int count = 0;

            StoreDepartmentCaller storeDepartment = new StoreDepartmentCaller();
            StoreFileUploadCaller fileU = new StoreFileUploadCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            DataSet dataSetCSV = new DataSet();
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            List<string> CSVlist = new List<string>();

            int ItemFor = 3;

            try
            {

                var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                string appRoot = appPathMatcher.Match(exePath).Value;
                string folderpath = appRoot;

                var files = Request.Form.Files;

                #region FilePath
                bulkUploadFilesPath = folderpath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)ItemFor);
                downloadFilePath = folderpath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)ItemFor);

                #endregion


                if (files.Count > 0)
                {
                    bulkUploadFilesPath = bulkUploadFilesPath + "\\Department\\";

                    if (!Directory.Exists(bulkUploadFilesPath))
                    {
                        Directory.CreateDirectory(bulkUploadFilesPath);
                    }

                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName = files[i].FileName.Replace(".", timeStamp + ".") + ",";
                        fileName = fileName.TrimEnd(',');

                        finalAttchment = fileName;

                        using (var ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(bulkUploadFilesPath + "\\" + fileName, FileMode.Create, FileAccess.Write);
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
                else
                {
                    statusCode = (int)EnumMaster.StatusCode.ButNoBody;
                    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode.ButNoBody));
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = statusMessage;
                    objResponseModel.ResponseData = 0;

                    return objResponseModel;
                }

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));



                dataSetCSV = CommonService.csvToDataSet(bulkUploadFilesPath + "\\" + finalAttchment);
                CSVlist = storeDepartment.DepartmentBulkUpload(new StoreDepartmentService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, ItemFor, dataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successFileSaved = CommonService.SaveFile(downloadFilePath + "\\Department\\Success" + "\\" + "DepartmentSuccessFile.csv", CSVlist[1]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorFileSaved = CommonService.SaveFile(downloadFilePath + "\\Department\\Error" + "\\" + "DepartmentErrorFile.csv", CSVlist[0]);



                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectioSting), authenticate.TenantId, finalAttchment, errorFileSaved,
                                   "CategoryErrorFile.csv", "CategorySuccessFile.csv", authenticate.UserMasterID, "Item",
                                   downloadFilePath + "\\Department\\Error" + "\\" + "DepartmentErrorFile.csv",
                                   downloadFilePath + "\\Department\\Success" + "\\" + "DepartmentSuccessFile.csv", ItemFor
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
