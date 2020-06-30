using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
  public  class CustomerCountDetail
    {
        /// <summary>
        /// Slot Id
        /// </summary>
        public int SlotId { get; set; }

        /// <summary>
        /// In Store Count
        /// </summary>
        public int InStoreCount { get; set; }

        /// <summary>
        /// Time Slot
        /// </summary>
        public string TimeSlot { get; set; }
    }
}
