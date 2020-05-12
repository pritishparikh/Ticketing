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
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreSLAController : ControllerBase
    {

        #region Variable Declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;

        private readonly string rootPath;
        private readonly string BulkUpload;
        private readonly string UploadFiles;
        private readonly string DownloadFile;   
        #endregion

        #region Constructor


        public StoreSLAController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
            BulkUpload = configuration.GetValue<string>("BulkUpload");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            DownloadFile = configuration.GetValue<string>("Downloadfile");
        }


        #endregion

        #region Custom Methods 


        /// <summary>
        ///Bind Issuetype for SLA Creation DropDown
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BindFunctions")]
        public ResponseModel BindFunctions(string SearchText)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<FunctionList> _objresponseModel = new List<FunctionList>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newCRM = new StoreSLACaller();

                _objresponseModel = _newCRM.BindFunctionList(new StoreSLAService(_connectioSting), authenticate.TenantId, SearchText);
                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Create SLA
        /// </summary>
        /// <param name="insertSLA"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateStoreSLA")]
        public ResponseModel CreateStoreSLA([FromBody]StoreSLAModel insertSLA)
        {
            int insertcount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newSLA = new StoreSLACaller();

                insertSLA.TenantID = authenticate.TenantId;
                insertSLA.CreatedBy = authenticate.UserMasterID;
                insertcount = _newSLA.InsertStoreSLA(new StoreSLAService(_connectioSting), insertSLA);

                StatusCode =
                insertcount == 0 ?
                     (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = insertcount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Create SLA
        /// </summary>
        /// <param name="insertSLA"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateStoreSLA")]
        public ResponseModel UpdateStoreSLA([FromBody]StoreSLAModel insertSLA)
        {
            int updatecount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newSLA = new StoreSLACaller();

                insertSLA.TenantID = authenticate.TenantId;
                insertSLA.CreatedBy = authenticate.UserMasterID;
                updatecount = _newSLA.UpdateStoreSLA(new StoreSLAService(_connectioSting), insertSLA);

                StatusCode =
                updatecount == 0 ?
                     (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = updatecount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        /// Delete SLA
        /// </summary>
        /// <param name="SLAID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteStoreSLA")]
        public ResponseModel DeleteSLA(int SLAID)
        {
            int Deletecount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newSLA = new StoreSLACaller();

                Deletecount = _newSLA.DeleteStoreSLA(new StoreSLAService(_connectioSting), authenticate.TenantId, SLAID);

                StatusCode =
                Deletecount == 0 ?
                     (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Deletecount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        /// Get SLA
        /// </summary>
        /// <param name="SLAFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreSLA")]
        public ResponseModel GetStoreSLA()
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<StoreSLAResponseModel> _objresponseModel = new List<StoreSLAResponseModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newCRM = new StoreSLACaller();
               
                _objresponseModel = _newCRM.StoreSLAList(new StoreSLAService(_connectioSting), authenticate.TenantId);
                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Get Store SLA Detail
        /// </summary>
        /// <param name="SLAId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreSLADetail")]
        public ResponseModel GetSLADetail(int SLAId)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            StoreSLAResponseModel _objresponseModel = new StoreSLAResponseModel();
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                StoreSLACaller _newSLA = new StoreSLACaller();
                _objresponseModel = _newSLA.GetStoreSLADetail(new StoreSLAService(_connectioSting), authenticate.TenantId, SLAId);
                StatusCode = (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;
            }
            catch (Exception)
            {
                throw;
            }
            return _objResponseModel;
        }


        /// <summary>
        /// Bullk Upload Store SLA
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadStoreSLA")]
        public ResponseModel BulkUploadStoreSLA(int SLAFor = 3)
        {


            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            int count = 0;
            List<string> CSVlist = new List<string>();
            StoreSLACaller newSLA = new StoreSLACaller();
            StoreFileUploadCaller fileU = new StoreFileUploadCaller();
            ResponseModel objResponseModel = new ResponseModel();
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));



                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", "_" + authenticate.UserMasterID + "_" + timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }

                #region FilePath
                string Folderpath = Directory.GetCurrentDirectory();
                filesName = finalAttchment.Split(",");


                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor));
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor));


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



                DataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));
                CSVlist = newSLA.StoreSLABulkUpload(new StoreSLAService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, DataSetCSV);


                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "Store_SLASuccessFile_" + timeStamp + ".csv";
                string ErrorFileName = "Store_SLAErrorFile_" + timeStamp + ".csv";

                string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                  rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)SLAFor) + "/Error/" + ErrorFileName : string.Empty;

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);

                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectioSting), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "Store_SLA", SuccessFileUrl, ErrorFileUrl, SLAFor);
                #endregion

                StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

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
