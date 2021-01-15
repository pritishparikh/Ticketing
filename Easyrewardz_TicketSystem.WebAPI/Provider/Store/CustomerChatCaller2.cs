using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class CustomerChatCaller
    {
        #region Methods 

        public async Task<List<CustomerChatMessages>> GetChatmessageDetails(ICustomerChat customerChat, int TenantId, int ChatID, int ForRecentChat)
        {
            _customerChat = customerChat;
            return await _customerChat.GetChatMessageDetails(TenantId, ChatID,  ForRecentChat);
        }

        public async Task<ChatMessageDetails> GetChatmessageDetailsNew(ICustomerChat customerChat, int TenantId, int ChatID, int ForRecentChat, int PageNo)
        {
            _customerChat = customerChat;
            return await _customerChat.GetChatMessageDetailsNew(TenantId, ChatID, ForRecentChat, PageNo);
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

        public async Task<List<CustomItemSearchResponseModel>> ChatItemSearchNew(ICustomerChat customerChat, int TenantID, string Programcode, string ClientAPIURL, string SearchText,string StoreCode)
        {
            _customerChat = customerChat;
            return await _customerChat.ChatItemDetailsSearchNew(TenantID, Programcode, ClientAPIURL, SearchText, StoreCode);
        }

        public async Task<CustomItemSearchWBResponseModel> ChatItemDetailsSearchWB(ICustomerChat customerChat, int TenantID, string Programcode, string ClientAPIURL, string SearchText, string StoreCode)
        {
            _customerChat = customerChat;
            return await _customerChat.ChatItemDetailsSearchWB(TenantID, Programcode, ClientAPIURL, SearchText, StoreCode);
        }



        public async Task<int> SaveCustomerChatMessageReply(ICustomerChat customerChat, CustomerChatReplyModel ChatReply)
        {
            _customerChat = customerChat;
            return await _customerChat.SaveCustomerChatMessageReply(ChatReply);

        }

        public async Task<List<object>> GetChatSuggestions(ICustomerChat customerChat, string SearchText)
        {
            _customerChat = customerChat;
            return await _customerChat.GetChatSuggestions(SearchText);
        }


        public int SendRecommendationsToCustomer(ICustomerChat customerChat,  int ChatID,int TenantID, string Programcode, int CustomerID, string MobileNo , string ClientAPIURL,int CreatedBy)
        {
            _customerChat = customerChat;
            return _customerChat.SendRecommendationsToCustomer(ChatID,TenantID, Programcode, CustomerID,  MobileNo, ClientAPIURL, CreatedBy);

        }

        public async Task<int> SendRecommendationsToCustomerNew(ICustomerChat customerChat, int ChatID, int TenantID, string Programcode, int CustomerID, string MobileNo, string ClientAPIURL, string SendImageMessage, string Recommended, int CreatedBy, string Source)
        {
            _customerChat = customerChat;
            return await _customerChat.SendRecommendationsToCustomerNew(ChatID, TenantID, Programcode, CustomerID, MobileNo, ClientAPIURL,  SendImageMessage,  Recommended, CreatedBy, Source);

        }

        public int SendMessageToCustomer(ICustomerChat customerChat, int ChatID, string MobileNo, string ProgramCode, string Message, string WhatsAppMessage, string ImageURL, string ClientAPIURL, int CreatedBy, int InsertChat)
        {
            _customerChat = customerChat;
            return _customerChat.SendMessageToCustomer( ChatID,  MobileNo,  ProgramCode,  Message, WhatsAppMessage, ImageURL,  ClientAPIURL,  CreatedBy, InsertChat);

        }

        public int sendMessageToCustomerNew(ICustomerChat customerChat, int ChatID, string MobileNo, string ProgramCode, string Message, string WhatsAppMessage, string ImageURL, string ClientAPIURL, int CreatedBy, int InsertChat, string Source)
        {
            _customerChat = customerChat;
            return _customerChat.sendMessageToCustomerNew(ChatID, MobileNo, ProgramCode, Message, WhatsAppMessage, ImageURL, ClientAPIURL, CreatedBy, InsertChat, Source);

        }


        #region Chat Sound Notification Setting

        public async Task<ChatSoundNotificationModel> GetChatSoundNotificationSetting(ICustomerChat customerChat, int TenantID, string Programcode,string SoundFilePath)
        {
            _customerChat = customerChat;
            return await _customerChat.GetChatSoundNotificationSetting( TenantID,  Programcode, SoundFilePath);
        }

        public async Task<List<ChatSoundModel>> GetChatSoundList(ICustomerChat customerChat, int TenantID, string Programcode, string SoundFilePath)
        {
            _customerChat = customerChat;
            return await _customerChat.GetChatSoundList( TenantID,  Programcode, SoundFilePath);
        }


        public async Task<int> UpdateChatSoundNotificationSetting(ICustomerChat customerChat, ChatSoundNotificationModel Setting)
        {
            _customerChat = customerChat;
            return await _customerChat.UpdateChatSoundNotificationSetting( Setting);
        }

        #endregion


        #endregion
    }
}
