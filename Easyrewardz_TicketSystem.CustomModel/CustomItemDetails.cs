using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomItemDetails
    {
        public string ArticleNumber { get; set; }
        public string ArticleSize { get; set; }
        public string ArticleMrp { get; set; }

        public string PricePaid { get; set; }
        
        public string StoreCode { get; set; }
        public string Discount { get; set; }
    }
}
