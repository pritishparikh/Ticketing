using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// AccountModel
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// User Email Id
        /// </summary>
        public string UserEmailID { get; set; }

        /// <summary>
        /// Application ID
        /// </summary>
        public string AppID { get; set; }
        
        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Login Time
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// Domain Name
        /// </summary>
        public string Domain_Name { get; set; }

        /// <summary>
        /// User Master Id
        /// </summary>
        public Int32 UserMasterID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string DeviceSource { get; set; }

        public string FBNID { get; set; }

        public string DeviceID { get; set; }

    }
}
