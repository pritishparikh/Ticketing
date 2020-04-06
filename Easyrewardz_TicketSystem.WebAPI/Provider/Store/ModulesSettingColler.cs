using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class ModulesSettingColler
    {
        #region Variable declaration

        private IModulesSetting _AttachmentSetting;
        #endregion

        #region Methods 

        public AttachmentSettingResponseModel GetStoreAttachmentSettings(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.GetStoreAttachmentSettings(TenantId, CreatedBy);
        }

        public int ModifyStoreAttachmentSettings(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy, AttachmentSettingsRequest AttachmentSettings)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.ModifyStoreAttachmentSettings(TenantId, CreatedBy, AttachmentSettings);
        }

        public List<CampaignScriptDetails> GetCampaignScript(IModulesSetting AttachmentSetting, int TenantId, int _CampaignID)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.GetCampaignScript(TenantId, _CampaignID);
        }

        public List<CampaignScriptName> GetCampaignName(IModulesSetting AttachmentSetting, int TenantId)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.GetCampaignName(TenantId);
        }

        public string ValidateCampaignNameExit(IModulesSetting AttachmentSetting, int CampaignNameID, int TenantID)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.ValidateCampaignNameExit(CampaignNameID, TenantID);
        }

        public int InsertCampaignScript(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy, CampaignScriptRequest Campaignscript)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.InsertCampaignScript(TenantId, CreatedBy, Campaignscript);
        }

        public int UpdateCampaignScript(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy, CampaignScriptRequest Campaignscript)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.UpdateCampaignScript(TenantId, CreatedBy, Campaignscript);
        }

        public int DeleteCampaignScript(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy, int CampaignID)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.DeleteCampaignScript(TenantId, CreatedBy, CampaignID);
        }

        #endregion
    }
}
