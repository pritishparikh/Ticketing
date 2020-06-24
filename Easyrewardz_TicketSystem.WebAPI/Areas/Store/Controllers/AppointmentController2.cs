using System;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public partial class AppointmentController : ControllerBase
    {
        /// <summary>
        /// GetStoreDetailsByStoreCode
        /// </summary>
        /// <param name="storeCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreDetailsByStoreCode")]
        public ResponseModel GetStoreDetailsByStoreCode(string storeCode)
        {
            StoreDetails storeDetails = new StoreDetails();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                storeDetails = newAppointment.GetStoreDetailsByStoreCode(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, storeCode);

                statusCode =
                string.IsNullOrEmpty(storeDetails.StoreName) ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = storeDetails;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
    }
}