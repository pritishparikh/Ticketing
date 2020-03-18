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
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }

        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public CategoryController(IConfiguration _iConfig,TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
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
        [AllowAnonymous]
        public List<Category> GetCategoryList(int BrandID)
        {
            List<Category> objCategoryList = new List<Category>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;           
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                authenticate.TenantId = 1;
                MasterCaller _newMasterCategory = new MasterCaller();
                objCategoryList = _newMasterCategory.GetCategoryList(new CategoryServices(_Cache, Db), authenticate.TenantId, BrandID);
               
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

            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                MasterCaller _newMasterCategory = new MasterCaller();
                result = _newMasterCategory.AddCategory(new CategoryServices(_Cache, Db), category, authenticate.TenantId, authenticate.UserMasterID, BrandID);
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
                _objResponseModel.Message = ex.Message;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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

            MasterCaller _newMasterCategory = new MasterCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                int result = _newMasterCategory.DeleteCategory(new CategoryServices(_Cache, Db), CategoryID, authenticate.TenantId);
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
                _objResponseModel.Message = ex.Message;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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

            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterCategory = new MasterCaller();
                category.TenantID = authenticate.TenantId;
                category.ModifyBy = authenticate.UserMasterID;
                result = _newMasterCategory.UpdateCategory(new CategoryServices(_Cache, Db), category);

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

                _objResponseModel.Status = false;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = ex.Message;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterCategory = new MasterCaller();

                objcategory = _newMasterCategory.CategoryList(new CategoryServices(_Cache, Db), authenticate.TenantId);
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = ex.Message;
                _objResponseModel.ResponseData = null;
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

            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                MasterCaller _newMasterCategory = new MasterCaller();
                customCreateCategory.CreatedBy = authenticate.UserMasterID;
                result = _newMasterCategory.CreateCategoryBrandMapping(new CategoryServices(_Cache, Db), customCreateCategory);
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
                _objResponseModel.Message = ex.Message;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                MasterCaller _newMasterCategory = new MasterCaller();
                customCreateCategories = _newMasterCategory.ListCategoryBrandMapping(new CategoryServices(_Cache, Db));
                StatusCode =
               customCreateCategories.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = customCreateCategories;

            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = ex.Message;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //  authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                authenticate.TenantId = 1;
                MasterCaller _newMasterCategory = new MasterCaller();

                objcategory = _newMasterCategory.GetCategoryListByMultiBrandID(new CategoryServices(_Cache, Db), BrandIDs, authenticate.TenantId);
                StatusCode =
               objcategory.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objcategory;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = ex.Message;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }

        /// <summary>
        /// Bullk Upload  Hierarchy
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadCategory")]
        public ResponseModel BulkUploadCategory(int CategoryFor = 1)
        {
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            int count = 0;

            MasterCaller masterCaller = new MasterCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet DataSetCSV = new DataSet();
            string[] filesName = null;
            List<string> CSVlist = new List<string>();

            try
            {
                //var files = Request.Form.Files;

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                #region FilePath
                //BulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)CategoryFor);
                //DownloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)CategoryFor);

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

                DataSetCSV = CommonService.csvToDataSet("D:\\VipinSingh\\CategoryBulk.csv");
                // DataSetCSV = CommonService.csvToDataSet(BulkUploadFilesPath + "\\Categorymaster.csv");

                CSVlist = masterCaller.CategoryBulkUpload(new CategoryServices(_Cache, Db),
                  authenticate.TenantId, authenticate.UserMasterID, CategoryFor, "Categorymaster.csv", DataSetCSV);
                // int result = masterCaller.CategoryBulkUpload(new CategoryServices(_connectioSting),authenticate.TenantId, authenticate.UserMasterID, DataSetCSV);
                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Category\\ Success" + "\\" + "CategorySuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    successfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Category\\Error" + "\\" + "CategoryErrorFile.csv", CSVlist[1]);

                count = fileU.CreateFileUploadLog(new FileUploadService(_Cache, Db), authenticate.TenantId, "Categorymaster.csv", errorfilesaved,
                                   "CategoryErrorFile.csv", "CategorySuccessFile.csv", authenticate.UserMasterID, "Category",
                                   DownloadFilePath + "\\Category\\Error" + "\\" + "CategoryErrorFile.csv",
                                   DownloadFilePath + "\\Category\\ Success" + "\\" + "CategorySuccessFile.csv", CategoryFor
                                   );
                #endregion
                StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = CSVlist.Count;

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
            MasterCaller _newMasterCaller = new MasterCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                claimCategory.CreatedBy = authenticate.UserMasterID;
                claimCategory.TenantID = authenticate.TenantId;

                int result = _newMasterCaller.CreateClaimCategory(new CategoryServices(_Cache, Db), claimCategory);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        #endregion

        #endregion
    }
}