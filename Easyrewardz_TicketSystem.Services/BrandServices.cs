using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class BrandServices : IBrand
    {
        MySqlConnection conn = new MySqlConnection();

        public BrandServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Get Brand List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<Brand> GetBrandList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Brand> brands = new List<Brand>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetBrandList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Brand brand = new Brand();
                        brand.BrandID = ds.Tables[0].Rows[i]["BrandID"]== DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        brand.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        brand.BrandCode = ds.Tables[0].Rows[i]["BrandCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandCode"]);
                        brand.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);

                        brands.Add(brand);
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
            }

            return brands;
        }

        /// <summary>
        /// Update Brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int UpdateBrand(Brand brand)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_UpdateBrand", conn);
                cmd1.Parameters.AddWithValue("@Brand_ID", brand.BrandID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", brand.TenantID);
                cmd1.Parameters.AddWithValue("@Brand_Name", brand.BrandName);
                cmd1.Parameters.AddWithValue("@Brand_Code", brand.BrandCode);
                cmd1.Parameters.AddWithValue("@Is_Active", brand.IsActive);
                cmd1.Parameters.AddWithValue("@Modified_By", brand.ModifyBy);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteNonQuery());
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
        /// Delete Brand
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int DeleteBrand(int BrandID, int TenantId)
        {
            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_DeleteBrand", conn);
                cmd1.Parameters.AddWithValue("@Brand_ID", BrandID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;
                k = Convert.ToInt32(cmd1.ExecuteScalar());
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

            return k;
        }

        /// <summary>
        /// Brand List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<Brand> BrandList(int tenantId)
        {
            DataSet ds = new DataSet();
            List<Brand> brands = new List<Brand>();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_BrandList", conn);

                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantId);
                cmd1.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Brand brand = new Brand();
                        brand.BrandID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        brand.TenantID = ds.Tables[0].Rows[i]["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        brand.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        brand.BrandCode = ds.Tables[0].Rows[i]["BrandCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandCode"]);
                        brand.Created_By = ds.Tables[0].Rows[i]["Created_By"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Created_By"]);
                        // brand.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);
                        brand.CreatedDateFormat = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        brand.Modify_By = ds.Tables[0].Rows[i]["Modified_By"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Modified_By"]);
                        // brand.ModifyDate = Convert.ToDateTime(dt.Rows[i]["ModifiedDate"]);
                        brand.ModifyDateFormat = ds.Tables[0].Rows[i]["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]);
                        brand.Status = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                        brands.Add(brand);
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
            }

            return brands;
        }

        /// <summary>
        /// Add Brand
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int AddBrand(Brand brand, int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int success = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_InsertBrand", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.Parameters.AddWithValue("@Brand_Name", brand.BrandName);
                cmd1.Parameters.AddWithValue("@Brand_Code", brand.BrandCode);
                cmd1.Parameters.AddWithValue("@Is_Active", brand.IsActive);
                cmd1.Parameters.AddWithValue("@Created_By", brand.CreatedBy);

                success = Convert.ToInt32(cmd1.ExecuteScalar());
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

    }
}
