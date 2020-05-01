using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class CustomerChatCaller
    {
        #region Methods 

        public List<CustomerChatMessages> GetChatmessageDetails(ICustomerChat customerChat, int TenantId, int ChatID)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatMessageDetails(TenantId, ChatID);
        }


        #endregion
    }
}
