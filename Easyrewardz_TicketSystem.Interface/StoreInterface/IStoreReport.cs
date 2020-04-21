using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IStoreReport
    {
         int GetStoreReportSearch(StoreReportModel searchModel);

         int ScheduleStoreReport(ScheduleMaster scheduleMaster, int TenantID, int UserID);

        List<ReportModel> StoreReportList(int tenantID);

    }
}
