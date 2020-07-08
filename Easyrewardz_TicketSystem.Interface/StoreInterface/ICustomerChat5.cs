using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        /// <summary>
        /// SaveReInitiateChatMessages
        /// </summary>
        /// <param name="customerChatMaster"></param>
        /// <returns></returns>
        int SaveReInitiateChatMessages(CustomerChatMaster customerChatMaster, int TenantId,string ProgramCode);
    }
}
