using System.Collections.Generic;
using System.ComponentModel;

namespace Easyrewardz_TicketSystem.Model
{
    public class OrderResponseDetails
    {
        /// <summary>
        /// OrdersList
        /// </summary>
        public List<Orders> OrdersList { get; set; }
        /// <summary>
        /// TotalCount
        /// </summary>
        public int TotalCount { get; set; }
    }

    public class Orders
    {
        /// <summary>
        /// ID
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
        /// Amount
        /// </summary>
        public string Amount { get; set; }
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
        /// ShippingAddress
        /// </summary>
        public string ShippingAddress { get; set; }
        /// <summary>
        /// ActionTypeName
        /// </summary>
        public string ActionTypeName { get; set; }
        /// <summary>
        /// IsShoppingBagConverted
        /// </summary>
        public bool IsShoppingBagConverted { get; set; }
        /// <summary>
        /// ShoppingID
        /// </summary>
        public int ShoppingID { get; set; }
        /// <summary>
        /// ShoppingBagNo
        /// </summary>
        public string ShoppingBagNo { get; set; }
        /// <summary>
        /// POSGenratedInvoiceNo
        /// </summary>
        public string POSGenratedInvoiceNo { get; set; }
        /// <summary>
        /// DeliveryType
        /// </summary>
        public int DeliveryType { get; set; }
        /// <summary>
        /// DeliveryTypeName
        /// </summary>
        public string DeliveryTypeName { get; set; }
        /// <summary>
        /// PaymentLink
        /// </summary>
        public string PaymentLink { get; set; }
        /// <summary>
        /// ModeOfPayment
        /// </summary>
        public string ModeOfPayment { get; set; }
        /// <summary>
        /// PaymentVia
        /// </summary>
        public string PaymentVia { get; set; }
        /// <summary>
        /// TotalAmount
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// PickupDate
        /// </summary>
        public string PickupDate { get; set; }
        /// <summary>
        /// PickupTime
        /// </summary>
        public string PickupTime { get; set; }
        /// <summary>
        /// CourierPartner
        /// </summary>
        public string CourierPartner { get; set; }
        /// <summary>
        /// StoreCode
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// Disablebutton
        /// </summary>
        public bool DisablePaymentlinkbutton { get; set; }
        /// <summary>
        /// ShowPaymentLinkPopup
        /// </summary>
        public bool ShowPaymentLinkPopup { get; set; }
        /// <summary>
        /// CountSendPaymentLink
        /// </summary>
        public int CountSendPaymentLink { get; set; }
        /// <summary>
        /// SourceOfOrder
        /// </summary>
        public string SourceOfOrder { get; set; }
        /// <summary>
        /// PaymentBillDate
        /// </summary>
        public string PaymentBillDate { get; set; }
        /// <summary>
        /// OrdersItemList
        /// </summary>
        public List<OrdersItem> OrdersItemList { get; set; }
        /// <summary>
        /// ShippingCharges
        /// </summary>
        public string ShippingCharges { get; set; }
        /// <summary>
        /// EstimatedDeliveryDate
        /// </summary>
        public string EstimatedDeliveryDate { get; set; }
        /// <summary>
        /// PickupScheduledDate
        /// </summary>
        public string PickupScheduledDate { get; set; }

        /// <summary>
        /// CancelButtonInShipment
        /// </summary>
        public bool CancelButtonInShipment { get; set; }

        /// <summary>
        /// ShowShipmentCharges
        /// </summary>
        public bool ShowShipmentCharges { get; set; }

        /// <summary>
        /// IsPODPaymentReceived
        /// </summary>
        public bool IsPODPaymentReceived { get; set; }

        /// <summary>
        /// PODPaymentReceivedOn
        /// </summary>
        public string PODPaymentReceivedOn { get; set; }

        /// <summary>
        /// PODPaymentComent
        /// </summary>
        public string PODPaymentComent { get; set; }

        /// <summary>
        /// PODPaymentComent
        /// </summary>
        public string PODCommentBy { get; set; }

        

