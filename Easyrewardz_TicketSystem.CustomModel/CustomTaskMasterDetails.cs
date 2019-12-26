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
        public int? CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Assign Name
        /// </summary>
        public string AssignName { get; set; }
    }
}
