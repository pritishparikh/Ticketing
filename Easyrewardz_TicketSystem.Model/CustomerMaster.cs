using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class CustomerMaster
    {
        public int CustomerID { get;set; }
        public int TenantID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmailId { get; set; }
        public int GenderID { get; set; }
        public string AltNumber { get; set; }
        public string AltEmailID { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
