using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class CustomerService : ICustomer
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        CustomResponse ApiResponse = null;
        string apiResponse = string.Empty;
        string apisecurityToken = string.Empty;
        string apiURL = string.Empty;
        public CustomerService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
            apisecurityToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJQcm9ncmFtQ29kZSI6IkJhdGEiLCJVc2VySUQiOiIzIiwiQXBwSUQiOiI3IiwiRGF5IjoiMjgiLCJNb250aCI6IjMiLCJZZWFyIjoiMjAyMSIsIlJvbGUiOiJBZG1pbiIsImlzcyI6IkF1dGhTZWN1cml0eUlzc3VlciIsImF1ZCI6IkF1dGhTZWN1cml0eUF1ZGllbmNlIn0.0XeF7V5LWfQn0NlSlG7Rb-Qq1hUCtUYRDg6dMGIMvg0";
            apiURL = "http://searchapi.ercx.co/api/Search/CustomerDetails";
        }
        #endregion


        /// <summary>
        /// Get Customer By Id
        /// </summary>
        /// <param name="CustomerID"></param>  
        /// <param name="TenantId"></param>  
        /// <returns></returns>
        public CustomerMaster getCustomerbyId(int CustomerID,int TenantId)
        {
            DataSet ds = new DataSet();
            CustomerMaster customerMaster = new CustomerMaster();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getCustomerDetailsById", conn);
                cmd1.Parameters.AddWithValue("@Customer_ID", CustomerID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {

                    if(ds.Tables[0].Rows.Count> 0)
                        {
                        customerMaster.CustomerID = ds.Tables[0].Rows[0]["CustomerID"] == DBNull.Value ? 0 :  Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"]);
                        customerMaster.TenantID = ds.Tables[0].Rows[0]["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["TenantID"]);
                        customerMaster.CustomerName = ds.Tables[0].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                        customerMaster.CustomerPhoneNumber = ds.Tables[0].Rows[0]["CustomerPhoneNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CustomerPhoneNumber"]);
                        customerMaster.CustomerEmailId = ds.Tables[0].Rows[0]["CustomerEmailId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CustomerEmailId"]);
                        customerMaster.GenderID = ds.Tables[0].Rows[0]["GenderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["GenderID"]);
                        customerMaster.AltNumber = ds.Tables[0].Rows[0]["AltNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["AltNumber"]);
                        customerMaster.AltEmailID = ds.Tables[0].Rows[0]["AltEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["AltEmailID"]);
                        customerMaster.DateOfBirth = default(DateTime);
                        customerMaster.IsActive = ds.Tables[0].Rows[0]["IsActive"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["IsActive"]);
                        customerMaster.DOB = ds.Tables[0].Rows[0]["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["DOB"]);

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

                if (ds!=null)
                {
                    ds.Dispose();
                }

            }
            return customerMaster;
        }


        /// <summary>
        /// Get Customer BY Email and PhoneNo
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<CustomerMaster> getCustomerbyEmailIdandPhoneNo(string searchText, int TenantId, int UserID)
        {
            List<CustomerMaster> customerMasters = new List<CustomerMaster>();

            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
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
                        CustomerMaster customer = new CustomerMaster
                        {
                            CustomerName = dt.Rows[i]["CustomerName"] == DBNull.Value ? string.Empty: Convert.ToString(dt.Rows[i]["CustomerName"]),
                            CustomerID = dt.Rows[i]["CustomerID"] == DBNull.Value ? 0 :  Convert.ToInt32(dt.Rows[i]["CustomerID"])
                        };

                        customerMasters.Add(customer);
                    }
                }
                else
                {
                    //get data from API
                    customerMasters= GetCustomerByAPI(searchText, TenantId, UserID);
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
            return customerMasters;
        }

        /// <summary>
        /// Add Customer Detail
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int addCustomerDetails(CustomerMaster customerMaster,int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
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
        /// Update Customer Details
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int updateCustomerDetails(CustomerMaster customerMaster,int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_updateCustomer", conn);
                cmd1.Parameters.AddWithValue("@objCustomerID", customerMaster.CustomerID);
                cmd1.Parameters.AddWithValue("@TenantID", TenantId);
                cmd1.Parameters.AddWithValue("@CustomerName", customerMaster.CustomerName);
                cmd1.Parameters.AddWithValue("@CustomerPhoneNumber", customerMaster.CustomerPhoneNumber);
                cmd1.Parameters.AddWithValue("@CustomerEmailId", customerMaster.CustomerEmailId);
                cmd1.Parameters.AddWithValue("@GenderID", customerMaster.GenderID);
                cmd1.Parameters.AddWithValue("@dateOfBirth", customerMaster.DateOfBirth);
                cmd1.Parameters.AddWithValue("@AltNumber", customerMaster.AltNumber);
                cmd1.Parameters.AddWithValue("@AltEmailID", customerMaster.AltEmailID);
                cmd1.Parameters.AddWithValue("@IsActive", customerMaster.IsActive);
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
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("Sp_ValidateCustomerExists", conn);
                cmd1.Parameters.AddWithValue("@Cust_EmailId", Cust_EmailId);
                cmd1.Parameters.AddWithValue("@Cust_PhoneNumber", Cust_PhoneNumber);
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantId);
               
                cmd1.CommandType = CommandType.StoredProcedure;
                message = Convert.ToString(cmd1.ExecuteScalar());
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

        public List<CustomerMaster> GetCustomerByAPI(string searchText, int TenantId,int UserID)
        {
            List<CustomerMaster> customerMasters = new List<CustomerMaster>();
            CustomSearchCustomer apiSearchCustomer = new CustomSearchCustomer();
            List < CustomCustomerDetails> customCustomerDetails = new List<CustomCustomerDetails>();

           
            int InsertedCustID = 0;
            try
            {
                apiSearchCustomer.programCode = "Bata";
                apiSearchCustomer.mobileNumber = searchText;
                apiSearchCustomer.customerName = "";
                apiSearchCustomer.email = "";
                apiSearchCustomer.securityToken = apisecurityToken;
                apiSearchCustomer.userID = 3;
                apiSearchCustomer.appID = 7;

                string apiReq = JsonConvert.SerializeObject(apiSearchCustomer);
                apiResponse = CommonService.SendApiRequest(apiURL, apiReq);

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    ApiResponse = JsonConvert.DeserializeObject<CustomResponse>(apiResponse);

                    if (ApiResponse != null)
                    {
                        customCustomerDetails = JsonConvert.DeserializeObject<List<CustomCustomerDetails>>(Convert.ToString((ApiResponse.Responce)));
                        if (customCustomerDetails != null )
                        {
                            if(customCustomerDetails.Count > 0)
                            {

                                for(int k =0; k< customCustomerDetails.Count; k++ )
                                {
                                    CustomerMaster customerDetails = new CustomerMaster();
                                    customerDetails.TenantID = TenantId;
                                    customerDetails.CustomerName = customCustomerDetails[k].CustomerName;
                                    customerDetails.CreatedBy = UserID;
                                    customerDetails.CustomerPhoneNumber = customCustomerDetails[k].MobileNumber;
                                    customerDetails.CustomerEmailId = customCustomerDetails[k].Email;
                                    customerDetails.GenderID = customCustomerDetails[k].Gender.ToLower().Equals("male") ? 1 : 2;
                                    customerDetails.AltEmailID = customCustomerDetails[k].Email;
                                    customerDetails.AltNumber = customCustomerDetails[k].MobileNumber;
                                    customerDetails.IsActive = Convert.ToInt16(true);
                                    
                                   
                                    

                                    //call add customer
                                    InsertedCustID = addCustomerDetails(customerDetails, TenantId);
                                    if (InsertedCustID > 0)
                                    {
                                        customerDetails.CustomerID = InsertedCustID;
                                    }

                                    customerMasters.Add(customerDetails);
                                }
                            }
                            

                            
                        }
                        
                    }

                }


            }
            catch (Exception )
            {
                throw;
            }

            return customerMasters;
        }
    }
}
