using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Threading.Tasks;

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

        /// <summary>
        /// Insert API request response Log
        /// </summary>
        /// <param name="Log"></param>
        /// <returns></returns>
        Task<int> InsertAPILog(APILogModel Log);
    }
}
