using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomClaimByID
    {
        /// <summary>
        /// Claim ID
        /// </summary>
        public int ClaimID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

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
        /// Claim Ask For
        /// </summary>
        public decimal ClaimAskFor { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Ticket ID
        /// </summary>
        public int TicketID { get; set; }

        /// <summary>
        /// Ticketing Task ID
        /// </summary>
        public int TicketingTaskID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName  { get; set; }

        /// <summary>
        /// Customer Phone Number
        /// </summary>
        public string CustomerPhoneNumber { get; set; }

        /// <summary>
        /// Customer Alternate Number
        /// </summary>
        public string CustomerAlternateNumber { get; set; }

        /// <summary>
        /// Email ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// Alternate Email ID
        /// </summary>
        public string AlternateEmailID { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Raise By
        /// </summary>
        public string RaiseBy { get; set; }

        /// <summary>
        /// Creation On
        /// </summary>
        public string CreationOn { get; set; }

        /// <summary>
        /// Assignee ID
        /// </summary>
        public int AssigneeID { get; set; }

        /// <summary>
        /// Assign To
        /// </summary>
        public string AssignTo { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Target Clouser Date
        /// </summary>
        public string TargetClouserDate { get; set; }

        /// <summary>
        /// Attachments
        /// </summary>
        public List<ClaimAttachment> Attachments { get; set; }

        /// <summary>
        /// Comment By Stores
        /// </summary>
        public List<CommentByStore> CommentByStores { get; set; }

        /// <summary>
        /// Comment By Approvels
        /// </summary>
        public List<CommentByApprovel> CommentByApprovels { get; set; }

        /// <summary>
        /// Custom Order Master
        /// </summary>
        public CustomOrderMaster CustomOrderMaster { get; set; }

        /// <summary>
        /// Can Approveclaim 
        /// </summary>
        public bool CanApproveclaim { get; set; }

    }
    public class ClaimAttachment
    {
        /// <summary>
        /// Claim Attachment Id
        /// </summary>
        public int ClaimAttachmentId { get; set; }

        /// <summary>
        /// Attachment Name
        /// </summary>
        public string AttachmentName { get; set; }
    }
    public class CommentByStore
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Comment Date
        /// </summary>
        public string CommentDate { get; set; }

        /// <summary>
        /// Old Agent ID
        /// </summary>
        public int OldAgentID { get; set; }

        /// <summary>
        /// Old Agent Name
        /// </summary>
        public string OldAgentName { get; set; }

        /// <summary>
        /// New Agent ID
        /// </summary>
        public int NewAgentID { get; set; }

        /// <summary>
        /// New Agent Name
        /// </summary>
        public string NewAgentName { get; set; }

        /// <summary>
        /// Is Ticketing Comment
        /// </summary>
        public bool IsTicketingComment { get; set; }
    }
    public class CommentByApprovel
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Comment Date
        /// </summary>
        public string CommentDate { get; set; }

        /// <summary>
        /// Is Reject Comment
        /// </summary>
        public bool IsRejectComment { get; set; }
    }
}
