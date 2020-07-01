using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class StoreTimeSlotSettingModel
    {
        /// <summary>
        /// Slot Setting ID
        /// </summary>
        public int SlotSettingID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Store Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Store Code
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// Store Timimg
        /// </summary>
        public string StoreTimimg { get; set; }

        /// <summary>
        /// Non Operational Timimg
        /// </summary>
        public string NonOperationalTimimg { get; set; }

        /// <summary>
        /// Store Slot Duration
        /// </summary>
        public string StoreSlotDuration { get; set; }

        /// <summary>
        /// Max Capacity
        /// </summary>
        public int MaxCapacity { get; set; }

        /// <summary>
        /// Total Slot
        /// </summary>
        public int TotalSlot { get; set; }

        /// <summary>
        /// Appointment Days
        /// </summary>
        public int AppointmentDays { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Modify By
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// Modify By Name
        /// </summary>
        public string ModifyByName { get; set; }

        /// <summary>
        /// Modify Date
        /// </summary>
        public string ModifyDate { get; set; } 
       
        
    }


    public class StoreTimeSlotInsertUpdate
    {
        /// <summary>
        /// Slot Id
        /// </summary>
        public int SlotId { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Store Ids
        /// </summary>
        public string StoreIds { get; set; }

        /// <summary>
        /// Store Open Value
        /// </summary>
        public int StoreOpenValue { get; set; }

        /// <summary>
        /// Store Open At
        /// </summary>
        public string StoreOpenAt { get; set; }

        /// <summary>
        /// Store Close Value
        /// </summary>
        public int StoreCloseValue { get; set; }

        /// <summary>
        /// Store Close At
        /// </summary>
        public string StoreCloseAt { get; set; }

        /// <summary>
        /// Slot duration
        /// </summary>
        public float Slotduration { get; set; }

        /// <summary>
        /// Slot Max Capacity
        /// </summary>
        public int SlotMaxCapacity { get; set; }

        /// <summary>
        /// Store NonOp From Value
        /// </summary>
        public int StoreNonOpFromValue { get; set; }

        /// <summary>
        /// Store NonOp From At
        /// </summary>
        public string StoreNonOpFromAt { get; set; }

        /// <summary>
        /// Store NonOp From At
        /// </summary>
        public int StoreNonOpToValue { get; set; }

        /// <summary>
        /// Store NonOp To At
        /// </summary>
        public string StoreNonOpToAt { get; set; }

        /// <summary>
        /// Store Total Slot
        /// </summary>
        public int StoreTotalSlot { get; set; }

        /// <summary>
        /// Appointment Days
        /// </summary>
        public int AppointmentDays { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }


    }
}
