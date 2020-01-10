using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomOrderDetailsByCustomer
    {
        public int CusotmerID { get; set; }
        public string CusotmerName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }

        public int OrderMasterID { get; set; }
        public string OrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string DateFormat { get; set; }
        public int? ItemCount { get; set; }
        public decimal? ItemPrice { get; set; }
        public decimal? PricePaid { get; set; }
        public string StoreCode { get; set; }
        public string StoreAddress { get; set; }
        public decimal Discount { get; set; }
        public string PaymentModename { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
