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
            DataSet ds = new DataSet();
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
                cmd.Parameters.AddWithValue("@Company_Email", companyModel.CompanyEmailID);
                cmd.Parameters.AddWithValue("@Company_ContactNo", companyModel.CompanyContactNo);
                cmd.Parameters.AddWithValue("@Contact_Person", companyModel.ContactPersonName);
                cmd.Parameters.AddWithValue("@ContactPerson_No", companyModel.ContactPersonNo);
                cmd.Parameters.AddWithValue("@Company_Address", companyModel.CompanyAddress);
                cmd.Parameters.AddWithValue("@Pincode", companyModel.Pincode);
                cmd.Parameters.AddWithValue("@City", companyModel.CityID);
                cmd.Parameters.AddWithValue("@State", companyModel.StateID);
                cmd.Parameters.AddWithValue("@Country", companyModel.CountryID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        OutTenantID = ds.Tables[0].Rows[0]["TenantID"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["TenantID"]);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (ds != null)
                ds.Dispose();
                conn.Close();
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


        public int OtherDetails(OtherDetailsModel OtherDetails)
        {

            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertOtherDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", OtherDetails._TenantID);
                cmd.Parameters.AddWithValue("@_NoOfUsers", OtherDetails._NoOfUsers);
                cmd.Parameters.AddWithValue("@_NoOfSimultaneous", OtherDetails._NoOfSimultaneous);
                cmd.Parameters.AddWithValue("@_MonthlyTicketVolume", OtherDetails._MonthlyTicketVolume);
                cmd.Parameters.AddWithValue("@_TicketAchivePolicy", OtherDetails._TicketAchivePolicy);
                cmd.Parameters.AddWithValue("@_TenantType", OtherDetails._TenantType);
                cmd.Parameters.AddWithValue("@_ServerType", OtherDetails._ServerType);
                cmd.Parameters.AddWithValue("@_EmailSenderID", OtherDetails._EmailSenderID);
                cmd.Parameters.AddWithValue("@_SMSSenderID", OtherDetails._SMSSenderID);
                cmd.Parameters.AddWithValue("@_CRMInterfaceLanguage", OtherDetails._CRMInterfaceLanguage);
                cmd.Parameters.AddWithValue("@_ModifiedBy", OtherDetails._ModifiedBy);
                cmd.Parameters.AddWithValue("@_Createdby", OtherDetails._Createdby);
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
