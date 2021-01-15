using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreCRMRoleModel
    {


        /// <summary>
        /// TenantID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// ProgramCode
        /// </summary>
        public string  ProgramCode { get; set; }

        /// <summary>
        /// Store Code
        /// </summary>
        public string  StoreCode { get; set; }

        /// <summary>
        ///User ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///Agent Name
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// CRM Role ID
        /// </summary>
        public int CRMRoleID { get; set; }

        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Modified Date
        /// </summary>
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Is Role Active
        /// </summary>
        public string isRoleActive { get; set; }

        /// <summary>
        /// Modules
        /// </summary>
        public List<StoreModuleDetails> Modules { get; set; }
    }
    public class StoreModuleDetails
    {
        /// <summary>
        /// CRM Role ID
        /// </summary>
        public int CRMRoleID { get; set; }

        /// <summary>
        /// Module ID
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// Module Name
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Module status
        /// </summary>
        public bool Modulestatus { get; set; }

        /// <summary>
        /// Module Priority
        /// </summary>
        public int ModulePriority { get; set; }

    }

    public class CrmModule
    {
        /// <summary>
        /// ModuleID
        /// </summary>
        public int ModuleID { get; set; }
        /// <summary>
        /// ModuleName
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
    }
}
