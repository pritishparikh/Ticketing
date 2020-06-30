using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IStoreReport
    {
        /// <summary>
        /// Get Store Report Search
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="StoreUserList"></param>
        /// <returns></returns>
        int GetStoreReportSearch(StoreReportModel searchModel, List<StoreUserListing> StoreUserList);

        /// <summary>
        /// Download Store Report Search
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="UserID"></param>
        /// <param name="TenantID"></param>
        /// <param name="StoreUserList"></param>
        /// <returns></returns>
        string DownloadStoreReportSearch(int ReportID, int UserID, int TenantID, List<StoreUserListing> StoreUserList);

        /// <summary>
        /// Check If Report Name Exists
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="ReportName"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        bool CheckIfReportNameExists(int ReportID, string ReportName, int TenantID);

        /// <summary>
        /// Schedule Store Report
        /// </summary>
        /// <param name="scheduleMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int ScheduleStoreReport(ScheduleMaster scheduleMaster, int TenantID, int UserID);

        /// <summary>
        /// Store Report List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<ReportModel> StoreReportList(int tenantID);

        /// <summary>
        /// Delete Store Report
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        int DeleteStoreReport(int tenantID, int ReportID);

        /// <summary>
        /// Save Store Report
        /// </summary>
        /// <param name="ReportMaster"></param>
        /// <returns></returns>
        int SaveStoreReport(StoreReportRequest ReportMaster);

        /// <summary>
        /// Get Campaign Names
        /// </summary>
        /// <returns></returns>
        List<CampaignScriptName> GetCampaignNames();

    }
}
