using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Master tables
    /// </summary>
   public interface IMasterInterface
    {
        List<ChannelOfPurchase> GetChannelOfPurchaseList(int TenantID);

        List<DepartmentMaster> GetDepartmentList(int TenantID);

        List<FuncationMaster> GetFunctionByDepartment(int DepartmentID,int TenantID);

        List<PaymentMode> GetPaymentMode();
    }
}
