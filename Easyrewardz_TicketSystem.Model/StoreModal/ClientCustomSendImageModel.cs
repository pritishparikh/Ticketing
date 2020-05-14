using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ClientCustomSendImageModel
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
        /// imageUrl
        /// </summary>
        public string imageUrl { get; set; }
    }
}
