using Easyrewardz_TicketSystem.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IAttachmentSetting
    {
        AttachmentSettingResponseModel GetStoreAttachmentSettings(int TenantId, int CreatedBy);

        int ModifyStoreAttachmentSettings(int TenantId, int CreatedBy, AttachmentSettingsRequest AttachmentSettings);
    }
}
