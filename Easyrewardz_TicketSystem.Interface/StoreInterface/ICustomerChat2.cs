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
        /// GetChatMessageDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="ChatID"></param>
        /// <param name="ForRecentChat"></param>
        /// <returns></returns>
        List<CustomerChatMessages> GetChatMessageDetails(int tenantId, int ChatID, int ForRecentChat);

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
        /// SaveCustomerChatMessageReply
        /// </summary>
        /// <param name="ChatReply"></param>
        /// <returns></returns>
        int SaveCustomerChatMessageReply(CustomerChatReplyModel ChatReply);

        /// <summary>
        /// GetChatSuggestions
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        List<CustomerChatSuggestionModel>  GetChatSuggestions(string SearchText);

        /// <summary>
        /// SendRecommendationsToCustomer
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int SendRecommendationsToCustomer(int TenantID, string Programcode, int CustomerID, string MobileNo, string ClientAPIURL, int CreatedBy);

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


    }
}
