using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// AccountModel
    /// </summary>
    public class AccountModal
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        public string UserID { get; set; }

        public  bool IsActive { get; set; }

        public DateTime LoginTime { get; set; }

        /// <summary>
        /// Is Valid
        /// </summary>
        public bool IsValidUser { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserEmailID { get; set; }
    }
}
