using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class HSChatTicketingService : IHSChatTicketing
    {
        MySqlConnection conn = new MySqlConnection();

        public HSChatTicketingService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public List<CustomGetChatTickets> GetTicketsOnLoad(int statusID, int tenantID, int userMasterID, string programCode)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomGetChatTickets> lstGetChatTickets = new List<CustomGetChatTickets>();
            List<string> countList = new List<string>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_HSGetChatTickets", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("Status_ID", statusID);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", tenantID);
                sqlcmd.Parameters.AddWithValue("UserMaster_ID", userMasterID);
                sqlcmd.Parameters.AddWithValue("program_Code", programCode);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomGetChatTickets customGetChatTickets = new CustomGetChatTickets
                        {
                            TicketID = ds.Tables[0].Rows[i]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]),
                            TicketStatus = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"])),
                            TicketTitle = ds.Tables[0].Rows[i]["TicketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]),
                            Category = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                            SubCategory = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                            IssueType = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]),
                            AssignTo = ds.Tables[0].Rows[i]["Assignee"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignee"]),
                            Priority  = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]),
                            CreatedOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]),
                            CreatedDate= ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            UpdatedBy= ds.Tables[0].Rows[i]["ModifyBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyBy"]),
                            UpdatedDate= ds.Tables[0].Rows[i]["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]),
                        };

                        lstGetChatTickets.Add(customGetChatTickets);
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
            return lstGetChatTickets;
        }

        public List<TicketStatusModel> TicketStatusCount(int tenantID, int userID, string programCode)
        {
            List<TicketStatusModel> ticketCount = new List<TicketStatusModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();

            try

            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_HSGetChatTicketCount", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("tenantID", tenantID);
                sqlcmd.Parameters.AddWithValue("user_ID", userID);
                sqlcmd.Parameters.AddWithValue("program_Code", programCode);
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
    }
}
