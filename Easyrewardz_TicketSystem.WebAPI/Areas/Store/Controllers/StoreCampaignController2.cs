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
        public ResponseModel GetCampaignDetails(string campaignName = "", string statusId="All")
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objStoreCampaign = storecampaigncaller.GetStoreCampaign(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, campaignName, statusId);
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
        public ResponseModel GetCustomerpopupDetails(string mobileNumber,string programCode, string campaignID)
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objStoreCampaign = storecampaigncaller.GetCustomerpopupDetailsList(new StoreCampaignService(_connectioSting), mobileNumber, programCode, campaignID, authenticate.TenantId, authenticate.UserMasterID, ClientAPIURL);
                statusCode =
                   objStoreCampaign.campaignrecommended.Count == 0 ?
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
        /// Get Campaign Logo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignLogo")]
        public ResponseModel GetCampaignLogo()
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objStoreCampaign = storecampaigncaller.GetCampaignDetailsLogo(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID);
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