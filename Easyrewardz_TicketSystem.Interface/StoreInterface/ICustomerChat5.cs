using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        /// <summary>
        /// SaveReInitiateChatMessages
        /// </summary>
        /// <param name="customerChatMaster"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <param name="sendText"></param>
        /// <param name="MakeBellActive"></param>
        /// <returns></returns>
        Task<int> SaveReInitiateChatMessages(ReinitiateChatModel customerChatMaster, string ClientAPIUrl,string sendText,string MakeBellActive);
    }
}
