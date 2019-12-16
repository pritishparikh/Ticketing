using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class UserDetails
    {
        public int IndexID { get; set; }
        public string UserName { get; set; }
        public string SecurityToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean Inactive { get; set; }
        public string SessionID { get; set; }
        public string ProgramCode { get; set; }
        public string Password { get; set; }
    }
}
