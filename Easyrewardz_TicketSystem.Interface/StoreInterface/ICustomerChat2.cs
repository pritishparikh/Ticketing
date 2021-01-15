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
        /// GetChatMessageDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="ChatID"></param>
        /// <param name="ForRecentChat"></param>
        /// <returns></returns>
        Task<List<CustomerChatMessages>> GetChatMessageDetails(int tenantId, int ChatID, int ForRecentChat);

        /// <summary>
        /// GetChatMessageDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="ChatID"></param>
        /// <param name="ForRecentChat"></param>
        /// <param name="PageNo"></param>
        /// <returns></returns>
        Task<ChatMessageDetails> GetChatMessageDetailsNew(int tenantId, int ChatID, int ForRecentChat, int PageNo);

        /// <summary>
        /// SaveChatMessages
        /// </summary>
        /// <param name="ChatMessageDetails"></param>
        /// <returns></returns>
        int SaveChatMessages(CustomerChatModel ChatMessageDetails);

        /// <summary>
        /// ChatItemDetailsSearch
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        List<CustomItemSearchResponseModel>  ChatItemDetailsSearch(int TenantID, string Programcode, string ClientAPIURL,string SearchText);

        /// <summary>
        /// ChatItemDetailsSearch
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="SearchText"></param>
        ///  <param name="StoreCode"></param>
        /// <returns></returns>
        Task<List<CustomItemSearchResponseModel>> ChatItemDetailsSearchNew(int TenantID, string Programcode, string ClientAPIURL, string SearchText,string StoreCode);


        /// <summary>
        /// ChatItemDetailsSearch WebBot
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="SearchText"></param>
        ///  <param name="StoreCode"></param>
        /// <returns></returns>
        Task<List<CustomItemSearchWBResponseModel>> ChatItemDetailsSearchWB(int TenantID, string Programcode, string ClientAPIURL, string SearchText, string StoreCode);


        /// <summary>
        /// SaveCustomerChatMessageReply
        /// </summary>
        /// <param name="ChatReply"></param>
        /// <returns></returns>
        Task<int> SaveCustomerChatMessageReply(CustomerChatReplyModel ChatReply);

        /// <summary>
        /// GetChatSuggestions
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
       Task< List<object>>  GetChatSuggestions(string SearchText);

        /// <summary>
        /// SendRecommendationsToCustomer
        /// </summary>
        /// <param name="ChatID"></param>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int SendRecommendationsToCustomer(int ChatID,int TenantID, string Programcode, int CustomerID, string MobileNo, string ClientAPIURL, int CreatedBy);


        Task<int> SendRecommendationsToCustomerNew(int ChatID, int TenantID, string Programcode, int CustomerID, string MobileNo, string ClientAPIURL,
            string SendImageMessage, string Recommended, int CreatedBy, string Source);

        /// <summary>
        /// SendMessageToCustomer
        /// </summary>
        /// <param name="ChatID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="Message"></param>
        /// <param name="WhatsAppMessage"></param>
        /// <param name="ImageURL"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="InsertChat"></param>
        /// <returns></returns>
        int SendMessageToCustomer(int ChatID, string MobileNo,string ProgramCode,string Message, string WhatsAppMessage, string ImageURL, string ClientAPIURL,int CreatedBy, int InsertChat);

        int sendMessageToCustomerNew(int ChatID, string MobileNo, string ProgramCode, string Message, string WhatsAppMessage, string ImageURL, string ClientAPIURL, int CreatedBy, int InsertChat, string Source);

        #region Chat Sound Notification Setting


        /// <summary>
        /// Get Chat Sound Notification Setting
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <returns></returns>
        ///
        Task<ChatSoundNotificationModel> GetChatSoundNotificationSetting(int TenantID, string Programcode, string SoundFilePath);

        /// <summary>
        ///  Get Chat Sound List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <returns></returns>
        /// 
        Task<List<ChatSoundModel>> GetChatSoundList(int TenantID, string Programcode, string SoundFilePath);

        /// <summary>
        /// Update Chat Sound Notification Setting
        /// </summary>
        /// <param name="ChatSoundNotificationModel"></param>
        /// <returns></returns>
        ///
        Task<int> UpdateChatSoundNotificationSetting(ChatSoundNotificationModel Setting);

        #endregion


    }
}
