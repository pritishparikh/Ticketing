using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel.StoreModal
{
    public class StoreReportModel
    {

        public int ActiveTabId { get; set; }
        public int TenantID { get; set; }

        // task search params

        public string TaskTitle { get; set; }
        public string TaskStatus { get; set; }
        public bool IsTaskWithTicket { get; set; }
        public int TaskTicketID { get; set; }
        public string DepartmentIds { get; set; }
        public string FunctionIds { get; set; }
        public string PriorityIds { get; set; }
        public bool IsTaskWithClaim { get; set; }
        public int TaskClaimID { get; set; }
        public string TaskCreatedDate { get; set; }
        public int TaskCreatedBy { get; set; }
        public int TaskAssignedId { get; set; }

        //ends here

        // claim search params

        public int ClaimID { get; set; }
        public string ClaimStatus { get; set; }
        public bool IsClaimWithTicket { get; set; }
        public int ClaimTicketID { get; set; }
        public string ClaimCategoryIds { get; set; }
        public string ClaimSubCategoryIds { get; set; }
        public string ClaimIssuetypeIds { get; set; }
        public bool IsClaimWithTask { get; set; }
        public int ClaimTaskID { get; set; }
        public string ClaimCreatedDate { get; set; }
        public int ClaimCreatedBy { get; set; }
        public int ClaimAssignedId { get; set; }

        //ends here
         

        // cmapaign search params
        public string CampaignName { get; set; }
        public int CampaignAssignedIds { get; set; }
        public string CampaignStartDate { get; set; }
        public string CampaignEndDate { get; set; }
        public string CampaignStatusids { get; set; }

        //ends here
        
    }

    public class StoreReportRequest
    {
        public int ReportID { get; set; }
        public int TenantID { get; set; }
        public string ReportName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int ScheduleID { get; set; }
        public string StoreReportSearchParams { get; set; }

    }

}
