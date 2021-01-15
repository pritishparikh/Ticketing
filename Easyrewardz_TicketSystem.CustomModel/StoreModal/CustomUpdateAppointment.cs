using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomUpdateAppointment
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
        /// NO of People
        /// </summary>
        public int NOofPeople { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Slot Id
        /// </summary>
        public int SlotId { get; set; }

        /// <summary>
        /// Slot date
        /// </summary>
        public string Slotdate { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Check in flag
        /// </summary>
        public int Checkinflag { get; set; }
        
    }
}
