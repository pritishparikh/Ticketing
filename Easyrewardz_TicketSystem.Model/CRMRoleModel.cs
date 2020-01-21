using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class CRMRoleModel
    {
        public int CRMRoleID { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public string  CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public bool isRoleActive { get; set; }
      
        List<ModuleDetails> Modules { get; set; }
    }

    public class ModuleDetails
     {
        public string ModuleName { get; set; }
        public bool isEnabled { get; set; }

    }
}
