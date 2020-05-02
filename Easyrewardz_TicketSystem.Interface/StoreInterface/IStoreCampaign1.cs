using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        List<CampaignCustomerModel> GetCampaignCustomer(int tenantID, int userID, int campaignScriptID, int pageNo, int pageSize);
        int UpdateCampaignStatusResponse(CampaignResponseInput objRequest, int TenantID, int UserID);
        int CampaignShareChatbot(ShareChatbotModel objRequest, int TenantID, int UserID);
    }
}
