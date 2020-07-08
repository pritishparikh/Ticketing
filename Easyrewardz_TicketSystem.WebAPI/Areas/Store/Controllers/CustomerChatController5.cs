using System;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    
    public partial class CustomerChatController : ControllerBase
    {
        #region Custom Methods
        /// <summary>
        /// Save Re-Initiate Chat 
        /// </summary>
        /// <param name="customerChatMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveReInitiateChat")]
        public ResponseModel saveReInitiateChat([FromBody]  CustomerChatMaster customerChatMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();
                customerChatMaster.CreatedBy = authenticate.UserMasterID;
                result = customerChatCaller.SaveReInitiateChat(new CustomerChatService(_connectionString),authenticate.TenantId, customerChatMaster);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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