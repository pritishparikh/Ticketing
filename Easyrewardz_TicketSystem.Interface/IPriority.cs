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
        List<Priority> GetPriorityList(int tenantID,int PriorityFor);
        int AddPriority(string PriorityName, int status, int tenantID, int UserID,int PriorityFor);
        int UpdatePriority(int PriorityID, string PriorityName, int status, int tenantID, int UserID,int PriorityFor);
        int DeletePriority(int PriorityID,int tenantID, int UserID,int PriorityFor);
        List<Priority> PriorityList(int tenantID, int PriorityFor);
    }
}
