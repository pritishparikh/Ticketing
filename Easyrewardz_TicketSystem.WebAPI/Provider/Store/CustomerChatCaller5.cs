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
        #region Methods 

        /// <summary>
        /// SaveReInitiateChat 
        /// </summary>
        /// <param name="customerChatMaster"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <param name="sendText"></param>
        /// <param name="MakeBellActive"></param>
        /// <returns></returns>
        public async Task <int> SaveReInitiateChat(ICustomerChat customerChat, ReinitiateChatModel customerChatMaster,string ClientAPIUrl, string sendText, string MakeBellActive)
        {
            _customerChat = customerChat;
            return await _customerChat.SaveReInitiateChatMessages(customerChatMaster, ClientAPIUrl,  sendText,  MakeBellActive);

        }
        #endregion
    }
}
