using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IDashBoard
    {
        DashBoardDataModel GetDashBoardCountData(string BrandID, string UserID, string fromdate, string todate, int TenantID);

        DashBoardGraphModel GetDashBoardGraphdata(string BrandID, string UserID, string fromdate, string todate, int TenantID);
        List<SearchResponseDashBoard> GetDashboardTicketsOnSearch(SearchModelDashBoard searchModel);
        string DashBoardSearchDataToCSV(SearchModelDashBoard searchModel);
        LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserId, string emailID,string AccountName);


    }
}
