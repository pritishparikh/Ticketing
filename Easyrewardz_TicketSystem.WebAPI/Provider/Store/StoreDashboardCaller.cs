using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreDashboardCaller
    {

        private IStoreDashboard _dashboard;

        #region _dashboard
        /// <summary>
        ///get store Dashborad Details
        /// </summary>
        public List<StoreDashboardResponseModel> getStoreDashboardTaskList(IStoreDashboard dashboard, StoreDashboardModel modelname)
        {
            _dashboard = dashboard;
            return _dashboard.GetTaskDataForStoreDashboard(modelname);
        }

        #endregion




        #region _dashboard
        /// <summary>
        ///get store Dashborad Details For Claim
        /// </summary>
        public List<CustomClaimList> getStoreDashboardClaimList(IStoreDashboard DashBoard, StoreDashboardClaimModel ClaimSearchModel)
        {
            _dashboard = DashBoard;
            return _dashboard.GetClaimDataForStoreDashboard(ClaimSearchModel);
        }

        public LoggedInAgentModel GetLogginAccountInfo(IStoreDashboard dashboard, int tenantID, int UserId, string ProfilePicPath)
        {
            _dashboard = dashboard;
            return _dashboard.GetLogginAccountInfo(tenantID, UserId, ProfilePicPath);

        }
        /// <summary>
        ///get store  By Tenant
        /// </summary>
        public async Task<List<GetStoreByTenantModel>> GetStoreByTenantID(IStoreDashboard DashBoard, int tenantID, int userID)
        {
            _dashboard = DashBoard;
            return await _dashboard.GetStoreByTenantID(tenantID, userID);
        }

        /// <summary>
        ///StoreUserProductivityReport
        /// </summary>
        public async Task<List<StoreUserProductivityReport>> StoreUserProductivityReport(IStoreDashboard DashBoard, int tenantID, int userID, ReportDataRequest reportDataRequest)
        {
            _dashboard = DashBoard;
            return await _dashboard.StoreUserProductivityReport(tenantID, userID, reportDataRequest);
        }

        /// <summary>
        ///StoreUserProductivityReport
        /// </summary>
        public async Task<List<StoreProductivityReport>> StoreProductivityReport(IStoreDashboard DashBoard, int tenantID, int userID, ReportDataRequest reportDataRequest)
        {
            _dashboard = DashBoard;
            return await _dashboard.StoreProductivityReport(tenantID, userID, reportDataRequest);
        }
        #endregion
    }
}
