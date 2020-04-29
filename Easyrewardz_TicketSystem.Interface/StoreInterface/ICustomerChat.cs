using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ICustomerChat
    {
        List<CustomerChatMaster> OngoingChat(int userMasterID,int tenantID);
    }
}
