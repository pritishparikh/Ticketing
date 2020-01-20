using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomCreateCategory
    {
        public int BraindID { get; set; }
        public string BrandName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int IssueTypeID { get; set; }
        public string IssueTypeName { get; set; }
        public bool Status { get; set; }
        public string StatusName { get; set; }
        public int CreatedBy { get; set; }
    }
}
