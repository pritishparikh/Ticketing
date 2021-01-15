using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public partial class StoreCampaignController : ControllerBase
    {
        /// <summary>
        /// Get Campaign Setting List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignSettingList")]
        public async Task<ResponseModel> GetCampaignSettingList()
        {
            StoreCampaignModel3 objStoreCampaignSetting =new StoreCampaignModel3();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objStoreCampaignSetting =await storecampaigncaller.GetStoreCampignSetting(new StoreCampaignService(_connectioSting),
                    authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   objStoreCampaignSetting.CampaignSettingTimer.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreCampaignSetting;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Update Campaign Setting
        /// </summary>
        /// <param name="CampaignModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCampaignSetting")]
        public async Task<ResponseModel> UpdateCampaignSetting([FromBody] StoreCampaignSettingModel CampaignModel)
        {

            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int UpdateCount = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount =await storecampaigncaller.UpdateStoreCampaignSetting(new StoreCampaignService(_connectioSting), CampaignModel);
                statusCode = UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        ///Update Campaign Max Click Timer
        /// </summary>
        /// <param name="TimerID"></param>
        /// <param name="MaxClick"></param>
        /// <param name="EnableClickAfter"></param>
        /// <param name="ClickAfterDuration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCampaignMaxClickTimer")]
        public async Task<ResponseModel> UpdateCampaignMaxClickTimer([FromBody] StoreCampaignSettingTimer storeCampaignSettingTimer)
        {

            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int UpdateCount = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = await storecampaigncaller.UpdateCampaignMaxClickTimer(new StoreCampaignService(_connectioSting), storeCampaignSettingTimer, authenticate.UserMasterID);
                statusCode = UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Broadcast Configuration
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetBroadcastConfiguration")]
        public async Task<ResponseModel> GetBroadcastConfiguration()
        {
            StoreBroadcastConfiguration StoreBroadcastConfiguration = new StoreBroadcastConfiguration();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreBroadcastConfiguration =await storecampaigncaller.GetBroadcastConfiguration(new StoreCampaignService(_connectioSting),
                    authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   StoreBroadcastConfiguration.ID.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = StoreBroadcastConfiguration;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Appointment Configuration
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAppointmentConfiguration")]
        public async Task<ResponseModel> GetAppointmentConfiguration()
        {
            StoreAppointmentConfiguration StoreBroadcastConfiguration = new StoreAppointmentConfiguration();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreBroadcastConfiguration =await storecampaigncaller.GetAppointmentConfiguration(new StoreCampaignService(_connectioSting),
                    authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   StoreBroadcastConfiguration.ID.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = StoreBroadcastConfiguration;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Broadcast Configuration
        /// </summary>
        /// <param name="storeBroadcastConfiguration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateBroadcastConfiguration")]
        public async Task<ResponseModel> UpdateBroadcastConfiguration([FromBody]StoreBroadcastConfiguration storeBroadcastConfiguration)
        {
            int UpdateCount = 0;
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount =await storecampaigncaller.UpdateBroadcastConfiguration(new StoreCampaignService(_connectioSting),
                    storeBroadcastConfiguration, authenticate.UserMasterID);
                statusCode =
                   UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Appointment Configuration
        /// </summary>
        /// <param name="storeAppointmentConfiguration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAppointmentConfiguration")]
        public async Task<ResponseModel> UpdateAppointmentConfiguration([FromBody]StoreAppointmentConfiguration storeAppointmentConfiguration)
        {
            int UpdateCount = 0;
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount =await storecampaigncaller.UpdateAppointmentConfiguration(new StoreCampaignService(_connectioSting),
                    storeAppointmentConfiguration, authenticate.UserMasterID);
                statusCode =
                   UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetLanguageDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLanguageDetails")]
        public async Task<ResponseModel> GetLanguageDetails()
        {
            List<Languages> languageslist = new List<Languages>();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                languageslist =await storecampaigncaller.GetLanguageDetails(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   languageslist.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = languageslist;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// InsertLanguageDetails
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertLanguageDetails")]
        public async Task<ResponseModel> InsertLanguageDetails(int languageID)
        {
            int UpdateCount = 0;
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount =await storecampaigncaller.InsertLanguageDetails(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, languageID);
                statusCode =
                   UpdateCount == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : UpdateCount == 2 ? (int)EnumMaster.StatusCode.RecordAlreadyExists :  (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetSelectedLanguageDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSelectedLanguageDetails")]
        public async Task<ResponseModel> GetSelectedLanguageDetails()
        {
            List<SelectedLanguages> languageslist = new List<SelectedLanguages>();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                languageslist =await storecampaigncaller.GetSelectedLanguageDetails(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   languageslist.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = languageslist;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Delete Selected Language
        /// </summary>
        /// <param name="selectedLanguageID"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSelectedLanguage")]
        public async Task<ResponseModel> DeleteSelectedLanguage(int selectedLanguageID, bool isActive)
        {
            int UpdateCount = 0;
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount =await storecampaigncaller.DeleteSelectedLanguage(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, selectedLanguageID, isActive);
                statusCode =
                   UpdateCount == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
    }
}