using ClientAPIServiceCall;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.WebAPI.Provider.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StorePayController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string _ClientAPIUrlForGenerateToken;
        private readonly string _ClientAPIUrlForGeneratePaymentLink;
        private readonly string _Client_Id;
        private readonly string _Client_Secret;
        private readonly string _Grant_Type;
        private readonly string _Scope;

        private readonly string _SHAHash;
        private readonly string _EpochTime;
        private readonly string _AESEncrypt;
        private readonly string _tokenGeneration;


        private readonly GeneratePaymentLinkHttpClientService _generatePaymentLinkHttpClientService; 
        private readonly GenerateToken _generateToken;
        private readonly ILogger<StorePayController> _logger;
        #endregion

        #region Cunstructor
        public StorePayController(IConfiguration _iConfig, GeneratePaymentLinkHttpClientService generatePaymentLinkHttpClientService, GenerateToken generateToken,
            ILogger<StorePayController> logger)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _ClientAPIUrlForGenerateToken = configuration.GetValue<string>("ClientAPIForGenerateToken");
            _ClientAPIUrlForGeneratePaymentLink = configuration.GetValue<string>("ClientAPIForGeneratePaymentLink");
            _Client_Id = configuration.GetValue<string>("Client_Id");
            _Client_Secret = configuration.GetValue<string>("Client_Secret");
            _Grant_Type = configuration.GetValue<string>("Grant_Type");
            _Scope = configuration.GetValue<string>("Scope");

            _SHAHash = configuration.GetValue<string>("ClientAPI:SHAHash");
            _EpochTime = configuration.GetValue<string>("ClientAPI:EpochTime");
            _AESEncrypt = configuration.GetValue<string>("ClientAPI:AESEncrypt");
            _tokenGeneration = configuration.GetValue<string>("GenerateTokenAPI:token");

            _generatePaymentLinkHttpClientService = generatePaymentLinkHttpClientService;
            _generateToken = generateToken;
            _logger = logger;

        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// Generate Store Pay Link
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GenerateStorePayLink")]
        public async Task<ResponseModel> GenerateStorePayLink()
        {
            string StorePayLink = string.Empty;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StorePayCaller newStorePay = new StorePayCaller();

                HSRequestGenerateToken hSRequestGenerateToken = new HSRequestGenerateToken()
                {
                    Client_Id = _Client_Id,
                    Client_Secret = _Client_Secret,
                    Grant_Type = _Grant_Type,
                    Scope = _Scope,

                    SHAHash = _SHAHash,
                    EpochTime = _EpochTime,
                    AESEncrypt = _AESEncrypt,
                    tokenGeneration = _tokenGeneration,
                };

                StorePayLink = await newStorePay.GenerateStorePayLink(new StorePayService(_connectioSting, _generatePaymentLinkHttpClientService,_generateToken, _logger),
                    authenticate.TenantId, authenticate.ProgramCode, authenticate.UserMasterID,
                    _ClientAPIUrlForGenerateToken, _ClientAPIUrlForGeneratePaymentLink, hSRequestGenerateToken);

                statusCode =
                string.IsNullOrEmpty(StorePayLink) ? 
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = StorePayLink;

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
