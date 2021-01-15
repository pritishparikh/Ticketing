using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class MobileNotificationModel
    {
        public int UnReadNotiCount { get; set; }

        public int TotalNotiCount { get; set; }

        public List<MobileNotificationDetails> ChatNotification { get; set; }

    }

    public class MobileNotificationDetails
    {

        public int IndexID { get; set; }

        public string NotificationFor { get; set; }

        public int ChatID { get; set; }

        public int AppointmentID { get; set; }

        public string OrderID { get; set; }

        public string CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CustomerMobileNumber { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }

        public string CreatedDate { get; set; }

        public bool IsSlotEnabled { get; set; }
    }
}
