using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Pritority 
    /// </summary>
   public class SLA
    {
        /// <summary>
        /// Priority Id
        /// </summary>
        public int SLAID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Priority Name
        /// </summary>
        public int IssueTypeID { get; set; }

        /// <summary>
        /// Function Id
        /// </summary>
        public int FunctionID { get; set; }


        /// <summary>
        /// Active/InActive
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
        /// Modified By
        /// </summary>
        public int ModifiedBy { get; set; }

        /// <summary>
        /// Modified Date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        
        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Modified By Name
        /// </summary>
        public string ModifiedByName { get; set; }

    }
}
