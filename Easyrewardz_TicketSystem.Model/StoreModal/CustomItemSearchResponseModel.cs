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
        /// categoryName
        /// </summary>
        public string categoryName { get; set; }

        /// <summary>
        /// subCategoryName
        /// </summary>
        public string subCategoryName { get; set; }

        /// <summary>
        /// color
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// colorCode
        /// </summary>
        public string colorCode { get; set; }

        /// <summary>
        /// brandName
        /// </summary>
        public string brandName { get; set; }

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

        /// <summary>
        /// size
        /// </summary>
        public string size { get; set; }



    }


   
    public class ItemProperties
    {
        public string Type { get; set; }
        public string Use { get; set; }
    }

    public class CustomItemSearchWBResponseModel
    {
        public string name { get; set; }
        public List<string> barCode { get; set; }
        public string itemCode { get; set; }
        public string brandName { get; set; }
        public string itemDescription { get; set; }
        public List<string> availableSize { get; set; }
        public List<string> sizeAvailability { get; set; }
        public List<string> sizePrice { get; set; }
        public string price { get; set; }
        public string discount { get; set; }
        public string inventoryCount { get; set; }
        public string imageUrl { get; set; }
        public string categoryName { get; set; }
        public string gender { get; set; }
        public List<string> catalogueUrl { get; set; }
        public ItemProperties properties { get; set; }
    }




}
