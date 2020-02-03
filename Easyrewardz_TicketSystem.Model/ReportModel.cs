using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class ReportModel
    {
        public int ReportID { get; set; }
        public int ScheduleID { get; set; }
        public string ReportName { get; set; }
        public string ScheduleStatus { get; set; }
        public string ReportStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

    }
}
