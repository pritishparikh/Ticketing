using System;
using System.Collections.Generic;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using System.Threading.Tasks;

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
        private readonly string _PhygitalClientAPIURL;
        private readonly string _Client_Id;
        private readonly string _Client_Secret;
        private readonly string _Grant_Type;
        private readonly string _Scope;
        private readonly string BulkUpload;
        private readonly string UploadFiles;
        private readonly string DownloadFile;
        private readonly string rootPath;
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
            _PhygitalClientAPIURL = configuration.GetValue<string>("PhygitalClientAPIURL");
            _Client_Id = configuration.GetValue<string>("Client_Id");
            _Client_Secret = configuration.GetValue<string>("Client_Secret");
            _Grant_Type = configuration.GetValue<string>("Grant_Type");
            _Scope = configuration.GetValue<string>("Scope");
            BulkUpload = configuration.GetValue<string>("BulkUpload");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            DownloadFile = configuration.GetValue<string>("Downloadfile");
            rootPath = configuration.GetValue<string>("APIURL");
        }
        #endregion


        /// <summary>
        /// Get Order Configuration Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderConfiguration")]
        public async  Task<ResponseModel> GetOrderConfiguration()
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

                orderConfiguration =await hSOrderCaller.GetOrderConfiguration(new HSOrderService(_connectionString),
                    authenticate.TenantId);
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
        public async Task<ResponseModel> UpdateOrderConfiguration([FromBody]OrderConfiguration orderConfiguration)
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

                UpdateCount = await hSOrderCaller.UpdateOrderConfiguration(new HSOrderService(_connectionString),
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
        /// Update Order Configuration Message Template
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateOrderConfigurationMessageTemplate")]
        public async Task<ResponseModel> UpdateOrderConfigurationMessageTemplate([FromBody]OrderConfiguration orderConfiguration)
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

                UpdateCount =await hSOrderCaller.UpdateOrderConfigurationMessageTemplate(new HSOrderService(_connectionString),
                    orderConfiguration.pHYOrderMessageTemplates, authenticate.TenantId);
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
        /// Get Whatsapp Template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWhatsappTemplate")]
        public async Task<ResponseModel> GetWhatsappTemplate(string MessageName)
        {
            List<PHYWhatsAppTemplate> pHYWhatsAppTemplates = new List<PHYWhatsAppTemplate>();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                pHYWhatsAppTemplates =await hSOrderCaller.GetWhatsappTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, MessageName);
                statusCode =
                   pHYWhatsAppTemplates.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = pHYWhatsAppTemplates;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Whatsapp Template
        /// </summary>
        /// <param name="templateDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateWhatsappTemplate")]
        public async Task<ResponseModel> UpdateWhatsappTemplate([FromBody]PHYWhatsAppTemplateDetails templateDetails)
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

                UpdateCount =await hSOrderCaller.UpdateWhatsappTemplate(new HSOrderService(_connectionString),
                    templateDetails.pHYWhatsAppTemplates, authenticate.TenantId);
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
        public async Task<ResponseModel> GetOrderDeliveredDetails(OrderDeliveredFilterRequest orderDeliveredFilter)
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

                orderDelivereds = await hSOrderCaller.GetOrderDeliveredDetails(new HSOrderService(_connectionString),
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
        public async Task<ResponseModel> GetOrderStatusFilter(int pageID)
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

                orderStatusFilter =await hSOrderCaller.GetOrderStatusFilter(new HSOrderService(_connectionString),
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
        public async Task<ResponseModel> GetShipmentAssignedDetails(ShipmentAssignedFilterRequest shipmentAssignedFilter)
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

                assignedDetails =await hSOrderCaller.GetShipmentAssignedDetails(new HSOrderService(_connectionString),
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


        /// <summary>
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="shipmentAssignedRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShipmentAssignedData")]
        public async Task<ResponseModel> UpdateShipmentAssignedData([FromBody]ShipmentAssignedRequest shipmentAssignedRequest)
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

                UpdateCount =await hSOrderCaller.UpdateShipmentAssignedData(new HSOrderService(_connectionString), shipmentAssignedRequest,authenticate.TenantId,authenticate.UserMasterID,authenticate.ProgramCode, _ClientAPIUrl);
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
        public async Task<ResponseModel> UpdateShipmentBagCancelData(int ShoppingID, string CancelComment)
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

                UpdateCount =await hSOrderCaller.UpdateShipmentBagCancelData(new HSOrderService(_connectionString), ShoppingID, CancelComment, authenticate.UserMasterID);
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
        public async Task<ResponseModel> UpdateShipmentPickupPendingData(int OrderID)
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

                UpdateCount =await hSOrderCaller.UpdateShipmentPickupPendingData(new HSOrderService(_connectionString), OrderID);
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
        public async Task<ResponseModel> InsertOrderDetails([FromBody]ConvertToOrder convertToOrder)
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

                InsertCount =await hSOrderCaller.InsertOrderDetails(new HSOrderService(_connectionString), convertToOrder, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, _ClientAPIUrl);
                statusCode =
                   InsertCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;

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
        public async Task<ResponseModel> UpdateAddressPending([FromBody]AddressPendingRequest addressPendingRequest)
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

                UpdateCount =await hSOrderCaller.UpdateAddressPending(new HSOrderService(_connectionString), addressPendingRequest, authenticate.TenantId, authenticate.UserMasterID);
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
        public async Task<ResponseModel> GetOrderReturnDetails(OrderReturnsFilterRequest orderReturnsFilter)
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

                orderReturns = await hSOrderCaller.GetOrderReturnDetails(new HSOrderService(_connectionString),
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
        public async Task<ResponseModel> UpdateShipmentAssignedDelivered(int orderID)
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

                UpdateCount =await hSOrderCaller.UpdateShipmentAssignedDelivered(new HSOrderService(_connectionString), orderID);
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
        public async Task<ResponseModel> UpdateShipmentAssignedRTO(int orderID)
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

                UpdateCount =await hSOrderCaller.UpdateShipmentAssignedRTO(new HSOrderService(_connectionString), orderID);
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
        public ResponseModel ShipmentAssignedPrintManifest(int OrderIds)
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

                printManifest = hSOrderCaller.ShipmentAssignedPrintManifest(new HSOrderService(_connectionString), OrderIds, _PhygitalClientAPIURL);
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
        public ResponseModel ShipmentAssignedPrintLabel(int ShipmentId)
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

                printLabel = hSOrderCaller.ShipmentAssignedPrintLabel(new HSOrderService(_connectionString), ShipmentId, _PhygitalClientAPIURL);
                statusCode = string.IsNullOrEmpty(printLabel.label_url)?  (int)EnumMaster.StatusCode.RecordNotFound :  printLabel.label_url.Length > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShipmentAssignedPrintInvoice")]
        public ResponseModel ShipmentAssignedPrintInvoice(int OrderIds)
        {
            ResponseModel objResponseModel = new ResponseModel();
            PrintInvoiceResponse printInvoice = new PrintInvoiceResponse();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                HSOrderCaller hSOrderCaller = new HSOrderCaller();

                printInvoice = hSOrderCaller.ShipmentAssignedPrintInvoice(new HSOrderService(_connectionString), OrderIds, _PhygitalClientAPIURL);
                statusCode = printInvoice.is_invoice_created ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = printInvoice;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendSMSWhatsupOnReturnCancel")]
        public async Task<ResponseModel> SendSMSWhatsupOnReturnCancel(int OrderId)
        {
            ResponseModel objResponseModel = new ResponseModel();

            int statusCode = 0;
            string statusMessage = "";
            int UpdateCount = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                HSOrderCaller hSOrderCaller = new HSOrderCaller();

                UpdateCount =await hSOrderCaller.SendSMSWhatsupOnReturnCancel(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, OrderId, _ClientAPIUrl);
                statusCode = UpdateCount.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateOnReturnRetry")]
        public async Task<ResponseModel> UpdateOnReturnRetry(int OrderId, int StatusId, string AWBNo, int ReturnId)
        {
            ResponseModel objResponseModel = new ResponseModel();

            int statusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                HSOrderCaller hSOrderCaller = new HSOrderCaller();

                result =await hSOrderCaller.UpdateOnReturnRetry(new HSOrderService(_connectionString), OrderId, StatusId, AWBNo, ReturnId);
                statusCode = result.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
    }
}