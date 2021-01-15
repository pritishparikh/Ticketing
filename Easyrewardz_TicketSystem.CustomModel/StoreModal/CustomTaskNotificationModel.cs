using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
  public class CustomTaskNotificationModel
    {
        /// <summary>
        /// Notification ID
        /// </summary>
        public int NotificationID { get; set; }

        /// <summary>
        /// Alert ID
        /// </summary>
        public int AlertID { get; set; }

        /// <summary>
        /// Notificaton Type ID
        /// </summary>
        public int NotificatonTypeID { get; set; }

        /// <summary>
        /// Notificaton Type
        /// </summary>
        public int NotificatonType { get; set; }

        /// <summary>
        /// Notificaton Type Name
        /// </summary>
        public string NotificatonTypeName { get; set; }
    }
    public class StoreNotificationModel
    {
        /// <summary>
        /// Notification Count
        /// </summary>
        public int NotificationCount { get; set; }

        /// <summary>
        /// Alert ID
        /// </summary>
        public int AlertID { get; set; }

        /// <summary>
        /// Notification Name
        /// </summary>
        public string NotificationName { get; set; }

        /// <summary>
        /// Custom Task Notification list
        /// </summary>
        public List<CustomTaskNotificationModel> CustomTaskNotificationModels { get; set; }
    }
    public class ListStoreNotificationModels
    {
        /// <summary>
        /// Store Notification list
        /// </summary>
        public List<StoreNotificationModel> StoreNotificationModel  { get; set; }

        /// <summary>
        /// Notification Count
        /// </summary>
        public int NotiCount { get; set; }
    }


    public class CampaignFollowUpNotiModel
    {
        /// <summary>
        ///Follow Up ID
        /// </summary>
        public int FollowUpID { get; set; }

        /// <summary>
        /// Notification Content
        /// </summary>
        public string NotificationContent { get; set; }

        /// <summary>
        ///Call Reschedule Date
        /// </summary>
        public string CallRescheduleDate { get; set; }

       
    }
}
