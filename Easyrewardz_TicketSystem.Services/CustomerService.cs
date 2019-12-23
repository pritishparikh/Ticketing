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
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public CustomerService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion


        /// <summary>
        /// Get Customer By Id
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public List<CustomerMaster> getCustomerbyId(int CustomerID)
        {
            DataSet ds = new DataSet();
            List<CustomerMaster> customerMasters = new List<CustomerMaster>();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getCustomerDetailsById", conn);
                cmd1.Parameters.AddWithValue("@Customer_ID", CustomerID);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomerMaster customerMaster = new CustomerMaster();
                        customerMaster.TenantID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        customerMaster.CustomerName = Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        customerMaster.CustomerPhoneNumber = Convert.ToString(ds.Tables[0].Rows[i]["CustomerPhoneNumber"]);
                        customerMaster.CustomerEmailId = Convert.ToString(ds.Tables[0].Rows[i]["CustomerEmailId"]);
                        customerMaster.GenderID = Convert.ToInt32(ds.Tables[0].Rows[i]["GenderID"]);
                        customerMaster.AltNumber = Convert.ToString(ds.Tables[0].Rows[i]["AltNumber"]);
                        customerMaster.AltEmailID = Convert.ToString(ds.Tables[0].Rows[i]["AltEmailID"]);
                        customerMaster.IsActive = Convert.ToInt32(ds.Tables[0].Rows[i]["IsActive"]);
                        customerMasters.Add(customerMaster);
                    }
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


        /// <summary>
        /// Get Customer BY Email and PhoneNo
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Phoneno"></param>
        /// <returns></returns>
        public CustomerMaster getCustomerbyEmailIdandPhoneNo(string Email, string Phoneno)
        {
            CustomerMaster customerMasters = new CustomerMaster();
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_searchCustomerByEmailAndPhone", conn);
                cmd1.Parameters.AddWithValue("@objEmailId", Email);
                cmd1.Parameters.AddWithValue("@objCustomerPhoneNumber", Phoneno);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    customerMasters.TenantID = Convert.ToInt32(dt.Rows[0]["TenantID"]);
                    customerMasters.CustomerName = Convert.ToString(dt.Rows[0]["CustomerName"]);
                    customerMasters.CustomerPhoneNumber = Convert.ToString(dt.Rows[0]["CustomerPhoneNumber"]);
                    customerMasters.CustomerEmailId = Convert.ToString(dt.Rows[0]["CustomerEmailId"]);
                    customerMasters.GenderID = Convert.ToInt32(dt.Rows[0]["GenderID"]);
                    customerMasters.AltNumber = Convert.ToString(dt.Rows[0]["AltNumber"]);
                    customerMasters.AltEmailID = Convert.ToString(dt.Rows[0]["AltEmailID"]);
                }
                conn.Close();
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

        /// <summary>
        /// Add Customer Detail
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public int addCustomerDetails(CustomerMaster customerMaster)
        {
            
            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createCustomer", conn);
                cmd1.Parameters.AddWithValue("@TenantID", customerMaster.TenantID);
                cmd1.Parameters.AddWithValue("@CustomerName", customerMaster.CustomerName);
                cmd1.Parameters.AddWithValue("@CustomerPhoneNumber", customerMaster.CustomerPhoneNumber);
                cmd1.Parameters.AddWithValue("@CustomerEmailId", customerMaster.CustomerEmailId);
                cmd1.Parameters.AddWithValue("@GenderID", customerMaster.GenderID);
                cmd1.Parameters.AddWithValue("@AltNumber", customerMaster.AltNumber);
                cmd1.Parameters.AddWithValue("@AltEmailID", customerMaster.AltEmailID);
                cmd1.Parameters.AddWithValue("@IsActive", customerMaster.IsActive);
                cmd1.Parameters.AddWithValue("@DOB", customerMaster.DateOfBirth);
                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();

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

            return i;
        }


        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public int updateCustomerDetails(CustomerMaster customerMaster)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_updateCustomer", conn);
                cmd1.Parameters.AddWithValue("@objCustomerID", customerMaster.CustomerID);
                cmd1.Parameters.AddWithValue("@TenantID", customerMaster.TenantID);
                cmd1.Parameters.AddWithValue("@CustomerName", customerMaster.CustomerName);
                cmd1.Parameters.AddWithValue("@CustomerPhoneNumber", customerMaster.CustomerPhoneNumber);
                cmd1.Parameters.AddWithValue("@CustomerEmailId", customerMaster.CustomerEmailId);
                cmd1.Parameters.AddWithValue("@GenderID", customerMaster.GenderID);
                cmd1.Parameters.AddWithValue("@AltNumber", customerMaster.AltNumber);
                cmd1.Parameters.AddWithValue("@AltEmailID", customerMaster.AltEmailID);
                cmd1.Parameters.AddWithValue("@IsActive", customerMaster.IsActive);
                cmd1.CommandType = CommandType.StoredProcedure;
                i = cmd1.ExecuteNonQuery();
                conn.Close();
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

            return i;
        }
    }
}
