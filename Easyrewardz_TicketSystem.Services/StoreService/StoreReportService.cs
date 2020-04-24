using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        /// <param name="StoreReportModel"></param>
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

                cmd = new MySqlCommand("SP_GetStoreReportCount", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", searchModel.TenantID);
                cmd.Parameters.AddWithValue("@_ActiveTabID", searchModel.ActiveTabId);

                /*------------------ TASK PARAMETERS ------------------------------*/

                cmd.Parameters.AddWithValue("@_TaskTitle", string.IsNullOrEmpty(searchModel.TaskTitle) ? "" : searchModel.TaskTitle);
                cmd.Parameters.AddWithValue("@_TaskStatus", string.IsNullOrEmpty(searchModel.TaskStatus) ? "" :  searchModel.TaskStatus);
                cmd.Parameters.AddWithValue("@_IsTaskWithTicket",Convert.ToInt16( searchModel.IsTaskWithTicket));
                cmd.Parameters.AddWithValue("@_TaskTicketID", searchModel.TaskTicketID);
                cmd.Parameters.AddWithValue("@_DepartmentIds", string.IsNullOrEmpty(searchModel.DepartmentIds) ? "" : searchModel.DepartmentIds);
                cmd.Parameters.AddWithValue("@_FunctionIds", string.IsNullOrEmpty(searchModel.FunctionIds) ? "" : searchModel.FunctionIds);
                cmd.Parameters.AddWithValue("@_PriorityIds", string.IsNullOrEmpty(searchModel.PriorityIds) ? "" :  searchModel.PriorityIds);
                cmd.Parameters.AddWithValue("@_IsTaskWithClaim", Convert.ToInt16(searchModel.IsTaskWithClaim));
                cmd.Parameters.AddWithValue("@_TaskClaimID", searchModel.TaskClaimID);
                cmd.Parameters.AddWithValue("@_TaskCreatedDate", searchModel.TaskCreatedDate);
                cmd.Parameters.AddWithValue("@_TaskCreatedBy", searchModel.TaskCreatedBy);
                cmd.Parameters.AddWithValue("@_TaskAssignedId", searchModel.TaskAssignedId);

                /*------------------ ENDS HERE-------------------------------*/

                /*------------------ CLAIM  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_ClaimID", searchModel.ClaimID);
                cmd.Parameters.AddWithValue("@_ClaimStatus", string.IsNullOrEmpty(searchModel.ClaimStatus) ? "" : searchModel.ClaimStatus);
                cmd.Parameters.AddWithValue("@_IsClaimWithTicket", Convert.ToInt16(searchModel.IsClaimWithTicket));
                cmd.Parameters.AddWithValue("@_ClaimTicketID", searchModel.ClaimTicketID);
                cmd.Parameters.AddWithValue("@_ClaimCategoryIds", string.IsNullOrEmpty(searchModel.ClaimCategoryIds) ? "" : searchModel.ClaimCategoryIds);
                cmd.Parameters.AddWithValue("@_ClaimSubCategoryIds", string.IsNullOrEmpty(searchModel.ClaimSubCategoryIds) ? "" : searchModel.ClaimSubCategoryIds);
                cmd.Parameters.AddWithValue("@_ClaimIssuetypeIds", string.IsNullOrEmpty(searchModel.ClaimIssuetypeIds) ? "" : searchModel.ClaimIssuetypeIds);
                cmd.Parameters.AddWithValue("@_IsClaimWithTask", Convert.ToInt16(searchModel.IsClaimWithTask));
                cmd.Parameters.AddWithValue("@_ClaimTaskID", searchModel.ClaimTaskID);
                cmd.Parameters.AddWithValue("@_ClaimCreatedDate", searchModel.ClaimCreatedDate);
                cmd.Parameters.AddWithValue("@_ClaimCreatedBy", searchModel.ClaimCreatedBy);
                cmd.Parameters.AddWithValue("@_ClaimAssignedId", searchModel.ClaimAssignedId);



                /*------------------ CAMPAIGN  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_CampaignName", string.IsNullOrEmpty(searchModel.CampaignName) ? "" : searchModel.CampaignName);
                cmd.Parameters.AddWithValue("@_CampaignAssignedId", searchModel.CampaignAssignedIds);
                cmd.Parameters.AddWithValue("@_CampaignStartDate", string.IsNullOrEmpty(searchModel.CampaignStartDate) ? "" : searchModel.CampaignStartDate);
                cmd.Parameters.AddWithValue("@_CampaignEndDate", string.IsNullOrEmpty(searchModel.CampaignEndDate) ? "" : searchModel.CampaignEndDate);
                cmd.Parameters.AddWithValue("@_CampaignStatusids", string.IsNullOrEmpty(searchModel.CampaignStatusids) ? "" : searchModel.CampaignStatusids);

                /*------------------ ENDS HERE-------------------------------*/


                cmd.CommandType = CommandType.StoredProcedure;

                resultCount = Convert.ToInt32(cmd.ExecuteScalar());

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
        /// Search the Report
        /// </summary>
        /// <param name="StoreReportModel"></param>
        /// <returns></returns>
        public List<string> DownloadStoreReportSearch(StoreReportModel searchModel)
        {
            List<string> ReportDownloadList = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_ScheduleStoreReportForDownload", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", searchModel.TenantID);
                cmd.Parameters.AddWithValue("@_ActiveTabID", searchModel.ActiveTabId);

                /*------------------ TASK PARAMETERS ------------------------------*/

                cmd.Parameters.AddWithValue("@_TaskTitle", string.IsNullOrEmpty(searchModel.TaskTitle) ? "" : searchModel.TaskTitle);
                cmd.Parameters.AddWithValue("@_TaskStatus", string.IsNullOrEmpty(searchModel.TaskStatus) ? "" : searchModel.TaskStatus);
                cmd.Parameters.AddWithValue("@_IsTaskWithTicket", Convert.ToInt16(searchModel.IsTaskWithTicket));
                cmd.Parameters.AddWithValue("@_TaskTicketID", searchModel.TaskTicketID);
                cmd.Parameters.AddWithValue("@_DepartmentIds", string.IsNullOrEmpty(searchModel.DepartmentIds) ? "" : searchModel.DepartmentIds);
                cmd.Parameters.AddWithValue("@_FunctionIds", string.IsNullOrEmpty(searchModel.FunctionIds) ? "" : searchModel.FunctionIds);
                cmd.Parameters.AddWithValue("@_PriorityIds", string.IsNullOrEmpty(searchModel.PriorityIds) ? "" : searchModel.PriorityIds);
                cmd.Parameters.AddWithValue("@_IsTaskWithClaim", Convert.ToInt16(searchModel.IsTaskWithClaim));
                cmd.Parameters.AddWithValue("@_TaskClaimID", searchModel.TaskClaimID);
                cmd.Parameters.AddWithValue("@_TaskCreatedDate", searchModel.TaskCreatedDate);
                cmd.Parameters.AddWithValue("@_TaskCreatedBy", searchModel.TaskCreatedBy);
                cmd.Parameters.AddWithValue("@_TaskAssignedId", searchModel.TaskAssignedId);

                /*------------------ ENDS HERE-------------------------------*/

                /*------------------ CLAIM  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_ClaimID", searchModel.ClaimID);
                cmd.Parameters.AddWithValue("@_ClaimStatus", string.IsNullOrEmpty(searchModel.ClaimStatus) ? "" : searchModel.ClaimStatus);
                cmd.Parameters.AddWithValue("@_IsClaimWithTicket", Convert.ToInt16(searchModel.IsClaimWithTicket));
                cmd.Parameters.AddWithValue("@_ClaimTicketID", searchModel.ClaimTicketID);
                cmd.Parameters.AddWithValue("@_ClaimCategoryIds", string.IsNullOrEmpty(searchModel.ClaimCategoryIds) ? "" : searchModel.ClaimCategoryIds);
                cmd.Parameters.AddWithValue("@_ClaimSubCategoryIds", string.IsNullOrEmpty(searchModel.ClaimSubCategoryIds) ? "" : searchModel.ClaimSubCategoryIds);
                cmd.Parameters.AddWithValue("@_ClaimIssuetypeIds", string.IsNullOrEmpty(searchModel.ClaimIssuetypeIds) ? "" : searchModel.ClaimIssuetypeIds);
                cmd.Parameters.AddWithValue("@_IsClaimWithTask", Convert.ToInt16(searchModel.IsClaimWithTask));
                cmd.Parameters.AddWithValue("@_ClaimTaskID", searchModel.ClaimTaskID);
                cmd.Parameters.AddWithValue("@_ClaimCreatedDate", searchModel.ClaimCreatedDate);
                cmd.Parameters.AddWithValue("@_ClaimCreatedBy", searchModel.ClaimCreatedBy);
                cmd.Parameters.AddWithValue("@_ClaimAssignedId", searchModel.ClaimAssignedId);



                /*------------------ CAMPAIGN  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_CampaignName", string.IsNullOrEmpty(searchModel.CampaignName) ? "" : searchModel.CampaignName);
                cmd.Parameters.AddWithValue("@_CampaignAssignedId", searchModel.CampaignAssignedIds);
                cmd.Parameters.AddWithValue("@_CampaignStartDate", string.IsNullOrEmpty(searchModel.CampaignStartDate) ? "" : searchModel.CampaignStartDate);
                cmd.Parameters.AddWithValue("@_CampaignEndDate", string.IsNullOrEmpty(searchModel.CampaignEndDate) ? "" : searchModel.CampaignEndDate);
                cmd.Parameters.AddWithValue("@_CampaignStatusids", string.IsNullOrEmpty(searchModel.CampaignStatusids) ? "" : searchModel.CampaignStatusids);

                /*------------------ ENDS HERE-------------------------------*/


                cmd.CommandType = CommandType.StoredProcedure;

                resultCount = Convert.ToInt32(cmd.ExecuteScalar());

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
            return ReportDownloadList;
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

        /// <summary>
        /// Get Store Report List
        /// </summary>
        public List<ReportModel> StoreReportList(int tenantID)
        {
            List<ReportModel> objReportLst = new List<ReportModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreReports", conn);
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
                            ScheduleType = Convert.ToInt32(r.Field<object>("ScheduleType")),
                            ReportSearchParams = r.Field<object>("StoreReportSearchParams") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("StoreReportSearchParams")),
                            IsDownloaded = Convert.ToInt32(r.Field<object>("IsDownloaded")),
                            ReportName = r.Field<object>("ReportName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ReportName")),
                            ReportStatus = r.Field<object>("ReportStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ReportStatus")),
                            ScheduleStatus = r.Field<object>("ScheduleStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ScheduleStatus")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ScheduleFor = r.Field<object>("ScheduleFor") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ScheduleFor")),
                            ScheduleTime = r.Field<object>("ScheduleTime") == System.DBNull.Value ? default(DateTime?) : Convert.ToDateTime(Convert.ToString(r.Field<object>("ScheduleTime"))),
                            IsDaily = Convert.ToBoolean(r.Field<object>("IsDaily") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsDaily"))),
                            NoOfDay = Convert.ToInt32(r.Field<object>("NoOfDay") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfDay"))),
                            IsWeekly = Convert.ToBoolean(r.Field<object>("IsWeekly") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsWeekly"))),
                            NoOfWeek = Convert.ToInt32(r.Field<object>("NoOfWeek") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfWeek"))),
                            DayIds = r.Field<object>("DayIds") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("DayIds")),
                            IsDailyForMonth = Convert.ToBoolean(r.Field<object>("IsDailyForMonth") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsDailyForMonth"))),
                            NoOfDaysForMonth = Convert.ToInt32(r.Field<object>("NoOfDaysForMonth") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfDaysForMonth"))),
                            NoOfMonthForMonth = Convert.ToInt32(r.Field<object>("NoOfMonthForMonth") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfMonthForMonth"))),
                            IsWeeklyForMonth = Convert.ToBoolean(r.Field<object>("IsWeeklyForMonth") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsWeeklyForMonth"))),
                            NoOfMonthForWeek = Convert.ToInt32(r.Field<object>("NoOfMonthForWeek") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfMonthForWeek"))),
                            NoOfWeekForWeek = Convert.ToInt32(r.Field<object>("NoOfWeekForWeek") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfWeekForWeek"))),
                            NameOfDayForYear = r.Field<object>("NameOfDayForYear") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NameOfDayForYear")),
                            NameOfDayForWeek = r.Field<object>("NameOfDayForWeek") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NameOfDayForWeek")),
                            NoOfWeekForYear = r.Field<object>("NoOfWeekForYear") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfWeekForYear")),
                            NameOfMonthForYear = r.Field<object>("NameOfMonthForYear") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NameOfMonthForYear")),
                            IsDailyForYear = Convert.ToBoolean(r.Field<object>("IsDailyForYear") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsDailyForYear"))),
                            NameOfMonthForDailyYear = r.Field<object>("NameOfMonthForDailyYear") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NameOfMonthForDailyYear")),
                            NoOfDayForDailyYear = Convert.ToInt32(r.Field<object>("NoOfDayForDailyYear") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NoOfDayForDailyYear")))

                        }).ToList();
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ds != null)
                 ds.Dispose();
                conn.Close();
            }


            return objReportLst;

        }

        /// <summary>
        /// Delete StoreReport
        /// </summary>
        public int DeleteStoreReport(int tenantID, int ReportID)
        {
            int deleteCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteStoreReport", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantId", tenantID);
                cmd.Parameters.AddWithValue("@_reportId", ReportID);
                cmd.CommandType = CommandType.StoredProcedure;
                deleteCount = cmd.ExecuteNonQuery();
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

            return deleteCount;
        }


        /// <summary>
        /// Save/Update  Store Re[ort
        /// </summary>
        /// <param name="StoreReportRequest"></param>
        /// <returns></returns>
        public int SaveStoreReport(StoreReportRequest ReportMaster)
        {


            int ReportID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_SaveStoreReport", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", ReportMaster.TenantID);
                cmd.Parameters.AddWithValue("@_ScheduleID", ReportMaster.ScheduleID);
                cmd.Parameters.AddWithValue("@_ReportID", ReportMaster.ReportID);
                cmd.Parameters.AddWithValue("@_ReportName", string.IsNullOrEmpty(ReportMaster.ReportName) ? "" : ReportMaster.ReportName);
                cmd.Parameters.AddWithValue("@_StoreReportSearchParams", string.IsNullOrEmpty(ReportMaster.StoreReportSearchParams) ? "" : ReportMaster.StoreReportSearchParams);
                cmd.Parameters.AddWithValue("@_CreatedBy", ReportMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@_ModifyBy", ReportMaster.ModifyBy);
                cmd.Parameters.AddWithValue("@_IsActive", Convert.ToInt16(ReportMaster.IsActive));

                cmd.CommandType = CommandType.StoredProcedure;

                ReportID = Convert.ToInt32(cmd.ExecuteScalar());
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
            return ReportID;
        }


        /// <summary>
        /// Get Campaign Names
        /// </summary>
        /// <returns></returns>
       public  List<CampaignScriptName> GetCampaignNames()
        {
            List<CampaignScriptName> objCampaignList = new List<CampaignScriptName>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("GetCampaignNames", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                //  cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objCampaignList = ds.Tables[0].AsEnumerable().Select(r => new CampaignScriptName()
                        {
                            CampaignNameID = Convert.ToInt32(r.Field<object>("CampaignID")),
                            CampaignName = r.Field<object>("CampaignName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CampaignName")),

                        }).ToList();

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
                conn.Close();
            }

            return objCampaignList;
        }
    }
}
