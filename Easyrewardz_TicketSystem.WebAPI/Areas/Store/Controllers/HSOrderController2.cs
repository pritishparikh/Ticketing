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

        /// <summary>
        /// GetShoppingBagDetails
        /// </summary>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetShoppingBagDetails")]
        public ResponseModel GetShoppingBagDetails(OrdersDataRequest ordersDataRequest)
        {
            ShoppingBagResponseDetails shoppingBagResponseDetails = new ShoppingBagResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shoppingBagResponseDetails = storecampaigncaller.GetShoppingBagDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, ordersDataRequest);
                statusCode =
                   shoppingBagResponseDetails.TotalShoppingBag.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shoppingBagResponseDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetShoppingBagDeliveryType
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetShoppingBagDeliveryType")]
        public ResponseModel GetShoppingBagDeliveryType(int pageID)
        {
            List<ShoppingBagDeliveryFilter> shoppingBagDeliveryFilter = new List<ShoppingBagDeliveryFilter>();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shoppingBagDeliveryFilter = storecampaigncaller.GetShoppingBagDeliveryType(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, pageID);
                statusCode =
                   shoppingBagDeliveryFilter.Count.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shoppingBagDeliveryFilter;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetShipmentDetails
        /// </summary>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetShipmentDetails")]
        public ResponseModel GetShipmentDetails(OrdersDataRequest ordersDataRequest)
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

                orderResponseDetails = storecampaigncaller.GetShipmentDetails(new HSOrderService(_connectionString),
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

        /// <summary>
        /// GetOrderTabSettingDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderTabSettingDetails")]
        public ResponseModel GetOrderTabSettingDetails()
        {
            OrderTabSetting orderResponseDetails = new OrderTabSetting();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderResponseDetails = storecampaigncaller.GetOrderTabSettingDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   orderResponseDetails.Exists.Equals(0) ?
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

        /// <summary>
        /// SetOrderHasBeenReturn
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetOrderHasBeenReturn")]
        public ResponseModel SetOrderHasBeenReturn(int orderID)
        {
            int UpdateCount = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = storecampaigncaller.SetOrderHasBeenReturn(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, orderID);
                statusCode =
                   UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetOrderShippingTemplate
        /// </summary>
        /// <param name="shippingTemplateReques"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderShippingTemplate")]
        public ResponseModel GetOrderShippingTemplate(ShippingTemplateRequest shippingTemplateReques)
        {
            ShippingTemplateDetails shippingTemplateDetails = new ShippingTemplateDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shippingTemplateDetails = storecampaigncaller.GetOrderShippingTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, shippingTemplateReques);
                statusCode =
                   shippingTemplateDetails.ShippingTemplateList.Count < 1 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shippingTemplateDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// InsertUpdateOrderShippingTemplate
        /// </summary>
        /// <param name="addEditShippingTemplate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertUpdateOrderShippingTemplate")]
        public ResponseModel InsertUpdateOrderShippingTemplate(AddEditShippingTemplate addEditShippingTemplate)
        {
            int result = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                result = storecampaigncaller.InsertUpdateOrderShippingTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, addEditShippingTemplate);
                statusCode =
                   result.Equals(-1) ?
                           (int)EnumMaster.StatusCode.RecordAlreadyExists :
                           result.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound :
                           (int)EnumMaster.StatusCode.Success;

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
        /// GetOrderShippingTemplateName
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderShippingTemplateName")]
        public ResponseModel GetOrderShippingTemplateName()
        {
            ShippingTemplateDetails shippingTemplateDetails = new ShippingTemplateDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shippingTemplateDetails = storecampaigncaller.GetOrderShippingTemplateName(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   shippingTemplateDetails.ShippingTemplateList.Count < 1 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shippingTemplateDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// SetOrderHasBeenSelfPickUp
        /// </summary>
        /// <param name="orderSelfPickUp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetOrderHasBeenSelfPickUp")]
        public ResponseModel SetOrderHasBeenSelfPickUp([FromBody]OrderSelfPickUp orderSelfPickUp)
        {
            int UpdateCount = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = storecampaigncaller.SetOrderHasBeenSelfPickUp(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, orderSelfPickUp);
                statusCode =
                   UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetCourierPartnerFilter
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCourierPartnerFilter")]
        public ResponseModel GetCourierPartnerFilter(int pageID)
        {
            List<string> CourierPartnerFilter = new List<string>();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CourierPartnerFilter = storecampaigncaller.GetCourierPartnerFilter(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, pageID);
                statusCode =
                   CourierPartnerFilter.Count.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CourierPartnerFilter;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
    }
}