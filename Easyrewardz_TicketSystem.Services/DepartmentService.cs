using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class DepartmentService: IDepartment
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion

        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public DepartmentService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        #endregion



        /// <summary>
        /// Delete department Brand Mapping 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="DepartmentBrandMappingID"></param>
        /// <returns></returns>
        public int DeleteDepartmentMapping(int tenantID, int DepartmentBrandMappingID)
        {

            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteDepartmentBrandMapping", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Store_ID", tenantID);
                cmd.Parameters.AddWithValue("@tenant_ID", DepartmentBrandMappingID);
                success = Convert.ToInt32(cmd.ExecuteScalar());

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

            return success;
        }

        /// <summary>
        /// Update Department Mapping
        /// </summary>
        /// <returns></returns>
        public int UpdateDepartmentMapping(int TenantID, int DepartmentBrandID,int BrandID,int StoreID,int DepartmentID,int FunctionID,bool Status,int CreatedBy)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateDepartmentMapping", conn);
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@BrandID", DepartmentBrandID);
                cmd.Parameters.AddWithValue("@BrandID",  BrandID);
                cmd.Parameters.AddWithValue("@StoreID",  StoreID);
                cmd.Parameters.AddWithValue("@DepartmentID",  DepartmentID);
                cmd.Parameters.AddWithValue("@FunctionID",  FunctionID);
                cmd.Parameters.AddWithValue("@Status",  Convert.ToInt16(Status));
                cmd.Parameters.AddWithValue("@TenantID",  TenantID);
                cmd.Parameters.AddWithValue("@UserID",  CreatedBy);


                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return result;
        }
    }
}
