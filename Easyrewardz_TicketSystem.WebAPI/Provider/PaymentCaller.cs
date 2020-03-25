using System;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;


namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class PaymentCaller
    {
        #region Variable
        private IPayment _PaymentList;
        #endregion

        #region Method

        public int InsertChequeDetails(IPayment _payment, OfflinePaymentModel offlinePaymentModel)
        {
            _PaymentList = _payment;
            return _PaymentList.InsertChequeDetails(offlinePaymentModel);
        }

        #endregion
    }
}
