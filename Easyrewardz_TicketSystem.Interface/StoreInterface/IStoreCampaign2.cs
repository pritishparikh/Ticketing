using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        /// <summary>
        /// Get Store Campaign
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="campaignName"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        List<StoreCampaignModel2> GetStoreCampaign(int tenantID, int userID, string campaignName, string statusId);

        /// <summary>
        /// Get Customer Popup Details List
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="campaignID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        StoresCampaignStatusResponse GetCustomerpopupDetailsList(string mobileNumber, string programCode, string campaignID, int tenantID, int userID, string ClientAPIURL);

        /// <summary>
        /// Get Campaign Details Logo
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<StoreCampaignLogo> GetCampaignDetailsLogo(int tenantID, int userID);
    }
}
