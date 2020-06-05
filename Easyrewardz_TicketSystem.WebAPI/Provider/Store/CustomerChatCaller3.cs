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
        public int UpdateChatSession(ICustomerChat customerChat ,int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue, string ChatDisplayDuration,int ModifiedBy)
        {
            _customerChat = customerChat;
            return _customerChat.UpdateChatSession( ChatSessionValue,  ChatSessionDuration,  ChatDisplayValue,  ChatDisplayDuration,  ModifiedBy);

        }

        public ChatSessionModel GetChatSession(ICustomerChat customerChat)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatSession();

        }

        public List<AgentRecentChatHistory>  GetAgentRecentChat(ICustomerChat customerChat)
        {
            _customerChat = customerChat;
            return _customerChat.GetAgentRecentChat();

        }

        public List<AgentCustomerChatHistory> GetAgentChatHistory(ICustomerChat customerChat, int TenantId, int StoreManagerID, string ProgramCode)
        {
            _customerChat = customerChat;
            return _customerChat.GetAgentChatHistory( TenantId,  StoreManagerID,  ProgramCode);

        }

        public List<AgentRecentChatHistory> GetAgentList(ICustomerChat customerChat, int TenantID)
        {
            _customerChat = customerChat;
            return _customerChat.GetAgentList(TenantID); 

        }

        public List<ChatCardImageUploadModel> GetCardImageUploadlog(ICustomerChat customerChat,int ListingFor, int TenantID, string ProgramCode)
        {
            _customerChat = customerChat;
            return _customerChat.GetCardImageUploadlog(ListingFor,TenantID,  ProgramCode);

        }

        public int InsertCardImageUpload(ICustomerChat customerChat, int TenantID, string ProgramCode, string ItemID, string ImageUrl, int CreatedBy)
        {
            _customerChat = customerChat;
            return _customerChat.InsertCardImageUpload( TenantID,  ProgramCode,  ItemID,  ImageUrl,  CreatedBy);

        }

        public int ApproveRejectCardImage(ICustomerChat customerChat, int ID, int TenantID, string ProgramCode, string ItemID, bool AddToLibrary, int ModifiedBy)
        {
            _customerChat = customerChat;
            return _customerChat.ApproveRejectCardImage(ID, TenantID,  ProgramCode,  ItemID,  AddToLibrary,  ModifiedBy);

        }


        public int InsertNewCardItemConfiguration(ICustomerChat customerChat, int TenantID, string ProgramCode, string CardItem, bool IsEnabled, int CreatedBy)
        {
            _customerChat = customerChat;
            return _customerChat.InsertNewCardItemConfiguration( TenantID,  ProgramCode,  CardItem,  IsEnabled,  CreatedBy);

        }

        public int UpdateCardItemConfiguration(ICustomerChat customerChat, int TenantID, string ProgramCode, string EnabledCardItems, string DisabledCardItems, int ModifiedBy)
        {
            _customerChat = customerChat;
            return _customerChat.UpdateCardItemConfiguration( TenantID,  ProgramCode,  EnabledCardItems,  DisabledCardItems,  ModifiedBy);

        }

        public List<ChatCardConfigurationModel> GetCardConfiguration(ICustomerChat customerChat,  int TenantID, string ProgramCode)
        {
            _customerChat = customerChat;
            return _customerChat.GetCardConfiguration( TenantID, ProgramCode);

        }

        public int UpdateStoreManagerChatStatus(ICustomerChat customerChat, int TenantID, string ProgramCode, int ChatID, int ChatStatusID, int StoreManagerID)
        {
            _customerChat = customerChat;
            return _customerChat.UpdateStoreManagerChatStatus( TenantID,  ProgramCode,  ChatID,  ChatStatusID, StoreManagerID);

        }

    }
}
