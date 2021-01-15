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
        public async Task<List<StoreCampaignModel2>> GetStoreCampaign(IStoreCampaign Campaign, int TenantID, int UserID, string campaignName, string statusId)
        {
            _CampaignRepository = Campaign;
            return await _CampaignRepository.GetStoreCampaign(TenantID, UserID, campaignName, statusId);

        }


        #region Camapaign Customer Pop up  

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
        public async Task<StoresCampaignStatusResponse> GetCustomerpopupDetailsList(IStoreCampaign Campaign, string mobileNumber, string programCode, string campaignID, int TenantID, int UserID, string ClientAPIURL)
        {
            _CampaignRepository = Campaign;
            return await _CampaignRepository.GetCustomerpopupDetailsList(mobileNumber, programCode, campaignID, TenantID, UserID, ClientAPIURL);

        }

        /// <summary>
        /// Get Store Campaign Key Insights
        /// </summary>
        /// <param name="lifetimeValue"></param>
        /// <param name="VisitCount"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="campaignID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<StoreCampaignKeyInsight> GetStoreCampaignKeyInsight(IStoreCampaign Campaign, string lifetimeValue, string VisitCount, string mobileNumber, string programCode, string campaignID, int TenantID, int UserID, string ClientAPIURL)
        {
            _CampaignRepository = Campaign;
            return await _CampaignRepository.GetStoreCampaignKeyInsight( lifetimeValue,  VisitCount, mobileNumber, programCode, campaignID, TenantID, UserID, ClientAPIURL);

        }

        /// <summary>
        /// Get Campaign Recommendation List
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<StoreCampaignRecommended>> GetCampaignRecommendationList(IStoreCampaign Campaign, string mobileNumber, string programCode, int tenantID, int userID)
        {
            _CampaignRepository = Campaign;
            return await _CampaignRepository.GetCampaignRecommendationList( mobileNumber,  programCode,  tenantID,  userID);

        }

        /// <summary>
        /// Get Store Campaign Last Transaction Details
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<StoreCampaignLastTransactionDetails> GetStoreCampaignLastTransactionDetails(IStoreCampaign Campaign, string mobileNumber, string programCode, string ClientAPIURL)
        {
            _CampaignRepository = Campaign;
            return await _CampaignRepository.GetStoreCampaignLastTransactionDetails( mobileNumber,  programCode,  ClientAPIURL);

        }

        /// <summary>
        /// Get Share Campaign Via Setting
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<ShareCampaignViaSettingModal> GetShareCampaignViaSetting(IStoreCampaign Campaign, string mobileNumber, string programCode, int tenantID, int userID)
        {
            _CampaignRepository = Campaign;
            return await _CampaignRepository.GetShareCampaignViaSetting( mobileNumber,  programCode,  tenantID,  userID);

        }


        #endregion

        /// <summary>
        /// Get Campaign Details Logo
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<List<StoreCampaignLogo>> GetCampaignDetailsLogo(IStoreCampaign Campaign, int TenantID, int UserID)
        {
            _CampaignRepository = Campaign;
            return await _CampaignRepository.GetCampaignDetailsLogo(TenantID, UserID);

        }

        #endregion
    }
}
