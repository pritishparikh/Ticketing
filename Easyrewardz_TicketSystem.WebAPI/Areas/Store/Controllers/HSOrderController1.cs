using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public partial class HSOrderController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionString;
        private readonly string _radisCacheServerAddress;
        private readonly string _ClientAPIUrl;
        private readonly string _ClientAPIUrlForGenerateToken;
        private readonly string _ClientAPIUrlForGeneratePaymentLink;
        private readonly string _Client_Id;
        private readonly string _Client_Secret;
        private readonly string _Grant_Type;
        private readonly string _Scope;
        #endregion

        #region Constructor
        public HSOrderController(IConfiguration iConfig)
        {
            configuration = iConfig;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _ClientAPIUrl = configuration.GetValue<string>("ClientAPIURL");
            _ClientAPIUrlForGenerateToken = configuration.GetValue<string>("ClientAPIForGenerateToken");
            _ClientAPIUrlForGeneratePaymentLink = configuration.GetValue<string>("ClientAPIForGeneratePaymentLink");
            _Client_Id = configuration.GetValue<string>("Client_Id");
            _Client_Secret = configuration.GetValue<string>("Client_Secret");
            _Grant_Type = configuration.GetValue<string>("Grant_Type");
            _Scope = configuration.GetValue<string>("Scope");
        }
        #endregion


        //[HttpPost]
        //[Route("GetModuleConfiguration")]
        //public ResponseModel GetModuleConfiguration()
        //{
        //    ModuleConfiguration moduleConfiguration = new ModuleConfiguration();
        //    HSOrderCaller storecampaigncaller = new HSOrderCaller();
        //    ResponseModel objResponseModel = new ResponseModel();
        //    int statusCode = 0;
        //    string statusMessage = "";
        //    try
        //    {
        //        string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
        //        Authenticate authenticate = new Authenticate();
        //        authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

        //        moduleConfiguration = storecampaigncaller.GetModuleConfiguration(new HSOrderService(_connectionString),
        //            authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
        //        statusCode =
        //           moduleConfiguration.ID.Equals(0) ?
        //                   (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

        //        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

        //        objResponseModel.Status = true;
        //        objResponseModel.StatusCode = statusCode;
        //        objResponseModel.Message = statusMessage;
        //        objResponseModel.ResponseData = moduleConfiguration;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return objResponseModel;
        //}


        //[HttpPost]
        //[Route("UpdateModuleConfiguration")]
        //public ResponseModel UpdateModuleConfiguration([FromBody]ModuleConfiguration moduleConfiguration)
        //{
        //    int UpdateCount = 0;
        //    HSOrderCaller hSOrderCaller = new HSOrderCaller();
        //    ResponseModel objResponseModel = new ResponseModel();
        //    int statusCode = 0;
        //    string statusMessage = "";
        //    try
        //    {
        //        string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
        //        Authenticate authenticate = new Authenticate();
        //        authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

        //        UpdateCount = hSOrderCaller.UpdateModuleConfiguration(new HSOrderService(_connectionString),
        //            moduleConfiguration, authenticate.UserMasterID);
        //        statusCode =
        //           UpdateCount.Equals(0) ?
        //                   (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

        //        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

        //        objResponseModel.Status = true;
        //        objResponseModel.StatusCode = statusCode;
        //        objResponseModel.Message = statusMessage;
        //        objResponseModel.ResponseData = UpdateCount;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return objResponseModel;
        //}

        /// <summary>
        /// Get Order Configuration Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderConfiguration")]
        public ResponseModel GetOrderConfiguration()
        {
            OrderConfiguration orderConfiguration = new OrderConfiguration();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderConfiguration = hSOrderCaller.GetOrderConfiguration(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   orderConfiguration.ID.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderConfiguration;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Order Configuration Data
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateOrderConfiguration")]
        public ResponseModel UpdateOrderConfiguration([FromBody]OrderConfiguration orderConfiguration)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateOrderConfiguration(new HSOrderService(_connectionString),
                    orderConfiguration, authenticate.UserMasterID);
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
        /// Get Order Delivered Details
        /// </summary>
        /// <param name="orderDeliveredFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderDeliveredDetails")]
        public ResponseModel GetOrderDeliveredDetails(OrderDeliveredFilterRequest orderDeliveredFilter)
        {
            OrderDeliveredDetails orderDelivereds = new OrderDeliveredDetails();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderDelivereds = hSOrderCaller.GetOrderDeliveredDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, orderDeliveredFilter);
                statusCode =
                   orderDelivereds.TotalCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderDelivereds;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Order Status Filters
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderStatusFilter")]
        public ResponseModel GetOrderStatusFilter(int pageID)
        {
            List<OrderStatusFilter> orderStatusFilter = new List<OrderStatusFilter>();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderStatusFilter = hSOrderCaller.GetOrderStatusFilter(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, pageID);
                statusCode =
                   orderStatusFilter.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderStatusFilter;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Order Shipment Assigned Details
        /// </summary>
        /// <param name="shipmentAssignedFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetShipmentAssignedDetails")]
        public ResponseModel GetShipmentAssignedDetails(ShipmentAssignedFilterRequest shipmentAssignedFilter)
        {
            ShipmentAssignedDetails assignedDetails = new ShipmentAssignedDetails();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                assignedDetails = hSOrderCaller.GetShipmentAssignedDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, shipmentAssignedFilter);
                statusCode =
                   assignedDetails.TotalCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = assignedDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        //[HttpPost]
        //[Route("UpdateMarkAsDelivered")]
        //public ResponseModel UpdateMarkAsDelivered(int orderID)
        //{
        //    int UpdateCount = 0;
        //    HSOrderCaller hSOrderCaller = new HSOrderCaller();
        //    ResponseModel objResponseModel = new ResponseModel();
        //    int statusCode = 0;
        //    string statusMessage = "";
        //    try
        //    {
        //        string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
        //        Authenticate authenticate = new Authenticate();
        //        authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

        //        UpdateCount = hSOrderCaller.UpdateMarkAsDelivered(new HSOrderService(_connectionString),
        //            authenticate.TenantId, authenticate.UserMasterID, orderID);
        //        statusCode =
        //           UpdateCount.Equals(0) ?
        //                   (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

        //        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

        //        objResponseModel.Status = true;
        //        objResponseModel.StatusCode = statusCode;
        //        objResponseModel.Message = statusMessage;
        //        objResponseModel.ResponseData = UpdateCount;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return objResponseModel;
        //}

        /// <summary>
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="shipmentAssignedRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentAssignedData")]
        public ResponseModel UpdateShipmentAssignedData([FromBody]ShipmentAssignedRequest shipmentAssignedRequest)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateShipmentAssignedData(new HSOrderService(_connectionString), shipmentAssignedRequest,authenticate.TenantId,authenticate.UserMasterID,authenticate.ProgramCode, _ClientAPIUrl);
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
        /// Update Shopping Bag Cancel Data 
        /// </summary>
        /// <param name="ShoppingID"></param>
        /// <param name="CancelComment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentBagCancelData")]
        public ResponseModel UpdateShipmentBagCancelData(int ShoppingID, string CancelComment)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateShipmentBagCancelData(new HSOrderService(_connectionString), ShoppingID, CancelComment, authenticate.UserMasterID);
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
        /// Update Shipment Pickup Pending Data
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentPickupPendingData")]
        public ResponseModel UpdateShipmentPickupPendingData(int OrderID)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateShipmentPickupPendingData(new HSOrderService(_connectionString), OrderID);
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
        /// Insert Convert To Order Details
        /// </summary>
        /// <param name="convertToOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertOrderDetails")]
        public ResponseModel InsertOrderDetails([FromBody]ConvertToOrder convertToOrder)
        {
            int InsertCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                InsertCount = hSOrderCaller.InsertOrderDetails(new HSOrderService(_connectionString), convertToOrder, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, _ClientAPIUrl);
                statusCode =
                   InsertCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = InsertCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Address Pending
        /// </summary>
        /// <param name="addressPendingRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAddressPending")]
        public ResponseModel UpdateAddressPending([FromBody]AddressPendingRequest addressPendingRequest)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateAddressPending(new HSOrderService(_connectionString), addressPendingRequest, authenticate.TenantId, authenticate.UserMasterID);
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
        /// Get Order Return Details
        /// </summary>
        /// <param name="orderReturnsFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderReturnDetails")]
        public ResponseModel GetOrderReturnDetails(OrderReturnsFilterRequest orderReturnsFilter)
        {
            OrderReturnsDetails orderReturns = new OrderReturnsDetails();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderReturns = hSOrderCaller.GetOrderReturnDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, orderReturnsFilter);
                statusCode =
                   orderReturns.TotalCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderReturns;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Shipment Assigned Delivered
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentAssignedDelivered")]
        public ResponseModel UpdateShipmentAssignedDelivered(int orderID)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateShipmentAssignedDelivered(new HSOrderService(_connectionString), orderID);
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
        /// Update Shipment Assigned RTO
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentAssignedRTO")]
        public ResponseModel UpdateShipmentAssignedRTO(int orderID)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateShipmentAssignedRTO(new HSOrderService(_connectionString), orderID);
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
        /// Shipment Assigned Print Manifest
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShipmentAssignedPrintManifest")]
        public ResponseModel ShipmentAssignedPrintManifest(Int64 OrderIds)
        {
            ResponseModel objResponseModel = new ResponseModel();
            PrintManifestResponse printManifest = new PrintManifestResponse();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                HSOrderCaller hSOrderCaller = new HSOrderCaller();

                printManifest = hSOrderCaller.ShipmentAssignedPrintManifest(new HSOrderService(_connectionString), OrderIds, _ClientAPIUrl);
                statusCode = printManifest.manifestUrl.Length > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = printManifest;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Shipment Assigned Print Label
        /// </summary>
        /// <param name="ShipmentId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShipmentAssignedPrintLabel")]
        public ResponseModel ShipmentAssignedPrintLabel(Int64 ShipmentId)
        {
            ResponseModel objResponseModel = new ResponseModel();
            PrintLabelResponse printLabel = new PrintLabelResponse();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                HSOrderCaller hSOrderCaller = new HSOrderCaller();

                printLabel = hSOrderCaller.ShipmentAssignedPrintLabel(new HSOrderService(_connectionString), ShipmentId, _ClientAPIUrl);
                statusCode = printLabel.label_url.Length > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = printLabel;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

    }
}