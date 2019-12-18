using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class Brand
    {
        public int BrandID { get; set; }

        public int TenantID { get; set; }

        public string BrandName { get; set; }

        public string BrandCode { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreatedByName { get; set; }

    }
}
