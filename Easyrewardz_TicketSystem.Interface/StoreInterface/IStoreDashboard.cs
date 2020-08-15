using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreDashboard
    {
        /// <summary>
        /// Get Task Data For Store Dashboard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<StoreDashboardResponseModel> GetTaskDataForStoreDashboard(StoreDashboardModel model);

        /// <summary>
        /// Get Claim Data For Store Dashboard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<StoreDashboardClaimResponseModel> GetClaimDataForStoreDashboard(StoreDashboardClaimModel model);

        /// <summary>
        /// Get Loggin Account Info
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="ProfilePicPath"></param>
        /// <returns></returns>
        LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserId, string ProfilePicPath);

    }
}
