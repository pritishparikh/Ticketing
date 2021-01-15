using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class NotificationController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string connectioSting;
        private readonly string radisCacheServerAddress;
        #endregion

        #region Constructor
        public NotificationController(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get Notification
        /// </summary>
        [HttpPost]
        [Route("GetNotifications")]
        public ResponseModel GetNotifications()
        {

            ResponseModel objResponseModel = new ResponseModel();
           NotificationModel objresponseModel = new NotificationModel(); 
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                //Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                NotificationCaller _newNoti = new NotificationCaller();

                objresponseModel = _newNoti.GetNotification(new NotificationService(connectioSting), authenticate.TenantId, authenticate.UserMasterID);
                StatusCode = objresponseModel.TicketNotification.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Read Notification
        /// <param name="TicketID"></param>
        /// <param name="IsFollowUp"></param> 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ReadNotification")]
        public ResponseModel ReadNotification(int TicketID,int IsFollowUp = 0)
        {
            int updatecount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                NotificationCaller _newNoti = new NotificationCaller();

                updatecount = _newNoti.ReadNotification(new NotificationService(connectioSting), authenticate.TenantId, authenticate.UserMasterID, TicketID, IsFollowUp);

                StatusCode = updatecount > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServiceNotWorking;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updatecount;

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
