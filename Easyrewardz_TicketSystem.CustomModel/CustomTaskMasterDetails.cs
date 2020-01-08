using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomTaskMasterDetails
    {
        /// <summary>
        /// Ticketing TaskID
        /// </summary>
        public int TicketingTaskID { get; set; }
        /// <summary>
        /// TaskStatus
        /// </summary>
        public string TaskStatus { get; set; }
        /// <summary>
        /// Task Title
        /// </summary>
        public string TaskTitle { get; set; }
        /// <summary>
        /// Task Description
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// StoreCode
        /// </summary>
        public int? StoreCode { get; set; }
        /// <summary>
        ///Created By
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Assign Name
        /// </summary>
        public string AssignName { get; set; }
        /// <summary>
        ///Duedate
        /// </summary>
        public DateTime Duedate { get; set; }
        /// <summary>
        /// DueDateFormat by SHLOK (Format :- 12 March 2019)
        /// </summary>
        public string DateFormat { get; set; }

       public List<UserComment> Comments { get; set; }
    }

}
