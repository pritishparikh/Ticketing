using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class TicketingMailerQue
    {
        public int MailID { get; set; }
        public int TicketID { get; set; }
        public int TenantID { get; set; }
        public int AlertID { get; set; }
        public string TikcketMailSubject { get; set; }
        public string TicketMailBody { get; set; }
        public int TicketSource { get; set; }
        public string MailFrom { get; set; }
        public string ToEmail { get; set; }
        public string UserCC { get; set; }
        public string UserBCC { get; set; }
        public bool IsSent { get; set; }
        public int PriorityID { get; set; }
        public int CreatedBy { get; set; }
        public bool IsCustomerComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsInternalComment { get; set; }
        public bool IsResponseToCustomer { get; set; }
     
    }

}
