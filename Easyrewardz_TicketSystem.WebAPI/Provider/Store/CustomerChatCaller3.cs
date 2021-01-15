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
        public async Task<int> UpdateChatSession(ICustomerChat customerChat , ChatSessionModel Chat)
        {
            _customerChat = customerChat;
            return await _customerChat.UpdateChatSession(Chat);

        }

        public async Task<ChatSessionModel> GetChatSession(ICustomerChat customerChat, int TenantId, string ProgramCode)
        {
            _customerChat = customerChat;
            return await _customerChat.GetChatSession( TenantId,  ProgramCode);

        }

        public async Task<int> UpdateChatSessionNew(ICustomerChat customerChat, ChatSessionModel Chat)
        {
            _customerChat = customerChat;
            return await _customerChat.UpdateChatSessionNew(Chat);

        }

        public async Task<ChatSessionModel> GetChatSessionNew(ICustomerChat customerChat, int TenantId, string ProgramCode)
        {
            _customerChat = customerChat;
            return await _customerChat.GetChatSessionNew(TenantId, ProgramCode);

        }

        public async Task<List<AgentRecentChatHistory>>  GetAgentRecentChat(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID)
        {
            _customerChat = customerChat;
            return await _customerChat.GetAgentRecentChat( TenantId,  ProgramCode, CustomerID);

        }

        public async Task<List<AgentRecentChatHistory>> GetAgentRecentChatNew(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, int PageNo)
        {
            _customerChat = customerChat;
            return await _customerChat.GetAgentRecentChatNew(TenantId, ProgramCode, CustomerID, PageNo);

        }

        public async Task<List<AgentCustomerChatHistory>> GetAgentChatHistory(ICustomerChat customerChat, int TenantId, int StoreManagerID, string ProgramCode)
        {
            _customerChat = customerChat;
            return await _customerChat.GetAgentChatHistory( TenantId,  StoreManagerID,  ProgramCode);

        }

        public async Task<List<AgentCustomerChatHistory>> GetAgentChatHistoryNew(ICustomerChat customerChat, int TenantId, int StoreManagerID, string ProgramCode, int PageNo)
        {
            _customerChat = customerChat;
            return await _customerChat.GetAgentChatHistoryNew(TenantId, StoreManagerID, ProgramCode, PageNo);

        }

        public async Task<List<AgentRecentChatHistory>> GetAgentList(ICustomerChat customerChat, int TenantID, int UserID)
        {
            _customerChat = customerChat;
            return await _customerChat.GetAgentList(TenantID,  UserID); 

        }

        public async Task<List<ChatCardImageUploadModel>> GetCardImageUploadlog(ICustomerChat customerChat,int ListingFor, int TenantID, string ProgramCode)
        {
            _customerChat = customerChat;
            return await _customerChat.GetCardImageUploadlog(ListingFor,TenantID,  ProgramCode);

        }

        public async Task<int> InsertCardImageUpload(ICustomerChat customerChat, int TenantID, string ProgramCode, string StoreCode, string ClientAPIUrl, string SearchText, string ItemID,string ImageUrl, int CreatedBy)
        {
            _customerChat = customerChat;
            return await _customerChat.InsertCardImageUpload( TenantID,  ProgramCode,  StoreCode, ClientAPIUrl, SearchText, ItemID, ImageUrl,  CreatedBy);

        }

        public async Task<int> ApproveRejectCardImage(ICustomerChat customerChat, int ID, int TenantID, string ProgramCode, string ItemID, bool AddToLibrary,string RejectionReason, int ModifiedBy)
        {
            _customerChat = customerChat;
            return await _customerChat.ApproveRejectCardImage(ID, TenantID,  ProgramCode,  ItemID,  AddToLibrary, RejectionReason,  ModifiedBy);

        }


        public async Task<int> InsertNewCardItemConfiguration(ICustomerChat customerChat, int TenantID, string ProgramCode, string CardItem, bool IsEnabled, int CreatedBy)
        {
            _customerChat = customerChat;
            return await _customerChat.InsertNewCardItemConfiguration( TenantID,  ProgramCode,  CardItem,  IsEnabled,  CreatedBy);

        }

        public async Task<int> UpdateCardItemConfiguration(ICustomerChat customerChat, int TenantID, string ProgramCode, string EnabledCardItems, string DisabledCardItems, int ModifiedBy)
        {
            _customerChat = customerChat;
            return await _customerChat.UpdateCardItemConfiguration( TenantID,  ProgramCode,  EnabledCardItems,  DisabledCardItems,  ModifiedBy);

        }

        public async Task<List<ChatCardConfigurationModel>> GetCardConfiguration(ICustomerChat customerChat,  int TenantID, string ProgramCode)
        {
            _customerChat = customerChat;
            return await _customerChat.GetCardConfiguration( TenantID, ProgramCode);

        }

        public async Task<int> UpdateStoreManagerChatStatus(ICustomerChat customerChat, int TenantID, string ProgramCode, int ChatID, int ChatStatusID, int StoreManagerID)
        {
            _customerChat = customerChat;
            return await _customerChat.UpdateStoreManagerChatStatus( TenantID,  ProgramCode,  ChatID,  ChatStatusID, StoreManagerID);

        }


        public async  Task<List<CardImageApprovalModel>> GetCardImageApprovalList(ICustomerChat customerChat, int TenantID, string ProgramCode)
        {
            _customerChat = customerChat;
            return await _customerChat.GetCardImageApprovalList(TenantID, ProgramCode);

        }

        public async Task<int> UpdateCardImageApproval(ICustomerChat customerChat, int TenantID, string ProgramCode, int ID, int ModifiedBy)
        {
            _customerChat = customerChat;
            return await _customerChat.UpdateCardImageApproval(TenantID, ProgramCode, ID,  ModifiedBy);

        }

        public async Task<int> EndCustomerChat(ICustomerChat customerChat, int TenantID, string ProgramCode, int ChatID, string EndChatMessage, int UserID)
        {
            _customerChat = customerChat;
            return await _customerChat.EndCustomerChat( TenantID,  ProgramCode,  ChatID,  EndChatMessage,  UserID);

        }
        

    }
}
