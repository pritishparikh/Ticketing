using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomGetChatTickets
    {
        /// <summary>
        /// Ticket ID
        /// </summary>
        public int TicketID { get; set; }

        /// <summary>
        /// Ticket Status
        /// </summary>
        public string TicketStatus { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Sub Category
        /// </summary>
        public string SubCategory { get; set; }

        /// <summary>
        /// Issue Type
        /// </summary>
        public string IssueType { get; set; }

        /// <summary>
        /// Assign To
        /// </summary>
        public string AssignTo { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Ticket Title
        /// </summary>
        public string TicketTitle { get; set; }

        /// <summary>
        /// Ticket Description
        /// </summary>
        public string TicketDescription { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Updated Date
        /// </summary>
        public string UpdatedDate { get; set; }
    }

    public class GetChatTicketsByID 
    {
        /// <summary>
        /// TicketID
        /// </summary>
        public int TicketID { get; set; }

        /// <summary>
        /// Ticket Status
        /// </summary>
        public int ? TicketStatus { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// SubCategory
        /// </summary>
        public string SubCategory { get; set; }

        /// <summary>
        /// IssueType
        /// </summary>
        public string IssueType { get; set; }

        /// <summary>
        /// Category ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// SubCategoryID
        /// </summary>
        public int SubCategoryID { get; set; }

        /// <summary>
        /// IssueType ID
        /// </summary>
        public int IssueTypeID { get; set; }

        /// <summary>
        /// Brand
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Assign To
        /// </summary>
        public string AssignTo { get; set; }

        /// <summary>
        /// CustomerID 
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// Customer Name 
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        public string CustomerMobileNumber { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Ticket Title
        /// </summary>
        public string TicketTitle { get; set; }

        /// <summary>
        /// Ticket Description
        /// </summary>
        public string TicketDescription { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Updated Date
        /// </summary>
        public string UpdatedDate { get; set; }


        /// <summary>
        /// ChatID
        /// </summary>
        public int  ChatID { get; set; }


        /// <summary>
        /// ChatEndDateTime
        /// </summary>
        public string ChatEndDateTime { get; set; }
       
        /// <summary>
        /// IsIconDisplay
        /// </summary>
        public bool IsIconDisplay { get; set; }

        /// <summary>
        /// IsChatAllreadyActive
        /// </summary>
        public bool IsChatAllreadyActive { get; set; }

        /// <summary>
        /// ReInitiateChatDateTime
        /// </summary>
        public string ReInitiateChatDateTime { get; set; }


        /// <summary>
        ///List of Chat Ticket Note
        /// </summary>
        public List<ChatTicketNotes> ChatTicketNote { get; set; }
    }

    public class ChatTicketNotes
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
    }

    public class CreateChatTickets
    {
        /// <summary>
        /// TenantID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Ticket Status
        /// </summary>
        public int? TicketStatus { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public string SubCategory { get; set; }

        /// <summary>
        /// IssueType
        /// </summary>
        public string IssueType { get; set; }

        /// <summary>
        /// Brand
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Store Code
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// CustomerID
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// CustomerMobileNumber
        /// </summary>
        public string CustomerMobileNumber { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Ticket Title
        /// </summary>
        public string TicketTitle { get; set; }

        /// <summary>
        ///Ticket Description
        /// </summary>
        public string TicketDescription { get; set; }

        /// <summary>
        ///Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        ///Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        ///Updated By
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        ///Updated Date
        /// </summary>
        public string UpdatedDate { get; set; }
    }
}
