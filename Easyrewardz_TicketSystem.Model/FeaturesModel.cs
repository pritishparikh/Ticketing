using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class FeaturesModel
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public string Tooltip { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal YearlyPrice { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }      
    }
}
