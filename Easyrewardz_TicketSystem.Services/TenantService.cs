using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class TenantService : ITenant
    {
        MySqlConnection conn = new MySqlConnection();

        public TenantService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public int InsertCompany(CompanyModel companyModel, int TenantId)
        {
           
            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_InsertCompany", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.Parameters.AddWithValue("@User_ID", companyModel.CreatedBy);
                cmd1.Parameters.AddWithValue("@Company_Type", companyModel.CompanyTypeID);
                cmd1.Parameters.AddWithValue("@Company_Name", companyModel.CompanyName);
                cmd1.Parameters.AddWithValue("@CompanyIncorporation_date", companyModel.CompanyIncorporationDate);
                cmd1.Parameters.AddWithValue("@Company_NoOfEmployee", companyModel.NoOfEmployee);
                cmd1.Parameters.AddWithValue("@Company_Email", companyModel.CompanayEmailID);
                cmd1.Parameters.AddWithValue("@Company_ContactNo", companyModel.CompanayContactNo);
                cmd1.Parameters.AddWithValue("@Contact_Person", companyModel.ContactPersonName);
                cmd1.Parameters.AddWithValue("@ContactPerson_No", companyModel.ContactPersonNo);
                cmd1.Parameters.AddWithValue("@Companay_Address", companyModel.CompanayAddress);
                cmd1.Parameters.AddWithValue("@Pincode", companyModel.Pincode);
                cmd1.Parameters.AddWithValue("@City", companyModel.CityID);
                cmd1.Parameters.AddWithValue("@State", companyModel.StateID);
                cmd1.Parameters.AddWithValue("@Country", companyModel.CountryID);
        

                k = Convert.ToInt32(cmd1.ExecuteNonQuery());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return k;

        }

        public int BillingDetails_crud(BillingDetails BillingDetails)
        {
            
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_BillingDetails_crud", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Billing_ID", BillingDetails.Billing_ID);
                cmd.Parameters.AddWithValue("@InvoiceBilling_ID", BillingDetails.InvoiceBilling_ID);
                cmd.Parameters.AddWithValue("@Tennant_ID", BillingDetails.Tennant_ID);
                cmd.Parameters.AddWithValue("@CompanyRegistration_Number", BillingDetails.CompanyRegistration_Number);
                cmd.Parameters.AddWithValue("@GSTTIN_Number", BillingDetails.GSTTIN_Number);
                cmd.Parameters.AddWithValue("@Pan_No", BillingDetails.Pan_No);
                cmd.Parameters.AddWithValue("@Tan_No", BillingDetails.Tan_No);
                cmd.Parameters.AddWithValue("@Created_By", BillingDetails.Created_By);
                cmd.Parameters.AddWithValue("@Modified_By", BillingDetails.Modified_By);
                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
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
    }
}
