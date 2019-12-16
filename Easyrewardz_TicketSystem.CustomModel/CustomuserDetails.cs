using System;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomuserDetails
    {
        public int IndexID { get; set; }
        public string UserName { get; set; }
        public string SecurityToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean Isactive { get; set; }
        public string SessionID { get; set; }
        public string ProgramCode { get; set; }
        public string Password { get; set; }
    }

}
