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


        public LoggedInAgentModel GetLogginAccountInfo(IDashBoard _dashboard, int tenantID, int UserId, string ProfilePicPath)
        {
            _dashboardlist = _dashboard;
            return _dashboardlist.GetLogginAccountInfo(tenantID,UserId, ProfilePicPath); 
        }


        public int AddDashBoardSearch(IDashBoard _dashboard, int UserID, string SearchParamID, string parameter, int TenantId)
        {
            _dashboardlist = _dashboard;
            return _dashboardlist.AddDashBoardSearch(UserID, SearchParamID, parameter, TenantId);

        }

        public int DeleteDashBoardSavedSearch(IDashBoard _dashboard, int SearchParamID, int UserID)
        {
            _dashboardlist = _dashboard;
            return _dashboardlist.DeleteDashBoardSavedSearch(SearchParamID, UserID);

        }

        public List<UserTicketSearchMaster> ListSavedDashBoardSearch(IDashBoard _dashboard, int UserID)
        {
            _dashboardlist = _dashboard;
            return _dashboardlist.ListSavedDashBoardSearch(UserID);

        }

        public DashBoardSavedSearch GetDashBoardTicketsOnSavedSearch(IDashBoard _dashboard, int TenantID, int UserID, int SearchParamID)
        {
            _dashboardlist = _dashboard;
            return _dashboardlist.GetDashBoardTicketsOnSavedSearch(TenantID, UserID, SearchParamID);
        }

        #endregion
    }
}
