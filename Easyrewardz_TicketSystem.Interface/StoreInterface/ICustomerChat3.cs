using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        /// <summary>
        /// UpdateChatSession
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ChatSessionValue"></param>
        /// <param name="ChatSessionDuration"></param>
        /// <param name="ChatDisplayValue"></param>
        /// <param name="ChatDisplayDuration"></param>
        /// <param name="ChatCharLimit"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        int UpdateChatSession(int TenantId, string ProgramCode, int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue,
                            string ChatDisplayDuration,int ChatCharLimit, int ModifiedBy);

        /// <summary>
        /// GetChatSession
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        ChatSessionModel GetChatSession(int TenantId, string ProgramCode);

        /// <summary>
        /// GetAgentRecentChat
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        List<AgentRecentChatHistory> GetAgentRecentChat(int TenantId, string ProgramCode, int CustomerID);

        /// <summary>
        /// GetAgentChatHistory
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="StoreManagerID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        List<AgentCustomerChatHistory> GetAgentChatHistory(int TenantId, int StoreManagerID, string ProgramCode);

        /// <summary>
        /// GetAgentList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<AgentRecentChatHistory> GetAgentList(int TenantID, int UserID);

        /// <summary>
        /// GetCardImageUploadlog
        /// </summary>
        /// <param name="ListingFor"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        List<ChatCardImageUploadModel> GetCardImageUploadlog(int ListingFor,int TenantID, string ProgramCode);

        /// <summary>
        /// InsertCardImageUpload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <param name="SearchText"></param>
        /// <param name="ItemID"></param>
        /// <param name="ImageUrl"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int InsertCardImageUpload(int TenantID, string ProgramCode, string ClientAPIUrl, string SearchText, string ItemID, string ImageUrl, int CreatedBy);

        /// <summary>
        /// ApproveRejectCardImage
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ItemID"></param>
        /// <param name="AddToLibrary"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        int ApproveRejectCardImage(int ID,int TenantID, string ProgramCode, string ItemID, bool AddToLibrary, int ModifiedBy);

        /// <summary>
        /// InsertNewCardItemConfiguration
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CardItem"></param>
        /// <param name="IsEnabled"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int InsertNewCardItemConfiguration(int TenantID, string ProgramCode, string CardItem, bool IsEnabled, int CreatedBy);

        /// <summary>
        /// UpdateCardItemConfiguration
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="EnabledCardItems"></param>
        /// <param name="DisabledCardItems"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        int UpdateCardItemConfiguration(int TenantID, string ProgramCode,string EnabledCardItems, string DisabledCardItems, int ModifiedBy);

        /// <summary>
        /// GetCardConfiguration
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        List<ChatCardConfigurationModel> GetCardConfiguration(int TenantID, string ProgramCode);

        /// <summary>
        /// UpdateStoreManagerChatStatus
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ChatID"></param>
        /// <param name="ChatStatusID"></param>
        /// <param name="StoreManagerID"></param>
        /// <returns></returns>
        int UpdateStoreManagerChatStatus(int TenantID, string ProgramCode, int ChatID, int ChatStatusID, int StoreManagerID);

        /// <summary>
        /// UpdateCardImageApproval
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        int UpdateCardImageApproval(int TenantID, string ProgramCode, int ID, int ModifiedBy);

        /// <summary>
        /// GetCardImageApprovalList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        List<CardImageApprovalModel> GetCardImageApprovalList(int TenantID, string ProgramCode);

        int EndCustomerChat(int TenantID, string ProgramCode, int ChatID,string EndChatMessage, int UserID);
    }
}
 