using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomClaimList
    {
        public int ClaimID { get; set; }
        public int TenantID { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int IssueTypeID { get; set; }
        public string IssueTypeName { get; set; }
        public int CreatedBy { get; set; }
        public string RaiseBy { get; set; }
        public string CreationOn { get; set; }
        public string AssignTo { get; set; }
        public string Status { get; set; }
    }
}
