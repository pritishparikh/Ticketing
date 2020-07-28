using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ClientChatAddProduct
    {
        /// <summary>
        ///ProgramCode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        public string CustomerMobile { get; set; }


        /// <summary>
        /// Product Details
        /// </summary>
        public CustomItemSearchResponseModel ProductDetails { get; set; }
    }
}
