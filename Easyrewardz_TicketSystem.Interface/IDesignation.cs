using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IDesignation
    {
        /// <summary>
        /// Get Designations
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<DesignationMaster> GetDesignations(int TenantID);

        /// <summary>
        /// Get Reportee Designation
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="HierarchyFor"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<DesignationMaster> GetReporteeDesignation(int DesignationID,int HierarchyFor, int TenantID);

        /// <summary>
        /// Get Report To User
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="IsStoreUser"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<CustomSearchTicketAgent> GetReportToUser(int DesignationID, int IsStoreUser, int TenantID);
    }
}
