using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleSettingController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor
        public ModuleSettingController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods

        #region Claim Attachment Setting

        /// <summary>
        /// Get Store Attachment Settings
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreAttachmentSettings")]
        public ResponseModel GetStoreAttachmentSettings()
        {
            AttachmentSettingResponseModel attachmentsettingList = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ModulesSettingColler attachmentsettingcoller = new ModulesSettingColler();

                attachmentsettingList = attachmentsettingcoller.GetStoreAttachmentSettings(new ModulesSettingService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID);

                statusCode =(int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = attachmentsettingList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Modify Store Attachment Settings
        /// </summary>
        /// <param name="AttachmentSettings"></param
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyStoreAttachmentSettings")]
        public ResponseModel ModifyStoreAttachmentSettings([FromBody] AttachmentSettingsRequest AttachmentSettings)
        {
            int response = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ModulesSettingColler attachmentsettingcoller = new ModulesSettingColler();

                response = attachmentsettingcoller.ModifyStoreAttachmentSettings(new ModulesSettingService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, AttachmentSettings);

                statusCode = response == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = response;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion


        #region Campaign Script

        /// <summary>
        ///  Get All Campaign Script / By CampaignID
        /// </summary>
        /// <param name="CampaignID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignScript")]
        public ResponseModel GetCampaignScript(int CampaignID = 0)
        {
            List<CampaignScriptDetails> campaignscriptdetailsList= null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ModulesSettingColler attachmentsettingcoller = new ModulesSettingColler();

                campaignscriptdetailsList = attachmentsettingcoller.GetCampaignScript(new ModulesSettingService(_connectioSting), authenticate.TenantId, CampaignID);

                statusCode = campaignscriptdetailsList.Count < 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = campaignscriptdetailsList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        ///  Get Campaign Name
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignName")]
        public ResponseModel GetCampaignName()
        {
            List<CampaignScriptName> campaignscriptnameList = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ModulesSettingColler attachmentsettingcoller = new ModulesSettingColler();

                campaignscriptnameList = attachmentsettingcoller.GetCampaignName(new ModulesSettingService(_connectioSting), authenticate.TenantId);

                statusCode = campaignscriptnameList.Count < 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = campaignscriptnameList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Validate Campaign Name Exit
        /// </summary>
        /// <param name="CampaignNameID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateCampaignNameExit")]
        public ResponseModel ValidateCampaignNameExit(int CampaignNameID)
        {
            string CampaignNameExist = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ModulesSettingColler attachmentsettingcoller = new ModulesSettingColler();

                CampaignNameExist = attachmentsettingcoller.ValidateCampaignNameExit(new ModulesSettingService(_connectioSting), CampaignNameID, authenticate.TenantId);

                statusCode = CampaignNameExist.Length < 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CampaignNameExist;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Insert Campaign Script
        /// </summary>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertCampaignScript")]
        public ResponseModel InsertCampaignScript([FromBody]CampaignScriptRequest Campaignscript)
        {
            int result = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ModulesSettingColler attachmentsettingcoller = new ModulesSettingColler();

                result = attachmentsettingcoller.InsertCampaignScript(new ModulesSettingService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, Campaignscript);

                statusCode = result < 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// Update Campaign Script
        /// </summary>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCampaignScript")]
        public ResponseModel UpdateCampaignScript([FromBody]CampaignScriptRequest Campaignscript)
        {
            int result = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ModulesSettingColler attachmentsettingcoller = new ModulesSettingColler();

                result = attachmentsettingcoller.UpdateCampaignScript(new ModulesSettingService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, Campaignscript);

                statusCode = result < 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// Delete Campaign Script
        /// </summary>
        /// <param name="CampaignID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteCampaignScript")]
        public ResponseModel DeleteCampaignScript(int CampaignID)
        {
            int result = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ModulesSettingColler attachmentsettingcoller = new ModulesSettingColler();

                result = attachmentsettingcoller.DeleteCampaignScript(new ModulesSettingService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, CampaignID);

                statusCode = result < 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        #endregion

        #endregion
    }
}