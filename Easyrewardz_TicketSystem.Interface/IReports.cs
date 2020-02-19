using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IReports
    {


        int InsertReport(int tenantId, string ReportName, bool isReportActive, string TicketReportParams,
            bool IsDaily, bool IsDailyForMonth,  bool IsWeekly, bool IsWeeklyForMonth, bool IsDailyForYear, bool IsWeeklyForYear , int createdBy);


        int DeleteReport(int tenantID, int ReportID);

        List<ReportModel> GetReportList(int tenantID);

        List<SearchResponseReport> GetReportSearch(ReportSearchModel searchModel);
    }
}
