using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Extensions.Caching.Distributed;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.AspNetCore.Authorization;
using Easyrewardz_TicketSystem.WebAPI.Filters;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class HierarchyController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public HierarchyController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
            rootPath = Configuration.GetValue<string>("APIURL");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// CreateHierarchy
        /// </summary>
        /// <param name="TaskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateHierarchy")]
        public ResponseModel CreateHierarchy([FromBody] CustomHierarchymodel customHierarchymodel)
        {
            HierarchyCaller hierarchy = new HierarchyCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                customHierarchymodel.TenantID = authenticate.TenantId;
                customHierarchymodel.CreatedBy = authenticate.UserMasterID;
                int result = hierarchy.CreateHierarchy(new HierarchyService(Cache, Db), customHierarchymodel);
                if (customHierarchymodel.Deleteflag == 1)
                {
                    if (result == 0)
                    {
                        statusMessage = "Record in use";
                    }
                    else
                    {
                        statusMessage = "Record deleted successfully ";
                    }

                    statusCode =
                        result == 0 ? (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

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
        /// List Hierarchy
        /// </summary>
        /// <returns></returns>
        [Route("ListHierarchy")]
        public ResponseModel ListHierarchy(int HierarchyFor = 1)
        {
            List<CustomHierarchymodel> customHierarchymodels = new List<CustomHierarchymodel>();
            HierarchyCaller hierarchy = new HierarchyCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                customHierarchymodels = hierarchy.ListofHierarchy(new HierarchyService(Cache, Db), authenticate.TenantId, HierarchyFor);
                statusCode =
                   customHierarchymodels.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customHierarchymodels;

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
        /// Bullk Upload  Hierarchy
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadHierarchy")]
        public ResponseModel BulkUploadHierarchy(int HierarchyFor)
        {
            string downloadFilePath = string.Empty;
            string bulkUploadFilesPath = string.Empty;
            bool errorFileSaved = false;
            bool successFileSaved = false;
            int count = 0;
            HierarchyCaller hierarchy = new HierarchyCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = ""; string fileName = ""; string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet dataSetCSV = new DataSet();
            string[] filesName = null;
            List<string> CSVlist = new List<string>();



            try
            {
                var files = Request.Form.Files;

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                #region FilePath
                bulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor);
                downloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor);

                #endregion

                #region Read from Form

                // if (files.Count > 0)
                // {
                //     for (int i = 0; i < files.Count; i++)
                //     {
                //         fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                //     }
                //     finalAttchment = fileName.TrimEnd(',');
                // }

                // //var exePath = Path.GetDirectoryName(System.Reflection
                // //     .Assembly.GetExecutingAssembly().CodeBase);
                // //Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                // //var appRoot = appPathMatcher.Match(exePath).Value;
                // //string Folderpath = appRoot + "\\" + "UploadFiles"+"\\"+ CommonFunction.GetEnumDescription((EnumMaster.FIleUpload)HierarchyFor);



                // if (files.Count > 0)
                // {
                //    filesName = finalAttchment.Split(",");
                //     for (int i = 0; i < files.Count; i++)
                //     {
                //         using (var ms = new MemoryStream())
                //         {
                //             files[i].CopyTo(ms);
                //             var fileBytes = ms.ToArray();
                //             MemoryStream msfile = new MemoryStream(fileBytes);
                //             FileStream docFile = new FileStream(BulkUploadFilesPath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                //             msfile.WriteTo(docFile);
                //             docFile.Close();
                //             ms.Close();
                //             msfile.Close();

                //         }
                //     }
                // }

                //dataSetCSV = CommonService.csvToDataSet(BulkUploadFilesPath + "\\" + filesName[0]);

                #endregion

                dataSetCSV = CommonService.csvToDataSet(bulkUploadFilesPath + "\\hierarchymaster.csv");

                CSVlist = hierarchy.HierarchyBulkUpload(new HierarchyService(Cache, Db),
                    authenticate.TenantId, authenticate.UserMasterID, HierarchyFor, dataSetCSV);

                //CSVlist = hierarchy.HierarchyBulkUpload(new HierarchyService(_connectionSting),
                //  authenticate.TenantId, authenticate.UserMasterID, HierarchyFor, filesName[0], dataSetCSV);

                #region Create Error and Success files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    errorFileSaved = CommonService.SaveFile(downloadFilePath + "\\Hierarchy\\ Success" + "\\" + "HierarchySuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    successFileSaved = CommonService.SaveFile(downloadFilePath + "\\Hierarchy\\Error" + "\\" + "HierarchyErrorFile.csv", CSVlist[1]);

                count = fileU.CreateFileUploadLog(new FileUploadService(Cache, Db), authenticate.TenantId, "hierarchymaster.csv", errorFileSaved,
                                   "HierarchyErrorFile.csv", "HierarchySuccessFile.csv", authenticate.UserMasterID, "Hierarchy",
                                   downloadFilePath + "\\Hierarchy\\Error" + "\\" + "HierarchyErrorFile.csv",
                                   downloadFilePath + "\\Hierarchy\\ Success" + "\\" + "HierarchySuccessFile.csv", HierarchyFor
                                   );
                #endregion

                statusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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

        #endregion
    }
}