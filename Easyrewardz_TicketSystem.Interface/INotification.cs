using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface INotification
    {
        NotificationModel GetNotification(int TenantID, int UserID);

        int ReadNotification(int TenantID, int UserID, int TicketID); 

    }
}
