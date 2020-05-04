using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        List<CampaignCustomerModel> GetCampaignCustomer(int tenantID, int userID, int campaignScriptID, int pageNo, int pageSize, string FilterStatus);
        int UpdateCampaignStatusResponse(CampaignResponseInput objRequest, int TenantID, int UserID);
        int CampaignShareChatbot(ShareChatbotModel objRequest, string ClientAPIURL, int TenantID, int UserID);
        string CampaignShareMassanger(ShareChatbotModel objRequest, int TenantID, int UserID);
        int CampaignShareSMS(ShareChatbotModel objRequest, string ClientAPIURL, string SMSsenderId, int TenantID, int UserID);
    }
}
