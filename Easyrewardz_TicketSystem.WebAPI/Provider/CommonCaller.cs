using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class commonCaller
    {
        public ICommon _commonRepository;

        public string sendEmail(SMTPDetails smtpDetails, string emailToAddress, string subject, string body)
        {
            return _commonRepository.SendEmail(smtpDetails, emailToAddress, subject, body);
        }
    }
}
