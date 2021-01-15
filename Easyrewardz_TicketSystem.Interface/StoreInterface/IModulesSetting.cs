using Easyrewardz_TicketSystem.CustomModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IModulesSetting
    {
        /// <summary>
        /// Get Store Attachment Settings
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        AttachmentSettingResponseModel GetStoreAttachmentSettings(int TenantId, int CreatedBy);

        /// <summary>
        /// Modify Store Attachment Settings
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="AttachmentSettings"></param>
        /// <returns></returns>
        int ModifyStoreAttachmentSettings(int TenantId, int CreatedBy, AttachmentSettingsRequest AttachmentSettings);

        /// <summary>
        /// Get Campaign Script
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CampaignID"></param>
        /// <returns></returns>
        List<CampaignScriptDetails> GetCampaignScript(int TenantId, int CampaignID);

        /// <summary>
        /// Validate CampaignName Exit
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<CampaignScriptName> GetCampaignName(int TenantId);

        /// <summary>
        /// Validate CampaignName Exit
        /// </summary>
        /// <param name="CampaignNameID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        string ValidateCampaignNameExit(int CampaignNameID, int TenantID);

        /// <summary>
        /// Insert Campaign Script
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        int InsertCampaignScript(int TenantId, int CreatedBy, CampaignScriptRequest Campaignscript);

        /// <summary>
        /// Update Campaign Script
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        int UpdateCampaignScript(int TenantId, int CreatedBy, CampaignScriptRequest Campaignscript);

        /// <summary>
        /// Delete Campaign Script
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CampaignID"></param>
        /// <returns></returns>
        int DeleteCampaignScript(int TenantId, int CreatedBy, int CampaignID);

        /// <summary>
        /// Campaign Bulk Upload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> CampaignBulkUpload(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);
    }
}
