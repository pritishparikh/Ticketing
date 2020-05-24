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
    }
}
