using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
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
        /// Generate Store Pay Link
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        ///  <param name="clientAPIUrlForGenerateToken"></param>
        ///   <param name="clientAPIUrlForGeneratePaymentLink"></param>
        ///    <param name="hSRequestGenerateToken"></param>
        /// <returns></returns>
        public string GenerateStorePayLink(IStorePay storePay, int TenantID, string ProgramCode, int UserID,
            string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, HSRequestGenerateToken hSRequestGenerateToken)
        {
            _storePay = storePay;
            return _storePay.GenerateStorePayLink(TenantID, ProgramCode, UserID, clientAPIUrlForGenerateToken, clientAPIUrlForGeneratePaymentLink, hSRequestGenerateToken);
        }

        #endregion
    }
}
