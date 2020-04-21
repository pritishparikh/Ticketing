using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreReportService: IStoreReport
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public StoreReportService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion


        /// <summary>
        /// Search the Report
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        public int GetStoreReportSearch(StoreReportModel searchModel)
        {

            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0; 
            
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;

                resultCount = 10;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return resultCount;
        }


        /// <summary>
        /// Schedule
        /// </summary>
        /// <param name="ScheduleMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int ScheduleStoreReport(ScheduleMaster scheduleMaster, int TenantID, int UserID)
        {


            int scheduleID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddStoreReportSchedule", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Report_Name", string.IsNullOrEmpty(scheduleMaster.ReportName)? "" : scheduleMaster.ReportName);
                cmd.Parameters.AddWithValue("@Schedule_For", scheduleMaster.ScheduleFor);
                cmd.Parameters.AddWithValue("@Schedule_Type", scheduleMaster.ScheduleType);
                cmd.Parameters.AddWithValue("@Schedule_Time", scheduleMaster.ScheduleTime);
                cmd.Parameters.AddWithValue("@Is_Daily", scheduleMaster.IsDaily);
                cmd.Parameters.AddWithValue("@NoOf_Day", scheduleMaster.NoOfDay);
                cmd.Parameters.AddWithValue("@Is_Weekly", scheduleMaster.IsWeekly);
                cmd.Parameters.AddWithValue("@NoOf_Week", scheduleMaster.NoOfWeek);
                cmd.Parameters.AddWithValue("@Day_Ids", scheduleMaster.DayIds);
                cmd.Parameters.AddWithValue("@IsDailyFor_Month", scheduleMaster.IsDailyForMonth);
                cmd.Parameters.AddWithValue("@NoOfDaysFor_Month", scheduleMaster.NoOfDaysForMonth);
                cmd.Parameters.AddWithValue("@NoOfMonthFor_Month", scheduleMaster.NoOfMonthForMonth);
                cmd.Parameters.AddWithValue("@IsWeeklyFor_Month", scheduleMaster.IsWeeklyForMonth);
                cmd.Parameters.AddWithValue("@NoOfMonthFor_Week", scheduleMaster.NoOfMonthForWeek);
                cmd.Parameters.AddWithValue("@NoOfWeekFor_Week", scheduleMaster.NoOfWeekForWeek);
                cmd.Parameters.AddWithValue("@NameOfDayFor_Week", scheduleMaster.NameOfDayForWeek);
                cmd.Parameters.AddWithValue("@IsWeeklyFor_Year", scheduleMaster.IsWeeklyForYear);
                cmd.Parameters.AddWithValue("@NoOfWeekFor_Year", scheduleMaster.NoOfWeekForYear);
                cmd.Parameters.AddWithValue("@NameOfDayFor_Year", scheduleMaster.NameOfDayForYear);
                cmd.Parameters.AddWithValue("@NameOfMonthFor_Year", scheduleMaster.NameOfMonthForYear);
                cmd.Parameters.AddWithValue("@IsDailyFor_Year", scheduleMaster.IsDailyForYear);
                cmd.Parameters.AddWithValue("@NameOfMonthForDaily_Year", scheduleMaster.NameOfMonthForDailyYear);
                cmd.Parameters.AddWithValue("@NoOfDayForDaily_Year", scheduleMaster.NoOfDayForDailyYear);
                cmd.Parameters.AddWithValue("@SearchInput_Params", scheduleMaster.SearchInputParams);
                cmd.Parameters.AddWithValue("@Schedule_From", scheduleMaster.ScheduleFrom == null ? 0 : scheduleMaster.ScheduleFrom);
                cmd.Parameters.AddWithValue("@Primary_Sch_ID", scheduleMaster.PrimaryScheduleID == null ? 0 : scheduleMaster.PrimaryScheduleID);

                cmd.CommandType = CommandType.StoredProcedure;
              
                scheduleID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return scheduleID;
        }

    }
}
