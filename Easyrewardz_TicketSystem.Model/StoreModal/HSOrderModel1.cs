using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class ModuleConfiguration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// ShoppingBag
        /// </summary>
        public bool ShoppingBag { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public bool Payment { get; set; }
        /// <summary>
        /// Shipment
        /// </summary>
        public bool Shipment { get; set; }
    }

    public class OrderConfiguration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// ShoppingBag
        /// </summary>
        public bool IntegratedSystem { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public bool Payment { get; set; }
        /// <summary>
        /// Shipment
        /// </summary>
        public bool Shipment { get; set; }
        /// <summary>
        /// ShoppingBag
        /// </summary>
        public bool ShoppingBag { get; set; }

        /// <summary>
        /// StoreDelivery
        /// </summary>
        public bool StoreDelivery { get; set; }

        /// <summary>
        /// AlertCommunicationviaWhtsup 
        /// </summary>
        public bool AlertCommunicationviaWhtsup { get; set; }

        /// <summary>
        /// AlertCommunicationviaSMS 
        /// </summary>
        public bool AlertCommunicationviaSMS { get; set; }

        /// <summary>
        /// AlertCommunicationSMSText
        /// </summary>
        public string AlertCommunicationSMSText { get; set; }

        /// <summary>
        /// EnableClickAfterValue
        /// </summary>
        public int EnableClickAfterValue { get; set; }
        /// <summary>
        /// EnableClickAfterDuration
        /// </summary>
        public string EnableClickAfterDuration { get; set; }

        /// <summary>
        /// ShoppingBagConvertToOrder
        /// </summary>
        public bool ShoppingBagConvertToOrder { get; set; }

        /// <summary>
        /// ShoppingBagConvertToOrderText
        /// </summary>
        public string ShoppingBagConvertToOrderText { get; set; }

        /// <summary>
        /// AWBAssigned
        /// </summary>
        public bool AWBAssigned { get; set; }

        /// <summary>
        /// AWBAssignedText
        /// </summary>
        public string AWBAssignedText { get; set; }

        /// <summary>
        /// PickeupScheduled
        /// </summary>
        public bool PickupScheduled { get; set; }

        /// <summary>
        /// PickeupScheduledText
        /// </summary>
        public string PickupScheduledText { get; set; }

        /// <summary>
        /// Shipped
        /// </summary>
        public bool Shipped { get; set; }

        /// <summary>
        /// ShippedText
        /// </summary>
        public string ShippedText { get; set; }

        /// <summary>
        /// Delivered
        /// </summary>
        public bool Delivered { get; set; }

        /// <summary>
        /// DeliveredText
        /// </summary>
        public string DeliveredText { get; set; }

        /// <summary>
        /// Cancel
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// CancelText
        /// </summary>
        public string CancelText { get; set; }

        /// <summary>
        /// UnDeliverable
        /// </summary>
        public bool UnDeliverable { get; set; }

        /// <summary>
        /// UnDeliverableText
        /// </summary>
        public string UnDeliverableText { get; set; }

        /// <summary>
        /// StoreDeliveryText
        /// </summary>
        public string StoreDeliveryText { get; set; }

        /// <summary>
        /// PaymentTenantCodeText
        /// </summary>
        public string PaymentTenantCodeText { get; set; }

        /// <summary>
        /// Retry
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// StateFlag
        /// </summary>
        public bool StateFlag { get; set; }

        /// <summary>
        /// CurrencyText
        /// </summary>
        public string CurrencyText { get; set; }

        /// <summary>
        /// List Of PHYOrderMessageTemplate
        /// </summary>
        public List<PHYOrderMessageTemplate> pHYOrderMessageTemplates { get; set; }

    }

    public class PHYOrderMessageTemplate
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// MessageName
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// StoreDeliveryIsActive
        /// </summary>
        public bool StoreDeliveryIsActive { get; set; }

        /// <summary>
        /// StoreDeliveryDescription
        /// </summary>
        public string StoreDeliveryDescription { get; set; }
    }

    public class OrderDelivered
    {
        /// <summary>
        /// OrderId
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// ActionTypeName
        /// </summary>
        public string ActionTypeName { get; set; }

        /// <summary>
        /// Order Delivered Items
        /// </summary>
        public List<OrderDeliveredItem> orderDeliveredItems { get; set; }
    }

    public class OrderDeliveredItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// ItemID
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// ItemName
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// ItemPrice
        /// </summary>
        public double ItemPrice { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID { get; set; }
    }

    public class OrderDeliveredFilterRequest
    {
        /// <summary>
        /// SearchText
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// PageNo
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// FilterStatus
        /// </summary>
        public string FilterStatus { get; set; }
    }

    public class OrderDeliveredDetails
    {
        /// <summary>
        /// OrderDelivereds
        /// </summary>
        public List<OrderDelivered> orderDelivereds { get; set; }

        /// <summary>
        /// TotalCount
        /// </summary>
        public int TotalCount { get; set; }
    }

    public class OrderStatusFilter
    {
        /// <summary>
        /// StatusID
        /// </summary>
        public int StatusID { get; set; }

        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }
    }

    public class ShipmentAssigned
    {
        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// AWBNo
        /// </summary>
        public string AWBNo { get; set; }

        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// CourierPartner
        /// </summary>
        public string CourierPartner { get; set; }

        /// <summary>
        /// CourierPartnerOrderID
        /// </summary>
        public string CourierPartnerOrderID { get; set; }

        /// <summary>
        /// CourierPartnerShipmentID
        /// </summary>
        public string CourierPartnerShipmentID { get; set; }

        /// <summary>
        /// ReferenceNo
        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// StoreName
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// StaffName
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// IsProceed
        /// </summary>
        public bool IsProceed { get; set; }

        /// <summary>
        /// ShipmentAWBID
        /// </summary>
        public string ShipmentAWBID { get; set; }
    }

    public class ShipmentAssignedDetails
    {
        /// <summary>
        /// Shipment Assigned
        /// </summary>
        public List<ShipmentAssigned> shipmentAssigned { get; set; }

        /// <summary>
        /// TotalCount
        /// </summary>
        public int TotalCount { get; set; }
    }

    public class ShipmentAssignedFilterRequest
    {
        /// <summary>
        /// SearchText
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// PageNo
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// FilterReferenceNo
        /// </summary>
        public string FilterReferenceNo { get; set; }
        /// <summary>
        /// CourierPartner
        /// </summary>
        public string CourierPartner { get; set; }
    }

    public class ShipmentAssignedRequest
    {
        /// <summary>
        /// ShipmentAWBID
        /// </summary>
        public string ShipmentAWBID { get; set; }

        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// ReferenceNo
        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// StoreName
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// StaffName
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// IsProceed
        /// </summary>
        public bool IsProceed { get; set; }
    }

    public class ConvertToOrder
    {
        /// <summary>
        /// ShoppingID
        /// </summary>
        public int ShoppingID { get; set; }

        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public double Amount { get; set; }
    }

    public class AddressPendingRequest
    {
        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// ShipmentAddress
        /// </summary>
        public string ShipmentAddress { get; set; }

        /// <summary>
        /// Landmark
        /// </summary>
        public string Landmark { get; set; }

        /// <summary>
        /// PinCode
        /// </summary>
        public string PinCode { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
    }

    public class ReturnShipmentDetails
    {
        /// <summary>
        /// AWBNumber
        /// </summary>
        public string AWBNumber { get; set; }

        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// ItemIDs
        /// </summary>
        public string ItemIDs { get; set; }

        /// <summary>
        /// CourierPartner
        /// </summary>
        public string CourierPartner { get; set; }

        /// <summary>
        /// IsStoreDelivery
        /// </summary>
        public bool IsStoreDelivery { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// StatusMessge
        /// </summary>
        public string StatusMessge { get; set; }

        /// <summary>
        /// ShipmentCharges
        /// </summary>
        public string ShipmentCharges { get; set; }
    }

    public class OrderReturns
    {
        /// <summary>
        /// ReturnID
        /// </summary>
        public int ReturnID { get; set; }

        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// AWBNo
        /// </summary>
        public string AWBNo { get; set; }

        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// ReturnDate
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// ReturnTime
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// StatusId
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// RetryCount
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// IsRetry
        /// </summary>
        public bool IsRetry { get; set; }

        /// <summary>
        /// IsCancelled
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// OrderReturnsItems
        /// </summary>
        public List<OrderReturnsItem> orderReturnsItems { get; set; }
    }

    public class OrderReturnsItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// ItemID
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// ItemName
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// ItemPrice
        /// </summary>
        public double ItemPrice { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID { get; set; }

    }

    public class OrderReturnsDetails
    {
        /// <summary>
        /// orderReturns
        /// </summary>
        public List<OrderReturns> orderReturns { get; set; }

        /// <summary>
        /// TotalCount
        /// </summary>
        public int TotalCount { get; set; }
    }

    public class OrderReturnsFilterRequest
    {
        /// <summary>
        /// SearchText
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// PageNo
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// FilterStatus
        /// </summary>
        public string FilterStatus { get; set; }
    }
    public class AWbdetailModel
    {
        /// <summary>
        /// AWBNumber
        /// </summary>
        public string AWBNumber { get; set; }

        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// ItemIDs
        /// </summary>
        public string ItemIDs { get; set; }

        /// <summary>
        /// CourierPartner
        /// </summary>
        public string CourierPartner { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// ShipmentCharges
        /// </summary>
        public string ShipmentCharges { get; set; }
    }

    public class PrintManifestRequest
    {
        /// <summary>
        /// List of order Ids
        /// </summary>
        public List<int> orderIds { get; set; }
    }

    public class PrintManifestResponse
    {
        /// <summary>
        /// manifestUrl
        /// </summary>
        public string manifestUrl { get; set; }
    }

    public class PrintLabelRequest
    {
        /// <summary>
        /// List of shipment Ids
        /// </summary>
        public List<int> shipmentId { get; set; }
    }

    public class PrintLabelResponse
    {
        /// <summary>
        /// label_created
        /// </summary>
        public int label_created { get; set; }

        /// <summary>
        /// label_url
        /// </summary>
        public string label_url { get; set; }

        /// <summary>
        /// response
        /// </summary>
        public string response { get; set; }
    }

    public class PrintInvoiceRequest
    {
        /// <summary>
        /// List of shipment Ids
        /// </summary>
        public List<int> ids { get; set; }
    }

    public class PrintInvoiceResponse
    {
        /// <summary>
        /// label_created
        /// </summary>
        public bool is_invoice_created { get; set; }

        /// <summary>
        /// label_url
        /// </summary>
        public string invoice_url { get; set; } = "";

        /// <summary>
        /// response
        /// </summary>
        public List<object> not_created { get; set; } = null;
    }

    public class PHYWhatsAppTemplate
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// MessageName
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// TemplateName
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }
    }

    public class PHYWhatsAppTemplateDetails
    {
        public List<PHYWhatsAppTemplate> pHYWhatsAppTemplates { get; set; }
    }
}
