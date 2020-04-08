using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Task Master 
    /// </summary>
    public class StoreTaskMaster
    {
        /// <summary>
        /// Ticketing TaskID
        /// </summary>
        public int TaskID { get; set; }
        /// <summary>
        /// Ticket ID
        /// </summary>
        public int TicketID { get; set; }
        /// <summary>
        /// Task Title
        /// </summary>
        public string TaskTitle { get; set; }
        /// <summary>
        /// Task Description
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// Department Id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// Assign To ID
        /// </summary>
        public int AssignToID { get; set; }
        /// <summary>
        /// Assign To Name
        /// </summary>
        public string AssignToName { get; set; }
        /// <summary>
        ///Priority ID
        /// </summary>
        public int PriorityID { get; set; }
        /// <summary>
        /// Function ID
        /// </summary>
        public int FunctionID { get; set; }
        /// <summary>
        /// Task End Time
        /// </summary>
        public DateTime TaskEndTime { get; set; }
        /// <summary>
        /// Task Status Id
        /// </summary>
        public int TaskStatusId { get; set; }
        /// <summary>
        ///Created By
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }
        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        ///Modified Date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        ///Task Comments
        /// </summary>
        public string TaskComments { get; set; }
        /// <summary>
        ///Task Comments
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        ///Task Comments
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        ///Task Comments
        /// </summary>
        public string StoreCode { get; set; }
    }


    public class StoreTaskComment
    {
        public int TaskID { get; set; }
        public string Comment { get; set; }
    }

    public class TaskCommentModel
    {
        public int TaskCommentID { get; set; }
        public int TaskID { get; set; }
        public string Comment { get; set; }
        public int CommentBy { get; set; }
        public string CommentedDate { get; set; }
        public string CommentByName { get; set; }
        public string CommentedDiff { get; set; }
    }

    public class CustomTaskHistory
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string DateandTime { get; set; }
    }

}
