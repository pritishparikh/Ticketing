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
        public string SearchText { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string FilterStatus { get; set; }
    }

    public class OrderDeliveredDetails
    {
        public List<OrderDelivered> orderDelivereds { get; set; }

        public int TotalCount { get; set; }
    }

    public class OrderStatusFilter
    {
        public int StatusID { get; set; }

        public string StatusName { get; set; }
    }

    public class ShipmentAssigned
    {
        public int OrderID { get; set; }

        public string AWBNo { get; set; }

        public string InvoiceNo { get; set; }

        public string CourierPartner { get; set; }

        public string CourierPartnerOrderID { get; set; }

        public string CourierPartnerShipmentID { get; set; }

        public string ReferenceNo { get; set; }

        public string StoreName { get; set; }

        public string StaffName { get; set; }

        public string MobileNumber { get; set; }

        public bool IsProceed { get; set; }

        public string ShipmentAWBID { get; set; }
    }

    public class ShipmentAssignedDetails
    {
        public List<ShipmentAssigned> shipmentAssigned { get; set; }

        public int TotalCount { get; set; }
    }

    public class ShipmentAssignedFilterRequest
    {
        public string SearchText { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string FilterReferenceNo { get; set; }
    }

    public class ShipmentAssignedRequest
    {
        public string ShipmentAWBID { get; set; }

        public int OrderID { get; set; }

        public string ReferenceNo { get; set; }

        public string StoreName { get; set; }

        public string StaffName { get; set; }

        public string MobileNumber { get; set; }

        public bool IsProceed { get; set; }
    }

    public class ConvertToOrder
    {
        public int ShoppingID { get; set; }

        public string InvoiceNo { get; set; }

        public double Amount { get; set; }
    }

    public class AddressPendingRequest
    {
        public int OrderID { get; set; }

        public string ShipmentAddress { get; set; }

        public string Landmark { get; set; }

        public string PinCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }

    public class ReturnShipmentDetails
    {
        public string AWBNumber { get; set; }

        public string InvoiceNo { get; set; }

        public string ItemIDs { get; set; }

        public bool Status { get; set; }

        public string StatusMessge { get; set; }
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
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }

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
        public List<OrderReturns> orderReturns { get; set; }

        public int TotalCount { get; set; }
    }

    public class OrderReturnsFilterRequest
    {
        public string SearchText { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string FilterStatus { get; set; }
    }
    public class AWbdetailModel
    {
        public string AWBNumber { get; set; }

        public string InvoiceNo { get; set; }

        public string ItemIDs { get; set; }

        public string CourierPartner { get; set; }

        public string Date { get; set; }
    }

    public class PrintManifestRequest
    {
        public List<Int64> orderIds { get; set; }
    }

    public class PrintManifestResponse
    {
        public string manifestUrl { get; set; }
    }

    public class PrintLabelRequest
    {
        public List<Int64> shipmentId { get; set; }
    }

    public class PrintLabelResponse
    {
        public int label_created { get; set; }
        public string label_url { get; set; }
        public string response { get; set; }
    }
}
