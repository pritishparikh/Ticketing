using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Priority
    /// </summary>
   public interface IPriority
    {
        List<Priority> GetPriorityList(int CategoryID);
    }
}
