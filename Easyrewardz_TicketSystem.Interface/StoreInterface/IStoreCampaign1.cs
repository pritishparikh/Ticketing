﻿using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        /// <summary>
        /// Get Campaign Customer
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="campaingCustomerFilterRequest"></param>
        /// <returns></returns>
        CampaignCustomerDetails GetCampaignCustomer(int tenantID, int userID, CampaingCustomerFilterRequest campaingCustomerFilterRequest);

        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int UpdateCampaignStatusResponse(CampaignResponseInput objRequest, int TenantID, int UserID);

        /// <summary>
        /// Campaign Share Chatbot
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        int CampaignShareChatbot(ShareChatbotModel objRequest, string ClientAPIURL, int TenantID, int UserID, string ProgramCode);

        /// <summary>
        /// Campaign Share Messenger
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        string CampaignShareMassanger(ShareChatbotModel objRequest, int TenantID, int UserID);

        /// <summary>
        /// Campaign Share SMS
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="SMSsenderId"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int CampaignShareSMS(ShareChatbotModel objRequest, string ClientAPIURL, string SMSsenderId, int TenantID, int UserID);

        /// <summary>
        /// Get Broadcast Configuration Responses
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <param name="campaignCode"></param>
        /// <returns></returns>
        BroadcastDetails GetBroadcastConfigurationResponses(int tenantID, int userID, string programcode, string storeCode, string campaignCode);

        /// <summary>
        /// Insert BroadCast Details
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <param name="campaignCode"></param>
        /// <param name="channelType"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        int InsertBroadCastDetails(int tenantID, int userID, string programcode, string storeCode, string campaignCode, string channelType, string ClientAPIURL);
    }
}
