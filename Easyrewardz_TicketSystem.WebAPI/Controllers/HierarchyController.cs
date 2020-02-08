using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  // [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class HierarchyController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        private readonly string rootPath;
        #endregion

        #region Cunstructor
        public HierarchyController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
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
            HierarchyCaller _Hierarchy = new HierarchyCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                customHierarchymodel.TenantID = authenticate.TenantId;
                customHierarchymodel.CreatedBy = authenticate.UserMasterID;
                int result = _Hierarchy.CreateHierarchy(new HierarchyService(_connectionSting), customHierarchymodel);
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

                    StatusCode =
                        result == 0 ? (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

                }
                else
                {
                    StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                }
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
        /// List Hierarchy
        /// </summary>
        /// <returns></returns>
        [Route("ListHierarchy")]
        public ResponseModel ListHierarchy(int HierarchyFor=1)
        {
            List<CustomHierarchymodel> customHierarchymodels = new List<CustomHierarchymodel>();
            HierarchyCaller _Hierarchy = new HierarchyCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                customHierarchymodels = _Hierarchy.ListofHierarchy(new HierarchyService(_connectionSting), authenticate.TenantId, HierarchyFor);
                StatusCode =
                   customHierarchymodels.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = customHierarchymodels;

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
        /// Bullk Upload  Hierarchy
        /// </summary>
        /// <returns></returns>
           [HttpPost]
        [Route("BulkUploadHierarchy")]
        public ResponseModel BulkUploadHierarchy(int HierarchyFor)
        {
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            int count = 0;
            HierarchyCaller _Hierarchy = new HierarchyCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = ""; string fileName = ""; string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet DataSetCSV = new DataSet();
            string[] filesName = null;
            List<string> CSVlist = new List<string>();



            try
            {
                var files = Request.Form.Files;

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                #region FilePath
                BulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor);
                DownloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor);

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

                //DataSetCSV = CommonService.csvToDataSet(BulkUploadFilesPath + "\\" + filesName[0]);

                #endregion

                DataSetCSV = CommonService.csvToDataSet(BulkUploadFilesPath + "\\hierarchymaster.csv");

                CSVlist = _Hierarchy.HierarchyBulkUpload(new HierarchyService(_connectionSting),
                    authenticate.TenantId, authenticate.UserMasterID, HierarchyFor, "hierarchymaster.csv", DataSetCSV);

                //CSVlist = _Hierarchy.HierarchyBulkUpload(new HierarchyService(_connectionSting),
                //  authenticate.TenantId, authenticate.UserMasterID, HierarchyFor, filesName[0], DataSetCSV);

                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Hierarchy\\ Success" + "\\" + "HierarchySuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    successfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Hierarchy\\Error" + "\\" + "HierarchyErrorFile.csv", CSVlist[1]);

                count = fileU.CreateFileUploadLog(new FileUploadService(_connectionSting), authenticate.TenantId, "hierarchymaster.csv", errorfilesaved,
                                   "HierarchyErrorFile.csv", "HierarchySuccessFile.csv", authenticate.UserMasterID, "Hierarchy",
                                   DownloadFilePath + "\\Hierarchy\\Error" + "\\" + "HierarchyErrorFile.csv",
                                   DownloadFilePath + "\\Hierarchy\\ Success" + "\\" + "HierarchySuccessFile.csv", HierarchyFor
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

        #endregion
    }
}