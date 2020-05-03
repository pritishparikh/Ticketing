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


        public int SaveCustomerChatMessageReply(ICustomerChat customerChat, CustomerChatReplyModel ChatReply)
        {
            _customerChat = customerChat;
            return _customerChat.SaveCustomerChatMessageReply(ChatReply);

        }

        public List<CustomerChatSuggestionModel> GetChatSuggestions(ICustomerChat customerChat, string SearchText)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatSuggestions(SearchText);
        }


        public int SendRecommendationsToCustomer(ICustomerChat customerChat, int CustomerID, string MobileNo ,int CreatedBy)
        {
            _customerChat = customerChat;
            return _customerChat.SendRecommendationsToCustomer( CustomerID,  MobileNo, CreatedBy);

        }


        #endregion
    }
}
