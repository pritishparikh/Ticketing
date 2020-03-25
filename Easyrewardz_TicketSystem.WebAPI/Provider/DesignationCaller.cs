using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class DesignationCaller
    {

        #region Variable
        public IDesignation _designationRepository;
        #endregion

        #region Customer wrapper method

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public List<DesignationMaster> GetDesignations(IDesignation designation, int TenantId)
        {
            _designationRepository = designation;
            return _designationRepository.GetDesignations(TenantId);
        }

        public List<DesignationMaster> GetReporteeDesignation(IDesignation designation, int DesignationID, int HierarchyFor, int TenantID)
        {
            _designationRepository = designation;
            return _designationRepository.GetReporteeDesignation(DesignationID, HierarchyFor, TenantID);
        }

        public List<CustomSearchTicketAgent> GetReportToUser(IDesignation designation, int DesignationID, int IsStoreUser, int TenantID)
        {
            _designationRepository = designation;
            return _designationRepository.GetReportToUser(DesignationID, IsStoreUser, TenantID);
        }
        #endregion



    }
}
