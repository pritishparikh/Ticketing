using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// AccountModel
    /// </summary>
    public class Authenticate
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        //public int SessionID { get; set; }

        public string UserID { get; set; }

        //public string SecurityToken { get; set; }

        public  bool IsActive { get; set; }

        //public string SystemSessionID { get; set; }

        public string AppID { get; set; }
        
        public string ProgramCode { get; set; }

        //public DateTime ExpiryDate { get; set; }

        public DateTime LoginTime { get; set; }

        //public int IndexID { get; set; }    
        
        public string Domain_Name { get; set; }


        public Int32 UserMasterID { get; set; }

        public int TenantId { get; set; }
    }
}
