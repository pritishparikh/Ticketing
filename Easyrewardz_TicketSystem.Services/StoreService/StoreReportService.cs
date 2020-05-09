using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
        public int GetStoreReportSearch(StoreReportModel searchModel, List<StoreUserListing> StoreUserList)
        {

            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;

            string UserList = "";
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // get all users if created by =0 and assigned user =0

                if (StoreUserList.Count > 0)
                {
                    UserList = string.Join(',', StoreUserList.AsEnumerable().Select(x => x.UserID).ToList());
                }

                if (searchModel.ActiveTabId.Equals(1))
                {

                    if (searchModel.TaskCreatedBy.Equals("0"))
                    {
                        searchModel.TaskCreatedBy = UserList;
                    }

                    if (searchModel.TaskAssignedId.Equals("0"))
                    {
                        searchModel.TaskAssignedId = UserList;
                    }
                }

                else if (searchModel.ActiveTabId.Equals(2))
                {
                    if (searchModel.ClaimCreatedBy.Equals("0"))
                    {
                        searchModel.ClaimCreatedBy = UserList;
                    }

                    if (searchModel.ClaimAssignedId.Equals("0"))
                    {
                        searchModel.ClaimAssignedId = UserList;
                    }
                }

                else
                {
                    if (searchModel.CampaignAssignedIds.Equals("0"))
                    {
                        searchModel.CampaignAssignedIds = UserList;
                    }
                }



                //--------


                cmd = new MySqlCommand("SP_GetStoreReportCount", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", searchModel.TenantID);
                cmd.Parameters.AddWithValue("@_ActiveTabID", searchModel.ActiveTabId);

                /*------------------ TASK PARAMETERS ------------------------------*/

                cmd.Parameters.AddWithValue("@_TaskTitle", string.IsNullOrEmpty(searchModel.TaskTitle) ? "" : searchModel.TaskTitle);
                cmd.Parameters.AddWithValue("@_TaskStatus", string.IsNullOrEmpty(searchModel.TaskStatus) ? "" : searchModel.TaskStatus);
                cmd.Parameters.AddWithValue("@_IsTaskWithTicket", Convert.ToInt16(searchModel.IsTaskWithTicket));
                cmd.Parameters.AddWithValue("@_TaskTicketID", searchModel.TaskTicketID == null ? 0 : searchModel.TaskTicketID);
                cmd.Parameters.AddWithValue("@_DepartmentIds", string.IsNullOrEmpty(searchModel.DepartmentIds) ? "" : searchModel.DepartmentIds);
                cmd.Parameters.AddWithValue("@_FunctionIds", string.IsNullOrEmpty(searchModel.FunctionIds) ? "" : searchModel.FunctionIds);
                cmd.Parameters.AddWithValue("@_PriorityIds", string.IsNullOrEmpty(searchModel.PriorityIds) ? "" : searchModel.PriorityIds);
                cmd.Parameters.AddWithValue("@_IsTaskWithClaim", Convert.ToInt16(searchModel.IsTaskWithClaim));
                cmd.Parameters.AddWithValue("@_TaskClaimID", searchModel.TaskClaimID == null ? 0 : searchModel.TaskClaimID);
                cmd.Parameters.AddWithValue("@_TaskCreatedDate", string.IsNullOrEmpty(searchModel.TaskCreatedDate) ? "" : searchModel.TaskCreatedDate);
                cmd.Parameters.AddWithValue("@_TaskCreatedBy", string.IsNullOrEmpty(searchModel.TaskCreatedBy) ? "" : searchModel.TaskCreatedBy);
                cmd.Parameters.AddWithValue("@_TaskAssignedId", string.IsNullOrEmpty(searchModel.TaskAssignedId) ? "" : searchModel.TaskAssignedId);

                /*------------------ ENDS HERE-------------------------------*/

                /*------------------ CLAIM  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_ClaimID", searchModel.ClaimID);
                cmd.Parameters.AddWithValue("@_ClaimStatus", string.IsNullOrEmpty(searchModel.ClaimStatus) ? "" : searchModel.ClaimStatus);
                cmd.Parameters.AddWithValue("@_IsClaimWithTicket", Convert.ToInt16(searchModel.IsClaimWithTicket));
                cmd.Parameters.AddWithValue("@_ClaimTicketID", searchModel.ClaimTicketID == null ? 0 : searchModel.ClaimTicketID);
                cmd.Parameters.AddWithValue("@_ClaimCategoryIds", string.IsNullOrEmpty(searchModel.ClaimCategoryIds) ? "" : searchModel.ClaimCategoryIds);
                cmd.Parameters.AddWithValue("@_ClaimSubCategoryIds", string.IsNullOrEmpty(searchModel.ClaimSubCategoryIds) ? "" : searchModel.ClaimSubCategoryIds);
                cmd.Parameters.AddWithValue("@_ClaimIssuetypeIds", string.IsNullOrEmpty(searchModel.ClaimIssuetypeIds) ? "" : searchModel.ClaimIssuetypeIds);
                cmd.Parameters.AddWithValue("@_IsClaimWithTask", Convert.ToInt16(searchModel.IsClaimWithTask));
                cmd.Parameters.AddWithValue("@_ClaimTaskID", searchModel.ClaimTaskID == null ? 0 : searchModel.ClaimTaskID);
                cmd.Parameters.AddWithValue("@_ClaimCreatedDate", string.IsNullOrEmpty(searchModel.ClaimCreatedDate) ? "" : searchModel.ClaimCreatedDate);
                cmd.Parameters.AddWithValue("@_ClaimCreatedBy", string.IsNullOrEmpty(searchModel.ClaimCreatedBy) ? "" : searchModel.ClaimCreatedBy);
                cmd.Parameters.AddWithValue("@_ClaimAssignedId", string.IsNullOrEmpty(searchModel.ClaimAssignedId) ? "" : searchModel.ClaimAssignedId);



                /*------------------ CAMPAIGN  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_CampaignName", string.IsNullOrEmpty(searchModel.CampaignName) ? "" : searchModel.CampaignName);
                cmd.Parameters.AddWithValue("@_CampaignAssignedId", string.IsNullOrEmpty(searchModel.CampaignAssignedIds) ? "" : searchModel.CampaignAssignedIds);
                cmd.Parameters.AddWithValue("@_CampaignStartDate", string.IsNullOrEmpty(searchModel.CampaignStartDate) ? "" : searchModel.CampaignStartDate);
                cmd.Parameters.AddWithValue("@_CampaignEndDate", string.IsNullOrEmpty(searchModel.CampaignEndDate) ? "" : searchModel.CampaignEndDate);
                cmd.Parameters.AddWithValue("@_CampaignStatusids", string.IsNullOrEmpty(searchModel.CampaignStatusids) ? "" : searchModel.CampaignStatusids);

                /*------------------ ENDS HERE-------------------------------*/


                cmd.CommandType = CommandType.StoredProcedure;

                resultCount = Convert.ToInt32(cmd.ExecuteScalar());


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
        /// Download the Report
        /// </summary>
        /// <param name="ScdeduleID"></param>
        ///  <param name="UserId"></param>
        ///   <param name="TenantId"></param>
        /// <returns></returns>
        public string DownloadStoreReportSearch(int SchedulerID, int UserID, int TenantID, List<StoreUserListing> StoreUserList)
        {
            MySqlCommand cmd = new MySqlCommand();
            string SearchInputParams = string.Empty;
            StoreReportModel SearchModel = new StoreReportModel();

            SearchStoreResponseReport ReportDownloadList = new SearchStoreResponseReport();
            List<SearchStoreTaskReportResponse> TaskReport = new List<SearchStoreTaskReportResponse>();
            List<SearchStoreClaimReportResponse> ClaimReport = new List<SearchStoreClaimReportResponse>();
            List<SearchStoreCampaignReportResponse> CampaignReport = new List<SearchStoreCampaignReportResponse>();

            string CSVReport = string.Empty;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_DownloadStoreReportSearch", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Schedule_ID", SchedulerID);

                cmd.CommandType = CommandType.StoredProcedure;

                SearchInputParams = Convert.ToString(cmd.ExecuteScalar()); 

                if (!string.IsNullOrEmpty(SearchInputParams))
                {
                    SearchModel = JsonConvert.DeserializeObject<StoreReportModel>(SearchInputParams);
                    SearchModel.TenantID = TenantID;
                    ReportDownloadList = GetStoreReportSearchList(SearchModel, StoreUserList);

                    if (ReportDownloadList != null)
                    {
                        if (SearchModel.ActiveTabId.Equals(1))
                        {
                            TaskReport = ReportDownloadList.TaskReport;
                            CSVReport = TaskReport!=null &&  TaskReport.Count > 0 ? CommonService.ListToCSV(TaskReport) : string.Empty;
                        }
                        else if (SearchModel.ActiveTabId.Equals(2))
                        {
                            ClaimReport = ReportDownloadList.ClaimReport;
                            CSVReport = ClaimReport != null && ClaimReport.Count > 0 ? CommonService.ListToCSV(ClaimReport) : string.Empty;
                        }
                        else
                        {
                            CampaignReport = ReportDownloadList.CampaignReport;
                            CSVReport = CampaignReport != null && CampaignReport.Count > 0 ? CommonService.ListToCSV(CampaignReport) : string.Empty;
                        }
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return CSVReport;
        }



        /// <summary>
        /// Get List of reports for Download
        /// </summary>
        /// <param name="StoreReportModel"></param>
        /// <returns></returns>
        public SearchStoreResponseReport GetStoreReportSearchList(StoreReportModel searchModel, List<StoreUserListing> StoreUserList)
        {
            List<string> ReportDownloadList = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();

            SearchStoreResponseReport objSearchResult = new SearchStoreResponseReport();
            List<SearchStoreTaskReportResponse> TaskReport = new List<SearchStoreTaskReportResponse>();
            List<SearchStoreClaimReportResponse> ClaimReport = new List<SearchStoreClaimReportResponse>();
            List<SearchStoreCampaignReportResponse> CampaignReport = new List<SearchStoreCampaignReportResponse>();

            string UserList = "";
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }



                // get all users if created by =0 and assigned user =0

                if (StoreUserList.Count > 0)
                {
                    UserList = string.Join(',', StoreUserList.AsEnumerable().Select(x => x.UserID).ToList());
                }

                if (searchModel.ActiveTabId.Equals(1))
                {

                    if (searchModel.TaskCreatedBy.Equals("0"))
                    {
                        searchModel.TaskCreatedBy = UserList;
                    }

                    if (searchModel.TaskAssignedId.Equals("0"))
                    {
                        searchModel.TaskAssignedId = UserList;
                    }
                }

               else if (searchModel.ActiveTabId.Equals(2))
                {
                    if (searchModel.ClaimCreatedBy.Equals("0"))
                    {
                        searchModel.ClaimCreatedBy = UserList;
                    }

                    if (searchModel.ClaimAssignedId.Equals("0"))
                    {
                        searchModel.ClaimAssignedId = UserList;
                    }
                }

                else
                {
                    if (searchModel.CampaignAssignedIds.Equals("0"))
                    {
                        searchModel.CampaignAssignedIds = UserList;
                    }
                }



                //--------

                cmd = new MySqlCommand("SP_ScheduleStoreReportForDownload", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", searchModel.TenantID);
                cmd.Parameters.AddWithValue("@_ActiveTabID", searchModel.ActiveTabId);

                /*------------------ TASK PARAMETERS ------------------------------*/

                cmd.Parameters.AddWithValue("@_TaskTitle", string.IsNullOrEmpty(searchModel.TaskTitle) ? "" : searchModel.TaskTitle);
                cmd.Parameters.AddWithValue("@_TaskStatus", string.IsNullOrEmpty(searchModel.TaskStatus) ? "" : searchModel.TaskStatus);
                cmd.Parameters.AddWithValue("@_IsTaskWithTicket", Convert.ToInt16(searchModel.IsTaskWithTicket));
                cmd.Parameters.AddWithValue("@_TaskTicketID", searchModel.TaskTicketID == null ? 0 : searchModel.TaskTicketID);
                cmd.Parameters.AddWithValue("@_DepartmentIds", string.IsNullOrEmpty(searchModel.DepartmentIds) ? "" : searchModel.DepartmentIds);
                cmd.Parameters.AddWithValue("@_FunctionIds", string.IsNullOrEmpty(searchModel.FunctionIds) ? "" : searchModel.FunctionIds);
                cmd.Parameters.AddWithValue("@_PriorityIds", string.IsNullOrEmpty(searchModel.PriorityIds) ? "" : searchModel.PriorityIds);
                cmd.Parameters.AddWithValue("@_IsTaskWithClaim", Convert.ToInt16(searchModel.IsTaskWithClaim));
                cmd.Parameters.AddWithValue("@_TaskClaimID", searchModel.TaskClaimID == null ? 0 : searchModel.TaskClaimID);
                cmd.Parameters.AddWithValue("@_TaskCreatedDate", string.IsNullOrEmpty(searchModel.TaskCreatedDate) ? "" : searchModel.TaskCreatedDate);
                cmd.Parameters.AddWithValue("@_TaskCreatedBy", string.IsNullOrEmpty(searchModel.TaskCreatedBy) ? "" : searchModel.TaskCreatedBy);
                cmd.Parameters.AddWithValue("@_TaskAssignedId", string.IsNullOrEmpty(searchModel.TaskAssignedId) ? "" : searchModel.TaskAssignedId);

                /*------------------ ENDS HERE-------------------------------*/

                /*------------------ CLAIM  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_ClaimID", searchModel.ClaimID);
                cmd.Parameters.AddWithValue("@_ClaimStatus", string.IsNullOrEmpty(searchModel.ClaimStatus) ? "" : searchModel.ClaimStatus);
                cmd.Parameters.AddWithValue("@_IsClaimWithTicket", Convert.ToInt16(searchModel.IsClaimWithTicket));
                cmd.Parameters.AddWithValue("@_ClaimTicketID", searchModel.ClaimTicketID == null ? 0 : searchModel.ClaimTicketID);
                cmd.Parameters.AddWithValue("@_ClaimCategoryIds", string.IsNullOrEmpty(searchModel.ClaimCategoryIds) ? "" : searchModel.ClaimCategoryIds);
                cmd.Parameters.AddWithValue("@_ClaimSubCategoryIds", string.IsNullOrEmpty(searchModel.ClaimSubCategoryIds) ? "" : searchModel.ClaimSubCategoryIds);
                cmd.Parameters.AddWithValue("@_ClaimIssuetypeIds", string.IsNullOrEmpty(searchModel.ClaimIssuetypeIds) ? "" : searchModel.ClaimIssuetypeIds);
                cmd.Parameters.AddWithValue("@_IsClaimWithTask", Convert.ToInt16(searchModel.IsClaimWithTask));
                cmd.Parameters.AddWithValue("@_ClaimTaskID", searchModel.ClaimTaskID == null ? 0 : searchModel.ClaimTaskID);
                cmd.Parameters.AddWithValue("@_ClaimCreatedDate", string.IsNullOrEmpty(searchModel.ClaimCreatedDate) ? "" : searchModel.ClaimCreatedDate);
                cmd.Parameters.AddWithValue("@_ClaimCreatedBy", string.IsNullOrEmpty(searchModel.ClaimCreatedBy) ? "" : searchModel.ClaimCreatedBy);
                cmd.Parameters.AddWithValue("@_ClaimAssignedId", string.IsNullOrEmpty(searchModel.ClaimAssignedId) ? "" : searchModel.ClaimAssignedId);



                /*------------------ CAMPAIGN  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_CampaignName", string.IsNullOrEmpty(searchModel.CampaignName) ? "" : searchModel.CampaignName);
                cmd.Parameters.AddWithValue("@_CampaignAssignedId", string.IsNullOrEmpty(searchModel.CampaignAssignedIds) ? "" : searchModel.CampaignAssignedIds);
                cmd.Parameters.AddWithValue("@_CampaignStartDate", string.IsNullOrEmpty(searchModel.CampaignStartDate) ? "" : searchModel.CampaignStartDate);
                cmd.Parameters.AddWithValue("@_CampaignEndDate", string.IsNullOrEmpty(searchModel.CampaignEndDate) ? "" : searchModel.CampaignEndDate);
                cmd.Parameters.AddWithValue("@_CampaignStatusids", string.IsNullOrEmpty(searchModel.CampaignStatusids) ? "" : searchModel.CampaignStatusids);

                /*------------------ ENDS HERE-------------------------------*/


                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (searchModel.ActiveTabId.Equals(1))// task mapping
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                SearchStoreTaskReportResponse obj = new SearchStoreTaskReportResponse()
                                {
                                    TaskID = Convert.ToInt32(dr["TaskID"]),
                                    TicketID = dr["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TicketID"]),
                                    TicketDescription = dr["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketDescription"]),
                                    TaskTitle = dr["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskTitle"]),
                                    TaskDescription = dr["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskDescription"]),
                                    DepartmentId = dr["DepartmentId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DepartmentId"]),
                                    DepartmentName = dr["DepartmentName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                                    FunctionID = dr["FunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["FunctionID"]),
                                    FunctionName = dr["FunctionName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["FunctionName"]),
                                    PriorityID = dr["PriorityID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PriorityID"]),
                                    PriorityName = dr["PriorityName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PriorityName"]),
                                    TaskEndTime = dr["TaskEndTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskEndTime"]),
                                    TaskStatus = dr["TaskStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskStatus"]),
                                    CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                    CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                    CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                    ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifiedBy"]),
                                    ModifiedByName = dr["ModifiedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedByName"]),
                                    ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedDate"]),
                                    IsActive = dr["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IsActive"]),

                                };

                                TaskReport.Add(obj);
                            }

                            objSearchResult.TaskReport = TaskReport;
                        }
                        else if (searchModel.ActiveTabId.Equals(2))// claim mapping
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                SearchStoreClaimReportResponse obj = new SearchStoreClaimReportResponse()
                                {
                                    ClaimID = Convert.ToInt32(dr["ClaimID"]),
                                    ClaimTitle = dr["ClaimTitle"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimTitle"]),
                                    ClaimDescription = dr["ClaimDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimDescription"]),
                                    BrandID = dr["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BrandID"]),
                                    BrandName = dr["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["BrandName"]),
                                    CategoryID = dr["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CategoryID"]),
                                    CategoryName = dr["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CategoryName"]),
                                    SubCategoryID = dr["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SubCategoryID"]),
                                    SubCategoryName = dr["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubCategoryName"]),

                                    IssueTypeID = dr["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IssueTypeID"]),
                                    IssueTypeName = dr["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IssueTypeName"]),
                                    PriorityID = dr["PriorityID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PriorityID"]),
                                    PriorityName = dr["PriorityName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PriorityName"]),
                                    CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                                    CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                    OrderMasterID = dr["OrderMasterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrderMasterID"]),
                                    OrderNo = dr["OrderNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrderNumber"]),
                                    ClaimPercent = dr["ClaimPercent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimPercent"]),
                                    ClaimAssignedID = dr["AssignedID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AssignedID"]),
                                    AssignedToName = dr["AssignedToName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedToName"]),
                                    ClaimStatus = dr["ClaimStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimStatus"]),

                                    IsActive = dr["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IsActive"]),
                                    ClaimApproved = dr["ClaimApproved"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimApproved"]),
                                    ClaimRejected = dr["ClaimRejected"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimRejected"]),
                                    CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                    CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                    CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                    ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifiedBy"]),
                                    ModifiedByName = dr["ModifiedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedByName"]),
                                    ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedDate"]),
                                    IsClaimEscalated = dr["IsClaimEscalated"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IsClaimEscalated"]),
                                    IsCustomerResponseDone = dr["IsCustomerResponseDone"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IsCustomerResponseDone"]),
                                    CustomerResponsedOn = dr["CustomerResponsedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerResponsedOn"]),
                                    FinalClaimPercent = dr["FinalClaimPercent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["FinalClaimPercent"]),
                                    TicketDescription = dr["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketDescription"]),
                                    TaskDescription = dr["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskDescription"]),


                                };
                                ClaimReport.Add(obj);
                            }
                            objSearchResult.ClaimReport = ClaimReport;
                        }
                        else// campaign mapping
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                SearchStoreCampaignReportResponse obj = new SearchStoreCampaignReportResponse()
                                {
                                    CampaignCustomerID = dr["CampaignCustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CampaignCustomerID"]),
                                    CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                                    CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                    CampaignTypeID = dr["CampaignTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CampaignTypeID"]),
                                    CampaignName = dr["CampaignName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignName"]),
                                    CampaignTypeDate = dr["CampaignTypeDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignTypeDate"]),
                                    CallReScheduledTo = dr["CallReScheduledTo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CallReScheduledTo"]),
                                    CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                    CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),

                                    CampaignStatus = dr["CampaignStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignStatus"]),
                                    AssignedTo = dr["AssignedTo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AssignedTo"]),
                                    AssignedToName = dr["AssignedToName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedToName"]),
                                    Response = dr["Response"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Response"]),
                                    NoOfTimesNotContacted = dr["NoOfTimesNotContacted"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfTimesNotContacted"]),

                                };
                                CampaignReport.Add(obj);
                            }

                            objSearchResult.CampaignReport = CampaignReport;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return objSearchResult;
        }


        /// <summary>
        /// Check If Report Name Exists
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="ReportName"></param>
        /// <returns></returns>
        public bool CheckIfReportNameExists(int ReportID, string ReportName, int TenantID)
        {


            bool Isexists = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_CheckIfStoreReportNameAlreadyExsists", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Report_ID", ReportID);
                cmd.Parameters.AddWithValue("@Report_Name", string.IsNullOrEmpty(ReportName) ? "" : ReportName.ToLower());
                

                cmd.CommandType = CommandType.StoredProcedure;

                Isexists = Convert.ToBoolean(Convert.ToInt32(cmd.ExecuteScalar()));
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
            return Isexists;
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
                DateTime date = new DateTime();
              
                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

                       for(int i=0; i< ds.Tables[0].Rows.Count; i++ )
                        {
                            ReportModel Report = new ReportModel();
                            Report.ReportID = ds.Tables[0].Rows[i]["ReportID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ReportID"]);
                            Report.ScheduleID = ds.Tables[0].Rows[i]["ScheduleID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleID"]);
                            Report.ScheduleType = ds.Tables[0].Rows[i]["ScheduleType"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleType"]);
                            Report.ReportSearchParams = ds.Tables[0].Rows[i]["StoreReportSearchParams"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreReportSearchParams"]);
                            Report.IsDownloaded = ds.Tables[0].Rows[i]["IsDownloaded"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IsDownloaded"]);
                            Report.ReportName = ds.Tables[0].Rows[i]["ReportName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReportName"]);
                            Report.ReportStatus = ds.Tables[0].Rows[i]["ReportStatus"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReportStatus"]);
                            Report.ScheduleStatus = ds.Tables[0].Rows[i]["ScheduleStatus"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ScheduleStatus"]);
                            Report.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]);
                            Report.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                            Report.ModifiedBy = ds.Tables[0].Rows[i]["UpdatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UpdatedBy"]);
                            Report.ScheduleFor = ds.Tables[0].Rows[i]["ScheduleFor"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ScheduleFor"]);
                            Report.ScheduleTime = ds.Tables[0].Rows[i]["ScheduleTime"] == DBNull.Value  || Convert.ToString(ds.Tables[0].Rows[i]["ScheduleTime"]) == "" 
                                ? date : new DateTime().Add(TimeSpan.Parse(Convert.ToString(ds.Tables[0].Rows[i]["ScheduleTime"])));
                            Report.IsDaily = ds.Tables[0].Rows[i]["IsDaily"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDaily"]);
                            Report.NoOfDay = ds.Tables[0].Rows[i]["NoOfDay"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfDay"]);
                            Report.IsWeekly = ds.Tables[0].Rows[i]["IsWeekly"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsWeekly"]);
                            Report.NoOfWeek = ds.Tables[0].Rows[i]["NoOfWeek"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfWeek"]);
                            Report.DayIds = ds.Tables[0].Rows[i]["DayIds"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DayIds"]);
                            Report.IsDailyForMonth = ds.Tables[0].Rows[i]["IsDailyForMonth"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDailyForMonth"]);
                            Report.NoOfDaysForMonth = ds.Tables[0].Rows[i]["NoOfDaysForMonth"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfDaysForMonth"]);
                            Report.NoOfMonthForMonth = ds.Tables[0].Rows[i]["NoOfMonthForMonth"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfMonthForMonth"]);
                            Report.IsWeeklyForMonth = ds.Tables[0].Rows[i]["IsWeeklyForMonth"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsWeeklyForMonth"]);
                            Report.NoOfMonthForWeek = ds.Tables[0].Rows[i]["NoOfMonthForWeek"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfMonthForWeek"]);
                            Report.NoOfWeekForWeek = ds.Tables[0].Rows[i]["NoOfWeekForWeek"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfWeekForWeek"]);
                            Report.NameOfDayForYear = ds.Tables[0].Rows[i]["NameOfDayForYear"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NameOfDayForYear"]);
                            Report.NameOfDayForWeek = ds.Tables[0].Rows[i]["NameOfDayForWeek"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NameOfDayForWeek"]);
                            Report.NoOfWeekForYear = ds.Tables[0].Rows[i]["NoOfWeekForYear"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfWeekForYear"]);
                            Report.NameOfMonthForYear = ds.Tables[0].Rows[i]["NameOfMonthForYear"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NameOfMonthForYear"]);
                            Report.IsDailyForYear = ds.Tables[0].Rows[i]["IsDailyForYear"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDailyForYear"]);
                            Report.NameOfMonthForDailyYear = ds.Tables[0].Rows[i]["NameOfMonthForDailyYear"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NameOfMonthForDailyYear"]);
                            Report.NoOfDayForDailyYear = ds.Tables[0].Rows[i]["NoOfDayForDailyYear"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfDayForDailyYear"]);

                            objReportLst.Add(Report);
                        }


                        #region old store report mapping mapping 
                        /*

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
                             ScheduleTime = r.Field<object>("ScheduleTime") == System.DBNull.Value || Convert.ToString(r.Field<object>("ScheduleTime")) =="" ? default(DateTime?) : new DateTime().Add(TimeSpan.Parse(r.Field<object>("ScheduleTime").ToString())),
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


                     */

                        #endregion
                    }


                }
            }
            catch (Exception ex)
            {

                throw ex;
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
