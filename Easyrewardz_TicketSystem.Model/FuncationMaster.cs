using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class FuncationMaster
    {
        public int FunctionID { get; set; }
        public int TenantID { get; set; }
        public int DepartmentID { get; set; }
        public string FuncationName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
