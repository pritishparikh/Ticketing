using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ClientCustomSendTextModel
    {
        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// textToReply
        /// </summary>
        public string textToReply { get; set; }

        /// <summary>
        /// programCode
        /// </summary>
        public string programCode { get; set; }

      

    }

    public class ClientCustomSendProductModel
    {
        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// textToReply
        /// </summary>
        public string textToReply { get; set; }

        /// <summary>
        /// programCode
        /// </summary>
        public string programCode { get; set; }

        /// <summary>
        /// programCode
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// ShoppingBag flag
        /// </summary>
        public string ShoppingBag { get; set; }

        /// <summary>
        /// Like flag 
        /// </summary>
        public string Like { get; set; }

    }
}
