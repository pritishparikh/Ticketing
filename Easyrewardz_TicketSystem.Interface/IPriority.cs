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
        /// <summary>
        /// Get Priority List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        List<Priority> GetPriorityList(int tenantID,int PriorityFor);

        /// <summary>
        /// Add Priority
        /// </summary>
        /// <param name="PriorityName"></param>
        /// <param name="status"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        int AddPriority(string PriorityName, int status, int tenantID, int UserID,int PriorityFor);

        /// <summary>
        /// Update Priority
        /// </summary>
        /// <param name="PriorityID"></param>
        /// <param name="PriorityName"></param>
        /// <param name="status"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        int UpdatePriority(int PriorityID, string PriorityName, int status, int tenantID, int UserID,int PriorityFor);

        /// <summary>
        /// Delete Priority
        /// </summary>
        /// <param name="PriorityID"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        int DeletePriority(int PriorityID,int tenantID, int UserID,int PriorityFor);

        /// <summary>
        /// Priority List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        List<Priority> PriorityList(int tenantID, int PriorityFor);

        /// <summary>
        /// Update Priority Order
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="selectedPriorityID"></param>
        /// <param name="currentPriorityID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        bool UpdatePriorityOrder(int TenantID, int selectedPriorityID, int currentPriorityID, int PriorityFor);

        /// <summary>
        /// Validate Priority
        /// </summary>
        /// <param name="priorityName"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        string ValidatePriority(string priorityName, int TenantID);
    }
}
