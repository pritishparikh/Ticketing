using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {

        int UpdateChatSession(int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue, string ChatDisplayDuration, int ModifiedBy);

        ChatSessionModel GetChatSession();

        List<AgentRecentChatHistory> GetAgentRecentChat(int TenantId, string ProgramCode, int CustomerID);

        List<AgentCustomerChatHistory> GetAgentChatHistory(int TenantId, int StoreManagerID, string ProgramCode);

        List<AgentRecentChatHistory> GetAgentList(int TenantID);


        List<ChatCardImageUploadModel> GetCardImageUploadlog(int ListingFor,int TenantID, string ProgramCode);

        int InsertCardImageUpload(int TenantID, string ProgramCode, string ItemID, string ImageUrl, int CreatedBy);

        int ApproveRejectCardImage(int ID,int TenantID, string ProgramCode, string ItemID, bool AddToLibrary, int ModifiedBy);

        int InsertNewCardItemConfiguration(int TenantID, string ProgramCode, string CardItem, bool IsEnabled, int CreatedBy);

        int UpdateCardItemConfiguration(int TenantID, string ProgramCode,string EnabledCardItems, string DisabledCardItems, int ModifiedBy);


        List<ChatCardConfigurationModel> GetCardConfiguration(int TenantID, string ProgramCode);

        int UpdateStoreManagerChatStatus(int TenantID, string ProgramCode, int ChatID, int ChatStatusID, int StoreManagerID);

        int UpdateCardImageApproval(int TenantID, string ProgramCode, int ID, int ModifiedBy);

        List<CardImageApprovalModel> GetCardImageApprovalList(int TenantID, string ProgramCode);
    }
}
 