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
        public string isRoleActive { get; set; }
      
        public List<ModuleDetails> Modules { get; set; }
    }

    public class ModuleDetails
     {
        public int CRMRoleID { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string Modulestatus { get; set; }

    }
}
