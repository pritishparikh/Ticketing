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

        /// <summary>
        /// Get Lis tOf Template For Note
        /// </summary>
        /// <param name="IssueTypeID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getListOfTemplateForNote")]
        public ResponseModel GetListOfTemplateForNote(int IssueTypeID)
        {

            List<Template> objTemplate = new List<Template>();
            TemplateCaller templatecaller = new TemplateCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                objTemplate = templatecaller.GetTemplateForNote(new TemplateService(_connectioSting), IssueTypeID, authenticate.TenantId);
                StatusCode =
                   objTemplate.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTemplate;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Template Content
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTemplateContent")]
        public ResponseModel GetTemplateContent(int TemplateId)
        {
            Template objTemplate = new Template();
            TemplateCaller templatecaller = new TemplateCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objTemplate = templatecaller.GetTemplateContent(new TemplateService(_connectioSting), TemplateId, authenticate.TenantId);
                StatusCode =
                   objTemplate == null  ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTemplate;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        #region Contoller for Setting

        /// <summary>
        /// Create Template
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <param name="TemplateSubject"></param>
        /// <param name="TemplateBody"></param>
        /// <param name="issueTypes"></param>
        /// <param name="isTemplateActive"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateTemplate")]
        public ResponseModel CreateTemplate(string TemplateName, string TemplateSubject, string TemplateBody, string issueTypes, bool isTemplateActive)
        {
            int insertcount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();

              
                insertcount = newTemplate.InsertTemplate(new TemplateService(_connectioSting), authenticate.TenantId,TemplateName, TemplateSubject,
                    TemplateBody, issueTypes, isTemplateActive,authenticate.UserMasterID);

                StatusCode =
                insertcount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Update Template
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <param name="TemplateName"></param>
        /// <param name="issueType"></param>
        /// <param name="isTemplateActive"></param>
        /// <param name="templateSubject"></param>
        /// <param name="templateContent"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyTemplate")]
        public ResponseModel ModifyTemplate(int TemplateID, string TemplateName, string issueType, bool isTemplateActive, string templateSubject, string templateContent)
        {
            int updatecount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();


                updatecount = newTemplate.UpdateTemplate(new TemplateService(_connectioSting), authenticate.TenantId, TemplateID, TemplateName, issueType, isTemplateActive,
                authenticate.UserMasterID, templateSubject, templateContent);

                StatusCode =
                updatecount == 0 ?
                (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updatecount;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Delete Template
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteTemplate")]
        public ResponseModel DeleteTemplate(int TemplateID)
        {
            int Deletecount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();
                Deletecount = newTemplate.DeleteTemplate(new TemplateService(_connectioSting), authenticate.TenantId, TemplateID );

                StatusCode =
                Deletecount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Deletecount;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Get  Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTemplate")]
        public ResponseModel GetTemplate()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<TemplateModel> objresponseModel = new List<TemplateModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();
                objresponseModel = newTemplate.GetTemplates(new TemplateService(_connectioSting), authenticate.TenantId);

                StatusCode = objresponseModel.Count > 0 ? (int)EnumMaster.StatusCode.Success  : (int)EnumMaster.StatusCode.RecordNotFound; ;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get Mail Parameter
        /// </summary>
        /// <param name="AlertID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMailParameter")]
        public ResponseModel GetMailParameter(int AlertID)
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<MailParameterModel> objMailParameterModel = new List<MailParameterModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();
                objMailParameterModel = newTemplate.GetMailParameter(new TemplateService(_connectioSting), authenticate.TenantId, AlertID);

                StatusCode = objMailParameterModel.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound; ;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objMailParameterModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion


        #endregion
    }
}