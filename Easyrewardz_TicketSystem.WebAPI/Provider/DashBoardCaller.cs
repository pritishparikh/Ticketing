using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class DashBoardCaller
    {

        #region Variable declaration

        private IDashBoard _dashboardlist;
        #endregion

        #region Methods 
        public DashBoardDataModel GetDashBoardCountData(IDashBoard _dashboard, string BrandID, string UserID, string fromdate, string todate, int TenantID)
        {
            _dashboardlist = _dashboard;

            return _dashboardlist.GetDashBoardCountData(BrandID, UserID, fromdate, todate, TenantID);
        }

        public DashBoardGraphModel GetDashBoardGraphdata(IDashBoard _dashboard, string BrandID, string UserID, string fromdate, string todate, int TenantID)
        {
            _dashboardlist = _dashboard;

            return _dashboardlist.GetDashBoardGraphdata(BrandID, UserID, fromdate, todate, TenantID);
        }

        public List<SearchResponseDashBoard> GetDashboardTicketsOnSearch(IDashBoard _dashboard, SearchModelDashBoard searchModel)
        {
            _dashboardlist = _dashboard;
            return _dashboardlist.GetDashboardTicketsOnSearch(searchModel);
        }

       public string DashBoardSearchDataToCSV(IDashBoard _dashboard, SearchModelDashBoard searchModel)
        {
            _dashboardlist = _dashboard;
            return _dashboardlist.DashBoardSearchDataToCSV(searchModel);
        }


        public LoggedInAgentModel GetLogginAccountInfo(IDashBoard _dashboard, int tenantID, int UserId, string emailID,string AccountName)
        {
            _dashboardlist = _dashboard;
            return _dashboardlist.GetLogginAccountInfo(tenantID,UserId,emailID, AccountName);
        }
        #endregion
    }
}
