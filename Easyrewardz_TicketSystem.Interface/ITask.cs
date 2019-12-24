using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ITask
    {
        int AddTaskDetails(TaskMaster taskMaster);
        TaskMaster GetTaskbyId(int taskID);
        List<TaskMaster> GetTaskList();
    }
}
