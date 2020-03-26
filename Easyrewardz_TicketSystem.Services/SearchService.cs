using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Easyrewardz_TicketSystem.CustomModel;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;
using Easyrewardz_TicketSystem.MySqlDBContext;

namespace Easyrewardz_TicketSystem.Services
{
    public class SearchService : ISearchTicket
    {
        #region variable
        private readonly IDistributedCache Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        #region DB connection
        MySqlConnection conn = new MySqlConnection();
        public SearchService(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            Cache = cache;
        }
        #endregion

        public List<SearchResponse> SearchTickets(SearchRequest searchparams)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponse> objSearchResult = new List<SearchResponse>();
            List<SearchResponse> temp = new List<SearchResponse>(); //delete later
            List<string> countList = new List<string>();

            int rowStart = (searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand sqlCmd = new MySqlCommand("SP_SearchTickets", conn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("_isEscalated", Convert.ToInt32(searchparams.isEscalation));
                sqlCmd.Parameters.AddWithValue("_isByStatus", Convert.ToInt32(searchparams.isByStatus));
                sqlCmd.Parameters.AddWithValue("_isByFilter", Convert.ToInt32(searchparams.isByFilter));
                sqlCmd.Parameters.AddWithValue("_isByDate", Convert.ToInt32(searchparams.ByDate));
                sqlCmd.Parameters.AddWithValue("_isByCustomerType", Convert.ToInt32(searchparams.ByCustomerType));
                sqlCmd.Parameters.AddWithValue("_isByticketType", Convert.ToInt32(searchparams.ByTicketType));
                sqlCmd.Parameters.AddWithValue("_isByCategory", Convert.ToInt32(searchparams.ByCategory));
                sqlCmd.Parameters.AddWithValue("_isByAll", Convert.ToInt32(searchparams.byAll));
                sqlCmd.Parameters.AddWithValue("_tenantID", Convert.ToInt32(searchparams.tenantID));
                sqlCmd.Parameters.AddWithValue("_createdDate", searchparams.creationDate);
                sqlCmd.Parameters.AddWithValue("_lastUpdatedDate", searchparams.lastUpdatedDate);
                sqlCmd.Parameters.AddWithValue("_SLADue", searchparams.SLADue);
                sqlCmd.Parameters.AddWithValue("_ticketStatus", searchparams.ticketStatus);
                sqlCmd.Parameters.AddWithValue("_customerMob", searchparams.customerMob);
                sqlCmd.Parameters.AddWithValue("_customerMail", searchparams.customerEmail);
                sqlCmd.Parameters.AddWithValue("_ticketID", searchparams.TicketID);
                sqlCmd.Parameters.AddWithValue("_priority", searchparams.Priority);
                sqlCmd.Parameters.AddWithValue("_channelOfPurchase", searchparams.chanelOfPurchase);
                sqlCmd.Parameters.AddWithValue("_ticketActionType", searchparams.ticketActionType);
                sqlCmd.Parameters.AddWithValue("_categoryName", searchparams.Category);
                sqlCmd.Parameters.AddWithValue("_subCategoryName", searchparams.subCategory);
                sqlCmd.Parameters.AddWithValue("_issueType", searchparams.issueType);
                sqlCmd.Parameters.AddWithValue("_tikcetSource", searchparams.ticketSource);
                sqlCmd.Parameters.AddWithValue("_claimID", searchparams.claimID);
                sqlCmd.Parameters.AddWithValue("_ticketTitle", searchparams.ticketTitle);
                sqlCmd.Parameters.AddWithValue("_invoiceSubOrderNo", searchparams.invoiceSubOrderNo);
                sqlCmd.Parameters.AddWithValue("_assignedTo", searchparams.assignedTo);
                sqlCmd.Parameters.AddWithValue("_didVisitStore", Convert.ToInt32(searchparams.didVisitStore));
                sqlCmd.Parameters.AddWithValue("_itemID", Convert.ToInt32(searchparams.itemID));
                sqlCmd.Parameters.AddWithValue("_purchaseStorecoseAddr", searchparams.purchaseStoreCodeAddress);
                sqlCmd.Parameters.AddWithValue("_SLAstatus", searchparams.SLAstatus);
                sqlCmd.Parameters.AddWithValue("_wantToVisitStore", Convert.ToInt32(searchparams.wantToVisitStore));
                sqlCmd.Parameters.AddWithValue("_wantToVisitStoreCodeAddr", Convert.ToInt32(searchparams.wantToVisitStoreCodeAddress));
                sqlCmd.Parameters.AddWithValue("_withClaim", Convert.ToInt32(searchparams.withClaim));
                sqlCmd.Parameters.AddWithValue("_withTask", Convert.ToInt32(searchparams.withTask));
                sqlCmd.Parameters.AddWithValue("_claimStatus", searchparams.claimStatus);
                sqlCmd.Parameters.AddWithValue("_taskStatus", searchparams.taskStatus);
                sqlCmd.Parameters.AddWithValue("_claimCategory", searchparams.claimCategory);
                sqlCmd.Parameters.AddWithValue("_taskDept", searchparams.taskDept);
                sqlCmd.Parameters.AddWithValue("_taskFunction", searchparams.taskFunction);
                sqlCmd.Parameters.AddWithValue("_claimIssuetype", searchparams.claimIssuetype);
                sqlCmd.Parameters.AddWithValue("_claimsubCategory", searchparams.claimSubcategory);

                if (!string.IsNullOrEmpty(searchparams.SLAstatus))
                {
                    sqlCmd.Parameters.AddWithValue("_SLAstatusResponse", Convert.ToInt32(searchparams.SLAstatus.Split('|')[0].Split('-')[0]));
                    sqlCmd.Parameters.AddWithValue("_SLAstatusResponsetime", 1);
                    sqlCmd.Parameters.AddWithValue("_SLAstatusResolution", Convert.ToInt32(searchparams.SLAstatus.Split('|')[1].Split('-')[0]));
                    sqlCmd.Parameters.AddWithValue("_SLAstatusResoltiontime", 1);
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("_SLAstatusResponse", 0);
                    sqlCmd.Parameters.AddWithValue("_SLAstatusResponsetime", 0);
                    sqlCmd.Parameters.AddWithValue("_SLAstatusResolution", 0);
                    sqlCmd.Parameters.AddWithValue("_SLAstatusResoltiontime", 0);
                }


                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlCmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {


                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        objSearchResult = ds.Tables[0].AsEnumerable().Select(r => new SearchResponse()
                        {
                            ticketID = Convert.ToInt32(r.Field<object>("TicketID")),
                            ticketStatus = Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(r.Field<object>("StatusID"))),
                            Message = Convert.ToString(r.Field<object>("TicketDescription")),
                            Category = Convert.ToString(r.Field<object>("CategoryName")),
                            subCategory = Convert.ToString(r.Field<object>("SubCategoryName")),
                            IssueType = Convert.ToString(r.Field<object>("IssueTypeName")),
                            Priority = Convert.ToString(r.Field<object>("PriortyName")),
                            Assignee = Convert.ToString(r.Field<object>("AssignedName")),
                            CreatedOn = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedOn"))) ? string.Empty : Convert.ToString(r.Field<object>("CreatedOn")),


                            createdBy = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedByName"))) ? string.Empty : Convert.ToString(r.Field<object>("CreatedByName")),

                            createdago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("CreatedDate")), "CreatedSpan"),
                            assignedTo = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedName"))) ? string.Empty : Convert.ToString(r.Field<object>("AssignedName")),

                            assignedago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("AssignedDate")), "AssignedSpan"),

