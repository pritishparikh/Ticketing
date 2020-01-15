using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Knowlege Base Master
    /// </summary>
    public class KnowlegeBaseMaster
    {
        /// <summary>
        /// KBID
        /// </summary>
        public int KBID { get; set; }
        /// <summary>
        /// KB CODE
        /// </summary>
        public string KBCODE { get; set; }
        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }
        /// <summary>
        /// Category ID
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Sub Category ID
        /// </summary>
        public int SubCategoryID { get; set; }
        /// <summary>
        /// Is Approved
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// ModifyBy
        /// </summary>
        public int ModifyBy { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        public DateTime ModifyDate { get; set; }
        /// <summary>
        /// IssueType ID
        /// </summary>
        public int IssueTypeID { get; set; }
        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }
        /// <summary>
        /// IssueType Name
        /// </summary>
        public string IssueTypeName { get; set; }

        public string IsApproveStatus { get; set; }



        public string Status { get; set; }

        public string CreatedName { get; set; }

        public string ModifyName { get; set; }
    }
}
