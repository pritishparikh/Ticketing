using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class HSOrderCaller
    {
        public IHSOrder _OrderRepository;

        /// <summary>
        /// Get Order Configuration
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<OrderConfiguration> GetOrderConfiguration(IHSOrder order, int tenantId)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderConfiguration(tenantId);
        }

        /// <summary>
        /// Update Order Configuration
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateOrderConfiguration(IHSOrder order, OrderConfiguration orderConfiguration, int ModifiedBy)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateOrderConfiguration(orderConfiguration, ModifiedBy);
        }


        /// <summary>
        /// Update Order Configuration Message Template
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pHYOrderMessageTemplates"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<int> UpdateOrderConfigurationMessageTemplate(IHSOrder order, List<PHYOrderMessageTemplate> pHYOrderMessageTemplates, int tenantId)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateOrderConfigurationMessageTemplate(pHYOrderMessageTemplates, tenantId);
        }

        /// <summary>
        /// Get Whatsapp Template
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userID"></param>
        ///  <param name="messageName"></param>
        /// <returns></returns>
        public async Task<List<PHYWhatsAppTemplate>> GetWhatsappTemplate(IHSOrder Order, int tenantId, int userID, string messageName)
        {
            _OrderRepository = Order;
            return await _OrderRepository.GetWhatsappTemplate(tenantId, userID, messageName);
        }

        /// <summary>
        /// Update Whatsapp Template
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pHYOrderMessageTemplates"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<int> UpdateWhatsappTemplate(IHSOrder order, List<PHYWhatsAppTemplate> pHYWhatsAppTemplates, int tenantId)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateWhatsappTemplate(pHYWhatsAppTemplates, tenantId);
        }

        /// <summary>
        /// Get Order Delivered Details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userID"></param>
        /// <param name="orderDeliveredFilter"></param>
        /// <returns></returns>
        public async Task<OrderDeliveredDetails> GetOrderDeliveredDetails(IHSOrder order, int tenantID, int userID, OrderDeliveredFilterRequest orderDeliveredFilter)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderDeliveredDetails(tenantID, userID, orderDeliveredFilter);
        }

        /// <summary>
        /// Get Order Status Filters
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public async Task<List<OrderStatusFilter>> GetOrderStatusFilter(IHSOrder order, int tenantID, int userID, int pageID)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderStatusFilter(tenantID, userID, pageID);
        }

        /// <summary>
        /// Get Order Shipment Assigned Details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shipmentAssignedFilter"></param>
        /// <returns></returns>
        public async Task<ShipmentAssignedDetails> GetShipmentAssignedDetails(IHSOrder order, int tenantID, int userID, ShipmentAssignedFilterRequest shipmentAssignedFilter)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetShipmentAssignedDetails(tenantID, userID, shipmentAssignedFilter);
        }

        /// <summary>
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="order"></param>
        /// <param name="shipmentAssignedRequest"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programCode"></param>
        /// <param name="clientAPIUrl"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedData(IHSOrder order, ShipmentAssignedRequest shipmentAssignedRequest, int tenantID, int userID,string programCode, string clientAPIUrl, WebBotContentRequest webBotcontentRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentAssignedData(shipmentAssignedRequest, tenantID, userID, programCode, clientAPIUrl, webBotcontentRequest);
        }

        /// <summary>
        /// Update Shopping Bag Cancel Data
        /// </summary>
        /// <param name="order"></param>
        /// <param name="shoppingID"></param>
        /// <param name="cancelComment"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentBagCancelData(IHSOrder order, int shoppingID, string cancelComment, int userID, bool sharewithCustomer, WebBotContentRequest webBotcontentRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentBagCancelData(shoppingID, cancelComment, userID, sharewithCustomer, webBotcontentRequest);
        }

        /// <summary>
        /// Update Shipment Pickup Pending Data
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentPickupPendingData(IHSOrder order, int orderID)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentPickupPendingData(orderID);
        }

        /// <summary>
        /// Insert Convert To Order Details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="convertToOrder"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param> 
        /// <param name="programCode"></param>
        /// <param name="clientAPIUrl"></param>
        /// <returns></returns>
        public async Task<int> InsertOrderDetails(IHSOrder order, ConvertToOrder convertToOrder, int tenantID, int userID, string programCode, string clientAPIUrl, WebBotContentRequest webBotcontentRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.InsertOrderDetails(convertToOrder, tenantID, userID, programCode, clientAPIUrl, webBotcontentRequest);
        }

        /// <summary>
        /// Push Order ToPoss
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pushToPoss"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="clientAPIUrl"></param>
        /// <returns></returns>
        public async Task<MessageData> PushOrderToPoss(IHSOrder order, ConvertToOrder pushToPoss, int tenantID, int userID, string programCode, string phygitalClientAPIURL)
        {
            _OrderRepository = order;
            return await _OrderRepository.PushOrderToPoss(pushToPoss, tenantID, userID, programCode, phygitalClientAPIURL);
        }

        /// <summary>
        /// Update Address Pending
        /// </summary>
        /// <param name="order"></param>
        /// <param name="addressPendingRequest"></param>
        /// <param name="tenantId"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<int> UpdateAddressPending(IHSOrder order, AddressPendingRequest addressPendingRequest, int tenantID, int userID)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateAddressPending(addressPendingRequest, tenantID, userID);
        }

        /// <summary>
        /// Get Order Return Details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderReturnsFilter"></param>
        /// <returns></returns>
        public async Task<OrderReturnsDetails> GetOrderReturnDetails(IHSOrder order, int tenantId, int userID, OrderReturnsFilterRequest orderReturnsFilter)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderReturnDetails(tenantId, userID, orderReturnsFilter);
        }

        /// <summary>
        /// Update Shipment Assigned Delivered
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedDelivered(IHSOrder order, int orderID)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentAssignedDelivered(orderID);
        }

        /// <summary>
        /// Update Shipment Assigned RTO
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedRTO(IHSOrder order, int orderID)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentAssignedRTO(orderID);
        }

        /// <summary>
        /// Shipment Assigned Print Manifest
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderIds"></param>
        /// <param name="clientAPIURL"></param>
        /// <returns></returns>
        public async Task<PrintManifestResponse> ShipmentAssignedPrintManifest(IHSOrder order, int orderID, string clientAPIURL)
        {
            _OrderRepository = order;
            return await _OrderRepository.ShipmentAssignedPrintManifest(orderID, clientAPIURL);
        }

        /// <summary>
        /// Shipment Assigned Print Label
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ShipmentId"></param>
        /// <param name="clientAPIURL"></param>
        /// <returns></returns>
        public async Task<PrintLabelResponse> ShipmentAssignedPrintLabel(IHSOrder order, int shipmentID, string clientAPIURL)
        {
            _OrderRepository = order;
            return await _OrderRepository.ShipmentAssignedPrintLabel(shipmentID, clientAPIURL);
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderIds"></param>
        /// <param name="clientAPIURL"></param>
        /// <returns></returns>
        public async Task<PrintInvoiceResponse> ShipmentAssignedPrintInvoice(IHSOrder order, int orderID, string clientAPIURL)
        {
            _OrderRepository = order;
            return await _OrderRepository.ShipmentAssignedPrintInvoice(orderID, clientAPIURL);
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderIds"></param>
        /// <param name="clientAPIURL"></param>
        /// <returns></returns>
        public async Task<int> SendSMSWhatsupOnReturnCancel(IHSOrder order, int tenantId, int userID, string programCode, int orderID, string clientAPIURL, WebBotContentRequest webBotcontentRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.SendSMSWhatsupOnReturnCancel(tenantId, userID, programCode, orderID, clientAPIURL, webBotcontentRequest);
        }

        /// <summary>
        /// UpdateOnReturnRetry
        /// <param name="order"></param>
        /// <param name="orderID"></param>
        /// <param name="statusID"></param>
        /// <param name="AWBNo"></param>
        /// <param name="returnID"></param>
        /// <returns></returns>
        public async Task<int> UpdateOnReturnRetry(IHSOrder order, int orderID, int statusID, string AWBNo, int returnID)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateOnReturnRetry(orderID, statusID, AWBNo, returnID);
        }
    }
}
