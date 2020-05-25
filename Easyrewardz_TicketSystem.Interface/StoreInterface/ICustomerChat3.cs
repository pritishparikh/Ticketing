using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {

        int UpdateChatSession(int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue, string ChatDisplayDuration, int ModifiedBy);

        ChatSessionModel GetChatSession();

        List<AgentRecentChatHistory> GetAgentRecentChat();
    }
}
