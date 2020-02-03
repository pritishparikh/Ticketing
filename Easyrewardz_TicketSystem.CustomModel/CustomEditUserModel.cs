using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomEditUserModel
    {
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public int RoleID { get; set; }
        public int DesignationID { get; set; }
        public int ReporteeID { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }
        public bool IsCopyEscalation { get; set; }
        public bool IsAssignEscalation { get; set; }
        public int CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BrandIds { get; set; }
        public string categoryIds { get; set; }
        public string subCategoryIds { get; set; }
        public string IssuetypeIds { get; set; }
        public bool IsAgent { get; set; }
        public int EscalateAssignToId { get; set; }
        public int IsStoreUser { get; set; }
        public CustomEditUserModel()
        {
            IsStoreUser = 1;
        }
    }
}
