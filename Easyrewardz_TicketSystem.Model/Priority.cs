using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class Priority
    {
        public int PriorityID { get; set; }

        public int TenantID { get; set; }

        public string PriortyName { get; set; }


        public bool IsActive { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }


        public DateTime ModifiedDate { get; set; }

        public string CreatedByName { get; set; }

    }
}
