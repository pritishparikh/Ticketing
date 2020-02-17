using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class TenantPlan
    {
        public int TenantID { get; set; }
        public int PlanID { get; set; }
        public int CustomPlanID { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int GreacePeriodDays { get; set; }
        public int GreacePeriodMonth { get; set; }
        public string AddonsFeatureIDs { get; set; }
        public bool IsCustomPlan { get; set; }
        public int Created_By { get; set; }
        public int Modified_By { get; set; }
    }
}
