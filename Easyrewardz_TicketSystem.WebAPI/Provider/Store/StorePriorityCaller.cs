using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StorePriorityCaller
    {
        /// <summary>
        /// Priority
        /// </summary>
        private IStorePriority _priorityList;
        #region Methods for the Priority

        public List<Priority> GetPriorityList(IStorePriority _priority, int TenantID)
        {
            _priorityList = _priority;
            return _priorityList.GetPriorityList(TenantID);
        }
        public int Addpriority(IStorePriority _priority, string PriorityName, int status, int tenantID, int UserID)
        {
            _priorityList = _priority;
            return _priorityList.AddPriority(PriorityName, status, tenantID, UserID);
        }
        public int Updatepriority(IStorePriority _priority, int PriorityID, string PriorityName, int status, int tenantID, int UserID)
        {
            _priorityList = _priority;
            return _priorityList.UpdatePriority(PriorityID, PriorityName, status, tenantID, UserID);
        }
        public int Deletepriority(IStorePriority _priority, int PriorityID, int tenantID, int UserID)
        {
            _priorityList = _priority;
            return _priorityList.DeletePriority(PriorityID, tenantID, UserID);
        }
        public List<Priority> PriorityList(IStorePriority _priority, int TenantID)
        {
            _priorityList = _priority;
            return _priorityList.PriorityList(TenantID);
        }
        public bool UpdatePriorityOrder(IStorePriority _priority, int TenantID, int selectedPriorityID, int currentPriorityID)
        {
            _priorityList = _priority;
            return _priorityList.UpdatePriorityOrder(TenantID, selectedPriorityID, currentPriorityID);
        }
        public string VallidatePriority(IStorePriority _priority, string priorityName, int tenantID)
        {
            _priorityList = _priority;
            return _priorityList.ValidatePriority(priorityName, tenantID);

        }
        #endregion
    }
}
