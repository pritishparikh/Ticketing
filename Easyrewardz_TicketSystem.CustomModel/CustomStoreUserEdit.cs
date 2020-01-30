using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomStoreUserEdit
    {
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public int DesignationID { get; set; }
        public int ReporteeID { get; set; }
        public int DepartmentID { get; set; }
        public int FunctionID { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BrandIds { get; set; }
        public string CategoryIds { get; set; }
        public string SubCategoryIds { get; set; }
        public string IssuetypeIds { get; set; }
        public int IsStoreUser { get; set; }
        public string StoreBrandIDs { get; set; }
        public string StoreIDs  { get; set; }
        public bool IsClaimApprove  { get; set; }
        public int CRMRoleID { get; set; }

    }
}
