using System;
using System.Collections.Generic;
using System.Data;
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

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CategoryController : ControllerBase
    {

        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }

        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public CategoryController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get CategoryList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCategoryList")]
        public List<Category> GetCategoryList(int BrandID)
        {
            List<Category> objCategoryList = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                authenticate.TenantId = 1;
                MasterCaller newMasterCategory = new MasterCaller();
                objCategoryList = newMasterCategory.GetCategoryList(new CategoryServices(Cache, Db), authenticate.TenantId, BrandID);

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
        public ResponseModel AddCategory(int BrandID, string category)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                result = newMasterCategory.AddCategory(new CategoryServices(Cache, Db), category, authenticate.TenantId, authenticate.UserMasterID, BrandID);
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
                objResponseModel.Message = ex.Message;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Delete category
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
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = newMasterCategory.DeleteCategory(new CategoryServices(Cache, Db), CategoryID, authenticate.TenantId);
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
                objResponseModel.Message = ex.Message;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="category"></param>
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
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();
                category.TenantID = authenticate.TenantId;
                category.ModifyBy = authenticate.UserMasterID;
                result = newMasterCategory.UpdateCategory(new CategoryServices(Cache, Db), category);

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

                objResponseModel.Status = false;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = ex.Message;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get category list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CategoryList")]
        public List<Category> CategoryList()
        {
            List<Category> objcategory = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCategory = new MasterCaller();

                objcategory = newMasterCategory.CategoryList(new CategoryServices(Cache, Db), authenticate.TenantId);
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = ex.Message;
                objResponseModel.ResponseData = null;
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
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                customCreateCategory.CreatedBy = authenticate.UserMasterID;
                result = newMasterCategory.CreateCategoryBrandMapping(new CategoryServices(Cache, Db), customCreateCategory);
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
                objResponseModel.Message = ex.Message;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Create Categorybrand mapping
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
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                customCreateCategories = newMasterCategory.ListCategoryBrandMapping(new CategoryServices(Cache, Db));
                statusCode =
               customCreateCategories.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customCreateCategories;

            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = ex.Message;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Create Categorybrand mapping
        /// </summary>
        /// <param name="CustomCreateCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCategoryListByMultiBrandID")]
        public ResponseModel GetCategoryListByMultiBrandID(string BrandIDs)
        {
            List<Category> objcategory = new List<Category>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //  authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                authenticate.TenantId = 1;
                MasterCaller newMasterCategory = new MasterCaller();

                objcategory = newMasterCategory.GetCategoryListByMultiBrandID(new CategoryServices(Cache, Db), BrandIDs, authenticate.TenantId);
                statusCode =
               objcategory.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objcategory;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = ex.Message;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Bullk Upload  Hierarchy
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadCategory")]
        public ResponseModel BulkUploadCategory(int CategoryFor = 1)
        {
            string downloadFilePath = string.Empty;
            string bulkUploadFilesPath = string.Empty;
            bool errorFileSaved = false;
            bool successFileSaved = false;
            int Count = 0;

            MasterCaller masterCaller = new MasterCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet dataSetCSV = new DataSet();
            string[] filesName = null;
            List<string> CSVlist = new List<string>();

            try
            {
                //var files = Request.Form.Files;

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                #region FilePath
                //bulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)CategoryFor);
                //downloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)CategoryFor);

                #endregion
                //#region Read from Form

                //if (files.Count > 0)
                //{
                //    for (int i = 0; i < files.Count; i++)
                //    {
                //        fileName += files[i].FileName.Replace(".","_"+ authenticate .UserMasterID+ "_"+ timeStamp + ".") + ",";
                //    }
                //    finalAttchment = fileName.TrimEnd(',');
                //}

                //var exePath = Path.GetDirectoryName(System.Reflection
                //     .Assembly.GetExecutingAssembly().CodeBase);
                //Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                //var appRoot = appPathMatcher.Match(exePath).Value;
                //string Folderpath = appRoot + "\\" + "BulkUpload\\Category";



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
                //            FileStream docFile = new FileStream(Folderpath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                //            msfile.WriteTo(docFile);
                //            docFile.Close();
                //            ms.Close();
                //            msfile.Close();

                //        }
                //    }
                //}

                //DataSetCSV = CommonService.csvToDataSet(Folderpath + "\\" + filesName[0]);

                //#endregion

                dataSetCSV = CommonService.csvToDataSet("D:\\VipinSingh\\CategoryBulk.csv");
                // DataSetCSV = CommonService.csvToDataSet(bulkUploadFilesPath + "\\Categorymaster.csv");

                CSVlist = masterCaller.CategoryBulkUpload(new CategoryServices(Cache, Db),
                  authenticate.TenantId, authenticate.UserMasterID, CategoryFor, "Categorymaster.csv", dataSetCSV);
                // int result = masterCaller.CategoryBulkUpload(new CategoryServices(_connectioSting),authenticate.TenantId, authenticate.UserMasterID, DataSetCSV);
                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    errorFileSaved = CommonService.SaveFile(downloadFilePath + "\\Category\\ Success" + "\\" + "CategorySuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    successFileSaved = CommonService.SaveFile(downloadFilePath + "\\Category\\Error" + "\\" + "CategoryErrorFile.csv", CSVlist[1]);

                Count = fileU.CreateFileUploadLog(new FileUploadService(Cache, Db), authenticate.TenantId, "Categorymaster.csv", errorFileSaved,
                                   "CategoryErrorFile.csv", "CategorySuccessFile.csv", authenticate.UserMasterID, "Category",
                                   downloadFilePath + "\\Category\\Error" + "\\" + "CategoryErrorFile.csv",
                                   downloadFilePath + "\\Category\\ Success" + "\\" + "CategorySuccessFile.csv", CategoryFor
                                   );
                #endregion
                statusCode = Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CSVlist.Count;

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

        #region Create Claim Category
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
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                claimCategory.CreatedBy = authenticate.UserMasterID;
                claimCategory.TenantID = authenticate.TenantId;

                int result = newMasterCaller.CreateClaimCategory(new CategoryServices(Cache, Db), claimCategory);

                statusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        #endregion

        #endregion
    }
}