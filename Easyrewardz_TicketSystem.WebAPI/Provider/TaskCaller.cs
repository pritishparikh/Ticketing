using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class TaskCaller
    {
        #region Variable
        public ITask _TaskRepository;
        #endregion
        /// <summary>
        /// Add Task
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int AddTask(ITask task, TaskMaster taskMaster, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.AddTaskDetails(taskMaster, TenantID, UserID);
        }
        public CustomTaskMasterDetails gettaskDetailsById(ITask task, int taskID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskbyId(taskID);
        }
        public List<CustomTaskMasterDetails> gettaskList(ITask task, int TicketId)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskList(TicketId);
        }
        public int DeleteTask(ITask task, int task_Id)
        {
            _TaskRepository = task;
            return _TaskRepository.DeleteTask(task_Id);
        }
        public List<CustomUserAssigned> GetAssignedTo(ITask task,int Function_ID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetAssignedTo(Function_ID);
        }
        public int AddComment(ITask task, int CommentForId,int ID, string Comment, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.AddComment(CommentForId, ID, Comment, UserID);
        }
        public List<CustomClaimMaster> GetClaimList(ITask task,int TicketId)
        {

            _TaskRepository = task;
            return _TaskRepository.GetClaimList(TicketId);
        }

        public List<UserComment> GetTaskComment(ITask task, int TaskId)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskComment(TaskId);
        }
        public int AddCommentOnTask(ITask task, TaskMaster taskMaster)
        {
            _TaskRepository = task;
            return _TaskRepository.AddCommentOnTask(taskMaster);
        }

    }
}
