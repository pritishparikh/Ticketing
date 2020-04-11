using Easyrewardz_TicketSystem.CustomModel;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreNotification
    {
        ListStoreNotificationModels GetNotification(int TenantID, int UserID);

        int ReadNotification(int TenantID, int UserID, int NotificatonTypeID, int NotificatonType);
    } 
}
