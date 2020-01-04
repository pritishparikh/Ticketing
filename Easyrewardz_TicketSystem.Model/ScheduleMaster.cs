using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public  class ScheduleMaster
    {
        /// <summary>
        /// Schedule ID
        /// </summary>
        public int ScheduleID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Schedule For
        /// </summary>
        public string ScheduleFor { get; set; }

        /// <summary>
        /// Schedule Type
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// Schedule Time
        /// </summary>
        public DateTime ScheduleTime { get; set; }

        /// <summary>
        /// IsDaily
        /// </summary>
        public bool IsDaily { get; set; }

        /// <summary>
        /// No. Of Day
        /// </summary>
        public int NoOfDay { get; set; }

        /// <summary>
        /// Is Weekly
        /// </summary>
        public bool IsWeekly { get; set; }

        /// <summary>
        /// No Of Week
        /// </summary>
        public int NoOfWeek { get; set; }

        /// <summary>
        /// Day Ids
        /// </summary>
        public string DayIds { get; set; }

        /// <summary>
        /// IsDailyForMonth 
        /// </summary>
        public bool IsDailyForMonth { get; set; }

        /// <summary>
        /// NoOfDaysForMonth 
        /// </summary>
        public int NoOfDaysForMonth { get; set; }

        /// <summary>
        /// NoOfMonthForMonth 
        /// </summary>
        public int NoOfMonthForMonth { get; set; }

        /// <summary>
        /// IsWeeklyForMonth 
        /// </summary>
        public bool IsWeeklyForMonth { get; set; }

        /// <summary>
        /// NoOfMonthForWeek 
        /// </summary>
        public int NoOfMonthForWeek { get; set; }

        /// <summary>
        /// NoOfWeekForWeek 
        /// </summary>
        public int NoOfWeekForWeek { get; set; }

        /// <summary>
        /// NameOfDayForWeek 
        /// </summary>
        public string NameOfDayForWeek { get; set; }

        /// <summary>
        /// IsWeeklyForYear 
        /// </summary
        public bool IsWeeklyForYear { get; set; }

        /// <summary>
        /// NoOfWeekForYear 
        /// </summary
        public int NoOfWeekForYear { get; set; }

        /// <summary>
        /// NameOfDayForYear 
        /// </summary
        public string NameOfDayForYear { get; set; }

        /// <summary>
        /// No Of Day ForYear
        /// </summary
        public string NameOfMonthForYear { get; set; }

        /// <summary>
        /// NameOfMonthForYear
        /// </summary
        public bool IsDailyForYear { get; set; }

        /// <summary>
        /// NameOfMonthForDailyYear 
        /// </summary
        public string NameOfMonthForDailyYear { get; set; }

        /// <summary>
        /// NoOfDayForDailyYear 
        /// </summary
        public int NoOfDayForDailyYear { get; set; }

        /// <summary>
        /// SearchInputParams  
        /// </summary
        public string SearchInputParams { get; set; }

        /// <summary>
        /// IsActive   
        /// </summary
        public bool IsActive { get; set; }

        /// <summary>
        /// CreatedBy    
        /// </summary
        public int CreatedBy { get; set; }

        /// <summary>
        /// CreatedDate     
        /// </summary
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// ModifyBy     
        /// </summary
        public int ModifyBy { get; set; }

        /// <summary>
        /// ModifyBy     
        /// </summary
        public DateTime ModifyDate { get; set; }
    }

}

