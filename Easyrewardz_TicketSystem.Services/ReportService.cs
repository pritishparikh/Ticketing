using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                cmd.Parameters.AddWithValue("@_tenantId", tenantId);
                cmd.Parameters.AddWithValue("@_ReportName", ReportName);
                cmd.Parameters.AddWithValue("@_isReportActive", Convert.ToInt16(isReportActive));
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


        /// <summary>
        /// Get Alert List
        /// </summary>
        public List<ReportModel> GetReportList(int tenantID)
        {
            List<ReportModel> objReportLst = new List<ReportModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetReportList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objReportLst = ds.Tables[0].AsEnumerable().Select(r => new ReportModel()
                        {
                            ReportID = Convert.ToInt32(r.Field<object>("ReportID")),
                            ScheduleID = Convert.ToInt32(r.Field<object>("ScheduleID")),
                            ReportName = r.Field<object>("ReportName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ReportName")),

                            ScheduleStatus = r.Field<object>("ReportStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ReportStatus")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate"))

                        }).ToList();
                    }


                }
            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose(); conn.Close();
            }


            return objReportLst;

        }

        #endregion

    }
}
