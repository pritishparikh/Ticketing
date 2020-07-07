using Easyrewardz_TicketSystem.Interface.StoreInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider.Store
{
    public class StorePayCaller
    {
        #region Variable declaration

        private IStorePay _storePay;
        #endregion



        #region Methods 

        /// <summary>
        /// Generate StorePay Link
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string GenerateStorePayLink(IStorePay storePay, int TenantID, string ProgramCode,  int UserID)
        {
            _storePay = storePay;
            return _storePay.GenerateStorePayLink(TenantID, ProgramCode, UserID);
        }

        #endregion
    }
}
