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

        /// <summary>
        /// SaveReInitiateChat 
        /// </summary>
        /// <param name="customerChat"></param>
        /// <param name="customerChatMaster"></param>
        /// <returns></returns>
        public int SaveReInitiateChat(ICustomerChat customerChat,int TenantId, CustomerChatMaster customerChatMaster)
        {
            _customerChat = customerChat;
            return _customerChat.SaveReInitiateChatMessages(customerChatMaster, TenantId);

        }
        #endregion
    }
}
