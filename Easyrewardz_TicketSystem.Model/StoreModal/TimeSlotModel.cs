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
        public int StoreID { get; set; }

        /// <summary>
        /// Time Slot
        /// </summary>
        public string TimeSlot { get; set; }

        /// <summary>
        /// Max Capacity
        /// </summary>
        public int MaxCapacity { get; set; }
        public List<AlreadyScheduleDetail> AlreadyScheduleDetails { get; set; }
    }
    public class AlreadyScheduleDetail
    {

        public int TimeSlotId { get; set; }
        public string AppointmentDate { get; set; }
        public int VisitedCount { get; set; }
        public int MaxCapacity { get; set; }
        public int Remaining { get; set; }
        public int StoreId { get; set; }
        public string TimeSlot { get; set; }


    }

    public class DateofSchedule
    {

        /// <summary>
        /// Day
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Dates
        /// </summary>
        public string Dates { get; set; }

        public List<TimeSlotModel> TimeSlotModels { get; set; }

    }
}
