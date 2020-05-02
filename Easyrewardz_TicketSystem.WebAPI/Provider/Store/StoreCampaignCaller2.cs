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

        public List<StoreCampaignModel2> GetStoreCampaign(IStoreCampaign Campaign, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetStoreCampaign(TenantID, UserID);

        }

        public CampaignStatusResponse1 GetCustomerpopupDetailsList(IStoreCampaign Campaign, string mobileNumber, string programCode, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCustomerpopupDetailsList(mobileNumber, programCode, TenantID, UserID);

        }

        public List<StoreCampaignLogo> GetCampaignDetailsLogo(IStoreCampaign Campaign, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCampaignDetailsLogo(TenantID, UserID);

        }
    }
}
