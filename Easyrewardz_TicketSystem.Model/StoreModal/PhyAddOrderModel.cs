using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class PhyAddOrderModel
    {
        public string source { get; set; }
        public string progCode { get; set; }
        public string storeCode { get; set; }
        public string billNo { get; set; }
        public string date { get; set; }
        public string customerName { get; set; }
        public string customerMobile { get; set; }
        public double amount { get; set; }
        public List<ItemDetail> itemDetails { get; set; }
        public string paymentCollected { get; set; }
        public string tender { get; set; }
        public string deleveryType { get; set; }
        public string pickupDateTime { get; set; }
        public Address address { get; set; }
        public List<TenderDetail> tenderDetails { get; set; }

    }

    public class ItemDetail
    {
        public string itemID { get; set; }
        public string itemName { get; set; }
        public string itemPrice { get; set; }
        public string quantity { get; set; }
        public string category { get; set; }
        public string group { get; set; }
        public string department { get; set; }

    }

    public class Address
    {
        public string address { get; set; }
        public string landmark { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pinCode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

    }

    public class TenderDetail
    {
        public string tenderID { get; set; }
        public string tenderCode { get; set; }
        public string tenderValue { get; set; }

    }
}
