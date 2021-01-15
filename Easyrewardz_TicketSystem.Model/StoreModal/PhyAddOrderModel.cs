using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class PhyAddOrderModel
    {
        /// <summary>
        /// source
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// progCode
        /// </summary>
        public string progCode { get; set; }
        /// <summary>
        /// storeCode
        /// </summary>
        public string storeCode { get; set; }
        /// <summary>
        /// billNo
        /// </summary>
        public string billNo { get; set; }
        /// <summary>
        /// date
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// customerName
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// customerMobile
        /// </summary>
        public string customerMobile { get; set; }
        /// <summary>
        /// amount
        /// </summary>
        public double amount { get; set; }
        /// <summary>
        /// itemDetails
        /// </summary>
        public List<ItemDetail> itemDetails { get; set; }
        /// <summary>
        /// paymentCollected
        /// </summary>
        public string paymentCollected { get; set; }
        /// <summary>
        /// tender
        /// </summary>

        public string tender { get; set; }
        /// <summary>
        /// deleveryType
        /// </summary>
        public string deleveryType { get; set; }
        /// <summary>
        /// pickupDateTime
        /// </summary>
        public string pickupDateTime { get; set; }
        /// <summary>
        /// address
        /// </summary>
        public Address address { get; set; }
        /// <summary>
        /// tenderDetails
        /// </summary>
        public List<TenderDetail> tenderDetails { get; set; }

    }

    public class ItemDetail
    {
        /// <summary>
        /// itemID
        /// </summary>
        public string itemID { get; set; }
        /// <summary>
        /// itemName
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// itemPrice
        /// </summary>
        public string itemPrice { get; set; }
        /// <summary>
        /// quantity
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// category
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// group
        /// </summary>
        public string group { get; set; }
        /// <summary>
        /// department
        /// </summary>
        public string department { get; set; }

    }

    public class Address
    {
        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// landmark
        /// </summary>
        public string landmark { get; set; }
        /// <summary>
        /// city
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// state
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// country
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// pinCode
        /// </summary>
        public string pinCode { get; set; }
        /// <summary>
        /// latitude
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// longitude
        /// </summary>
        public string longitude { get; set; }

    }

    public class TenderDetail
    {
        /// <summary>
        /// tenderID
        /// </summary>
        public string tenderID { get; set; }
        /// <summary>
        /// tenderCode
        /// </summary>
        public string tenderCode { get; set; }
        /// <summary>
        /// tenderValue
        /// </summary>
        public string tenderValue { get; set; }

    }
}
