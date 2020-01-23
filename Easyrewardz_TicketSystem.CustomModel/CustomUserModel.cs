using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomUserModel
    {
        public string BrandIds { get; set; }
        public string categoryIds { get; set; }
        public string subCategoryIds { get; set; }
        public string IssuetypeIds { get; set; }
        public int RoleID { get; set; }
        public bool IsCopyEscalation { get; set; }
        public bool IsAssignEscalation { get; set; }
        public bool IsAgent  { get; set; }
        public bool IsActive { get; set; }
        public int  UserId  { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int EscalateAssignToId { get; set; }
    }
}
