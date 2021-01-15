using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class OrderMaster
    {
        public int OrderMasterID { get; set; }
        public int TenantID { get; set; }
        public string OrderNumber { get; set; }
        public string BillID { get; set; }
        public string ProductBarCode { get; set; }
        public int TicketSourceID { get; set; }
        public int ModeOfPaymentID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal PricePaid { get; set; }
        public int CustomerID { get; set; }
        public int PurchaseFromStoreId { get; set; }
        public decimal Discount { get; set; }
        public string Size { get; set; }
        public string RequireSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } 
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public int CreatedBy { get; set; }
        List<OrderItem> orderItems { get; set; }
    }
}
