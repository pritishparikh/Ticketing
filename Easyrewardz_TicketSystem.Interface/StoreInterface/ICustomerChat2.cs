using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {

        List<CustomerChatMessages> GetChatMessageDetails(int tenantId, int ChatID);

        int SaveChatMessages(CustomerChatModel ChatMessageDetails);
    }
}
