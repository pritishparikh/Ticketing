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
        int AddTaskDetails(TaskMaster taskMaster);
        CustomTaskMasterDetails GetTaskbyId(int taskID);
        List<CustomTaskMasterDetails> GetTaskList();
        int DeleteTask(int task_Id);
    }
}
