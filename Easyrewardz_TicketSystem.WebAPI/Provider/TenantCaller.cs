using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class TenantCaller
    {
        #region Variable

        /// <summary>
        /// tenant
        /// </summary>
        private ITenant _tenantlist;

        public int InsertCompany(ITenant _tenant,  CompanyModel companyModel, int TenantId)
        {
            _tenantlist = _tenant;
            return _tenantlist.InsertCompany(companyModel, TenantId);
        }
        #endregion
    }
}
