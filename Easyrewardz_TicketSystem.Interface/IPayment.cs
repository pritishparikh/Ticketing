using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace Easyrewardz_TicketSystem.Interface
{
    public interface IPayment
    {
        /// <summary>
        /// Insert Cheque Details
        /// </summary>
        /// <param name="offlinePaymentModel"></param>
        /// <returns></returns>
        int InsertChequeDetails(OfflinePaymentModel offlinePaymentModel);
    }
}
