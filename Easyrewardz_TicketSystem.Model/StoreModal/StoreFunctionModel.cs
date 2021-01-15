using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class StoreFunctionModel
    {
        /// <summary>
        /// Function ID
        /// </summary>
        public int FunctionID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Function Name
        /// </summary>
        public string FuncationName { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int? ModifiedBy { get; set; }

        /// <summary>
        /// Modified Date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
