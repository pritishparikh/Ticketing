using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Brand
    /// </summary>
    public interface ISLA
    {
        List<SLAStatus> GetSLAStatusList(int TenantID);

        int InsertSLA(SLAModel SLA);

        int UpdateSLA(int tenantID,int SLAID,  int IssuetypeID, bool isActive, int modifiedBy);

        int DeleteSLA(int tenantID, int SLAID);

        List<SLAResponseModel> SLAList(int tenantID);

    }
}
