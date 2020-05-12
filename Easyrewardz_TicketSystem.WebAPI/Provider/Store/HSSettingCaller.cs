using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider.Store
{
    public class HSSettingCaller
    {
        #region Variable
        public IHSSetting _IHSettingRepository;
        #endregion
        /// <summary>
        /// Store Agent List
        /// </summary>
        /// <param name="hSSetting"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<HSSettingModel> GetStoreAgentList(IHSSetting hSSetting, int tenantID, int BrandID, string StoreCode)
        {
            _IHSettingRepository = hSSetting;
            return _IHSettingRepository.GetStoreAgentList(tenantID, BrandID, StoreCode);
        }

        public int InsertUpdateAgentDetails(IHSSetting hSSetting, HSSettingModel hSSettingModel, int tenantID)
        {
            _IHSettingRepository = hSSetting;
            return _IHSettingRepository.InsertUpdateAgentDetails(hSSettingModel, tenantID);
        }

        public List<HSSettingModel> GetStoreAgentDetailsById(IHSSetting hSSetting, int tenantID, int AgentID)
        {
            _IHSettingRepository = hSSetting;
            return _IHSettingRepository.GetStoreAgentDetailsById(tenantID, AgentID);
        }

    }
}
