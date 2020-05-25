using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class AppointmentServices : IAppointment
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
                            Status = x.Field<int?>("Status").ToString() == "" ? "" :
                        Convert.ToInt32(x.Field<int?>("Status")) == 1 ? "Visited" :
                        Convert.ToInt32(x.Field<int?>("Status")) == 2 ? "Not Visited" : "Cancel",
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
        public List<AppointmentDetails> CreateAppointment(AppointmentMaster appointmentMaster)
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
                cmd.Parameters.AddWithValue("@Time_Slot", appointmentMaster.TimeSlot);
                cmd.Parameters.AddWithValue("@Tenant_ID", appointmentMaster.TenantID);
                cmd.Parameters.AddWithValue("@Created_By", appointmentMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@NOof_People", appointmentMaster.NOofPeople);
                cmd.Parameters.AddWithValue("@Mobile_No", appointmentMaster.MobileNo);
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
                            Status = x.Field<int?>("Status").ToString() == "" ? "" :
                        Convert.ToInt32(x.Field<int?>("Status")) == 1 ? "Visited" :
                        Convert.ToInt32(x.Field<int?>("Status")) == 2 ? "Not Visited" : "Cancel",
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
                MySqlCommand cmd1 = new MySqlCommand("", conn);
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
    }
}
