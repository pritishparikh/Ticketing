using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class SMTPDetails
    {
        public string FromEmailId { get; set; }

        public string Password { get; set; }

        public bool EnableSsl { get; set; }

        public int SMTPPort { get; set; }

        public string SMTPServer { get; set; }

        public bool IsBodyHtml { get; set; }

        public string SMTPHost { get; set; }
    }
}
