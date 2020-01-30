using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomStoreUser
    {
        public string BrandIDs { get; set; }
        public string CategoryIds { get; set; }
        public string SubCategoryIds { get; set; }
        public string IssuetypeIds { get; set; }
        public int ClaimApproverID { get; set; }
        public int CRMRoleID { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public int? IsStoreUser { get; set; }
        public int? UserID { get; set; }
        public int TenantID { get; set; }
    }
}
