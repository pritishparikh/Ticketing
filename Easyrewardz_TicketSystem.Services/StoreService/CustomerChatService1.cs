using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public partial class CustomerChatService: ICustomerChat
    {
        #region variable
        MySqlConnection conn = new MySqlConnection();
        #endregion

        #region Constructor
        public CustomerChatService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Read On Going Message
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        public int MarkAsReadOnGoingChat(int chatID)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_MarkAsReadOnGoingChat", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@chat_ID", chatID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
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
        /// Get New Chat
        /// </summary>
        /// <param name="userMasterID"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<CustomerChatMaster> NewChat(int userMasterID, int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomerChatMaster> lstCustomerChatMaster = new List<CustomerChatMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSNewChat", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomerChatMaster customerChatMaster = new CustomerChatMaster();
                        customerChatMaster.ChatID = Convert.ToInt32(ds.Tables[0].Rows[i]["CurrentChatID"]);
                       // customerChatMaster.CustomerID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerID"]);
                        customerChatMaster.CumtomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        customerChatMaster.MobileNo = ds.Tables[0].Rows[i]["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerNumber"]);
                        customerChatMaster.MessageCount = ds.Tables[0].Rows[i]["NewMessageCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NewMessageCount"]);
                        customerChatMaster.TimeAgo = ds.Tables[0].Rows[i]["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TimeAgo"]);
                        lstCustomerChatMaster.Add(customerChatMaster);
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
            return lstCustomerChatMaster;
        }

        /// <summary>
        /// Get Ongoing Chat
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userMasterID"></param>
        /// <returns></returns>
        public List<CustomerChatMaster> OngoingChat(int userMasterID, int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomerChatMaster> lstCustomerChatMaster = new List<CustomerChatMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSOngoingChat", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomerChatMaster customerChatMaster = new CustomerChatMaster();
                        customerChatMaster.ChatID = Convert.ToInt32(ds.Tables[0].Rows[i]["CurrentChatID"]);
                       // customerChatMaster.CustomerID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerID"]);
                        customerChatMaster.CumtomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        customerChatMaster.MobileNo = ds.Tables[0].Rows[i]["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerNumber"]);
                        customerChatMaster.MessageCount= ds.Tables[0].Rows[i]["NewMessageCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NewMessageCount"]);
                        customerChatMaster.TimeAgo = ds.Tables[0].Rows[i]["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TimeAgo"]);
                        lstCustomerChatMaster.Add(customerChatMaster);
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
            return lstCustomerChatMaster;
        }

        /// <summary>
        /// Schedule Visit 
        /// </summary>
        /// <param name="AppointmentMaster"></param>
        /// <returns></returns>
        public string ScheduleVisit(AppointmentMaster appointmentMaster)
        {
            string message ="";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSScheduleVisit", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Customer_ID", appointmentMaster.CustomerID);
                cmd.Parameters.AddWithValue("@Appointment_Date", appointmentMaster.AppointmentDate);
                cmd.Parameters.AddWithValue("@Time_Slot", appointmentMaster.TimeSlot);
                cmd.Parameters.AddWithValue("@Tenant_ID", appointmentMaster.TenantID);
                cmd.Parameters.AddWithValue("@Created_By", appointmentMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@NOof_People", appointmentMaster.NOofPeople);
                cmd.Parameters.AddWithValue("@Mobile_No", appointmentMaster.MobileNo);
                cmd.Parameters.AddWithValue("@Store_ID", appointmentMaster.StoreID);
                cmd.CommandType = CommandType.StoredProcedure;
                message = Convert.ToString(cmd.ExecuteScalar());
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
            return message;
        }

        /// <summary>
        /// UpdateCustomerChatStatus
        /// </summary>
        /// <param name="chatid"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public int UpdateCustomerChatIdStatus(int chatID, int tenantId)
        {

            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateCustomerChatStatus", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@chat_id", chatID);
                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());
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

            return result;
        }

        /// <summary>
        /// CustomerChatHistory
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        public List<CustomerChatHistory> CustomerChatHistory(int chatID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomerChatHistory> customerChatHistory = new List<CustomerChatHistory>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSCustomerChatHistory", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@chat_id", chatID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomerChatHistory chatHistory = new CustomerChatHistory();
                        chatHistory.ChatID = Convert.ToInt32(ds.Tables[0].Rows[i]["ChatID"]);
                        chatHistory.CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        chatHistory.Time = ds.Tables[0].Rows[i]["Time"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Time"]);
                        chatHistory.Message = ds.Tables[0].Rows[i]["Message"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Message"]);
                        customerChatHistory.Add(chatHistory);
                    }
                }
            }
            catch (Exception ex)
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

            return customerChatHistory;
        }

        /// <summary>
        /// Get New Chat
        /// </summary>
        /// <param name=""></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public int GetChatCount(int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            int counts = 0;
            try
            {

                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSCountTotalUnreadOngoingChat", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        counts= ds.Tables[0].Rows[i]["TotalUnreadChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TotalUnreadChatCount"]);
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
            return counts;
        }
    }
}
