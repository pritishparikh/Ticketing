using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IHSOrder
    {
        /// <summary>
        /// Get Order Configuration
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        OrderConfiguration GetOrderConfiguration(int tenantId);

        /// <summary>
        /// Update Order Configuration
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        int UpdateOrderConfiguration(OrderConfiguration orderConfiguration, int modifiedBy);

        /// <summary>
        /// Update Order Configuration Message Template
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        int UpdateOrderConfigurationMessageTemplate(List<PHYOrderMessageTemplate> pHYOrderMessageTemplates, int TenantId);

        /// <summary>
        /// Get Whatsapp Template
        /// </summary>
        /// <returns></returns>
        List<PHYWhatsAppTemplate> GetWhatsappTemplate(int TenantId, int UserId, string MessageName);

        /// <summary>
        /// Update Whatsapp Template
        /// </summary>
        /// <param name="pHYWhatsAppTemplates"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int UpdateWhatsappTemplate(List<PHYWhatsAppTemplate> pHYWhatsAppTemplates, int TenantId);

        /// <summary>
        /// Get Order Delivered Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderDeliveredFilter"></param>
        /// <returns></returns>
        OrderDeliveredDetails GetOrderDeliveredDetails(int tenantId, int userId, OrderDeliveredFilterRequest orderDeliveredFilter);

        /// <summary>
        /// Get Order Status Filters
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        List<OrderStatusFilter> GetOrderStatusFilter(int tenantId, int userId, int pageID);

        /// <summary>
        /// Get Order Shipment Assigned Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shipmentAssignedFilter"></param>
        /// <returns></returns>
        ShipmentAssignedDetails GetShipmentAssignedDetails(int tenantId, int userId, ShipmentAssignedFilterRequest shipmentAssignedFilter);

        /// <summary>
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="shipmentAssignedRequest"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        int UpdateShipmentAssignedData(ShipmentAssignedRequest shipmentAssignedRequest, int tenantId, int userId, string programCode, string clientAPIUrl);

        /// <summary>
        /// Update Shopping Bag Cancel Data
        /// </summary>
        /// <param name="shoppingID"></param>
        /// <param name="cancelComment"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int UpdateShipmentBagCancelData(int shoppingID, string cancelComment, int userId);

        /// <summary>
        /// Update Shipment Pickup Pending Data
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        int UpdateShipmentPickupPendingData(int OrderID);

        /// <summary>
        /// Insert Convert To Order Details
        /// </summary>
        /// <param name="convertToOrder"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        int InsertOrderDetails(ConvertToOrder convertToOrder, int tenantId, int userId, string programCode, string clientAPIUrl);

        /// <summary>
        /// Update Address Pending
        /// </summary>
        /// <param name="addressPendingRequest"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int UpdateAddressPending(AddressPendingRequest addressPendingRequest, int tenantId, int userId);

        /// <summary>
        /// Get Order Return Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderReturnsFilter"></param>
        /// <returns></returns>
        OrderReturnsDetails GetOrderReturnDetails(int tenantId, int userId, OrderReturnsFilterRequest orderReturnsFilter);

        /// <summary>
        /// Update Shipment Assigned Delivered
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        int UpdateShipmentAssignedDelivered(int orderID);

        /// <summary>
        /// Update Shipment Assigned RTO
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        int UpdateShipmentAssignedRTO(int orderID);

        /// <summary>
        /// Shipment Assigned Print Manifest
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        PrintManifestResponse ShipmentAssignedPrintManifest(int orderIds, string clientAPIURL);

        /// <summary>
        /// Shipment Assigned Print Label
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        PrintLabelResponse ShipmentAssignedPrintLabel(int orderIds, string clientAPIURL);

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        PrintInvoiceResponse ShipmentAssignedPrintInvoice(int OrderIds, string ClientAPIURL);

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        int SendSMSWhatsupOnReturnCancel(int TenantId, int UserId, string ProgramCode, int OrderId, string ClientAPIURL);

        int UpdateOnReturnRetry(int OrderId, int StatusId, string AWBNo, int ReturnId);
    }
}
