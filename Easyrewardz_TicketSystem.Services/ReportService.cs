using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;
using Easyrewardz_TicketSystem.MySqlDBContext;

namespace Easyrewardz_TicketSystem.Services
{
    public class ReportService : IReports
    {
        #region variable
        private readonly IDistributedCache _Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public ReportService(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            _Cache = cache;
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
                conn = Db.Connection;
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
            
            return deletecount;
        }


        /// <summary>
        /// Delete Report
        /// </summary>
        public int SaveReportForDownload(int tenantID, int UserID, int ScheduleID)
        {
            int saveCount = 0;
            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_SaveReport", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_Id", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Schedule_ID", ScheduleID);
                cmd.CommandType = CommandType.StoredProcedure;
                saveCount = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                throw ex;
            }

            return saveCount;
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
                conn = Db.Connection;
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

                conn = Db.Connection;
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
                            ScheduleType = Convert.ToInt32(r.Field<object>("ScheduleType")),
                            ReportSearchParams = r.Field<object>("TicketReportSearchParams") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TicketReportSearchParams")),
                            IsDownloaded = Convert.ToInt32(r.Field<object>("IsDownloaded")),
                            ReportName = r.Field<object>("ReportName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ReportName")),
                            ReportStatus = r.Field<object>("ReportStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ReportStatus")),
                            ScheduleStatus = r.Field<object>("ScheduleStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ScheduleStatus")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ScheduleFor = r.Field<object>("ScheduleFor") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ScheduleFor")),
                            ScheduleTime = r.Field<object>("ScheduleTime") == "" ? default(DateTime?) : Convert.ToDateTime(r.Field<object>("ScheduleTime")),
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
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose(); 
            }


            return objReportLst;

        }

        public int GetReportSearch(ReportSearchModel searchModel)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponseReport> objSearchResult = new List<SearchResponseReport>();

            List<string> CountList = new List<string>();

            int resultCount = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn = Db.Connection;
                }
                cmd.Connection = conn;

                /*Based on active tab stored procedure will call
                    1. SP_SearchTicketData_ByDate
                    2. SP_SearchTicketData_ByCustomerType
                    3. SP_SearchTicketData_ByTicketType
                    4. SP_SearchTicketData_ByCategoryType
                    5. SP_SearchTicketData_ByAll                 
                 */
                MySqlCommand sqlcmd = new MySqlCommand("", conn);

                // sqlcmd.Parameters.AddWithValue("HeaderStatus_Id", searchModel.HeaderStatusId);
                // sqlcmd.CommandText = "SP_SearchReportData";

                sqlcmd.CommandText = "SP_SearchReportData";

                /*Column 1 (5)*/
                sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.reportSearch.CreatedDate) ? "" : searchModel.reportSearch.CreatedDate);
                sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.reportSearch.ModifiedDate) ? "" : searchModel.reportSearch.ModifiedDate);
                sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.reportSearch.CategoryId);
                sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.reportSearch.SubCategoryId);
                sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.reportSearch.IssueTypeId);

                /*Column 2 (5) */
                sqlcmd.Parameters.AddWithValue("TicketSourceType_ID", searchModel.reportSearch.TicketSourceTypeID);
                sqlcmd.Parameters.AddWithValue("TicketIdORTitle", string.IsNullOrEmpty(searchModel.reportSearch.TicketIdORTitle) ? "" : searchModel.reportSearch.TicketIdORTitle);
                sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.reportSearch.PriorityId);
                sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.reportSearch.TicketSatutsID);
                sqlcmd.Parameters.AddWithValue("SLAStatus", string.IsNullOrEmpty(searchModel.reportSearch.SLAStatus) ? "" : searchModel.reportSearch.SLAStatus);

                /*Column 3 (5)*/
                sqlcmd.Parameters.AddWithValue("TicketClaim_ID", Convert.ToInt32(searchModel.reportSearch.ClaimId == "" ? "0" : searchModel.reportSearch.ClaimId));
                sqlcmd.Parameters.AddWithValue("InvoiceNumberORSubOrderNo", string.IsNullOrEmpty(searchModel.reportSearch.InvoiceNumberORSubOrderNo) ? "" : searchModel.reportSearch.InvoiceNumberORSubOrderNo);
                sqlcmd.Parameters.AddWithValue("OrderItemId", string.IsNullOrEmpty(Convert.ToString(searchModel.reportSearch.OrderItemId)) ? 0 : Convert.ToInt32(searchModel.reportSearch.OrderItemId));
                //sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.reportSearch.IsVisitStore == "yes" ? 1 : 0);
                //sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.reportSearch.IsWantVistingStore == "yes" ? 1 : 0);
                /*All for to load all the data*/
                if (searchModel.reportSearch.IsVisitStore.ToLower() != "all")
                    sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.reportSearch.IsVisitStore == "yes" ? 1 : 0);
                else
                    sqlcmd.Parameters.AddWithValue("IsVisitedStore", -1);

                if (searchModel.reportSearch.IsWantVistingStore.ToLower() != "all")
                    sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.reportSearch.IsWantVistingStore == "yes" ? 1 : 0);
                else
                    sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", -1);

                /*Column 4 (5)*/
                sqlcmd.Parameters.AddWithValue("Customer_EmailID", searchModel.reportSearch.CustomerEmailID);
                sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.reportSearch.CustomerMobileNo) ? "" : searchModel.reportSearch.CustomerMobileNo);
                sqlcmd.Parameters.AddWithValue("AssignTo", searchModel.reportSearch.AssignTo);
                sqlcmd.Parameters.AddWithValue("StoreCodeORAddress", searchModel.reportSearch.StoreCodeORAddress);
                sqlcmd.Parameters.AddWithValue("WantToStoreCodeORAddress", searchModel.reportSearch.WantToStoreCodeORAddress);

                //Row - 2 and Column - 1  (5)
                sqlcmd.Parameters.AddWithValue("HaveClaim", searchModel.reportSearch.HaveClaim);
                sqlcmd.Parameters.AddWithValue("ClaimStatusId", searchModel.reportSearch.ClaimStatusId);
                sqlcmd.Parameters.AddWithValue("ClaimCategoryId", searchModel.reportSearch.ClaimCategoryId);
                sqlcmd.Parameters.AddWithValue("ClaimSubCategoryId", searchModel.reportSearch.ClaimSubCategoryId);
                sqlcmd.Parameters.AddWithValue("ClaimIssueTypeId", searchModel.reportSearch.ClaimIssueTypeId);

                //Row - 2 and Column - 2  (4)
                sqlcmd.Parameters.AddWithValue("HaveTask", searchModel.reportSearch.HaveTask);
                sqlcmd.Parameters.AddWithValue("TaskStatus_Id", searchModel.reportSearch.TaskStatusId);
                sqlcmd.Parameters.AddWithValue("TaskDepartment_Id", searchModel.reportSearch.TaskDepartment_Id);
                sqlcmd.Parameters.AddWithValue("TaskFunction_Id", searchModel.reportSearch.TaskFunction_Id);
                //     sqlcmd.Parameters.AddWithValue("Task_Priority", searchModel.reportSearch.TaskPriority);

                sqlcmd.Parameters.AddWithValue("CurrentUserId", searchModel.curentUserId);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("Assignto_IDs", searchModel.reportSearch.AssignTo.ToString());
                sqlcmd.Parameters.AddWithValue("Brand_IDs", searchModel.reportSearch.BrandID.ToString());

                sqlcmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        resultCount = Convert.ToInt32(ds.Tables[0].Rows[0]["RowCount"]);

                        //objSearchResult = ds.Tables[0].AsEnumerable().Select(r => new SearchResponseReport()
                        //{
                        //    ticketID = Convert.ToInt32(r.Field<object>("TicketID")),
                        //    ticketStatus = Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(r.Field<object>("StatusID"))),
                        //    Message = r.Field<object>("TicketDescription") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TicketDescription")),
                        //    Category = r.Field<object>("CategoryName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CategoryName")),
                        //    subCategory = r.Field<object>("SubCategoryName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SubCategoryName")),
                        //    IssueType = r.Field<object>("IssueTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IssueTypeName")),
                        //    Priority = r.Field<object>("PriortyName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriortyName")),
                        //    Assignee = r.Field<object>("AssignedName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AssignedName")),
                        //    CreatedOn = r.Field<object>("CreatedOn") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedOn")),
                        //    createdBy = r.Field<object>("CreatedByName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedByName")),
                        //    createdago = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("CreatedDate")), "CreatedSpan"),
                        //    assignedTo = r.Field<object>("AssignedName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AssignedName")),
                        //    assignedago = r.Field<object>("AssignedDate") == System.DBNull.Value ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("AssignedDate")), "AssignedSpan"),
                        //    updatedBy = r.Field<object>("ModifyByName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModifyByName")),
                        //    updatedago = r.Field<object>("ModifiedDate") == System.DBNull.Value ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("ModifiedDate")), "ModifiedSpan"),

                        //    responseTimeRemainingBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityRespond") == System.DBNull.Value) ?
                        //    string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "RespondTimeRemainingSpan"),
                        //    responseOverdueBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityRespond") == System.DBNull.Value) ?
                        //    string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResponseOverDueSpan"),

                        //    resolutionOverdueBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityResolve") == System.DBNull.Value) ?
                        //    string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityResolve")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResolutionOverDueSpan"),

                        //    TaskStatus = r.Field<object>("TaskDetails") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TaskDetails")),
                        //    ClaimStatus = r.Field<object>("ClaimDetails") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ClaimDetails")),
                        //    TicketCommentCount = r.Field<object>("ClaimDetails") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TicketComments")),
                        //    isEscalation = r.Field<object>("IsEscalated") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsEscalated")),
                        //    ticketSourceType = Convert.ToString(r.Field<object>("TicketSourceType")),
                        //    IsReassigned = Convert.ToBoolean(r.Field<object>("IsReassigned")),
                        //    ticketSourceTypeID = Convert.ToInt16(r.Field<object>("TicketSourceTypeID"))
                        //}).ToList();
                    }
                }
                // return resultCount;
                //paging here
                //if (searchparams.pageSize > 0 && objSearchResult.Count > 0)
                //    objSearchResult[0].totalpages = objSearchResult.Count > searchparams.pageSize ? Math.Round(Convert.ToDouble(objSearchResult.Count / searchparams.pageSize)) : 1;

                //objSearchResult = objSearchResult.Skip(rowStart).Take(searchparams.pageSize).ToList();
            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose(); 
            }
            return resultCount;
        }

        public string DownloadReportSearch(int SchedulerID, int curentUserId, int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();

            List<ScheduleMaster> ticketschedulemodal = new List<ScheduleMaster>();

            List<string> CountList = new List<string>();
            string csv = String.Empty;

            int resultCount = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn = Db.Connection;
                }
                cmd.Connection = conn;


                MySqlCommand sqlcmd = new MySqlCommand("", conn);

                sqlcmd.CommandText = "SP_DownloadReportSearch";

                sqlcmd.Parameters.AddWithValue("Schedule_ID", SchedulerID);


                sqlcmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

                        ticketschedulemodal = ds.Tables[0].AsEnumerable().Select(r => new ScheduleMaster()
                        {
                            ScheduleID = r.Field<object>("ScheduleID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("ScheduleID")),
                            TenantID = r.Field<object>("TenantID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TenantID")),
                            ScheduleFor = r.Field<object>("ScheduleFor") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ScheduleFor")),
                            ScheduleType = r.Field<object>("ScheduleType") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("ScheduleType")),
                            //ScheduleTime = Convert.ToString(r.Field<object>("ScheduleTime")),
                            IsDaily = Convert.ToBoolean(r.Field<object>("IsDaily")),
                            NoOfDay = r.Field<object>("NoOfDay") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfDay")),
                            IsWeekly = Convert.ToBoolean(r.Field<object>("IsWeekly")),
                            NoOfWeek = r.Field<object>("NoOfWeek") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfWeek")),
                            DayIds = r.Field<object>("DayIds") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("DayIds")),
                            IsDailyForMonth = Convert.ToBoolean(r.Field<object>("IsDailyForMonth")),
                            NoOfDaysForMonth = r.Field<object>("NoOfDaysForMonth") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfDaysForMonth")),
                            NoOfMonthForMonth = r.Field<object>("NoOfMonthForMonth") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfMonthForMonth")),
                            IsWeeklyForMonth = Convert.ToBoolean(r.Field<object>("IsWeeklyForMonth")),
                            NoOfMonthForWeek = r.Field<object>("NoOfMonthForWeek") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfMonthForWeek")),
                            NoOfWeekForWeek = r.Field<object>("NoOfWeekForWeek") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfWeekForWeek")),
                            NameOfDayForWeek = r.Field<object>("NameOfDayForWeek") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NameOfDayForWeek")),
                            IsWeeklyForYear = Convert.ToBoolean(r.Field<object>("IsWeeklyForYear")),
                            NoOfWeekForYear = r.Field<object>("NoOfWeekForYear") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfWeekForYear")),
                            NameOfDayForYear = r.Field<object>("NameOfDayForYear") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NameOfDayForYear")),
                            NameOfMonthForYear = r.Field<object>("NameOfMonthForYear") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NameOfMonthForYear")),
                            IsDailyForYear = Convert.ToBoolean(r.Field<object>("IsDailyForYear")),
                            NameOfMonthForDailyYear = r.Field<object>("NameOfMonthForDailyYear") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NameOfMonthForDailyYear")),
                            NoOfDayForDailyYear = r.Field<object>("NoOfDayForDailyYear") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("NoOfDayForDailyYear")),
                            SearchInputParams = r.Field<object>("SearchInputParams") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SearchInputParams")),
                            IsActive = Convert.ToBoolean(r.Field<object>("IsActive")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("CreatedBy")),
                            CreatedDate = Convert.ToDateTime(r.Field<object>("CreatedDate")),
                            ModifyBy = r.Field<object>("ModifyBy") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("ModifyBy")),
                            ModifyDate = Convert.ToDateTime(r.Field<object>("ModifyDate")),
                            ScheduleFrom = r.Field<object>("ScheduleFrom") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("ScheduleFrom"))
                        }).ToList();

                        if (ticketschedulemodal.Count > 0)
                        {
                            conn.Close();
                            ReportSearchModel searchModel = new ReportSearchModel();
                            searchModel.reportSearch = JsonConvert.DeserializeObject<ReportSearchData>(ticketschedulemodal[0].SearchInputParams);
                            List<SearchResponseReport> searchresponsereport = new List<SearchResponseReport>();
                            searchModel.TenantID = TenantID;
                            searchModel.curentUserId = curentUserId;

                            searchresponsereport = GetDownloadReportSearch(searchModel);
                            csv = CommonService.ListToCSV(searchresponsereport, "totalpages,isEscalation,ClaimStatus,TaskStatus,TicketCommentCount");


                        }
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
                if (ds != null) ds.Dispose(); 
            }
            return csv;
        }

        public List<SearchResponseReport> GetDownloadReportSearch(ReportSearchModel searchModel)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponseReport> objSearchResult = new List<SearchResponseReport>();

            List<string> CountList = new List<string>();

            int resultCount = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn = Db.Connection;
                }
                cmd.Connection = conn;

                /*Based on active tab stored procedure will call
                    1. SP_SearchTicketData_ByDate
                    2. SP_SearchTicketData_ByCustomerType
                    3. SP_SearchTicketData_ByTicketType
                    4. SP_SearchTicketData_ByCategoryType
                    5. SP_SearchTicketData_ByAll                 
                 */
                MySqlCommand sqlcmd = new MySqlCommand("", conn);

                // sqlcmd.Parameters.AddWithValue("HeaderStatus_Id", searchModel.HeaderStatusId);
                // sqlcmd.CommandText = "SP_SearchReportData";

                sqlcmd.CommandText = "SP_Report_SchedulerSearch";

                /*Column 1 (5)*/
                sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.reportSearch.CreatedDate) ? "" : searchModel.reportSearch.CreatedDate);
                sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.reportSearch.ModifiedDate) ? "" : searchModel.reportSearch.ModifiedDate);
                sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.reportSearch.CategoryId);
                sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.reportSearch.SubCategoryId);
                sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.reportSearch.IssueTypeId);

                /*Column 2 (5) */
                sqlcmd.Parameters.AddWithValue("TicketSourceType_ID", searchModel.reportSearch.TicketSourceTypeID);
                sqlcmd.Parameters.AddWithValue("TicketIdORTitle", string.IsNullOrEmpty(searchModel.reportSearch.TicketIdORTitle) ? "" : searchModel.reportSearch.TicketIdORTitle);
                sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.reportSearch.PriorityId);
                sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.reportSearch.TicketSatutsID);
                sqlcmd.Parameters.AddWithValue("SLAStatus", string.IsNullOrEmpty(searchModel.reportSearch.SLAStatus) ? "" : searchModel.reportSearch.SLAStatus);

                /*Column 3 (5)*/
                sqlcmd.Parameters.AddWithValue("TicketClaim_ID", Convert.ToInt32(searchModel.reportSearch.ClaimId == "" ? "0" : searchModel.reportSearch.ClaimId));
                sqlcmd.Parameters.AddWithValue("InvoiceNumberORSubOrderNo", string.IsNullOrEmpty(searchModel.reportSearch.InvoiceNumberORSubOrderNo) ? "" : searchModel.reportSearch.InvoiceNumberORSubOrderNo);
                sqlcmd.Parameters.AddWithValue("OrderItemId", string.IsNullOrEmpty(Convert.ToString(searchModel.reportSearch.OrderItemId)) ? 0 : Convert.ToInt32(searchModel.reportSearch.OrderItemId));
                sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.reportSearch.IsVisitStore == "yes" ? 1 : 0);
                sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.reportSearch.IsWantVistingStore == "yes" ? 1 : 0);

                /*Column 4 (5)*/
                sqlcmd.Parameters.AddWithValue("Customer_EmailID", searchModel.reportSearch.CustomerEmailID);
                sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.reportSearch.CustomerMobileNo) ? "" : searchModel.reportSearch.CustomerMobileNo);
                sqlcmd.Parameters.AddWithValue("AssignTo", searchModel.reportSearch.AssignTo);
                sqlcmd.Parameters.AddWithValue("StoreCodeORAddress", searchModel.reportSearch.StoreCodeORAddress);
                sqlcmd.Parameters.AddWithValue("WantToStoreCodeORAddress", searchModel.reportSearch.WantToStoreCodeORAddress);

                //Row - 2 and Column - 1  (5)
                sqlcmd.Parameters.AddWithValue("HaveClaim", searchModel.reportSearch.HaveClaim);
                sqlcmd.Parameters.AddWithValue("ClaimStatusId", searchModel.reportSearch.ClaimStatusId);
                sqlcmd.Parameters.AddWithValue("ClaimCategoryId", searchModel.reportSearch.ClaimCategoryId);
                sqlcmd.Parameters.AddWithValue("ClaimSubCategoryId", searchModel.reportSearch.ClaimSubCategoryId);
                sqlcmd.Parameters.AddWithValue("ClaimIssueTypeId", searchModel.reportSearch.ClaimIssueTypeId);

                //Row - 2 and Column - 2  (4)
                sqlcmd.Parameters.AddWithValue("HaveTask", searchModel.reportSearch.HaveTask);
                sqlcmd.Parameters.AddWithValue("TaskStatus_Id", searchModel.reportSearch.TaskStatusId);
                sqlcmd.Parameters.AddWithValue("TaskDepartment_Id", searchModel.reportSearch.TaskDepartment_Id);
                sqlcmd.Parameters.AddWithValue("TaskFunction_Id", searchModel.reportSearch.TaskFunction_Id);
                //     sqlcmd.Parameters.AddWithValue("Task_Priority", searchModel.reportSearch.TaskPriority);

                sqlcmd.Parameters.AddWithValue("CurrentUserId", searchModel.curentUserId);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("Assignto_IDs", searchModel.reportSearch.AssignTo.ToString());
                sqlcmd.Parameters.AddWithValue("Brand_IDs", searchModel.reportSearch.BrandID.ToString());

                sqlcmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        // resultCount = Convert.ToInt32(ds.Tables[0].Rows[0]["RowCount"]);

                        objSearchResult = ds.Tables[0].AsEnumerable().Select(r => new SearchResponseReport()
                        {
                            ticketID = Convert.ToInt32(r.Field<object>("TicketID")),
                            ticketStatus = Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(r.Field<object>("StatusID"))),
                            Message = r.Field<object>("TicketDescription") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TicketDescription")),
                            Category = r.Field<object>("CategoryName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CategoryName")),
                            subCategory = r.Field<object>("SubCategoryName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SubCategoryName")),
                            IssueType = r.Field<object>("IssueTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IssueTypeName")),
                            Priority = r.Field<object>("PriortyName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriortyName")),
                            Assignee = r.Field<object>("AssignedName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AssignedName")),
                            CreatedOn = r.Field<object>("CreatedOn") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedOn")),
                            createdBy = r.Field<object>("CreatedByName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedByName")),
                            createdago = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("CreatedDate")), "CreatedSpan"),
                            assignedTo = r.Field<object>("AssignedName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AssignedName")),
                            assignedago = r.Field<object>("AssignedDate") == System.DBNull.Value ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("AssignedDate")), "AssignedSpan"),
                            updatedBy = r.Field<object>("ModifyByName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModifyByName")),
                            updatedago = r.Field<object>("ModifiedDate") == System.DBNull.Value ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("ModifiedDate")), "ModifiedSpan"),

                            responseTimeRemainingBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityRespond") == System.DBNull.Value) ?
                           string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "RespondTimeRemainingSpan"),
                            responseOverdueBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityRespond") == System.DBNull.Value) ?
                           string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResponseOverDueSpan"),

                            resolutionOverdueBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityResolve") == System.DBNull.Value) ?
                           string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityResolve")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResolutionOverDueSpan"),

                            TaskStatus = r.Field<object>("TaskDetails") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TaskDetails")),
                            ClaimStatus = r.Field<object>("ClaimDetails") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ClaimDetails")),
                            TicketCommentCount = r.Field<object>("ClaimDetails") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TicketComments")),
                            isEscalation = r.Field<object>("IsEscalated") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsEscalated")),
                            ticketSourceType = Convert.ToString(r.Field<object>("TicketSourceType")),
                            IsReassigned = Convert.ToBoolean(r.Field<object>("IsReassigned")),
                            ticketSourceTypeID = Convert.ToInt16(r.Field<object>("TicketSourceTypeID"))
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
                if (ds != null) ds.Dispose(); 
            }
            return objSearchResult;
        }

        public string setCreationdetails(string time, string ColName)
        {
            string timespan = string.Empty;
            DateTime now = DateTime.Now;
            TimeSpan diff = new TimeSpan();
            string[] PriorityArr = null;

            try
            {
                if (ColName == "CreatedSpan" || ColName == "ModifiedSpan" || ColName == "AssignedSpan")
                {
                    diff = now - Convert.ToDateTime(time);
                    timespan = CalculateSpan(diff) + " ago";

                }
                else if (ColName == "RespondTimeRemainingSpan")
                {
                    PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });

                    switch (PriorityArr[1])
                    {
                        case "D":
                            diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddDays(Convert.ToDouble(PriorityArr[0]))) - now;

                            break;

                        case "H":
                            diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddHours(Convert.ToDouble(PriorityArr[0]))) - now;

                            break;

                        case "M":
                            diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddMinutes(Convert.ToDouble(PriorityArr[0]))) - now;

                            break;

                    }
                    timespan = CalculateSpan(diff);
                }
                else if (ColName == "ResponseOverDueSpan" || ColName == "ResolutionOverDueSpan")
                {
                    PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });

                    switch (PriorityArr[1])
                    {
                        case "D":
                            diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddDays(Convert.ToDouble(PriorityArr[0])));

                            break;

                        case "H":
                            diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddHours(Convert.ToDouble(PriorityArr[0])));

                            break;

                        case "M":
                            diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddMinutes(Convert.ToDouble(PriorityArr[0])));

                            break;

                    }

                    timespan = CalculateSpan(diff);
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                if (PriorityArr != null && PriorityArr.Length > 0)
                    Array.Clear(PriorityArr, 0, PriorityArr.Length);
            }
            return timespan;
        }

        public string CalculateSpan(TimeSpan ts)
        {
            string span = string.Empty;

            if (Math.Abs(ts.Days) > 0)
            {
                span = Convert.ToString(Math.Abs(ts.Days)) + " Days";
            }
            else if (Math.Abs(ts.Hours) > 0)
            {
                span = Convert.ToString(Math.Abs(ts.Hours)) + " Hours";
            }
            else if (Math.Abs(ts.Minutes) > 0)
            {
                span = Convert.ToString(Math.Abs(ts.Minutes)) + " Minutes";
            }
            else if (Math.Abs(ts.Seconds) > 0)
            {
                span = Convert.ToString(Math.Abs(ts.Seconds)) + " Seconds";
            }

            return span;
        }

        public string DownloadDefaultReport(DefaultReportRequestModel defaultReportRequestModel, int curentUserId, int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();

            List<SearchResponse> objSearchResult = new List<SearchResponse>();
            if (!string.IsNullOrEmpty(defaultReportRequestModel.Ticket_CreatedFrom) && defaultReportRequestModel.Ticket_CreatedFrom.Equals("Invalid date"))
            {
                defaultReportRequestModel.Ticket_CreatedFrom = "";
            }
            if (!string.IsNullOrEmpty(defaultReportRequestModel.Ticket_CreatedTo) && defaultReportRequestModel.Ticket_CreatedTo.Equals("Invalid date"))
            {
                defaultReportRequestModel.Ticket_CreatedTo = "";
            }
            if (!string.IsNullOrEmpty(defaultReportRequestModel.Ticket_CloseFrom) && defaultReportRequestModel.Ticket_CloseFrom.Equals("Invalid date"))
            {
                defaultReportRequestModel.Ticket_CloseFrom = "";
            }
            if (!string.IsNullOrEmpty(defaultReportRequestModel.Ticket_CloseTo) && defaultReportRequestModel.Ticket_CloseTo.Equals("Invalid date"))
            {
                defaultReportRequestModel.Ticket_CloseTo = "";
            }

            List<string> CountList = new List<string>();
            string csv = String.Empty;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn = Db.Connection;
                }
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                ////1. Total Ticket Created
                if (defaultReportRequestModel.ReportTypeID == 1)
                {
                    sqlcmd.CommandText = "sp_DefaultReport_TotalTicketCreated";

                    sqlcmd.Parameters.AddWithValue("TicketCreatedFrom", defaultReportRequestModel.Ticket_CreatedFrom);
                    sqlcmd.Parameters.AddWithValue("TicketCreatedTo", defaultReportRequestModel.Ticket_CreatedTo);
                    sqlcmd.Parameters.AddWithValue("TicketSourceIDs", defaultReportRequestModel.Ticket_SourceIDs);

                }
                ////2. Total Open Tickets //5. Escalated Tickets //6.Re-Assigned Tickets
                //7.Re-Opened Tickets  //4. Ticket Count by Associates
                else if (defaultReportRequestModel.ReportTypeID == 2 || defaultReportRequestModel.ReportTypeID == 4
                    || defaultReportRequestModel.ReportTypeID == 5 || defaultReportRequestModel.ReportTypeID == 6
                    || defaultReportRequestModel.ReportTypeID == 7)
                {
                    sqlcmd.CommandText = "sp_DefaultReport_TicketsByStatus";

                    sqlcmd.Parameters.AddWithValue("Ticket_StatusIDs", defaultReportRequestModel.Ticket_StatusIDs);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", defaultReportRequestModel.Ticket_StatusID);
                    sqlcmd.Parameters.AddWithValue("TicketCreatedFrom", defaultReportRequestModel.Ticket_CreatedFrom);
                    sqlcmd.Parameters.AddWithValue("TicketCreatedTo", defaultReportRequestModel.Ticket_CreatedTo);
                    sqlcmd.Parameters.AddWithValue("TicketSourceIDs", defaultReportRequestModel.Ticket_SourceIDs);
                    sqlcmd.Parameters.AddWithValue("TicketAssignIDs", string.IsNullOrEmpty(defaultReportRequestModel.Ticket_AssignIDs) ? "" : defaultReportRequestModel.Ticket_AssignIDs);
                    sqlcmd.Parameters.AddWithValue("ReportTypeID", defaultReportRequestModel.ReportTypeID);
                }
                //3. 3.	Total Closed Ticket
                else if (defaultReportRequestModel.ReportTypeID == 3)
                {
                    sqlcmd.CommandText = "sp_DefaultReport_TotalTicketsByCloseDate";

                    sqlcmd.Parameters.AddWithValue("TicketClosedFrom", defaultReportRequestModel.Ticket_CloseFrom);
                    sqlcmd.Parameters.AddWithValue("TicketClosedTo", defaultReportRequestModel.Ticket_CloseTo);

                    sqlcmd.Parameters.AddWithValue("TicketCreatedFrom", defaultReportRequestModel.Ticket_CreatedFrom);
                    sqlcmd.Parameters.AddWithValue("TicketCreatedTo", defaultReportRequestModel.Ticket_CreatedTo);

                    sqlcmd.Parameters.AddWithValue("TicketSourceIDs", defaultReportRequestModel.Ticket_SourceIDs);
                }

                sqlcmd.Parameters.AddWithValue("Tenant_ID", TenantID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        csv = CommonService.DataTableToCsv(ds.Tables[0]);
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
                if (ds != null) ds.Dispose(); 
            }
            return csv;
        }

        public bool SendReportMail(string EmailID, string FilePath, int TenantID, int UserMasterID)
        {
            MySqlCommand cmd = new MySqlCommand();

            bool result = false;
            int resultcount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn = Db.Connection;
                }
                cmd.Connection = conn;

                cmd.CommandText = "SP_SendReportMail";

                cmd.Parameters.AddWithValue("_EmailID", EmailID);
                cmd.Parameters.AddWithValue("_FilePath", FilePath);
                cmd.Parameters.AddWithValue("_TenantID", TenantID);
                cmd.Parameters.AddWithValue("_UserMasterID", UserMasterID);

                cmd.CommandType = CommandType.StoredProcedure;

                resultcount = cmd.ExecuteNonQuery();

                if(resultcount == 1)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            
            return result;
        }

        #endregion



    }
}
