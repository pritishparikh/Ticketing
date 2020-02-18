using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface ITenant
    {
        int InsertCompany(CompanyModel companyModel, int TenantId);
        int BillingDetails_crud(BillingDetails BillingDetails);
        int OtherDetails(OtherDetailsModel OtherDetails);
        int InsertPlanFeature(string PlanName, string FeatureID, int UserMasterID,int TenantId);
        List<GetPlanDetails> GetPlanDetails(int CustomPlanID, int TenantId);
        int AddPlan(TenantPlan tenantPlan);
        List<CompanyTypeModel> GetCompanyType();
    }
}
