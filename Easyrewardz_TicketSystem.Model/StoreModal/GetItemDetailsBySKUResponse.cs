using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
   public  class GetItemDetailsBySKUResponse
    {
        public List<SKUDetails> data { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string appVersion { get; set; }
    }

    public class SKUDetails
    {
        public string skuId { get; set; }
        public string defaultImageUrl { get; set; }
        public string brandName { get; set; }
        public string categoryName { get; set; }
        public string gender { get; set; }
        public string groupCode { get; set; }
        public string itemDescription { get; set; }
        public string name { get; set; }
        public int availability { get; set; }
        public string size { get; set; }
        public string colour { get; set; }
        public string price { get; set; }
        public List<string> catalogueUrl { get; set; }
        public Properties properties { get; set; }

    }

    public class Properties
    {
    }

    public class GetItemDetailsBySKURequest
    {
        public string programCode { get; set; }
        public string storeCode { get; set; }
        public List<string> skUs { get; set; }
    }
}
