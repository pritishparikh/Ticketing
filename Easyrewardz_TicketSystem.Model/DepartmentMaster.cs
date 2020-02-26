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

        public List<CreateDepartmentModel> createDepartmentModels { get; set; }

    }

    public class CreateDepartmentModel
    {
        public string BrandID { get; set; }

        public string StoreID { get; set; }

        public string DepartmentID { get; set; }

        public string FunctionID { get; set; }

        public string Status { get; set; }

        public int TenantID { get; set; }

        public int CreatedBy { get; set; }

    }
}
