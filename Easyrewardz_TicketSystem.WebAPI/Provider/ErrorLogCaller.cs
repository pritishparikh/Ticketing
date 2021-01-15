using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class ErrorLogCaller
    {
        #region Variable
        public IErrorLogging _IErrorLogging;
        #endregion

        public int AddErrorLog(IErrorLogging errorLogging, ErrorLog errorLog)
        {
            _IErrorLogging = errorLogging;
            return _IErrorLogging.InsertErrorLog(errorLog);
        }

        public async Task<int> AddAPILog(IErrorLogging errorLogging, APILogModel Log)
        {
            _IErrorLogging = errorLogging;
            return await _IErrorLogging.InsertAPILog(Log);
        }
    }
}
