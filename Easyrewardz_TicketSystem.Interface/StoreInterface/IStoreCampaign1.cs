using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        CampaignCustomerDetails GetCampaignCustomer(int tenantID, int userID, CampaingCustomerFilterRequest campaingCustomerFilterRequest);
        int UpdateCampaignStatusResponse(CampaignResponseInput objRequest, int TenantID, int UserID);
        int CampaignShareChatbot(ShareChatbotModel objRequest, string ClientAPIURL, int TenantID, int UserID, string ProgramCode);
        string CampaignShareMassanger(ShareChatbotModel objRequest, int TenantID, int UserID);
        int CampaignShareSMS(ShareChatbotModel objRequest, string ClientAPIURL, string SMSsenderId, int TenantID, int UserID);
        BroadcastDetails GetBroadcastConfigurationResponses(int tenantID, int userID, string programcode, string storeCode, string campaignCode);
        int InsertBroadCastDetails(int tenantID, int userID, string programcode, string storeCode, string campaignCode, string channelType, string ClientAPIURL);
    }
}
