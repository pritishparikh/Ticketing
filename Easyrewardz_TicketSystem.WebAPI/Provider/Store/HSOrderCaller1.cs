using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
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
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public async Task<OrderConfiguration> GetOrderConfiguration(IHSOrder Order, int TenantId)
        {
            _OrderRepository = Order;
            return await _OrderRepository.GetOrderConfiguration(TenantId);
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
        /// <param name="orderConfiguration"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateOrderConfigurationMessageTemplate(IHSOrder order, List<PHYOrderMessageTemplate> pHYOrderMessageTemplates, int TenantId)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateOrderConfigurationMessageTemplate(pHYOrderMessageTemplates, TenantId);
        }

        /// <summary>
        /// Get Whatsapp Template
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public async Task<List<PHYWhatsAppTemplate>> GetWhatsappTemplate(IHSOrder Order, int TenantId, int UserId, string MessageName)
        {
            _OrderRepository = Order;
            return await _OrderRepository.GetWhatsappTemplate(TenantId, UserId, MessageName);
        }

        /// <summary>
        /// Update Whatsapp Template
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pHYOrderMessageTemplates"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public async Task<int> UpdateWhatsappTemplate(IHSOrder order, List<PHYWhatsAppTemplate> pHYWhatsAppTemplates, int TenantId)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateWhatsappTemplate(pHYWhatsAppTemplates, TenantId);
        }

        /// <summary>
        /// Get Order Delivered Details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderDeliveredFilter"></param>
        /// <returns></returns>
        public async Task<OrderDeliveredDetails> GetOrderDeliveredDetails(IHSOrder order, int TenantId, int UserId, OrderDeliveredFilterRequest orderDeliveredFilter)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderDeliveredDetails(TenantId, UserId, orderDeliveredFilter);
        }

        /// <summary>
        /// Get Order Status Filters
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public async Task<List<OrderStatusFilter>> GetOrderStatusFilter(IHSOrder order, int TenantId, int UserId, int PageID)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderStatusFilter(TenantId, UserId, PageID);
        }

        /// <summary>
        /// Get Order Shipment Assigned Details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shipmentAssignedFilter"></param>
        /// <returns></returns>
        public async Task<ShipmentAssignedDetails> GetShipmentAssignedDetails(IHSOrder order, int TenantId, int UserId, ShipmentAssignedFilterRequest shipmentAssignedFilter)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetShipmentAssignedDetails(TenantId, UserId, shipmentAssignedFilter);
        }

        /// <summary>
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="order"></param>
        /// <param name="shipmentAssignedRequest"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedData(IHSOrder order, ShipmentAssignedRequest shipmentAssignedRequest, int TenantId, int UserId,string ProgramCode, string ClientAPIUrl)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentAssignedData(shipmentAssignedRequest, TenantId, UserId, ProgramCode, ClientAPIUrl);
        }

        /// <summary>
        /// Update Shopping Bag Cancel Data
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ShoppingID"></param>
        /// <param name="CancelComment"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentBagCancelData(IHSOrder order, int ShoppingID, string CancelComment, int UserId)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentBagCancelData(ShoppingID, CancelComment, UserId);
        }

        /// <summary>
        /// Update Shipment Pickup Pending Data
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentPickupPendingData(IHSOrder order, int OrderID)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentPickupPendingData(OrderID);
        }

        /// <summary>
        /// Insert Convert To Order Details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="convertToOrder"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        public async Task<int> InsertOrderDetails(IHSOrder order, ConvertToOrder convertToOrder, int TenantId, int UserId, string ProgramCode, string ClientAPIUrl)
        {
            _OrderRepository = order;
            return await _OrderRepository.InsertOrderDetails(convertToOrder, TenantId, UserId, ProgramCode, ClientAPIUrl);
        }

        /// <summary>
        /// Update Address Pending
        /// </summary>
        /// <param name="order"></param>
        /// <param name="addressPendingRequest"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<int> UpdateAddressPending(IHSOrder order, AddressPendingRequest addressPendingRequest, int TenantId, int UserId)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateAddressPending(addressPendingRequest, TenantId, UserId);
        }

        /// <summary>
        /// Get Order Return Details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="orderReturnsFilter"></param>
        /// <returns></returns>
        public async Task<OrderReturnsDetails> GetOrderReturnDetails(IHSOrder order, int TenantId, int UserId, OrderReturnsFilterRequest orderReturnsFilter)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderReturnDetails(TenantId, UserId, orderReturnsFilter);
        }

        /// <summary>
        /// Update Shipment Assigned Delivered
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedDelivered(IHSOrder order, int OrderID)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentAssignedDelivered(OrderID);
        }

        /// <summary>
        /// Update Shipment Assigned RTO
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedRTO(IHSOrder order, int OrderID)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShipmentAssignedRTO(OrderID);
        }

        /// <summary>
        /// Shipment Assigned Print Manifest
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public PrintManifestResponse ShipmentAssignedPrintManifest(IHSOrder order, int OrderIds, string ClientAPIURL)
        {
            _OrderRepository = order;
            return _OrderRepository.ShipmentAssignedPrintManifest(OrderIds, ClientAPIURL);
        }

        /// <summary>
        /// Shipment Assigned Print Label
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ShipmentId"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public PrintLabelResponse ShipmentAssignedPrintLabel(IHSOrder order, int ShipmentId, string ClientAPIURL)
        {
            _OrderRepository = order;
            return _OrderRepository.ShipmentAssignedPrintLabel(ShipmentId, ClientAPIURL);
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIUR"></param>
        /// <returns></returns>
        public PrintInvoiceResponse ShipmentAssignedPrintInvoice(IHSOrder order, int OrderIds, string ClientAPIUR)
        {
            _OrderRepository = order;
            return _OrderRepository.ShipmentAssignedPrintInvoice(OrderIds, ClientAPIUR);
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIUR"></param>
        /// <returns></returns>
        public async Task<int> SendSMSWhatsupOnReturnCancel(IHSOrder order, int TenantId, int UserId, string ProgramCode, int OrderId, string ClientAPIURL)
        {
            _OrderRepository = order;
            return await _OrderRepository.SendSMSWhatsupOnReturnCancel(TenantId, UserId, ProgramCode, OrderId, ClientAPIURL);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="OrderId"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<int> UpdateOnReturnRetry(IHSOrder order, int OrderId, int StatusId, string AWBNo, int ReturnId)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateOnReturnRetry(OrderId, StatusId, AWBNo, ReturnId);
        }
    }
}
