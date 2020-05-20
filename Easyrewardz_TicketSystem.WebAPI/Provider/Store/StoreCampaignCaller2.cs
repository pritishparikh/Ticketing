using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class StoreCampaignCaller
    {
        #region Variable
        public IStoreCampaign _CampaignRepository;
        #endregion

        public List<StoreCampaignModel2> GetStoreCampaign(IStoreCampaign Campaign, int TenantID, int UserID, string campaignName, string statusId)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetStoreCampaign(TenantID, UserID, campaignName, statusId);

        }

        public StoresCampaignStatusResponse GetCustomerpopupDetailsList(IStoreCampaign Campaign, string mobileNumber, string programCode, string campaignID, int TenantID, int UserID, string ClientAPIURL)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCustomerpopupDetailsList(mobileNumber, programCode, campaignID, TenantID, UserID, ClientAPIURL);

        }

        public List<StoreCampaignLogo> GetCampaignDetailsLogo(IStoreCampaign Campaign, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCampaignDetailsLogo(TenantID, UserID);

        }
    }
}
