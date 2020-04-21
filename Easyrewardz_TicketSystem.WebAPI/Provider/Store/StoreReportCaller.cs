using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreReportCaller 
    {
        #region Variable declaration

        private IStoreReport _Reports;

        #endregion

        public int StoreReportSearch(IStoreReport Report, StoreReportModel searchModel)
        {
            _Reports = Report;
            return _Reports.GetStoreReportSearch(searchModel);
        }


        public int ScheduleStoreReport(IStoreReport Report, ScheduleMaster scheduleMaster, int TenantID, int UserID)
        {
            _Reports = Report;
            return _Reports.ScheduleStoreReport(scheduleMaster, TenantID, UserID);

        }

    }
}
