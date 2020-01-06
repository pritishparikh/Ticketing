using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using Easyrewardz_TicketSystem.DBContext;

namespace Easyrewardz_TicketSystem.Services
{
    public class BrandServices : IBrand
    {
        MySqlConnection conn = new MySqlConnection();

        public BrandServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

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
                        brand.BrandID = Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        brand.BrandName = Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        brand.BrandCode = Convert.ToString(ds.Tables[0].Rows[i]["BrandCode"]);
                        brand.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);

                        brands.Add(brand);
                    }
                }
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

            return brands;
        }

        public int AddBrand(Brand brand, int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
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
    }
}
