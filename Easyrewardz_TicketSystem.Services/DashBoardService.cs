using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class DashBoardService : IDashBoard
    {
        public CultureInfo culture = CultureInfo.InvariantCulture;
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();

        public DashBoardService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Get DashBoard Count Data
        /// </summary>
        /// <param name="BrandID"></param>  
        /// <param name="UserID"></param>  
        /// <param name="fromdate"></param>  
        /// <param name="todate"></param>  
        /// <param name="TenantID"></param>  
        /// <returns></returns>
        public DashBoardDataModel GetDashBoardCountData(string BrandID, string UserID, string fromdate, string todate, int TenantID)
        {
            DataSet ds = new DataSet();
            DataSet Graphds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            DashBoardDataModel dashBoarddata = new DashBoardDataModel();
            DateTime date = new DateTime();
            TimeSpan ts = new TimeSpan();
            // DashBoardGraphModel dashBoardGraphdata = new DashBoardGraphModel();
            int TotalTickets = 0;
            int respondedTickets = 0; int UnrespondedTickets = 0; int TotalResponseTime = 0;
            int resolvedTickets = 0; int UnresolvedTickets = 0;int TotalResolutionTime = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;

                #region DashBoard Data
                MySqlCommand cmd1 = new MySqlCommand("SP_DashBoardList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_BrandID", string.IsNullOrEmpty(BrandID) ? "" : BrandID);
                cmd1.Parameters.AddWithValue("@User_ID", string.IsNullOrEmpty(UserID) ? "" : UserID);
                //cmd1.Parameters.AddWithValue("@Tenant_ID", 1);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@_FromDate", fromdate);
                cmd1.Parameters.AddWithValue("@_ToDate", todate);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0) //Resolution %
                    {
                        dashBoarddata.ResolutionPercentage = ds.Tables[0].Rows[0]["Resolution%"] != System.DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["Resolution%"]) : 0;
                    }

                    if (ds.Tables[1].Rows.Count > 0) //AllTicket 
                    {
                        dashBoarddata.All = ds.Tables[1].Rows[0]["AllTicket"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[1].Rows[0]["AllTicket"]) : 0;
                    }

                    if (ds.Tables[2].Rows.Count > 0) //OpenTicket 
                    {
                        dashBoarddata.Open = ds.Tables[2].Rows[0]["OpenTicket"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[2].Rows[0]["OpenTicket"]) : 0;
                    }

                    if (ds.Tables[3].Rows.Count > 0) //SLADue 
                    {
                        dashBoarddata.DueToday = ds.Tables[3].Rows[0]["SLADueCount"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[3].Rows[0]["SLADueCount"]) : 0;
                    }

                    if (ds.Tables[4].Rows.Count > 0) //SLAOverDue 
                    {
                        dashBoarddata.OverDue = ds.Tables[4].Rows[0]["SLAOverDueCount"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[4].Rows[0]["SLAOverDueCount"]) : 0;
                    }

                    if (ds.Tables[5].Rows.Count > 0) //TaskOpen 
                    {
                        dashBoarddata.TaskOpen = ds.Tables[5].Rows[0]["TaskOpen"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[5].Rows[0]["TaskOpen"]) : 0;
                    }

                    if (ds.Tables[6].Rows.Count > 0) //TaskClose 
                    {
                        dashBoarddata.TaskClose = ds.Tables[6].Rows[0]["TaskClose"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[6].Rows[0]["TaskClose"]) : 0;
                    }

                    if (ds.Tables[7].Rows.Count > 0) //ClaimOpen 
                    {
                        dashBoarddata.ClaimOpen = ds.Tables[7].Rows[0]["ClaimOpen"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[7].Rows[0]["ClaimOpen"]) : 0;
                    }

                    if (ds.Tables[8].Rows.Count > 0) //ClaimClose 
                    {
                        dashBoarddata.ClaimClose = ds.Tables[8].Rows[0]["ClaimClose"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[8].Rows[0]["ClaimClose"]) : 0;
                    }

                    //if (ds.Tables[9].Rows.Count > 0) //Response SLA  ----hardcoded for now-----
                    //{
                    //    dashBoarddata.ResponseRate = ds.Tables[9].Rows[0]["ResponseSLA"] != System.DBNull.Value ? Convert.ToString(ds.Tables[9].Rows[0]["ResponseSLA"]) + "%" : "";
                    //    dashBoarddata.isResponseSuccess = true;
                    //}


                    if ((ds.Tables[9].Rows.Count > 0)) //Resolution SLA
                    {
                        TotalTickets = ds.Tables[9].Rows[0]["TotalTickets"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[9].Rows[0]["TotalTickets"]) : 0;
                        respondedTickets = ds.Tables[9].Rows[0]["RespondedTickets"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[9].Rows[0]["RespondedTickets"]) : 0;
                        resolvedTickets = ds.Tables[9].Rows[0]["ResolvedTickets"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[9].Rows[0]["ResolvedTickets"]) : 0;

                        UnrespondedTickets = ds.Tables[9].Rows[0]["UnRespondedTickets"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[9].Rows[0]["UnRespondedTickets"]) : 0;
                        TotalResponseTime = ds.Tables[9].Rows[0]["TotalRespondTime"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[9].Rows[0]["TotalRespondTime"]) : 0;
                        UnresolvedTickets = ds.Tables[9].Rows[0]["UnresolvedTickets"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[9].Rows[0]["UnresolvedTickets"]) : 0;
                        TotalResolutionTime = ds.Tables[9].Rows[0]["TotalResolutionTime"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[9].Rows[0]["TotalResolutionTime"]) : 0;

                        if (TotalTickets > 0)
                        {
                            #region response SLA calculation

                            dashBoarddata.isResponseSuccess = respondedTickets > 0;
                            dashBoarddata.isResolutionSuccess = resolvedTickets > 0;

                            dashBoarddata.ResponseRate= ds.Tables[9].Rows[0]["ResponseRate"] != System.DBNull.Value ? Convert.ToString(ds.Tables[9].Rows[0]["ResponseRate"]) : "0%";
                            dashBoarddata.ResolutionRate = ds.Tables[9].Rows[0]["ResolutionRate"] != System.DBNull.Value ? Convert.ToString(ds.Tables[9].Rows[0]["ResolutionRate"]) : "0%";

                            if (TotalResponseTime > 0)
                            {
                                ts = date.AddHours(TotalResponseTime / UnrespondedTickets) - date;
                                dashBoarddata.AvgResponseTAT = ts.Days + "d " + ts.Hours + "h";
                            }
                            else
                            {
                                dashBoarddata.AvgResponseTAT = "0d 0h";

                            }
                            #endregion

                            #region resolution SLA calculation
                            date = new DateTime();
                            

                            if (TotalResolutionTime > 0)
                            {
                                ts = date.AddHours(TotalResolutionTime/ UnresolvedTickets) - date;
                                dashBoarddata.AvgResolutionTAT = ts.Days + "d " + ts.Hours + "h";
                            }
                            else
                            {
                                dashBoarddata.AvgResolutionTAT = "0d 0h";
                            
                            }
                            #endregion
                        }
                        else
                        {
                            dashBoarddata.isResponseSuccess = false;
                            dashBoarddata.ResponseRate = "0 %";
                            dashBoarddata.isResolutionSuccess = false;
                            dashBoarddata.ResolutionRate = "0 %";
                            dashBoarddata.AvgResponseTAT = "0d 0h";
                            dashBoarddata.AvgResolutionTAT = "0d 0h";
                        }
                    }
                }

                #endregion



            }
            catch (Exception)
            {
                throw;
            }
            return dashBoarddata;
        }

        /// <summary>
        /// Get DashBoard Graph data
        /// </summary>
        /// <param name="BrandID"></param>  
        /// <param name="UserID"></param>  
        /// <param name="fromdate"></param>  
        /// <param name="todate"></param>  
        /// <param name="TenantID"></param>  
        /// <returns></returns>
        public DashBoardGraphModel GetDashBoardGraphdata(string BrandID, string UserID, string fromdate, string todate, int TenantID)
        {

            DataSet Graphds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter da = new MySqlDataAdapter();
            DashBoardGraphModel dashBoardGraphdata = new DashBoardGraphModel();
           
            List<OpenByPriorityModel> OpenPriorityTktList = new List<OpenByPriorityModel>();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                #region DashBoardGraph Data

                MySqlCommand cmd1 = new MySqlCommand("Sp_DashBoardGraphData", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_BrandID", string.IsNullOrEmpty(BrandID) ? "" : BrandID);
                cmd1.Parameters.AddWithValue("_userid", string.IsNullOrEmpty(UserID) ? "" : UserID);
                cmd1.Parameters.AddWithValue("_tenantID", TenantID);
                cmd1.Parameters.AddWithValue("_fromdate", fromdate);
                cmd1.Parameters.AddWithValue("_todate", todate);
                da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(Graphds);
                if (Graphds != null && Graphds.Tables.Count > 0)
                {
                    if (Graphds.Tables[0].Rows.Count > 0) //PriorityGraph 
                    {

                        dashBoardGraphdata.PriorityChart = Graphds.Tables[0].AsEnumerable().Select(r => new OpenByPriorityModel()
                        {
                            priorityID = r.Field<object>("PriorityID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("PriorityID")),
                            priorityName = r.Field<object>("PriortyName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriortyName")),
                            priorityCount = r.Field<object>("PriorityCount") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("PriorityCount")),


                        }).ToList();

                        dashBoardGraphdata.OpenPriorityTicketCount = dashBoardGraphdata.PriorityChart.Count > 0 ?
                            dashBoardGraphdata.PriorityChart.Sum(x => x.priorityCount) : 0;

                        
                    }

                    if (Graphds.Tables[1].Rows.Count > 0) //Ticket To Bill   
                    {
                        dashBoardGraphdata.tickettoBillGraph = Graphds.Tables[1].AsEnumerable().Select(r => new TicketToBillGraphModel()
                        {
                            ticketSourceID = r.Field<object>("TicketSourceID") == System.DBNull.Value ? 0: Convert.ToInt32(r.Field<object>("TicketSourceID")),
                            ticketSourceName = r.Field<object>("TicketSourceName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TicketSourceName")),
                            totalBills = r.Field<object>("TotalBills") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TotalBills")),
                            ticketedBills = r.Field<object>("TicketedBills") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TicketedBills"))

                        }).ToList();
                    }



                    if (Graphds.Tables[2].Rows.Count > 0) //Ticket Generation Source
                    {
                        dashBoardGraphdata.ticketSourceGraph = Graphds.Tables[2].AsEnumerable().Select(r => new TicketSourceModel()
                        {
                            ticketSourceID = r.Field<object>("TicketSourceID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TicketSourceID")),
                            ticketSourceName = r.Field<object>("TicketSourceName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TicketSourceName")),
                            ticketSourceCount = r.Field<object>("TicketSourceCount") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TicketSourceCount"))

                        }).ToList();
                    }


                    if (Graphds.Tables[3].Rows.Count > 0) //Ticket TO Task
                    {
                        dashBoardGraphdata.tickettoTaskGraph = Graphds.Tables[3].AsEnumerable().Select(r => new TicketToTask()
                        {
                            totalTickets = r.Field<object>("AllTicket") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("AllTicket")),
                            taskTickets = r.Field<object>("Task") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("Task")),
                            Day = r.Field<object>("AllDay") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AllDay"))

                        }).ToList();
                    }

                    if (Graphds.Tables[4].Rows.Count > 0) //Ticket TO Claim
                    {
                        dashBoardGraphdata.tickettoClaimGraph = Graphds.Tables[4].AsEnumerable().Select(r => new TicketToClaim()
                        {
                            totalTickets = r.Field<object>("AllTicket") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("AllTicket")),
                            ClaimTickets = r.Field<object>("Claim") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("Claim")),
                            Day = r.Field<object>("AllDay") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AllDay"))

                        }).ToList();
                    }

                }

                #endregion

            }
            catch (Exception)
            {
                throw;
            }
            return dashBoardGraphdata;
        }

        /// <summary>
        /// Get Dashboard Tickets On Search
        /// </summary>
        /// <param name="searchModel"></param>  
        /// <returns></returns>
        public List<SearchResponseDashBoard> GetDashboardTicketsOnSearch(SearchModelDashBoard searchModel)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponseDashBoard> objSearchResult = new List<SearchResponseDashBoard>();

            List<string> CountList = new List<string>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
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

                if (searchModel.ActiveTabId == 1)//ByDate
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByDate_ForDashboard";

                    sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_CreatedOn) ? "" : searchModel.searchDataByDate.Ticket_CreatedOn);
                    sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_ModifiedOn) ? "" : searchModel.searchDataByDate.Ticket_ModifiedOn);
                    sqlcmd.Parameters.AddWithValue("SLA_DueON", searchModel.searchDataByDate.SLA_DueON);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByDate.Ticket_StatusID);
                }
                else if (searchModel.ActiveTabId == 2)//ByCustomerType
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByCustomerType_ForDashBoard";

                    sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerMobileNo) ? "" : searchModel.searchDataByCustomerType.CustomerMobileNo);
                    sqlcmd.Parameters.AddWithValue("customerEmail", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerEmailID) ? "" : searchModel.searchDataByCustomerType.CustomerEmailID);

                    if (string.IsNullOrEmpty(Convert.ToString(searchModel.searchDataByCustomerType.TicketID)) || Convert.ToString(searchModel.searchDataByCustomerType.TicketID) == "")
                        sqlcmd.Parameters.AddWithValue("TicketID", 0);
                    else
                        sqlcmd.Parameters.AddWithValue("TicketID", Convert.ToInt32(searchModel.searchDataByCustomerType.TicketID));
                                       
                    sqlcmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByCustomerType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 3)//ByTicketType
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByTicketType_ForDashBoard";

                    sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByTicketType.TicketPriorityID);
                    sqlcmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByTicketType.TicketStatusID);
                    sqlcmd.Parameters.AddWithValue("channelOfPurchaseIDs", string.IsNullOrEmpty(searchModel.searchDataByTicketType.ChannelOfPurchaseIds) ? "" : searchModel.searchDataByTicketType.ChannelOfPurchaseIds);
                    sqlcmd.Parameters.AddWithValue("ActionTypeIds", searchModel.searchDataByTicketType.ActionTypes);
                }
                else if (searchModel.ActiveTabId == 4) //ByCategory
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByCategory_Dashboard";

                    sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.searchDataByCategoryType.CategoryId);
                    sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.searchDataByCategoryType.SubCategoryId);
                    sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByCategoryType.IssueTypeId);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByCategoryType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 5)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByAll_ForDashBoard";

                    /*Column 1 (5)*/
                    sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.CreatedDate) ? "" : searchModel.searchDataByAll.CreatedDate);
                    sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.ModifiedDate) ? "" : searchModel.searchDataByAll.ModifiedDate);
                    sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.searchDataByAll.CategoryId);
                    sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.searchDataByAll.SubCategoryId);
                    sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByAll.IssueTypeId);

                    /*Column 2 (5) */
                    sqlcmd.Parameters.AddWithValue("TicketSourceType_ID", searchModel.searchDataByAll.TicketSourceTypeID);
                    sqlcmd.Parameters.AddWithValue("TicketIdORTitle", string.IsNullOrEmpty(searchModel.searchDataByAll.TicketIdORTitle) ? "" : searchModel.searchDataByAll.TicketIdORTitle);
                    sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByAll.PriorityId);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByAll.TicketSatutsID);
                    sqlcmd.Parameters.AddWithValue("SLAStatus", string.IsNullOrEmpty(searchModel.searchDataByAll.SLAStatus) ? "" : searchModel.searchDataByAll.SLAStatus);

                    /*Column 3 (5)*/
                    sqlcmd.Parameters.AddWithValue("TicketClaim_ID", Convert.ToInt32(searchModel.searchDataByAll.ClaimId));
                    sqlcmd.Parameters.AddWithValue("InvoiceNumberORSubOrderNo", string.IsNullOrEmpty(searchModel.searchDataByAll.InvoiceNumberORSubOrderNo) ? "" : searchModel.searchDataByAll.InvoiceNumberORSubOrderNo);
                    sqlcmd.Parameters.AddWithValue("OrderItemId", string.IsNullOrEmpty(Convert.ToString(searchModel.searchDataByAll.OrderItemId)) ?  0 : Convert.ToInt32(searchModel.searchDataByAll.OrderItemId));
                    //sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.searchDataByAll.IsVisitStore == "yes" ? 1 : 0);
                    //sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.searchDataByAll.IsWantVistingStore == "yes" ? 1 : 0);

                    /*All for to load all the data*/
                    if (searchModel.searchDataByAll.IsVisitStore.ToLower() != "all")
                        sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.searchDataByAll.IsVisitStore == "yes" ? 1 : 0);
                    else
                        sqlcmd.Parameters.AddWithValue("IsVisitedStore", -1);

                    if (searchModel.searchDataByAll.IsWantVistingStore.ToLower() != "all")
                        sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.searchDataByAll.IsWantVistingStore == "yes" ? 1 : 0);
                    else
                        sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", -1);

                    /*Column 4 (5)*/
                    sqlcmd.Parameters.AddWithValue("Customer_EmailID", searchModel.searchDataByAll.CustomerEmailID);
                    sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByAll.CustomerMobileNo) ? "" : searchModel.searchDataByAll.CustomerMobileNo);
                    sqlcmd.Parameters.AddWithValue("AssignTo", string.IsNullOrEmpty(searchModel.searchDataByAll.AssignTo) ? 0 : Convert.ToInt32(searchModel.searchDataByAll.AssignTo));
                    sqlcmd.Parameters.AddWithValue("StoreCodeORAddress", searchModel.searchDataByAll.StoreCodeORAddress);
                    sqlcmd.Parameters.AddWithValue("WantToStoreCodeORAddress", searchModel.searchDataByAll.WantToStoreCodeORAddress);

                    //Row - 2 and Column - 1  (5)
                    sqlcmd.Parameters.AddWithValue("HaveClaim", searchModel.searchDataByAll.HaveClaim);
                    sqlcmd.Parameters.AddWithValue("ClaimStatusId", searchModel.searchDataByAll.ClaimStatusId);
                    sqlcmd.Parameters.AddWithValue("ClaimCategoryId", searchModel.searchDataByAll.ClaimCategoryId);
                    sqlcmd.Parameters.AddWithValue("ClaimSubCategoryId", searchModel.searchDataByAll.ClaimSubCategoryId);
                    sqlcmd.Parameters.AddWithValue("ClaimIssueTypeId", searchModel.searchDataByAll.ClaimIssueTypeId);

                    //Row - 2 and Column - 2  (4)
                    sqlcmd.Parameters.AddWithValue("HaveTask", searchModel.searchDataByAll.HaveTask);
                    sqlcmd.Parameters.AddWithValue("TaskStatus_Id", searchModel.searchDataByAll.TaskStatusId);
                    sqlcmd.Parameters.AddWithValue("TaskDepartment_Id", searchModel.searchDataByAll.TaskDepartment_Id);
                    sqlcmd.Parameters.AddWithValue("TaskFunction_Id", searchModel.searchDataByAll.TaskFunction_Id);
                }



                sqlcmd.Parameters.AddWithValue("CurrentUserId", searchModel.curentUserId);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("Assignto_IDs", searchModel.AssigntoId.TrimEnd(','));
                sqlcmd.Parameters.AddWithValue("Brand_IDs", searchModel.BrandId.TrimEnd(','));

                sqlcmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = sqlcmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objSearchResult = ds.Tables[0].AsEnumerable().Select(r => new SearchResponseDashBoard()
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
                            createdago = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(r.Field<object>("CreatedDate")), "CreatedSpan"),
                            assignedTo = r.Field<object>("AssignedName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AssignedName")),
                            assignedago = r.Field<object>("AssignedDate") == System.DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(r.Field<object>("AssignedDate")), "AssignedSpan"),
                            updatedBy = r.Field<object>("ModifyByName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModifyByName")),
                            updatedago = r.Field<object>("ModifiedDate") == System.DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(r.Field<object>("ModifiedDate")), "ModifiedSpan"),

                            responseTimeRemainingBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityRespond") == System.DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "RespondTimeRemainingSpan"),
                            responseOverdueBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityRespond") == System.DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResponseOverDueSpan"),

                            resolutionOverdueBy = (r.Field<object>("AssignedDate") == System.DBNull.Value || r.Field<object>("PriorityResolve") == System.DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(r.Field<object>("PriorityResolve")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResolutionOverDueSpan"),

                            TaskStatus = r.Field<object>("TaskDetails") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TaskDetails")),
                            ClaimStatus = r.Field<object>("ClaimDetails") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ClaimDetails")),
                            TicketCommentCount = r.Field<object>("TicketComments") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TicketComments")),
                            isEscalation = r.Field<object>("IsEscalated") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsEscalated")),
                            ticketSourceType = Convert.ToString(r.Field<object>("TicketSourceType")),
                            IsReassigned = Convert.ToBoolean(r.Field<object>("IsReassigned")),
                            ticketSourceTypeID = Convert.ToInt16(r.Field<object>("TicketSourceTypeID")),
                            IsSLANearBreach = Convert.ToBoolean(r.Field<object>("IsSLANearBreach"))
                        }).ToList();
                    }
                }

                //paging here
                //if (searchparams.pageSize > 0 && objSearchResult.Count > 0)
                //    objSearchResult[0].totalpages = objSearchResult.Count > searchparams.pageSize ? Math.Round(Convert.ToDouble(objSearchResult.Count / searchparams.pageSize)) : 1;

                //objSearchResult = objSearchResult.Skip(rowStart).Take(searchparams.pageSize).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (ds != null) ds.Dispose(); conn.Close();
            }
            return objSearchResult;
        }

        /// <summary>
        /// DashBoard Search Data To CSV
        /// </summary>
        /// <param name="searchModel"></param>  
        /// <returns></returns>
        public string DashBoardSearchDataToCSV(SearchModelDashBoard searchModel)
        {
            List<SearchResponseDashBoard> objSearchResult = new List<SearchResponseDashBoard>();
            string csv = string.Empty;

            try
            {
                objSearchResult = GetDashboardTicketsOnSearch(searchModel);

                if (objSearchResult.Count > 0)
                {
                    csv = CommonService.ListToCSV(objSearchResult, "");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return csv;
        }

        /// <summary>
        /// Get Loggin Account Info
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="ProfilePicPath"></param>
        /// <returns></returns>
        public LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserID,string ProfilePicPath)
        {
            DataSet ds = new DataSet();
            DateTime now = DateTime.Now; DateTime temp = new DateTime();
            TimeSpan diff = new TimeSpan();
            MySqlCommand cmd = new MySqlCommand();
            LoggedInAgentModel loggedInAcc = new LoggedInAgentModel();
            ChatStatus chatstat = new ChatStatus();
            string profileImage = string.Empty;
            int ShiftDuration = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;

                loggedInAcc.AgentId = UserID; 
                //loggedInAcc.AgentName = AccountName;
                //loggedInAcc.AgentEmailId = EmailID;

                MySqlCommand cmd1 = new MySqlCommand("SP_LoggedInAccountInformation", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("_userID", UserID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        
                        loggedInAcc.LoginTime = ds.Tables[0].Rows[0]["logintime"] != System.DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["logintime"]).ToString("h:mm tt", culture) : "";
                        loggedInAcc.LogoutTime = ds.Tables[0].Rows[0]["logouttime"] != System.DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["logouttime"]).ToString("h:mm tt", culture) : "";
                        loggedInAcc.AgentName= ds.Tables[0].Rows[0]["AccountName"] != System.DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["AccountName"]) : string.Empty;
                        loggedInAcc.AgentEmailId = ds.Tables[0].Rows[0]["EmailID"] != System.DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]) : string.Empty ;
                       
                        ShiftDuration = ds.Tables[0].Rows[0]["ShiftDuration"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ShiftDuration"]) : 0;
                        profileImage= ds.Tables[0].Rows[0]["ProfilePicture"] != System.DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["ProfilePicture"]) : string.Empty;


                        loggedInAcc.ProfilePicture = !string.IsNullOrEmpty(profileImage) ? Path.Combine(ProfilePicPath, profileImage) : string.Empty;
                        if (ShiftDuration > 0)
                        {
                            temp = temp.AddHours(ShiftDuration);
                            loggedInAcc.ShiftDurationInHour = temp.Hour;
                            loggedInAcc.ShiftDurationInMinutes = temp.Minute;
                        }

                        if (!string.IsNullOrEmpty(loggedInAcc.LoginTime))
                        {
                            diff = now - Convert.ToDateTime(ds.Tables[0].Rows[0]["logintime"]);
                            loggedInAcc.LoggedInDuration = Math.Abs(diff.Hours) + "H " + Math.Abs(diff.Minutes) + "M";
                            loggedInAcc.LoggedInDurationInHours = Math.Abs(diff.Hours);
                            loggedInAcc.LoggedInDurationInMinutes = Math.Abs(diff.Minutes);

                            chatstat.isOnline = true;
                        }
                        else
                        {
                            loggedInAcc.LoggedInDuration = "0 H 0 M";
                            chatstat.isOffline = true;

                        }

                        loggedInAcc.Chatstatus = chatstat;
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        loggedInAcc.SLAScore = ds.Tables[1].Rows[0]["SLAScore"] != System.DBNull.Value ? Convert.ToString(ds.Tables[1].Rows[0]["SLAScore"]) : string.Empty;
                        loggedInAcc.AvgResponseTime = ds.Tables[1].Rows[0]["AverageResponseTime"] != System.DBNull.Value ? Convert.ToString(ds.Tables[1].Rows[0]["AverageResponseTime"]) : string.Empty;
                        loggedInAcc.CSATScore = ds.Tables[1].Rows[0]["CSATScore"] != System.DBNull.Value ? Convert.ToString(ds.Tables[1].Rows[0]["CSATScore"]) : string.Empty;
                    }

                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        loggedInAcc.WorkTimeInPercentage = Convert.ToString(ds.Tables[2].Rows[0]["WorkTimeInPercentage"]);
                        loggedInAcc.TotalWorkingTime = Convert.ToString(ds.Tables[2].Rows[0]["TotalWorkingTime"]);
                        loggedInAcc.workingMinute = Convert.ToString(ds.Tables[2].Rows[0]["workingMinute"]);
                    }


                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (ds != null) ds.Dispose(); conn.Close();
            }

            return loggedInAcc;
        }

        /// <summary>
        /// Add DashBoard Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="SearchSaveName"></param>
        /// <param name="parameter"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int AddDashBoardSearch(int UserID, string SearchSaveName, string parameter, int TenantId)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_SaveSearchUTSM", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Search_Parameters", parameter);
                cmd.Parameters.AddWithValue("@Search_Name", SearchSaveName);
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantId);
                cmd.Parameters.AddWithValue("@searchFor", 1);
                cmd.CommandType = CommandType.StoredProcedure;
                i = cmd.ExecuteNonQuery();
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
            return i;
        }

        /// <summary>
        /// Delete DashBoard Saved Search
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int DeleteDashBoardSavedSearch(int SearchParamID, int UserID)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteSearchByID_UTSM", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@SearchParam_ID", SearchParamID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                i = cmd.ExecuteNonQuery();
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
            return i;
        }

        /// <summary>
        /// List Saved DashBoard Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<UserTicketSearchMaster> ListSavedDashBoardSearch(int UserID)
        {

            DataSet ds = new DataSet();
            List<UserTicketSearchMaster> listSavedSearch = new List<UserTicketSearchMaster>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetSearchUTSMList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@searchFor", 1);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        UserTicketSearchMaster Savedsearch = new UserTicketSearchMaster();
                        Savedsearch.SearchParamID = Convert.ToInt32(ds.Tables[0].Rows[i]["SearchParamID"]);
                        Savedsearch.SearchName = Convert.ToString(ds.Tables[0].Rows[i]["SearchName"]);
                        listSavedSearch.Add(Savedsearch);
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
            return listSavedSearch;
        }

        /// <summary>
        /// Get DashBoard Tickets On Saved Search
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        public DashBoardSavedSearch GetDashBoardTicketsOnSavedSearch(int TenantID, int UserID, int SearchParamID)
        {
            string jsonSearchParams = string.Empty;
            DataSet ds = new DataSet();
            SearchModelDashBoard searchModel = new SearchModelDashBoard();
            DashBoardSavedSearch dbsavedsearch = new DashBoardSavedSearch();
            List<SearchResponseDashBoard> objSearchResult = new List<SearchResponseDashBoard>();


            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetSaveSearchByID_UTSM", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@SearchParam_ID", SearchParamID);

                cmd.Parameters.AddWithValue("@searchFor", 1);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        jsonSearchParams = ds.Tables[0].Rows[0]["SearchParameters"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["SearchParameters"]);
                    }
                }

                if (!string.IsNullOrEmpty(jsonSearchParams))
                {

                    searchModel = JsonConvert.DeserializeObject<SearchModelDashBoard>(jsonSearchParams);

                    if (searchModel != null)
                    {
                        searchModel.TenantID = TenantID;
                        searchModel.curentUserId = UserID;
                        objSearchResult = GetDashboardTicketsOnSearch(searchModel);
                        dbsavedsearch.dbsearchParams = jsonSearchParams;
                        dbsavedsearch.DashboardTicketList = objSearchResult;


                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (ds != null) ds.Dispose(); conn.Close();
            }
            return dbsavedsearch;
        }


        #region Mapping

        /// <summary>
        /// set Creation details
        /// </summary>
        /// <param name="time"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        public string SetCreationdetails(string time, string ColName)
        {
            string timespan = string.Empty;
            DateTime now = DateTime.Now;
            TimeSpan diff = new TimeSpan();
            string[] PriorityArr = null;
            string spantext = "{0}D {1}H {2}M Ago";
            try
            {
                if (string.IsNullOrEmpty(time))
                {
                    return "";
                }
                if (time.Split(new char[] { '|' }).Length < 2)
                {
                    return "";
                }
                if (time.Split(new char[] { '|' })[0].Trim().Length < 1)
                {
                    return "";
                }
                if (time.Split(new char[] { '|' })[1].Trim().Length < 1)
                {
                    return "";
                }

                if (ColName == "CreatedSpan" || ColName == "ModifiedSpan" || ColName == "AssignedSpan")
                {
                    diff = DateTime.Now - Convert.ToDateTime(time);
                    timespan = string.Format(spantext, Math.Abs(diff.Days), Math.Abs(diff.Hours), Math.Abs(diff.Minutes));
                  
                }
                else if (ColName == "RespondTimeRemainingSpan")
                {
                    PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });
                    DateTime assigneddate = Convert.ToDateTime(time.Split(new char[] { '|' })[1]);
                    

                    switch (PriorityArr[1])
                    {
                        case "D":
                            if(assigneddate.AddDays(Convert.ToDouble(PriorityArr[0])) > DateTime.Now)
                            {
                                diff = (assigneddate.AddDays(Convert.ToDouble(PriorityArr[0]))) - DateTime.Now;
                            }
                            break;

                        case "H":

                            if (assigneddate.AddHours(Convert.ToDouble(PriorityArr[0])) > DateTime.Now)
                            {
                                diff = (assigneddate.AddHours(Convert.ToDouble(PriorityArr[0]))) - DateTime.Now;
                            }
                           

                            break;

                        case "M":

                            if (assigneddate.AddMinutes(Convert.ToDouble(PriorityArr[0])) > DateTime.Now)
                            {
                                diff = (assigneddate.AddMinutes(Convert.ToDouble(PriorityArr[0]))) - DateTime.Now;
                            }
                           
                            break;

                    }
                    timespan = string.Format(spantext, Math.Abs(diff.Days), Math.Abs(diff.Hours), Math.Abs(diff.Minutes));
                    
                }
                else if (ColName == "ResponseOverDueSpan" || ColName == "ResolutionOverDueSpan")
                {
                    PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });
                    DateTime assigneddate = Convert.ToDateTime(time.Split(new char[] { '|' })[1]);

                    switch (PriorityArr[1])
                    {
                        case "D":
                            if (assigneddate.AddDays(Convert.ToDouble(PriorityArr[0])) < DateTime.Now)
                            {
                                diff = DateTime.Now - (assigneddate.AddDays(Convert.ToDouble(PriorityArr[0])));
                            }
                            break;

                        case "H":
                            if (assigneddate.AddHours(Convert.ToDouble(PriorityArr[0])) < DateTime.Now)
                            {
                                diff = DateTime.Now - (assigneddate.AddHours(Convert.ToDouble(PriorityArr[0])));
                            }
                            

                            break;

                        case "M":
                            if(assigneddate.AddMinutes(Convert.ToDouble(PriorityArr[0])) < DateTime.Now)
                            {
                                diff = DateTime.Now - (assigneddate.AddMinutes(Convert.ToDouble(PriorityArr[0])));
                            }
                            

                            break;

                    }

                    timespan = string.Format(spantext, Math.Abs(diff.Days), Math.Abs(diff.Hours), Math.Abs(diff.Minutes));
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                if (PriorityArr != null && PriorityArr.Length > 0)
                    Array.Clear(PriorityArr, 0, PriorityArr.Length);
            }
            return timespan;

        }

        //public string setCreationdetails1(string time, string ColName)
        //{
        //    string timespan = string.Empty;
        //    DateTime now = DateTime.Now;
        //    TimeSpan diff = new TimeSpan();
        //    string[] PriorityArr = null;


        //    try
        //    {
        //        if (ColName == "CreatedSpan" || ColName == "ModifiedSpan" || ColName == "AssignedSpan")
        //        {
        //            diff = now - Convert.ToDateTime(time);
        //            timespan = CalculateSpan(diff) + " ago";

        //        }
        //        else if (ColName == "RespondTimeRemainingSpan")
        //        {
        //            PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });

        //            switch (PriorityArr[1])
        //            {
        //                case "D":
        //                    diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddDays(Convert.ToDouble(PriorityArr[0]))) - now;

        //                    break;

        //                case "H":
        //                    diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddHours(Convert.ToDouble(PriorityArr[0]))) - now;

        //                    break;

        //                case "M":
        //                    diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddMinutes(Convert.ToDouble(PriorityArr[0]))) - now;

        //                    break;

        //            }
        //            timespan = CalculateSpan(diff);
        //        }
        //        else if (ColName == "ResponseOverDueSpan" || ColName == "ResolutionOverDueSpan")
        //        {
        //            PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });

        //            switch (PriorityArr[1])
        //            {
        //                case "D":
        //                    diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddDays(Convert.ToDouble(PriorityArr[0])));

        //                    break;

        //                case "H":
        //                    diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddHours(Convert.ToDouble(PriorityArr[0])));

        //                    break;

        //                case "M":
        //                    diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddMinutes(Convert.ToDouble(PriorityArr[0])));

        //                    break;

        //            }

        //            timespan = CalculateSpan(diff);
        //        }

        //    }
        //    catch (Exception)
        //    {

        //    }
        //    finally
        //    {
        //        if (PriorityArr != null && PriorityArr.Length > 0)
        //            Array.Clear(PriorityArr, 0, PriorityArr.Length);
        //    }
        //    return timespan;
        //}

        /// <summary>
        /// Calculate Span
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
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

        #endregion
    }
}
