using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ReinitiateChatModel
    {
        public int TenantID { get; set; }
        public string ProgramCode { get; set; }
        public string StoreID { get; set; }
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public int ChatTicketID { get; set; }
        public int CreatedBy { get; set; }
      
    }
}
