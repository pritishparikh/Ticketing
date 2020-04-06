using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class AttachmentSettingColler
    {
        #region Variable declaration

        private IAttachmentSetting _AttachmentSetting;
        #endregion

        #region Methods 

        public AttachmentSettingResponseModel GetStoreAttachmentSettings(IAttachmentSetting AttachmentSetting, int TenantId, int CreatedBy)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.GetStoreAttachmentSettings(TenantId, CreatedBy);
        }

        public int ModifyStoreAttachmentSettings(IAttachmentSetting AttachmentSetting, int TenantId, int CreatedBy, AttachmentSettingsRequest AttachmentSettings)
        {
            _AttachmentSetting = AttachmentSetting;

            return _AttachmentSetting.ModifyStoreAttachmentSettings(TenantId, CreatedBy, AttachmentSettings);
        }

        #endregion
    }
}
