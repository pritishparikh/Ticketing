using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class CustomerChatReplyModel
    {
        /// <summary>
        /// ProgramCode
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// StoreCode
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// Mobile number of the customer
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Message sent by the customer
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Datetime of the message
        /// </summary>
        public string DateTime { get; set; }

    }
}
