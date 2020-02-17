using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class SearchRequest
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }

        public bool isEscalation { get; set; }
        public bool isByStatus { get; set; }
        public bool isByFilter { get; set; }

        public int tenantID { get; set; }
        public bool ByDate { get; set; }
        public bool ByCustomerType { get; set; }
        public bool ByTicketType { get; set; }
        public bool ByCategory { get; set; }
        public bool byAll { get; set; }



        public DateTime creationDate { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public string SLADue { get; set; }
        public int ticketStatus { get; set; }




        public string customerMob { get; set; }
        public string customerEmail { get; set; }
        public int TicketID { get; set; }




        public int Priority { get; set; }
        public string chanelOfPurchase { get; set; }
        public string ticketActionType { get; set; }




        public int Category { get; set; }
        public int subCategory { get; set; }
        public int issueType { get; set; }



        public string ticketSource { get; set; }
        public int claimID { get; set; }
        public string ticketTitle { get; set; }


        public int itemID { get; set; }
        public int invoiceSubOrderNo { get; set; }
        public int assignedTo { get; set; }
        public bool didVisitStore { get; set; }
        public int purchaseStoreCodeAddress { get; set; }
        public string SLAstatus { get; set; }
        public bool wantToVisitStore { get; set; }
        public bool wantToVisitStoreCodeAddress { get; set; }
        public bool withClaim { get; set; } //boolean if claim exist or not
        public bool withTask { get; set; }//boolean if task exist or not
        public int claimStatus { get; set; }
        public int taskStatus { get; set; }
        public int claimCategory { get; set; }
        public int taskDept { get; set; }
        public int claimSubcategory { get; set; }
        public int claimIssuetype { get; set; }
        public int taskFunction { get; set; }

    }

    public class SearchResponse
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

    //public class TicketCreationDetails
    //{
    //    public string createdBy { get; set; }
    //    public string createdago { get; set; }
    //    public string assignedTo { get; set; }
    //    public string assignedago { get; set; }
    //    public string updatedBy { get; set; }
    //    public string updatedago { get; set; }
    //    public string responseTimeRemainingBy { get; set; }
    //    public string responseOverdueBy { get; set; }
    //    public string resolutionOverdueBy { get; set; }
    //}


    
}

