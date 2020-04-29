using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class CustomerChatCaller
    {
        #region Variable declaration

        private ICustomerChat _customerChat;
        #endregion

        #region Methods 
        public List<CustomerChatMaster> OngoingChat(ICustomerChat customerChat,int userMasterID,int tenantID)
        {
            _customerChat = customerChat;

            return _customerChat.OngoingChat(userMasterID, tenantID);
        }

        #endregion
    }
}
