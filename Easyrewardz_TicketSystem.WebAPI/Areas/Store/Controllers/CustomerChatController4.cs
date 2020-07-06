using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    public partial class CustomerChatController : ControllerBase
    {
        #region Custom Methods

        /// <summary>
        /// Get Chat Customer Profile Details
        /// <param name="CustomerID"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatCustomerProfile")]
        public ResponseModel GetChatCustomerProfile(int CustomerID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            ChatCustomerProfileModel CustomerProfile = new ChatCustomerProfileModel();

            int statusCode = 0;
            string statusMessage = "";
            string SoundPhysicalFilePath = string.Empty;
            string SoundFilePath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                CustomerProfile = customerChatCaller.GetChatCustomerProfileDetails(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID,authenticate.UserMasterID,_ClientAPIUrl);
                statusCode = CustomerProfile.CustomerID > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CustomerProfile;
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
