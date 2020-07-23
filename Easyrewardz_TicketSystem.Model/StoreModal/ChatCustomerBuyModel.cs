using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ChatCustomerBuyModel
    {
        public int TenantID { get; set; }
        public string ProgramCode { get; set; }
        public int CustomerID { get; set; }
        public string CustomerMobile { get; set; }
        public string ItemCodes { get; set; }
        public bool IsFromRecommendation { get; set; }
        public bool IsDirectBuy { get; set; }
        public int UserID { get; set; }
        public Address CustomerAddress { get; set; }
  
    }
}
