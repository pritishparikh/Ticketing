using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Task Master 
    /// </summary>
    public class StoreTaskMaster
    {
        /// <summary>
        /// Ticketing TaskID
        /// </summary>
        public int TaskID { get; set; }
        /// <summary>
        /// Ticket ID
        /// </summary>
        public int TicketID { get; set; }
        /// <summary>
        /// Task Title
        /// </summary>
        public string TaskTitle { get; set; }
        /// <summary>
        /// Task Description
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// Department Id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// Assign To ID
        /// </summary>
        public int AssignToID { get; set; }
        /// <summary>
        /// Assign To Name
        /// </summary>
        public string AssignToName { get; set; }
        /// <summary>
        ///Priority ID
        /// </summary>
        public int PriorityID { get; set; }
        /// <summary>
        /// Function ID
        /// </summary>
        public int FunctionID { get; set; }
        /// <summary>
        /// Task End Time
        /// </summary>
        public DateTime TaskEndTime { get; set; }
        /// <summary>
        /// Task Status Id
        /// </summary>
        public int TaskStatusId { get; set; }
        /// <summary>
        /// Task Status Name
        /// </summary>
        public string TaskStatusName { get; set; }
        /// <summary>
        ///Created By
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }
        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        ///Modified Date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        ///Task Comments
        /// </summary>
        public string TaskComments { get; set; }
        /// <summary>
        ///Task Comments
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        ///Task Comments
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        ///Task Comments
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// CanEdit
        /// </summary>
        public int CanEdit { get; set; }
        /// <summary>
        /// CanSubmit
        /// </summary>
        public int CanSubmit { get; set; }
        /// <summary>
        /// IsAssignTo
        /// </summary>
        public int IsAssignTo { get; set; }
    }

    public class TaskFilterRaisedBymeModel
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

        /// <summary>
        /// user id
        /// </summary>
        public int? userid { get; set; }
    }

    public class TaskFilterRaisedBymeResponseModel
    {

        /// <summary>
        /// Ticketing TaskID
        /// </summary>
        public int StoreTaskID { get; set; }
        /// <summary>
        /// Task Status
        /// </summary>
        public string TaskStatus { get; set; }
        /// <summary>
        /// Task Title
        /// </summary>
        public string TaskTitle { get; set; }
        /// <summary>
        /// Task Description
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Store Code
        /// </summary>
        public int? StoreCode { get; set; }
        /// <summary>
        ///Created By
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Assign Name
        /// </summary>
        public string Assignto { get; set; }
        /// <summary>
        ///Due date
        /// </summary>
        public DateTime Duedate { get; set; }
        /// <summary>
        ///Priority
        /// </summary>
        public string PriorityName { get; set; }
        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Store Address
        /// </summary>
        public string StoreAddress { get; set; }
        /// <summary>
        /// CreationOn
        /// </summary>
        public string CreationOn { get; set; }
        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// total Count
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// Created ago
        /// </summary>
        public string Createdago { get; set; }

        /// <summary>
        /// Assigned ago
        /// </summary>
        public string Assignedago { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public string UpdatedBy { get; set; }

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

        /// <summary>
        /// Ticket ID
        /// </summary>
        public int TicketID { get; set; }
    }


    public class TaskFilterAssignBymeModel
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

        /// <summary>
        /// user id
        /// </summary>
        public int? userid { get; set; }
    }

    public class TaskFilterAssignBymeResponseModel
    {

        /// <summary>
        /// Ticketing TaskID
        /// </summary>
        public int StoreTaskID { get; set; }
        /// <summary>
        /// TaskStatus
        /// </summary>
        public string TaskStatus { get; set; }
        /// <summary>
        /// Task Title
        /// </summary>
        public string TaskTitle { get; set; }
        /// <summary>
        /// Task Description
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// StoreCode
        /// </summary>
        public int? StoreCode { get; set; }
        /// <summary>
        ///Created By
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Assign Name
        /// </summary>
        public string Assignto { get; set; }
        /// <summary>
        ///Duedate
        /// </summary>
        public DateTime Duedate { get; set; }
        /// <summary>
        ///Priority
        /// </summary>
        public string PriorityName { get; set; }
        /// <summary>
        /// StoreName
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// StoreAddress
        /// </summary>
        public string StoreAddress { get; set; }
        /// <summary>
        /// CreationOn
        /// </summary>
        public string CreationOn { get; set; }
        /// <summary>
        /// FunctionName
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// total Count
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// Created ago
        /// </summary>
        public string Createdago { get; set; }

        /// <summary>
        /// Assigned ago
        /// </summary>
        public string Assignedago { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public string UpdatedBy { get; set; }

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

        /// <summary>
        /// Ticket ID
        /// </summary>
        public int TicketID { get; set; }
    }






    public class TaskFilterTicketByModel
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

    public class TaskFilterTicketByResponseModel
    {

        /// <summary>
        /// Ticketing TaskID
        /// </summary>
        public int StoreTaskID { get; set; }
        /// <summary>
        /// TaskStatus
        /// </summary>
        public string TaskStatus { get; set; }
        /// <summary>
        /// Task Title
        /// </summary>
        public string TaskTitle { get; set; }
        /// <summary>
        /// Task Description
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// StoreCode
        /// </summary>
        public int? StoreCode { get; set; }
        /// <summary>
        ///Created By
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Assign Name
        /// </summary>
        public string Assignto { get; set; }
        /// <summary>
        ///Due date
        /// </summary>
        public DateTime Duedate { get; set; }
        /// <summary>
        ///Priority
        /// </summary>
        public string PriorityName { get; set; }
        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Store Address
        /// </summary>
        public string StoreAddress { get; set; }

        /// <summary>
        /// Creation On
        /// </summary>
        public string CreationOn { get; set; }

        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// total Count
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// Created ago
        /// </summary>
        public string Createdago { get; set; }

        /// <summary>
        /// Assigned ago
        /// </summary>
        public string Assignedago { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public string UpdatedBy { get; set; }

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

        /// <summary>
        /// Ticket ID
        /// </summary>
        public int TicketID { get; set; }
    }


    public class StoreTaskComment
    {
        /// <summary>
        /// Task ID
        /// </summary>
        public int TaskID { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Task For
        /// </summary>
        public int TaskFor { get; set; }
    }

    public class TaskCommentModel
    {
        /// <summary>
        /// Task Comment ID
        /// </summary>
        public int TaskCommentID { get; set; }

        /// <summary>
        /// Task ID
        /// </summary>
        public int TaskID { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Comment By
        /// </summary>
        public int CommentBy { get; set; }

        /// <summary>
        /// Commented Date
        /// </summary>
        public string CommentedDate { get; set; }

        /// <summary>
        /// Comment By Name
        /// </summary>
        public string CommentByName { get; set; }

        /// <summary>
        /// Commented Diff
        /// </summary>
        public string CommentedDiff { get; set; }

        /// <summary>
        /// Is Comment On Assign
        /// </summary>
        public int IsCommentOnAssign { get; set; }

        /// <summary>
        /// New Agent ID
        /// </summary>
        public int NewAgentID { get; set; }

        /// <summary>
        /// New Agent Name
        /// </summary>
        public string NewAgentName { get; set; }

        /// <summary>
        /// Old Agent ID
        /// </summary>
        public int OldAgentID { get; set; }

        /// <summary>
        /// Old Agent Name
        /// </summary>
        public string OldAgentName { get; set; }
    }

    public class CustomTaskHistory
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Date and Time
        /// </summary>
        public string DateandTime { get; set; }
    }

    public class StoreCampaign
    {
        /// <summary>
        /// Campaign Type ID
        /// </summary>
        public int CampaignTypeID { get; set; }

        /// <summary>
        /// Campaign Name
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// Campaign Script
        /// </summary>
        public string CampaignScript { get; set; }

        /// <summary>
        /// Campaign Script Less
        /// </summary>
        public string CampaignScriptLess { get; set; }

        /// <summary>
        /// Contact Count
        /// </summary>
        public int ContactCount { get; set; }

        /// <summary>
        /// Campaign End Date
        /// </summary>
        public string CampaignEndDate { get; set; }

        /// <summary>
        /// Store Campaign Customer List
        /// </summary>
        public List<StoreCampaignCustomer> StoreCampaignCustomerList { get; set; }
    }

    public class StoreCampaignCustomer
    {
        /// <summary>
        /// Campaign Customer ID
        /// </summary>
        public int CampaignCustomerID { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Campaign Type Date
        /// </summary>
        public string CampaignTypeDate { get; set; }

        /// <summary>
        /// Campaign Type ID
        /// </summary>
        public int CampaignTypeID { get; set; }

        /// <summary>
        /// Campaign Status
        /// </summary>
        public int CampaignStatus { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public int Response { get; set; }

        /// <summary>
        /// Call Re Scheduled To
        /// </summary>
        public string CallReScheduledTo { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Phone Number
        /// </summary>
        public string CustomerPhoneNumber { get; set; }

        /// <summary>
        /// Customer Email Id
        /// </summary>
        public string CustomerEmailId { get; set; }

        /// <summary>
        /// Date Of Birth 
        /// </summary>
        public string DOB { get; set; }

        /// <summary>
        /// No Of Times Not Contacted
        /// </summary>
        public int NoOfTimesNotContacted { get; set; }



        /// <summary>
        /// Is RescdehuleCall Disabled
        /// </summary>
        public bool IsRescheduleCallDisabled { get; set; }

        /// <summary>
        /// Campaign Response List
        /// </summary>
        public List<CampaignResponse> CampaignResponseList { get; set; }
    }

    public class CampaignStatusResponse
    {
        /// <summary>
        /// Campaign Status List
        /// </summary>
        public List<CampaignStatus> CampaignStatusList { get; set; }

        /// <summary>
        /// Campaign Response List
        /// </summary>
        public List<CampaignResponse> CampaignResponseList { get; set; }
    }

    public class CampaignStatus
    {
        /// <summary>
        /// Status ID
        /// </summary>
        public int StatusID { get; set; }

        /// <summary>
        /// Status Name
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Status Name ID
        /// </summary>
        public int StatusNameID { get; set; }
    }

    public class CampaignResponse
    {
        /// <summary>
        /// Response ID
        /// </summary>
        public int ResponseID { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Status Name ID
        /// </summary>
        public int StatusNameID { get; set; }
    }

    public class StoreCampaignCustomerRequest
    {
        /// <summary>
        /// Campaign Customer ID
        /// </summary>
        public int CampaignCustomerID { get; set; }

        /// <summary>
        /// Status Name ID
        /// </summary>
        public int StatusNameID { get; set; }

        /// <summary>
        /// Response ID
        /// </summary>
        public int ResponseID { get; set; }

        /// <summary>
        /// Call ReScheduled To
        /// </summary>
        public string CallReScheduledTo { get; set; }

        /// <summary>
        /// Call ReScheduled To Date
        /// </summary>
        public DateTime? CallReScheduledToDate { get; set; }
    }

    public class TaskTicketDetails
    {
        /// <summary>
        /// Ticket Id
        /// </summary>
        public int TicketID { get; set; }
        /// <summary>
        /// Ticket Title
        /// </summary>
        public string TicketTitle { get; set; }
        /// <summary>
        /// Ticket Description
        /// </summary>
        public string Ticketdescription { get; set; }
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public string Ticketnotes { get; set; }
        /// <summary>
        /// Brand Id
        /// </summary>
        public int BrandID { get; set; }
        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Subcategory Id
        /// </summary>
        public int SubCategoryID { get; set; }
        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }
        /// <summary>
        /// Issue Type Id
        /// </summary>
        public int IssueTypeID { get; set; }
        /// <summary>
        /// Issue Type Name
        /// </summary>
        public string IssueTypeName { get; set; }
        /// <summary>
        /// Priority Id
        /// </summary>
        public int PriortyID { get; set; }
        /// <summary>
        /// Priorty Name
        /// </summary>
        public string PriortyName { get; set; }
        /// <summary>
        /// Customer Id
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerPhoneNumber
        /// </summary>
        public string CustomerPhoneNumber { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// AltNumber
        /// </summary>
        public string AltNumber { get; set; }
        /// <summary>
        /// CustomerEmailId
        /// </summary>
        public string CustomerEmailId { get; set; }
        /// <summary>
        /// UpdateDate
        /// </summary>
        public string UpdateDate { get; set; }       
        /// <summary>
        /// TargetClouredate
        /// </summary>
        public string TicketAssignDate { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// ChannelOfPurchaseID
        /// </summary>
        public int ChannelOfPurchaseID { get; set; }
        /// <summary>
        /// AssignedID
        /// </summary>
        public int AssignedID { get; set; }
        /// <summary>
        /// TicketActionID
        /// </summary>
        public int TicketActionTypeID { get; set; }
        /// <summary>
        /// OrderMasterID
        /// </summary>
        public int OrderMasterID { get; set; }
        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Updated By
        /// </summary>
        public int UpdatedBy { get; set; }
        /// <summary>
        /// Updated Date
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// StoreID
        /// </summary>
        public string StoreID { get; set; }
        /// <summary>
        /// StoreNames
        /// </summary>
        public string StoreNames { get; set; }
        /// <summary>
        /// ProductID
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// ProductNames
        /// </summary>
        public string ProductNames { get; set; }
        /// <summary>
        /// User Email ID
        /// </summary>
        public string UserEmailID { get; set; }
        /// <summary>
        /// Order Item ID
        /// </summary>
        public string OrderItemID { get; set; }
    }

    public class StoreTaskWithTicket
    {
        /// <summary>
        /// Store Task Master Details
        /// </summary>
        public StoreTaskMaster StoreTaskMasterDetails { get; set; }

        /// <summary>
        /// Task Ticket Details
        /// </summary>
        public TaskTicketDetails TaskTicketDetails { get; set; }
    }

    public class StoreTaskProcressBar
    {
        /// <summary>
        /// Progress
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// Progress In
        /// </summary>
        public string ProgressIn { get; set; }

        /// <summary>
        /// Remaining Time
        /// </summary>
        public string RemainingTime { get; set; }

        /// <summary>
        /// Closure Task Date
        /// </summary>
        public string ClosureTaskDate { get; set; }

        /// <summary>
        /// Color Name
        /// </summary>
        public string ColorName { get; set; }

        /// <summary>
        /// Color Code
        /// </summary>
        public string ColorCode { get; set; }
    }

    public class AssignTaskModel
    {
        /// <summary>
        /// Task ID
        /// </summary>
        public string TaskID { get; set; }

        /// <summary>
        /// Agent ID
        /// </summary>
        public int AgentID { get; set; }

        /// <summary>
        /// Comment On Assign
        /// </summary>
        public string CommentOnAssign { get; set; }

        /// <summary>
        /// Is Comment On Assign
        /// </summary>
        public int IsCommentOnAssign { get; set; }

        /// <summary>
        /// Old Agent ID
        /// </summary>
        public int OldAgentID { get; set; }
    }

}
