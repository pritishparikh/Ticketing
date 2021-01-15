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
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Easyrewardz_TicketSystem.Services
{
   public partial class CustomerChatService: ICustomerChat
    {
        #region variable
        MySqlConnection conn = new MySqlConnection();
        ChatbotBellHttpClientService APICall = null;

        ILogger<object> _logger;
        #endregion

        #region Constructor
        public CustomerChatService(string _connectionString, ChatbotBellHttpClientService _APICall=null, ILogger<object> logger=null)
        {
            conn.ConnectionString = _connectionString;
            APICall = _APICall;
            _logger = logger;
        }

      
        #endregion

        /// <summary>
        /// Read On Going Message
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        public async Task<int> MarkAsReadOnGoingChat(int chatID)
        {
            int success = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                  await  conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_MarkAsReadOnGoingChat", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@chat_ID", chatID);
                    cmd.CommandType = CommandType.StoredProcedure; 
                    success =await cmd.ExecuteNonQueryAsync();
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
            return success;
        }

        /// <summary>
        /// Get New Chat
        /// </summary>
        /// <param name="userMasterID"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public async Task<List<CustomerChatMaster>> NewChat(int userMasterID, int tenantID)
        {
            DataTable schemaTable = new DataTable();
            List<CustomerChatMaster> lstCustomerChatMaster = new List<CustomerChatMaster>(); 
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSNewChat", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                    cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                CustomerChatMaster customerChatMaster = new CustomerChatMaster()
                                {
                                    ChatID = Convert.ToInt32(dr["CurrentChatID"]),
                                    CustomerID = dr["CustomerID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerID"]),
                                    CumtomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                    MobileNo = dr["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerNumber"]),
                                    MessageCount = dr["NewMessageCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NewMessageCount"]),
                                    TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),
                                    ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                    StoreManagerId = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                    StoreManagerName = dr["StoreManagerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreManagerName"]),
                                    StoreID = dr["StoreID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreID"]),
                                    ChatSourceID = dr["SourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SourceID"]),
                                    SourceName = dr["SourceName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceName"]),
                                    SourceAbbr = dr["SourceAbbr"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceAbbr"]),
                                    SourceIconUrl = dr["SourceIconUrl"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceIconUrl"]),
                                    ChatSourceIsActive = dr["SourceIsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["SourceIsActive"]),
                                };
                                lstCustomerChatMaster.Add(customerChatMaster);
                            }

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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
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
        public async Task<List<CustomerChatMaster>> OngoingChat(int userMasterID, int tenantID, string Search, int StoreManagerID)
        {
            DataTable schemaTable = new DataTable();
            List<CustomerChatMaster> lstCustomerChatMaster = new List<CustomerChatMaster>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                   await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSOngoingChat", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                    cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@search", string.IsNullOrEmpty(Search) ? "" : Search);
                    cmd.Parameters.AddWithValue("@StoreMgr_ID", StoreManagerID);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                CustomerChatMaster customerChatMaster = new CustomerChatMaster()
                                {
                                    ChatID = dr["CurrentChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CurrentChatID"]),
                                    CustomerID = dr["CustomerID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerID"]),
                                    CumtomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                    MobileNo = dr["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerNumber"]),
                                    MessageCount = dr["NewMessageCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NewMessageCount"]),
                                    TimeAgo = dr["TimeAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeAgo"]),
                                    ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                    StoreID= dr["StoreID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreID"]),
                                    StoreManagerId = dr["StoreManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreManagerId"]),
                                    StoreManagerName = dr["StoreManagerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreManagerName"]),
                                    IsCustEndChat = dr["IsCustEndChat"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsCustEndChat"]),
                                    IsCustTimeout = dr["IsCustTimeout"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsCustTimeout"]),
                                    CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                    ChatSourceID = dr["SourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SourceID"]),
                                    SourceName = dr["SourceName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceName"]),
                                    SourceAbbr = dr["SourceAbbr"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceAbbr"]),
                                    SourceIconUrl = dr["SourceIconUrl"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SourceIconUrl"]),
                                    ChatSourceIsActive = dr["SourceIsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["SourceIsActive"]),
                                };
                                lstCustomerChatMaster.Add(customerChatMaster);
                            }

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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }
            return lstCustomerChatMaster;
        }

        /// <summary>
        /// Schedule Visit 
        /// </summary>
        /// <param name="AppointmentMaster"></param>
        /// <returns></returns>
        public async Task<List<AppointmentDetails>> ScheduleVisit(AppointmentMaster appointmentMaster)
        {
            DataTable schemaTable = new DataTable();
            List<AppointmentDetails> lstAppointmentDetails = new List<AppointmentDetails>();
            try 
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                  await  conn.OpenAsync();
                }
                using (conn)
                {
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
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                AppointmentDetails appointmentDetails = new AppointmentDetails()
                                {
                                    AppointmentID = Convert.ToInt32(dr["AppointmentID"]),
                                    CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                    CustomerMobileNo = dr["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MobileNo"]),
                                    StoreName = dr["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreName"]),
                                    StoreAddress = dr["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreAddress"]),
                                    NoOfPeople = dr["NOofPeople"] == DBNull.Value ? string.Empty : Convert.ToString(dr["NOofPeople"]),
                                    StoreManagerMobile = dr["StoreManagerMobile"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreManagerMobile"])
                                };
                                lstAppointmentDetails.Add(appointmentDetails);
                            }

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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }
            return lstAppointmentDetails;
        }

        /// <summary>
        /// UpdateCustomerChatStatus
        /// </summary>
        /// <param name="chatid"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<int> UpdateCustomerChatIdStatus(int chatID, int tenantId, int UserID)
        {
            int result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                   await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_UpdateCustomerChatStatus", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@chat_id", chatID);
                    cmd.Parameters.AddWithValue("@storemanager_Id", UserID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    result = Convert.ToInt32( await cmd.ExecuteScalarAsync());
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
            return result;
        }

        /// <summary>
        /// CustomerChatHistory
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        public async Task<List<CustomerChatHistory>> CustomerChatHistory(int chatID)
        {
            DataTable schemaTable = new DataTable();
            List<CustomerChatHistory> customerChatHistory = new List<CustomerChatHistory>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                   await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSCustomerChatHistory", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@chat_id", chatID);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                CustomerChatHistory chatHistory = new CustomerChatHistory()
                                {
                                    ChatID = Convert.ToInt32(dr["ChatID"]),
                                    CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                    Time = dr["Time"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Time"]),
                                    Message = dr["Message"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Message"])
                                };
                                customerChatHistory.Add(chatHistory);
                            }

                        }
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
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
        public async Task<int> GetChatCount(int tenantID,int UserMasterID)
        {
            DataTable schemaTable = new DataTable();
            int counts = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                   await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSCountTotalUnreadChat", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@UserMaster_ID", UserMasterID);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                counts = dr["TotalUnreadChatCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalUnreadChatCount"]);
                            }

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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }
            return counts;
        }

        /// <summary>
        /// Get Time Slot
        /// </summary>
        /// <param name="userMasterID"></param>
        /// <param name="tenantID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public List<DateofSchedule> GetTimeSlot(int TenantID, string Programcode, int UserID)
        {
            DataSet ds = new DataSet();     
            List<DateofSchedule> lstdateofSchedule = new List<DateofSchedule>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetTimeSlot", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                    cmd.Parameters.AddWithValue("@_ProgramCode", Programcode);
                    cmd.Parameters.AddWithValue("@_UserID", UserID);

                    MySqlDataAdapter da = new MySqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                    da.Fill(ds);
                    if (ds != null && ds.Tables[0] != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                DateofSchedule dateofSchedule = new DateofSchedule();
                                dateofSchedule.ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                                dateofSchedule.Day = ds.Tables[0].Rows[i]["DayName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DayName"]);
                                dateofSchedule.Dates = ds.Tables[0].Rows[i]["DateFormat"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DateFormat"]);
                                dateofSchedule.MaxPeopleAllowed = ds.Tables[0].Rows[i]["MaxPeopleAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["MaxPeopleAllowed"]);
                                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                                {
                                    dateofSchedule.AlreadyScheduleDetails = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<object>("AID")).Equals(dateofSchedule.ID))
                                        .Select(x => new AlreadyScheduleDetail()
                                        {
                                            TimeSlotId = x.Field<object>("SlotId") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("SlotId")),
                                            AppointmentDate = x.Field<object>("AppointmentDate") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("AppointmentDate")),
                                            VisitedCount = x.Field<object>("VisitedCount") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("VisitedCount")),
                                            MaxCapacity = x.Field<object>("MaxCapacity") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("MaxCapacity")),
                                            Remaining = x.Field<object>("Remaining") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("Remaining")),
                                            TimeSlot = x.Field<object>("TimeSlot") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("TimeSlot")),
                                            StoreId = x.Field<object>("StoreId") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("StoreId")),
                                            IsDisabled = x.Field<object>("IsDisabled") == System.DBNull.Value ? false : Convert.ToBoolean(x.Field<object>("IsDisabled")),

                                        }).ToList();
                                }
                                lstdateofSchedule.Add(dateofSchedule);
                            }
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
            return lstdateofSchedule;
        }

        /// <summary>
        /// Send Message To Customer For Visit
        /// </summary>
        /// <param name="appointmentMaster"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int SendMessageToCustomerForVisit(AppointmentMaster appointmentMaster, string ClientAPIURL, int CreatedBy)
        {
            int resultCount = 0;
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

                #endregion
               
            }
            catch (Exception)
            {
                throw;
            }
            return resultCount;
        }
    }
}
