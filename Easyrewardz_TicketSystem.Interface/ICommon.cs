using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the common methods
    /// </summary>
    public interface ICommon
    {
        string SendEmail(SMTPDetails smtpDetails, string emailToAddress, string subject, string body);
    }
}
