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
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }

        #endregion

        #region constructor
        public PaymentController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;

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
            PaymentCaller newPaymentCaller = new PaymentCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //authenticate.TenantId = 0;
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                offlinePaymentModel.CreatedBy = authenticate.UserMasterID;
                offlinePaymentModel.TenantID = authenticate.TenantId;

                int result = newPaymentCaller.InsertChequeDetails(new PaymentService(Cache, Db), offlinePaymentModel);

                statusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;

            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }
        #endregion

        #endregion
    }
}