using ClientAPIServiceCall;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public partial class HSOrderController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionString;
        private readonly string _radisCacheServerAddress;
        private readonly string _ClientAPIUrl;
        private readonly string _ClientAPIUrlForGenerateToken;
        private readonly string _ClientAPIUrlForGeneratePaymentLink;
        private readonly string _PhygitalClientAPIURL;
        private readonly string _Client_Id;
        private readonly string _Client_Secret;
        private readonly string _Grant_Type;
        private readonly string _Scope;
        private readonly string _BulkUpload;
        private readonly string _UploadFiles;
        private readonly string _DownloadFile;
        private readonly string _rootPath;
        private readonly OrderURLList _OrderURLList;
        private readonly ClientHttpClientService _clienthttpclientservice;
        private readonly GeneratePaymentLinkHttpClientService _generatepaymentlinkhttpclientservice;
        private readonly PhygitalHttpClientService _phygitalhttpclientservice;

        private readonly ChatbotBellHttpClientService _chatbotBellHttpClientService;
        private readonly WebBotHttpClientService _webBotBellHttpClientService;
        private readonly MaxWebBotHttpClientService _maxwebBotBellHttpClientService;
        private readonly ILogger<WebBotController> _logger;
        private readonly GenerateToken _generateToken;

        #endregion

        #region API url variable declaration

        private static string _WebBotUrl;
        private static string _MaxHSMURL;

        private readonly string _getWebBotLink;
        private readonly string _sendCampaign;
        private readonly string _makeBellActive;
        private readonly string _MaxWeBotHSM;
        private readonly string _GetItemDetailsBySKU;



        private readonly string _from;
        private readonly string _ttl;
        private readonly string _type;
        private readonly string _namespace;
        private readonly string _policy;
        private readonly string _code;

        private readonly string _maxbot;
        private readonly string _lsbot;
        private readonly string _hcbot;

        private readonly List<WebBotHSMSetting> _hsmProgramcode;
        #endregion

        #region Constructor
        public HSOrderController(IConfiguration iConfig, ClientHttpClientService clienthttpclientservice,
            GeneratePaymentLinkHttpClientService generatepaymentlinkhttpclientservice,
            PhygitalHttpClientService phygitalhttpclientservice, ChatbotBellHttpClientService chatbotBellHttpClientService, WebBotHttpClientService webBotBellHttpClientService,
            MaxWebBotHttpClientService maxwebBotBellHttpClientService,
        ILogger<WebBotController> logger, GenerateToken generateToken)
        {
            configuration = iConfig;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _ClientAPIUrl = configuration.GetValue<string>("ClientAPIURL");
            _ClientAPIUrlForGenerateToken = configuration.GetValue<string>("ClientAPIForGenerateToken");
            _ClientAPIUrlForGeneratePaymentLink = configuration.GetValue<string>("ClientAPIForGeneratePaymentLink");
            _PhygitalClientAPIURL = configuration.GetValue<string>("PhygitalClientAPIURL");
            _Client_Id = configuration.GetValue<string>("Client_Id");
            _Client_Secret = configuration.GetValue<string>("Client_Secret");
            _Grant_Type = configuration.GetValue<string>("Grant_Type");
            _Scope = configuration.GetValue<string>("Scope");
            _BulkUpload = configuration.GetValue<string>("BulkUpload");
            _UploadFiles = configuration.GetValue<string>("Uploadfiles");
            _DownloadFile = configuration.GetValue<string>("Downloadfile");
            _rootPath = configuration.GetValue<string>("APIURL");
            _clienthttpclientservice = clienthttpclientservice;
            _generatepaymentlinkhttpclientservice = generatepaymentlinkhttpclientservice;
            _phygitalhttpclientservice = phygitalhttpclientservice;
            _generateToken = generateToken;

            _OrderURLList = new OrderURLList()
            {
                Generatepickup = configuration.GetValue<string>("ClientAPI:GeneratePickup"),
                Generatemanifest = configuration.GetValue<string>("ClientAPI:GenerateManifest"),
                Printmanifest = configuration.GetValue<string>("ClientAPI:PrintManifest"),
                Generatelabel = configuration.GetValue<string>("ClientAPI:GenerateLabel"),
                Printinvoice = configuration.GetValue<string>("ClientAPI:PrintInvoice"),
                Getwhatsappmessagedetails = configuration.GetValue<string>("ClientAPI:GetWhatsappMessageDetails"),
                Sendcampaign = configuration.GetValue<string>("ClientAPI:SendCampaign"),
                Sendsms = configuration.GetValue<string>("ClientAPI:SendSMS"),
                Createorder = configuration.GetValue<string>("PhygitalClientAPI:CreateOrder"),
                Chkcourieravailibilty = configuration.GetValue<string>("PhygitalClientAPI:ChkCourierAvailibilty"),
                Cancelorder = configuration.GetValue<string>("PhygitalClientAPI:CancelOrder"),
                Getgeocodebyaddress = configuration.GetValue<string>("PhygitalClientAPI:GetGeocodeByAddress"),
                Resendpaymentlink = configuration.GetValue<string>("GeneratePaymentLinkAPI:ResendPaymentLink"),
                Generatepaymentlink = configuration.GetValue<string>("GeneratePaymentLinkAPI:GeneratePaymentLink"),
                Token = configuration.GetValue<string>("GenerateTokenAPI:token"),
                GetPushOrderToPoss = configuration.GetValue<string>("PhygitalClientAPI:PushOrderToPoss")
            };

            _chatbotBellHttpClientService = chatbotBellHttpClientService;
            _webBotBellHttpClientService = webBotBellHttpClientService;
            _maxwebBotBellHttpClientService = maxwebBotBellHttpClientService;
            _logger = logger;

            _WebBotUrl = configuration.GetValue<string>("WebBotAPIURL");
            _MaxHSMURL = configuration.GetValue<string>("MaxHSMURL");
            _getWebBotLink = configuration.GetValue<string>("ClientAPI:WebBotLink");
            _sendCampaign = configuration.GetValue<string>("ClientAPI:SendCampaign");
            _makeBellActive = configuration.GetValue<string>("ClientAPI:MakeBellActive");
            _MaxWeBotHSM = configuration.GetValue<string>("ClientAPI:MaxWeBotHSM");
            _GetItemDetailsBySKU = configuration.GetValue<string>("ClientAPI:GetItemDetailsBySKU");


            _from = configuration.GetValue<string>("WebBotHSMSParameters:from");
            _ttl = configuration.GetValue<string>("WebBotHSMSParameters:ttl");
            _type = configuration.GetValue<string>("WebBotHSMSParameters:type");
            _namespace = configuration.GetValue<string>("WebBotHSMSParameters:namespace");
            _policy = configuration.GetValue<string>("WebBotHSMSParameters:policy");
            _code = configuration.GetValue<string>("WebBotHSMSParameters:code");
            _maxbot = configuration.GetValue<string>("WebBotHSMSParameters:maxbot");
            _lsbot = configuration.GetValue<string>("WebBotHSMSParameters:lsbot");
            _hcbot = configuration.GetValue<string>("WebBotHSMSParameters:hcbot");

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
        #endregion
        /// <summary>
        /// Get Order Configuration Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderConfiguration")]
        public async  Task<ResponseModel> GetOrderConfiguration()
        {
            OrderConfiguration orderConfiguration = new OrderConfiguration();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                orderConfiguration =await hSOrderCaller.GetOrderConfiguration(new HSOrderService(_connectionString),authenticate.TenantId);
                statusCode =
                   orderConfiguration.ID.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderConfiguration;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Order Configuration Data
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateOrderConfiguration")]
        public async Task<ResponseModel> UpdateOrderConfiguration([FromBody]OrderConfiguration orderConfiguration)
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                updateCount = await hSOrderCaller.UpdateOrderConfiguration(new HSOrderService(_connectionString),orderConfiguration, authenticate.UserMasterID);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Update Order Configuration Message Template
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateOrderConfigurationMessageTemplate")]
        public async Task<ResponseModel> UpdateOrderConfigurationMessageTemplate([FromBody]OrderConfiguration orderConfiguration)
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                updateCount = await hSOrderCaller.UpdateOrderConfigurationMessageTemplate(new HSOrderService(_connectionString),orderConfiguration.pHYOrderMessageTemplates, authenticate.TenantId);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Get Whatsapp Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWhatsappTemplate")]
        public async Task<ResponseModel> GetWhatsappTemplate(string MessageName)
        {
            List<PHYWhatsAppTemplate> pHYWhatsAppTemplates = new List<PHYWhatsAppTemplate>();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                pHYWhatsAppTemplates =await hSOrderCaller.GetWhatsappTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, MessageName);
                statusCode =
                   pHYWhatsAppTemplates.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = pHYWhatsAppTemplates;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Whatsapp Template
        /// </summary>
        /// <param name="templateDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateWhatsappTemplate")]
        public async Task<ResponseModel> UpdateWhatsappTemplate([FromBody]PHYWhatsAppTemplateDetails templateDetails)
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                updateCount = await hSOrderCaller.UpdateWhatsappTemplate(new HSOrderService(_connectionString),templateDetails.pHYWhatsAppTemplates, authenticate.TenantId);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Get Order Delivered Details
        /// </summary>
        /// <param name="orderDeliveredFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderDeliveredDetails")]
        public async Task<ResponseModel> GetOrderDeliveredDetails(OrderDeliveredFilterRequest orderDeliveredFilter)
        {
            OrderDeliveredDetails orderDelivereds = new OrderDeliveredDetails();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                orderDelivereds = await hSOrderCaller.GetOrderDeliveredDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, orderDeliveredFilter);
                statusCode =
                   orderDelivereds.TotalCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderDelivereds;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Order Status Filters
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderStatusFilter")]
        public async Task<ResponseModel> GetOrderStatusFilter(int pageID)
        {
            List<OrderStatusFilter> orderStatusFilter = new List<OrderStatusFilter>();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                orderStatusFilter =await hSOrderCaller.GetOrderStatusFilter(new HSOrderService(_connectionString),authenticate.TenantId, authenticate.UserMasterID, pageID);
                statusCode =
                   orderStatusFilter.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderStatusFilter;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Order Shipment Assigned Details
        /// </summary>
        /// <param name="shipmentAssignedFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetShipmentAssignedDetails")]
        public async Task<ResponseModel> GetShipmentAssignedDetails(ShipmentAssignedFilterRequest shipmentAssignedFilter)
        {
            ShipmentAssignedDetails assignedDetails = new ShipmentAssignedDetails();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                assignedDetails =await hSOrderCaller.GetShipmentAssignedDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, shipmentAssignedFilter);
                statusCode =
                   assignedDetails.TotalCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = assignedDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="shipmentAssignedRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentAssignedData")]
        public async Task<ResponseModel> UpdateShipmentAssignedData([FromBody]ShipmentAssignedRequest shipmentAssignedRequest)
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                WebBotContentRequest webBotcontentRequest = new WebBotContentRequest
                {
                    MakeBellActiveUrl = _ClientAPIUrl + _makeBellActive,
                    ClientAPIUrl = _ClientAPIUrl + _sendCampaign,
                    WeBBotGenerationLink = _WebBotUrl + _getWebBotLink,
                    MaxWebBotHSMURL = _MaxHSMURL + _MaxWeBotHSM,
                    ProgramCode = authenticate.ProgramCode
                };

                WebBotHSMSetting webBotHSMSetting = new WebBotHSMSetting();
                webBotHSMSetting = _hsmProgramcode.Find(x => x.Programcode.Equals(authenticate.ProgramCode));
                
                if (webBotHSMSetting != null)
                {
                    webBotcontentRequest.webBotHSMSetting = new WebBotHSMSetting();
                    if (!string.IsNullOrEmpty(webBotHSMSetting.Programcode))
                    {
                        webBotcontentRequest.MaxWebBotHSMURL = webBotcontentRequest.MaxWebBotHSMURL + "?bot=" + webBotHSMSetting.bot;
                        webBotcontentRequest.webBotHSMSetting = webBotHSMSetting;
                        MaxWebBotHSMRequest maxreq = new MaxWebBotHSMRequest();
                        Hsm hsmReq = new Hsm()
                        {
                            @namespace = webBotHSMSetting.@namespace,
                            language = new Language() { policy = _policy, code = _code }
                        };
                        Body body = new Body()
                        {
                            // from = webBotcontentRequest.WABANo,// _from,
                            ttl = Convert.ToInt32(_ttl),
                            type = _type,
                            hsm = hsmReq
                        };
                        maxreq.body = body;

                        webBotcontentRequest.MaxHSMRequest = maxreq;
                    }
                }

                updateCount = await hSOrderCaller.UpdateShipmentAssignedData(new HSOrderService(_connectionString, _OrderURLList, _clienthttpclientservice), shipmentAssignedRequest,authenticate.TenantId,authenticate.UserMasterID,authenticate.ProgramCode, _ClientAPIUrl, webBotcontentRequest);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Update Shopping Bag Cancel Data 
        /// </summary>
        /// <param name="ShoppingID"></param>
        /// <param name="CancelComment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentBagCancelData")]
        public async Task<ResponseModel> UpdateShipmentBagCancelData(int ShoppingID, string CancelComment, bool SharewithCustomer= false, string CustomerName = "", string CustomerMob = "")
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                WebBotContentRequest webBotcontentRequest = new WebBotContentRequest
                {
                    TenantID = authenticate.TenantId,
                    ProgramCode = authenticate.ProgramCode,
                    UserID = authenticate.UserMasterID,
                    MakeBellActiveUrl = _ClientAPIUrl + _makeBellActive,
                    ClientAPIUrl = _ClientAPIUrl + _sendCampaign,
                    WeBBotGenerationLink = _WebBotUrl + _getWebBotLink,
                    MaxWebBotHSMURL = _MaxHSMURL + _MaxWeBotHSM,
                    CustomerName = CustomerName,
                    MobileNo = CustomerMob.Replace(" ","").Replace("+",""),
                    OptionID = 3,
                    ShopingBagNo = ShoppingID
                };

                WebBotHSMSetting webBotHSMSetting = new WebBotHSMSetting();
                webBotHSMSetting = _hsmProgramcode.Find(x => x.Programcode.Equals(authenticate.ProgramCode));
                if (webBotHSMSetting != null)
                {
                    webBotcontentRequest.webBotHSMSetting = new WebBotHSMSetting();
                    if (!string.IsNullOrEmpty(webBotHSMSetting.Programcode))
                    {
                        webBotcontentRequest.MaxWebBotHSMURL = webBotcontentRequest.MaxWebBotHSMURL + "?bot=" + webBotHSMSetting.bot;
                        webBotcontentRequest.webBotHSMSetting = webBotHSMSetting;
                        MaxWebBotHSMRequest maxreq = new MaxWebBotHSMRequest();
                        Hsm hsmReq = new Hsm()
                        {
                            @namespace = webBotHSMSetting.@namespace,
                            language = new Language() { policy = _policy, code = _code }
                        };
                        Body body = new Body()
                        {
                            // from = _from,
                            ttl = Convert.ToInt32(_ttl),
                            type = _type,
                            hsm = hsmReq
                        };
                        maxreq.body = body;

                        webBotcontentRequest.MaxHSMRequest = maxreq;
                    }
                }


                updateCount = await hSOrderCaller.UpdateShipmentBagCancelData(new HSOrderService(_connectionString, null, null, null, null, _chatbotBellHttpClientService, _webBotBellHttpClientService, _maxwebBotBellHttpClientService), ShoppingID, CancelComment, authenticate.UserMasterID, SharewithCustomer, webBotcontentRequest);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Update Shipment Pickup Pending Data
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentPickupPendingData")]
        public async Task<ResponseModel> UpdateShipmentPickupPendingData(int OrderID)
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                updateCount = await hSOrderCaller.UpdateShipmentPickupPendingData(new HSOrderService(_connectionString), OrderID);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Insert Convert To Order Details
        /// </summary>
        /// <param name="convertToOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertOrderDetails")]
        public async Task<ResponseModel> InsertOrderDetails([FromBody]ConvertToOrder convertToOrder)
        {
            int insertCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                WebBotContentRequest webBotcontentRequest = new WebBotContentRequest
                {
                    MakeBellActiveUrl = _ClientAPIUrl + _makeBellActive,
                    ClientAPIUrl = _ClientAPIUrl + _sendCampaign,
                    WeBBotGenerationLink = _WebBotUrl + _getWebBotLink,
                    MaxWebBotHSMURL = _MaxHSMURL + _MaxWeBotHSM,
                    ProgramCode = authenticate.ProgramCode
                };

                WebBotHSMSetting webBotHSMSetting = new WebBotHSMSetting();
                webBotHSMSetting = _hsmProgramcode.Find(x => x.Programcode.Equals(authenticate.ProgramCode));
                
                if (webBotHSMSetting != null)
                {
                    webBotcontentRequest.webBotHSMSetting = new WebBotHSMSetting();
                    if (!string.IsNullOrEmpty(webBotHSMSetting.Programcode))
                    {
                        webBotcontentRequest.MaxWebBotHSMURL = webBotcontentRequest.MaxWebBotHSMURL + "?bot=" + webBotHSMSetting.bot;
                        webBotcontentRequest.webBotHSMSetting = webBotHSMSetting;
                        MaxWebBotHSMRequest maxreq = new MaxWebBotHSMRequest();
                        Hsm hsmReq = new Hsm()
                        {
                            @namespace = webBotHSMSetting.@namespace,
                            language = new Language() { policy = _policy, code = _code }
                        };
                        Body body = new Body()
                        {
                            // from = webBotcontentRequest.WABANo,// _from,
                            ttl = Convert.ToInt32(_ttl),
                            type = _type,
                            hsm = hsmReq
                        };
                        maxreq.body = body;

                        webBotcontentRequest.MaxHSMRequest = maxreq;
                    }
                }
                insertCount = await hSOrderCaller.InsertOrderDetails(new HSOrderService(_connectionString, _OrderURLList, _clienthttpclientservice), convertToOrder, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, _ClientAPIUrl, webBotcontentRequest);
                statusCode =
                   insertCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
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
        /// Update Address Pending
        /// </summary>
        /// <param name="addressPendingRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAddressPending")]
        public async Task<ResponseModel> UpdateAddressPending([FromBody]AddressPendingRequest addressPendingRequest)
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                updateCount = await hSOrderCaller.UpdateAddressPending(new HSOrderService(_connectionString), addressPendingRequest, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Get Order Return Details
        /// </summary>
        /// <param name="orderReturnsFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderReturnDetails")]
        public async Task<ResponseModel> GetOrderReturnDetails(OrderReturnsFilterRequest orderReturnsFilter)
        {
            OrderReturnsDetails orderReturns = new OrderReturnsDetails();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                orderReturns = await hSOrderCaller.GetOrderReturnDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, orderReturnsFilter);
                statusCode =
                   orderReturns.TotalCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderReturns;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Shipment Assigned Delivered
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentAssignedDelivered")]
        public async Task<ResponseModel> UpdateShipmentAssignedDelivered(int orderID)
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                updateCount = await hSOrderCaller.UpdateShipmentAssignedDelivered(new HSOrderService(_connectionString), orderID);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Update Shipment Assigned RTO
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentAssignedRTO")]
        public async Task<ResponseModel> UpdateShipmentAssignedRTO(int orderID)
        {
            int updateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                updateCount = await hSOrderCaller.UpdateShipmentAssignedRTO(new HSOrderService(_connectionString), orderID);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Shipment Assigned Print Manifest
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShipmentAssignedPrintManifest")]
        public async Task<ResponseModel> ShipmentAssignedPrintManifest(int OrderIds)
        {
            ResponseModel objResponseModel = new ResponseModel();
            PrintManifestResponse printManifest = new PrintManifestResponse();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                HSOrderCaller hSOrderCaller = new HSOrderCaller();
                printManifest = await hSOrderCaller.ShipmentAssignedPrintManifest(new HSOrderService(_connectionString, _OrderURLList, _clienthttpclientservice, null, _phygitalhttpclientservice), OrderIds, _PhygitalClientAPIURL);
                statusCode = printManifest.manifestUrl.Length > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = printManifest;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Shipment Assigned Print Label
        /// </summary>
        /// <param name="ShipmentId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShipmentAssignedPrintLabel")]
        public async Task<ResponseModel> ShipmentAssignedPrintLabel(int ShipmentId)
        {
            ResponseModel objResponseModel = new ResponseModel();
            PrintLabelResponse printLabel = new PrintLabelResponse();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                HSOrderCaller hSOrderCaller = new HSOrderCaller();
                printLabel = await hSOrderCaller.ShipmentAssignedPrintLabel(new HSOrderService(_connectionString, _OrderURLList, _clienthttpclientservice, null, _phygitalhttpclientservice), ShipmentId, _PhygitalClientAPIURL);
                statusCode = string.IsNullOrEmpty(printLabel.label_url)?  (int)EnumMaster.StatusCode.RecordNotFound :  printLabel.label_url.Length > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = printLabel;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShipmentAssignedPrintInvoice")]
        public async Task<ResponseModel> ShipmentAssignedPrintInvoice(int OrderIds)
        {
            ResponseModel objResponseModel = new ResponseModel();
            PrintInvoiceResponse printInvoice = new PrintInvoiceResponse();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                HSOrderCaller hSOrderCaller = new HSOrderCaller();
                printInvoice = await hSOrderCaller.ShipmentAssignedPrintInvoice(new HSOrderService(_connectionString, _OrderURLList, _clienthttpclientservice, null, _phygitalhttpclientservice), OrderIds, _PhygitalClientAPIURL);
                statusCode = printInvoice.is_invoice_created ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = printInvoice;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendSMSWhatsupOnReturnCancel")]
        public async Task<ResponseModel> SendSMSWhatsupOnReturnCancel(int OrderId)
        {
            ResponseModel objResponseModel = new ResponseModel();

            int statusCode = 0;
            string statusMessage = "";
            int updateCount = 0;
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                WebBotContentRequest webBotcontentRequest = new WebBotContentRequest
                {
                    MakeBellActiveUrl = _ClientAPIUrl + _makeBellActive,
                    ClientAPIUrl = _ClientAPIUrl + _sendCampaign,
                    WeBBotGenerationLink = _WebBotUrl + _getWebBotLink,
                    MaxWebBotHSMURL = _MaxHSMURL + _MaxWeBotHSM,
                    ProgramCode = authenticate.ProgramCode
                };

                WebBotHSMSetting webBotHSMSetting = new WebBotHSMSetting();
                webBotHSMSetting = _hsmProgramcode.Find(x => x.Programcode.Equals(authenticate.ProgramCode));
                
                if (webBotHSMSetting != null)
                {
                    webBotcontentRequest.webBotHSMSetting = new WebBotHSMSetting();
                    if (!string.IsNullOrEmpty(webBotHSMSetting.Programcode))
                    {
                        webBotcontentRequest.MaxWebBotHSMURL = webBotcontentRequest.MaxWebBotHSMURL + "?bot=" + webBotHSMSetting.bot;
                        webBotcontentRequest.webBotHSMSetting = webBotHSMSetting;
                        MaxWebBotHSMRequest maxreq = new MaxWebBotHSMRequest();
                        Hsm hsmReq = new Hsm()
                        {
                            @namespace = webBotHSMSetting.@namespace,
                            language = new Language() { policy = _policy, code = _code }
                        };
                        Body body = new Body()
                        {
                            // from = webBotcontentRequest.WABANo,// _from,
                            ttl = Convert.ToInt32(_ttl),
                            type = _type,
                            hsm = hsmReq
                        };
                        maxreq.body = body;

                        webBotcontentRequest.MaxHSMRequest = maxreq;
                    }
                }


                HSOrderCaller hSOrderCaller = new HSOrderCaller();
                updateCount = await hSOrderCaller.SendSMSWhatsupOnReturnCancel(new HSOrderService(_connectionString, _OrderURLList, _clienthttpclientservice), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, OrderId, _ClientAPIUrl, webBotcontentRequest);
                statusCode = updateCount.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateOnReturnRetry")]
        public async Task<ResponseModel> UpdateOnReturnRetry(int OrderId, int StatusId, string AWBNo, int ReturnId)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                HSOrderCaller hSOrderCaller = new HSOrderCaller();
                result =await hSOrderCaller.UpdateOnReturnRetry(new HSOrderService(_connectionString), OrderId, StatusId, AWBNo, ReturnId);
                statusCode = result.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Get Push Order to Poss data
        /// </summary>
        /// <param name="PushOrderToPoss"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PushOrderToPoss")]
        public async Task<ResponseModel> PushOrderToPoss([FromBody]ConvertToOrder PushToPoss)
        {
            MessageData messageData = new MessageData();
             HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                messageData = await hSOrderCaller.PushOrderToPoss(new HSOrderService(_connectionString, _OrderURLList, _clienthttpclientservice, null, _phygitalhttpclientservice), PushToPoss, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, _PhygitalClientAPIURL);
                statusCode =
                   messageData.resultCode.Equals("SUCCESS") ?
                           (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServiceNotWorking;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = messageData;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

    }
}