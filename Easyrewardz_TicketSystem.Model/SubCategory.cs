using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class SubCategory
    {
        public int SubCategoryID { get; set; }

        public string SubCategoryName { get; set; }

        public int CategoryID { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string CreatedByName { get; set; }
    }
}
