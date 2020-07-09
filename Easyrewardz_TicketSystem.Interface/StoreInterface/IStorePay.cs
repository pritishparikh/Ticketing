using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStorePay
    {
        string GenerateStorePayLink(int TenantID, string ProgramCode, int UserID,
            string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, HSRequestGenerateToken hSRequestGenerateToken);

    }
}
