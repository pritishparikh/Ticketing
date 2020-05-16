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
       

        public StoreCampaignModel3 GetStoreCampignSetting(IStoreCampaign Campaign, int TenantId, int UserId, string ProgramCode)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetStoreCampignSetting( TenantId,  UserId,  ProgramCode);

        }

        public int UpdateStoreCampaignSetting(IStoreCampaign Campaign, StoreCampaignSettingModel CampaignModel)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.UpdateStoreCampaignSetting(CampaignModel);

        }

    }
}
