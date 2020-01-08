using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStore
    {
        List<StoreMaster> getStoreDetailByStorecodenPincode(string searchText, int tenantID);

        List<StoreMaster> getStores(string searchText, int tenantID);
    }
}
