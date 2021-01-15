using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
  public  class CustomOrderwithCustomerDetails
    {
        /// <summary>
        /// Order Master ID
        /// </summary>
        public int OrderMasterID { get; set; }

        /// <summary>
        /// Invoice Number
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Invoice Date
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Date Format
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Item Count
        /// </summary>
        public int? ItemCount { get; set; }

        /// <summary>
        /// Item Price
        /// </summary>
        public decimal? ItemPrice { get; set; }

        /// <summary>
        /// Price Paid
        /// </summary>
        public decimal? PricePaid { get; set; }

        /// <summary>
        /// Order Item Price
        /// </summary>
        public decimal? OrdeItemPrice { get; set; }

        /// <summary>
        /// Order Price Paid
        /// </summary>
        public decimal? OrderPricePaid { get; set; }

        /// <summary>
        /// Store Code
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// Store Address
        /// </summary>
        public string StoreAddress { get; set; }

        /// <summary>
        /// Discount
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Order Items
        /// </summary>
        public List<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// is Checked
        /// </summary>
        public bool isChecked { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Phone Number
        /// </summary>
        public string CustomerPhoneNumber { get; set; }

        /// <summary>
        /// Customer Alternate Number
        /// </summary>
        public string CustomerAlternateNumber { get; set; }

        /// <summary>
        /// Email ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// Alternate Email ID
        /// </summary>
        public string AlternateEmailID { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }
    }
}
