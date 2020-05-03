using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class CustomItemSearchResponseModel
    {
        /// <summary>
        /// Item Image URL
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// Item Image URL
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// AlterbativeText
        /// </summary>
        public string AlternativeText { get; set; }

        /// <summary>
        /// RedirectionUrl
        /// </summary>
        public string RedirectionUrl { get; set; }


    }
}
