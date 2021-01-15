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
        /// <summary>
        /// Appointment ID
        /// </summary>
        public int AID { get; set; }

        /// <summary>
        /// Time Slot Id
        /// </summary>
        public int TimeSlotId { get; set; }

        /// <summary>
        /// Appointment Date
        /// </summary>
        public string AppointmentDate { get; set; }

        /// <summary>
        /// Visited Count
        /// </summary>
        public int VisitedCount { get; set; }

        /// <summary>
        /// Max Capacity
        /// </summary>
        public int MaxCapacity { get; set; }

        /// <summary>
        /// Remaining
        /// </summary>
        public int Remaining { get; set; }

        /// <summary>
        /// Store Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Time Slot
        /// </summary>
        public string TimeSlot { get; set; }

        /// <summary>
        /// Is Disabled
        /// </summary>
        public bool IsDisabled { get; set; }

    }

    public class DateofSchedule
    {

        /// <summary>
        /// Day
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Day
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Dates
        /// </summary>
        public string Dates { get; set; }


        /// <summary>
        /// Max People Allowed in One Appointment
        /// </summary>
        public int  MaxPeopleAllowed { get; set; }

        /// <summary>
        /// Already Schedule Details List
        /// </summary>
        // public List<TimeSlotModel> TimeSlotModels { get; set; }
        public List<AlreadyScheduleDetail> AlreadyScheduleDetails { get; set; }

    }
}
