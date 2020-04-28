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
    public class CategoryController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string _UploadedBulkFile;
        #endregion

        #region Constructor
        public CategoryController(IConfiguration _iConfig)
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
        /// Claim Category List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClaimCategoryList")]
        public List<CustomCreateCategory> GetClaimCategoryList()
        {
            List<CustomCreateCategory> objcategory = new List<CustomCreateCategory>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();

                objcategory = newMasterCategory.ClaimCategoryList(new CategoryServices(_connectioSting), authenticate.TenantId);


                StatusCode =
               objcategory.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objcategory;
            }
            catch (Exception)
            {
                throw;
            }
            return objcategory;
        }

        /// <summary>
        /// Get Claim Category List
        /// </summary>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClaimCategoryListByBrandID")]
        public List<Category> GetClaimCategoryListByBrandID(int BrandID)
        {
            List<Category> objCategoryList = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                if (BrandID == 0)
                {
                    return objCategoryList;
                }

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();
                objCategoryList = newMasterCategory.GetClaimCategoryList(new CategoryServices(_connectioSting), authenticate.TenantId, BrandID);

                StatusCode =
              objCategoryList.Count == 0 ?
                   (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCategoryList;
            }
            catch (Exception)
            {
                throw;
            }
            return objCategoryList;
        }

        /// <summary>
        /// Get ClaimCategory By Search
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClaimCategoryBySearch")]
        public ResponseModel GetClaimCategoryBySearch(string CategoryName)
        {
            List<Category> objCategoryList = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                if (CategoryName.Length < 3)
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = StatusCode;
                    objResponseModel.Message = "Record Not Found";
                    objResponseModel.ResponseData = objCategoryList;
                    return objResponseModel;
                }

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();
                objCategoryList = newMasterCategory.GetClaimCategoryBySearch(new CategoryServices(_connectioSting), authenticate.TenantId, CategoryName);

                StatusCode =
              objCategoryList.Count == 0 ?
                   (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCategoryList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        ///Add Claim Category
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddClaimCategory")]
        public ResponseModel AddClaimCategory(int BrandID, string CategoryName)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {

                if (BrandID == 0 || String.IsNullOrEmpty(CategoryName))
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode.ButNoBody));
                    objResponseModel.ResponseData = (int)EnumMaster.StatusCode.ButNoBody;
                }

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                result = newMasterCategory.AddClaimCategory(new CategoryServices(_connectioSting), CategoryName, BrandID, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = result == 0 ? false : true;
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
        /// Get Claim SubCategory By CategoryID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClaimSubCategoryByCategoryID")]
        public ResponseModel GetClaimSubCategoryByCategoryID(int CategoryID, int TypeId = 0)
        {
            List<SubCategory> objSubCategory = new List<SubCategory>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller newMasterSubCat = new MasterCaller();

                objSubCategory = newMasterSubCat.GetClaimSubCategoryByCategoryID(new CategoryServices(_connectioSting), CategoryID, TypeId);

                StatusCode =
                objSubCategory.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSubCategory;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Add Claim Sub Category
        /// </summary>
        /// <param name="categoryID"></param>
        ///  <param name="SubcategoryName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddClaimSubCategory")]
        public ResponseModel AddClaimSubCategory(int CategoryID, string SubcategoryName)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                result = newMasterCategory.AddClaimSubCategory(new CategoryServices(_connectioSting), CategoryID, SubcategoryName, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = result == 0 ? false : true;
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
        /// Get Claim IssueType List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClaimIssueTypeList")]
        public ResponseModel GetClaimIssueTypeList(int SubCategoryID)
        {
            List<IssueType> objIssueTypeList = new List<IssueType>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                MasterCaller newMasterBrand = new MasterCaller();

                objIssueTypeList = newMasterBrand.GetClaimIssueTypeList(new CategoryServices(_connectioSting), authenticate.TenantId, SubCategoryID);

                StatusCode =
                objIssueTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objIssueTypeList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Add Claim Issue Type
        /// </summary>
        /// <param name="SubcategoryID"></param>
        /// <param name="IssuetypeName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddClaimIssueType")]
        public ResponseModel AddClaimIssueType(int SubcategoryID, string IssuetypeName)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                result = newMasterCategory.AddClaimIssueType(new CategoryServices(_connectioSting), SubcategoryID, IssuetypeName, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = result == 0 ? false : true;
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
        /// Create Categorybrand mapping
        /// </summary>
        /// <param name="CustomCreateCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateClaimCategorybrandmapping")]
        public ResponseModel CreateClaimCategorybrandmapping([FromBody] CustomCreateCategory customCreateCategory)
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
                result = newMasterCategory.CreateClaimCategorybrandmapping(new CategoryServices(_connectioSting), customCreateCategory);
                if (customCreateCategory.BrandCategoryMappingID == 0)
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
        /// DeleteCategory
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteClaimCategory")]
        public ResponseModel DeleteClaimCategory(int CategoryID)
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

                int result = newMasterCategory.DeleteClaimCategory(new CategoryServices(_connectioSting), CategoryID, authenticate.TenantId);
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
        ///Bulk Upload Claim Category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadClaimCategory")]
        public ResponseModel BulkUploadClaimCategory()
        {
            string downloadFilePath = string.Empty;
            string bulkUploadFilesPath = string.Empty;
            bool errorFileSaved = false;
            bool successFileSaved = false;
            int count = 0;

            MasterCaller masterCaller = new MasterCaller();
            StoreFileUploadCaller fileU = new StoreFileUploadCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            DataSet dataSetCSV = new DataSet();
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");

            List<string> CSVlist = new List<string>();

            int CategoryFor = 3;

            try
            {
                var files = Request.Form.Files;

                var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string folderpath = appRoot;


                #region FilePath
                bulkUploadFilesPath = folderpath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)CategoryFor);
                downloadFilePath = folderpath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)CategoryFor);

                #endregion

                if (files.Count > 0)
                {
                    bulkUploadFilesPath = bulkUploadFilesPath + "\\Category\\";

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
                CSVlist = masterCaller.ClaimCategoryBulkUpload(new CategoryServices(_connectioSting),authenticate.TenantId, authenticate.UserMasterID, CategoryFor, dataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successFileSaved = CommonService.SaveFile(downloadFilePath + "\\Category\\Success" + "\\" + "CategorySuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorFileSaved = CommonService.SaveFile(downloadFilePath + "\\Category\\Error" + "\\" + "CategoryErrorFile.csv", CSVlist[1]);



                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectioSting), authenticate.TenantId, finalAttchment, errorFileSaved,
                                   "CategoryErrorFile.csv", "CategorySuccessFile.csv", authenticate.UserMasterID, "Category",
                                   downloadFilePath + "\\Category\\Error" + "\\" + "CategoryErrorFile.csv",
                                   downloadFilePath + "\\Category\\Success" + "\\" + "CategorySuccessFile.csv", CategoryFor
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