using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        List<StoreCampaignModel2> GetStoreCampaign(int tenantID, int userID);
    }
}
