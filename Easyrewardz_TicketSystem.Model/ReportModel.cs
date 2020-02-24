using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class ReportModel
    {
        public int ReportID { get; set; }
        public int ScheduleID { get; set; }
        public int IsDownloaded { get; set; }
        public string ReportName { get; set; }
        public string ReportSearchParams { get; set; }
        public string ScheduleStatus { get; set; }
        public string ReportStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ScheduleFor { get; set; }
        public int ScheduleType { get; set; }
        public DateTime ScheduleTime { get; set; }
        public bool IsDaily { get; set; }
        public int NoOfDay { get; set; }
        public bool IsWeekly { get; set; }
        public int NoOfWeek { get; set; }
        public string DayIds { get; set; }
        public bool IsDailyForMonth { get; set; }
        public int NoOfDaysForMonth { get; set; }
        public int NoOfMonthForMonth { get; set; }
        public bool IsWeeklyForMonth { get; set; }
        public int NoOfMonthForWeek { get; set; }
        public int NoOfWeekForWeek { get; set; }
        public string NameOfDayForWeek { get; set; }
        public bool IsWeeklyForYear { get; set; }        
        public int NoOfWeekForYear { get; set; }
        public string NameOfDayForYear { get; set; }
        public string NameOfMonthForYear { get; set; }
        public bool IsDailyForYear { get; set; }
        public string NameOfMonthForDailyYear { get; set; }
        public int NoOfDayForDailyYear { get; set; }
    }
}
