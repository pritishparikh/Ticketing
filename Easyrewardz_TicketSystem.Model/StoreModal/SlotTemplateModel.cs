using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
   public class SlotTemplateModel
    {
        
        public int SlotTemplateID { get; set; }

        public string SlotTemplateName { get; set; }
    }

    public class StoreOperationalDays
    {

        public int DayID { get; set; }

        public string DayName { get; set; }
    }

    public class TemplateBasedSlots
    {

        public int SlotID { get; set; }

        public int SlotTemplateID { get; set; }

        public string SlotStartTime { get; set; }

        public string SlotEndTime { get; set; }

        public int SlotOccupancy { get; set; }

        public bool IsSlotEnabled { get; set; }
    }

    public class CreateStoreSlotTemplate
    {


        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }


        /// <summary>
        /// Slot Template Name
        /// </summary>
        public string SlotTemplateName { get; set; }


        /// <summary>
        /// Slot Template Type : Automatic,Manual
        /// </summary>
        public string SlotTemplateType { get; set; }

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
        /// Slot gaps
        /// </summary>
        public float SlotGaps { get; set; }



        /// <summary>
        /// Store NonOp From Value
        /// </summary>
        public int StoreNonOpFromValue { get; set; }

        /// <summary>
        /// Store NonOp From At
        /// </summary>
        public string StoreNonOpFromAt { get; set; }

        /// <summary>
        /// Store NonOperational From At
        /// </summary>
        public int StoreNonOpToValue { get; set; }

        /// <summary>
        /// Store NonOperational To At
        /// </summary>
        public string StoreNonOpToAt { get; set; }

        public List<TemplateBasedSlots> TemplateSlots { get; set; }


        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }

    }

}
