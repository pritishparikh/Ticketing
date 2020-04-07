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
        /// <summary>
        /// Add Task
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
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
    }
}
