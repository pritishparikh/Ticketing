using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class AppointmentModel
    {

        public string AppointmentDate { get; set; }
        public int SlotId { get; set; }
        public string TimeSlot { get; set; }
        public int NOofPeople { get; set; }
        public int MaxCapacity { get; set; }
        public List<AppointmentCustomer> AppointmentCustomerList { get; set; }

    }

    public class AppointmentCustomer
    {
        public int AppointmentID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public int NOofPeople { get; set; }
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
        public string MobileNo { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
    }

}
