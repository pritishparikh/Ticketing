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
            int OutTenantID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertCompany", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd.Parameters.AddWithValue("@User_ID", companyModel.CreatedBy);
                cmd.Parameters.AddWithValue("@Company_Type", companyModel.CompanyTypeID);
                cmd.Parameters.AddWithValue("@Company_Name", companyModel.CompanyName);
                cmd.Parameters.AddWithValue("@CompanyIncorporation_date", companyModel.CompanyIncorporationDate);
                cmd.Parameters.AddWithValue("@Company_NoOfEmployee", companyModel.NoOfEmployee);
                cmd.Parameters.AddWithValue("@Company_Email", companyModel.CompanayEmailID);
                cmd.Parameters.AddWithValue("@Company_ContactNo", companyModel.CompanayContactNo);
                cmd.Parameters.AddWithValue("@Contact_Person", companyModel.ContactPersonName);
                cmd.Parameters.AddWithValue("@ContactPerson_No", companyModel.ContactPersonNo);
                cmd.Parameters.AddWithValue("@Companay_Address", companyModel.CompanayAddress);
                cmd.Parameters.AddWithValue("@Pincode", companyModel.Pincode);
                cmd.Parameters.AddWithValue("@City", companyModel.CityID);
                cmd.Parameters.AddWithValue("@State", companyModel.StateID);
                cmd.Parameters.AddWithValue("@Country", companyModel.CountryID);
                cmd.Parameters.Add("@OutTenantID", MySqlDbType.Int32);
                cmd.Parameters["@OutTenantID"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                OutTenantID = Convert.ToInt32(cmd.Parameters["@OutTenantID"].Value.ToString());
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

            return OutTenantID;

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
