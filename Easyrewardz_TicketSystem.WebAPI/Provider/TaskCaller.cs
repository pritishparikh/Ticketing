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
        public int AddTask(ITask task, TaskMaster taskMaster)
        {
            _TaskRepository = task;
            return _TaskRepository.AddTaskDetails(taskMaster);
        }
        public CustomTaskMasterDetails gettaskDetailsById(ITask task, int taskID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskbyId(taskID);
        }
        public List<CustomTaskMasterDetails> gettaskList(ITask task)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskList();
        }
        public int DeleteTask(ITask task, int task_Id)
        {
            _TaskRepository = task;
            return _TaskRepository.DeleteTask(task_Id);
        }
    }
}
