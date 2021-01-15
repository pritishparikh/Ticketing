using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class TicketingService : ITicketing
    {


        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public TicketingService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Get Auto Suggest Ticket Title
        /// </summary>
        /// <param name="TicketTitle"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<TicketTitleDetails> GetTicketList(string TikcketTitle, int TenantId)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<TicketTitleDetails> ticketing = new List<TicketTitleDetails>();
            string ticketTitle = string.Empty;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getTitleSuggestions", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", TenantId);
                cmd1.Parameters.AddWithValue("@_ticketTitle", string.IsNullOrEmpty(TikcketTitle) ? "" : TikcketTitle);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TicketTitleDetails tktTitle = new TicketTitleDetails();
                        ticketTitle = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"])) ? string.Empty
                                        : Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]);

                        if (!string.IsNullOrEmpty(ticketTitle))
                        {
                            tktTitle.TicketTitle = ticketTitle.Length > 10 ? ticketTitle.Substring(0, 10) : ticketTitle;
                            tktTitle.TicketTitleToolTip = ticketTitle.Length > 10 ? ticketTitle : string.Empty;
                        }

                        ticketing.Add(tktTitle);
                      
                    }


                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                    conn.Close();

                if (ds != null)
                    ds.Dispose();
            }
            return ticketing;
        }

        /// <summary>
        /// Create Tickets
        /// <param name="TicketingDetails"></param>
        /// <param name="TenantId"></param>
        /// <param name="FolderPath"></param>
        /// /// <param name="finalAttchment"></param>
        /// </summary>

        public int addTicket(TicketingDetails ticketingDetails, int TenantId, string FolderPath, string finalAttchment)
        {
            MySqlCommand cmd = new MySqlCommand();
            int ticketID = 0;
            bool issentflag = false;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createTicket", conn);
                cmd1.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd1.Parameters.AddWithValue("@_TicketDescription", ticketingDetails.Ticketdescription);
                cmd1.Parameters.AddWithValue("@_TicketSourceID", ticketingDetails.TicketSourceID);
                cmd1.Parameters.AddWithValue("@_BrandID", ticketingDetails.BrandID);
                cmd1.Parameters.AddWithValue("@_CategoryID", ticketingDetails.CategoryID);
                cmd1.Parameters.AddWithValue("@_SubCategoryID", ticketingDetails.SubCategoryID);
                cmd1.Parameters.AddWithValue("@_PriorityID", ticketingDetails.PriorityID);
                cmd1.Parameters.AddWithValue("@_CustomerID", ticketingDetails.CustomerID);
                cmd1.Parameters.AddWithValue("@_OrderMasterID", ticketingDetails.OrderMasterID);
                cmd1.Parameters.AddWithValue("@_IssueTypeID", ticketingDetails.IssueTypeID);
                cmd1.Parameters.AddWithValue("@_ChannelOfPurchaseID", ticketingDetails.ChannelOfPurchaseID);
               
                cmd1.Parameters.AddWithValue("@_AssignedID", ticketingDetails.AssignedID);
                cmd1.Parameters.AddWithValue("@_TicketActionID", ticketingDetails.TicketActionID);

                cmd1.Parameters.AddWithValue("@_StatusID", ticketingDetails.StatusID);

                cmd1.Parameters.AddWithValue("@_TicketTemplateID", ticketingDetails.TicketTemplateID);

                cmd1.Parameters.AddWithValue("@_CreatedBy", ticketingDetails.IsGenFromStoreCamPaign ? -2 : ticketingDetails.CreatedBy);
                cmd1.Parameters.AddWithValue("@_Notes", ticketingDetails.Ticketnotes);

                cmd1.Parameters.AddWithValue("@_IsInstantEscalateToHighLevel", Convert.ToInt16(ticketingDetails.IsInstantEscalateToHighLevel));
                cmd1.Parameters.AddWithValue("@_IsWantToVisitedStore", Convert.ToInt16(ticketingDetails.IsWantToVisitedStore));
                cmd1.Parameters.AddWithValue("@_IsAlreadyVisitedStore", Convert.ToInt16(ticketingDetails.IsAlreadyVisitedStore));
                cmd1.Parameters.AddWithValue("@_IsWantToAttachOrder", Convert.ToInt16(ticketingDetails.IsWantToAttachOrder));
                cmd1.Parameters.AddWithValue("@_IsActive", Convert.ToInt16(ticketingDetails.IsActive));
                cmd1.Parameters.AddWithValue("@_OrderItemID", string.IsNullOrEmpty(ticketingDetails.OrderItemID) ? "" : ticketingDetails.OrderItemID);

                cmd1.Parameters.AddWithValue("@_TikcketTitle", string.IsNullOrEmpty(ticketingDetails.TicketTitle) ? "" : ticketingDetails.TicketTitle);
                cmd1.Parameters.AddWithValue("@_StoreID", string.IsNullOrEmpty(ticketingDetails.StoreID) ? "" : ticketingDetails.StoreID);
               

                if(ticketingDetails.ticketingMailerQues != null && ticketingDetails.ticketingMailerQues.Count > 0 )
                {
                    if(!string.IsNullOrEmpty(ticketingDetails.ticketingMailerQues[0].TicketMailBody))
                    {
                        issentflag = true;
                    }
                }
                cmd1.Parameters.AddWithValue("@_Is_Sent", Convert.ToInt16(issentflag));

                cmd1.CommandType = CommandType.StoredProcedure;


                ticketID = Convert.ToInt32(cmd1.ExecuteScalar());

                if (ticketingDetails.taskMasters.Count > 0)
                {
                    for (int j = 0; j < ticketingDetails.taskMasters.Count; j++)
                    {
                        int taskId = 0;
                        try
                        {

                           
                            MySqlCommand cmdtask = new MySqlCommand("SP_createTask", conn);
                            cmdtask.Connection = conn;
                            cmdtask.Parameters.AddWithValue("@Ticket_ID", ticketID);
                            cmdtask.Parameters.AddWithValue("@TaskTitle", ticketingDetails.taskMasters[j].TaskTitle);
                            cmdtask.Parameters.AddWithValue("@TaskDescription", ticketingDetails.taskMasters[j].TaskDescription);
                            cmdtask.Parameters.AddWithValue("@DepartmentId", ticketingDetails.taskMasters[j].DepartmentId);
                            cmdtask.Parameters.AddWithValue("@FunctionID", ticketingDetails.taskMasters[j].FunctionID);
                            cmdtask.Parameters.AddWithValue("@AssignTo_ID", ticketingDetails.taskMasters[j].AssignToID);
                            cmdtask.Parameters.AddWithValue("@PriorityID", ticketingDetails.taskMasters[j].PriorityID);
                            cmdtask.Parameters.AddWithValue("@Tenant_Id", TenantId);
                            cmdtask.Parameters.AddWithValue("@Created_By", ticketingDetails.CreatedBy);
                            cmdtask.CommandType = CommandType.StoredProcedure;
                            taskId = Convert.ToInt32(cmdtask.ExecuteScalar());
                           
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                    }
                }

                //Attchment 
                try
                {
                    int i = 0;
                  

                    if(!string.IsNullOrEmpty(finalAttchment))
                    {
                        MySqlCommand cmdattachment = new MySqlCommand("SP_SaveAttachment", conn);
                        cmdattachment.Parameters.AddWithValue("@fileName", finalAttchment);
                        cmdattachment.Parameters.AddWithValue("@Ticket_ID", ticketID);
                        cmdattachment.CommandType = CommandType.StoredProcedure;
                        i = cmdattachment.ExecuteNonQuery();
                    }
                   


                }
                catch (Exception)
                {
                    throw;
                }
                int a = 0;
              

                #region Fetch email based on storeID

                string storeIDlst = ticketingDetails.StoreID;
                if (!string.IsNullOrEmpty(storeIDlst) && ticketingDetails.IsInforToStore)
                {
                    string emailID = string.Empty;
                    List<string> emailLst = new List<string>();
                    DataSet EmailDs = new DataSet();
                    MySqlCommand cmdemail = new MySqlCommand("SP_GetEmailsOnStoreID", conn);
                    cmdemail.Parameters.AddWithValue("StoreIds", storeIDlst);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmdemail;
                    adapter.Fill(EmailDs);

                    if (EmailDs != null && EmailDs.Tables.Count > 0)
                    {
                        if (EmailDs.Tables[0] != null && EmailDs.Tables[0].Rows.Count > 0)
                        {
                            emailLst = EmailDs.Tables[0].AsEnumerable().Select(x => Convert.ToString((x.Field<object>("StoreEmailID")))).ToList();
                            emailLst.RemoveAll(x => string.IsNullOrEmpty(x));

                            if (emailLst.Count > 0)
                                emailID = string.Join(",", emailLst);

                        }
                    }

                    if (!string.IsNullOrEmpty(emailID))
                        ticketingDetails.ticketingMailerQues[0].UserCC += emailID;
                }




                #endregion


                if(!string.IsNullOrEmpty(ticketingDetails.ticketingMailerQues[0].TicketMailBody))
                {

                ticketingDetails.ticketingMailerQues[0].CreatedBy = ticketingDetails.CreatedBy;


                MySqlCommand cmdMail = new MySqlCommand("SP_SendTicketingEmail", conn);
                cmdMail.Parameters.AddWithValue("@Tenant_ID", ticketingDetails.TenantID);
                cmdMail.Parameters.AddWithValue("@Ticket_ID", ticketID);
                cmdMail.Parameters.AddWithValue("@TikcketMail_Subject", ticketingDetails.ticketingMailerQues[0].TikcketMailSubject);
                cmdMail.Parameters.AddWithValue("@TicketMail_Body", ticketingDetails.ticketingMailerQues[0].TicketMailBody);
                cmdMail.Parameters.AddWithValue("@To_Email", ticketingDetails.ticketingMailerQues[0].ToEmail);
                cmdMail.Parameters.AddWithValue("@User_CC", ticketingDetails.ticketingMailerQues[0].UserCC);
                cmdMail.Parameters.AddWithValue("@User_BCC", ticketingDetails.ticketingMailerQues[0].UserBCC);
                cmdMail.Parameters.AddWithValue("@Ticket_Source", ticketingDetails.ticketingMailerQues[0].TicketSource);
                cmdMail.Parameters.AddWithValue("@Alert_ID", ticketingDetails.ticketingMailerQues[0].AlertID);
                cmdMail.Parameters.AddWithValue("@Is_Sent", ticketingDetails.ticketingMailerQues[0].IsSent);
                cmdMail.Parameters.AddWithValue("@Priority_ID", ticketingDetails.ticketingMailerQues[0].PriorityID);
                cmdMail.Parameters.AddWithValue("@Created_By", ticketingDetails.ticketingMailerQues[0].CreatedBy);
                if (finalAttchment == null || finalAttchment == String.Empty)
                {
                    cmdMail.Parameters.AddWithValue("@Has_Attachment", 0);
                }
                else
                {
                    cmdMail.Parameters.AddWithValue("@Has_Attachment", 1);
                }

                cmdMail.CommandType = CommandType.StoredProcedure;
                a = Convert.ToInt32(cmdMail.ExecuteScalar());

                }

            }
            catch (Exception )
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

            return ticketID;
        }

        /// <summary>
        /// GetDraft
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<CustomDraftDetails> GetDraft(int UserID, int TenantId)
        {
            DataSet ds = new DataSet();
            List<CustomDraftDetails> Draftlist = new List<CustomDraftDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetDraft", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@TicketStatusID", (int)EnumMaster.TicketStatus.Draft);
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomDraftDetails DraftDetails = new CustomDraftDetails
                        {
                            TicketId = ds.Tables[0].Rows[i]["TicketId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketId"]),
                            TicketTitle = ds.Tables[0].Rows[i]["TikcketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TikcketTitle"]),
                            TicketDescription = ds.Tables[0].Rows[i]["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketDescription"]),
                            CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                            SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                            IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]),
                            CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]),
                            CustomerID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"]),
                            CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]),
                            AssignedID = ds.Tables[0].Rows[i]["AssignedID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AssignedID"]),
                        };
                       
                        Draftlist.Add(DraftDetails);
                    }
                }
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
            return Draftlist;
        }


        /// <summary>
        /// Search ticket agent
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        /// <param name="DesignationID"></param>
        /// <returns></returns>
        public List<CustomSearchTicketAgent> SearchAgent(string FirstName, string LastName, string Email, int DesignationID, int TenantId)
        {
            DataSet ds = new DataSet();
            List<CustomSearchTicketAgent> listSearchagent = new List<CustomSearchTicketAgent>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_SearchAgent", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@First_Name", FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", LastName);
                cmd.Parameters.AddWithValue("@Email_ID", Email);
                cmd.Parameters.AddWithValue("@Designation_ID", DesignationID);
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantId);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomSearchTicketAgent Searchagent = new CustomSearchTicketAgent();
                        Searchagent.User_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        Searchagent.AgentName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        Searchagent.Designation = Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);
                        Searchagent.Email = Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                        listSearchagent.Add(Searchagent);
                    }
                }
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
            return listSearchagent;
        }


        /// <summary>
        /// List Saved Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<UserTicketSearchMaster> ListSavedSearch(int UserID)
        {
            DataSet ds = new DataSet();
            List<UserTicketSearchMaster> listSavedSearch = new List<UserTicketSearchMaster>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetSearchUTSMList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@searchFor", 2);
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
                if (conn != null)
                {
                    conn.Close();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return listSavedSearch;
        }


        /// <summary>
        /// Get Saved Search By ID
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        public UserTicketSearchMaster GetSavedSearchByID(int SearchParamID)
        {
            DataSet ds = new DataSet();
            UserTicketSearchMaster Savedsearch = new UserTicketSearchMaster();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetSaveSearchByID_UTSM", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@SearchParam_ID", SearchParamID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Savedsearch.SearchParamID = Convert.ToInt32(ds.Tables[0].Rows[i]["SearchParamID"]);
                        Savedsearch.SearchParameters = Convert.ToString(ds.Tables[0].Rows[i]["SearchParameters"]);
                    }
                }
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return Savedsearch;
        }


        /// <summary>
        /// Soft Delete Saved Search
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <param name="UserID"></param>s
        /// <returns></returns>
        public int DeleteSavedSearch(int SearchParamID, int UserID)
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
        /// Add Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="SearchSaveName"></param>
        /// <param name="parameter"></param>s
        ///  <param name="parameter"></param>s
        public int AddSearch(int UserID, string SearchSaveName, string parameter, int TenantId)
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
                cmd.Parameters.AddWithValue("@searchFor", 2);
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
        /// AssignTicket
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="TenantID"></param>
        /// <param name="AgentID"></param>
        /// <param name="Remark"></param>

        public int AssignTicket(string TicketID, int TenantID, int UserID, int AgentID, string Remark)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_BulkTicketAssign", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Ticket_ID", TicketID);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Agent_ID", AgentID);
                cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(Remark) ? "" : Remark);
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
        /// Schedule
        /// </summary>
        /// <param name="ScheduleMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int Schedule(ScheduleMaster scheduleMaster, int TenantID, int UserID)
        {

            int i = 0;
            int scheduleID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddSchedule", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Report_Name", string.IsNullOrEmpty(scheduleMaster.ReportName) == true ? "" : scheduleMaster.ReportName);
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
                cmd.Parameters.AddWithValue("@Schedule_ID", "");
                cmd.Parameters["@Schedule_ID"].Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                i = cmd.ExecuteNonQuery();
                scheduleID = Convert.ToInt32(cmd.Parameters["@Schedule_ID"].Value);
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
        /// Get Auto Suggest Ticket Title
        /// </summary>
        /// <param name="TenantId"></param>
        /// 
        public List<TicketNotes> getNotesByTicketId(int TicketId)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<TicketNotes> ticketNotes = new List<TicketNotes>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getTitleNotes", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Ticket_Id", TicketId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TicketNotes ticket = new TicketNotes();
                        ticket.TicketNoteID = Convert.ToInt16(ds.Tables[0].Rows[i]["TicketNoteID"]);
                        ticket.Note = Convert.ToString(ds.Tables[0].Rows[i]["Note"]);
                        ticket.TicketID = Convert.ToInt16(ds.Tables[0].Rows[i]["TicketID"]);
                        ticket.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["CreatedByName"]);
                        ticketNotes.Add(ticket);
                    }
                }
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
                if(ds!=null)
                {
                    ds.Dispose();
                }
            }
            return ticketNotes;
        }


        /// <summary>
        /// update ticket status
        /// </summary>
        /// <param name="CustomTicketSolvedModel"></param>
        /// <param name="UserID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int submitticket(CustomTicketSolvedModel customTicketSolvedModel, int UserID, int TenantId)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateStatus", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Ticket_ID", customTicketSolvedModel.TicketID);
                cmd.Parameters.AddWithValue("@Priorty_ID", customTicketSolvedModel.PriortyID);
                cmd.Parameters.AddWithValue("@Status_ID", customTicketSolvedModel.StatusID);
                cmd.Parameters.AddWithValue("@Category_ID", customTicketSolvedModel.CategoryID);
                cmd.Parameters.AddWithValue("@SubCategory_ID", customTicketSolvedModel.SubCategoryID);
                cmd.Parameters.AddWithValue("@Brand_ID", customTicketSolvedModel.BrandID);
                cmd.Parameters.AddWithValue("@ChannelOfPurchase_ID", customTicketSolvedModel.ChannelOfPurchaseID);
                cmd.Parameters.AddWithValue("@IssueType_ID", customTicketSolvedModel.IssueTypeID);
                cmd.Parameters.AddWithValue("@TicketAction_ID", customTicketSolvedModel.TicketActionID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantId);
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
        /// Get Ticket Details By TicketId
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="TenantId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public CustomTicketDetail getTicketDetailsByTicketId(int TicketID, int TenantID, string url)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            CustomTicketDetail ticketDetails = new CustomTicketDetail();
            try
            {
                TicketingMailerQue ticketingMailerObj = new TicketingMailerQue();
                conn.Open();
                cmd.Connection = conn;
                
                MySqlCommand cmd1 = new MySqlCommand("SP_GetTicketDetailByID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Ticket_Id", TicketID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        ticketDetails.TicketID = ds.Tables[0].Rows[i]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]);
                        ticketDetails.TicketTitle = ds.Tables[0].Rows[i]["TikcketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TikcketTitle"]);
                        ticketDetails.Ticketdescription = ds.Tables[0].Rows[i]["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketDescription"]);
                        ticketDetails.CategoryID = ds.Tables[0].Rows[i]["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        ticketDetails.BrandID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        ticketDetails.SubCategoryID = ds.Tables[0].Rows[i]["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        ticketDetails.PriortyID = ds.Tables[0].Rows[i]["PriorityID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PriorityID"]);
                        ticketDetails.ChannelOfPurchaseID = ds.Tables[0].Rows[i]["ChannelOfPurchaseID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ChannelOfPurchaseID"]);
                        ticketDetails.IssueTypeID = ds.Tables[0].Rows[i]["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        ticketDetails.TicketActionTypeID = ds.Tables[0].Rows[i]["TicketActionID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketActionID"]);
                        ticketDetails.CustomerID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"]);
                        ticketDetails.CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        ticketDetails.CustomerEmailId = ds.Tables[0].Rows[i]["CustomerEmailId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerEmailId"]);
                        ticketDetails.CustomerPhoneNumber = ds.Tables[0].Rows[i]["CustomerPhoneNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerPhoneNumber"]);
                        ticketDetails.AltNumber = ds.Tables[0].Rows[i]["AltNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AltNumber"]);
                        ticketDetails.Username = ds.Tables[0].Rows[i]["Username"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Username"]);
                        ticketDetails.UpdateDate = ds.Tables[0].Rows[i]["UpdatedAt"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UpdatedAt"]);
                        ticketDetails.Status = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]);
                       
                        ticketDetails.AssignedID= ds.Tables[0].Rows[i]["AssignedID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AssignedID"]);
                        ticketDetails.UserEmailID = ds.Tables[0].Rows[i]["UserEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserEmailID"]);
                        ticketDetails.TicketAssignDate = ds.Tables[0].Rows[i]["TicketAssignDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketAssignDate"]);
                       

                        ticketDetails.OpenTicket = ds.Tables[0].Rows[i]["OpenTickets"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OpenTickets"]);
                        ticketDetails.Totalticket = ds.Tables[0].Rows[i]["Totaltickets"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Totaltickets"]);

                        ticketDetails.TotalTask = ds.Tables[0].Rows[i]["TotalTask"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TotalTask"]);

                        ticketDetails.RoleID = ds.Tables[0].Rows[i]["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"]);
                        ticketDetails.RoleName = ds.Tables[0].Rows[i]["RoleName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RoleName"]);

                        ticketDetails.stores = ds.Tables[1].AsEnumerable().Select(x => new Store()
                        {
                            StoreID = Convert.ToInt32(x.Field<int>("StoreID")),
                            Storename = x.Field<object>("StoreName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("StoreName"))

                        }).ToList();

                        ticketDetails.products = ds.Tables[2].AsEnumerable().Select(x => new Product()
                        {
                            ItemID = x.Field<object>("OrderItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("OrderItemID")),
                            InvoiceNumber = x.Field<object>("InvoiceNo") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("InvoiceNo"))

                        }).ToList();

                        ticketDetails.attachment = ds.Tables[3].AsEnumerable().Select(x => new Attachment()
                        {
                            TicketAttachmentId = Convert.ToInt32(x.Field<int>("TicketAttachmentId")),
                            AttachmentName = x.Field<object>("AttachmentName") == System.DBNull.Value || string.IsNullOrEmpty(Convert.ToString(x.Field<object>("AttachmentName")) ) ? string.Empty : url + "/" + Convert.ToString(x.Field<object>("AttachmentName"))
                        }).ToList();

                        if (ds != null && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                        {
                            ticketingMailerObj.MailID = ds.Tables[4].Rows[0]["MailID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[4].Rows[0]["MailID"]);
                            ticketingMailerObj.TikcketMailSubject = ds.Tables[4].Rows[0]["TikcketMailSubject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[4].Rows[0]["TikcketMailSubject"]);
                            ticketingMailerObj.TicketMailBody = ds.Tables[4].Rows[0]["TicketMailBody"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[4].Rows[0]["TicketMailBody"]);
                            ticketingMailerObj.ToEmail = ds.Tables[4].Rows[0]["ToEmail"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[4].Rows[0]["ToEmail"]);
                            ticketingMailerObj.UserCC = ds.Tables[4].Rows[0]["UserCC"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[4].Rows[0]["UserCC"]);
                            ticketingMailerObj.UserBCC = ds.Tables[4].Rows[0]["UserBCC"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[4].Rows[0]["UserBCC"]);
                            ticketingMailerObj.IsCustomerComment = ds.Tables[4].Rows[0]["IsCustomerComment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[4].Rows[0]["IsCustomerComment"]);
                            ticketingMailerObj.AlertID = ds.Tables[4].Rows[0]["AlertID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[4].Rows[0]["AlertID"]);
                        }
                        ticketDetails.ticketingMailerQue = ticketingMailerObj;

                        if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                        {
                            ticketDetails.TargetClosuredate = ds.Tables[5].Rows[0]["TargetClosuredate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[5].Rows[0]["TargetClosuredate"]);
                            ticketDetails.DurationRemaining = ds.Tables[5].Rows[0]["DaysRemaining"] == DBNull.Value ? "0 Day 0 Hour" : Convert.ToString(ds.Tables[5].Rows[0]["DaysRemaining"]);
                            
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
                if (conn != null)
                {
                    conn.Close();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return ticketDetails;

        }
        /// <summary>
        /// Get Ticket History
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        public List<CustomTicketHistory> GetTicketHistory(int TicketID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomTicketHistory> ListTicketHistory = new List<CustomTicketHistory>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetHistoryOfTicket", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Ticket_Id", TicketID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomTicketHistory TicketHistory = new CustomTicketHistory();
                        TicketHistory.Name = ds.Tables[0].Rows[i]["Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        TicketHistory.Action = ds.Tables[0].Rows[i]["Action"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Action"]);
                        TicketHistory.DateandTime = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        ListTicketHistory.Add(TicketHistory);
                    }
                }
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return ListTicketHistory;
        }

        public bool SendMail(SMTPDetails sMTPDetails, string mailTo, string cc, string bcc, string subject, string mailBody, bool informStore, string storeIDs, int TenantID)
        {
            bool IsMailSent = false;
            DataSet EmailDs = new DataSet();
            CommonService commonService = new CommonService();
            List<string> ccMailList = new List<string>(); List<string> StoreEmailList = new List<string>();
            string[] bccMailArray = null;
            try
            {

                #region get email based on storeid

                if (informStore && !string.IsNullOrEmpty(storeIDs))
                {
                    MySqlCommand cmdemail = new MySqlCommand("SP_GetEmailsOnStoreID", conn);
                    cmdemail.Parameters.AddWithValue("StoreIds", storeIDs);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmdemail;
                    adapter.Fill(EmailDs);

                    if (EmailDs != null && EmailDs.Tables.Count > 0)
                    {
                        if (EmailDs.Tables[0] != null && EmailDs.Tables[0].Rows.Count > 0)
                        {
                            StoreEmailList = EmailDs.Tables[0].AsEnumerable().Where(x => string.IsNullOrEmpty(Convert.ToString((x.Field<object>("StoreEmailID"))))).
                            Select(x => Convert.ToString((x.Field<object>("StoreEmailID")))).ToList();

                        }
                    }

                }

                #endregion


                if (!string.IsNullOrEmpty(cc))
                    ccMailList = cc.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (ccMailList.Count > 0)
                {
                    ccMailList.AddRange(StoreEmailList);
                }

                if (!string.IsNullOrEmpty(bcc))
                    bccMailArray = bcc.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


                IsMailSent = commonService.SendEmail(sMTPDetails, mailTo, subject, mailBody,
                    ccMailList.Count > 0 ? ccMailList.ToArray() : null, bccMailArray, TenantID);



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
                if (EmailDs != null)
                {
                    EmailDs.Dispose();
                }
            }

            return IsMailSent;
        }

        public CustomCountByTicket GetCountByTicket(int ticketID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            CustomCountByTicket CountByTicket = new CustomCountByTicket();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getCountDataByTicketID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Ticket_ID", ticketID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    CountByTicket.Messages = Convert.ToInt32(ds.Tables[0].Rows[0]["counts"]);
                    CountByTicket.Notes = Convert.ToInt32(ds.Tables[0].Rows[1]["counts"]);
                    CountByTicket.Task = Convert.ToInt32(ds.Tables[0].Rows[2]["counts"]);
                    CountByTicket.Claim = Convert.ToInt32(ds.Tables[0].Rows[3]["counts"]);
                }
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return CountByTicket;
        }

        public List<TicketMessage> TicketMessagelisting(int ticketID, int TenantID,string url)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<TicketMessage> ticketMessages = new List<TicketMessage>();
            List<CustomTicketMessage> TrailTicketMessagelist = new List<CustomTicketMessage>();
            List<MessageDetails> LatestTicketMessagelist = new List<MessageDetails>();      

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetTicketMessage", conn);               
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Ticket_Id", ticketID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {



                    //get trail messages date wise
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CustomTicketMessage TicketMessageDetails = new CustomTicketMessage();
                            TicketMessageDetails.MailID = Convert.ToInt32(ds.Tables[0].Rows[i]["MailID"]);
                            TicketMessageDetails.LatestMessageID = Convert.ToInt32(ds.Tables[0].Rows[i]["ThreadLatestID"]);
                            TicketMessageDetails.TicketID = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]);
                            TicketMessageDetails.TicketMailSubject = ds.Tables[0].Rows[i]["TikcketMailSubject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TikcketMailSubject"]);
                            TicketMessageDetails.TicketMailBody = ds.Tables[0].Rows[i]["TicketMailBody"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketMailBody"]);
                            TicketMessageDetails.IsCustomerComment = Convert.ToInt32(ds.Tables[0].Rows[i]["IsCustomerComment"]);
                            TicketMessageDetails.HasAttachment = ds.Tables[0].Rows[i]["HasAttachment"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["HasAttachment"]);
                            TicketMessageDetails.TicketSourceID = ds.Tables[0].Rows[i]["TicketSource"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketSource"]);
                            TicketMessageDetails.TicketSourceName = ds.Tables[0].Rows[i]["TicketSourceName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketSourceName"]);
                            TicketMessageDetails.CommentBy = ds.Tables[0].Rows[i]["CommentBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CommentBy"]);
                            TicketMessageDetails.DayOfCreation = ds.Tables[0].Rows[i]["DayOfCreation"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DayOfCreation"]);
                            TicketMessageDetails.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                            TicketMessageDetails.IsInternalComment = ds.Tables[0].Rows[i]["IsInternalComment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsInternalComment"]);

                            TicketMessageDetails.IsSystemGenerated = Convert.ToInt32(ds.Tables[0].Rows[i]["IsSystemGenerated"]) > 0;

                            TrailTicketMessagelist.Add(TicketMessageDetails);

                        }

                    }
                    // ends here

                    // get Latest Message in the thread
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            MessageDetails MsgDetails = new MessageDetails();
                            CustomTicketMessage TicketMessageDetails = new CustomTicketMessage();
                            TicketMessageDetails.MailID = Convert.ToInt32(ds.Tables[1].Rows[i]["MailID"]);
                            TicketMessageDetails.TicketID = Convert.ToInt32(ds.Tables[1].Rows[i]["TicketID"]);
                            TicketMessageDetails.TicketMailSubject = ds.Tables[1].Rows[i]["TikcketMailSubject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["TikcketMailSubject"]);
                            TicketMessageDetails.TicketMailBody = ds.Tables[1].Rows[i]["TicketMailBody"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["TicketMailBody"]);
                            TicketMessageDetails.IsCustomerComment = Convert.ToInt32(ds.Tables[1].Rows[i]["IsCustomerComment"]);
                            TicketMessageDetails.HasAttachment = ds.Tables[1].Rows[i]["HasAttachment"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["HasAttachment"]);
                            TicketMessageDetails.TicketSourceID = ds.Tables[1].Rows[i]["TicketSource"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["TicketSource"]);
                            TicketMessageDetails.TicketSourceName = ds.Tables[1].Rows[i]["TicketSourceName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["TicketSourceName"]);
                            TicketMessageDetails.CommentBy = ds.Tables[1].Rows[i]["CommentBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["CommentBy"]);
                            TicketMessageDetails.DayOfCreation = ds.Tables[1].Rows[i]["DayOfCreation"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["DayOfCreation"]);
                            TicketMessageDetails.CreatedDate = ds.Tables[1].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["CreatedDate"]);
                            TicketMessageDetails.IsInternalComment = ds.Tables[1].Rows[i]["IsInternalComment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[1].Rows[i]["IsInternalComment"]);
                            TicketMessageDetails.OldAgentID= ds.Tables[1].Rows[i]["OldAgentID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["OldAgentID"]);
                            TicketMessageDetails.OldAgentName = ds.Tables[1].Rows[i]["OldAgentName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["OldAgentName"]);
                            TicketMessageDetails.NewAgentID = ds.Tables[1].Rows[i]["NewAgentID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["NewAgentID"]);
                            TicketMessageDetails.NewAgentName = ds.Tables[1].Rows[i]["NewAgentName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["NewAgentName"]);
                            TicketMessageDetails.IsSystemGenerated = Convert.ToInt32(ds.Tables[1].Rows[i]["IsSystemGenerated"]) > 0;

                            if (TicketMessageDetails.OldAgentID>0)
                            {
                                TicketMessageDetails.IsReAssign = true;
                            }
                               

                            TicketMessageDetails.messageAttachments = ds.Tables[3].AsEnumerable().Where(x => x.Field<object>("TicketMessageID")!= DBNull.Value && Convert.ToInt32(x.Field<int>("TicketMessageID")).
                         Equals(TicketMessageDetails.MailID)).Select(x => new MessageAttachment()
                         {
                             MessageAttachmentId= x.Field<object>("MessageAttachmentId") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("MessageAttachmentId")),
                             AttachmentName = x.Field<object>("AttachmentName") == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(x.Field<object>("AttachmentName"))) ? string.Empty : url + "/" + Convert.ToString(x.Field<object>("AttachmentName")),
                             TicketMessageID= x.Field<object>("TicketMessageID") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("TicketMessageID"))
                         }).ToList();

                            MsgDetails.LatestMessageDetails = TicketMessageDetails;
                            MsgDetails.TrailMessageDetails = TrailTicketMessagelist
                                    .Where(x => x.LatestMessageID > 0 && x.LatestMessageID.Equals(MsgDetails.LatestMessageDetails.MailID)).ToList();


                            LatestTicketMessagelist.Add(MsgDetails);


                        }
                    }

                    // ends here



                    //get message grouped by date and bind the latest message with trail message
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {

                            TicketMessage ticketMessage = new TicketMessage();
                            ticketMessage.MessageCount = Convert.ToInt32(ds.Tables[2].Rows[i]["MessageCount"]);
                            ticketMessage.MessageDate = ds.Tables[2].Rows[i]["MessageDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[i]["MessageDate"]);

                            if (!string.IsNullOrEmpty(ticketMessage.MessageDate) && LatestTicketMessagelist.Count > 0)
                            {
                                ticketMessage.DayOfCreation = LatestTicketMessagelist.Where(x => !string.IsNullOrEmpty(x.LatestMessageDetails.CreatedDate) &&
                                   x.LatestMessageDetails.CreatedDate.Equals(ticketMessage.MessageDate)).Select(x => x.LatestMessageDetails.DayOfCreation).FirstOrDefault();
                                ticketMessage.MsgDetails = LatestTicketMessagelist.Where(x => !string.IsNullOrEmpty(x.LatestMessageDetails.CreatedDate) &&
                                 x.LatestMessageDetails.CreatedDate.Equals(ticketMessage.MessageDate)).ToList();
                            }

                            ticketMessages.Add(ticketMessage);
                        }

                    }
                    //ends here

                }
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return ticketMessages;
        }

        public List<CustomSearchTicketAgent> GetAgentList(int TenantID,int TicketID)
        {

            DataSet ds = new DataSet();
            List<CustomSearchTicketAgent> listSearchagent = new List<CustomSearchTicketAgent>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetAgentList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Ticket_ID", TicketID);
                cmd.Connection = conn;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomSearchTicketAgent Searchagent = new CustomSearchTicketAgent();
                        Searchagent.User_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        Searchagent.AgentName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        Searchagent.Designation = ds.Tables[0].Rows[i]["DesignationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);
                        listSearchagent.Add(Searchagent);
                    }
                }
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return listSearchagent;
        }

        public int CommentOnTicketDetail(TicketingMailerQue ticketingMailerQue, string finalAttchment)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmdMail = new MySqlCommand("SP_CommentOnMessage", conn);
                cmdMail.Parameters.AddWithValue("@Tenant_ID", ticketingMailerQue.TenantID);
                cmdMail.Parameters.AddWithValue("@Ticket_ID", ticketingMailerQue.TicketID);
                cmdMail.Parameters.AddWithValue("@TikcketMail_Subject", ticketingMailerQue.TikcketMailSubject);
                cmdMail.Parameters.AddWithValue("@TicketMail_Body", ticketingMailerQue.TicketMailBody);
                cmdMail.Parameters.AddWithValue("@To_Email", ticketingMailerQue.ToEmail);
                cmdMail.Parameters.AddWithValue("@User_CC", ticketingMailerQue.UserCC);
                cmdMail.Parameters.AddWithValue("@User_BCC", ticketingMailerQue.UserBCC);
                cmdMail.Parameters.AddWithValue("@Ticket_Source", ticketingMailerQue.TicketSource); 
                cmdMail.Parameters.AddWithValue("@Is_Sent", ticketingMailerQue.IsSent);
                cmdMail.Parameters.AddWithValue("@Is_CustomerComment", ticketingMailerQue.IsCustomerComment);
                cmdMail.Parameters.AddWithValue("@Created_By", ticketingMailerQue.CreatedBy);
                cmdMail.Parameters.AddWithValue("@ReplyMail_ID", ticketingMailerQue.MailID);
                cmdMail.Parameters.AddWithValue("@Is_InternalComment", ticketingMailerQue.IsInternalComment);
                cmdMail.Parameters.AddWithValue("@Is_ResponseToCustomer", ticketingMailerQue.IsResponseToCustomer);
                cmdMail.Parameters.AddWithValue("@fileName", string.IsNullOrEmpty(finalAttchment) ? "" : finalAttchment);
                cmdMail.Parameters.AddWithValue("@Has_Attachment",string.IsNullOrEmpty(finalAttchment) ? 0 : 1);
                cmdMail.Parameters.AddWithValue("@IsInformTo_Store", ticketingMailerQue.IsInformToStore);
                cmdMail.Parameters.AddWithValue("@Store_IDs", string.IsNullOrEmpty(ticketingMailerQue.StoreID) ? "" : ticketingMailerQue.StoreID);
                cmdMail.Parameters.AddWithValue("@OldAgent_ID", ticketingMailerQue.OldAgentID);
                cmdMail.Parameters.AddWithValue("@NewAgent_ID", ticketingMailerQue.NewAgentID);
                cmdMail.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmdMail.ExecuteNonQuery());
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
        /// Get ProgressBar Details
        /// <param name="TicketID"></param>
        /// <param name="TenantID"></param>
        /// </summary>
        public ProgressBarDetail GetProgressBarDetails(int TicketID, int TenantID)
        {
            ProgressBarDetail progressBarDetail = new ProgressBarDetail();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmdMail = new MySqlCommand("sp_getProgressBarDetailByTicketID", conn);
                cmdMail.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmdMail.Parameters.AddWithValue("@Ticket_ID", TicketID);
                cmdMail.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdMail;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        progressBarDetail.ProgressFirstPercentage = Convert.ToInt16(ds.Tables[0].Rows[0]["ProgessFirst"]);
                        progressBarDetail.ProgressFirstColorCode = Convert.ToString(ds.Tables[0].Rows[0]["ProgessFirstColorCode"]);
                        progressBarDetail.ProgressSecondPercentage = Convert.ToInt16(ds.Tables[0].Rows[0]["ProgessSecond"]);
                        progressBarDetail.ProgressSecondColorCode = Convert.ToString(ds.Tables[0].Rows[0]["ProgessSecondColorCode"]);
                    }
                }
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return progressBarDetail;
        }


        /// <summary>
        /// Set ticket Assignment for the followup
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="FollowUPUserID"></param>
        /// <param name="UserID"></param>
        public void setticketassigforfollowup(int TicketID, string FollowUPUserID, int UserID)
        {
            try
            {
                conn.Open();
                MySqlCommand cmdMail = new MySqlCommand("SP_assignTicketFollowUP", conn);
                
                cmdMail.Parameters.AddWithValue("@Ticket_ID", TicketID);
                cmdMail.Parameters.AddWithValue("@FollowUPUserIDs", FollowUPUserID);
                cmdMail.Parameters.AddWithValue("@User_ID", UserID);
                cmdMail.CommandType = CommandType.StoredProcedure;
                cmdMail.ExecuteScalar();
                
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

        }


        /// <summary>
        /// Get ticket Ids which show in Follow up
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string getticketsforfollowup(int UserID)
        {
            string ticketIds = "";
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_getTicketIdsForFollowUP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Connection = conn;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ticketIds = ticketIds + Convert.ToString(ds.Tables[0].Rows[i][0]) + ",";
                        }

                        if (!string.IsNullOrEmpty(ticketIds))
                        {
                            ticketIds = ticketIds.TrimEnd(',');
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
                if (conn != null)
                {
                    conn.Close();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return ticketIds;
        }

        /// <summary>
        /// Un-assign ticketids from the follow up
        /// </summary>
        /// <param name="TicketIDs"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool ticketunassigfromfollowup(string TicketIDs, int UserID)
        {
            bool isUpdate = false;
            try
            {
                conn.Open();
                MySqlCommand cmdMail = new MySqlCommand("SP_updateTicketFollowUP", conn);

                cmdMail.Parameters.AddWithValue("@Ticket_IDs", TicketIDs);
                cmdMail.Parameters.AddWithValue("@User_ID", UserID);
                cmdMail.CommandType = CommandType.StoredProcedure;
                cmdMail.ExecuteScalar();
                isUpdate = true;
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
            return isUpdate;
        }



        public int UpdateDraftTicket(TicketingDetails ticketingDetails, int TenantId, string FolderPath, string finalAttchment)
        {
            MySqlCommand cmd = new MySqlCommand();
            int ticketID = ticketingDetails.TicketID;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_UpdateDraftTicket", conn);
                cmd1.Parameters.AddWithValue("@_TicketID", ticketingDetails.TicketID);
                cmd1.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd1.Parameters.AddWithValue("@_TicketDescription", ticketingDetails.Ticketdescription);
                cmd1.Parameters.AddWithValue("@_TicketSourceID", ticketingDetails.TicketSourceID);
                cmd1.Parameters.AddWithValue("@_BrandID", ticketingDetails.BrandID);
                cmd1.Parameters.AddWithValue("@_CategoryID", ticketingDetails.CategoryID);
                cmd1.Parameters.AddWithValue("@_SubCategoryID", ticketingDetails.SubCategoryID);
                cmd1.Parameters.AddWithValue("@_PriorityID", ticketingDetails.PriorityID);
                cmd1.Parameters.AddWithValue("@_CustomerID", ticketingDetails.CustomerID);
                cmd1.Parameters.AddWithValue("@_OrderMasterID", ticketingDetails.OrderMasterID);
                cmd1.Parameters.AddWithValue("@_IssueTypeID", ticketingDetails.IssueTypeID);
                cmd1.Parameters.AddWithValue("@_ChannelOfPurchaseID", ticketingDetails.ChannelOfPurchaseID);
               
                cmd1.Parameters.AddWithValue("@_AssignedID", ticketingDetails.AssignedID);
                cmd1.Parameters.AddWithValue("@_TicketActionID", ticketingDetails.TicketActionID);

                cmd1.Parameters.AddWithValue("@_StatusID", ticketingDetails.StatusID);

                cmd1.Parameters.AddWithValue("@_TicketTemplateID", ticketingDetails.TicketTemplateID);

                cmd1.Parameters.AddWithValue("@_CreatedBy", ticketingDetails.CreatedBy);
                cmd1.Parameters.AddWithValue("@_Notes", ticketingDetails.Ticketnotes);

                cmd1.Parameters.AddWithValue("@_IsInstantEscalateToHighLevel", Convert.ToInt16(ticketingDetails.IsInstantEscalateToHighLevel));
                cmd1.Parameters.AddWithValue("@_IsWantToVisitedStore", Convert.ToInt16(ticketingDetails.IsWantToVisitedStore));
                cmd1.Parameters.AddWithValue("@_IsAlreadyVisitedStore", Convert.ToInt16(ticketingDetails.IsAlreadyVisitedStore));
                cmd1.Parameters.AddWithValue("@_IsWantToAttachOrder", Convert.ToInt16(ticketingDetails.IsWantToAttachOrder));
                cmd1.Parameters.AddWithValue("@_IsActive", Convert.ToInt16(ticketingDetails.IsActive));
                cmd1.Parameters.AddWithValue("@_OrderItemID", string.IsNullOrEmpty(ticketingDetails.OrderItemID) ? "" : ticketingDetails.OrderItemID);

                cmd1.Parameters.AddWithValue("@_TikcketTitle", string.IsNullOrEmpty(ticketingDetails.TicketTitle) ? "" : ticketingDetails.TicketTitle);
                cmd1.Parameters.AddWithValue("@_StoreID", string.IsNullOrEmpty(ticketingDetails.StoreID) ? "" : ticketingDetails.StoreID);
                // added for mailer check 
                cmd1.Parameters.AddWithValue("@_Is_Sent", Convert.ToInt16(!string.IsNullOrEmpty(ticketingDetails.ticketingMailerQues[0].TicketMailBody)));

                cmd1.CommandType = CommandType.StoredProcedure;


                 Convert.ToInt32(cmd1.ExecuteScalar());

                if (ticketingDetails.taskMasters.Count > 0)
                {
                    for (int j = 0; j < ticketingDetails.taskMasters.Count; j++)
                    {
                        int taskId = 0;
                        try
                        {

                          
                            MySqlCommand cmdtask = new MySqlCommand("SP_UpdateDraftTask", conn);
                            cmdtask.Connection = conn;
                            cmdtask.Parameters.AddWithValue("@Ticket_ID", ticketingDetails.TicketID);
                            cmdtask.Parameters.AddWithValue("@_TicketingTaskID", ticketingDetails.taskMasters[j].TicketingTaskID);
                            cmdtask.Parameters.AddWithValue("@_TaskTitle", ticketingDetails.taskMasters[j].TaskTitle);
                            cmdtask.Parameters.AddWithValue("@_TaskDescription", ticketingDetails.taskMasters[j].TaskDescription);
                            cmdtask.Parameters.AddWithValue("@_DepartmentId", ticketingDetails.taskMasters[j].DepartmentId);
                            cmdtask.Parameters.AddWithValue("@_FunctionID", ticketingDetails.taskMasters[j].FunctionID);
                            cmdtask.Parameters.AddWithValue("@_AssignTo_ID", ticketingDetails.taskMasters[j].AssignToID);
                            cmdtask.Parameters.AddWithValue("@_PriorityID", ticketingDetails.taskMasters[j].PriorityID);
                            cmdtask.Parameters.AddWithValue("@Tenant_Id", TenantId);
                            cmdtask.Parameters.AddWithValue("@_Created_By", ticketingDetails.CreatedBy);
                            cmdtask.Parameters.AddWithValue("@_StatusID", ticketingDetails.StatusID);
                            cmdtask.CommandType = CommandType.StoredProcedure;
                            taskId = Convert.ToInt32(cmdtask.ExecuteScalar());
                           
                        }
                        catch (Exception ex)
                        {
                            ticketID = 0;
                            throw ex;
                        }

                    }
                }

                //Attchment 
                try
                {
                    int i = 0;

                    if (!string.IsNullOrEmpty(finalAttchment))
                    {
                        MySqlCommand cmdattachment = new MySqlCommand("SP_UpdateDraftAttachment", conn);
                        cmdattachment.Parameters.AddWithValue("@fileName", finalAttchment);
                        cmdattachment.Parameters.AddWithValue("@Ticket_ID", ticketingDetails.TicketID);
                        cmdattachment.CommandType = CommandType.StoredProcedure;
                        i = cmdattachment.ExecuteNonQuery();
                    }
                }
                catch (IOException)
                {
                    ticketID = 0;
                }
                int a = 0;


                #region Fetch email based on storeID

                string storeIDlst = ticketingDetails.StoreID;
                if (!string.IsNullOrEmpty(storeIDlst) && ticketingDetails.IsInforToStore)
                {
                    string emailID = string.Empty;
                    List<string> emailLst = new List<string>();
                    DataSet EmailDs = new DataSet();
                    MySqlCommand cmdemail = new MySqlCommand("SP_GetEmailsOnStoreID", conn);
                    cmdemail.Parameters.AddWithValue("StoreIds", storeIDlst);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmdemail;
                    adapter.Fill(EmailDs);

                    if (EmailDs != null && EmailDs.Tables.Count > 0)
                    {
                        if (EmailDs.Tables[0] != null && EmailDs.Tables[0].Rows.Count > 0)
                        {
                            emailLst = EmailDs.Tables[0].AsEnumerable().Select(x => Convert.ToString((x.Field<object>("StoreEmailID")))).ToList();
                            emailLst.RemoveAll(x => string.IsNullOrEmpty(x));

                            if (emailLst.Count > 0)
                                emailID = string.Join(",", emailLst);

                        }
                    }

                    if (!string.IsNullOrEmpty(emailID))
                        ticketingDetails.ticketingMailerQues[0].UserCC += emailID;
                }

                #endregion


                if (!string.IsNullOrEmpty(ticketingDetails.ticketingMailerQues[0].TicketMailBody) && ticketingDetails.StatusID != 100)
                {

                    ticketingDetails.ticketingMailerQues[0].CreatedBy = ticketingDetails.CreatedBy;


                    MySqlCommand cmdMail = new MySqlCommand("SP_SendTicketingEmail", conn);
                    cmdMail.Parameters.AddWithValue("@Tenant_ID", ticketingDetails.TenantID);
                    cmdMail.Parameters.AddWithValue("@Ticket_ID", ticketingDetails.TicketID);
                    cmdMail.Parameters.AddWithValue("@TikcketMail_Subject", ticketingDetails.ticketingMailerQues[0].TikcketMailSubject);
                    cmdMail.Parameters.AddWithValue("@TicketMail_Body", ticketingDetails.ticketingMailerQues[0].TicketMailBody);
                    cmdMail.Parameters.AddWithValue("@To_Email", ticketingDetails.ticketingMailerQues[0].ToEmail);
                    cmdMail.Parameters.AddWithValue("@User_CC", ticketingDetails.ticketingMailerQues[0].UserCC);
                    cmdMail.Parameters.AddWithValue("@User_BCC", ticketingDetails.ticketingMailerQues[0].UserBCC);
                    cmdMail.Parameters.AddWithValue("@Ticket_Source", ticketingDetails.ticketingMailerQues[0].TicketSource);
                    cmdMail.Parameters.AddWithValue("@Alert_ID", ticketingDetails.ticketingMailerQues[0].AlertID);
                    cmdMail.Parameters.AddWithValue("@Is_Sent", ticketingDetails.ticketingMailerQues[0].IsSent);
                    cmdMail.Parameters.AddWithValue("@Priority_ID", ticketingDetails.ticketingMailerQues[0].PriorityID);
                    cmdMail.Parameters.AddWithValue("@Created_By", ticketingDetails.ticketingMailerQues[0].CreatedBy);
                    if (finalAttchment == null || finalAttchment == String.Empty)
                    {
                        cmdMail.Parameters.AddWithValue("@Has_Attachment", 0);
                    }
                    else
                    {
                        cmdMail.Parameters.AddWithValue("@Has_Attachment", 1);
                    }

                    cmdMail.CommandType = CommandType.StoredProcedure;
                    a = Convert.ToInt32(cmdMail.ExecuteScalar());

                }

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

            return ticketID;
        }

    }
}



