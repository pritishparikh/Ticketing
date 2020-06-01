using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IStoreCampaign
    {
        StoreCampaignModel3 GetStoreCampignSetting(int TenantId, int UserId, string ProgramCode);

        int UpdateStoreCampaignSetting(StoreCampaignSettingModel CampaignModel);

        int UpdateCampaignMaxClickTimer(StoreCampaignSettingTimer storeCampaignSettingTimer, int ModifiedBy);

        StoreBroadcastConfiguration GetBroadcastConfiguration(int tenantId, int userId, string programCode);

        StoreAppointmentConfiguration GetAppointmentConfiguration(int tenantId, int userId, string programCode);

        int UpdateBroadcastConfiguration(StoreBroadcastConfiguration storeBroadcastConfiguration, int modifiedBy);

        int UpdateAppointmentConfiguration(StoreAppointmentConfiguration storeAppointmentConfiguration, int modifiedBy);

        List<Languages> GetLanguageDetails(int tenantId, int userId, string programCode);

        int InsertLanguageDetails(int tenantId, int userId, string programCode, int languageID);

        List<SelectedLanguages> GetSelectedLanguageDetails(int tenantId, int userId, string programCode);

        int DeleteSelectedLanguage(int tenantId, int userId, string programCode, int selectedLanguageID, bool isActive);
    }
}
