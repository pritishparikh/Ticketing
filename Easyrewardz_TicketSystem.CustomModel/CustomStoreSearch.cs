using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomStoreSearch
    {
        public string programCode { get; set; }
        public string storeAddressPin { get; set; }

        public string securityToken { get; set; }
        public int userID { get; set; }
        public int appID { get; set; }
    }
}
