using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IDashBoard
    {
        /// <summary>
        /// Get DashBoard Count Data
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="UserID"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        DashBoardDataModel GetDashBoardCountData(string BrandID, string UserID, string fromdate, string todate, int TenantID);

        /// <summary>
        /// Get DashBoard Graph data
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="UserID"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        DashBoardGraphModel GetDashBoardGraphdata(string BrandID, string UserID, string fromdate, string todate, int TenantID);

        /// <summary>
        /// Get Dashboard Tickets On Search
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        List<SearchResponseDashBoard> GetDashboardTicketsOnSearch(SearchModelDashBoard searchModel);

        /// <summary>
        /// DashBoard Search Data To CSV
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        string DashBoardSearchDataToCSV(SearchModelDashBoard searchModel);

        /// <summary>
        /// Get Loggin Account Info
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="ProfilePicPath"></param>
        /// <returns></returns>
        LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserId,string ProfilePicPath);

        /// <summary>
        /// Add DashBoard Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="SearchSaveName"></param>
        /// <param name="parameter"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int AddDashBoardSearch(int UserID, string SearchSaveName, string parameter, int TenantId);

        /// <summary>
        /// Delete DashBoard Saved Search
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int DeleteDashBoardSavedSearch(int SearchParamID, int UserID);

        /// <summary>
        /// List Saved DashBoard Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<UserTicketSearchMaster> ListSavedDashBoardSearch(int UserID);

        /// <summary>
        /// Get DashBoard Tickets On Saved Search
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        DashBoardSavedSearch GetDashBoardTicketsOnSavedSearch(int TenantID, int UserID, int SearchParamID);
    }
}
