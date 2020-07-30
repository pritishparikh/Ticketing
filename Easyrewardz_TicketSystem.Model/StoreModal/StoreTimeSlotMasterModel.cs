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
        /// Operational Days Count
        /// </summary>
        public int OperationalDaysCount { get; set; }

        /// <summary>
        /// Operational Days
        /// </summary>
        public string OperationalDays { get; set; }

        /// <summary>
        /// Slot Template ID
        /// </summary>
        public int SlotTemplateID { get; set; }

        /// <summary>
        /// Slot Template Name
        /// </summary>
        public string SlotTemplateName { get; set; }

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

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Template Slots
        /// </summary>
        public List<TemplateBasedSlots> TemplateSlots { get; set; }
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
        /// Store Operational Days
        /// </summary>
        public string StoreOpdays{ get; set; }

        /// <summary>
        /// Slot Template ID
        /// </summary>
        public int SlotTemplateID { get; set; }


        /// <summary>
        /// Slot Max Capacity
        /// </summary>
        public int SlotMaxCapacity { get; set; }


        /// <summary>
        /// Appointment Days
        /// </summary>
        public int AppointmentDays { get; set; }


        /// <summary>
        /// Applicable From Date
        /// </summary>
        public string ApplicableFromDate { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }


        /// <summary>
        /// User ID
        /// </summary>
        public int SlotDisplayCode { get; set; } // refer enum DisplaySlotsFrom : 301-Current Slot , 302-Skip Current Slot & Show Next Slot, 303-Skip Current & Next Slot

        /// <summary>
        ///Template Slots List
        /// </summary>
        public List<TemplateBasedSlots> TemplateSlots { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }


    }

}
