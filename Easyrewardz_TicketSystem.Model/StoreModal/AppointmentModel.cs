using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class AppointmentModel
    {
        public DateTime ApointmentDate { get; set; }
        public DateTime TimeSlot { get; set; }
        public int NoOfPeople { get; set; }
        public List<AppointmentCustomer> AppointmentCustomerList { get; set; }
    }

    public class AppointmentCustomer
    {
        
        public string CustomerName { get; set; }
       
        public string MobileNo { get; set; }
        public int NoOfPeople { get; set; }
        public int? Status { get; set; }
    }

}
