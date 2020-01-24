using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class ModulesModel
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public bool ModuleisActive { get; set; }

        //public string CreatedDate { get; set; }
        //public string ModifiedBy { get; set; }
        //public string ModifiedDate { get; set; }


        //List<ModuleItems> ModuleItem { get; set; }

    }

    public class ModuleItems
    {
        public int ModuleID { get; set; }
        public int ModuleItemID { get; set; }
        public string ModuleItemName { get; set; }
        public bool ModuleItemisActive { get; set; }

    }


}
