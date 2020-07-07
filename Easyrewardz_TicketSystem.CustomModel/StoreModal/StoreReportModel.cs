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
        public int? TaskTicketID { get; set; }
        public string DepartmentIds { get; set; }
        public string FunctionIds { get; set; }
        public string PriorityIds { get; set; }
        public bool IsTaskWithClaim { get; set; }
        public int? TaskClaimID { get; set; }
        public string TaskCreatedDate { get; set; }
        //public int? TaskCreatedBy { get; set; }
        //public int? TaskAssignedId { get; set; }

        public string TaskCreatedBy { get; set; }
        public string TaskAssignedId { get; set; }
        //ends here

        // claim search params

        public int? ClaimID { get; set; }
        public string ClaimStatus { get; set; }
        public bool IsClaimWithTicket { get; set; }
        public int? ClaimTicketID { get; set; }
        public string ClaimCategoryIds { get; set; }
        public string ClaimSubCategoryIds { get; set; }
        public string ClaimIssuetypeIds { get; set; }
        public bool IsClaimWithTask { get; set; }
        public int? ClaimTaskID { get; set; }
        public string ClaimCreatedDate { get; set; }
        //public int? ClaimCreatedBy { get; set; }
        //public int? ClaimAssignedId { get; set; }

        public string ClaimCreatedBy { get; set; }
        public string ClaimAssignedId { get; set; }

        //ends here


        // campaign search params
        public string CampaignName { get; set; }
        //public int? CampaignAssignedIds { get; set; }
        public string CampaignStartDate { get; set; }
        public string CampaignEndDate { get; set; }
        public string CampaignStatusids { get; set; }
        public string CampaignAssignedIds { get; set; }
        public int CampaignRegion { get; set; }
        public int CampaignZone { get; set; }
        //ends here

        // login user search params
        public string LoginUsersIds { get; set; }
        //public int? CampaignAssignedIds { get; set; }
        public string LoginStartDate { get; set; }
        public string LoginEndDate { get; set; }
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


    #region Store report Model



    public class SearchStoreResponseReport
    {
        public List<SearchStoreTaskReportResponse> TaskReport { get; set; }
        public List<SearchStoreClaimReportResponse> ClaimReport { get; set; }
        public List<SearchStoreCampaignReportResponse> CampaignReport { get; set; }
    }



    public class SearchStoreTaskReportResponse
    {
        public int TaskID { get; set; }
        public int TicketID { get; set; }
        public string TicketDescription { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
        public string TaskEndTime { get; set; }

        public string TaskStatus { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public string ModifiedDate { get; set; }
        public string IsActive { get; set; }

    }


    public class SearchStoreClaimReportResponse
    {
        public int ClaimID { get; set; }
        public string ClaimTitle { get; set; }
        public string ClaimDescription { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int IssueTypeID { get; set; }
        public string IssueTypeName { get; set; }
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }
        public int OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public string ClaimPercent { get; set; }
        public int ClaimAssignedID { get; set; }
        public string AssignedToName { get; set; }
        public string ClaimStatus { get; set; }
        public string IsActive { get; set; }
        public string ClaimApproved { get; set; }
        public string ClaimRejected { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public string ModifiedDate { get; set; }
        public string IsClaimEscalated { get; set; }
        public string IsCustomerResponseDone { get; set; }
        public string CustomerResponsedOn { get; set; }

        public string FinalClaimPercent { get; set; }
        public string TicketDescription { get; set; }
        public string TaskDescription { get; set; }

    }



    public class SearchStoreCampaignReportResponse
    {


        /*----------Campaign Response Columns-----------*/

        public int CampaignCustomerID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobileNumber { get; set; }
        public int CampaignTypeID { get; set; }
        public string CampaignName { get; set; }
        public string CampaignTypeDate { get; set; }
        public string CallReScheduledTo { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public string CampaignStatus { get; set; }
        public int AssignedTo { get; set; }
        public string AssignedToName { get; set; }

        public string Response { get; set; }
        public int NoOfTimesNotContacted { get; set; }

        /*---------------*/

    }

    #endregion

}
