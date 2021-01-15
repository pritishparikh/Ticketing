using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IHSOrder
    {
        /// <summary>
        /// Get Order Configuration
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<OrderConfiguration> GetOrderConfiguration(int tenantId);

        /// <summary>
        /// Update Order Configuration
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        Task<int> UpdateOrderConfiguration(OrderConfiguration orderConfiguration, int modifiedBy);

        /// <summary>
        /// Update Order Configuration Message Template
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        Task<int> UpdateOrderConfigurationMessageTemplate(List<PHYOrderMessageTemplate> pHYOrderMessageTemplates, int tenantID);

        /// <summary>
        /// Get Whatsapp Template
        /// </summary>
        /// <returns></returns>
        Task<List<PHYWhatsAppTemplate>> GetWhatsappTemplate(int tenantID, int userID, string messageName);

        /// <summary>
        /// Update Whatsapp Template
        /// </summary>
        /// <param name="pHYWhatsAppTemplates"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<int> UpdateWhatsappTemplate(List<PHYWhatsAppTemplate> pHYWhatsAppTemplates, int tenantID);

        /// <summary>
        /// Get Order Delivered Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userID"></param>
        /// <param name="orderDeliveredFilter"></param>
        /// <returns></returns>
        Task<OrderDeliveredDetails> GetOrderDeliveredDetails(int tenantID, int userID , OrderDeliveredFilterRequest orderDeliveredFilter);

        /// <summary>
        /// Get Order Status Filters
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        Task<List<OrderStatusFilter>> GetOrderStatusFilter(int tenantID, int userID, int pageID);

        /// <summary>
        /// Get Order Shipment Assigned Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shipmentAssignedFilter"></param>
        /// <returns></returns>
        Task<ShipmentAssignedDetails> GetShipmentAssignedDetails(int tenantID, int userID, ShipmentAssignedFilterRequest shipmentAssignedFilter);

        /// <summary>
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="shipmentAssignedRequest"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        Task<int> UpdateShipmentAssignedData(ShipmentAssignedRequest shipmentAssignedRequest, int tenantID, int userID, string programCode, string clientAPIUrl, WebBotContentRequest webBotcontentRequest);

        /// <summary>
        /// Update Shopping Bag Cancel Data
        /// </summary>
        /// <param name="shoppingID"></param>
        /// <param name="cancelComment"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
       Task<int> UpdateShipmentBagCancelData(int shoppingID, string cancelComment, int userID, bool sharewithCustomer, WebBotContentRequest webBotcontentRequest);

        /// <summary>
        /// Update Shipment Pickup Pending Data
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        Task<int> UpdateShipmentPickupPendingData(int orderID);

        /// <summary>
        /// Insert Convert To Order Details
        /// </summary>
        /// <param name="convertToOrder"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        Task<int> InsertOrderDetails(ConvertToOrder convertToOrder, int tenantID, int userID, string programCode, string clientAPIUrl, WebBotContentRequest webBotcontentRequest);

        /// <summary>
        /// PushOrderToPoss
        /// </summary>
        /// <param name="pushToPoss"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="phygitalClientAPIURL"></param>
        /// <returns></returns>
        Task<MessageData> PushOrderToPoss(ConvertToOrder pushToPoss, int tenantID, int userID, string programCode, string phygitalClientAPIURL);

        /// <summary>
        /// Update Address Pending
        /// </summary>
        /// <param name="addressPendingRequest"></param>
        /// <param name="tenantId"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<int> UpdateAddressPending(AddressPendingRequest addressPendingRequest, int tenantID, int userID);

        /// <summary>
        /// Get Order Return Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userID"></param>
        /// <param name="orderReturnsFilter"></param>
        /// <returns></returns>
        Task<OrderReturnsDetails> GetOrderReturnDetails(int tenantID, int userID, OrderReturnsFilterRequest orderReturnsFilter);

        /// <summary>
        /// Update Shipment Assigned Delivered
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        Task<int> UpdateShipmentAssignedDelivered(int orderID);

        /// <summary>
        /// Update Shipment Assigned RTO
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        Task<int> UpdateShipmentAssignedRTO(int orderID);

        /// <summary>
        /// Shipment Assigned Print Manifest
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        Task<PrintManifestResponse> ShipmentAssignedPrintManifest(int orderID, string clientAPIURL);

        /// <summary>
        /// Shipment Assigned Print Label
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        Task<PrintLabelResponse> ShipmentAssignedPrintLabel(int orderID, string clientAPIURL);

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        Task<PrintInvoiceResponse> ShipmentAssignedPrintInvoice(int orderID, string clientAPIURL);

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programCode"></param>
        /// <param name="orderID"></param>
        /// <param name="clientAPIURL"></param>
        /// <returns></returns>
        Task<int> SendSMSWhatsupOnReturnCancel(int tenantID, int userID, string programCode, int orderID, string clientAPIURL, WebBotContentRequest webBotcontentRequest);

        /// <summary>
        /// UpdateOnReturnRetry
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="statusID"></param>
        /// <param name="AWBNo"></param>
        /// <param name="returnID"></param>
        /// <returns></returns>
        Task<int> UpdateOnReturnRetry(int orderID, int statusID, string AWBNo, int returnID);
    }
}
