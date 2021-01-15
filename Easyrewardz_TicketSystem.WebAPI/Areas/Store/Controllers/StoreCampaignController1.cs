using System;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
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

        private readonly ChatbotBellHttpClientService _chatbotBellHttpClientService;
        #endregion

        #region Constructor
        public StoreCampaignController(IConfiguration _iConfig, ChatbotBellHttpClientService chatbotBellHttpClientService)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _chatbotBellHttpClientService = chatbotBellHttpClientService;
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// Get Campaign Customer
        /// </summary>
        /// <param name="campaignScriptID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignCustomer")]
        public async Task<ResponseModel> GetCampaignCustomer(CampaingCustomerFilterRequest campaingCustomerFilterRequest)
        {
            CampaignCustomerDetails obj = new CampaignCustomerDetails();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                obj = await storecampaigncaller.GetCampaignCustomer(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, campaingCustomerFilterRequest);
                statusCode =
                   obj.CampaignCustomerCount == 0 ?
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
        public async Task<ResponseModel> UpdateCampaignStatusResponse(CampaignResponseInput objRequest)
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

                obj =await storecampaigncaller.UpdateCampaignStatusResponse(new StoreCampaignService(_connectioSting), objRequest, authenticate.TenantId, authenticate.UserMasterID);
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
        public async Task<ResponseModel> CampaignShareChatbot(ShareChatbotModel objRequest)
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

                string ClientAPIURL = configuration.GetValue<string>("ClientAPIURL");

                obj =await storecampaigncaller.CampaignShareChatbot(new StoreCampaignService(_connectioSting), objRequest, ClientAPIURL, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
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
        public async Task<ResponseModel> CampaignShareMassanger(ShareChatbotModel objRequest)
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

                obj = await storecampaigncaller.CampaignShareMassanger(new StoreCampaignService(_connectioSting), objRequest, authenticate.TenantId, authenticate.UserMasterID);
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
        public async Task<ResponseModel> CampaignShareSMS(ShareChatbotModel objRequest)
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

                string ClientAPIURL =  configuration.GetValue<string>("ClientAPIURL");
                string SMSsenderId = "";

                obj = await storecampaigncaller.CampaignShareSMS(new StoreCampaignService(_connectioSting), objRequest, ClientAPIURL, SMSsenderId, authenticate.TenantId, authenticate.UserMasterID);
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
        /// GetBroadcastConfigurationResponses
        /// </summary>
        /// <param name="storeCode"></param>
        /// <param name="campaignCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetBroadcastConfigurationResponses")]
        public async Task<ResponseModel> GetBroadcastConfigurationResponses(string storeCode, string campaignCode)
        {
            BroadcastDetails broadcastDetails = new BroadcastDetails();
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                broadcastDetails = await storecampaigncaller.GetBroadcastConfigurationResponses(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, storeCode, campaignCode);
                statusCode =
                   broadcastDetails.BroadcastConfigurationResponse.ID == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = broadcastDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// InsertBroadCastDetails
        /// </summary>
        /// <param name="storeCode"></param>
        /// <param name="campaignCode"></param>
        /// <param name="channelType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertBroadCastDetails")]
        public async Task<ResponseModel> InsertBroadCastDetails(string storeCode, string campaignCode, string channelType)
        {
            int result = 0;
            StoreCampaignCaller storecampaigncaller = new StoreCampaignCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                string clientAPIURL = configuration.GetValue<string>("ClientAPIURL");

                result = await storecampaigncaller.InsertBroadCastDetails(new StoreCampaignService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, storeCode, campaignCode, channelType, clientAPIURL);
                statusCode =
                   result == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
    }
}