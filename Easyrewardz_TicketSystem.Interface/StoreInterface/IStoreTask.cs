using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IStoreTask
    {
        int AddTaskDetails(TaskMaster taskMaster, int TenantID, int UserID);
        List<CustomStoreTaskDetails> GetTaskList(int tabFor, int tenantID, int userID);
    }
}
