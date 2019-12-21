using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Sub category
    /// </summary>
   public class SubCategory
    {
        /// <summary>
        /// Subcategory Id
        /// </summary>
        public int SubCategoryID { get; set; }

        /// <summary>
        /// Sub category Name
        /// </summary>
        public string SubCategoryName { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// IsActive/Active
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
