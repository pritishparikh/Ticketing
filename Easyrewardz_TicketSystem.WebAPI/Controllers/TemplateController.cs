using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class TemplateController : ControllerBase
    {
            #region variable declaration
            private IConfiguration configuration;
            private readonly string _connectioSting;
            private readonly string _radisCacheServerAddress;
            #endregion

            #region Cunstructor
            public TemplateController(IConfiguration _iConfig)
            {
                configuration = _iConfig;
                _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
                _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            }
            #endregion

        #region Custom Methods
        [HttpPost]
        [Route("getListOfTemplateForNote")]
        public ResponseModel getListOfTemplateForNote(int IssueTypeID)
        {

            List<Template> _objTemplate = new List<Template>();
            TemplateCaller _templatecaller = new TemplateCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));


                _objTemplate = _templatecaller.GetTemplateForNote(new TemplateService(_connectioSting), IssueTypeID, authenticate.TenantId);
                StatusCode =
                   _objTemplate.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objTemplate;

            }
            catch (Exception _ex)
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


        [HttpPost]
        [Route("getTemplateContent")]
        public ResponseModel getTemplateContent(int TemplateId)
        {
            Template _objTemplate = new Template();
            TemplateCaller _templatecaller = new TemplateCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                _objTemplate = _templatecaller.GetTemplateContent(new TemplateService(_connectioSting), TemplateId, authenticate.TenantId);
                StatusCode =
                   _objTemplate == null  ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objTemplate;

            }
            catch (Exception _ex)
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



        #region Contoller for Setting

        /// <summary>
        /// Create Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateTemplate")]
        public ResponseModel CreateTemplate(string TemplateName, string TemplateSubject, string TemplateBody, string issueTypes, bool isTemplateActive)
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

                SettingsCaller _newTemplate = new SettingsCaller();

              
                insertcount = _newTemplate.InsertTemplate(new TemplateService(_connectioSting), authenticate.TenantId,TemplateName, TemplateSubject,
                    TemplateBody, issueTypes, isTemplateActive,authenticate.UserMasterID);

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
        /// Update Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyTemplate")]
        public ResponseModel ModifyTemplate(int TemplateID, string TemplateName, string issueType, bool isTemplateActive)
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

                SettingsCaller _newTemplate = new SettingsCaller();


                updatecount = _newTemplate.UpdateTemplate(new TemplateService(_connectioSting), authenticate.TenantId, TemplateID,TemplateName, issueType, isTemplateActive,
                    authenticate.UserMasterID);

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
        /// Delete Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteTemplate")]
        public ResponseModel DeleteTemplate(int TemplateID)
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

                SettingsCaller _newTemplate = new SettingsCaller();
                Deletecount = _newTemplate.DeleteTemplate(new TemplateService(_connectioSting), authenticate.TenantId, TemplateID );

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
        /// View  Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTemplate")]

        public ResponseModel GetTemplate()
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<TemplateModel> _objresponseModel = new List<TemplateModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newTemplate = new SettingsCaller();
                _objresponseModel = _newTemplate.GetTemplates(new TemplateService(_connectioSting), authenticate.TenantId);

                StatusCode = _objresponseModel.Count > 0 ? (int)EnumMaster.StatusCode.Success  : (int)EnumMaster.StatusCode.RecordNotFound; ;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

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