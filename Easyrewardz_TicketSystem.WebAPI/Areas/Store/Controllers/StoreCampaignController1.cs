﻿using System;
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

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public partial class StoreCampaignController : ControllerBase
    {

        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor
        public StoreCampaignController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        /// <summary>
        /// Get Campaign Customer
        /// </summary>
        /// <param name="campaignScriptID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignCustomer")]
        public ResponseModel GetCampaignCustomer(int campaignScriptID, int pageNo, int pageSize)
        {
            List<CampaignCustomerModel> obj = new List<CampaignCustomerModel>();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                obj = storecampaigncaller.GetCampaignCustomer(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, campaignScriptID, pageNo, pageSize);
                statusCode =
                   obj.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCampaignStatusResponse")]
        public ResponseModel UpdateCampaignStatusResponse(CampaignResponseInput objRequest)
        {
            int obj = new int();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                obj = storecampaigncaller.UpdateCampaignStatusResponse(new StoreCampaignService(_connectioSting), objRequest, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   obj == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Campaign Share Chat bot
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CampaignShareChatbot")]
        public ResponseModel CampaignShareChatbot(ShareChatbotModel objRequest)
        {
            int obj = 0;
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                obj = storecampaigncaller.CampaignShareChatbot(new StoreCampaignService(_connectioSting), objRequest, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   obj == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Campaign Share Massanger
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CampaignShareMassanger")]
        public ResponseModel CampaignShareMassanger(ShareChatbotModel objRequest)
        {
            string obj = string.Empty;
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                obj = storecampaigncaller.CampaignShareMassanger(new StoreCampaignService(_connectioSting), objRequest, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   obj.Length == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Campaign Share SMS
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CampaignShareSMS")]
        public ResponseModel CampaignShareSMS(ShareChatbotModel objRequest)
        {
            int obj = 0;
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                string ClientAPIURL = configuration.GetValue<string>("ClientAPIURL") + "api/BellChatBotIntegration/SendSMS";
                string SMSsenderId = configuration.GetValue<string>("SMSsenderId");

                obj = storecampaigncaller.CampaignShareSMS(new StoreCampaignService(_connectioSting), objRequest, ClientAPIURL, SMSsenderId, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   obj == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
    }
}