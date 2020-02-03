using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class DashBoardService :IDashBoard
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
        /// Load Dashboard Data 
        /// </summary>
        public DashBoardDataModel GetDashBoardCountData(string BrandID, string UserID, string fromdate, string todate, int TenantID)
        {
            DataSet ds = new DataSet();
            DataSet Graphds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            DashBoardDataModel dashBoarddata = new DashBoardDataModel();
           // DashBoardGraphModel dashBoardGraphdata = new DashBoardGraphModel();
            int TotalTickets = 0; int resolvedTickets = 0; int UnresolvedTickets = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;

                #region DashBoard Data
                MySqlCommand cmd1 = new MySqlCommand("SP_DashBoardList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_BrandID", string.IsNullOrEmpty(BrandID) ? "" : BrandID);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
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

                    if (ds.Tables[9].Rows.Count > 0) //Response SLA  ----hardcoded for now-----
                    {
                        dashBoarddata.ResponseRate = ds.Tables[9].Rows[0]["ResponseSLA"] != System.DBNull.Value ? Convert.ToString(ds.Tables[9].Rows[0]["ResponseSLA"])+"%" : "";
                        dashBoarddata.isResponseSuccess = true;
                    }


                    if ((ds.Tables[10].Rows.Count > 0)) //Resolution SLA
                    {
                        TotalTickets = ds.Tables[10].Rows[0]["TotalTickets"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[10].Rows[0]["TotalTickets"]) : 0;
                        resolvedTickets = ds.Tables[10].Rows[0]["ResolvedTickets"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[10].Rows[0]["ResolvedTickets"]) : 0;
                        UnresolvedTickets = ds.Tables[10].Rows[0]["UnresolvedTickets"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[10].Rows[0]["UnresolvedTickets"]) : 0;

                        if (TotalTickets > 0)
                        {
                            if (resolvedTickets > 0)
                            {
                                dashBoarddata.ResolutionRate = Convert.ToString(Math.Round(Convert.ToDouble((resolvedTickets / TotalTickets) * 100), 2)) + "%";
                                dashBoarddata.isResolutionSuccess = true;
                            }
                            else
                            {
                                int test = UnresolvedTickets / TotalTickets * 100;
                                dashBoarddata.ResolutionRate = Convert.ToString(Math.Round(Convert.ToDouble((UnresolvedTickets / TotalTickets) * 100), 2)) + "%";
                                dashBoarddata.isResolutionSuccess = false;
                            }

                        }
                        else
                        {
                            dashBoarddata.isResolutionSuccess = false;
                            dashBoarddata.ResolutionRate = "0 %";
                        }
                    }
                }

                #endregion



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dashBoarddata;
        }

        /// <summary>
        /// Load Dashboard Graph Data
        /// </summary>
        public DashBoardGraphModel GetDashBoardGraphdata(string BrandID, string UserID, string fromdate, string todate, int TenantID)
        {

            DataSet Graphds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter da = new MySqlDataAdapter();
            DashBoardGraphModel dashBoardGraphdata = new DashBoardGraphModel();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                #region DashBoardGraph Data

                MySqlCommand cmd1 = new MySqlCommand("Sp_DashBoardGraphData", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_BrandID", string.IsNullOrEmpty(BrandID) ? "" : BrandID);
                cmd1.Parameters.AddWithValue("_userid", UserID);
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
                        dashBoardGraphdata.PriorityChart = Graphds.Tables[0].AsEnumerable().Select(r => new PriorityGraphModel()
                        {

                            priorityID = Convert.ToInt32(r.Field<object>("PriorityID")),
                            priorityName = Convert.ToString(r.Field<object>("PriortyName")),
                            priorityCount = Convert.ToInt32(r.Field<object>("PriorityCount"))


                        }).ToList();
                    }

                    if (Graphds.Tables[1].Rows.Count > 0) //Ticket To Bill   
                    {
                        dashBoardGraphdata.tickettoBillGraph = Graphds.Tables[1].AsEnumerable().Select(r => new TicketToBillGraphModel()
                        {
                            ticketSourceID = Convert.ToInt32(r.Field<object>("TicketSourceID")),
                            ticketSourceName = Convert.ToString(r.Field<object>("TicketSourceName")),
                            totalBills = Convert.ToInt32(r.Field<object>("TotalBills")),
                            ticketedBills = Convert.ToInt32(r.Field<object>("TicketedBills"))

                        }).ToList();
                    }



                    if (Graphds.Tables[2].Rows.Count > 0) //Ticket Generation Source
                    {
                        dashBoardGraphdata.ticketSourceGraph = Graphds.Tables[2].AsEnumerable().Select(r => new TicketSourceModel()
                        {
                            ticketSourceID = Convert.ToInt32(r.Field<object>("TicketSourceID")),
                            ticketSourceName = Convert.ToString(r.Field<object>("TicketSourceName")),
                            ticketSourceCount = Convert.ToInt32(r.Field<object>("TicketSourceCount"))

                        }).ToList();
                    }


                    if (Graphds.Tables[3].Rows.Count > 0) //Ticket TO Task
                    {
                        dashBoardGraphdata.tickettoTaskGraph = Graphds.Tables[3].AsEnumerable().Select(r => new TicketToTask()
                        {
                            totalTickets = Convert.ToInt32(r.Field<object>("AllTicket")),
                            taskTickets = Convert.ToInt32(r.Field<object>("Task")),
                            Day = Convert.ToString(r.Field<object>("AllDay"))

                        }).ToList();
                    }

                    if (Graphds.Tables[4].Rows.Count > 0) //Ticket TO Claim
                    {
                        dashBoardGraphdata.tickettoClaimGraph = Graphds.Tables[4].AsEnumerable().Select(r => new TicketToClaim()
                        {
                            totalTickets = Convert.ToInt32(r.Field<object>("AllTicket")),
                            ClaimTickets = Convert.ToInt32(r.Field<object>("Claim")),
                            Day = Convert.ToString(r.Field<object>("AllDay"))

                        }).ToList();
                    }

                }

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dashBoardGraphdata;
        }

        /// <summary>
        /// Get tickets on the dashboard
        /// </summary>
        public List<SearchResponseDashBoard> GetDashboardTicketsOnSearch(SearchModelDashBoard searchModel)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponseDashBoard> objSearchResult = new List<SearchResponseDashBoard>();
       
            List<string> CountList = new List<string>();

            int rowStart = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                conn.Open();
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
                    sqlcmd.Parameters.AddWithValue("TicketID", searchModel.searchDataByCustomerType.TicketID==null?0: searchModel.searchDataByCustomerType.TicketID);
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
                    sqlcmd.Parameters.AddWithValue("TicketClaim_ID", searchModel.searchDataByAll.ClaimId);
                    sqlcmd.Parameters.AddWithValue("InvoiceNumberORSubOrderNo", string.IsNullOrEmpty(searchModel.searchDataByAll.InvoiceNumberORSubOrderNo) ? "" : searchModel.searchDataByAll.InvoiceNumberORSubOrderNo);
                    sqlcmd.Parameters.AddWithValue("OrderItemId", searchModel.searchDataByAll.OrderItemId);
                    sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.searchDataByAll.IsVisitStore == "yes" ? 1 : 0);
                    sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.searchDataByAll.IsWantVistingStore == "yes" ? 1 : 0);

                    sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.searchDataByAll.IsWantVistingStore);

                    /*Column 4 (5)*/
                    sqlcmd.Parameters.AddWithValue("CustomerEmailID", searchModel.searchDataByAll.CustomerEmailID);
                    sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByAll.CustomerMobileNo) ? "" : searchModel.searchDataByAll.CustomerMobileNo);
                    sqlcmd.Parameters.AddWithValue("AssignTo", searchModel.searchDataByAll.AssignTo);
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
                sqlcmd.Parameters.AddWithValue("Assignto_IDs", searchModel.AssigntoId);
                sqlcmd.Parameters.AddWithValue("Brand_IDs", searchModel.BrandId);

                sqlcmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objSearchResult = ds.Tables[0].AsEnumerable().Select(r => new SearchResponseDashBoard()
                        {
                            ticketID = Convert.ToInt32(r.Field<object>("TicketID")),
                            ticketStatus = Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(r.Field<object>("StatusID"))),
                            Message = r.Field<object>("TicketDescription") == System.DBNull.Value ? string.Empty: Convert.ToString(r.Field<object>("TicketDescription")),
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

                            TaskStatus = r.Field<object>("TaskDetails") == System.DBNull.Value ? string.Empty:Convert.ToString(r.Field<object>("TaskDetails")),
                            ClaimStatus = r.Field<object>("ClaimDetails") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ClaimDetails")),
                            TicketCommentCount = r.Field<object>("ClaimDetails") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("TicketComments")),
                            isEscalation = r.Field<object>("IsEscalated")==System.DBNull.Value ? 0 :Convert.ToInt32(r.Field<object>("IsEscalated"))

                        }).ToList();
                    }
                }

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
                if (ds != null) ds.Dispose(); conn.Close();
            }
            return objSearchResult;
        }

        /// <summary>
        /// Export DashBoard search result to CSV
        /// </summary>
        public string DashBoardSearchDataToCSV(SearchModelDashBoard searchModel)
        {
            List<SearchResponseDashBoard> objSearchResult = new List<SearchResponseDashBoard>();
            string csv = string.Empty;

            try
            {
                objSearchResult = GetDashboardTicketsOnSearch(searchModel);

                if(objSearchResult.Count > 0)
                {
                    csv = CommonService.ListToCSV(objSearchResult, "");
                }
            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            
            return csv;
        }

        public LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserID, string EmailID,string AccountName)
        {
            DataSet ds = new DataSet();
            DateTime now = DateTime.Now; DateTime temp = new DateTime();
            TimeSpan diff = new TimeSpan();
            MySqlCommand cmd = new MySqlCommand();
            LoggedInAgentModel loggedInAcc = new LoggedInAgentModel();
            ChatStatus chatstat = new ChatStatus();
            int ShiftDuration = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;

                loggedInAcc.AgentId = UserID;
                loggedInAcc.AgentName = AccountName;
                loggedInAcc.AgentEmailId = EmailID;

                MySqlCommand cmd1 = new MySqlCommand("SP_LoggedInAccountInformation", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("_userID", UserID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                       
                        loggedInAcc.LoginTime = ds.Tables[0].Rows[0]["logintime"] != System.DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["logintime"]).ToString("h:mm tt", culture) : "";
                        loggedInAcc.LogoutTime = ds.Tables[0].Rows[0]["logouttime"] != System.DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["logouttime"]).ToString("h:mm tt", culture) : "";

                        ShiftDuration = ds.Tables[0].Rows[0]["ShiftDuration"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ShiftDuration"]) : 0;

                        if(ShiftDuration >0)
                        {
                            temp = temp.AddHours(ShiftDuration);
                            loggedInAcc.ShiftDurationInHour = temp.Hour;
                            loggedInAcc.ShiftDurationInMinutes = temp.Minute;
                        }

                        if (!string.IsNullOrEmpty(loggedInAcc.LoginTime))
                        {
                            diff = now - Convert.ToDateTime(ds.Tables[0].Rows[0]["logintime"]);
                            loggedInAcc.LoggedInDuration = Math.Abs(diff.Hours ) +"H " + Math.Abs(diff.Minutes) + "M";
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
                    }
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        loggedInAcc.AvgResponseTime = ds.Tables[2].Rows[0]["AverageResponseTime"] != System.DBNull.Value ? Convert.ToString(ds.Tables[2].Rows[0]["AverageResponseTime"]) : string.Empty;
                    }
                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        loggedInAcc.CSATScore = ds.Tables[3].Rows[0]["CSATScore"] != System.DBNull.Value ? Convert.ToString(ds.Tables[3].Rows[0]["CSATScore"]) : string.Empty;
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

            return loggedInAcc;
        }

        /// <summary>
        /// Creation Details Mapping
        /// </summary>
            #region Mapping
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

        #endregion
    }
}
