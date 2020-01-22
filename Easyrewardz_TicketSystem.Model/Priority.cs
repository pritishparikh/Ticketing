using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Pritority 
    /// </summary>
   public class Priority
    {
        /// <summary>
        /// Priority Id
        /// </summary>
        public int PriorityID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Priority Name
        /// </summary>
        public string PriortyName { get; set; }

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
        /// CreatedDateFormated
        /// </summary>
        public string CreatedDateFormated { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifiedBy { get; set; }

        /// <summary>
        /// Modified Date
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Modified Date Formated
        /// </summary>
        public string ModifiedDateFormated { get; set; }
        /// <summary>
        /// Created By Name
        /// </summary>
        /// 
        public string CreatedByName { get; set; }

        /// <summary>
        /// Modified By Name
        /// </summary>
        public string ModifiedByName { get; set; }

        public string PriortyStatus { get; set; }
    }
}
