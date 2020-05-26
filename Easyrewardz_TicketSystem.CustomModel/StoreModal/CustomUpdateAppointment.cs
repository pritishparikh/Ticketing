using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomUpdateAppointment
    {
        public int AppointmentID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public int NOofPeople { get; set; }
        public int Status { get; set; }
        public int TenantID { get; set; }
        public string ProgramCode { get; set; }
        public int SlotId { get; set; }
        public string Slotdate { get; set; }
        public int UserID { get; set; }
        public int Checkinflag { get; set; }
        
    }
}
