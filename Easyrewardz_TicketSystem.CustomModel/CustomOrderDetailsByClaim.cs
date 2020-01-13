using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomOrderDetailsByClaim
    {
        public int TicketClaimID { get; set; }
        public int TicketID { get; set; }
        public string ClaimCategory { get; set; }
        public string ClaimSubCategory  { get; set; }
        public string ClaimType { get; set; }
        public string ClaimAskedFor { get; set; }
        public int StatusID { get; set; }
        public string ClaimStatus { get; set; }

        public int CusotmerID { get; set; }
        public string CusotmerName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public int  Gender { get; set; }

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
        public string PaymentModename { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<UserComment> Comments { get; set; }
    }
}
