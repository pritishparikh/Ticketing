using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class OrderResponseDetails
    {
        public List<Orders> OrdersList { get; set; }

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
        public double ItemPrice { get; set; }
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
    }

}
