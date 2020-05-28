using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomCustomerInStore
    {
        public int AppointmentID { get; set; }
        public int MaxCapacity { get; set; }
        public int VisitedCount { get; set; }
        public string TimeSlot  { get; set; }
        public string StoreCode { get; set; }
        public int SlotID { get; set; }
        public int StoreID { get; set; }
        public int RemainingCount { get; set; }
        public int CustomerInStorePercentage { get; set; }
    }
}
