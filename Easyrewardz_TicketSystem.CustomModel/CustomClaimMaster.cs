using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomClaimMaster
    {

        public int TicketClaimID { get; set; }
        public string TaskStatus { get; set; }
        public string ClaimIssueType { get; set; }
        public string Category { get; set; }
        public string RaisedBy { get; set; }
        public DateTime Creation_on  { get; set; }
        public string  Dateformat { get; set; }
        public string AssignName { get; set; }

        public List<ClaimCategory> claimCategory { get; set; }

    }

    public class ClaimCategory
    {
        public string BrandName { get; set; }

        public string ClaimCategoryName { get; set; }

        public string ClaimSubCategory { get; set; }

        public string ClaimIssueType { get; set; }

        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public int TenantID { get; set; }
    }
