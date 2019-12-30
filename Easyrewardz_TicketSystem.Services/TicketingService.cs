using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Easyrewardz_TicketSystem.CustomModel;

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
        /// <returns></returns>
        public List<TicketingDetails> GetTicketList(string TikcketTitle)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<TicketingDetails> ticketing = new List<TicketingDetails>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getTitleSuggestions", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@TikcketTitle", TikcketTitle);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TicketingDetails ticketingDetails = new TicketingDetails();
                        ticketingDetails.TikcketTitle = Convert.ToString(ds.Tables[0].Rows[i]["TikcketTitle"]);
                        ticketing.Add(ticketingDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ticketing;
        }


        public int addTicket(TicketingDetails ticketingDetails)
        {
            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createTicket", conn);
                cmd1.Parameters.AddWithValue("@TenantID", ticketingDetails.TenantID);
                cmd1.Parameters.AddWithValue("@TikcketTitle", ticketingDetails.TikcketTitle);
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
                cmd1.Parameters.AddWithValue("@AssignedID", ticketingDetails.AssignedID);
                cmd1.Parameters.AddWithValue("@TicketActionID", ticketingDetails.TicketActionID);
                cmd1.Parameters.AddWithValue("@IsInstantEscalateToHighLevel", ticketingDetails.IsInstantEscalateToHighLevel);
                cmd1.Parameters.AddWithValue("@StatusID", ticketingDetails.StatusID);
                cmd1.Parameters.AddWithValue("@IsWantToVisitedStore", ticketingDetails.IsWantToVisitedStore);
                cmd1.Parameters.AddWithValue("@IsAlreadyVisitedStore", ticketingDetails.IsAlreadyVisitedStore);
                cmd1.Parameters.AddWithValue("@IsWantToAttachOrder", ticketingDetails.IsWantToAttachOrder);
                cmd1.Parameters.AddWithValue("@TicketTemplateID", ticketingDetails.TicketTemplateID);
                cmd1.Parameters.AddWithValue("@IsActive", ticketingDetails.IsActive);
                cmd1.Parameters.AddWithValue("@CreatedBy", ticketingDetails.CreatedBy);
                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
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
        /// GetDraft
        /// </summary>
        /// <param name="TikcketTitle"></param>
        /// <returns></returns>
        public List<CustomDraftDetails> GetDraft(int UserID)
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
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomDraftDetails DraftDetails = new CustomDraftDetails();
                        DraftDetails.TicketId = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketId"]);
                        DraftDetails.TicketTitle = Convert.ToString(ds.Tables[0].Rows[i]["TikcketTitle"]);
                        DraftDetails.TicketDescription = Convert.ToString(ds.Tables[0].Rows[i]["TicketDescription"]);
                        DraftDetails.CategoryName = Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        DraftDetails.SubCategoryName = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        DraftDetails.IssueTypeName = Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
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
        public List<CustomSearchTicketAgent> SearchAgent(string FirstName, string LastName, string Email, int DesignationID)
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
        public int AddSearch(int UserID, string SearchSaveName, string parameter)
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
    }
}

