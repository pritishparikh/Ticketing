using System;
using System.Collections.Generic;
using System.Text;

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
        /// CountSendPaymentLink
        /// </summary>
        public int CountSendPaymentLink { get; set; }
        /// <summary>
        /// SourceOfOrder
        /// </summary>
        public string SourceOfOrder { get; set; }
        /// <summary>
        /// OrdersItemList
        /// </summary>
        public List<OrdersItem> OrdersItemList { get; set; }
        /// <summary>
        /// ShoppingBagItemList
        /// </summary>
        public List<ShoppingBagItem> ShoppingBagItemList { get; set; }
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
        /// CanceledComment
        /// </summary>
        public string CanceledComment { get; set; }
        /// <summary>
        /// CanceledOn
        /// </summary>
        public string CanceledOn { get; set; }
        /// <summary>
        /// ShoppingBagItemList
        /// </summary>
        public List<ShoppingBagItem> ShoppingBagItemList { get; set; }
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
        /// StoreDelivery
        /// </summary>
        public bool StoreDelivery { get; set; }
        /// <summary>
        /// Exists
        /// </summary>
        public int Exists { get; set; }
    }
}
