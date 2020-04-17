using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IGraph
    {
        List<User> GetUserList(int TenantID, int UserID);
        GraphModal GetGraphCountData(int TenantID, int UserID, string UserIds, string BrandIDs);
    }
}
