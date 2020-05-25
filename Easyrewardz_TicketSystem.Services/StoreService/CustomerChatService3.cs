using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public partial class CustomerChatService: ICustomerChat
    {
        /// <summary>
        /// update customer chat session
        /// </summary>
        /// <param name="ChatSessionValue"></param>
        /// <param name="ChatSessionDuration"></param>
        /// <param name="ChatDisplayValue"></param>
        /// <param name="ChatDisplayDuration"></param>
        /// <returns></returns>
        public int UpdateChatSession(int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue, string ChatDisplayDuration, int ModifiedBy)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_UpdateChatSessionMaster"
                };
                cmd.Parameters.AddWithValue("@_ChatSessionValue", ChatSessionValue);
                cmd.Parameters.AddWithValue("@_ChatSessionDuration", ChatSessionDuration);
                cmd.Parameters.AddWithValue("@_ChatDisplayValue", ChatDisplayValue);
                cmd.Parameters.AddWithValue("@_ChatDisplayDuration", ChatDisplayDuration);
                cmd.Parameters.AddWithValue("@_ModifiedBy", ModifiedBy);

                success = Convert.ToInt32(cmd.ExecuteScalar());
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
            return success;
        }


        /// <summary>
        /// get customer chat session
        /// </summary>
      
        /// <returns></returns>
        public ChatSessionModel GetChatSession()
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            ChatSessionModel ChatSession = new ChatSessionModel();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_GetChatSession", conn);
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {


                        ChatSession.ProgramCode = ds.Tables[0].Rows[0]["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ProgramCode"]);
                        ChatSession.TenantID = ds.Tables[0].Rows[0]["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["TenantID"]);
                        ChatSession.ChatSessionValue = ds.Tables[0].Rows[0]["ChatSessionValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ChatSessionValue"]);
                        ChatSession.ChatSessionDuration = ds.Tables[0].Rows[0]["ChatSessionDuration"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ChatSessionDuration"]);
                        ChatSession.ChatDisplayValue = ds.Tables[0].Rows[0]["ChatDisplayValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ChatDisplayValue"]);
                        ChatSession.ChatDisplayDuration = ds.Tables[0].Rows[0]["ChatDisplayDuration"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ChatDisplayDuration"]);

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

            return ChatSession;
        }


        /// <summary>
        /// get recent chat history of agents
        /// </summary>
        /// 
        public List<AgentRecentChatHistory> GetAgentRecentChat()
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<AgentRecentChatHistory> RecentChatsList = new List<AgentRecentChatHistory>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetAgentRecentChat", conn);
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            AgentRecentChatHistory obj = new AgentRecentChatHistory()
                            {
                                ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]),
                                Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"]),
                                StoreManagerID = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                AgentName = dr["Agent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Agent"]),
                                ChatCount = dr["ChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatCount"]),
                                TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),

                            };

                            RecentChatsList.Add(obj);
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

            return RecentChatsList;
        }

    }
}
