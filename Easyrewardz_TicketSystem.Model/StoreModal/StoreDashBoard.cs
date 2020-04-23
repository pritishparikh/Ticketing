using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{

    public class StoreDashboardModel
    {
        public int taskid { get; set; }
        public int Department { get; set; }      
        public string tasktitle { get; set; }
        public int taskstatus { get; set; }
        public int ticketID { get; set; }
        public int functionID { get; set; }
        public string CreatedOnFrom { get; set; }
        public string CreatedOnTo { get; set; }
        public int AssigntoId { get; set; }
        public int createdID { get; set; }
        public string taskwithTicket { get; set; }
        public string taskwithClaim { get; set; }
        public int claimID { get; set; }
        public int Priority { get; set; }
    }

    public class StoreDashboardResponseModel
    {
        public int taskid { get; set; }
        public string Department { get; set; }
        public string storeName { get; set; }
        public string StoreAddress { get; set; }
        public string tasktitle { get; set; }
        public string taskstatus { get; set; }
        public int ticketID { get; set; }
        public int functionID { get; set; }
        public string CreatedOn { get; set; }
        public string AssigntoId { get; set; }
        public string CreatedBy { get; set; }
        public string taskwithTicket { get; set; }
        public string taskwithClaim { get; set; }
        public int claimID { get; set; }
        public string Priority { get; set; }
        public int totalCount { get; set; }
        public string modifedOn { get; set; }
        public string ModifiedBy { get; set; }
    }




    public class StoreDashboardClaimModel
    {
        public int tenantID { get; set; }

        public int claimID { get; set; }
        public int ticketID { get; set; }
        public int claimissueType { get; set; }
        public int ticketMapped { get; set; }
        public int claimsubcat { get; set; }
        public int assignTo { get; set; }
        public int claimcat { get; set; }
        public string claimraiseddate { get; set; }
        public int taskID { get; set; }
        public int claimstatus { get; set; }
        public int taskmapped { get; set; }
        public int raisedby { get; set; }


    }

    public class StoreDashboardClaimResponseModel
    {
        /*

        public string claimID { get; set; }
        public int ticketID { get; set; }

        public string claimissueType { get; set; }
        public int ticketMapped { get; set; }
        public string claimsubcat { get; set; }
        public string claimcat { get; set; }
        public int claimraise { get; set; }
        public int taskID { get; set; }
        public string claimstatus { get; set; }
        public int taskmapped { get; set; }
        public int raisedby { get; set; }
        public string CreatedOn { get; set; }
        public string AssigntoId { get; set; }
        public string createdID { get; set; }
        public int totalCount { get; set; }

        public string modifedOn { get; set; }
        public int modifiedID { get; set; }

    */

        public int ClaimID { get; set; }
        public int ClaimStatusId { get; set; }
        public string ClaimStatus { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int IssueTypeID { get; set; }
        public string IssueTypeName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public int ClaimRaisedByID { get; set; }
        public string ClaimRaisedBy { get; set; }
        public string CreationOn { get; set; }
        public int AssignedId { get; set; }
        public string AssignTo { get; set; }

    }




}
