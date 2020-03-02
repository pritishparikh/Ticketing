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
    
        int AddTaskDetails(TaskMaster taskMaster,int TenantID,int UserID);

        CustomTaskMasterDetails GetTaskbyId(int taskID);

        List<CustomTaskMasterDetails> GetTaskList(int TicketId);

        int DeleteTask(int task_Id);

        List<CustomUserAssigned> GetAssignedTo(int Function_ID);

        int AddComment(int CommentForId, int ID, string Comment ,int UserID);

        List<CustomClaimMaster> GetClaimList(int TicketId);

        /// <summary>
        /// Get list of the claim comments
        /// </summary>
        /// <param name="ClaimId">Id of the Claim</param>
        /// <returns></returns>
        List<UserComment> GetTaskComment(int TaskId);

        int AddCommentOnTask(TaskMaster taskMaster);
    }
}
