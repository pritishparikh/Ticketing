using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomGetChatTickets
    {
        public int TicketID { get; set; }
        public string TicketStatus { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string IssueType { get; set; }
        public string AssignTo { get; set; }
        public string Priority { get; set; }
        public string CreatedOn { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }

    public class GetChatTicketsByID 
    {
        public int TicketID { get; set; }
        public int ? TicketStatus { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string IssueType { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int IssueTypeID { get; set; }
        public string Brand { get; set; }
        public string AssignTo { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobileNumber { get; set; }
        public string Priority { get; set; }
        public string CreatedOn { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public List<ChatTicketNotes> ChatTicketNote { get; set; }
    }

    public class ChatTicketNotes
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }
    }

    public class CreateChatTickets
    {
        public int TenantID { get; set; }
        public int? TicketStatus { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string IssueType { get; set; }
        public string Brand { get; set; }
        public string StoreCode { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerMobileNumber { get; set; }
        public string Priority { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
