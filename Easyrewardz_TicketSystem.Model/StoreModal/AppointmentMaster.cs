using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class AppointmentMaster
    {
        /// <summary>
        /// AppointmentID
        /// </summary>
        public int AppointmentID { get; set; }

        /// <summary>
        /// TenantID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Store ID 
        /// </summary>
        public string StoreID { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// AppointmentDate
        /// </summary>
        public DateTime AppointmentDate { get; set; }
        
        /// <summary>
        /// TimeSlot
        /// </summary>
        public int SlotID { get; set; }
        
        /// <summary>
        /// MobileNo
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// NOofPeople 
        /// </summary>
        public int NOofPeople { get; set; }

        /// <summary>
        /// CreatedBy 
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// ModifyBy  
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// CreatedDate  
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// ModifyDate  
        /// </summary>
        public string ModifyDate { get; set; }
    }
}
