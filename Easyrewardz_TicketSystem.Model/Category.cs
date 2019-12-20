using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class Category
    {
        public int CategoryID { get; set; }

        public int TenantID { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreatedByName { get; set; }
    }
}
