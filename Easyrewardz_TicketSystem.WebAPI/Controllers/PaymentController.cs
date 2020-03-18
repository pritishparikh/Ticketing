using System;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class PaymentController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }

        #endregion

        #region constructor
        public PaymentController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;

        }
        #endregion


        #region Methods 

        #region Insert Cheque Details
        /// <summary>
        /// Insert cheque details
        /// </summary>
        /// <param name="offlinePaymentModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertChequeDetails")]
        public ResponseModel InsertChequeDetails([FromBody] OfflinePaymentModel offlinePaymentModel)
        {
            PaymentCaller _newPaymentCaller = new PaymentCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //authenticate.TenantId = 0;
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                offlinePaymentModel.CreatedBy = authenticate.UserMasterID;
                offlinePaymentModel.TenantID = authenticate.TenantId;

                int result = _newPaymentCaller.InsertChequeDetails(new PaymentService(_Cache, Db), offlinePaymentModel);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;

            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }
        #endregion

        #endregion
    }
}