using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CategoryController : ControllerBase
    {

        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;

        private readonly string BulkUpload;
        private readonly string UploadFiles;
        private readonly string DownloadFile;
        #endregion

        #region Constructor
        public CategoryController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            BulkUpload = configuration.GetValue<string>("BulkUpload");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            DownloadFile = configuration.GetValue<string>("Downloadfile");
            rootPath = configuration.GetValue<string>("APIURL");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get CategoryList
        /// </summary>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCategoryList")]
        public List<Category> GetCategoryList(int BrandID)
        {
            List<Category> objCategoryList = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();
                objCategoryList = newMasterCategory.GetCategoryList(new CategoryServices(_connectioSting), authenticate.TenantId, BrandID);
               
            }
            catch (Exception)
            {
                throw;
            }
            return objCategoryList;
        }
        /// <summary>
        ///Insert new category
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCategory")]
        public ResponseModel AddCategory(int BrandID,string category)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                result = newMasterCategory.AddCategory(new CategoryServices(_connectioSting), category, authenticate.TenantId, authenticate.UserMasterID,BrandID);
                statusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// DeleteCategory
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("DeleteCategory")]
        public ResponseModel DeleteCategory(int CategoryID)
        {

            MasterCaller newMasterCategory = new MasterCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

               int result = newMasterCategory.DeleteCategory(new CategoryServices(_connectioSting), CategoryID, authenticate.TenantId);
                statusCode =
                               result == 0 ?
                                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// UpdateCategory
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCategory")]
        public ResponseModel UpdateCategory([FromBody]Category category)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();
                category.TenantID = authenticate.TenantId;
                category.ModifyBy = authenticate.UserMasterID;
                result = newMasterCategory.UpdateCategory(new CategoryServices(_connectioSting), category);

                statusCode =
                result == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// CategoryList
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CategoryList")]
        public List<Category> CategoryList()
        {
            List<Category> objcategory = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();

                objcategory = newMasterCategory.CategoryList(new CategoryServices(_connectioSting), authenticate.TenantId);
            }
            catch (Exception)
            {
                throw;
            }
            return objcategory;
        }

        /// <summary>
        /// Create Categorybrand mapping
        /// </summary>
        /// <param name="CustomCreateCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCategorybrandmapping")]
        public ResponseModel CreateCategorybrandmapping([FromBody] CustomCreateCategory customCreateCategory)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                customCreateCategory.CreatedBy = authenticate.UserMasterID;
                result = newMasterCategory.CreateCategoryBrandMapping(new CategoryServices(_connectioSting), customCreateCategory);
               if( customCreateCategory.BrandCategoryMappingID==0)
                {
                    if (result == 0)
                    {
                        statusCode =
                     result == 0 ?
                        (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
                    }
                    else
                    {
                        statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                    }
                }
                else
                {
                    statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                }
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
        /// ListCategorybrandmapping
        /// </summary>
        /// <param name="CustomCreateCategory"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListCategorybrandmapping")]
        public ResponseModel ListCategorybrandmapping()
        {
            List<CustomCreateCategory> customCreateCategories = new List<CustomCreateCategory>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                customCreateCategories = newMasterCategory.ListCategoryBrandMapping(new CategoryServices(_connectioSting));
                statusCode =
               customCreateCategories.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customCreateCategories;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetCategoryListByMultiBrandID
        /// </summary>
        /// <param name="BrandIDs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCategoryListByMultiBrandID")]
        public ResponseModel GetCategoryListByMultiBrandID(string BrandIDs )
        {
            List<Category> objcategory = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();

                objcategory = newMasterCategory.GetCategoryListByMultiBrandID(new CategoryServices(_connectioSting), BrandIDs, authenticate.TenantId);
                statusCode =
               objcategory.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objcategory;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        ///BulkUploadCategory
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadCategory")]
        public ResponseModel BulkUploadCategory( int CategoryFor=1)
        {
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorFileSaved = false;
            bool successFileSaved = false;
            int count = 0;

            MasterCaller masterCaller = new MasterCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            DataSet dataSetCSV = new DataSet();
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            string[] filesName = null;
            List<string> CSVlist = new List<string>();

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

              
               
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                #region FilePath
                string Folderpath = Directory.GetCurrentDirectory();
                 filesName = finalAttchment.Split(",");


                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)CategoryFor));
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)CategoryFor)); 
                

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


                dataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));
                CSVlist = masterCaller.CategoryBulkUpload(new CategoryServices(_connectioSting),
                  authenticate.TenantId, authenticate.UserMasterID, CategoryFor, dataSetCSV);


                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "CategorySuccessFile_" + timeStamp + ".csv";
                string ErrorFileName = "CategoryErrorFile_" + timeStamp + ".csv";

                string SuccessFileUrl = rootPath + "/" + BulkUpload + "/" + DownloadFile + "/Success/" + SuccessFileName;
                string ErrorFileUrl = rootPath + "/" + BulkUpload + "/" + DownloadFile + "/Error/" + ErrorFileName;

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successFileSaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorFileSaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);

                count = fileU.CreateFileUploadLog(new FileUploadService(_connectioSting), authenticate.TenantId, filesName[0], errorFileSaved,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "Category", SuccessFileUrl, ErrorFileUrl, CategoryFor);
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

        /// <summary>
        /// Create a claim category
        /// </summary>
        /// <param name="claimCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateClaimCategory")]
        public ResponseModel CreateClaimCategory([FromBody] ClaimCategory claimCategory)
        {
            MasterCaller newMasterCaller = new MasterCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string  token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                claimCategory.CreatedBy = authenticate.UserMasterID;
                claimCategory.TenantID = authenticate.TenantId;

                int result = newMasterCaller.CreateClaimCategory(new CategoryServices(_connectioSting), claimCategory);

                statusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// Get CategoryList with search test
        /// </summary>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCategoryOnSearch")]
        public List<Category> GetCategoryOnSearch(int brandID, string searchText)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            List<Category> objCategoryList = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();
                objCategoryList = newMasterCategory.GetCategoryOnSearch(new CategoryServices(_connectioSting), authenticate.TenantId, brandID, searchText);

                StatusCode = objCategoryList.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objCategoryList;
            }
            catch (Exception)
            {
                throw;
            }
            return objCategoryList;
        }
        #endregion
    }
}