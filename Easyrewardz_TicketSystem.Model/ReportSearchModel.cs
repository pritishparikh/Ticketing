using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class ReportSearchModel
    {
        public int TenantID { get; set; }
        public int curentUserId { get; set; }
        public string AssigntoId { get; set; }
        public string BrandId { get; set; }
        public int ActiveTabId { get; set; }
        public ReportSearchData reportSearch { get; set; }
    }

    public class ReportSearchData
    {
        //Column -1
        public int? BrandID { get; set; }

        public int? IssueType { get; set; }

        public int? TaskPriority { get; set; }

        public string CreatedDate { get; set; }

        public string ModifiedDate { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public int? IssueTypeId { get; set; }

        //Column -2 
        public int? TicketSourceTypeID { get; set; }

        public string TicketIdORTitle { get; set; }

        public int? PriorityId { get; set; }

        public int? TicketSatutsID { get; set; }

        public string SLAStatus { get; set; }

        //Column - 3  
        public string ClaimId { get; set; }

        public string InvoiceNumberORSubOrderNo { get; set; }

        public int? OrderItemId { get; set; }

        public string IsVisitStore { get; set; }

        public string IsWantVistingStore { get; set; }

        //Column - 4  
        public string CustomerEmailID { get; set; }

        public string CustomerMobileNo { get; set; }

        public string AssignTo { get; set; }

        public string StoreCodeORAddress { get; set; }

        public string WantToStoreCodeORAddress { get; set; }

        //Row - 2 and Column - 1  
        public int? HaveClaim { get; set; }

        public int? ClaimStatusId { get; set; }

        public int? ClaimCategoryId { get; set; }

        public int? ClaimSubCategoryId { get; set; }

        public int? ClaimIssueTypeId { get; set; }

        //Row - 2 and Column - 2  
        public int? HaveTask { get; set; }

        public int? TaskStatusId { get; set; }

        public int? TaskDepartment_Id { get; set; }

        public int? TaskFunction_Id { get; set; }

    }

    public class SearchResponseReport
    {
        public double totalpages { get; set; }
        public int ticketID { get; set; }
        public string ticketStatus { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public string subCategory { get; set; }
        public string IssueType { get; set; }
        public string Assignee { get; set; }
        public string Priority { get; set; }
        public string CreatedOn { get; set; }
        public int isEscalation { get; set; }
        public string ClaimStatus { get; set; }
        public string TaskStatus { get; set; }
        public int TicketCommentCount { get; set; }

        public string createdBy { get; set; }
        public string createdago { get; set; }
        public string assignedTo { get; set; }
        public string assignedago { get; set; }
        public string updatedBy { get; set; }
        public string updatedago { get; set; }
        public string responseTimeRemainingBy { get; set; }
        public string responseOverdueBy { get; set; }
        public string resolutionOverdueBy { get; set; }
        public string ticketSourceType { get; set; }
        public int? ticketSourceTypeID { get; set; }
        public bool IsReassigned { get; set; }
    }
}
