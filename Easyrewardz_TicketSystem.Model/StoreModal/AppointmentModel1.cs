using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class AppointmentModel
    {
        /// <summary>
        /// Appointment Date
        /// </summary>
        public string AppointmentDate { get; set; }

        /// <summary>
        /// Slot Id
        /// </summary>
        public int SlotId { get; set; }

        /// <summary>
        /// Time Slot
        /// </summary>
        public string TimeSlot { get; set; }

        /// <summary>
        /// NO Of People
        /// </summary>
        public int NOofPeople { get; set; }

        /// <summary>
        /// Max Capacity
        /// </summary>
        public int MaxCapacity { get; set; }

        /// <summary>
        /// Appointment Customer List
        /// </summary>
        public List<AppointmentCustomer> AppointmentCustomerList { get; set; }

    }

    public class AppointmentCustomer
    {
        /// <summary>
        /// Appointment ID
        /// </summary>
        public int AppointmentID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Number
        /// </summary>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// NO Of People
        /// </summary>
        public int NOofPeople { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
       
    }

    

    public class AppointmentCount
    {
        
        public int Today { get; set; }
        public int Tomorrow { get; set; }
        public int DayAfterTomorrow { get; set; }
    }

    public class AppointmentDetails
    {

        public int AppointmentID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobileNo { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string NoOfPeople { get; set; }
        public string StoreManagerMobile { get; set; }
    }

}
