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
    public class AlertController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor
        public AlertController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Create Store Alert
        /// </summary>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateStoreAlert")]
        public ResponseModel CreateStoreAlert([FromBody] AlertInsertModel alertModel)
        {
            int insertcount = 0;
            ResponseModel  objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                AlertCaller newAlert = new AlertCaller();
                alertModel.TenantId = authenticate.TenantId;
                alertModel.CreatedBy = authenticate.UserMasterID;
                insertcount = newAlert.CreateStoreAlert(new StoreAlertService(_connectioSting), alertModel);

                statusCode =
                insertcount == 0 ?
                     (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = insertcount;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Modify Store Alert
        /// </summary>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyStoreAlert")]
        public ResponseModel ModifyStoreAlert([FromBody] AlertUpdateModel alertModel)
        {
            int updateCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AlertCaller newAlert = new AlertCaller();
                updateCount = newAlert.UpdateStoreAlert(new StoreAlertService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, alertModel);

                statusCode =
                updateCount == 0 ?
                (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updateCount;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Delete Store Alert
        /// </summary>
        /// <param name="AlertID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteStoreAlert")]
        public ResponseModel DeleteStoreAlert(int AlertID)
        {
            int deleteCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AlertCaller newAlert = new AlertCaller();

                deleteCount = newAlert.DeleteStoreAlert(new StoreAlertService(_connectioSting), authenticate.TenantId, AlertID);

                statusCode =
                deleteCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = deleteCount;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get Store Alert List
        /// </summary>
        /// <param name="alertId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreAlertList")]
        public ResponseModel GetStoreAlertList(int alertId=0)
        {

            ResponseModel  objResponseModel = new ResponseModel();
            List<AlertModel> objAlertModel = new List<AlertModel>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AlertCaller newAlert = new AlertCaller();

                objAlertModel = newAlert.GetStoreAlertList(new StoreAlertService(_connectioSting), authenticate.TenantId, alertId);
                statusCode = objAlertModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objAlertModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Bind Store Alerts
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BindStoreAlerts")]
        public ResponseModel BindStoreAlerts()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<AlertList>  objAlertList = new List<AlertList>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AlertCaller newAlert = new AlertCaller();

                objAlertList = newAlert.BindStoreAlerts(new StoreAlertService(_connectioSting), authenticate.TenantId);
                statusCode = objAlertList.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objAlertList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Bulk Upload Store Alert
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadStoreAlert")]
        public ResponseModel BulkUploadStoreAlert()
        {
            AlertCaller newAlert = new AlertCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            string fileName = "";
            string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet dataSetCSV = new DataSet();
            string[] filesName = null;


            try
            {
                var files = Request.Form.Files;

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

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
                string folderpath = appRoot + "\\" + "BulkUpload" + "\\" + "Alert";


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
                            FileStream docFile = new FileStream(folderpath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();

                        }
                    }
                }

                dataSetCSV = CommonService.csvToDataSet(folderpath + "\\" + filesName[0]);

                #endregion

                int result = newAlert.BulkUploadStoreAlert(new StoreAlertService(_connectioSting),
                    authenticate.TenantId, authenticate.UserMasterID, dataSetCSV);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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
        /// Validate Store Alert Name Exist
        /// </summary>
        /// <param name="alertTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateStoreAlertNameExist")]
        public ResponseModel ValidateStoreAlertNameExist(int alertTypeId)
        {
            string resultMessage = "";
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AlertCaller  newAlert = new AlertCaller();

                resultMessage = newAlert.ValidateStoreAlertNameExist(new StoreAlertService(_connectioSting), alertTypeId, authenticate.TenantId);
                
                statusCode =
              string.IsNullOrEmpty(resultMessage) ?
                   (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = resultMessage;

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
