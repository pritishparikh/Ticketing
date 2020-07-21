using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomUserList
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Designation { get; set; }
        public string IsCopyEscalation { get; set; }
        public string BrandIDs { get; set; }
        public string BrandNames { get; set; }
        public string Brand_Names { get; set; }
        public string CategoryIDs { get; set; }
        public string CategoryNames { get; set; }
        public string Category_Name { get; set; }
        public string SubCategoryIDs { get; set; }
        public string SubCategoryNames { get; set; }
        public string SubCategory_Name { get; set; }
        public string IssueTypeIDs  { get; set; }
        public string IssueTypeNames { get; set; }
        public string IssueType_Name { get; set; }
        public string ReportTo { get; set; }
        public string CrmRoleName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int RoleID  { get; set; }
        public int DesignationID { get; set; }
        public int ReporteeID { get; set; }
        public bool IsActive { get; set; }
        public bool Is_CopyEscalation { get; set; }
        public bool Is_AssignEscalation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AssignEscalation { get; set; }
        public string AssignName { get; set; }
        public int AssignID { get; set; }
        public int ReporteeDesignationID { get; set; }

        public string ReporteeDesignation { get; set; }
    }
}
