using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomStoreList
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string BranName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public int PinCode { get; set; }
        public string Status { get; set; }
        public string strPinCode { get; set; }

        public int CityID { get; set; }
        public int StateID { get; set; }
        public int RegionID { get; set; }
        public int ZoneID { get; set; }
        public int StoreTypeID { get; set; }
        public bool StatusID { get; set; }
        public string BrandIDs { get; set; }
        public string BrandNames{ get; set; }
        public string Brand_Names { get; set; } // Brand Names which show on hover
    }
}
