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
        public List<IssueTypeList> SearchIssueType(ISLA SLA, int TenantId, string SearchText)
        {
            _SLARepository = SLA;
            return _SLARepository.SearchIssueType(TenantId, SearchText);
        }

        public SLADetail GetSLADetail(ISLA SLA, int TenantID, int SLAID)
        {
            _SLARepository = SLA;
            return _SLARepository.GetSLADetail(TenantID, SLAID);
        }

        public bool UpdateSLADetails(ISLA SLA, SLADetail sLADetail, int TenantID, int UserID)
        {
            _SLARepository = SLA;
            return _SLARepository.UpdateSLADetails(sLADetail, TenantID, UserID);
        }
        public ValidateSLA ValidateSLAByIssueTypeID (ISLA SLA, int issueTypeID, int tenantID)
        {
            _SLARepository = SLA;
            return _SLARepository.ValidateSLAByIssueTypeID(issueTypeID, tenantID);
        }
    }
}
