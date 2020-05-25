using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class CustomerChatCaller
    {
        public int UpdateChatSession(ICustomerChat customerChat ,int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue, string ChatDisplayDuration,int ModifiedBy)
        {
            _customerChat = customerChat;
            return _customerChat.UpdateChatSession( ChatSessionValue,  ChatSessionDuration,  ChatDisplayValue,  ChatDisplayDuration,  ModifiedBy);

        }

        public ChatSessionModel GetChatSession(ICustomerChat customerChat)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatSession();

        }

        public List<AgentRecentChatHistory>  GetAgentRecentChat(ICustomerChat customerChat)
        {
            _customerChat = customerChat;
            return _customerChat.GetAgentRecentChat();

        }
    }
}
