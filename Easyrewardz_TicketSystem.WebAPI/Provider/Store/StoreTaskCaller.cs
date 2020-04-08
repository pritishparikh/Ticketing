using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;


namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreTaskCaller
    {
        #region Variable
        public IStoreTask _TaskRepository;
        #endregion
        
        public int AddTask(IStoreTask task, TaskMaster taskMaster, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.AddTaskDetails(taskMaster, TenantID, UserID);
        }
        public List<CustomStoreTaskDetails> gettaskList(IStoreTask task, int tabFor,int tenantID,int userID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskList(tabFor, tenantID, userID);
        }
        public StoreTaskMaster GetStoreTaskByID(IStoreTask task, int TaskID, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetStoreTaskByID(TaskID, TenantID, UserID);
        }
        public int AddStoreTaskComment(IStoreTask task, StoreTaskComment TaskComment, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.AddStoreTaskComment(TaskComment, TenantID, UserID);
        }
        public List<TaskCommentModel> GetCommentOnTask(IStoreTask task, int TaskID, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetCommentOnTask(TaskID, TenantID, UserID);
        }
        public List<CustomTaskHistory> GetTaskHistory(IStoreTask task, int TaskID, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskHistory(TaskID, TenantID, UserID);
        }
    }
}
