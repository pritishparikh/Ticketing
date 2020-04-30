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
        public List<CampaignCustomerModel> GetCampaignCustomer(IStoreCampaign Campaign, int tenantID, int userID, int campaignScriptID, int pageNo, int pageSize)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCampaignCustomer(tenantID, userID, campaignScriptID, pageNo, pageSize);

        }

    }
}
