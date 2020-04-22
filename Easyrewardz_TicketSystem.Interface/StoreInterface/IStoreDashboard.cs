using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreDashboard
    {   
        List<StoreDashboardResponseModel> GetTaskDataForStoreDashboard(StoreDashboardModel model);



        List<StoreDashboardClaimResponseModel> GetClaimDataForStoreDashboard(StoreDashboardClaimModel model);


        LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserId, string ProfilePicPath);

    }
}
