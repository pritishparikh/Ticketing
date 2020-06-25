using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        #region Custom Method

        /// <summary>
        /// Get Store Campaign Setting
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        StoreCampaignModel3 GetStoreCampignSetting(int TenantId, int UserId, string ProgramCode);

        /// <summary>
        /// Update Store Campaign Setting
        /// </summary>
        /// <param name="CampaignModel"></param>
        /// <returns></returns>
        int UpdateStoreCampaignSetting(StoreCampaignSettingModel CampaignModel);

        /// <summary>
        /// Update Campaign Max Click Timer
        /// </summary>
        /// <param name="storeCampaignSettingTimer"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        int UpdateCampaignMaxClickTimer(StoreCampaignSettingTimer storeCampaignSettingTimer, int ModifiedBy);

        /// <summary>
        /// Get Broadcast Configuration
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        StoreBroadcastConfiguration GetBroadcastConfiguration(int tenantId, int userId, string programCode);

        /// <summary>
        /// Get Appointment Configuration
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        StoreAppointmentConfiguration GetAppointmentConfiguration(int tenantId, int userId, string programCode);

        /// <summary>
        /// Update Broadcast Configuration
        /// </summary>
        /// <param name="storeBroadcastConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        int UpdateBroadcastConfiguration(StoreBroadcastConfiguration storeBroadcastConfiguration, int modifiedBy);

        /// <summary>
        /// Update Appointment Configuration
        /// </summary>
        /// <param name="storeAppointmentConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        int UpdateAppointmentConfiguration(StoreAppointmentConfiguration storeAppointmentConfiguration, int modifiedBy);

        /// <summary>
        /// Get Language Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        List<Languages> GetLanguageDetails(int tenantId, int userId, string programCode);

        /// <summary>
        /// Insert Language Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        int InsertLanguageDetails(int tenantId, int userId, string programCode, int languageID);

        /// <summary>
        /// Get Selected Language Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        List<SelectedLanguages> GetSelectedLanguageDetails(int tenantId, int userId, string programCode);

        /// <summary>
        /// Delete Selected Language
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="selectedLanguageID"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        int DeleteSelectedLanguage(int tenantId, int userId, string programCode, int selectedLanguageID, bool isActive);

        #endregion
    }
}
