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

    public class CustomBillItemDetails
    {
        public string ArticleCode { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }

        public string Brand { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }

        public string Rate { get; set; }
        public string Discount { get; set; }
        public string Tax { get; set; }

        public string PaidAmount { get; set; }
        public string Tender { get; set; }
  


    }

}
