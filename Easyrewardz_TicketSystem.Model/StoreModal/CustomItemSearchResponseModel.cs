using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class CustomItemSearchResponseModel
    {
        /// <summary>
        ///productName
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// uniqueItemCode
        /// </summary>
        public string uniqueItemCode { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// discount
        /// </summary>
        public string discount { get; set; }

        /// <summary>
        /// RedirectionUrl
        /// </summary>
        public string url { get; set; }


        /// <summary>
        /// imageURL
        /// </summary>
        public string imageURL { get; set; }



    }


}
