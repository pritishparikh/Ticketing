using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        List<CampaignCustomerModel> GetCampaignCustomer(int tenantID, int userID, int campaignScriptID, int pageNo, int pageSize);
    }
}
