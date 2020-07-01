using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface INotification
    {
        /// <summary>
        /// Get Notification
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        NotificationModel GetNotification(int TenantID, int UserID);

        /// <summary>
        /// Read Notification
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="TicketID"></param>
        /// <param name="IsFollowUp"></param>
        /// <returns></returns>
        int ReadNotification(int TenantID, int UserID, int TicketID, int IsFollowUp); 

    }
}
