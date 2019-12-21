using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    /// <summary>
    /// Commoncaller 
    /// </summary>
    public class commonCaller
    {
        #region Varialbe declaration
        public ICommon _commonRepository;
        #endregion

        #region Send mail

        /// <summary>
        /// Send mail
        /// </summary>
        /// <param name="smtpDetails">SMTP details</param>
        /// <param name="emailToAddress">To Email Address</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="body">Email body</param>
        /// <returns></returns>
        public string sendEmail(SMTPDetails smtpDetails, string emailToAddress, string subject, string body)
        {
            return _commonRepository.SendEmail(smtpDetails, emailToAddress, subject, body);
        }

        #endregion
    }
}
