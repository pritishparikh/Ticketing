using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{

    public class FeaturePlanCaller
    {

        #region Variable declaration

        private  IFeaturePlan _featurePlan;
        #endregion

        #region Methods
        public FeaturePlanModel GetFeaturePlan(IFeaturePlan _feature, int TenantID)
        {
            _featurePlan = _feature;
            return _featurePlan.GetFeaturePlanList(TenantID);
        }
        public string AddFeature(IFeaturePlan _feature,FeaturesModel objFeature)
        {
            _featurePlan = _feature;
            return _featurePlan.AddFeature(objFeature);
        }
        public int DeleteFeature(IFeaturePlan _feature,int UserID,int FeatureID)
        {
            _featurePlan = _feature;
            return _featurePlan.DeleteFeature(UserID,FeatureID);
        }
        #endregion
    }
}
