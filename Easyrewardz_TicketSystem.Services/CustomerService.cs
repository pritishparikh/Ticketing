﻿using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class CustomerService : ICustomer
    {
        #region variable
        private readonly IDistributedCache Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public CustomerService(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            Cache = cache;
        }
        #endregion


        /// <summary>
        /// Get Customer By Id
        /// </summary>
        /// <param name="CustomerID"></param>  
        /// <returns></returns>
        public CustomerMaster getCustomerbyId(int CustomerID,int TenantId)
        {
            DataSet ds = new DataSet();
            CustomerMaster customerMaster = new CustomerMaster();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getCustomerDetailsById", conn);
                cmd1.Parameters.AddWithValue("@Customer_ID", CustomerID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    customerMaster.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"]);
                    customerMaster.TenantID = Convert.ToInt32(ds.Tables[0].Rows[0]["TenantID"]);
                    customerMaster.CustomerName = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                    customerMaster.CustomerPhoneNumber = Convert.ToString(ds.Tables[0].Rows[0]["CustomerPhoneNumber"]);
                    customerMaster.CustomerEmailId = Convert.ToString(ds.Tables[0].Rows[0]["CustomerEmailId"]);
                    customerMaster.GenderID = Convert.ToInt32(ds.Tables[0].Rows[0]["GenderID"]);
                    customerMaster.AltNumber = Convert.ToString(ds.Tables[0].Rows[0]["AltNumber"]);
                    customerMaster.AltEmailID = Convert.ToString(ds.Tables[0].Rows[0]["AltEmailID"]);
                    customerMaster.DateOfBirth = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"]);
                    customerMaster.IsActive = Convert.ToInt32(ds.Tables[0].Rows[0]["IsActive"]);
                    customerMaster.DOB = customerMaster.DateOfBirth.ToString("dd/MM/yyyy");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            
            return customerMaster;
        }


        /// <summary>
        /// Get Customer BY Email and PhoneNo
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Phoneno"></param>
        /// <returns></returns>
        public List<CustomerMaster> getCustomerbyEmailIdandPhoneNo(string searchText, int TenantId)
        {
            List<CustomerMaster> customerMasters = new List<CustomerMaster>();

            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_searchCustomerByEmailAndPhone", conn);
                cmd1.Parameters.AddWithValue("@searchText", searchText);
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CustomerMaster customer = new CustomerMaster();
                        customer.CustomerName = Convert.ToString(dt.Rows[i]["CustomerName"]);
                        customer.CustomerID = Convert.ToInt32(dt.Rows[i]["CustomerID"]);

                        customerMasters.Add(customer);
                    }
                }
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            
            return customerMasters;
        }

        /// <summary>
        /// Add Customer Detail
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public int addCustomerDetails(CustomerMaster customerMaster,int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createCustomer", conn);
                cmd1.Parameters.AddWithValue("@TenantID", TenantId);
                cmd1.Parameters.AddWithValue("@CustomerName", customerMaster.CustomerName);
                cmd1.Parameters.AddWithValue("@CreatedBy", customerMaster.CreatedBy);
                cmd1.Parameters.AddWithValue("@CustomerPhoneNumber", customerMaster.CustomerPhoneNumber);
                cmd1.Parameters.AddWithValue("@CustomerEmailId", customerMaster.CustomerEmailId);
                cmd1.Parameters.AddWithValue("@GenderID", customerMaster.GenderID);
                cmd1.Parameters.AddWithValue("@AltNumber", customerMaster.AltNumber);
                cmd1.Parameters.AddWithValue("@AltEmailID", customerMaster.AltEmailID);
                cmd1.Parameters.AddWithValue("@IsActive", customerMaster.IsActive);
                cmd1.Parameters.AddWithValue("@DOB", customerMaster.DateOfBirth);
                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteScalar());

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
           
            return i;
        }


        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public int updateCustomerDetails(CustomerMaster customerMaster,int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_updateCustomer", conn);
                cmd1.Parameters.AddWithValue("@objCustomerID", customerMaster.CustomerID);
                cmd1.Parameters.AddWithValue("@TenantID", TenantId);
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
            
            return i;
        }

        /// <summary>
        /// Check Customer exist or not
        /// </summary>
        /// <param name="Cust_EmailId">Customer Email Id</param>
        /// <param name="Cust_PhoneNumber">Customer Phone Number </param>
        /// <param name="TenantId">Tenant Id</param>
        /// <returns></returns>
        public string validateCustomerExist(string Cust_EmailId, string Cust_PhoneNumber, int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            string message = "";
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("Sp_ValidateCustomerExists", conn);
                cmd1.Parameters.AddWithValue("@Cust_EmailId", Cust_EmailId);
                cmd1.Parameters.AddWithValue("@Cust_PhoneNumber", Cust_PhoneNumber);
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantId);
               
                cmd1.CommandType = CommandType.StoredProcedure;
                message = Convert.ToString(cmd1.ExecuteScalar());
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            
            return message;
        }
    }
}
