using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class HSRequestGeneratePaymentLink
    {
        public string ProgramCode { get; set; }
        public string StoreCode { get; set; }
        public string BillDateTime { get; set; }
        public string TerminalId { get; set; }
        public string MerchantTxnID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public decimal Amount { get; set; }
    }

    public class HSResponseGeneratePaymentLink
    {
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public string tokenId { get; set; }
        public string status { get; set; }
    }
    public class HSRequestGenerateToken
    {
        public string ClientID { get; set; } = "9090";
        public string ClientSecret { get; set; } = "090";
        public string GrantType { get; set; } = "pop";
        public string Scope { get; set; } = "lkl";
    }

    public class HSResponseGenerateToken
    {
        public string access_Token { get; set; }
        public int expires_In { get; set; }
        public string token_Type { get; set; }
    }

    public class SentPaymentLink
    {
        public string InvoiceNumber { get; set; }
        public string StoreCode { get; set; }
    }
}
