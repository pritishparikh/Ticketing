﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomClaimList
    {
        /// <summary>
        /// Claim ID
        /// </summary>
        public int ClaimID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Brand Name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Category ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Sub Category ID
        /// </summary>
        public int SubCategoryID { get; set; }

        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }

        /// <summary>
        /// Issue Type ID
        /// </summary>
        public int IssueTypeID { get; set; }

        /// <summary>
        /// Issue Type Name
        /// </summary>
        public string IssueTypeName { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Raise By
        /// </summary>
        public string RaiseBy { get; set; }

        /// <summary>
        /// Creation On
        /// </summary>
        public string CreationOn { get; set; }

        /// <summary>
        /// Creation Ago
        /// </summary>
        public string CreationAgo { get; set; }

        /// <summary>
        /// Assign To
        /// </summary>
        public string AssignTo { get; set; }

        /// <summary>
        /// Assign On
        /// </summary>
        public string AssignOn { get; set; }

        /// <summary>
        /// Modify On
        /// </summary>
        public string ModifyOn { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Modify By
        /// </summary>
        public string ModifyBy { get; set; }
    }
}
