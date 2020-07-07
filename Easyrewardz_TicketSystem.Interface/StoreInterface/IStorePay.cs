using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IStorePay
    {
        string GenerateStorePayLink(int TenantID, string ProgramCode, int UserID);


    }
}
