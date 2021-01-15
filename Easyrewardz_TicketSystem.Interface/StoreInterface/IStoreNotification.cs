using Easyrewardz_TicketSystem.CustomModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreNotification
    {
        /// <summary>
        /// Get Notification
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        ListStoreNotificationModels GetNotification(int TenantID, int UserID);

        /// <summary>
        /// Get Notification
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<ListStoreNotificationModels> GetNotificationNew(int TenantID, int UserID);

        /// <summary>
        /// Read Notification
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="NotificatonTypeID"></param>
        /// <param name="NotificatonType"></param>
        /// <returns></returns>
        int ReadNotification(int TenantID, int UserID, int NotificatonTypeID, int NotificatonType);


        /// <summary>
        ///Get Campaign Followup Notifications
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="FollowUpID"></param>
        /// <returns></returns>
        Task<List<CampaignFollowUpNotiModel>> GetCampaignFollowupNotifications(int TenantID, int UserID,int FollowUpID);


    } 
}
