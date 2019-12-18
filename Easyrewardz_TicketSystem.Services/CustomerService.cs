using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class CustomerService : ICustomer
    {
        MySqlConnection conn = new MySqlConnection();
        public CustomerService()
        {
            conn.ConnectionString = "Data Source = 13.67.69.216; port = 3306; Initial Catalog = Ticketing; User Id = brainvire; password = Logitech@123";
        }
        public List<CustomerMaster> getCustomerbyId(int CustomerID)
        {
            List<CustomerMaster> customerMasters = new List<CustomerMaster>();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getCustomerDetailsById", conn);
                cmd1.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                conn.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    customerMasters.Add(
                        new CustomerMaster
                        {
                            TenantID = Convert.ToInt32(dr["TenantID"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerPhoneNumber = Convert.ToString(dr["CustomerPhoneNumber"]),
                            CustomerEmailId = Convert.ToString(dr["CustomerEmailId"]),
                            GenderID = Convert.ToInt32(dr["GenderID"]),
                            AltNumber = Convert.ToString(dr["AltNumber"]),
                            AltEmailID = Convert.ToString(dr["AltEmailID"])
                        });
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return customerMasters;
        }
    }
}
