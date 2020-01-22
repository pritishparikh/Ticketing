using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Brand
    /// </summary>
    public class Brand
    {
        /// <summary>
        /// Brand Id 
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Brand Name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Brand Code 
        /// </summary>
        public string BrandCode { get; set; }

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
        /// CreatedOn Format
        /// </summary>
        public string CreatedDateFormat { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifyBy { get; set; }
       

        /// <summary>
        /// Modified On
        /// </summary>
        public DateTime ModifyDate { get; set; }
        /// <summary>
        /// ModifyDate Format
        /// </summary>
        public string ModifyDateFormat { get; set; }

        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Modifeid By Name
        /// </summary>
        public string ModifiedByName { get; set; }

        public string Status { get; set; }
        /// <summary>
        /// Modified By
        /// </summary>
        public string Modify_By { get; set; }
        /// <summary>
        /// Created_By
        /// </summary>
        public string Created_By { get; set; }

    }
}
