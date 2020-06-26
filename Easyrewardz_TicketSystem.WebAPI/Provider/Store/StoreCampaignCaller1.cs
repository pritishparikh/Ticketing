using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class StoreCampaignCaller
    {
        #region Custom Methods

        /// <summary>
        /// Get Campaign Customer
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="campaingCustomerFilterRequest"></param>
        /// <returns></returns>
        public CampaignCustomerDetails GetCampaignCustomer(IStoreCampaign Campaign, int tenantID, int userID, CampaingCustomerFilterRequest campaingCustomerFilterRequest)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCampaignCustomer(tenantID, userID, campaingCustomerFilterRequest);
        }

        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UpdateCampaignStatusResponse(IStoreCampaign Campaign, CampaignResponseInput objRequest, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.UpdateCampaignStatusResponse(objRequest, TenantID, UserID);
        }

        /// <summary>
        /// Campaign Share Chatbot
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="objRequest"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public int CampaignShareChatbot(IStoreCampaign Campaign, ShareChatbotModel objRequest, string ClientAPIURL, int TenantID, int UserID, string ProgramCode)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.CampaignShareChatbot(objRequest, ClientAPIURL, TenantID, UserID, ProgramCode);
        }

        /// <summary>
        /// Campaign Share Messenger
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string CampaignShareMassanger(IStoreCampaign Campaign, ShareChatbotModel objRequest, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.CampaignShareMassanger(objRequest, TenantID, UserID);
        }

        /// <summary>
        /// Campaign Share SMS
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="objRequest"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="SMSsenderId"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int CampaignShareSMS(IStoreCampaign Campaign, ShareChatbotModel objRequest, string ClientAPIURL, string SMSsenderId, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.CampaignShareSMS(objRequest, ClientAPIURL, SMSsenderId, TenantID, UserID);
        }

        /// <summary>
        /// Get Broadcast Configuration Responses
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <param name="campaignCode"></param>
        /// <returns></returns>
        public BroadcastDetails GetBroadcastConfigurationResponses(IStoreCampaign Campaign, int tenantID, int userID, string programcode, string storeCode, string campaignCode)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetBroadcastConfigurationResponses(tenantID, userID, programcode, storeCode, campaignCode);
        }

        /// <summary>
        /// Insert BroadCast Details
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <param name="campaignCode"></param>
        /// <param name="channelType"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public int InsertBroadCastDetails(IStoreCampaign Campaign, int tenantID, int userID, string programcode, string storeCode, string campaignCode, string channelType, string ClientAPIURL)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.InsertBroadCastDetails(tenantID, userID, programcode, storeCode, campaignCode, channelType, ClientAPIURL);
        }

        #endregion
    }
}
