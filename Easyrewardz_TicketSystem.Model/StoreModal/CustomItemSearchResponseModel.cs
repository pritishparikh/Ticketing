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
        public string Brand { get; set; }
        public string Fit { get; set; }
        public string Pattern { get; set; }
        public string ProductMaterial { get; set; }
        public string Sleeves { get; set; }
        public string Occasion { get; set; }
        public string Color { get; set; }
        public string Cuffs { get; set; }
        public string Subbrand { get; set; }
        public string ProductType { get; set; }
        public string Season { get; set; }
        public string Collar { get; set; }
        public string WashInstructions { get; set; }
        public string Neck { get; set; }
        public string ProductWidthincm { get; set; }
        public string ProductLengthincm { get; set; }
        public string Manufacturer { get; set; }
        public string Price { get; set; }
    }



    public class Items
    {
        public string name { get; set; }
        public List<List<string>> barCode { get; set; }
        public string itemCode { get; set; }
        public string brandName { get; set; }
        public string itemDescription { get; set; }
        public List<string> sizeData { get; set; }
        public List<List<string>> availableSize { get; set; }
        public List<List<string>> availableColor { get; set; }
        public List<List<string>> sizeAvailability { get; set; }
        public List<List<string>> sizePrice { get; set; }
        public string price { get; set; }
        public string discount { get; set; }
        public string inventoryCount { get; set; }
        public string imageUrl { get; set; }
        public string categoryName { get; set; }
        public string gender { get; set; }
        public List<List<string>> catalogueUrl { get; set; }
        public ItemProperties properties { get; set; }
        public string groupCode { get; set; }
        public string subcategory { get; set; }
        public string scCat { get; set; }
        public string scSubCat { get; set; }
        public List<List<string>> colourCode { get; set; }
    }

    public class CustomItemSearchWBResponseModel
{
        public List<Items> items { get; set; }        
    }


}
