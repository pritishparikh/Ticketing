﻿using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Easyrewardz_TicketSystem.CustomModel;
using System.IO;
using System.Text;
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
        /// <param name="TikcketTitle"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<TicketTitleDetails> GetTicketList(string TikcketTitle, int TenantId)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<TicketTitleDetails> ticketing = new List<TicketTitleDetails>();
            string _ticketTitle = string.Empty;
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
                        _ticketTitle = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"])) ? string.Empty
                                        : Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]);

                        if (!string.IsNullOrEmpty(_ticketTitle))
                        {
                            tktTitle.TicketTitle = _ticketTitle.Length > 10 ? _ticketTitle.Substring(0, 10) : _ticketTitle;
                            tktTitle.TicketTitleToolTip = _ticketTitle.Length > 10 ? _ticketTitle : string.Empty;
                        }

                        ticketing.Add(tktTitle);

                        //TicketingDetails ticketingDetails = new TicketingDetails();
                        //ticketingDetails.TicketTitle = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]));
                        //ticketingDetails.
                        //ticketing.Add(ticketingDetails);
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        public int addTicket(TicketingDetails ticketingDetails, int TenantId, string FolderPath, string finalAttchment)
        {
            MySqlCommand cmd = new MySqlCommand();
            int ticketID = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createTicket", conn);
                cmd1.Parameters.AddWithValue("@TenantID", TenantId);
                cmd1.Parameters.AddWithValue("@TicketDescription", ticketingDetails.Ticketdescription);
                cmd1.Parameters.AddWithValue("@TicketSourceID", ticketingDetails.TicketSourceID);
                cmd1.Parameters.AddWithValue("@BrandID", ticketingDetails.BrandID);
                cmd1.Parameters.AddWithValue("@CategoryID", ticketingDetails.CategoryID);
                cmd1.Parameters.AddWithValue("@SubCategoryID", ticketingDetails.SubCategoryID);
                cmd1.Parameters.AddWithValue("@PriorityID", ticketingDetails.PriorityID);
                cmd1.Parameters.AddWithValue("@CustomerID", ticketingDetails.CustomerID);
                cmd1.Parameters.AddWithValue("@OrderMasterID", ticketingDetails.OrderMasterID);
                cmd1.Parameters.AddWithValue("@IssueTypeID", ticketingDetails.IssueTypeID);
                cmd1.Parameters.AddWithValue("@ChannelOfPurchaseID", ticketingDetails.ChannelOfPurchaseID);
                //need to change as per TicketActionType ID[QB / ETA]
                cmd1.Parameters.AddWithValue("@AssignedID", ticketingDetails.AssignedID);
                cmd1.Parameters.AddWithValue("@TicketActionID", ticketingDetails.TicketActionID);

                cmd1.Parameters.AddWithValue("@StatusID", ticketingDetails.StatusID);

                cmd1.Parameters.AddWithValue("@TicketTemplateID", ticketingDetails.TicketTemplateID);

                cmd1.Parameters.AddWithValue("@CreatedBy", ticketingDetails.CreatedBy);
                cmd1.Parameters.AddWithValue("@Notes", ticketingDetails.Ticketnotes);

                cmd1.Parameters.AddWithValue("@IsInstantEscalateToHighLevel", Convert.ToInt16(ticketingDetails.IsInstantEscalateToHighLevel));
                cmd1.Parameters.AddWithValue("@IsWantToVisitedStore", Convert.ToInt16(ticketingDetails.IsWantToVisitedStore));
                cmd1.Parameters.AddWithValue("@IsAlreadyVisitedStore", Convert.ToInt16(ticketingDetails.IsAlreadyVisitedStore));
                cmd1.Parameters.AddWithValue("@IsWantToAttachOrder", Convert.ToInt16(ticketingDetails.IsWantToAttachOrder));
                cmd1.Parameters.AddWithValue("@IsActive", Convert.ToInt16(ticketingDetails.IsActive));
                cmd1.Parameters.AddWithValue("@OrderItemID", string.IsNullOrEmpty(ticketingDetails.OrderItemID) ? "" : ticketingDetails.OrderItemID);

                cmd1.Parameters.AddWithValue("@TikcketTitle", string.IsNullOrEmpty(ticketingDetails.TicketTitle) ? "" : ticketingDetails.TicketTitle);
                cmd1.Parameters.AddWithValue("@StoreID", string.IsNullOrEmpty(ticketingDetails.StoreID) ? "" : ticketingDetails.StoreID);


                cmd1.CommandType = CommandType.StoredProcedure;


                ticketID = Convert.ToInt32(cmd1.ExecuteScalar());

                if (ticketingDetails.taskMasters.Count > 0)
                {
                    for (int j = 0; j < ticketingDetails.taskMasters.Count; j++)
                    {
                        int taskId = 0;
                        try
                        {
                            //conn.Open();
                            //cmd.Connection = conn;
                            MySqlCommand cmdtask = new MySqlCommand("SP_createTask", conn);
                            cmdtask.Connection = conn;
                            cmdtask.Parameters.AddWithValue("@TicketID", ticketID);
                            cmdtask.Parameters.AddWithValue("@TaskTitle", ticketingDetails.taskMasters[j].TaskTitle);
                            cmdtask.Parameters.AddWithValue("@TaskDescription", ticketingDetails.taskMasters[j].TaskDescription);
                            cmdtask.Parameters.AddWithValue("@DepartmentId", ticketingDetails.taskMasters[j].DepartmentId);
                            cmdtask.Parameters.AddWithValue("@FunctionID", ticketingDetails.taskMasters[j].FunctionID);
                            cmdtask.Parameters.AddWithValue("@AssignToID", ticketingDetails.taskMasters[j].AssignToID);
                            cmdtask.Parameters.AddWithValue("@PriorityID", ticketingDetails.taskMasters[j].PriorityID);
                            cmdtask.CommandType = CommandType.StoredProcedure;
                            taskId = Convert.ToInt32(cmdtask.ExecuteScalar());
                            //conn.Close();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                }

                //Attchment 
                try
                {
                    int i = 0;
                    //if (!Directory.Exists(FolderPath))
                    //{
                    //    // Try to create the directory.
                    //    DirectoryInfo di = Directory.CreateDirectory(FolderPath);
                    //}


                    MySqlCommand cmdattachment = new MySqlCommand("SP_SaveAttachment", conn);
                    cmdattachment.Parameters.AddWithValue("@fileName", finalAttchment);
                    cmdattachment.Parameters.AddWithValue("@Ticket_ID", ticketID);
                    cmdattachment.CommandType = CommandType.StoredProcedure;
                    i = cmdattachment.ExecuteNonQuery();


                }
                catch (IOException ioex)
                {
                    Console.WriteLine(ioex.Message);
                }
                int a = 0;
                //conn.Open();
                //cmd.Connection = conn;

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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
                var tessst = ex.ToString() + "\n" + ex.InnerException + "\n" + ex.StackTrace;
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
        /// <param name="TikcketTitle"></param>
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
                        CustomDraftDetails DraftDetails = new CustomDraftDetails();
                        DraftDetails.TicketId = ds.Tables[0].Rows[i]["TicketId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketId"]);
                        DraftDetails.TicketTitle = ds.Tables[0].Rows[i]["TikcketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TikcketTitle"]);
                        DraftDetails.TicketDescription = ds.Tables[0].Rows[i]["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketDescription"]);
                        DraftDetails.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        DraftDetails.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        DraftDetails.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        DraftDetails.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                        Draftlist.Add(DraftDetails);
                    }
                }
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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
        /// <returns></returns>
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
                cmd.CommandType = CommandType.StoredProcedure;
                i = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
        /// <returns></returns>
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
                cmd.Parameters.AddWithValue("@Remarks", Remark);
                cmd.CommandType = CommandType.StoredProcedure;
                i = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddSchedule", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
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
                cmd.CommandType = CommandType.StoredProcedure;
                i = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
        /// Get Auto Suggest Ticket Title
        /// </summary>
        /// <param name="TikcketTitle"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                throw ex;
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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
        /// <returns></returns>
        public CustomTicketDetail getTicketDetailsByTicketId(int TicketID, int TenantID, string url)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                CustomTicketDetail ticketDetails = new CustomTicketDetail();
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
                        ticketDetails.TargetClouredate = ds.Tables[0].Rows[i]["TargetClouredate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TargetClouredate"]);
                        ticketDetails.OpenTicket = ds.Tables[0].Rows[i]["OpenTickets"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OpenTickets"]);
                        ticketDetails.Totalticket = ds.Tables[0].Rows[i]["Totaltickets"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Totaltickets"]);
                        ticketDetails.stores = ds.Tables[1].AsEnumerable().Select(x => new Store()
                        {
                            StoreID = Convert.ToInt32(x.Field<int>("StoreID")),
                            Storename = x.Field<object>("StoreName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("StoreName"))

                        }).ToList();

                        ticketDetails.products = ds.Tables[2].AsEnumerable().Select(x => new Product()
                        {
                            ItemID = Convert.ToInt32(x.Field<int>("OrderItemID")),
                            ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("ItemName"))

                        }).ToList();

                        ticketDetails.attachment = ds.Tables[3].AsEnumerable().Select(x => new Attachment()
                        {
                            TicketAttachmentId = Convert.ToInt32(x.Field<int>("TicketAttachmentId")),
                            AttachmentName = x.Field<object>("AttachmentName") == System.DBNull.Value ? string.Empty : url + "/" + Convert.ToString(x.Field<object>("AttachmentName"))
                        }).ToList();
                    }
                }
                return ticketDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Get Ticket History
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name=""></param>
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
                        TicketHistory.DateandTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                        ListTicketHistory.Add(TicketHistory);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
            }
            return CountByTicket;
        }

        public List<TicketMessage> TicketMessagelisting(int ticketID, int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<TicketMessage> ticketMessages = new List<TicketMessage>();
            List<CustomTicketMessage> CustumTicketMessagelist = new List<CustomTicketMessage>();
            try
            {
                conn.Open();
                cmd.Connection = conn; 
                 MySqlCommand cmd1 = new MySqlCommand("SP_GetTicketMessage", conn);
                //MySqlCommand cmd1 = new MySqlCommand("Test_GetTicketMessage", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Ticket_Id", ticketID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0] != null && ds.Tables[0] != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CustomTicketMessage TicketMessageDetails = new CustomTicketMessage();
                            TicketMessageDetails.MailID = Convert.ToInt32(ds.Tables[0].Rows[i]["MailID"]);
                            TicketMessageDetails.TicketID = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]);
                            TicketMessageDetails.TicketMailSubject = ds.Tables[0].Rows[i]["TikcketMailSubject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TikcketMailSubject"]);
                            TicketMessageDetails.TicketMailBody = ds.Tables[0].Rows[i]["TicketMailBody"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketMailBody"]);
                            TicketMessageDetails.IsCustomerComment = Convert.ToInt32(ds.Tables[0].Rows[i]["IsCustomerComment"]);
                            TicketMessageDetails.HasAttachment = Convert.ToInt32(ds.Tables[0].Rows[i]["HasAttachment"]);
                            TicketMessageDetails.CommentBy = ds.Tables[0].Rows[i]["CommentBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CommentBy"]);
                            TicketMessageDetails.UpdatedAt = ds.Tables[0].Rows[i]["UpdatedAt"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UpdatedAt"]);
                            TicketMessageDetails.CreatedDate= ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                            CustumTicketMessagelist.Add(TicketMessageDetails);
                        }
                    }


                    if (ds.Tables[1] != null && ds.Tables[1] != null)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                          
                            TicketMessage ticketMessage = new TicketMessage();
                            ticketMessage.MessageCount= Convert.ToInt32(ds.Tables[1].Rows[i]["MessageCount"]);
                            ticketMessage.MessageDate= ds.Tables[1].Rows[i]["MessageDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["MessageDate"]);
                           
                            ticketMessage.CustomTicketMessages = CustumTicketMessagelist.Where(x => !string.IsNullOrEmpty(x.CreatedDate) && x.CreatedDate == ticketMessage.MessageDate).ToList();
                            ticketMessage.UpdatedDate = CustumTicketMessagelist.Where(x => !string.IsNullOrEmpty(x.CreatedDate) && x.CreatedDate == ticketMessage.MessageDate).Select(x => x.UpdatedAt).ToList().FirstOrDefault();
                            
                            ticketMessages.Add(ticketMessage);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ticketMessages;

            // DataSet ds = new DataSet();
            // MySqlCommand cmd = new MySqlCommand();
            //List<CustomTicketMessage> TicketMessagelist = new List <CustomTicketMessage>();
            // try
            // {
            //     conn.Open();
            //     cmd.Connection = conn;
            //     MySqlCommand cmd1 = new MySqlCommand("SP_GetTicketMessage", conn);
            //     cmd1.CommandType = CommandType.StoredProcedure;
            //     cmd1.Parameters.AddWithValue("@Ticket_Id", ticketID);
            //     cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
            //     MySqlDataAdapter da = new MySqlDataAdapter();
            //     da.SelectCommand = cmd1;
            //     da.Fill(ds);
            //     if (ds != null && ds.Tables[0] != null)
            //     {
            //         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //         {
            //              CustomTicketMessage TicketMessage = new CustomTicketMessage();
            //             TicketMessage.MailID = Convert.ToInt32(ds.Tables[0].Rows[i]["MailID"]);
            //             TicketMessage.TicketID = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]);
            //             TicketMessage.TicketMailSubject = ds.Tables[0].Rows[i]["TikcketMailSubject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TikcketMailSubject"]);
            //             TicketMessage.TicketMailBody = ds.Tables[0].Rows[i]["TicketMailBody"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketMailBody"]);
            //             TicketMessage.IsCustomerComment = Convert.ToInt32(ds.Tables[0].Rows[i]["IsCustomerComment"]);
            //             TicketMessage.HasAttachment = Convert.ToInt32(ds.Tables[0].Rows[i]["HasAttachment"]);
            //             TicketMessage.CommentBy = ds.Tables[0].Rows[i]["CommentBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CommentBy"]);
            //             TicketMessage.UpdatedAt = ds.Tables[0].Rows[i]["UpdatedAt"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UpdatedAt"]);
            //             TicketMessagelist.Add(TicketMessage);
            //         }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            // }
            // return TicketMessagelist;
        }

        public List<CustomSearchTicketAgent> GetAgentList(int TenantID)
        {

            DataSet ds = new DataSet();
            List<CustomSearchTicketAgent> listSearchagent = new List<CustomSearchTicketAgent>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetAgentList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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

        public int CommentReplyOnTicketDetail(TicketingMailerQue ticketingMailerQue)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmdMail = new MySqlCommand("SP_ReplyComment", conn);
                cmdMail.Parameters.AddWithValue("@Tenant_ID", ticketingMailerQue.TenantID);
                cmdMail.Parameters.AddWithValue("@Ticket_ID", ticketingMailerQue.TicketID);
                cmdMail.Parameters.AddWithValue("@TikcketMail_Subject", ticketingMailerQue.TikcketMailSubject);
                cmdMail.Parameters.AddWithValue("@TicketMail_Body", ticketingMailerQue.TicketMailBody);
                cmdMail.Parameters.AddWithValue("@To_Email", ticketingMailerQue.ToEmail);
                cmdMail.Parameters.AddWithValue("@User_CC", ticketingMailerQue.UserCC);
                cmdMail.Parameters.AddWithValue("@User_BCC", ticketingMailerQue.UserBCC);
                //cmdMail.Parameters.AddWithValue("@Ticket_Source", ticketingMailerQue.TicketSource);
                cmdMail.Parameters.AddWithValue("@Alert_ID", ticketingMailerQue.AlertID);
                cmdMail.Parameters.AddWithValue("@Is_Sent", ticketingMailerQue.IsSent);
                cmdMail.Parameters.AddWithValue("@Priority_ID", ticketingMailerQue.PriorityID);
                cmdMail.Parameters.AddWithValue("@Created_By", ticketingMailerQue.CreatedBy);
                /* if (finalAttchment == null || finalAttchment == String.Empty)
                 {
                     cmdMail.Parameters.AddWithValue("@Has_Attachment", 0);
                 }
                 else
                 {
                     cmdMail.Parameters.AddWithValue("@Has_Attachment", 1);
                 }*/

                cmdMail.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmdMail.ExecuteNonQuery());
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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

        public int CommentOnTicketDetail(TicketingMailerQue ticketingMailerQue)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmdMail = new MySqlCommand("", conn);
                cmdMail.Parameters.AddWithValue("@Tenant_ID", ticketingMailerQue.TenantID);
                cmdMail.Parameters.AddWithValue("@Ticket_ID", ticketingMailerQue.TicketID);
                cmdMail.Parameters.AddWithValue("@TikcketMail_Subject", ticketingMailerQue.TikcketMailSubject);
                cmdMail.Parameters.AddWithValue("@TicketMail_Body", ticketingMailerQue.TicketMailBody);
                cmdMail.Parameters.AddWithValue("@To_Email", ticketingMailerQue.ToEmail);
                cmdMail.Parameters.AddWithValue("@User_CC", ticketingMailerQue.UserCC);
                cmdMail.Parameters.AddWithValue("@User_BCC", ticketingMailerQue.UserBCC);
                //cmdMail.Parameters.AddWithValue("@Ticket_Source", ticketingMailerQue.TicketSource);
                cmdMail.Parameters.AddWithValue("@Alert_ID", ticketingMailerQue.AlertID);
                cmdMail.Parameters.AddWithValue("@Is_Sent", ticketingMailerQue.IsSent);
                cmdMail.Parameters.AddWithValue("@Priority_ID", ticketingMailerQue.PriorityID);
                cmdMail.Parameters.AddWithValue("@Created_By", ticketingMailerQue.CreatedBy);
                /* if (finalAttchment == null || finalAttchment == String.Empty)
                 {
                     cmdMail.Parameters.AddWithValue("@Has_Attachment", 0);
                 }
                 else
                 {
                     cmdMail.Parameters.AddWithValue("@Has_Attachment", 1);
                 }*/

                cmdMail.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmdMail.ExecuteNonQuery());
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
    }
}

