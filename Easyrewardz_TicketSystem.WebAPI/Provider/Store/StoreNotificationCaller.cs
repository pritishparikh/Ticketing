using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreNotificationCaller
    {

        #region Variable declaration

        private IStoreNotification _Notification;

        #endregion

        /// <summary>
        /// Get AlertList
        // / </summary>
        public ListStoreNotificationModels GetNotification(IStoreNotification Notification, int TenantID, int UserID)
        {
            _Notification = Notification;
            return _Notification.GetNotification(TenantID, UserID);
        }

        public async Task<ListStoreNotificationModels> GetNotificationNew(IStoreNotification Notification, int TenantID, int UserID)
        {
            _Notification = Notification;
            return await  _Notification.GetNotificationNew(TenantID, UserID);
        }

        /// <summary>
        /// Update is Read on Notification Read
        // / </summary>
        public int ReadNotification(IStoreNotification Notification, int TenantID, int UserID, int NotificatonTypeID, int NotificatonType)
        {
            _Notification = Notification;
            return _Notification.ReadNotification(TenantID, UserID, NotificatonTypeID, NotificatonType);

        }


        public async Task<List<CampaignFollowUpNotiModel>> GetCampaignFollowupNotifications(IStoreNotification Notification, int TenantID,int UserID, int FollowUpID)
        {
            _Notification = Notification;
            return await _Notification.GetCampaignFollowupNotifications( TenantID,  UserID, FollowUpID);

        }
    }
}
