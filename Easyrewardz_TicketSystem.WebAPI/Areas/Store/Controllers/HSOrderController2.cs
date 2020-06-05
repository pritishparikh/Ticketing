using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{

    public partial class HSOrderController : ControllerBase
    {
        /// <summary>
        /// GetOrdersDetails
        /// </summary>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrdersDetails")]
        public ResponseModel GetOrdersDetails(OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails orderResponseDetails = new OrderResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderResponseDetails = storecampaigncaller.GetOrdersDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, ordersDataRequest);
                statusCode =
                   orderResponseDetails.TotalCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderResponseDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
    }
}