using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        Task<List<StoreCampaignModel2>> GetStoreCampaign(int tenantID, int userID, string campaignName, string statusId);

        #region Campaign Customer Pop up  

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
        Task<StoresCampaignStatusResponse> GetCustomerpopupDetailsList(string mobileNumber, string programCode, string campaignID, int tenantID, int userID, string ClientAPIURL);

        /// <summary>
        /// Get Store Campaign Key Insights
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="campaignID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        Task<StoreCampaignKeyInsight> GetStoreCampaignKeyInsight(string lifetimeValue, string VisitCount, string mobileNumber, string programCode, string campaignID, int tenantID, int userID, string ClientAPIURL);

        /// <summary>
        /// Get Campaign Recommendation List
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<StoreCampaignRecommended>> GetCampaignRecommendationList(string mobileNumber, string programCode, int tenantID, int userID);

        /// <summary>
        /// Get Store Campaign Last Transaction Details
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        Task<StoreCampaignLastTransactionDetails> GetStoreCampaignLastTransactionDetails(string mobileNumber, string programCode, string ClientAPIURL);

        /// <summary>
        /// Get Share Campaign Via Setting
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<ShareCampaignViaSettingModal> GetShareCampaignViaSetting(string mobileNumber, string programCode, int tenantID, int userID);

        #endregion


        /// <summary>
        /// Get Campaign Details Logo
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<StoreCampaignLogo>> GetCampaignDetailsLogo(int tenantID, int userID);
    }
}
