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
        #region Custom Method

        /// <summary>
        /// Get Store Campaign Setting
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public StoreCampaignModel3 GetStoreCampignSetting(IStoreCampaign Campaign, int TenantId, int UserId, string ProgramCode)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetStoreCampignSetting( TenantId,  UserId,  ProgramCode);

        }

        /// <summary>
        /// Update Store Campaign Setting
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="CampaignModel"></param>
        /// <returns></returns>
        public int UpdateStoreCampaignSetting(IStoreCampaign Campaign, StoreCampaignSettingModel CampaignModel)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.UpdateStoreCampaignSetting(CampaignModel);

        }

        /// <summary>
        /// Update Campaign Max Click Timer
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="storeCampaignSettingTimer"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int UpdateCampaignMaxClickTimer(IStoreCampaign Campaign, StoreCampaignSettingTimer storeCampaignSettingTimer, int ModifiedBy)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.UpdateCampaignMaxClickTimer(storeCampaignSettingTimer, ModifiedBy);

        }

        /// <summary>
        /// Get Broadcast Configuration
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public StoreBroadcastConfiguration GetBroadcastConfiguration(IStoreCampaign Campaign, int tenantId, int userId, string programCode)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetBroadcastConfiguration(tenantId, userId, programCode);
        }

        /// <summary>
        /// Get Appointment Configuration
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public StoreAppointmentConfiguration GetAppointmentConfiguration(IStoreCampaign Campaign, int tenantId, int userId, string programCode)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetAppointmentConfiguration(tenantId, userId, programCode);
        }

        /// <summary>
        /// Update Broadcast Configuration
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="storeBroadcastConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public int UpdateBroadcastConfiguration(IStoreCampaign Campaign, StoreBroadcastConfiguration storeBroadcastConfiguration, int modifiedBy)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.UpdateBroadcastConfiguration(storeBroadcastConfiguration, modifiedBy);
        }

        /// <summary>
        /// Update Appointment Configuration
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="storeAppointmentConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public int UpdateAppointmentConfiguration(IStoreCampaign Campaign, StoreAppointmentConfiguration storeAppointmentConfiguration, int modifiedBy)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.UpdateAppointmentConfiguration(storeAppointmentConfiguration, modifiedBy);
        }

        /// <summary>
        /// Get Language Details
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public List<Languages> GetLanguageDetails(IStoreCampaign Campaign, int tenantId, int userId, string programCode)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetLanguageDetails(tenantId, userId, programCode);
        }

        /// <summary>
        /// Insert Language Details
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public int InsertLanguageDetails(IStoreCampaign Campaign, int tenantId, int userId, string programCode, int languageID)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.InsertLanguageDetails(tenantId, userId, programCode, languageID);
        }

        /// <summary>
        /// Get Selected Language Details
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public List<SelectedLanguages> GetSelectedLanguageDetails(IStoreCampaign Campaign, int tenantId, int userId, string programCode)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.GetSelectedLanguageDetails(tenantId, userId, programCode);
        }

        /// <summary>
        /// Delete Selected Language
        /// </summary>
        /// <param name="Campaign"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="selectedLanguageID"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public int DeleteSelectedLanguage(IStoreCampaign Campaign, int tenantId, int userId, string programCode, int selectedLanguageID, bool isActive)
        {
            _CampaignRepository = Campaign;
            return _CampaignRepository.DeleteSelectedLanguage(tenantId, userId, programCode, selectedLanguageID, isActive);
        }

        #endregion
    }
}
