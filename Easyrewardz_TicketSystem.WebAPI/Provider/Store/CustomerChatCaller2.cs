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

        public List<CustomerChatMessages> GetChatmessageDetails(ICustomerChat customerChat, int TenantId, int ChatID, int ForRecentChat)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatMessageDetails(TenantId, ChatID,  ForRecentChat);
        }

        public int SaveChatMessages(ICustomerChat customerChat, CustomerChatModel ChatMessageDetails)
        {
            _customerChat = customerChat;
            return _customerChat.SaveChatMessages(ChatMessageDetails);

        }

        public List<CustomItemSearchResponseModel> ChatItemSearch(ICustomerChat customerChat, int TenantID, string Programcode, string ClientAPIURL, string SearchText)
        {
            _customerChat = customerChat;
            return _customerChat.ChatItemDetailsSearch( TenantID,  Programcode, ClientAPIURL,SearchText); 
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


        public int SendRecommendationsToCustomer(ICustomerChat customerChat, int TenantID, string Programcode, int CustomerID, string MobileNo , string ClientAPIURL,int CreatedBy)
        {
            _customerChat = customerChat;
            return _customerChat.SendRecommendationsToCustomer( TenantID, Programcode, CustomerID,  MobileNo, ClientAPIURL, CreatedBy);

        }

        public int SendMessageToCustomer(ICustomerChat customerChat, int ChatID, string MobileNo, string ProgramCode, string Message, string WhatsAppMessage, string ImageURL, string ClientAPIURL, int CreatedBy, int InsertChat)
        {
            _customerChat = customerChat;
            return _customerChat.SendMessageToCustomer( ChatID,  MobileNo,  ProgramCode,  Message, WhatsAppMessage, ImageURL,  ClientAPIURL,  CreatedBy, InsertChat);

        }




        #endregion
    }
}
