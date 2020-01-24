using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class ReportService : IReports
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public ReportService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion


            #region custom Methods
        /// <summary>
        /// Delete Report
        /// </summary>
        public int DeleteReport(int tenantID, int ReportID)
        {
            int deletecount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteReport", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantId", tenantID);
                cmd.Parameters.AddWithValue("@_reportId", ReportID);


                cmd.CommandType = CommandType.StoredProcedure;
                deletecount = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return deletecount;
        }

        /// <summary>
        /// Create Report 
        /// </summary>
        public int InsertReport(int tenantId, string ReportName, bool isReportActive, string TicketReportParams,
            bool IsDaily, bool IsDailyForMonth, bool IsWeekly, bool IsWeeklyForMonth, bool IsDailyForYear, bool IsWeeklyForYear, int createdBy)
        {
            int InsertCount = 0;


            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertReport", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantId ", tenantId);
                cmd.Parameters.AddWithValue("@_ReportName ", ReportName);
                cmd.Parameters.AddWithValue("@_isReportActive ", Convert.ToInt16(isReportActive));
                cmd.Parameters.AddWithValue("@_TicketReportParams", TicketReportParams);
                cmd.Parameters.AddWithValue("@_IsDaily", Convert.ToInt16(IsDaily));
                cmd.Parameters.AddWithValue("@_IsDailyForMonth", Convert.ToInt16(IsDailyForMonth));
                cmd.Parameters.AddWithValue("@_IsWeekly", Convert.ToInt16(IsWeekly));
                cmd.Parameters.AddWithValue("@_IsWeeklyForMonth", Convert.ToInt16(IsWeeklyForMonth));
                cmd.Parameters.AddWithValue("@_IsDailyForYear", Convert.ToInt16(IsDailyForYear));
                cmd.Parameters.AddWithValue("@_IsWeeklyForYear", Convert.ToInt16(IsWeeklyForYear));
                cmd.Parameters.AddWithValue("@_createdBy", createdBy);
                cmd.CommandType = CommandType.StoredProcedure;
                InsertCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return InsertCount;
        }

        #endregion

    }
}
