using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Task
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Add Task Details
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddTaskDetails(TaskMaster taskMaster,int TenantID,int UserID);

        /// <summary>
        /// Get Task by Id
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        CustomTaskMasterDetails GetTaskbyId(int taskID);

        /// <summary>
        /// Get Task List
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        List<CustomTaskMasterDetails> GetTaskList(int TicketId);

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="task_Id"></param>
        /// <returns></returns>
        int DeleteTask(int task_Id);

        /// <summary>
        /// Get Assigned To
        /// </summary>
        /// <param name="Function_ID"></param>
        /// <returns></returns>
        List<CustomUserAssigned> GetAssignedTo(int Function_ID);

        /// <summary>
        /// Add Comment
        /// </summary>
        /// <param name="CommentForId"></param>
        /// <param name="ID"></param>
        /// <param name="Comment"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddComment(int CommentForId, int ID, string Comment ,int UserID);

        /// <summary>
        /// Get Claim List
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        List<CustomClaimMaster> GetClaimList(int TicketId);

        /// <summary>
        /// Get list of the claim comments
        /// </summary>
        /// <param name="ClaimId">Id of the Claim</param>
        /// <returns></returns>
        List<UserComment> GetTaskComment(int TaskId);

        /// <summary>
        /// Add Comment On Task
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <returns></returns>
        int AddCommentOnTask(TaskMaster taskMaster);
    }
}
