using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class AlertModel
    {

        public int AlertID { get; set; }
        public string AlertTypeName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string isAlertActive { get; set; }

        public CommunicationModeBy ModeOfCommunication { get; set; }
        public string MailContent { get; set; }
        public string Subject { get; set; }
        public string SMSContent { get; set; }
        public string NotificationContent { get; set; }

        public bool IsEmailCustomer { get; set; }
        public bool IsEmailInternal { get; set; }
        public bool IsEmailStore { get; set; }
        public bool IsSMSCustomer { get; set; }
        public bool IsNotificationInternal { get; set; }
    }

    public class CommunicationModeBy
    {
        public bool isByEmail { get; set; }
        public bool isBySMS { get; set; }
        public bool isByNotification { get; set; }
    }

    public class AlertInsertModel
    {
        public int? TenantId { get; set; }
        public string AlertTypeName { get; set; }
        public int? CreatedBy { get; set; }
        public bool isAlertActive { get; set; }


        public List<CommunicationMode> CommunicationModeDetails { get; set; }
    }

    public class CommunicationMode
    {

        public int Communication_Mode { get; set; }
        public int CommunicationFor { get; set; }
        public string Content { get; set; }
        public string ToEmailID { get; set; }
        public string CCEmailID { get; set; }
        public string BCCEmailID { get; set; }
        public string Subject { get; set; }


    }

    public class AlertList
    {
        public int AlertID { get; set; }
        public string AlertTypeName { get; set; }
        public bool isAlertActive { get; set; }
    }



}
