using System;
using System.Collections.Generic;
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
    public class TemplateController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public TemplateController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get list of template list
        /// </summary>
        /// <param name="IssueTypeID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getListOfTemplateForNote")]
        public ResponseModel getListOfTemplateForNote(int IssueTypeID)
        {

            List<Template> objTemplate = new List<Template>();
            TemplateCaller templateCaller = new TemplateCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));


                objTemplate = templateCaller.GetTemplateForNote(new TemplateService(Cache, Db), IssueTypeID, authenticate.TenantId);
                statusCode =
                   objTemplate.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// Get template content
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTemplateContent")]
        public ResponseModel getTemplateContent(int TemplateId)
        {
            Template objTemplate = new Template();
            TemplateCaller templateCaller = new TemplateCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objTemplate = templateCaller.GetTemplateContent(new TemplateService(Cache, Db), TemplateId, authenticate.TenantId);
                statusCode =
                   objTemplate == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// <returns></returns>
        [HttpPost]
        [Route("CreateTemplate")]
        public ResponseModel CreateTemplate(string TemplateName, string TemplateSubject, string TemplateBody, string issueTypes, bool isTemplateActive)
        {
            int insertCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();


                insertCount = newTemplate.InsertTemplate(new TemplateService(Cache, Db), authenticate.TenantId, TemplateName, TemplateSubject,
                    TemplateBody, issueTypes, isTemplateActive, authenticate.UserMasterID);

                statusCode =
                insertCount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = insertCount;

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
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyTemplate")]
        public ResponseModel ModifyTemplate(int TemplateID, string TemplateName, string issueType, bool isTemplateActive, string templateSubject, string templateContent)
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
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();


                updateCount = newTemplate.UpdateTemplate(new TemplateService(Cache, Db), authenticate.TenantId, TemplateID, TemplateName, issueType, isTemplateActive,
                    authenticate.UserMasterID, templateSubject, templateContent);

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
        /// Delete Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteTemplate")]
        public ResponseModel DeleteTemplate(int TemplateID)
        {
            int deleteCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();
                deleteCount = newTemplate.DeleteTemplate(new TemplateService(Cache, Db), authenticate.TenantId, TemplateID);

                StatusCode =
                deleteCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// View  Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTemplate")]
        public ResponseModel GetTemplate()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<TemplateModel> objTemplateResponseModel = new List<TemplateModel>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();
                objTemplateResponseModel = newTemplate.GetTemplates(new TemplateService(Cache, Db), authenticate.TenantId);

                statusCode = objTemplateResponseModel.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound; ;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTemplateResponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get mail parameter
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMailParameter")]
        public ResponseModel GetMailParameter(int AlertID)
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<MailParameterModel> objMailParameterModel = new List<MailParameterModel>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newTemplate = new SettingsCaller();
                objMailParameterModel = newTemplate.GetMailParameter(new TemplateService(Cache, Db), authenticate.TenantId, AlertID);

                statusCode = objMailParameterModel.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound; ;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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