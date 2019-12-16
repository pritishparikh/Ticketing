using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ITicketing
    {
        /// <summary>
        /// Get Employees 
        /// </summary>
        /// <returns></returns>
        IEnumerable<TicketingDetails> getTcikets();
    }
}
