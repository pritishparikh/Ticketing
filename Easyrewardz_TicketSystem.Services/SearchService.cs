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

        public string[] SearchTickets(SearchRequest searchparams)
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

                if(!string.IsNullOrEmpty(searchparams.SLAstatus))
                {
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResponse", Convert.ToInt32(searchparams.SLAstatus.Split('|')[0].Split('-')[0]));
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResponsetime", 1);
                    sqlcmd.Parameters.AddWithValue("_SLAstatusResolution", Convert.ToInt32( searchparams.SLAstatus.Split('|')[1].Split('-')[0]));
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
                            creationDetails = setCreationdetails(r),
                            //creationDetails = JsonConvert.SerializeObject(ds.Tables[0])
                        }).ToList();
                    }
                }

                //objSearchResult[0].ticketStatusCount = ds.Tables[1].AsEnumerable()
                //    .Select(x => Enum.GetName(typeof(EnumMaster.TicketStatus), Convert.ToInt32(x.Field<object>("StatusID")))
                //        + "|" + Convert.ToString(Convert.ToInt32(x.Field<object>("StatusID")))).ToList();

                CountList= ds.Tables[1].AsEnumerable().Select(x => Enum.GetName(typeof(EnumMaster.TicketStatus), Convert.ToInt32(x.Field<object>("StatusID")))
                        + "|" + Convert.ToString(Convert.ToInt32(x.Field<object>("TicketStatusCount")))).ToList();


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
            return new string[] { JsonConvert.SerializeObject(objSearchResult),JsonConvert.SerializeObject(CountList) };
        }

        public TicketCreationDetails setCreationdetails(DataRow tkt )
        {
            string detail = string.Empty;
            DateTime now = DateTime.Now;
            TimeSpan diff = new TimeSpan();
            TimeSpan RespondT = new TimeSpan();
            TimeSpan ResolveT = new TimeSpan();
            TicketCreationDetails tktDetails = new TicketCreationDetails() ;
            
            try
            {

               tktDetails.createdBy= string.IsNullOrEmpty(Convert.ToString(tkt.Field<object>("CreatedByName"))) ? string.Empty : Convert.ToString(tkt.Field<object>("CreatedByName"));
               tktDetails.assignedTo= string.IsNullOrEmpty(Convert.ToString(tkt.Field<object>("AssignedName"))) ? null : Convert.ToString(tkt.Field<object>("AssignedName"));
               tktDetails.updatedBy = string.IsNullOrEmpty(Convert.ToString(tkt.Field<object>("ModifyByName"))) ? null : Convert.ToString(tkt.Field<object>("ModifyByName"));

                if(!string.IsNullOrEmpty(Convert.ToString(tkt.Field<object>("CreatedDate"))))
                {
                    diff = now - Convert.ToDateTime(tkt.Field<object>("CreatedDate"));
                    tktDetails.createdago = new TimeDetails() { Days = diff.Days, Hours = diff.Hours, Minutes = diff.Minutes, Seconds = diff.Seconds };
                    
                }
               

                if (!string.IsNullOrEmpty(Convert.ToString(tkt.Field<object>("AssignedDate"))))
                {
                    diff = now - Convert.ToDateTime(tkt.Field<object>("AssignedDate"));
                    tktDetails.assignedago = new TimeDetails() { Days = diff.Days, Hours = diff.Hours, Minutes = diff.Minutes, Seconds = diff.Seconds };

                    if (!string.IsNullOrEmpty(Convert.ToString(tkt.Field<object>("PriorityRespond"))))
                    {
                        string[] respondtime = Convert.ToString(tkt.Field<object>("PriorityRespond")).Split(new char[] { '-'});

                        switch(respondtime[1])
                        {
                            case "D":
                                RespondT = (Convert.ToDateTime(tkt.Field<object>("AssignedDate")).AddDays(Convert.ToDouble(respondtime[0])))-now;
                                
                                break;

                            case "H":
                                RespondT = (Convert.ToDateTime(tkt.Field<object>("AssignedDate")).AddHours(Convert.ToDouble(respondtime[0])))-now;
                               
                                break;

                            case "M":
                                RespondT = (Convert.ToDateTime(tkt.Field<object>("AssignedDate")).AddMinutes(Convert.ToDouble(respondtime[0])))-now;
                               
                                break;

                        }

                        if (RespondT.Minutes > 0)
                        {
                            tktDetails.responseTimeRemainingBy = new TimeDetails() { Days = diff.Days, Hours = diff.Hours, Minutes = diff.Minutes, Seconds = diff.Seconds };
                        }
                        else
                        {
                            tktDetails.responseOverdueBy= new TimeDetails() { Days = diff.Days, Hours = diff.Hours, Minutes = diff.Minutes, Seconds = diff.Seconds };
                        }
                        

                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(tkt.Field<object>("PriorityResolve"))))
                    {
                        string[] respondtime = Convert.ToString(tkt.Field<object>("PriorityResolve")).Split(new char[] { '-' });

                        switch (respondtime[1])
                        {
                            case "D":
                                ResolveT = now-(Convert.ToDateTime(tkt.Field<object>("AssignedDate")).AddDays(Convert.ToDouble(respondtime[0]))) ;

                                break;

                            case "H":
                                ResolveT = now-(Convert.ToDateTime(tkt.Field<object>("AssignedDate")).AddHours(Convert.ToDouble(respondtime[0]))) ;

                                break;

                            case "M":
                                ResolveT = now-(Convert.ToDateTime(tkt.Field<object>("AssignedDate")).AddMinutes(Convert.ToDouble(respondtime[0]))) ;

                                break;

                        }

                        if (ResolveT.Minutes > 0)
                        {
                            tktDetails.resolutionOverdueBy = new TimeDetails() { Days = diff.Days, Hours = diff.Hours, Minutes = diff.Minutes, Seconds = diff.Seconds };
                        }
                        
                    }
                }
                
                if (!string.IsNullOrEmpty(Convert.ToString(tkt.Field<object>("ModifiedDate"))))
                {
                    diff = now - Convert.ToDateTime(tkt.Field<object>("ModifiedDate"));
                    tktDetails.updatedago = new TimeDetails() { Days = diff.Days, Hours = diff.Hours, Minutes = diff.Minutes, Seconds = diff.Seconds };
                }
               
          

            }
            catch (Exception ex)
            {
                var test = ex.ToString() + "\n" + ex.StackTrace;
                tktDetails = null;
                
            }

            return tktDetails;

        }
        
    }
}
