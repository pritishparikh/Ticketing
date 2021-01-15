using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ClientCustomGetItemOnSearch
    {
        /// <summary>
        /// program code
        /// </summary>
        public string programcode { get; set; }


        /// <summary>
        /// search Criteria
        /// </summary>
        public string searchCriteria { get; set; }
    }


    public class ClientCustomGetItemOnSearchnew
    {
        /// <summary>
        /// program code
        /// </summary>
        public string programCode { get; set; }

        /// <summary>
        /// storeCode
        /// </summary>
        public string storeCode { get; set; }

        /// <summary>
        /// searchtext
        /// </summary>
        public string searchtext { get; set; }
    }
}
