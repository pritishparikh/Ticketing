using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class LastTransactionDetailsResponseModel
    {
        public string mobileNo { get; set; }
        public string billNo { get; set; }
        public string billDate { get; set; }
        public string storeName { get; set; }
        public string amount { get; set; }
        public List<ItemDetails> itemDetails { get; set; }

    }

    public class ItemDetails
    {
        public string mobileNo { get; set; }
        public string article { get; set; }
        public string quantity { get; set; }
        public string amount { get; set; }

    }
}
