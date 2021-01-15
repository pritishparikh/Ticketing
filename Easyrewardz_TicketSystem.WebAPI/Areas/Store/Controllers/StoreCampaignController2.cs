using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public partial class StoreCampaignController : ControllerBase
    {
        #region Custom Method

        /// <summary>
        /// GetCampaignDetails
        /// </summary>
        /// <param name="campaignName"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignDetails")]
        public async Task<ResponseModel> GetCampaignDetails(string campaignName = "", string statusId="All")
        {
            List<StoreCampaignModel2> objStoreCampaign = new List<StoreCampaignModel2>();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
               
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objStoreCampaign =await storecampaigncaller.GetStoreCampaign(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, campaignName, statusId);
                statusCode =
                   objStoreCampaign.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreCampaign;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Customer popup Details
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustomerpopupDetails")]
        public async Task<ResponseModel> GetCustomerpopupDetails(string mobileNumber, string campaignID)
        {
            StoresCampaignStatusResponse objStoreCampaign = new StoresCampaignStatusResponse();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string ClientAPIURL = configuration.GetValue<string>("ClientAPIURL");
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objStoreCampaign = await storecampaigncaller.GetCustomerpopupDetailsList(new StoreCampaignService(_connectioSting, _chatbotBellHttpClientService, _CampaingURLList), mobileNumber, authenticate.ProgramCode, campaignID, authenticate.TenantId, authenticate.UserMasterID, ClientAPIURL);
                statusCode =
                   objStoreCampaign==null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreCampaign;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        #region Camapaign Customr Pop up dateis APIs


        [HttpPost]
        [Route("GetStoreCampaignKeyInsight")]
        public async Task<ResponseModel> GetStoreCampaignKeyInsight(string mobileNumber, string campaignID, string lifetimeValue = null, string VisitCount = null)
        {
            StoreCampaignKeyInsight KeyInsight = new StoreCampaignKeyInsight();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string ClientAPIURL = configuration.GetValue<string>("ClientAPIURL");
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                KeyInsight = await storecampaigncaller.GetStoreCampaignKeyInsight(new StoreCampaignService(_connectioSting, _chatbotBellHttpClientService, _CampaingURLList),  
                    lifetimeValue,  VisitCount, mobileNumber, authenticate.ProgramCode, campaignID, authenticate.TenantId, authenticate.UserMasterID, ClientAPIURL);
                statusCode =  KeyInsight!=null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = KeyInsight;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        [HttpPost]
        [Route("GetCampaignRecommendationList")]
        public async Task<ResponseModel> GetCampaignRecommendationList(string mobileNumber)
        {
            List<StoreCampaignRecommended> objrecommendedDetails = new List<StoreCampaignRecommended>();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string ClientAPIURL = configuration.GetValue<string>("ClientAPIURL");
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objrecommendedDetails = await storecampaigncaller.GetCampaignRecommendationList(new StoreCampaignService(_connectioSting), mobileNumber, authenticate.ProgramCode , authenticate.TenantId, authenticate.UserMasterID);
                statusCode =  objrecommendedDetails.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objrecommendedDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;


        }


        [HttpPost]
        [Route("GetStoreCampaignLastTransactionDetails")]
        public async Task<ResponseModel> GetStoreCampaignLastTransactionDetails(string mobileNumber)
        {
            StoreCampaignLastTransactionDetails objLastTransactionDetails = new StoreCampaignLastTransactionDetails();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string ClientAPIURL = configuration.GetValue<string>("ClientAPIURL");
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objLastTransactionDetails = await storecampaigncaller.GetStoreCampaignLastTransactionDetails(new StoreCampaignService(_connectioSting, _chatbotBellHttpClientService, _CampaingURLList), 
                    mobileNumber, authenticate.ProgramCode,ClientAPIURL);
                statusCode = objLastTransactionDetails !=null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objLastTransactionDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;


        }


        [HttpPost]
        [Route("GetShareCampaignViaSetting")]
        public async Task<ResponseModel> GetShareCampaignViaSetting(string mobileNumber)
        {
            ShareCampaignViaSettingModal shareCampaignViaSettingModal = new ShareCampaignViaSettingModal();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string ClientAPIURL = configuration.GetValue<string>("ClientAPIURL");
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shareCampaignViaSettingModal = await storecampaigncaller.GetShareCampaignViaSetting(new StoreCampaignService(_connectioSting),
                    mobileNumber, authenticate.ProgramCode, authenticate.TenantId, authenticate.UserMasterID);
                statusCode = shareCampaignViaSettingModal != null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shareCampaignViaSettingModal;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;


        }



        #endregion


        /// <summary>
        /// Get Campaign Logo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignLogo")]
        public async Task<ResponseModel> GetCampaignLogo()
        {
            List<StoreCampaignLogo> objStoreCampaign = new List<StoreCampaignLogo>();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objStoreCampaign =await storecampaigncaller.GetCampaignDetailsLogo(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   objStoreCampaign.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreCampaign;
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