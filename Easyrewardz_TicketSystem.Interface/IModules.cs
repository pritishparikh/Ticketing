using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IModules
    {
        /// <summary>
        /// Update Modules
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="ModuleID"></param>
        /// <param name="ModulesActive"></param>
        /// <param name="ModuleInactive"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        int UpdateModules(int tenantID,int ModuleID, string ModulesActive, string ModuleInactive, int ModifiedBy);

        /// <summary>
        /// Get Modules List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<ModulesModel> GetModulesList(int tenantID);

        /// <summary>
        /// Get Modules Item List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        List<ModuleItems> GetModulesItemList(int tenantID, int ModuleID);

    }
}
    