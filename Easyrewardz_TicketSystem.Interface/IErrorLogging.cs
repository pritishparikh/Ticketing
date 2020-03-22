using Easyrewardz_TicketSystem.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IErrorLogging
    {
        int InsertErrorLog(ErrorLog errorLog);
    }
}
