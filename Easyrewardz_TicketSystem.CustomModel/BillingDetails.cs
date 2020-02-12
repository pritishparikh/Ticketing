using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class BillingDetails
    {
        public int Billing_ID { get; set; }
        public string InvoiceBilling_ID { get; set; }
        public int Tennant_ID { get; set; }
        public string CompanyRegistration_Number { get; set; }
        public string GSTTIN_Number { get; set; }
        public string Pan_No { get; set; }
        public string Tan_No { get; set; }
        public int Created_By { get; set; }
        public int Modified_By { get; set; }
    }
}
