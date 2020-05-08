using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IStoreReport
    {
         int GetStoreReportSearch(StoreReportModel searchModel, List<StoreUserListing> StoreUserList);

        string DownloadStoreReportSearch(int SchedulerID, int UserID, int TenantID, List<StoreUserListing> StoreUserList);

        int ScheduleStoreReport(ScheduleMaster scheduleMaster, int TenantID, int UserID);

        List<ReportModel> StoreReportList(int tenantID);

        int DeleteStoreReport(int tenantID, int ReportID); 


        int SaveStoreReport(StoreReportRequest ReportMaster);


        List<CampaignScriptName> GetCampaignNames();

    }
}
