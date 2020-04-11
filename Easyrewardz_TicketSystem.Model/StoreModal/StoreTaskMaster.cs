using System;
using System.Collections.Generic;
using System.Text;

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
    }


    public class StoreTaskComment
    {
        public int TaskID { get; set; }
        public string Comment { get; set; }
    }

    public class TaskCommentModel
    {
        public int TaskCommentID { get; set; }
        public int TaskID { get; set; }
        public string Comment { get; set; }
        public int CommentBy { get; set; }
        public string CommentedDate { get; set; }
        public string CommentByName { get; set; }
        public string CommentedDiff { get; set; }
    }

    public class CustomTaskHistory
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string DateandTime { get; set; }
    }

    public class StoreCampaign
    {
        public int CampaignTypeID { get; set; }
        public string CampaignName { get; set; }
        public string CampaignScript { get; set; }
        public string CampaignScriptLess { get; set; }
        public int ContactCount { get; set; }
        public string CampaignEndDate { get; set; }
        public List<StoreCampaignCustomer> StoreCampaignCustomerList { get; set; }
    }

    public class StoreCampaignCustomer
    {
        public int CampaignCustomerID { get; set; }
        public int CustomerID { get; set; }
        public string CampaignTypeDate { get; set; }
        public int CampaignTypeID { get; set; }
        public int CampaignStatus { get; set; }
        public int Response { get; set; }
        public string CallReScheduledTo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmailId { get; set; }
        public List<CampaignResponse> CampaignResponseList { get; set; }
    }

    public class CampaignStatusResponse
    {
        public List<CampaignStatus> CampaignStatusList { get; set; }
        public List<CampaignResponse> CampaignResponseList { get; set; }
    }

    public class CampaignStatus
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int StatusNameID { get; set; }
    }

    public class CampaignResponse
    {
        public int ResponseID { get; set; }
        public string Response { get; set; }
        public int StatusNameID { get; set; }
    }

    public class StoreCampaignCustomerRequest
    {
        public int CampaignCustomerID { get; set; }
        public int StatusNameID { get; set; }
        public int ResponseID { get; set; }
        public string CallReScheduledTo { get; set; }
        public DateTime CallReScheduledToDate { get; set; }
    }
}
