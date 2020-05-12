using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IHSSetting
    {
        List<HSSettingModel> GetStoreAgentList(int tenantID, int BrandID, string StoreCode);

        int InsertUpdateAgentDetails(HSSettingModel hSSettingModel, int tenantID);

        List<HSSettingModel> GetStoreAgentDetailsById(int tenantID, int AgentID);
    }
}
