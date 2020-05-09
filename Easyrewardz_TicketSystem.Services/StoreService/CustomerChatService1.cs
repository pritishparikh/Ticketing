using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Data;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

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
                        customerChatMaster.CustomerID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerID"]);
                        customerChatMaster.CumtomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        customerChatMaster.MobileNo = ds.Tables[0].Rows[i]["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerNumber"]);
                        customerChatMaster.MessageCount= ds.Tables[0].Rows[i]["NewMessageCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NewMessageCount"]);
                        customerChatMaster.TimeAgo = ds.Tables[0].Rows[i]["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TimeAgo"]);
                        customerChatMaster.ProgramCode = ds.Tables[0].Rows[i]["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ProgramCode"]);
                        customerChatMaster.StoreID = ds.Tables[0].Rows[i]["StoreID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreID"]);
                        customerChatMaster.StoreManagerId = ds.Tables[0].Rows[i]["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StoreManagerId"]);
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
        public int ScheduleVisit(AppointmentMaster appointmentMaster)
        {
            int message;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSScheduleVisit", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Customer_ID", appointmentMaster.CustomerID);
                cmd.Parameters.AddWithValue("@Appointment_Date", appointmentMaster.AppointmentDate); 
                cmd.Parameters.AddWithValue("@Slot_ID", appointmentMaster.SlotID);
                cmd.Parameters.AddWithValue("@Tenant_ID", appointmentMaster.TenantID);
                cmd.Parameters.AddWithValue("@Created_By", appointmentMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@NOof_People", appointmentMaster.NOofPeople);
                cmd.Parameters.AddWithValue("@Mobile_No", appointmentMaster.MobileNo);
                cmd.CommandType = CommandType.StoredProcedure;
                message = Convert.ToInt32(cmd.ExecuteScalar());

               // int response = SendMessageToCustomer( /*ChatID*/0, appointmentMaster.MobileNo, appointmentMaster.ProgramCode, appointmentMaster.MessageToReply,/*ClientAPIURL*/"",appointmentMaster.CreatedBy);
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
        public int GetChatCount(int tenantID,int UserMasterID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            int counts = 0;
            try
            {

                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSCountTotalUnreadChat", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd1.Parameters.AddWithValue("@UserMaster_ID", UserMasterID);
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

        /// <summary>
        /// GetTimeSlot
        /// </summary>
        /// <param name="userMasterID"></param>
        /// <param name="tenantID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public List<DateofSchedule> GetTimeSlot(int storeID,int userMasterID, int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();        
            List<DateofSchedule> lstdateofSchedule = new List<DateofSchedule>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSGetTimeSlot", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                //cmd1.Parameters.AddWithValue("@store_ID", storeID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateofSchedule dateofSchedule = new DateofSchedule();
                        dateofSchedule.Day = ds.Tables[0].Rows[i]["Today"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Today"]);
                        dateofSchedule.Dates = ds.Tables[0].Rows[i]["Dates"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Dates"]);

                        DataTable dataTable = new DataTable();

                        if(i==0)
                        {
                            dataTable = ds.Tables[1];
                        }
                       else if (i == 1)
                        {
                            dataTable = ds.Tables[2];
                        }
                        else if (i == 2)
                        {
                            dataTable = ds.Tables[3];
                        }

                        List<AlreadyScheduleDetail> lstAlreadyScheduleDetail = new List<AlreadyScheduleDetail>();
                        if (dataTable != null )
                        {
                            for (int j = 0; j < dataTable.Rows.Count; j++)
                            {
                                AlreadyScheduleDetail alreadyScheduleDetail = new AlreadyScheduleDetail();
                                alreadyScheduleDetail.TimeSlotId= dataTable.Rows[j]["SlotId"] == DBNull.Value ? 0 : Convert.ToInt32(dataTable.Rows[j]["SlotId"]);
                                alreadyScheduleDetail.AppointmentDate= dataTable.Rows[j]["AppointmentDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[j]["AppointmentDate"]);
                                alreadyScheduleDetail.VisitedCount = dataTable.Rows[j]["VisitedCount"] == DBNull.Value ? 0 : Convert.ToInt32(dataTable.Rows[j]["VisitedCount"]);
                                alreadyScheduleDetail.MaxCapacity = dataTable.Rows[j]["MaxCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(dataTable.Rows[j]["MaxCapacity"]);
                                alreadyScheduleDetail.Remaining = dataTable.Rows[j]["Remaining"] == DBNull.Value ? 0 : Convert.ToInt32(dataTable.Rows[j]["Remaining"]);
                                alreadyScheduleDetail.TimeSlot = dataTable.Rows[j]["TimeSlot"] == DBNull.Value ? string.Empty: Convert.ToString(dataTable.Rows[j]["TimeSlot"]);
                                alreadyScheduleDetail.StoreId = dataTable.Rows[j]["StoreId"] == DBNull.Value ? 0 : Convert.ToInt32(dataTable.Rows[j]["StoreId"]);
                                lstAlreadyScheduleDetail.Add(alreadyScheduleDetail);
                            }
                            dateofSchedule.AlreadyScheduleDetails = lstAlreadyScheduleDetail;
                        }
                        lstdateofSchedule.Add(dateofSchedule);
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
            return lstdateofSchedule;
        }

        public int SendMessageToCustomerForVisit(AppointmentMaster appointmentMaster, string ClientAPIURL, int CreatedBy)
        {
            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;
            //CustomerChatModel ChatMessageDetails = new CustomerChatModel();
            ClientCustomSendTextModel SendTextRequest = new ClientCustomSendTextModel();
            string ClientAPIResponse = string.Empty;

            try
            {

                string textToReply = "Dear" + appointmentMaster.CustomerName + ",Your Visit for Our Store is schedule On" + appointmentMaster.AppointmentDate +
                    "On Time Between"+ appointmentMaster.TimeSlot;
                #region call client api for sending message to customer

                SendTextRequest.To = appointmentMaster.MobileNo;
                SendTextRequest.textToReply = textToReply;
                SendTextRequest.programCode = appointmentMaster.ProgramCode;

                string JsonRequest = JsonConvert.SerializeObject(SendTextRequest);

                ClientAPIResponse = CommonService.SendApiRequest(ClientAPIResponse + "api/ChatbotBell/SendText", JsonRequest);


                // response binding pending as no response structure is provided yet from client------

                //--------

                #endregion

                //if (ChatID > 0)
                //{
                //    ChatMessageDetails.ChatID = ChatID;
                //    ChatMessageDetails.Message = Message;
                //    ChatMessageDetails.ByCustomer = false;
                //    ChatMessageDetails.ChatStatus = 1;
                //    ChatMessageDetails.StoreManagerId = CreatedBy;
                //    ChatMessageDetails.CreatedBy = CreatedBy;

                //    resultCount = SaveChatMessages(ChatMessageDetails);

                //}

            }
            catch (Exception)
            {
                throw;
            }

            return resultCount;
        }
    }
}
