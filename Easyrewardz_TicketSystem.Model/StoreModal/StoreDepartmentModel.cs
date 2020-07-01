using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class StoreDepartmentModel
    {
            /// <summary>
            /// Department ID
            /// </summary>
            public int DepartmentID { get; set; }

            /// <summary>
            /// Tenant ID
            /// </summary>
            public int TenantID { get; set; }

            /// <summary>
            /// Department Name
            /// </summary>
            public string DepartmentName { get; set; }
            
            /// <summary>
            /// Is Active
            /// </summary>
            public bool IsActive { get; set; }

            /// <summary>
            /// Created By
            /// </summary>
            public int CreatedBy { get; set; }

            /// <summary>
            /// Created Date
            /// </summary>
            public DateTime CreatedDate { get; set; }

            /// <summary>
            /// Modify By
            /// </summary>
            public int? ModifyBy { get; set; }

            /// <summary>
            /// Modified Date
            /// </summary>
            public DateTime? ModifiedDate { get; set; }
            
            /// <summary>
            /// create Department Models
            /// </summary>
            public List<CreateDepartmentModel> createDepartmentModels { get; set; }

    }

    public class DepartmentListingModel
    {
        /// <summary>
        /// Department Brand Mapping ID
        /// </summary>
        public int DepartmentBrandMappingID { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Brand Name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Store ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// Store Code
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Function ID
        /// </summary>
        public int FunctionID { get; set; }

        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Modified Date
        /// </summary>
        public string ModifiedDate { get; set; }
    }

    public class StoreCodeModel
    {
        /// <summary>
        /// Store ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Store Code
        /// </summary>
        public string StoreCode { get; set; }

    }

    public class CreateStoreDepartmentModel
    {
        /// <summary>
        /// Department Brand ID
        /// </summary>
        public int DepartmentBrandID { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public string BrandID { get; set; }

        /// <summary>
        /// Store ID
        /// </summary>
        public string StoreID { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Function ID
        /// </summary>
        public int FunctionID { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

    }

}
