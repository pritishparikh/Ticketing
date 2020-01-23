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
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
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

        #region Cunstructor
        public AlertController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Create Alert
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateAlert")]
        public ResponseModel CreateAlert(string TemplateName, string TemplateSubject, string TemplateBody, string issueTypes, bool isTemplateActive)
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

                SettingsCaller _newAlert = new SettingsCaller();


                insertcount = _newAlert.InsertTemplate(new TemplateService(_connectioSting), authenticate.TenantId, TemplateName, TemplateSubject,
                    TemplateBody, issueTypes, isTemplateActive, authenticate.UserMasterID);

                StatusCode =
                insertcount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = insertcount;

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
        /// UpdateAlert
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyAlert")]
        public ResponseModel ModifyAlert(int AlertID, string AlertTypeName, bool isAlertActive)
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

                SettingsCaller _newAlert = new SettingsCaller();

                updatecount = _newAlert.UpdateAlert(new AlertService(_connectioSting), authenticate.TenantId, AlertID, AlertTypeName, isAlertActive, authenticate.UserMasterID);

                StatusCode =
                updatecount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = updatecount;

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
        /// Delete Alert
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteAlert")]
        public ResponseModel DeleteAlert(int AlertID)
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

                SettingsCaller _newAlert = new SettingsCaller();

                Deletecount = _newAlert.DeleteAlert(new AlertService(_connectioSting), authenticate.TenantId, AlertID);

                StatusCode =
                Deletecount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Deletecount;

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
        /// View SLA 
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[Route("GetAlertList")]
        //public ResponseModel GetAlertList()
        //{

        //    ResponseModel _objResponseModel = new ResponseModel();
        //    List<AlertModel> _objresponseModel = new List<AlertModel>();
        //    int StatusCode = 0;
        //    string statusMessage = "";
        //    try
        //    {
        //        //Get token (Double encrypted) and get the tenant id 
        //        string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
        //        Authenticate authenticate = new Authenticate();
        //        authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

        //        SettingsCaller _newAlert = new SettingsCaller();

        //        _objresponseModel = _newAlert.GetAlertList(new AlertService(_connectioSting), authenticate.TenantId);
        //        StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

        //        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

        //        _objResponseModel.Status = true;
        //        _objResponseModel.StatusCode = StatusCode;
        //        _objResponseModel.Message = statusMessage;
        //        _objResponseModel.ResponseData = _objresponseModel;

        //    }
        //    catch (Exception ex)
        //    {
        //        StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
        //        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

        //        _objResponseModel.Status = true;
        //        _objResponseModel.StatusCode = StatusCode;
        //        _objResponseModel.Message = statusMessage;
        //        _objResponseModel.ResponseData = null;
        //    }

        //    return _objResponseModel;
        //}

        #endregion

    }
}
