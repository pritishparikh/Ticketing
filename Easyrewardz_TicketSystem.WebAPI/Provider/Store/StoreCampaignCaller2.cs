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

        #region Custom Method

        /// <summary>
        /// Get Store Campaign
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="campaignName"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public List<StoreCampaignModel2> GetStoreCampaign(IStoreCampaign Campaign, int TenantID, int UserID, string campaignName, string statusId)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetStoreCampaign(TenantID, UserID, campaignName, statusId);

        }

        /// <summary>
        /// Get Customer Popup Details List
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="campaignID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public StoresCampaignStatusResponse GetCustomerpopupDetailsList(IStoreCampaign Campaign, string mobileNumber, string programCode, string campaignID, int TenantID, int UserID, string ClientAPIURL)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCustomerpopupDetailsList(mobileNumber, programCode, campaignID, TenantID, UserID, ClientAPIURL);

        }

        /// <summary>
        /// Get Campaign Details Logo
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<StoreCampaignLogo> GetCampaignDetailsLogo(IStoreCampaign Campaign, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetCampaignDetailsLogo(TenantID, UserID);

        }

        #endregion
    }
}
