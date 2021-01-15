using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStorePriority
    {
        /// <summary>
        /// Get Priority List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<Priority> GetPriorityList(int tenantID);

        /// <summary>
        /// Add Priority
        /// </summary>
        /// <param name="PriorityName"></param>
        /// <param name="status"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddPriority(string PriorityName, int status, int tenantID, int UserID);

        /// <summary>
        /// Update Priority
        /// </summary>
        /// <param name="PriorityID"></param>
        /// <param name="PriorityName"></param>
        /// <param name="status"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int UpdatePriority(int PriorityID, string PriorityName, int status, int tenantID, int UserID);

        /// <summary>
        /// Delete Priority
        /// </summary>
        /// <param name="PriorityID"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int DeletePriority(int PriorityID, int tenantID, int UserID);

        /// <summary>
        /// Priority List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<Priority> PriorityList(int tenantID);

        /// <summary>
        /// Update Priority Order
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="selectedPriorityID"></param>
        /// <param name="currentPriorityID"></param>
        /// <returns></returns>
        bool UpdatePriorityOrder(int TenantID, int selectedPriorityID, int currentPriorityID);

        /// <summary>
        /// Validate Priority
        /// </summary>
        /// <param name="priorityName"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        string ValidatePriority(string priorityName, int tenantID);
    }
}
