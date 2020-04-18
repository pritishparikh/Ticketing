using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreDashboard
    {   
        List<StoreDashboardResponseModel> GetTaskDataForStoreDashboard(StoreDashboardModel model);
        LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserId, string ProfilePicPath);
    }
}
