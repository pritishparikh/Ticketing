using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IErrorLogging
    {
        int InsertErrorLog(ErrorLog errorLog);
    }
}
