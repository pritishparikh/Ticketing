using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IHSSetting
    {
        /// <summary>
        /// Get Store Agent List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="BrandID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        List<HSSettingModel> GetStoreAgentList(int tenantID, int BrandID, int StoreID);

        /// <summary>
        /// Insert And Update Agent Details
        /// </summary>
        /// <param name="hSSettingModel"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        int InsertUpdateAgentDetails(HSSettingModel hSSettingModel, int tenantID);

        /// <summary>
        /// Get Store Agent Details By Id
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        List<HSSettingModel> GetStoreAgentDetailsById(int tenantID, int AgentID);

    }
}
