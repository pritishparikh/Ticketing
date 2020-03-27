using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class TicketNotificationModel
    {
        public string TicketIDs { get; set; }
        public string NotificationMessage { get; set; }
        public int TicketCount { get; set; }
        public int IsFollowUp { get; set; }
    }

    public class NotificationModel
    {
       public List<TicketNotificationModel> TicketNotification { get; set; }
       public int NotiCount { get; set; }

    }

}
