﻿using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IFeaturePlan
    {
        FeaturePlanModel GetFeaturePlanList(int TenantID);
        string AddFeature(FeaturesModel objFeatures);
        int DeleteFeature(int UserID,int FeatureID);

        int AddPlan(PlanModel plan);

        List<PlanModel> GetPlanOnEdit(int TenantID);
    }

}
