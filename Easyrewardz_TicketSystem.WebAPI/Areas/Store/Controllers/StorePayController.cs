using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.WebAPI.Provider.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

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
        #endregion

        #region Cunstructor
        public StorePayController(IConfiguration _iConfig)
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

        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// Generate Store Pay Link
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GenerateStorePayLink")]
        public ResponseModel GenerateStorePayLink()
        {
            string StorePayLink = string.Empty;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StorePayCaller newStorePay = new StorePayCaller();

                HSRequestGenerateToken hSRequestGenerateToken = new HSRequestGenerateToken()
                {
                    Client_Id = _Client_Id,
                    Client_Secret = _Client_Secret,
                    Grant_Type = _Grant_Type,
                    Scope = _Scope,
                };

                StorePayLink = newStorePay.GenerateStorePayLink(new StorePayService(_connectioSting), authenticate.TenantId, authenticate.ProgramCode,authenticate.UserMasterID,
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
