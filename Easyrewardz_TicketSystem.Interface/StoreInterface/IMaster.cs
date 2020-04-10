using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IMaster
    {
        List<StoreUser> GetStoreUserList(int TenantId, int UserID);

        List<StoreFunctionModel> GetStoreFunctionList(int TenantId, int UserID);
    }
}
