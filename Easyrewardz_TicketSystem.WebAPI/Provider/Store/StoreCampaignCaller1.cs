using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class StoreCampaignCaller
    {
        public List<CampaignCustomerModel> GetCampaignCustomer(IStoreCampaign Campaign, int tenantID, int userID, int campaignScriptID, int pageNo, int pageSize)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCampaignCustomer(tenantID, userID, campaignScriptID, pageNo, pageSize);
        }

        public int UpdateCampaignStatusResponse(IStoreCampaign Campaign, CampaignResponseInput objRequest, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.UpdateCampaignStatusResponse(objRequest, TenantID, UserID);
        }

        public int CampaignShareChatbot(IStoreCampaign Campaign, ShareChatbotModel objRequest, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.CampaignShareChatbot(objRequest, TenantID, UserID);
        }

    }
}
