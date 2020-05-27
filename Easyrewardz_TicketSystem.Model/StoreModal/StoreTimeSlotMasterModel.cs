using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class StoreTimeSlotMasterModel
    {
        public int SlotId { get; set; }
   
        public int TenantId { get; set; }
        public string ProgramCode { get; set; }
        public int StoreId { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string TimeSlot { get; set; }
        public int OrderNumber { get; set; }
        public int MaxCapacity { get; set; }
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
        public int StoreId { get; set; }
        public string TimeSlot { get; set; }
        public int OrderNumber { get; set; }
        public int MaxCapacity { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }

    }
}
