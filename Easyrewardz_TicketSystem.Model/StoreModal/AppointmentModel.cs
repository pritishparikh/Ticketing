using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class AppointmentModel
    {
        public DateTime AppointmentDate { get; set; }
        public DateTime TimeSlot { get; set; }
        public int NOofPeople { get; set; }
        public List<AppointmentCustomer> AppointmentCustomerList { get; set; }
        
    }

    public class AppointmentCustomer
    {
        
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public int NOofPeople { get; set; }
        public int Status { get; set; }
       
    }

    //public class AppointmentCountModel
    //{
    //    public DateTime AppointmentDate { get; set; }
    //    public List<AppointmentCount> AppointmentCountList { get; set; }
    //}

    public class AppointmentCount
    {
        
        public int Today { get; set; }
        public int Tomorrow { get; set; }
        public int DayAfterTomorrow { get; set; }
    }

}
