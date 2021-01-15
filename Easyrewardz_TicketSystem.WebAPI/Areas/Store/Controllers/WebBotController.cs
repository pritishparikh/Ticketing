using ClientAPIServiceCall;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.WebAPI.Provider.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class WebBotController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionString;
        private readonly string _radisCacheServerAddress;

        private readonly string rootPath;

        #endregion

        #region API url variable declaration
        private static string _ClientAPIUrl;
        private static string _WebBotUrl;
        private static string _MaxHSMURL;

        private readonly string _getWebBotLink; 
        private readonly string _sendCampaign;
        private readonly string _makeBellActive;
        private readonly string _MaxWeBotHSM;



        private readonly string _from;
        private readonly string _ttl;
        private readonly string _type;
        //private readonly string _namespace;
        private readonly string _policy;
        private readonly string _code;
        //private readonly string _maxbot;
        //private readonly string _lsbot;
        //private readonly string _hcbot;
        private readonly List<WebBotHSMSetting> _hsmProgramcode;


        #endregion

        private readonly ChatbotBellHttpClientService _chatbotBellHttpClientService;
        private readonly WebBotHttpClientService _webBotBellHttpClientService;
        private readonly MaxWebBotHttpClientService _maxwebBotBellHttpClientService;
        private readonly ILogger<WebBotController> _logger;

        public WebBotController(IConfiguration iConfig, ChatbotBellHttpClientService chatbotBellHttpClientService, WebBotHttpClientService webBotBellHttpClientService,
            MaxWebBotHttpClientService maxwebBotBellHttpClientService,
        ILogger<WebBotController> logger)
        {
            configuration = iConfig;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
          
            rootPath = configuration.GetValue<string>("APIURL");
            _chatbotBellHttpClientService = chatbotBellHttpClientService;
            _webBotBellHttpClientService = webBotBellHttpClientService;
            _maxwebBotBellHttpClientService = maxwebBotBellHttpClientService;
            _logger = logger;

            _ClientAPIUrl = configuration.GetValue<string>("ClientAPIURL");
            _WebBotUrl = configuration.GetValue<string>("WebBotAPIURL");
            _MaxHSMURL = configuration.GetValue<string>("MaxHSMURL");
            _getWebBotLink = configuration.GetValue<string>("ClientAPI:WebBotLink");
            _sendCampaign = configuration.GetValue<string>("ClientAPI:SendCampaign");
            _makeBellActive = configuration.GetValue<string>("ClientAPI:MakeBellActive");
            _MaxWeBotHSM = configuration.GetValue<string>("ClientAPI:MaxWeBotHSM");

                 
            _from = configuration.GetValue<string>("WebBotHSMSParameters:from");
            _ttl = configuration.GetValue<string>("WebBotHSMSParameters:ttl");
            _type = configuration.GetValue<string>("WebBotHSMSParameters:type");
            //_namespace = configuration.GetValue<string>("WebBotHSMSParameters:namespace");
            _policy = configuration.GetValue<string>("WebBotHSMSParameters:policy");
            _code = configuration.GetValue<string>("WebBotHSMSParameters:code");
            //_maxbot = configuration.GetValue<string>("WebBotHSMSParameters:maxbot");
            //_lsbot = configuration.GetValue<string>("WebBotHSMSParameters:lsbot");
            //_hcbot = configuration.GetValue<string>("WebBotHSMSParameters:hcbot");

            _hsmProgramcode = new List<WebBotHSMSetting>();
            var valuesSection = configuration.GetSection("WebBotHSMSParameters:HSMProgramcode");
            foreach (IConfigurationSection section in valuesSection.GetChildren())
            {
                WebBotHSMSetting webBotHSMSetting = new WebBotHSMSetting
                {
                    Programcode = section.GetValue<string>("Programcode"),
                    bot = section.GetValue<string>("bot"),
                    @namespace = section.GetValue<string>("namespace")
                };
                _hsmProgramcode.Add(webBotHSMSetting);
            }

        }


        /// <summary>
        /// Get WebBot Option
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWebBotOption")]
        public async Task<ResponseModel> GetWebBotOption()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<HSWebBotModel> WebBotOption = new List<HSWebBotModel>();

            int statusCode = 0;
            string statusMessage = "";
            try
            {

                WebBotCaller caller = new WebBotCaller();
                WebBotOption = await caller.GetWebBotOption(new HSWebBotService(_connectionString));

                statusCode =
              WebBotOption.Count >  0 ?
                    (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = WebBotOption;   
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get WebBot Filter Details By OptionID
        /// </summary>
        /// <param name="OptionID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWebBotFilterByOptionID")]
        public async Task<ResponseModel> GetWebBotFilterByOptionID(int OptionID)
        {

            ResponseModel objResponseModel = new ResponseModel();
            WebBotFilterByOptionID webBotFilterByOptionID = new WebBotFilterByOptionID();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                WebBotCaller caller = new WebBotCaller();
                webBotFilterByOptionID = await caller.GetWebBotFilterByOptionID(new HSWebBotService(_connectionString),authenticate.TenantId,authenticate.UserMasterID, OptionID);

                statusCode = webBotFilterByOptionID.WebBotFilter.Count > 0 ?
                    (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = webBotFilterByOptionID;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Send WebBot HSM
        /// </summary>
        /// <param name="CustomerName"></param>
        /// <param name="MobileNo"></param>
        /// <param name="StoreCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendWebBotHSM")]
        public async Task<ResponseModel> SendWebBotHSM([FromBody] WebBotContentRequest webBotcontentRequest)
        {
            ResponseModel objResponseModel = new ResponseModel();
            WebContentDetails WebBotResponse = new WebContentDetails();
            string APIresponse = string.Empty;
            
            int statusCode = 0;
            string statusMessage = "";


            try
            {

                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                WebBotCaller caller = new WebBotCaller();

                webBotcontentRequest.TenantID = authenticate.TenantId;
                webBotcontentRequest.ProgramCode = authenticate.ProgramCode;
                webBotcontentRequest.UserID = authenticate.UserMasterID;
                webBotcontentRequest.MakeBellActiveUrl = _ClientAPIUrl + _makeBellActive;
                webBotcontentRequest.ClientAPIUrl = _ClientAPIUrl + _sendCampaign;
                webBotcontentRequest.WeBBotGenerationLink = _WebBotUrl + _getWebBotLink;
                webBotcontentRequest.MaxWebBotHSMURL = _MaxHSMURL + _MaxWeBotHSM;


                WebBotHSMSetting webBotHSMSetting = new WebBotHSMSetting();
                webBotHSMSetting = _hsmProgramcode.Find(x => x.Programcode.Equals(authenticate.ProgramCode));

                if (webBotHSMSetting != null)
                {
                    webBotcontentRequest.MaxWebBotHSMURL = webBotcontentRequest.MaxWebBotHSMURL + "?bot=" + webBotHSMSetting.bot;

                    MaxWebBotHSMRequest maxreq = new MaxWebBotHSMRequest();
                    Hsm hsmReq = new Hsm()
                    {
                        @namespace = webBotHSMSetting.@namespace,
                        language = new Language() { policy = _policy, code = _code }
                    };
                    Body body = new Body()
                    {
                        from = webBotcontentRequest.WABANo,// _from,
                        ttl = Convert.ToInt32(_ttl),
                        type = _type,
                        hsm = hsmReq
                    };
                    maxreq.body = body;

                    webBotcontentRequest.MaxHSMRequest = maxreq;

                }


                   

                WebBotResponse = await caller.SendWebBotHSM(new HSWebBotService(_connectionString, _chatbotBellHttpClientService, _webBotBellHttpClientService, _maxwebBotBellHttpClientService, _logger), webBotcontentRequest);
                statusCode =  WebBotResponse.IsHSMSent  ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServiceNotWorking;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = WebBotResponse;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Send WebBot HSM
        /// </summary>
        /// <param name="ActiveOptions"></param>
        /// <param name="InActiveOptions"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateHSMOptions")]
        public async Task<ResponseModel> UpdateHSMOptions(string ActiveOptions, string InActiveOptions)
        {
            ResponseModel objResponseModel = new ResponseModel();
            string statusMessage = string.Empty;
            int statusCode = 0;
            int Result = 0;
            try
            {

                WebBotCaller caller = new WebBotCaller();

                Result = await caller.UpdateHSMOptions(new HSWebBotService(_connectionString),  ActiveOptions,  InActiveOptions);
                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

    } 
}
