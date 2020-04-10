using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomOrderDetails
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public int ItemCount { get; set; }

        public decimal PricePaid { get; set; }
        public string StoreAddress { get; set; }
        public decimal ItemPrice { get; set; }
        public string StoreCode { get; set; }
        public string Discount { get; set; }
       
    }
}
