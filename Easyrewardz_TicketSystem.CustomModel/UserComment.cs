using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class UserComment
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string datetime { get; set; }
        public int OldAgentID { get; set; }
        public string OldAgentName { get; set; }
        public int NewAgentID { get; set; }
        public string NewAgentName { get; set; }
        public bool IsTicketingComment { get; set; }
    }
}
