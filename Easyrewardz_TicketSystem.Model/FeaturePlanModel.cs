using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class FeaturePlanModel
    {
        
      public List<PlanModel> Plans { get; set; }
      public List<FeatureModel> Features { get; set; }
      public List<PlanCheckedModel> PlanCheckeds { get; set; }

    }

    public class PlanModel
    {

        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public decimal MonthlyAmount { get; set; }
        public decimal YearlyAmount { get; set; }
        public int TotalUsers { get; set; }
        public int IsPublished { get; set; }
        public int IsMostPopular { get; set; }
        public int CreatedBy { get; set; }
        public string FeatureID { get; set; }
        public string AddOnsID { get; set; }

    }

    public class FeatureModel
    {
        public string FeatureName { get; set; }
        public int FeatureID { get; set; }
        public string Tooltip { get; set; }
    }

    public class PlanCheckedModel
    {
        public int FeatureID { get; set; }
        public string PlanName { get; set; } 
        public int PlanID { get; set; }
        public string Checked { get; set; }

    }
}
