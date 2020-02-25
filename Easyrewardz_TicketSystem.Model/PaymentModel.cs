using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class PaymentModel
    {
        public List<OfflinePaymentModel> offlinePayments { get; set; }

    }

    public class OfflinePaymentModel
    {
        public string ChequeNumber { get; set; }

        public decimal ChequeAmount { get; set; }

        public DateTime ChequeDate { get; set; }

        public string FromCompanyName { get; set; }

        public string PaidToname { get; set; }

        public int CreatedBy { get; set; }

        public int TenantID { get; set; }

        public int PaymentModeID { get; set; }


    }
}
