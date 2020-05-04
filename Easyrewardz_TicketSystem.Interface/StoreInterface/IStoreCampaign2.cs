using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        List<StoreCampaignModel2> GetStoreCampaign(int tenantID, int userID);

        StoresCampaignStatusResponse GetCustomerpopupDetailsList(string mobileNumber, string programCode, int tenantID, int userID, string ClientAPIURL);

        List<StoreCampaignLogo> GetCampaignDetailsLogo(int tenantID, int userID);
    }
}
