using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomStoreTaskDetails
    {
        /// <summary>
        /// Ticketing TaskID
        /// </summary>
        public int StoreTaskID { get; set; }
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
        public string Assignto { get; set; }
        /// <summary>
        /// Assign Id
        /// </summary>
        public string AssigntoId { get; set; }
        /// <summary>
        ///Duedate
        /// </summary>
        public DateTime Duedate { get; set; }
        /// <summary>
        ///Priority
        /// </summary>
        public string PriorityName { get; set; }
        /// <summary>
        /// StoreName
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// StoreAddress
        /// </summary>
        public string StoreAddress { get; set; }
        /// <summary>
        /// CreationOn
        /// </summary>
        public string CreationOn { get; set; }
        /// <summary>
        /// FunctionName
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// Created ago
        /// </summary>
        public string Createdago { get; set; }

        /// <summary>
        /// Assigned ago
        /// </summary>
        public string Assignedago { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Updated ago
        /// </summary>
        public string Updatedago { get; set; }

        /// <summary>
        /// Task Cloure Date
        /// </summary>
        public string TaskCloureDate { get; set; }

        /// <summary>
        /// Resolution Time Remaining
        /// </summary>
        public string ResolutionTimeRemaining { get; set; }

        /// <summary>
        /// Resolution Over due By
        /// </summary>
        public string ResolutionOverdueBy { get; set; }

        /// <summary>
        /// Color Name
        /// </summary>
        public string ColorName { get; set; }

        /// <summary>
        /// Color Code
        /// </summary>
        public string ColorCode { get; set; }

        /// <summary>
        /// Ticket ID
        /// </summary>
        public int TicketID { get; set; }

        /// <summary>
        /// Comments list
        /// </summary>
        public List<UserComment> Comments { get; set; }
    }
    
}
