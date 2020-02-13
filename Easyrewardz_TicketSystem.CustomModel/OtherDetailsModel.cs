using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public  class OtherDetailsModel
    {
        public int _TenantID { get; set; }
        public int _NoOfUsers { get; set; }
        public int _NoOfSimultaneous { get; set; }
        public int _MonthlyTicketVolume { get; set; }
        public int _TicketAchivePolicy { get; set; }
        public int _TenantType { get; set; }
        public int _ServerType { get; set; }
        public string _EmailSenderID { get; set; }
        public string _SMSSenderID { get; set; }
        public int _CRMInterfaceLanguage { get; set; }
        public int _ModifiedBy { get; set; }
        public int _Createdby { get; set; }
    }
}
