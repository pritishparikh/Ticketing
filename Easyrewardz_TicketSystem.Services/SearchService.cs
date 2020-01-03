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
    public class SearchService: ISearchTicket
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
            List<string> CountList = new List<string>();
            try
            {
                conn.Open(); 
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_SearchTickets", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                
            
                //sqlcmd.Parameters.AddWithValue("@CategoryID", CategoryID);

                sqlcmd.Parameters.AddWithValue("_isByStatus", Convert.ToInt32(searchparams.isByStatus));
                sqlcmd.Parameters.AddWithValue("_isByFilter", Convert.ToInt32(searchparams.isByFilter));
                sqlcmd.Parameters.AddWithValue("_isByDate", Convert.ToInt32(searchparams.ByDate));
                sqlcmd.Parameters.AddWithValue("_isByCustomerType", Convert.ToInt32(searchparams.ByCustomerType));
                sqlcmd.Parameters.AddWithValue("_isByticketType", Convert.ToInt32(searchparams.ByTicketType));
                sqlcmd.Parameters.AddWithValue("_isByCategory", Convert.ToInt32(searchparams.ByCategory));
                sqlcmd.Parameters.AddWithValue("_isByAll", Convert.ToInt32(searchparams.byAll));
                sqlcmd.Parameters.AddWithValue("_createdDate", searchparams.creationDate);
                sqlcmd.Parameters.AddWithValue("_lastUpdatedDate", searchparams.lastUpdatedDate);
                sqlcmd.Parameters.AddWithValue("_SLADue", searchparams.SLADue);
                sqlcmd.Parameters.AddWithValue("_ticketStatus", (int)((EnumMaster.TicketStatus)Enum.Parse(typeof(EnumMaster.TicketStatus), searchparams.ticketStatus)));
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
                sqlcmd.Parameters.AddWithValue("_claimsubcCategory", searchparams.claimSubcategory);


                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                  
                    var tmpdt = ds.Tables[0];
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
                            CreatedOn = Convert.ToDateTime(r.Field<object>("CreatedDate")),
                           // creationDetails = Convert.ToString(r.Field<int>(""))
                        }).ToList();
                    }
                }

                objSearchResult[0].ticketStatusCount = ds.Tables[1].AsEnumerable()
                    .Select(x => Enum.GetName(typeof(EnumMaster.TicketStatus), Convert.ToInt32(x.Field<object>("StatusID")))
                        + "|" + Convert.ToString(Convert.ToInt32(x.Field<object>("StatusID")))).ToList();
            
            }
            catch (Exception ex)
            {
                var test = ex.ToString() + "\n" + ex.StackTrace;
                throw;
            }
            finally
            {
                
                if (ds != null) ds.Dispose(); conn.Close();
            }
            return objSearchResult;
        }

        public string setCreationdetails(DataRow tkt )
        {
            string detail = string.Empty;
            DateTime now = DateTime.Now;
            List<string> creationddetailsLst = new List<string>();

            string createdby = string.Empty; string createdago = string.Empty; string assignedto = string.Empty; string assignedago = string.Empty;
            string updatedby = string.Empty; string updatedago = string.Empty;
            string resptime = string.Empty; string responseoverdue = string.Empty; string resolnoverdue = string.Empty;

            try
            {
                if(tkt!=null && tkt.ItemArray.Length > 0)
                {
                    for (int i = 0; i < tkt.ItemArray.Length; i++)
                    {

                    }
                }
              
            }
            catch (Exception)
            {
                detail = string.Empty;
                throw;
            }

            return detail;

        }
        
    }
}
