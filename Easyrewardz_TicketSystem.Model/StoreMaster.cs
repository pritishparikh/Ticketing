using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreMaster
    {
        public int StoreID { get; set; }
        public int TenantID { get; set; }
        public int BrandID { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int PincodeID { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string StoreCode { get; set; }
        public int RegionID { get; set; }
        public int ZoneID { get; set; }
        public int StoreTypeID { get; set; }
        public string StoreEmailID { get; set; }
        public string StorePhoneNo { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string Pincode { get; set; }
    }
}
