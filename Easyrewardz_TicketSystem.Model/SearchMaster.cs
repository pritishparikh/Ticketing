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


        public int itemID { get; set; }
        public string assignedTo { get; set; }
        public bool didVisitStore { get; set; }
        public string purchaseStoreCodeAddress { get; set; }
        public string SLAstatus { get; set; }
        public bool wantToVisitStore { get; set; }
        public bool wantToVisitStoreCodeAddress { get; set; }
        public string withClaim { get; set; }
        public string withTask { get; set; }
        public string claimStatus { get; set; }
        public string taskStatus { get; set; }
        public string claimCategory { get; set; }
        public string taskDept { get; set; }
        public string claimSubcategory { get; set; }




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
        public DateTime CreatedOn { get; set; }

        public string creationDetails { get; set; }

        public TicketStatus ticketStatusCount { get; set; }
        //public string createdBy { get; set; }
        //public string updatedBy { get; set; }
        //public string assignedTo { get; set; }
        //public double assignedOn { get; set; }
        //public string updatedByAssigneeOn { get; set; }
        //public string responseTimeRemainingBy { get; set; }
        //public string responseOverdueBy { get; set; }
        //public string resolutionOverdueBy { get; set; }



    }

    public class TicketStatus
    {
        public double ticketEscalated { get; set; }
        public double ticketNew { get; set; }
        public double ticketOpen { get; set; }
        public double ticketResolved { get; set; }
        public double ticketReassigned { get; set; }
        public double ticketClosed { get; set; }
        public double AllCount { get; set; }


    }
}

