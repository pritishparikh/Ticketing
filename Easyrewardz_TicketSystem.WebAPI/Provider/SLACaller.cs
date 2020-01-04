using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{

    public class SLACaller
    {
        #region Variable
        public ISLA _SLARepository;
        #endregion
        /// <summary>
        /// Search By Category
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<SLAStatus> GetSLAStatusList(ISLA SLA, int TenantId)
        {
            _SLARepository = SLA;
            return _SLARepository.GetSLAStatusList(TenantId);
        }
    }
}
