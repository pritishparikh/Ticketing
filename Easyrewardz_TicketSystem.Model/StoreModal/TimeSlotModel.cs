using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class TimeSlotModel
    {

        /// <summary>
        /// SlotID
        /// </summary>
        public int SlotID { get; set; }
        /// <summary>
        /// StoreID
        /// </summary>
        public int StoreID  { get; set; }

        /// <summary>
        /// Time Slot
        /// </summary>
        public string TimeSlot  { get; set; }

        /// <summary>
        /// Max Capacity
        /// </summary>
        public int MaxCapacity { get; set; }

    }
}
