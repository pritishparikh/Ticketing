using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class StoreDepartmentModel
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

    public class DepartmentListingModel
    {
        public int DepartmentBrandMappingID { get; set; }
        public int BrandID { get; set; }

        public string BrandName { get; set; }

        public int StoreID { get; set; }

        public string StoreCode { get; set; }

        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public int FunctionID { get; set; }

        public string FunctionName { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class StoreCodeModel
    {
        public int StoreID { get; set; }
        public int BrandID { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }

    }

    public class CreateStoreDepartmentModel
    {
        public int DepartmentBrandID { get; set; }

        public string BrandID { get; set; }

        public string StoreID { get; set; }

        public int DepartmentID { get; set; }

        public int FunctionID { get; set; }

        public bool Status { get; set; }

        public int TenantID { get; set; }

        public int CreatedBy { get; set; }

    }

}
