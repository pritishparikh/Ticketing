using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderMasterID { get; set; }
        public string ItemName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int ItemCount { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal PricePaid { get; set; }
        public bool CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
