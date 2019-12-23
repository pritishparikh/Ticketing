using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStore
    {
        List<StoreMaster> getStoreDetailByStorecodenPincode(string Storename, string Storecode, int Pincode);
    }
}
