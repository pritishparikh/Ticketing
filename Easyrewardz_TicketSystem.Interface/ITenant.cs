using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface ITenant
    {
        /// <summary>
        /// Insert Company
        /// </summary>
        /// <param name="companyModel"></param>
        /// <returns></returns>
        int InsertCompany(CompanyModel companyModel);

        /// <summary>
        /// Billing Details_crud
        /// </summary>
        /// <param name="BillingDetails"></param>
        /// <returns></returns>
        int BillingDetails_crud(BillingDetails BillingDetails);

        /// <summary>
        /// Other Details
        /// </summary>
        /// <param name="OtherDetails"></param>
        /// <returns></returns>
        int OtherDetails(OtherDetailsModel OtherDetails);

        /// <summary>
        /// Insert Plan Feature
        /// </summary>
        /// <param name="PlanName"></param>
        /// <param name="FeatureID"></param>
        /// <param name="UserMasterID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int InsertPlanFeature(string PlanName, string FeatureID, int UserMasterID,int TenantId);

        /// <summary>
        /// Get Plan Details
        /// </summary>
        /// <param name="CustomPlanID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<GetPlanDetails> GetPlanDetails(int CustomPlanID, int TenantId);

        /// <summary>
        /// Add Plan
        /// </summary>
        /// <param name="tenantPlan"></param>
        /// <returns></returns>
        int AddPlan(TenantPlan tenantPlan);

        /// <summary>
        /// Get Company Type
        /// </summary>
        /// <returns></returns>
        List<CompanyTypeModel> GetCompanyType();

        /// <summary>
        /// Get Registered Tenant
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<CompanyModel> GetRegisteredTenant(int TenantId);

    }
}
