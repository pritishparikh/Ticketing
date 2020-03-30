using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class JunkWordsMaster
    {
        public int JunkKeywordID { get; set; }
        public string JunkKeyword { get; set; }
        public string Reason { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredBy { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyDate { get; set; }
    }
}
