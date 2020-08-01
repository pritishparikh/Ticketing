using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{

    public class StoreDashboardModel
    {
        /// <summary>
        /// task id
        /// </summary>
        public int? taskid { get; set; }

        /// <summary>
        /// Department
        /// </summary>
        public int? Department { get; set; }

        /// <summary>
        /// task title
        /// </summary>
        public string tasktitle { get; set; }

        /// <summary>
        /// task status
        /// </summary>
        public int? taskstatus { get; set; }

        /// <summary>
        /// ticket ID
        /// </summary>
        public int? ticketID { get; set; }

        /// <summary>
        /// function ID
        /// </summary>
        public int? functionID { get; set; }

        /// <summary>
        /// Created On From
        /// </summary>
        public string CreatedOnFrom { get; set; }

        /// <summary>
        /// Created On To
        /// </summary>
        public string CreatedOnTo { get; set; }

        /// <summary>
        /// Assign to Id
        /// </summary>
        public int? AssigntoId { get; set; }

        /// <summary>
        /// created ID
        /// </summary>
        public int? createdID { get; set; }

        /// <summary>
        /// task with Ticket
        /// </summary>
        public string taskwithTicket { get; set; }

        /// <summary>
        /// task with Claim
        /// </summary>
        public string taskwithClaim { get; set; }

        /// <summary>
        /// claim ID
        /// </summary>
        public int? claimID { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public int? Priority { get; set; }
    }

    public class StoreDashboardResponseModel
    {
        /// <summary>
        /// task id
        /// </summary>
        public int taskid { get; set; }

        /// <summary>
        /// Department
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// store Name
        /// </summary>
        public string storeName { get; set; }

        /// <summary>
        /// Store Address
        /// </summary>
        public string StoreAddress { get; set; }

        /// <summary>
        /// task title
        /// </summary>
        public string tasktitle { get; set; }

        /// <summary>
        /// task status
        /// </summary>
        public string taskstatus { get; set; }

        /// <summary>
        /// ticket ID
        /// </summary>
        public int ticketID { get; set; }

        /// <summary>
        /// function ID
        /// </summary>
        public int functionID { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Assign to Id
        /// </summary>
        public string AssigntoId { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// task with Ticket
        /// </summary>
        public string taskwithTicket { get; set; }

        /// <summary>
        /// task with Claim
        /// </summary>
        public string taskwithClaim { get; set; }

        /// <summary>
        /// claim ID
        /// </summary>
        public int claimID { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// total Count
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// modifed On
        /// </summary>
        public string modifedOn { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// Created ago
        /// </summary>
        public string Createdago { get; set; }

        /// <summary>
        /// Assigned ago
        /// </summary>
        public string Assignedago { get; set; }
        /// <summary>
        /// Updated ago
        /// </summary>
        public string Updatedago { get; set; }
        /// <summary>
        /// Task Closure Date
        /// </summary>
        public string TaskCloureDate { get; set; }

        /// <summary>
        /// Resolution Time Remaining
        /// </summary>
        public string ResolutionTimeRemaining { get; set; }

        /// <summary>
        /// Resolution Over due By
        /// </summary>
        public string ResolutionOverdueBy { get; set; }

        /// <summary>
        /// Color Name
        /// </summary>
        public string ColorName { get; set; }

        /// <summary>
        /// Color Code 
        /// </summary>
        public string ColorCode { get; set; }
    }




    public class StoreDashboardClaimModel
    {
        /// <summary>
        /// tenant ID
        /// </summary>
        public int tenantID { get; set; }

        /// <summary>
        /// claim ID
        /// </summary>
        public int? claimID { get; set; }

        /// <summary>
        /// ticket ID
        /// </summary>
        public int? ticketID { get; set; }

        /// <summary>
        /// claim issue Type
        /// </summary>
        public int? claimissueType { get; set; }

        /// <summary>
        /// ticket Mapped
        /// </summary>
        public int? ticketMapped { get; set; }

        /// <summary>
        /// claim sub category
        /// </summary>
        public int? claimsubcat { get; set; }

        /// <summary>
        /// assign To
        /// </summary>
        public int? assignTo { get; set; }

        /// <summary>
        /// claim category
        /// </summary>
        public int? claimcat { get; set; }

        /// <summary>
        /// claim raised date
        /// </summary>
        public string claimraiseddate { get; set; }

        /// <summary>
        /// claim raise
        /// </summary>
        public int? claimraise { get; set; }

        /// <summary>
        /// task ID
        /// </summary>
        public int? taskID { get; set; }

        /// <summary>
        /// claim status
        /// </summary>
        public int? claimstatus { get; set; }

        /// <summary>
        /// task mapped
        /// </summary>
        public int? taskmapped { get; set; }

        /// <summary>
        /// raised by
        /// </summary>
        public int? raisedby { get; set; }

        /// <summary>
        /// Brand IDs
        /// </summary>
        public string BrandIDs { get; set; }

        /// <summary>
        /// Agent Ids
        /// </summary>
        public string AgentIds { get; set; }

        /// <summary>
        /// From Date
        /// </summary>
        public string FromDate { get; set; } //'yyyy-MM-dd'

        /// <summary>
        /// To Date
        /// </summary>
        public string ToDate { get; set; } //'yyyy-MM-dd' 


    }

    public class StoreDashboardClaimResponseModel
    {
        /// <summary>
        /// Claim ID
        /// </summary>
        public int ClaimID { get; set; }

        /// <summary>
        /// Claim Status Id
        /// </summary>
        public int ClaimStatusId { get; set; }

        /// <summary>
        /// Claim Status
        /// </summary>
        public string ClaimStatus { get; set; }

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
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Claim Raised By ID
        /// </summary>
        public int ClaimRaisedByID { get; set; }

        /// <summary>
        /// Claim Raised By
        /// </summary>
        public string ClaimRaisedBy { get; set; }

        /// <summary>
        /// Creation On
        /// </summary>
        public string CreationOn { get; set; }

        /// <summary>
        /// Assigned Id
        /// </summary>
        public int AssignedId { get; set; }

        /// <summary>
        /// Assign To
        /// </summary>
        public string AssignTo { get; set; }

  

    }



}
