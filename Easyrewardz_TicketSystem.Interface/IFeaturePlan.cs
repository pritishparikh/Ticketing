using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IFeaturePlan
    {
        /// <summary>
        /// Get Feature Plan List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        FeaturePlanModel GetFeaturePlanList(int TenantID);

        /// <summary>
        /// Add Feature
        /// </summary>
        /// <param name="objFeatures"></param>
        /// <returns></returns>
        string AddFeature(FeaturesModel objFeatures);

        /// <summary>
        /// Delete Feature
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="FeatureID"></param>
        /// <returns></returns>
        int DeleteFeature(int UserID,int FeatureID);

        /// <summary>
        /// Add Plan
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        int AddPlan(PlanModel plan);

        /// <summary>
        /// Get Plan On Edit
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<PlanModel> GetPlanOnEdit(int TenantID);
    }

}
