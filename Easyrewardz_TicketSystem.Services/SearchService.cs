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

namespace Easyrewardz_TicketSystem.Services
{
    public class SearchService : ISearchTicket
    {
        #region DB connection
        MySqlConnection conn = new MySqlConnection();
        public SearchService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        public List<SearchResponse> SearchTickets(SearchRequest searchparams)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponse> objSearchResult = new List<SearchResponse>();
            List<SearchResponse> temp = new List<SearchResponse>(); //delete later
            List<string> CountList = new List<string>();

            int rowStart = (searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_SearchTickets", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("_isEscalated", Convert.ToInt32(searchparams.isEscalation));
                sqlcmd.Parameters.AddWithValue("_isByStatus", Convert.ToInt32(searchparams.isByStatus));
                sqlcmd.Parameters.AddWithValue("_isByFilter", Convert.ToInt32(searchparams.isByFilter));
                sqlcmd.Parameters.AddWithValue("_isByDate", Convert.ToInt32(searchparams.ByDate));
                sqlcmd.Parameters.AddWithValue("_isByCustomerType", Convert.ToInt32(searchparams.ByCustomerType));
                sqlcmd.Parameters.AddWithValue("_isByticketType", Convert.ToInt32(searchparams.ByTicketType));
                sqlcmd.Parameters.AddWithValue("_isByCategory", Convert.ToInt32(searchparams.ByCategory));
                sqlcmd.Parameters.AddWithValue("_isByAll", Convert.ToInt32(searchparams.byAll));
                sqlcmd.Parameters.AddWithValue("_tenantID", Convert.ToInt32(searchparams.tenantID));
                sqlcmd.Parameters.AddWithValue("_createdDate", searchparams.creationDate);
                sqlcmd.Parameters.AddWithValue("_lastUpdatedDate", searchparams.lastUpdatedDate);
                sqlcmd.Parameters.AddWithValue("_SLADue", searchparams.SLADue);
                sqlcmd.Parameters.AddWithValue("_ticketStatus", searchparams.ticketStatus);
                sqlcmd.Parameters.AddWithValue("_customerMob", searchparams.customerMob);
                sqlcmd.Parameters.AddWithValue("_customerMail", searchparams.customerEmail);
                sqlcmd.Parameters.AddWithValue("_ticketID", searchparams.TicketID);
                sqlcmd.Parameters.AddWithValue("_priority", searchparams.Priority);
                sqlcmd.Parameters.AddWithValue("_channelOfPurchase", searchparams.chanelOfPurchase);
                sqlcmd.Parameters.AddWithValue("_ticketActionType", searchparams.ticketActionType);
                sqlcmd.Parameters.AddWithValue("_categoryName", searchparams.Category);
                sqlcmd.Parameters.AddWithValue("_subCategoryName", searchparams.subCategory);
                sqlcmd.Parameters.AddWithValue("_issueType", searchparams.issueType);
                sqlcmd.Parameters.AddWithValue("_tikcetSource", searchparams.ticketSource);
                sqlcmd.Parameters.AddWithValue("_claimID", searchparams.claimID);
                sqlcmd.Parameters.AddWithValue("_ticketTitle", searchparams.ticketTitle);
                sqlcmd.Parameters.AddWithValue("_invoiceSubOrderNo", searchparams.invoiceSubOrderNo);
                sqlcmd.Parameters.AddWithValue("_assignedTo", searchparams.assignedTo);
                sqlcmd.Parameters.AddWithValue("_didVisitStore", Convert.ToInt32(searchparams.didVisitStore));
                sqlcmd.Parameters.AddWithValue("_itemID", Convert.ToInt32(searchparams.itemID));
                sqlcmd.Parameters.AddWithValue("_purchaseStorecoseAddr", searchparams.purchaseStoreCodeAddress);
                sqlcmd.Parameters.AddWithValue("_SLAstatus", searchparams.SLAstatus);
                sqlcmd.Parameters.AddWithValue("_wantToVisitStore", Convert.ToInt32(searchparams.wantToVisitStore));
                sqlcmd.Parameters.AddWithValue("_wantToVisitStoreCodeAddr", Convert.ToInt32(searchparams.wantToVisitStoreCodeAddress));
                sqlcmd.Parameters.AddWithValue("_withClaim", Convert.ToInt32(searchparams.withClaim));
                sqlcmd.Parameters.AddWithValue("_withTask", Convert.ToInt32(searchparams.withTask));
                sqlcmd.Parameters.AddWithValue("_claimStatus", searchparams.claimStatus);
                sqlcmd.Parameters.AddWithValue("_taskStatus", searchparams.taskStatus);
                sqlcmd.Parameters.AddWithValue("_claimCategory", searchparams.claimCategory);
                sqlcmd.Parameters.AddWithValue("_taskDept", searchparams.taskDept);
                sqlcmd.Parameters.AddWithValue("_taskFunction", searchparams.taskFunction);
                sqlcmd.Parameters.AddWithValue("_claimIssuetype", searchparams.claimIssuetype);
                sqlcmd.Parameters.AddWithValue("_claimsubCategory", searchparams.claimSubcategory);

                if (!string.IsNullOrEmpty(searchparams.SLAstatus))
                {
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResponse", Convert.ToInt32(searchparams.SLAstatus.Split('|')[0].Split('-')[0]));
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResponsetime", 1);
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResolution", Convert.ToInt32(searchparams.SLAstatus.Split('|')[1].Split('-')[0]));
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResoltiontime", 1);
                }
                else
                {
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResponse", 0);
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResponsetime", 0);
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResolution", 0);
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResoltiontime", 0);
                }


                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
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
                            TicketCommentCount = Convert.ToInt32(r.Field<object>("TicketComments")),
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
                if (ds != null) ds.Dispose(); conn.Close();
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
                conn.Open();
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
                conn.Close();
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
            List<string> CountList = new List<string>();

            //int rowStart = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                conn.Open();
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
                            isEscalation = Convert.ToInt32(r.Field<object>("IsEscalated"))

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
                if (ds != null) ds.Dispose(); conn.Close();
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

                sqlcmd.Parameters.AddWithValue("HeaderStatus_Id", searchModel.HeaderStatusId);

                if (searchModel.ActiveTabId == 1)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByDate";

                    sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_CreatedOn) ? "" : searchModel.searchDataByDate.Ticket_CreatedOn);
                    sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_ModifiedOn) ? "" : searchModel.searchDataByDate.Ticket_ModifiedOn);
                    sqlcmd.Parameters.AddWithValue("SLA_DueON", searchModel.searchDataByDate.SLA_DueON);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByDate.Ticket_StatusID);
                }
                else if (searchModel.ActiveTabId == 2)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByCustomerType";

                    sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerMobileNo) ? "" : searchModel.searchDataByCustomerType.CustomerMobileNo);
                    sqlcmd.Parameters.AddWithValue("CustomerEmailID", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerEmailID) ? "" : searchModel.searchDataByCustomerType.CustomerEmailID);
                    sqlcmd.Parameters.AddWithValue("Ticket_ID", searchModel.searchDataByCustomerType.TicketID==null?0:searchModel.searchDataByCustomerType.TicketID);
                    sqlcmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByCustomerType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 3)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByTicketType";

                    sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByTicketType.TicketPriorityID);
                    sqlcmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByTicketType.TicketStatusID);
                    sqlcmd.Parameters.AddWithValue("ChannelOfPurchaseIDs", string.IsNullOrEmpty(searchModel.searchDataByTicketType.ChannelOfPurchaseIds) ? "" : searchModel.searchDataByTicketType.ChannelOfPurchaseIds);
                    sqlcmd.Parameters.AddWithValue("ActionTypeIds", searchModel.searchDataByTicketType.ActionTypes);
                }
                else if (searchModel.ActiveTabId == 4)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByCategory";

                    sqlcmd.Parameters.AddWithValue("Cateogry_Id", searchModel.searchDataByCategoryType.CategoryId);
                    sqlcmd.Parameters.AddWithValue("SubCategory_Id",searchModel.searchDataByCategoryType.SubCategoryId);
                    sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByCategoryType.IssueTypeId);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByCategoryType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 5)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByAll";

                    /*Column 1 (5)*/
                    sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.CreatedDate) ? "" : searchModel.searchDataByAll.CreatedDate);
                    sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.ModifiedDate) ? "" : searchModel.searchDataByAll.ModifiedDate);
                    sqlcmd.Parameters.AddWithValue("Cateogry_Id", searchModel.searchDataByAll.CategoryId);
                    sqlcmd.Parameters.AddWithValue("SubCateogry_Id", searchModel.searchDataByAll.SubCategoryId);
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
                    sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.searchDataByAll.IsVisitStore);
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

                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("Assignto_Id", searchModel.AssigntoId);

                sqlcmd.CommandType = CommandType.StoredProcedure;

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
                            isEscalation = Convert.ToInt32(r.Field<object>("IsEscalated"))

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
                if (ds != null) ds.Dispose(); conn.Close();
            }
            return objSearchResult;
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
