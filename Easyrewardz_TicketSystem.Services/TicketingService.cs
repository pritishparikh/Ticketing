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
                cmd.Parameters.AddWithValue("@TicketStatusID", (int) EnumMaster.TicketStatus.Draft); 
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomDraftDetails DraftDetails  = new CustomDraftDetails();
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
    }
}

