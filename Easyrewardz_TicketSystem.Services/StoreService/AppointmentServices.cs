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
                            ApointmentDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ApointmentDate"]),
                            TimeSlot = Convert.ToDateTime(ds.Tables[0].Rows[i]["TimeSlot"]),
                            NoOfPeople = Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfPeople"]),
                            AppointmentCustomerList = new List<AppointmentCustomer>()
                        };


                        obj.AppointmentCustomerList = ds.Tables[1].AsEnumerable().Where(x => Convert.ToDateTime(x.Field<DateTime>("ApointmentDate")).
                    Equals(obj.ApointmentDate)).Select(x => new AppointmentCustomer()
                    {
                        CustomerName = Convert.ToString(x.Field<string>("CustomerName")),
                        MobileNo = Convert.ToString(x.Field<string>("MobileNo")),
                        NoOfPeople = Convert.ToInt32(x.Field<int>("NoOfPeople")),
                        Status = Convert.ToInt32(x.Field<int>("Status"))

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
    }
}
