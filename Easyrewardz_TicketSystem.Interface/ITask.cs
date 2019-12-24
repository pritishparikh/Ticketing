using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ITask
    {
        int AddTaskDetails(TaskMaster taskMaster);
        //IEnumerable<TaskMaster> GetTaskMasters();
    }
}
