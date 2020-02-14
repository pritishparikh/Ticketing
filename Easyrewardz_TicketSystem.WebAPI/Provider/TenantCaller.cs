﻿using System;
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
        #endregion

        #region method
        public int InsertCompany(ITenant _tenant,  CompanyModel companyModel, int TenantId)
        {
            _tenantlist = _tenant;
            return _tenantlist.InsertCompany(companyModel, TenantId);
        }
        public int BillingDetails_crud(ITenant _tenant, BillingDetails BillingDetails)
        {
            _tenantlist = _tenant;
            return _tenantlist.BillingDetails_crud(BillingDetails);
        }

        public int OtherDetails(ITenant _tenant, OtherDetailsModel OtherDetails)
        {
            _tenantlist = _tenant;
            return _tenantlist.OtherDetails(OtherDetails);
        }

        public int InsertPlanFeature(ITenant _tenant,string PlanName,string FeatureID,int UserMasterID,int TenantId)
        {
            _tenantlist = _tenant;
            return _tenantlist.InsertPlanFeature(PlanName, FeatureID, UserMasterID, TenantId);
        }
        public  List<GetPlanDetails> GetPlanDetails(ITenant _tenant,  int CustomPlanID, int TenantId)
        {
            _tenantlist = _tenant;
            return _tenantlist.GetPlanDetails(CustomPlanID, TenantId);
        }

        #endregion
    }
}
