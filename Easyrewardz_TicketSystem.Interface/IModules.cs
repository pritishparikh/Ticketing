using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IModules
    {
        int UpdateModules(int tenantID,int ModuleID, string ModulesActive, string ModuleInactive, int ModifiedBy);

        List<ModulesModel> GetModulesList(int tenantID);

        List<ModuleItems> GetModulesItemList(int tenantID, int ModuleID);

    }
}
    