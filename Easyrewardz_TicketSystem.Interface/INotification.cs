using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface INotification
    {
        NotificationModel GetNotification(int TenantID, int UserID);

        int ReadNotification(int TenantID, int UserID, int TicketID, int IsFollowUp); 

    }
}
