using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Issue Type
    /// </summary>
    public class IssueType
    {
        /// <summary>
        /// Issue Type Id
        /// </summary>
        public int IssueTypeID { get;set;}

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get;set;}

        /// <summary>
        /// Issue Type Name
        /// </summary>
        public string IssueTypeName { get;set;}

        /// <summary>
        /// Sub category Id
        /// </summary>
        public int SubCategoryID { get; set; }

        /// <summary>
        /// Active/Inactive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifiedBy { get; set;}

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
