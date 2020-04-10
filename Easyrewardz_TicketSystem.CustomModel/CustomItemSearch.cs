﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomItemSearch
    {
        public string programCode { get; set; }
        public string invoiceNumber { get; set; }
        public string storeCode { get; set; }
        public string invoiceDate { get; set; }
        public string securityToken { get; set; }
        public int userID { get; set; }
        public int appID { get; set; }
    }
}
