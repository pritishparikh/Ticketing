using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        
        List<CustomerChatMaster> OngoingChat(int userMasterID,int tenantID);

        List<CustomerChatMaster> NewChat(int userMasterID, int tenantID);

        int MarkAsReadOnGoingChat(int chatID); 

        int UpdateCustomerChatIdStatus(int chatID, int tenantID);

        int ScheduleVisit(AppointmentMaster appointmentMaster);
    }
}
