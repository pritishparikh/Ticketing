using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IDesignation
    {
        List<DesignationMaster> GetDesignations(int TenantID, int hierarchyFor);
        List<DesignationMaster> GetReporteeDesignation(int DesignationID,int HierarchyFor, int TenantID);
        List<CustomSearchTicketAgent> GetReportToUser(int DesignationID, int IsStoreUser, int TenantID);
    }
}
