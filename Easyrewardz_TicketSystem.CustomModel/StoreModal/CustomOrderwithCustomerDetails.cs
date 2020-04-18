using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
  public  class CustomOrderwithCustomerDetails
    {
        public int OrderMasterID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string DateFormat { get; set; }
        public int? ItemCount { get; set; }
        public decimal? ItemPrice { get; set; }
        public decimal? PricePaid { get; set; }
        public decimal? OrdeItemPrice { get; set; }
        public decimal? OrderPricePaid { get; set; }
        public string StoreCode { get; set; }
        public string StoreAddress { get; set; }
        public decimal Discount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public bool isChecked { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAlternateNumber { get; set; }
        public string EmailID { get; set; }
        public string AlternateEmailID { get; set; }
        public string Gender { get; set; }
    }
}
