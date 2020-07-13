using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class CustomerChatProductModel
    {
        /// <summary>
        ///productName
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// uniqueItemCode
        /// </summary>
        public string uniqueItemCode { get; set; }

        /// <summary>
        ///productName
        /// </summary>
        public string productName { get; set; }
 

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


        /// <summary>
        /// StoreID
        /// </summary>
        public int StoreID { get; set; }



        /// <summary>
        /// StoreCode
        /// </summary>
        public string StoreCode { get; set; }


        /// <summary>
        /// IsShoppingBag
        /// </summary>
        public bool IsShoppingBag { get; set; }


        /// <summary>
        /// IsWishList
        /// </summary>
        public bool IsWishList { get; set; }



        /// <summary>
        /// IsRecommended
        /// </summary>
        public bool IsRecommended { get; set; }


    }
}
