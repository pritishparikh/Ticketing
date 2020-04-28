using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomClaimByID
    {
        public int ClaimID { get; set; }
        public int TenantID { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int IssueTypeID { get; set; }
        public string IssueTypeName { get; set; }
        public int ClaimAskFor { get; set; }
        public int CustomerID { get; set; }
        public int TicketID { get; set; }
        public int TicketingTaskID { get; set; }
        public string CustomerName  { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAlternateNumber { get; set; }
        public string EmailID { get; set; }
        public string AlternateEmailID { get; set; }
        public string Gender { get; set; }
        public int CreatedBy { get; set; }
        public string RaiseBy { get; set; }
        public string CreationOn { get; set; }
        public int AssigneeID { get; set; }
        public string AssignTo { get; set; }
        public string Status { get; set; }
        public string TargetClouserDate { get; set; }
        public List<ClaimAttachment> Attachments { get; set; }
        public List<CommentByStore> CommentByStores { get; set; }
        public List<CommentByApprovel> CommentByApprovels { get; set; }
        public CustomOrderMaster CustomOrderMaster { get; set; }
    }
    public class ClaimAttachment
    {
        public int ClaimAttachmentId { get; set; }
        public string AttachmentName { get; set; }
    }
    public class CommentByStore
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }
        public int OldAgentID { get; set; }
        public string OldAgentName { get; set; }
        public int NewAgentID { get; set; }
        public string NewAgentName { get; set; }
        public bool IsTicketingComment { get; set; }
    }
    public class CommentByApprovel
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }
        public bool IsRejectComment { get; set; }
    }
}