        /// <summary>
        /// ShoppingBagItemList
        /// </summary>
        public List<ShoppingBagItem> ShoppingBagItemList { get; set; }

        /// <summary>
        /// ShowItemProperty
        /// </summary>
        public bool ShowItemProperty { get; set; }
    }

    public class OrdersItem
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
        public string ItemPrice { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID { get; set; }
        /// OrderID
        /// </summary>
        public int Disable { get; set; }
        /// OrderID
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// ShowItemProperty
        /// </summary>
        public bool ShowItemProperty { get; set; }
    }

    public class ShoppingBagItem
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
        public string ItemPrice { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// ShoppingID
        /// </summary>
        public int ShoppingID { get; set; }
        /// <summary>
        /// ShowItemProperty
        /// </summary>
        public bool ShowItemProperty { get; set; }
        /// <summary>
        /// Availableqty
        /// </summary>
        public List<int> Availableqty { get; set; }
        /// <summary>
        /// SelectAvailableqty
        /// </summary>
        public int SelectAvailableqty { get; set; }
        /// <summary>
        /// ShowAvailableQuantity
        /// </summary>
        public bool ShowAvailableQuantity { get; set; }
    }

    public class PosShoppingBagStatus
    {
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public string Date { get; set; }
    }

    public class OrdersDataRequest
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
        /// <summary>
        /// FilterStatus
        /// </summary>
        public string FilterDelivery { get; set; }
        /// <summary>
        /// CourierPartner
        /// </summary>
        public string CourierPartner { get; set; }
    }


    public class ShoppingBag
    {
        /// <summary>
        /// ShoppingID
        /// </summary>
        public int ShoppingID { get; set; }
        /// <summary>
        /// ShoppingBagNo
        /// </summary>
        public string ShoppingBagNo { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// WabaNumber
        /// </summary>
        public string WabaNumber { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// DeliveryTypeName
        /// </summary>
        public string DeliveryTypeName { get; set; }
        /// <summary>
        /// PickupDate
        /// </summary>
        public string PickupDate { get; set; }
        /// <summary>
        /// PickupTime
        /// </summary>
        public string PickupTime { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ActionTypeName
        /// </summary>
        public string ActionTypeName { get; set; }
        /// <summary>
        /// Action
        /// </summary>
        public int Action { get; set; }
        /// <summary>
        /// IsCanceled
        /// </summary>
        public bool IsCanceled { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// CanceledComment
        /// </summary>
        public string CanceledComment { get; set; }

        /// <summary>
        /// IsPushToPoss
        /// </summary>
        public bool IsPushToPoss { get; set; }

        /// <summary>
        /// IsPosPushed
        /// </summary>
        public bool IsPosPushed { get; set; }
        /// <summary>
        /// CanceledOn
        /// </summary>
        public string CanceledOn { get; set; }
        /// <summary>
        /// ShoppingBagItemList
        /// </summary>
        public List<ShoppingBagItem> ShoppingBagItemList { get; set; }

        /// <summary>
        /// PosShoppingBagStatusList
        /// </summary>
        public List<PosShoppingBagStatus> PosShoppingBagStatusList { get; set; }
        /// <summary>
        /// ShowItemProperty
        /// </summary>
        public bool ShowItemProperty { get; set; }
        /// <summary>
        /// ShowAvailableQuantity
        /// </summary>
        public bool ShowAvailableQuantity { get; set; }
    }

    public class ShoppingBagResponseDetails
    {
        /// <summary>
        /// OrdersList
        /// </summary>
        public List<ShoppingBag> ShoppingBagList { get; set; }
        /// <summary>
        /// TotalCount
        /// </summary>
        public int TotalShoppingBag { get; set; }
    }

    public class ShoppingBagDeliveryFilter
    {
        /// <summary>
        /// DeliveryTypeID
        /// </summary>
        public int DeliveryTypeID { get; set; }
        /// <summary>
        /// DeliveryTypeName
        /// </summary>
        public string DeliveryTypeName { get; set; }
    }

    public class OrdersItemDetails
    {
        /// <summary>
        ///list of OrdersItems 
        /// </summary>
        public List<OrdersItem> OrdersItems { get; set; }
        /// <summary>
        /// InvoiceNumber 
        /// </summary>
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// ShowSelectCourierPartner
        /// </summary>
        public bool ShowSelectCourierPartner { get; set; }
        /// <summary>
        /// ShowTemplate
        /// </summary>
        public bool ShowTemplate { get; set; }
    }

    public class OrderTabSetting
    {
        /// <summary>
        /// Payment
        /// </summary>
        public bool PaymentVisible { get; set; }
        /// <summary>
        /// Shipment
        /// </summary>
        public bool ShipmentVisible { get; set; }
        /// <summary>
        /// ShoppingBag
        /// </summary>
        public bool ShoppingBagVisible { get; set; }
        /// <summary>
        /// POD
        /// </summary>
        public bool PODVisible { get; set; }
        /// <summary>
        /// StoreDelivery
        /// </summary>
        public bool StoreDelivery { get; set; }
        /// <summary>
        /// Exists
        /// </summary>
        public int Exists { get; set; }
        /// <summary>
        /// EnableCheckService
        /// </summary>
        public bool EnableCheckService { get; set; }
        /// <summary>
        /// ShowShipmentCharges
        /// </summary>
        public bool ShowShipmentCharges { get; set; }
        /// <summary>
        /// ShowSelfPickupTab
        /// </summary>
        public bool ShowSelfPickupTab { get; set; }
    }

    public class OrdersSmsWhatsUpDataDetails
    {
        /// <summary>
        /// Order ID
        /// </summary>
        public int OderID { get; set; }

        /// <summary>
        /// Alert Communication via Whtsup
        /// </summary>
        public bool AlertCommunicationviaWhtsup { get; set; }

        /// <summary>
        /// Alert Communication via SMS
        /// </summary>
        public bool AlertCommunicationviaSMS { get; set; }

        /// <summary>
        /// SMS Sender Name
        /// </summary>
        public string SMSSenderName { get; set; }

        /// <summary>
        /// Is Send
        /// </summary>
        public bool IsSend { get; set; }

        /// <summary>
        /// Message Text
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Invoice No
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Additional Info
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        public string WabaNumber { get; set; }
    }

    public enum SMSWhtappTemplate
    {
        /// <summary>
        /// Shopping Bag Convert To Order
        /// </summary>
        [Description("ShoppingBagConvertToOrder")]
        ShoppingBagConvertToOrder,

        /// <summary>
        /// AWB Assigned
        /// </summary>
        [Description("AWBAssigned")]
        AWBAssigned,

        /// <summary>
        /// Pick up Scheduled
        /// </summary>
        [Description("PickupScheduled")]
        PickupScheduled,

        /// <summary>
        /// Shipped
        /// </summary>
        [Description("Shipped")]
        Shipped,

        /// <summary>
        /// Delivered
        /// </summary>
        [Description("Delivered")]
        Delivered
    }

    public class ShippingTemplateRequest
    {
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

    public class ShippingTemplateDetails
    {
        /// <summary>
        /// ShippingTemplateList
        /// </summary>
        public List<ShippingTemplate> ShippingTemplateList { get; set; }
        /// <summary>
        /// TotalCount
        /// </summary>
        public int TotalCount { get; set; }
    }


    public class ShippingTemplate
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// TemplateName
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Height
        /// </summary>
        public decimal Height { get; set; }
        /// <summary>
        /// Height_Unit
        /// </summary>
        public string Height_Unit { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        public decimal Length { get; set; }
        /// <summary>
        /// Length_Unit
        /// </summary>
        public string Length_Unit { get; set; }
        /// <summary>
        /// Breath
        /// </summary>
        public decimal Breath { get; set; }
        /// <summary>
        /// Breath_Unit
        /// </summary>
        public string Breath_Unit { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// Weight_Unit
        /// </summary>
        public string Weight_Unit { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// CreatedOn
        /// </summary>
        public string CreatedOn { get; set; }
        /// <summary>
        /// ModifiedOn
        /// </summary>
        public string ModifiedOn { get; set; }
        /// <summary>
        /// Createdby
        /// </summary>
        public string Createdby { get; set; }
        /// <summary>
        /// Modifiedby
        /// </summary>
        public string Modifiedby { get; set; }
    }


    public class AddEditShippingTemplate
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; } = 0;
        /// <summary>
        /// TemplateName
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Height
        /// </summary>
        public decimal Height { get; set; }
        /// <summary>
        /// Height_Unit
        /// </summary>
        public string Height_Unit { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        public decimal Length { get; set; }
        /// <summary>
        /// Length_Unit
        /// </summary>
        public string Length_Unit { get; set; }
        /// <summary>
        /// Breath
        /// </summary>
        public decimal Breath { get; set; }
        /// <summary>
        /// Breath_Unit
        /// </summary>
        public string Breath_Unit { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// Weight_Unit
        /// </summary>
        public string Weight_Unit { get; set; }
        
    }

    public class PincodeCheck
    {
        /// <summary>
        /// PincodeAvailable
        /// </summary>
        public bool PincodeAvailable { get; set; }
        /// <summary>
        /// PincodeSatate
        /// </summary>
        public string PincodeState { get; set; }
    }

    public class OrderSelfPickUp
    {
        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// PickupDate
        /// </summary>
        public string PickupDate { get; set; }
        /// <summary>
        /// PickupTime
        /// </summary>
        public string PickupTime { get; set; }
    }

    public class OrderCountry
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// CreatedOn
        /// </summary>
        public string CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// ModifiedOn
        /// </summary>
        public string ModifiedOn { get; set; }
        /// <summary>
        /// ModifiedBy
        /// </summary>
        public string ModifiedBy { get; set; }
    }

    public class ModifyOrderCountryRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// IsDelete
        /// </summary>
        public bool IsDelete { get; set; } = false;
    }

    public class CustAddressDetails
    {
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// ShippingAddress
        /// </summary>
        public string ShippingAddress { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// PinCode
        /// </summary>
        public string PinCode { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Landmark
        /// </summary>
        public string Landmark { get; set; }
    }


    public class PrintLabelDetails
    {
        public OrderCustDetails OrderCustDetails { get; set; }
        public OrderLabelDetails OrderLabelDetails { get; set; }
        public OrderTemplateDetails OrderTemplateDetails { get; set; }
        public List<OrderLabelItemsDetails> OrderLabelItemsDetails { get; set; }
        public string TotalPrice { get; set; }
    }

    public class OrderCustDetails
    {
        public string OrderCreatedOn { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Landmark { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string PincodeID { get; set; }
        public string StoreEmailID { get; set; }
        public string StorePhoneNo { get; set; }
    }

    public class OrderLabelDetails
    {
        public string OrderID { get; set; }
        public string RequestID { get; set; }
    }

    public class OrderTemplateDetails
    {
        public string Weight { get; set; }
        public string Dimensions { get; set; }
    }

    public class OrderLabelItemsDetails
    {
        public string SKU { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string Quantity { get; set; }
    }

    public class SelfPickupOrdersDataRequest
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
        public string Filterdate { get; set; }
        /// <summary>
        /// CourierPartner
        /// </summary>
        public string FilterTimeSlot { get; set; }
    }

    public class SelfPickupOrderResponseDetails
    {
        /// <summary>
        /// OrdersList
        /// </summary>
        public List<Orders> OrdersList { get; set; }
        /// <summary>
        /// TotalCount
        /// </summary>
        public int TotalCount { get; set; }
    }

    public class RequestShoppingAvailableQty
    {
        public List<ShoppingAvailableQty> shoppingAvailableQty { get; set; }
    }
    public class ShoppingAvailableQty
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
        /// Availableqty
        /// </summary>
        public int Availableqty { get; set; }
        /// <summary>
        /// ShoppingID
        /// </summary>
        public int ShoppingID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerMob
        /// </summary>
        public string CustomerMob { get; set; }

    }
}
