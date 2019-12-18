using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IMasterInterface
    {
        List<ChannelOfPurchase> GetChannelOfPurchaseList(int TenantID);
    }
}
