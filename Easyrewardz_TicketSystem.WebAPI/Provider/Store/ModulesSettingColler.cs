using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class ModulesSettingColler
    {
        #region Variable declaration

        private IModulesSetting _AttachmentSetting;
        #endregion

        #region Methods 

        /// <summary>
        /// Get Store Attachment Settings
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public AttachmentSettingResponseModel GetStoreAttachmentSettings(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.GetStoreAttachmentSettings(TenantId, CreatedBy);
        }

        /// <summary>
        /// Modify Store Attachment Settings
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="AttachmentSettings"></param>
        /// <returns></returns>
        public int ModifyStoreAttachmentSettings(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy, AttachmentSettingsRequest AttachmentSettings)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.ModifyStoreAttachmentSettings(TenantId, CreatedBy, AttachmentSettings);
        }

        /// <summary>
        /// Get Campaign Script
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="TenantId"></param>
        /// <param name="_CampaignID"></param>
        /// <returns></returns>
        public List<CampaignScriptDetails> GetCampaignScript(IModulesSetting AttachmentSetting, int TenantId, int _CampaignID)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.GetCampaignScript(TenantId, _CampaignID);
        }

        /// <summary>
        /// Get CampaignName
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<CampaignScriptName> GetCampaignName(IModulesSetting AttachmentSetting, int TenantId)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.GetCampaignName(TenantId);
        }

        /// <summary>
        /// Validate CampaignName Exit
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="CampaignNameID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public string ValidateCampaignNameExit(IModulesSetting AttachmentSetting, int CampaignNameID, int TenantID)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.ValidateCampaignNameExit(CampaignNameID, TenantID);
        }

        /// <summary>
        /// Insert Campaign Script
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        public int InsertCampaignScript(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy, CampaignScriptRequest Campaignscript)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.InsertCampaignScript(TenantId, CreatedBy, Campaignscript);
        }

        /// <summary>
        /// Update Campaign Script
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        public int UpdateCampaignScript(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy, CampaignScriptRequest Campaignscript)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.UpdateCampaignScript(TenantId, CreatedBy, Campaignscript);
        }

        /// <summary>
        /// Delete Campaign Script
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CampaignID"></param>
        /// <returns></returns>
        public int DeleteCampaignScript(IModulesSetting AttachmentSetting, int TenantId, int CreatedBy, int CampaignID)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.DeleteCampaignScript(TenantId, CreatedBy, CampaignID);
        }

        /// <summary>
        /// Campaign Bulk Upload
        /// </summary>
        /// <param name="AttachmentSetting"></param>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public List<string> CampaignBulkUpload(IModulesSetting AttachmentSetting, int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            _AttachmentSetting = AttachmentSetting;
            return _AttachmentSetting.CampaignBulkUpload(TenantID, CreatedBy, CategoryFor, DataSetCSV);
        }
        #endregion
    }
}
