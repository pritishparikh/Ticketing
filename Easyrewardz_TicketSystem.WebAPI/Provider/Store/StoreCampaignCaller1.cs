using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class StoreCampaignCaller
    {
        public List<CampaignCustomerModel> GetCampaignCustomer(IStoreCampaign Campaign, int tenantID, int userID, int campaignScriptID, int pageNo, int pageSize, string FilterStatus)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCampaignCustomer(tenantID, userID, campaignScriptID, pageNo, pageSize, FilterStatus);
        }

        public int UpdateCampaignStatusResponse(IStoreCampaign Campaign, CampaignResponseInput objRequest, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.UpdateCampaignStatusResponse(objRequest, TenantID, UserID);
        }

        public int CampaignShareChatbot(IStoreCampaign Campaign, ShareChatbotModel objRequest, string ClientAPIURL, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.CampaignShareChatbot(objRequest, ClientAPIURL, TenantID, UserID);
        }

        public string CampaignShareMassanger(IStoreCampaign Campaign, ShareChatbotModel objRequest, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.CampaignShareMassanger(objRequest, TenantID, UserID);
        }

        public int CampaignShareSMS(IStoreCampaign Campaign, ShareChatbotModel objRequest, string ClientAPIURL, string SMSsenderId, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.CampaignShareSMS(objRequest, ClientAPIURL, SMSsenderId, TenantID, UserID);
        }

    }
}
