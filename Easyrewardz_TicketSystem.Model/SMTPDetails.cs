using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// SMTP details
    /// </summary>
    public class SMTPDetails
    {
        /// <summary>
        /// Frome Email Id
        /// </summary>
        public string FromEmailId { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Enable SSL
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// SMTP Port
        /// </summary>
        public string SMTPPort { get; set; }

        /// <summary>
        /// SMTP Server
        /// </summary>
        public string SMTPServer { get; set; }

        /// <summary>
        /// Is body HTML
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// SMTP Host
        /// </summary>
        public string SMTPHost { get; set; }
    }
}
