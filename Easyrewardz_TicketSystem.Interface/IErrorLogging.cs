using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IErrorLogging
    {
        /// <summary>
        /// Insert Error Log
        /// </summary>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        int InsertErrorLog(ErrorLog errorLog);
    }
}
