using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStorePay
    {
        Task<string> GenerateStorePayLink(int TenantID, string ProgramCode, int UserID,
            string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, HSRequestGenerateToken hSRequestGenerateToken);

    }
}
