using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomTicketMessage
    {
        public int MailID { get; set; }
        public int TicketID { get; set; }
        public string TicketMailSubject { get; set; }
        public string TicketMailBody { get; set; }
        public bool IsCustomerComment   { get; set; }
        public bool HasAttachment { get; set; }
        public string CommentBy { get; set; }
    }
}
