using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class DepartmentMaster
    {
        public int DepartmentID { get; set; }
        public int TenantID { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