                            updatedBy = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("ModifyByName"))) ? string.Empty : Convert.ToString(r.Field<object>("ModifyByName")),

                            updatedago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("ModifiedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("ModifiedDate")), "ModifiedSpan"),

                            responseTimeRemainingBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityRespond")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "RespondTimeRemainingSpan"),

                            responseOverdueBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityRespond")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResponseOverDueSpan"),

                            resolutionOverdueBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityResolve")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityResolve")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResolutionOverDueSpan"),

                            TaskStatus = Convert.ToString(r.Field<object>("TaskDetails")),
                            ClaimStatus = Convert.ToString(r.Field<object>("ClaimDetails")),
                            //TicketCommentCount = Convert.ToInt32(r.Field<object>("TicketComments")),
                            TicketCommentCount = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("TicketComments")))) ? 0 : Convert.ToInt32(r.Field<object>("TicketComments")),
                            isEscalation = Convert.ToInt32(r.Field<object>("IsEscalated"))

                        }).ToList();
                    }
                }

                //temporary filter for react binding

                //if(objSearchResult.Count > 0)
                //temp.Add(objSearchResult.Select(x => x).FirstOrDefault());

                //if (searchparams.pageSize > 0 && objSearchResult.Count > 0)
                //    temp[0].totalpages = temp.Count > searchparams.pageSize ? Math.Round(Convert.ToDouble(temp.Count / searchparams.pageSize)) : 1;

                //********


                //paging here
                if (searchparams.pageSize > 0 && objSearchResult.Count > 0)
                    objSearchResult[0].totalpages = objSearchResult.Count > searchparams.pageSize ? Math.Round(Convert.ToDouble(objSearchResult.Count / searchparams.pageSize)) : 1;

                objSearchResult = objSearchResult.Skip(rowStart).Take(searchparams.pageSize).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose(); 
            }
            return objSearchResult;
            //return temp;   
        }

        public List<TicketStatusModel> TicketStatusCount(SearchRequest searchparams)
        {
            List<TicketStatusModel> ticketCount = new List<TicketStatusModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();

            try

            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_TicketStatusCount", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("_tenantID", Convert.ToInt32(searchparams.tenantID));
                sqlcmd.Parameters.AddWithValue("_assignedID", Convert.ToInt32(searchparams.assignedTo));
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

                        ticketCount = ds.Tables[0].AsEnumerable().Select(r => new TicketStatusModel()
                        {
                            ticketStatus = Convert.ToString(r.Field<object>("TicketStatus")),
                            ticketCount = Convert.ToInt32(r.Field<object>("TicketStatusCount"))

                        }).ToList();

                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{
                        //    string ticketStatus = Convert.ToString(ds.Tables[0].Rows[i]["TicketStatus"]) + "|" + Convert.ToString(ds.Tables[0].Rows[i]["TicketStatusCount"]);
                        //    ticketCount.Add(ticketStatus);
                        //}
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
                
            }

            return ticketCount;
        }

        /// <summary>
        /// Get tickets on the page load
        /// </summary>
        /// <param name="HeaderStatus_ID"></param>
        /// <param name="Tenant_ID"></param>
        /// <param name="AssignTo_ID"></param>
        /// <returns></returns>
        public List<SearchResponse> GetTicketsOnLoad(int HeaderStatus_ID, int Tenant_ID, int AssignTo_ID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponse> objSearchResult = new List<SearchResponse>();
            List<SearchResponse> temp = new List<SearchResponse>(); //delete later
            List<string> countList = new List<string>();

            //int rowStart = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_getTicketSearchOnLoad", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("HeaderStatus_ID", HeaderStatus_ID);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", Tenant_ID);
                sqlcmd.Parameters.AddWithValue("AssignTo_ID", AssignTo_ID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objSearchResult = ds.Tables[0].AsEnumerable().Select(r => new SearchResponse()
                        {
                            ticketID = Convert.ToInt32(r.Field<object>("TicketID")),
                            ticketStatus = Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(r.Field<object>("StatusID"))),
                            Message = Convert.ToString(r.Field<object>("TicketDescription")),
                            Category = Convert.ToString(r.Field<object>("CategoryName")),
                            subCategory = Convert.ToString(r.Field<object>("SubCategoryName")),
                            IssueType = Convert.ToString(r.Field<object>("IssueTypeName")),
                            Priority = Convert.ToString(r.Field<object>("PriortyName")),
                            Assignee = Convert.ToString(r.Field<object>("AssignedName")),
                            CreatedOn = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedOn"))) ? string.Empty : Convert.ToString(r.Field<object>("CreatedOn")),
                            createdBy = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedByName"))) ? string.Empty : Convert.ToString(r.Field<object>("CreatedByName")),
                            createdago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("CreatedDate")), "CreatedSpan"),
                            assignedTo = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedName"))) ? string.Empty : Convert.ToString(r.Field<object>("AssignedName")),
                            assignedago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("AssignedDate")), "AssignedSpan"),
                            updatedBy = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("ModifyByName"))) ? string.Empty : Convert.ToString(r.Field<object>("ModifyByName")),
                            updatedago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("ModifiedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("ModifiedDate")), "ModifiedSpan"),

                            responseTimeRemainingBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityRespond")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "RespondTimeRemainingSpan"),
                            responseOverdueBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityRespond")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResponseOverDueSpan"),

                            resolutionOverdueBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityResolve")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityResolve")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResolutionOverDueSpan"),

                            TaskStatus = Convert.ToString(r.Field<object>("TaskDetails")),
                            ClaimStatus = Convert.ToString(r.Field<object>("ClaimDetails")),
                            TicketCommentCount = Convert.ToInt32(r.Field<object>("TicketComments")),
                            isEscalation = Convert.ToInt32(r.Field<object>("IsEscalated")),
                            ticketSourceType =  Convert.ToString(r.Field<object>("TicketSourceType")),
                            ticketSourceTypeID = Convert.ToInt16(r.Field<object>("TicketSourceTypeID")),
                            IsReassigned = Convert.ToBoolean(r.Field<object>("IsReassigned")),
                            IsSLANearBreach = Convert.ToBoolean(r.Field<object>("IsSLANearBreach"))
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
                //throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
            return objSearchResult;
        }

        /// <summary>
        /// Get tickets on the page load
        /// </summary>
        /// <param name="HeaderStatus_ID"></param>
        /// <param name="Tenant_ID"></param>
        /// <param name="AssignTo_ID"></param>
        /// <returns></returns>
        public List<SearchResponse> GetTicketsOnSearch(SearchModel searchModel)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponse> objSearchResult = new List<SearchResponse>();
            List<SearchResponse> temp = new List<SearchResponse>(); //delete later
            List<string> countList = new List<string>();

            int rowStart = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
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
                MySqlCommand sqlCmd = new MySqlCommand("", conn);

                sqlCmd.Parameters.AddWithValue("HeaderStatus_Id", searchModel.HeaderStatusId);

                if (searchModel.ActiveTabId == 1)
                {
                    sqlCmd.CommandText = "SP_SearchTicketData_ByDate";

                    sqlCmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_CreatedOn) ? "" : searchModel.searchDataByDate.Ticket_CreatedOn);
                    sqlCmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_ModifiedOn) ? "" : searchModel.searchDataByDate.Ticket_ModifiedOn);
                    sqlCmd.Parameters.AddWithValue("SLA_DueON", searchModel.searchDataByDate.SLA_DueON);
                    sqlCmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByDate.Ticket_StatusID);
                }
                else if (searchModel.ActiveTabId == 2)
                {
                    sqlCmd.CommandText = "SP_SearchTicketData_ByCustomerType";

                    sqlCmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerMobileNo) ? "" : searchModel.searchDataByCustomerType.CustomerMobileNo);
                    sqlCmd.Parameters.AddWithValue("CustomerEmailID", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerEmailID) ? "" : searchModel.searchDataByCustomerType.CustomerEmailID);
                    sqlCmd.Parameters.AddWithValue("Ticket_ID", searchModel.searchDataByCustomerType.TicketID == null ? 0 : searchModel.searchDataByCustomerType.TicketID);
                    sqlCmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByCustomerType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 3)
                {
                    sqlCmd.CommandText = "SP_SearchTicketData_ByTicketType";

                    sqlCmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByTicketType.TicketPriorityID);
                    sqlCmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByTicketType.TicketStatusID);
                    sqlCmd.Parameters.AddWithValue("ChannelOfPurchaseIDs", string.IsNullOrEmpty(searchModel.searchDataByTicketType.ChannelOfPurchaseIds) ? "" : searchModel.searchDataByTicketType.ChannelOfPurchaseIds);
                    sqlCmd.Parameters.AddWithValue("ActionTypeIds", searchModel.searchDataByTicketType.ActionTypes);
                }
                else if (searchModel.ActiveTabId == 4)
                {
                    sqlCmd.CommandText = "SP_SearchTicketData_ByCategory";

                    sqlCmd.Parameters.AddWithValue("Category_Id", searchModel.searchDataByCategoryType.CategoryId);
                    sqlCmd.Parameters.AddWithValue("SubCategory_Id", searchModel.searchDataByCategoryType.SubCategoryId);
                    sqlCmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByCategoryType.IssueTypeId);
                    sqlCmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByCategoryType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 5)
                {
                    sqlCmd.CommandText = "SP_SearchTicketData_ByAll";

                    /*Column 1 (5)*/
                    sqlCmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.CreatedDate) ? "" : searchModel.searchDataByAll.CreatedDate);
                    sqlCmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.ModifiedDate) ? "" : searchModel.searchDataByAll.ModifiedDate);
                    sqlCmd.Parameters.AddWithValue("Category_Id", searchModel.searchDataByAll.CategoryId);
                    sqlCmd.Parameters.AddWithValue("SubCategory_Id", searchModel.searchDataByAll.SubCategoryId);
                    sqlCmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByAll.IssueTypeId);

                    /*Column 2 (5) */
                    sqlCmd.Parameters.AddWithValue("TicketSourceType_ID", searchModel.searchDataByAll.TicketSourceTypeID);
                    sqlCmd.Parameters.AddWithValue("TicketIdORTitle", string.IsNullOrEmpty(searchModel.searchDataByAll.TicketIdORTitle) ? "" : searchModel.searchDataByAll.TicketIdORTitle);
                    sqlCmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByAll.PriorityId);
                    sqlCmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByAll.TicketSatutsID);
                    sqlCmd.Parameters.AddWithValue("SLAStatus", string.IsNullOrEmpty(searchModel.searchDataByAll.SLAStatus) ? "" : searchModel.searchDataByAll.SLAStatus);

                    /*Column 3 (5)*/
                    sqlCmd.Parameters.AddWithValue("TicketClaim_ID", searchModel.searchDataByAll.ClaimId);
                    sqlCmd.Parameters.AddWithValue("InvoiceNumberORSubOrderNo", string.IsNullOrEmpty(searchModel.searchDataByAll.InvoiceNumberORSubOrderNo) ? "" : searchModel.searchDataByAll.InvoiceNumberORSubOrderNo);
                    sqlCmd.Parameters.AddWithValue("OrderItemId", searchModel.searchDataByAll.OrderItemId);

                    /*All for to load all the data*/
                    if (searchModel.searchDataByAll.IsVisitStore.ToLower() != "all")
                        sqlCmd.Parameters.AddWithValue("IsVisitedStore", searchModel.searchDataByAll.IsVisitStore == "yes" ? 1 : 0);
                    else
                        sqlCmd.Parameters.AddWithValue("IsVisitedStore", -1);

                    if (searchModel.searchDataByAll.IsWantVistingStore.ToLower() != "all")
                        sqlCmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.searchDataByAll.IsWantVistingStore == "yes" ? 1 : 0);
                    else
                        sqlCmd.Parameters.AddWithValue("IsWantToVisitStore", -1);

                    /*Column 4 (5)*/
                    sqlCmd.Parameters.AddWithValue("Customer_EmailID", searchModel.searchDataByAll.CustomerEmailID);
                    sqlCmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByAll.CustomerMobileNo) ? "" : searchModel.searchDataByAll.CustomerMobileNo);
                    sqlCmd.Parameters.AddWithValue("OtherAgentAssignTo", string.IsNullOrEmpty(Convert.ToString(searchModel.searchDataByAll.AssignTo)) ? 0 : Convert.ToInt32(searchModel.searchDataByAll.AssignTo));
                    sqlCmd.Parameters.AddWithValue("StoreCodeORAddress", searchModel.searchDataByAll.StoreCodeORAddress);
                    sqlCmd.Parameters.AddWithValue("WantToStoreCodeORAddress", string.IsNullOrEmpty(searchModel.searchDataByAll.WantToStoreCodeORAddress) ? "" : searchModel.searchDataByAll.WantToStoreCodeORAddress);

                    //Row - 2 and Column - 1  (5)
                    sqlCmd.Parameters.AddWithValue("HaveClaim", searchModel.searchDataByAll.HaveClaim);
                    sqlCmd.Parameters.AddWithValue("ClaimStatusId", searchModel.searchDataByAll.ClaimStatusId);
                    sqlCmd.Parameters.AddWithValue("ClaimCategoryId", searchModel.searchDataByAll.ClaimCategoryId);
                    sqlCmd.Parameters.AddWithValue("ClaimSubCategoryId", searchModel.searchDataByAll.ClaimSubCategoryId);
                    sqlCmd.Parameters.AddWithValue("ClaimIssueTypeId", searchModel.searchDataByAll.ClaimIssueTypeId);

                    //Row - 2 and Column - 2  (4)
                    sqlCmd.Parameters.AddWithValue("HaveTask", searchModel.searchDataByAll.HaveTask);
                    sqlCmd.Parameters.AddWithValue("TaskStatus_Id", searchModel.searchDataByAll.TaskStatusId);
                    sqlCmd.Parameters.AddWithValue("TaskDepartment_Id", searchModel.searchDataByAll.TaskDepartment_Id);
                    sqlCmd.Parameters.AddWithValue("TaskFunction_Id", searchModel.searchDataByAll.TaskFunction_Id);
                }

                sqlCmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlCmd.Parameters.AddWithValue("Assignto_Id", searchModel.AssigntoId);

                sqlCmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlCmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objSearchResult = ds.Tables[0].AsEnumerable().Select(r => new SearchResponse()
                        {
                            ticketID = Convert.ToInt32(r.Field<object>("TicketID")),
                            ticketStatus = Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(r.Field<object>("StatusID"))),
                            Message = Convert.ToString(r.Field<object>("TicketDescription")),
                            Category = Convert.ToString(r.Field<object>("CategoryName")),
                            subCategory = Convert.ToString(r.Field<object>("SubCategoryName")),
                            IssueType = Convert.ToString(r.Field<object>("IssueTypeName")),
                            Priority = Convert.ToString(r.Field<object>("PriortyName")),
                            Assignee = Convert.ToString(r.Field<object>("AssignedName")),
                            CreatedOn = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedOn"))) ? string.Empty : Convert.ToString(r.Field<object>("CreatedOn")),
                            createdBy = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedByName"))) ? string.Empty : Convert.ToString(r.Field<object>("CreatedByName")),
                            createdago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("CreatedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("CreatedDate")), "CreatedSpan"),
                            assignedTo = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedName"))) ? string.Empty : Convert.ToString(r.Field<object>("AssignedName")),
                            assignedago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("AssignedDate")), "AssignedSpan"),
                            updatedBy = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("ModifyByName"))) ? string.Empty : Convert.ToString(r.Field<object>("ModifyByName")),
                            updatedago = string.IsNullOrEmpty(Convert.ToString(r.Field<object>("ModifiedDate"))) ? string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("ModifiedDate")), "ModifiedSpan"),

                            responseTimeRemainingBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityRespond")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "RespondTimeRemainingSpan"),
                            responseOverdueBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityRespond")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityRespond")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResponseOverDueSpan"),

                            resolutionOverdueBy = (string.IsNullOrEmpty(Convert.ToString(r.Field<object>("AssignedDate"))) || string.IsNullOrEmpty(Convert.ToString(r.Field<object>("PriorityResolve")))) ?
                            string.Empty : setCreationdetails(Convert.ToString(r.Field<object>("PriorityResolve")) + "|" + Convert.ToString(r.Field<object>("AssignedDate")), "ResolutionOverDueSpan"),

                            TaskStatus = Convert.ToString(r.Field<object>("TaskDetails")),
                            ClaimStatus = Convert.ToString(r.Field<object>("ClaimDetails")),
                            TicketCommentCount = Convert.ToInt32(r.Field<object>("TicketComments")),
                            isEscalation = Convert.ToInt32(r.Field<object>("IsEscalated")),
                            ticketSourceType = Convert.ToString(r.Field<object>("TicketSourceType")),
                            ticketSourceTypeID = Convert.ToInt16(r.Field<object>("TicketSourceTypeID")),
                            IsReassigned = Convert.ToBoolean(r.Field<object>("IsReassigned")),
                            IsSLANearBreach = Convert.ToBoolean(r.Field<object>("IsSLANearBreach"))
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
                //throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
            return objSearchResult;
        }


        /// <summary>
        /// Get tickets on the saved search params
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        /// 
        public TicketSaveSearch GetTicketsOnSavedSearch(int TenantID,int UserID, int SearchParamID)
        {

            string jsonSearchParams = string.Empty;
            DataSet ds = new DataSet();
            SearchModel searchModel = new SearchModel();
            TicketSaveSearch tktSearch = new TicketSaveSearch();
            List<SearchResponse> objSearchResult = new List<SearchResponse>();


            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_GetSaveSearchByID_UTSM", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@SearchParam_ID", SearchParamID);

                cmd.Parameters.AddWithValue("@searchFor", 2);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        jsonSearchParams = ds.Tables[0].Rows[0]["SearchParameters"] == DBNull.Value ? string.Empty: Convert.ToString(ds.Tables[0].Rows[0]["SearchParameters"]);
                    }
                }

                if (!string.IsNullOrEmpty(jsonSearchParams))
                {
                
                    searchModel = JsonConvert.DeserializeObject<SearchModel>(jsonSearchParams);

                    if (searchModel!= null)
                    {
                        searchModel.TenantID = TenantID;
                        searchModel.AssigntoId = UserID;
                        objSearchResult = GetTicketsOnSearch(searchModel);

                        tktSearch.ticketList = objSearchResult;
                        tktSearch.searchParams = jsonSearchParams;

                    }


                }

            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                //throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
            return tktSearch;
        }


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
