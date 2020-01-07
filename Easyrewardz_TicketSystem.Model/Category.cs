using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Category
    /// </summary>
    public class Category
    {
        /// <summary>        
        /// Category Id
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Active/Inactive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Cateated By
        /// </summary>
        public Nullable<int> CreatedBy { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int? ModifyBy { get; set; }

        /// <summary>
        /// Modified On
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Modified By Name
        /// </summary>
        public string ModifiedByName { get; set; }


        public string Status { get; set; }

    }
}
