using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace Easyrewardz_TicketSystem.Interface
{
    public interface IPayment
    {
        int InsertChequeDetails(OfflinePaymentModel offlinePaymentModel);
    }
}
