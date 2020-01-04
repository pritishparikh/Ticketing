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
        List<SLAStatus> GetSLAList(int TenantID);
    }
}
