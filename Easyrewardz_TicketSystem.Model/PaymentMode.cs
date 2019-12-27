using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class PaymentMode
    {
        /// <summary>
        /// Id of the Payment Mode
        /// </summary>
        public int PaymentModeID { get; set; }

        public string PaymentModename { get; set; }

        public bool IsOnline { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? ModifyBy { get; set; }

        public int? ModifiedDate { get; set; }


    }
}
