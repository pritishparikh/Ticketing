using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IReports
    {

        /// <summary>
        /// Insert Report
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="ReportName"></param>
        /// <param name="isReportActive"></param>
        /// <param name="TicketReportParams"></param>
        /// <param name="IsDaily"></param>
        /// <param name="IsDailyForMonth"></param>
        /// <param name="IsWeekly"></param>
        /// <param name="IsWeeklyForMonth"></param>
        /// <param name="IsDailyForYear"></param>
        /// <param name="IsWeeklyForYear"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        int InsertReport(int tenantId, string ReportName, bool isReportActive, string TicketReportParams,
            bool IsDaily, bool IsDailyForMonth,  bool IsWeekly, bool IsWeeklyForMonth, bool IsDailyForYear, bool IsWeeklyForYear , int createdBy);

        /// <summary>
        /// Delete Report
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        int DeleteReport(int tenantID, int ReportID);

        /// <summary>
        /// Save Report For Download
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="ScheduleID"></param>
        /// <returns></returns>
        int SaveReportForDownload(int tenantID,int UserID, int ScheduleID);

        /// <summary>
        /// Get Report List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<ReportModel> GetReportList(int tenantID);

        /// <summary>
        /// Get Report Search
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        int GetReportSearch(ReportSearchModel searchModel);

        /// <summary>
        /// Download Report Search
        /// </summary>
        /// <param name="SchedulerID"></param>
        /// <param name="curentUserId"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        string DownloadReportSearch(int SchedulerID, int curentUserId, int TenantID);

        /// <summary>
        /// Download Default Report
        /// </summary>
        /// <param name="defaultReportRequestModel"></param>
        /// <param name="curentUserId"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        string DownloadDefaultReport(DefaultReportRequestModel defaultReportRequestModel, int curentUserId, int TenantID);

        /// <summary>
        /// Send Report Mail
        /// </summary>
        /// <param name="EmailID"></param>
        /// <param name="FilePath"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserMasterID"></param>
        /// <returns></returns>
        bool SendReportMail(string EmailID, string FilePath, int TenantID, int UserMasterID);
    }
}
