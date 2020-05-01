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
        public List<AppointmentModel> GetAppointmentList(int TenantID)
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
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AppointmentModel obj = new AppointmentModel
                        {
                            AppointmentDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["AppointmentDate"]),
                            TimeSlot = Convert.ToDateTime(ds.Tables[0].Rows[i]["TimeSlot"]),
                            NOofPeople = Convert.ToInt32(ds.Tables[0].Rows[i]["NOofPeople"]),
                            AppointmentCustomerList = new List<AppointmentCustomer>()
                        };


                        obj.AppointmentCustomerList = ds.Tables[1].AsEnumerable().Where(x => Convert.ToDateTime(x.Field<DateTime>("AppointmentDate")).
                    Equals(obj.AppointmentDate)).Select(x => new AppointmentCustomer()
                    {
                        CustomerName = Convert.ToString(x.Field<string>("CustomerName")),
                        CustomerNumber = Convert.ToString(x.Field<string>("CustomerNumber")),
                        NOofPeople = Convert.ToInt32(x.Field<int>("NOofPeople")),
                        Status = x.Field<object>("Status") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("Status")),
                    }).ToList();





                        appointments.Add(obj);
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
            }

            return appointments;
        }
        /// <summary>
        /// Get Appointment Count
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<AppointmentCount> GetAppointmentCount(int TenantID)
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
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AppointmentCount obj = new AppointmentCount
                        {
                            Today = Convert.ToInt32(ds.Tables[0].Rows[i]["Today"]),
                            Tomorrow = Convert.ToInt32(ds.Tables[1].Rows[i]["Tomorrow"]),
                            DayAfterTomorrow = Convert.ToInt32(ds.Tables[2].Rows[i]["DayAfterTomorrow"])
                        };

                       appointmentsCount.Add(obj);
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
            }

            return appointmentsCount;

        }



    }
}
