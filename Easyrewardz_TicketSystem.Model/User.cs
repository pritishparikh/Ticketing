using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class User
    {

        /// <summary>
        /// User Id 
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Reportee Id 
        /// </summary>
        public int ReporteeID { get; set; }
        public string RoleName { get; set; }
        public int RoleID { get; set; }
    }
}
