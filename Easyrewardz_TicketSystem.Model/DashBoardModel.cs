using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class DashBoardDataModel
    {
        public double ResolutionPercentage { get; set; }
        public int All { get; set; }

        public int Open { get; set; }

        public int DueToday { get; set; }

        public int OverDue { get; set; }

        public bool isResolutionSuccess { get; set; }
        public string ResolutionRate { get; set; }
        public string AvgResolutionTAT { get; set; }

        public bool isResponseSuccess { get; set; }
        public string ResponseRate { get; set; }
        public string AvgResponseTAT { get; set; }

        public int TaskClose { get; set; }

        public int TaskOpen { get; set; }

        public int ClaimClose { get; set; }

        public int ClaimOpen { get; set; }

       
    }

    
}
