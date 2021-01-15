using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        List<CustomClaimList> GetClaimDataForStoreDashboard(StoreDashboardClaimModel model);

        /// <summary>
        /// Get Loggin Account Info
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="ProfilePicPath"></param>
        /// <returns></returns>
        LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserId, string ProfilePicPath);
        /// <summary>
        /// Get Store By Tenant ID
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        Task<List<GetStoreByTenantModel>> GetStoreByTenantID(int tenantID, int userID);

        /// <summary>
        /// Store User Productivity Report
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<StoreUserProductivityReport>> StoreUserProductivityReport(int tenantID, int userID, ReportDataRequest reportDataRequest);

        /// <summary>
        /// Store Productivity Report
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<StoreProductivityReport>> StoreProductivityReport(int tenantID, int userID, ReportDataRequest reportDataRequest);
    }
}
