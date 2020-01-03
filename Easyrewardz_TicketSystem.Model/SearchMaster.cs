using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class SearchRequest
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }

        public bool isByStatus { get; set; }
        public bool isByFilter { get; set; }

        public bool ByDate { get; set; }
        public bool ByCustomerType { get; set; }
        public bool ByTicketType { get; set; }
        public bool ByCategory { get; set; }
        public bool byAll { get; set; }



        public DateTime creationDate { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public string SLADue { get; set; }
        public string ticketStatus { get; set; }




        public string customerMob { get; set; }
        public string customerEmail { get; set; }
        public int TicketID { get; set; }




        public string Priority { get; set; }
        public string chanelOfPurchase { get; set; }
        public string ticketActionType { get; set; }




        public string Category { get; set; }
        public string subCategory { get; set; }
        public string issueType { get; set; }



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

        //public int _SLAstatusResponse { get; set; }
        //public int _SLAstatusResponsetime { get; set; }
        //public int _SLAstatusResolution { get; set; }
        //public int _SLAstatusResoltiontime { get; set; }


    }

    public class SearchResponse
    {
        public int ticketID { get; set; }
        public string ticketStatus { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public string subCategory { get; set; }
        public string IssueType { get; set; }
        public string Assignee { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedOn { get; set; }

        public string creationDetails { get; set; }

        public List<string> ticketStatusCount { get; set; }


        //public string createdBy { get; set; }
        //public string updatedBy { get; set; }
        //public string assignedTo { get; set; }
        //public double assignedOn { get; set; }
        //public string updatedByAssigneeOn { get; set; }
        //public string responseTimeRemainingBy { get; set; }
        //public string responseOverdueBy { get; set; }
        //public string resolutionOverdueBy { get; set; }



    }


}

