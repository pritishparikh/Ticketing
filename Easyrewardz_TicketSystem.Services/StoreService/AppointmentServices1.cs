using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model.StoreModal;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class AppointmentServices : IAppointment
    {
        MySqlConnection conn = new MySqlConnection();

        public AppointmentServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Get AppointmentList List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<AppointmentModel> GetAppointmentList(int TenantID, int UserId, string AppDate)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentModel> appointments = new List<AppointmentModel>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSAppointmentDeatils", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd1.Parameters.AddWithValue("@User_Id", UserId);
                cmd1.Parameters.AddWithValue("@Apt_Date", AppDate);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AppointmentModel obj = new AppointmentModel
                        {
                            AppointmentDate = Convert.ToString(ds.Tables[0].Rows[i]["AppointmentDate"]),
                            SlotId = Convert.ToInt32(ds.Tables[0].Rows[i]["SlotId"]),
                            TimeSlot = Convert.ToString(ds.Tables[0].Rows[i]["TimeSlot"]),
                            NOofPeople = Convert.ToInt32(ds.Tables[0].Rows[i]["NOofPeople"]),
                            MaxCapacity = Convert.ToInt32(ds.Tables[0].Rows[i]["MaxCapacity"]),
                            AppointmentCustomerList = new List<AppointmentCustomer>()
                        };


                        obj.AppointmentCustomerList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<string>("AppointmentDate")).
                        Equals(obj.AppointmentDate) && (x.Field<int>("SlotId")).Equals(obj.SlotId)).Select(x => new AppointmentCustomer()
                        {
                            AppointmentID = Convert.ToInt32(x.Field<int>("AppointmentID")),
                            CustomerName = Convert.ToString(x.Field<string>("CustomerName")),
                            CustomerNumber = Convert.ToString(x.Field<string>("CustomerNumber")),
                            NOofPeople = Convert.ToInt32(x.Field<int>("NOofPeople")),
                            PeopleEntered = x.Field<int?>("PeopleEntered") == null ? 0 : Convert.ToInt32(x.Field<int>("PeopleEntered")),
                            PeopleCheckout = x.Field<int?>("PeopleCheckout") == null ? 0 : Convert.ToInt32(x.Field<int>("PeopleCheckout")),
                            Status = Convert.ToString(x.Field<string>("Status")),
                        //    Status = x.Field<int?>("Status").ToString() == "" ? "" :
                        //Convert.ToInt32(x.Field<int?>("Status")) == 1 ? "Visited" :
                        //Convert.ToInt32(x.Field<int?>("Status")) == 2 ? "Not Visited" : "Cancel",
                        }).ToList();

                        appointments.Add(obj);
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

            return appointments;
        }

        /// <summary>
        /// Get Appointment Count
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<AppointmentCount> GetAppointmentCount(int TenantID, int UserId)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentCount> appointmentsCount = new List<AppointmentCount>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSGetAppointmentCount", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd1.Parameters.AddWithValue("@User_Id", UserId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                AppointmentCount obj = new AppointmentCount
                {
                    Today = ds.Tables[0].Rows.Count == 0 ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["Today"]),
                    Tomorrow = ds.Tables[1].Rows.Count == 0 ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["Tomorrow"]),
                    DayAfterTomorrow = ds.Tables[2].Rows.Count == 0 ? 0 : Convert.ToInt32(ds.Tables[2].Rows[0]["DayAfterTomorrow"])
                };

                appointmentsCount.Add(obj);
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
            }

            return appointmentsCount;

        }

        public int UpdateAppointmentStatus(AppointmentCustomer appointmentCustomer, int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSUpdateAppoinmentStatus", conn);
                cmd1.Parameters.AddWithValue("@Appointment_ID", appointmentCustomer.AppointmentID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.Parameters.AddWithValue("@_Status", appointmentCustomer.Status);
                cmd1.Parameters.AddWithValue("@_PeopleCheckout", appointmentCustomer.PeopleCheckout);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = cmd1.ExecuteNonQuery();
                conn.Close();
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
        /// Create Appointment
        /// </summary>
        /// <param name="appointmentMaster"></param>
        /// <returns></returns>
        public List<AppointmentDetails> CreateAppointment(AppointmentMaster appointmentMaster, bool IsSMS, bool IsLoyalty)
        {
            int message;
            DataSet ds = new DataSet();
            List<AppointmentDetails> lstAppointmentDetails = new List<AppointmentDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSCreateAppointment", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Appointment_Date", appointmentMaster.AppointmentDate);
                cmd.Parameters.AddWithValue("@CustomerName", appointmentMaster.CustomerName);
                cmd.Parameters.AddWithValue("@Slot_ID", appointmentMaster.SlotID);
                cmd.Parameters.AddWithValue("@Tenant_ID", appointmentMaster.TenantID);
                cmd.Parameters.AddWithValue("@Created_By", appointmentMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@NOof_People", appointmentMaster.NOofPeople);
                cmd.Parameters.AddWithValue("@Mobile_No", appointmentMaster.MobileNo);
                cmd.Parameters.AddWithValue("@Is_SMS", IsSMS);
                cmd.Parameters.AddWithValue("@Is_Loyalty", IsLoyalty);
                cmd.CommandType = CommandType.StoredProcedure;
                //message = Convert.ToInt32(cmd.ExecuteScalar());
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AppointmentDetails appointmentDetails = new AppointmentDetails();
                        appointmentDetails.AppointmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppointmentID"]);
                        appointmentDetails.CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        appointmentDetails.MobileNo = ds.Tables[0].Rows[i]["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNo"]);
                        appointmentDetails.StoreName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        appointmentDetails.StoreAddress = ds.Tables[0].Rows[i]["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreAddress"]);
                        appointmentDetails.NoOfPeople = ds.Tables[0].Rows[i]["NOofPeople"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NOofPeople"]);

                        lstAppointmentDetails.Add(appointmentDetails);
                    }
                }

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
            return lstAppointmentDetails;
        }

        /// <summary>
        /// Create Appointment for non existing customer
        /// </summary>
        /// <param name="appointmentMaster"></param>
        /// <returns></returns>
        public List<AppointmentDetails> CreateNonExistCustAppointment(AppointmentMaster appointmentMaster, bool IsSMS, bool IsLoyalty)
        {
            int message;
            DataSet ds = new DataSet();
            List<AppointmentDetails> lstAppointmentDetails = new List<AppointmentDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSCreateNonExistAppointment", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Appointment_Date", appointmentMaster.AppointmentDate);
                cmd.Parameters.AddWithValue("@CustomerName", appointmentMaster.CustomerName);
                cmd.Parameters.AddWithValue("@Time_Slot", appointmentMaster.TimeSlot);
                cmd.Parameters.AddWithValue("@Tenant_ID", appointmentMaster.TenantID);
                cmd.Parameters.AddWithValue("@Created_By", appointmentMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@NOof_People", appointmentMaster.NOofPeople);
                cmd.Parameters.AddWithValue("@Mobile_No", appointmentMaster.MobileNo);
                cmd.Parameters.AddWithValue("@Is_SMS", IsSMS);
                cmd.Parameters.AddWithValue("@Is_Loyalty", IsLoyalty);
                cmd.CommandType = CommandType.StoredProcedure;
                //message = Convert.ToInt32(cmd.ExecuteScalar());
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AppointmentDetails appointmentDetails = new AppointmentDetails();
                        appointmentDetails.AppointmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppointmentID"]);
                        appointmentDetails.CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        appointmentDetails.MobileNo = ds.Tables[0].Rows[i]["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNo"]);
                        appointmentDetails.StoreName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        appointmentDetails.StoreAddress = ds.Tables[0].Rows[i]["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreAddress"]);
                        appointmentDetails.NoOfPeople = ds.Tables[0].Rows[i]["NOofPeople"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NOofPeople"]);

                        lstAppointmentDetails.Add(appointmentDetails);
                    }
                }

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
            return lstAppointmentDetails;
        }

        /// <summary>
        /// Search Appointment
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<AppointmentModel> SearchAppointment(int TenantID, int UserId, string searchText, string appointmentDate)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentModel> appointments = new List<AppointmentModel>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSSearchAppointment", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd1.Parameters.AddWithValue("@User_Id", UserId);
                cmd1.Parameters.AddWithValue("@search_Text", searchText);
                cmd1.Parameters.AddWithValue("@appointment_Date", appointmentDate);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AppointmentModel obj = new AppointmentModel
                        {
                            AppointmentDate = Convert.ToString(ds.Tables[0].Rows[i]["AppointmentDate"]),
                            SlotId = Convert.ToInt32(ds.Tables[0].Rows[i]["SlotId"]),
                            TimeSlot = Convert.ToString(ds.Tables[0].Rows[i]["TimeSlot"]),
                            NOofPeople = Convert.ToInt32(ds.Tables[0].Rows[i]["NOofPeople"]),
                            MaxCapacity = Convert.ToInt32(ds.Tables[0].Rows[i]["MaxCapacity"]),
                            AppointmentCustomerList = new List<AppointmentCustomer>()
                        };


                        obj.AppointmentCustomerList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<string>("AppointmentDate")).
                        Equals(obj.AppointmentDate) && (x.Field<int>("SlotId")).Equals(obj.SlotId)).Select(x => new AppointmentCustomer()
                        {
                            AppointmentID = Convert.ToInt32(x.Field<int>("AppointmentID")),
                            CustomerName = Convert.ToString(x.Field<string>("CustomerName")),
                            CustomerNumber = Convert.ToString(x.Field<string>("CustomerNumber")),
                            NOofPeople = Convert.ToInt32(x.Field<int>("NOofPeople")),
                            Status = Convert.ToString(x.Field<string>("Status")),
                            //    Status = x.Field<int?>("Status").ToString() == "" ? "" :
                            //Convert.ToInt32(x.Field<int?>("Status")) == 1 ? "Visited" :
                            //Convert.ToInt32(x.Field<int?>("Status")) == 2 ? "Not Visited" : "Cancel",
                        }).ToList();

                        appointments.Add(obj);
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

            return appointments;
        }

        public int GenerateOTP(int TenantID, int UserId, string mobileNumber)
        {
            int OTPID = 0;
            try
            {
                //For generating OTP
                Random r = new Random();
                string OTP = r.Next(1000, 9999).ToString();

                MySqlCommand cmd = new MySqlCommand();

                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSSaveGenerateOTP", conn);
                cmd1.Parameters.AddWithValue("@User_Id", UserId);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@mobile_OTP", OTP);
                cmd1.Parameters.AddWithValue("@mobile_Number", mobileNumber);
                cmd1.CommandType = CommandType.StoredProcedure;
                OTPID = Convert.ToInt32(cmd1.ExecuteScalar());


                //Send message
                //string Username = "";
                //string APIKey = "";//This may vary api to api. like ite may be password, secrate key, hash etc
                //string SenderName = "";
                //string Number = mobileNumber;
                //string Message = "Your OTP code is - " + OTP;
                ////string URL = "http://api.urlname.in/send/?username=" + Username + "&hash=" + APIKey + "&sender=" + SenderName + "&numbers=" + Number + "&message=" + Message;
                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                //StreamReader sr = new StreamReader(resp.GetResponseStream());
                //string results = sr.ReadToEnd();
                //sr.Close();
                conn.Close();
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
            return OTPID;
        }

        public int VarifyOTP(int TenantID, int UserId, int otpID, string otp)
        {
            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSVarifyOTP", conn);
                cmd1.Parameters.AddWithValue("@UserId", UserId);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@otp_ID", otpID);
                cmd1.Parameters.AddWithValue("@_otp", otp);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();
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

        public int UpdateAppointment(CustomUpdateAppointment appointmentCustomer)
        {
            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSUpdateAppointment", conn);
                cmd1.Parameters.AddWithValue("@Appointment_ID", appointmentCustomer.AppointmentID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", appointmentCustomer.TenantID);
                cmd1.Parameters.AddWithValue("@_Status", appointmentCustomer.Status);
                cmd1.Parameters.AddWithValue("@_NOofPeople", appointmentCustomer.NOofPeople);
                cmd1.Parameters.AddWithValue("@Program_Code", appointmentCustomer.ProgramCode);
                cmd1.Parameters.AddWithValue("@Slot_Id", appointmentCustomer.SlotId);
                cmd1.Parameters.AddWithValue("@Slot_date", string.IsNullOrEmpty(appointmentCustomer.Slotdate) ? string.Empty : appointmentCustomer.Slotdate);
                cmd1.Parameters.AddWithValue("@User_ID", appointmentCustomer.UserID);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = cmd1.ExecuteNonQuery();
                conn.Close();
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

        public List<AlreadyScheduleDetail> GetTimeSlotDetail(int userMasterID, int tenantID, string AppDate)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<AlreadyScheduleDetail> lstAlreadyScheduleDetail = new List<AlreadyScheduleDetail>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSGetTimeSlotsDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd1.Parameters.AddWithValue("@User_ID", userMasterID);
                cmd1.Parameters.AddWithValue("@Apt_Date", AppDate);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                                AlreadyScheduleDetail alreadyScheduleDetail = new AlreadyScheduleDetail();
                                alreadyScheduleDetail.TimeSlotId = ds.Tables[0].Rows[i]["SlotId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SlotId"]);
                                alreadyScheduleDetail.AppointmentDate = ds.Tables[0].Rows[i]["AppointmentDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AppointmentDate"]);
                                alreadyScheduleDetail.VisitedCount = ds.Tables[0].Rows[i]["VisitedCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["VisitedCount"]);
                                alreadyScheduleDetail.MaxCapacity = ds.Tables[0].Rows[i]["MaxCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["MaxCapacity"]);
                                alreadyScheduleDetail.Remaining = ds.Tables[0].Rows[i]["Remaining"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Remaining"]);
                                alreadyScheduleDetail.TimeSlot = ds.Tables[0].Rows[i]["TimeSlot"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TimeSlot"]);
                                alreadyScheduleDetail.StoreId = ds.Tables[0].Rows[i]["StoreId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StoreId"]);
                                lstAlreadyScheduleDetail.Add(alreadyScheduleDetail);
                         
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
            return lstAlreadyScheduleDetail;
        }

        public int ValidateMobileNo(int TenantID, int UserId, string mobileNumber)
        {
            MySqlCommand cmd = new MySqlCommand();
            int message;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("Sp_HSValidateMobileNo", conn);
                cmd1.Parameters.AddWithValue("@mobile_Number", mobileNumber);
                cmd1.Parameters.AddWithValue("@User_Id", UserId);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.CommandType = CommandType.StoredProcedure;
                message = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();
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

        public int StartVisit(CustomUpdateAppointment appointmentCustomer)
        {
            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSStartAppointmentVisit", conn);
                cmd1.Parameters.AddWithValue("@Appointment_ID", appointmentCustomer.AppointmentID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", appointmentCustomer.TenantID);
                cmd1.Parameters.AddWithValue("@_Status", appointmentCustomer.Status);
                cmd1.Parameters.AddWithValue("@_NOofPeople", appointmentCustomer.NOofPeople);
                cmd1.Parameters.AddWithValue("@Slot_Id", appointmentCustomer.SlotId);
                cmd1.Parameters.AddWithValue("@User_ID", appointmentCustomer.UserID);
                cmd1.Parameters.AddWithValue("@Customer_Number", appointmentCustomer.CustomerNumber);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = cmd1.ExecuteNonQuery();
                conn.Close();
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


        #region TimeSlotMaster CRUD

        /// <summary>
        /// Insert/ Update HSTimeSlotMaster
        /// </summary>
        /// <param name="StoreTimeSlotInsertUpdate"></param>
        /// <returns></returns>
        /// 
        public int InsertUpdateTimeSlotMaster(StoreTimeSlotInsertUpdate Slot)
        {

            int Result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                MySqlCommand cmd = new MySqlCommand("SP_HSInsertUpdateHSStoreTimeSlotMaster", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_SlotId", Slot.SlotId);
                cmd.Parameters.AddWithValue("@_TenantId", Slot.TenantId);
                cmd.Parameters.AddWithValue("@_StoreId", Slot.StoreId);
                cmd.Parameters.AddWithValue("@_ProgramCode", Slot.ProgramCode); 
                cmd.Parameters.AddWithValue("@_TimeSlot", string.IsNullOrEmpty(Slot.TimeSlot) ? "" :Slot.TimeSlot);
                cmd.Parameters.AddWithValue("@_MaxCapacity", Slot.MaxCapacity);
                cmd.Parameters.AddWithValue("@_OrderNo", Slot.OrderNumber); 
                cmd.Parameters.AddWithValue("@_CreatedBy", Slot.CreatedBy); 
                cmd.Parameters.AddWithValue("@_ModifyBy", Slot.ModifyBy);

                cmd.CommandType = CommandType.StoredProcedure;
                Result = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
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

            return Result;
        }

        /// <summary>
        /// Delete HSTimeSlotMaster
        /// </summary>
        /// <param name="SlotID"></param>
        /// <returns></returns>
        public int DeleteTimeSlotMaster(int SlotID, int TenantID)
        {

            int Result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                MySqlCommand cmd = new MySqlCommand("SP_DeleteStoreTimeSlotMaster", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_SlotId", SlotID);
                cmd.Parameters.AddWithValue("@_TenantId", TenantID);
               
                cmd.CommandType = CommandType.StoredProcedure;
                Result = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
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

            return Result;
        }

        /// <summary>
        /// Get HSTimeSlotMaster List
        /// </summary>
        /// <returns></returns>
        public List<StoreTimeSlotMasterModel> StoreTimeSlotMasterList(int TenantID, string ProgramCode)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreTimeSlotMasterModel> TimeSlotList = new List<StoreTimeSlotMasterModel>();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreTimeSlotDetails", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_TenantId", TenantID);
                cmd1.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);


                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TimeSlotList.Add(new StoreTimeSlotMasterModel()
                            {

                                SlotId = dr["SlotId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotId"]),
                                TenantId = dr["TenantId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TenantId"]),
                                ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                StoreId = dr["StoreId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreId"]),
                                StoreCode = dr["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreCode"]),
                                StoreName = dr["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreName"]),
                                TimeSlot = dr["TimeSlot"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TimeSlot"]),
                                OrderNumber = dr["OrderNumber"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrderNumber"]),
                                MaxCapacity = dr["MaxCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxCapacity"]),
                                CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                ModifyBy = dr["ModifyBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                ModifyDate = dr["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyDate"]),



                            });

                              
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

            return TimeSlotList;

        }

        public List<CustomerCountDetail> GetCustomerInStore(int TenantID, int UserId)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomerCountDetail> appointments = new List<CustomerCountDetail>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetCustomerInStore", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd1.Parameters.AddWithValue("@User_Id", UserId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomerCountDetail customerCountDetail = new CustomerCountDetail();
                        customerCountDetail.SlotId = Convert.ToInt32(ds.Tables[0].Rows[i][""]);
                        customerCountDetail.InStoreCount = ds.Tables[0].Rows[i][""] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][""]);
                        customerCountDetail.TimeSlot= ds.Tables[0].Rows[i][""] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i][""]);
                        appointments.Add(customerCountDetail);
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

            return appointments;
        }


        #endregion
    }



}
