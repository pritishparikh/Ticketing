using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IAppointment
    {
        /// <summary>
        /// Get Store Details By StoreCode
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <returns></returns>
        StoreDetails GetStoreDetailsByStoreCode(int tenantID, int userID, string programcode, string storeCode);
    }
}
