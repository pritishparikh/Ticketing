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
}
