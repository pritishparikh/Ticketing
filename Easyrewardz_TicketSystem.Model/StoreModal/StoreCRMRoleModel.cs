using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreCRMRoleModel
    {
        public int CRMRoleID { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string isRoleActive { get; set; }

        public List<StoreModuleDetails> Modules { get; set; }
    }
    public class StoreModuleDetails
    {
        public int CRMRoleID { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public bool Modulestatus { get; set; }

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
    }
}
