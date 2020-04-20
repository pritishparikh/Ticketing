using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreDashboard
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
        public LoggedInAgentModel GetLogginAccountInfo(IStoreDashboard dashboard, int tenantID, int UserId, string ProfilePicPath)
        {
            _dashboard = dashboard;
            return _dashboard.GetLogginAccountInfo(tenantID, UserId, ProfilePicPath);
        }
        #endregion
    }
}
