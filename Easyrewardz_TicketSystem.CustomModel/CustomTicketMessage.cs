using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class TicketMessage
    {
        public int MessageCount { get; set; }
        public string MessageDate { get; set; }
        public string UpdatedDate { get; set; }
        public List<CustomTicketMessage> CustomTicketMessages { get; set; }
    }


   public class CustomTicketMessage
    {
        public int MailID { get; set; }
        public int TicketID { get; set; }
        public string TicketMailSubject { get; set; }
        public string TicketMailBody { get; set; }
        public int IsCustomerComment   { get; set; }
        public int HasAttachment { get; set; }
        public int TicketSource { get; set; }
        public string CommentBy { get; set; }
        public string UpdatedAt { get; set; }
        public string CreatedDate { get; set; }
    }
}
