using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public ResponseModel GetCampaignSettingList()
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

                objStoreCampaignSetting = storecampaigncaller.GetStoreCampignSetting(new StoreCampaignService(_connectioSting),
                    authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   objStoreCampaignSetting.CampaignSetting.Count.Equals(0) ?
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
        public ResponseModel GetCampaignDetails([FromBody] StoreCampaignSettingModel CampaignModel)
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

                UpdateCount = storecampaigncaller.UpdateStoreCampaignSetting(new StoreCampaignService(_connectioSting), CampaignModel);
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
        public ResponseModel UpdateCampaignMaxClickTimer(int TimerID,int MaxClick, int EnableClickAfter, string ClickAfterDuration)
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

                UpdateCount = storecampaigncaller.UpdateCampaignMaxClickTimer(new StoreCampaignService(_connectioSting), TimerID, MaxClick, EnableClickAfter,
                    ClickAfterDuration, authenticate.UserMasterID);
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


    }
}