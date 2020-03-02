using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class TicketMessage
    {
        public int MessageCount { get; set; }
        public string MessageDate { get; set; }
        public string DayOfCreation { get; set; }
        public List<MessageDetails> MsgDetails { get; set; }
       
    }

    public class MessageDetails
    {
        public CustomTicketMessage LatestMessageDetails { get; set; }
        public List<CustomTicketMessage> TrailMessageDetails { get; set; }
    }


   public class CustomTicketMessage
    {
        public int MailID { get; set; }
        public int LatestMessageID { get; set; }
        public int TicketID { get; set; }
        public string TicketMailSubject { get; set; }
        public string TicketMailBody { get; set; }
        public int IsCustomerComment   { get; set; }
        public int HasAttachment { get; set; }
        public int TicketSourceID { get; set; }
        public string TicketSourceName { get; set; }
        public string CommentBy { get; set; }
        public string DayOfCreation { get; set; }
        public string CreatedDate { get; set; }
        public bool IsInternalComment { get; set; }

    }
}
