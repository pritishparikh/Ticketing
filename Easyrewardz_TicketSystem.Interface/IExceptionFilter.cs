using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Easyrewardz_TicketSystem.Interface
{
    interface IExceptionFilter: IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}
