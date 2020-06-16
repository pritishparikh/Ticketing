using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class StoreTimeSlotSettingModel
    {
        public int SlotSettingID { get; set; }
   
        public int TenantId { get; set; }
        public string ProgramCode { get; set; }
        public int StoreId { get; set; }
        public string StoreCode { get; set; }
        public string StoreTimimg { get; set; }
        public string NonOperationalTimimg { get; set; }
        public string StoreSlotDuration { get; set; }
        public int MaxCapacity { get; set; }
        public int TotalSlot { get; set; }
        public int AppointmentDays { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyByName { get; set; }
        public string ModifyDate { get; set; } 
       
        
    }


    public class StoreTimeSlotInsertUpdate
    {
        public int SlotId { get; set; }
        public int TenantId { get; set; }
        public string ProgramCode { get; set; }
        public string StoreIds { get; set; }
        public int StoreOpenValue { get; set; }
        public string StoreOpenAt { get; set; }
        public int StoreCloseValue { get; set; }
        public string StoreCloseAt { get; set; }
        public float Slotduration { get; set; }

        public int SlotMaxCapacity { get; set; }
        public int StoreNonOpFromValue { get; set; }
        public string StoreNonOpFromAt { get; set; }
        public int StoreNonOpToValue { get; set; }
        public string StoreNonOpToAt { get; set; }
        public int StoreTotalSlot { get; set; }
        public int AppointmentDays { get; set; }
        public int UserID { get; set; }


    }
}
