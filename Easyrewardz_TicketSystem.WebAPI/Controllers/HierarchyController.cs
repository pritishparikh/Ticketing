﻿using Easyrewardz_TicketSystem.CustomModel;
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
    public class HierarchyController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        private readonly string rootPath;
        private readonly string BulkUpload;
        private readonly string UploadFiles;
        private readonly string DownloadFile;

        #endregion

        #region Cunstructor
        public HierarchyController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
            BulkUpload = configuration.GetValue<string>("BulkUpload");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            DownloadFile = configuration.GetValue<string>("Downloadfile");

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
            HierarchyCaller Hierarchy = new HierarchyCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                customHierarchymodel.TenantID = authenticate.TenantId;
                customHierarchymodel.CreatedBy = authenticate.UserMasterID;
                int result = Hierarchy.CreateHierarchy(new HierarchyService(_connectionSting), customHierarchymodel);
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
                if (customHierarchymodel.DesignationID == 0)
                {
                    if (result == 0)
                    {
                        StatusCode =
                     result == 0 ?
                        (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
                    }
                    else
                    {
                        StatusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                    }
                }              
                else
                {
                    StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                }
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
        /// List Hierarchy
        /// </summary>
        /// <returns></returns>
        [Route("ListHierarchy")]
        public ResponseModel ListHierarchy(int HierarchyFor=1)
        {
            List<CustomHierarchymodel> customHierarchymodels = new List<CustomHierarchymodel>();
            HierarchyCaller Hierarchy = new HierarchyCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                customHierarchymodels = Hierarchy.ListofHierarchy(new HierarchyService(_connectionSting), authenticate.TenantId, HierarchyFor);
                StatusCode =
                   customHierarchymodels.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
               objResponseModel.StatusCode = StatusCode;
               objResponseModel.Message = statusMessage;
               objResponseModel.ResponseData = customHierarchymodels;

            }
            catch (Exception )
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Bullk Upload  Hierarchy
        /// </summary>
        /// <returns></returns>
           [HttpPost]
        [Route("BulkUploadHierarchy")]
        public ResponseModel BulkUploadHierarchy(int HierarchyFor=1)
        {
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            int count = 0;
            HierarchyCaller _Hierarchy = new HierarchyCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
           string statusMessage = "";
            DataSet DataSetCSV = new DataSet();
            List<string> CSVlist = new List<string>();
            string fileName = "";
            string finalAttchment = "";


            try
            {
                var files = Request.Form.Files;
                string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }
                var Keys = Request.Form;

                //var exePath = Path.GetDirectoryName(System.Reflection
                //     .Assembly.GetExecutingAssembly().CodeBase);
                //Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                //var appRoot = appPathMatcher.Match(exePath).Value;
                //string Folderpath = appRoot + "\\" + _UploadedBulkFile;



                #region FilePath
                string Folderpath = Directory.GetCurrentDirectory();
                string[] filesName = finalAttchment.Split(",");


                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor));// Folderpath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor);
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor)); ;// Folderpath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor);


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

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                       

                DataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));

                CSVlist = _Hierarchy.HierarchyBulkUpload(new HierarchyService(_connectionSting),
                    authenticate.TenantId, authenticate.UserMasterID, HierarchyFor, DataSetCSV);

                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "HierarchySuccessFile_" + timeStamp + ".csv";
                string ErrorFileName = "HierarchyErrorFile_" + timeStamp + ".csv";

                string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                   rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)HierarchyFor) + "/Error/" + ErrorFileName : string.Empty;


                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath,"Success", SuccessFileName), CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);

                count = fileU.CreateFileUploadLog(new FileUploadService(_connectionSting), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "Hierarchy", SuccessFileUrl, ErrorFileUrl, HierarchyFor);
                #endregion

                StatusCode = count>0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CSVlist.Count;

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