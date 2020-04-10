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
        private readonly string _UploadedBulkFile;
        #endregion

        #region Constructor


        public StoreSLAController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _UploadedBulkFile = configuration.GetValue<string>("FileUploadLocation");
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
        public ResponseModel BulkUploadStoreSLA()
        {


            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            int count = 0;
            List<string> CSVlist = new List<string>();
            StoreSLACaller newSLA = new StoreSLACaller();
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

                #region Read from Form

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", "_" + authenticate.UserMasterID + "_" + timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }

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

                        }
                    }
                }


                BulkUploadFilesPath = Folderpath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)3);
                DownloadFilePath = Folderpath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)3);



                DataSetCSV = CommonService.csvToDataSet(Folderpath + "\\" + filesName[0]);
                CSVlist = newSLA.StoreSLABulkUpload(new StoreSLAService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, DataSetCSV);

                #endregion


                #region Create Error and Succes files

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Store\\Success" + "\\" + "SLASuccessFile.csv", CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\Store\\Error" + "\\" + "SLAErrorFile.csv", CSVlist[1]);

                #endregion

                #region Insert in FileUploadLog


                count = newSLA.CreateFileUploadLog(new FileUploadService(_connectioSting), authenticate.TenantId, finalAttchment, errorfilesaved,
                                   "SLAErrorFile.csv", "SLASuccessFile.csv", authenticate.UserMasterID, "SLA",
                                   DownloadFilePath + "\\SLA\\Error" + "\\" + "StoreErrorFile.csv",
                                   DownloadFilePath + "\\SLA\\ Success" + "\\" + "StoreSuccessFile.csv", 1
                                   );
                #endregion

                StatusCode = successfilesaved ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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
