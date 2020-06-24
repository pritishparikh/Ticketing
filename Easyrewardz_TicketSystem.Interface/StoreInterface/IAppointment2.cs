using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IAppointment
    {

        StoreDetails GetStoreDetailsByStoreCode(int tenantID, int userID, string programcode, string storeCode);
    }
}
