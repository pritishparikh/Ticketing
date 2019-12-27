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

        #endregion



    }
}
