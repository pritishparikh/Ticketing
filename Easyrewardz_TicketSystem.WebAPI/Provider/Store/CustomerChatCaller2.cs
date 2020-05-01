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

        public List<CustomerChatMessages> GetChatmessageDetails(ICustomerChat customerChat, int TenantId, int ChatID)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatMessageDetails(TenantId, ChatID);
        }

        public int SaveChatMessages(ICustomerChat customerChat, CustomerChatModel ChatMessageDetails)
        {
            _customerChat = customerChat;
            return _customerChat.SaveChatMessages(ChatMessageDetails);

        }

        public List<CustomItemSearchResponseModel> ChatItemSearch(ICustomerChat customerChat, string SearchText)
        {
            _customerChat = customerChat;
            return _customerChat.ChatItemDetailsSearch(SearchText);
        }


        #endregion
    }
}
