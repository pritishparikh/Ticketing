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
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;

namespace Easyrewardz_TicketSystem.Services
{
    public class BrandServices : IBrand
    {
        #region variable
        private readonly IDistributedCache _Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        MySqlConnection conn = new MySqlConnection();
        public BrandServices(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            _Cache = cache;
        }
       
        public List<Brand> GetBrandList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Brand> brands = new List<Brand>();

            try
            {
                conn= Db.Connection;
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
            return brands;
        }

        public int UpdateBrand(Brand brand)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn = Db.Connection;
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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            
            return i;
        }

        public int DeleteBrand(int BrandID, int TenantId)
        {
            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_DeleteBrand", conn);
                cmd1.Parameters.AddWithValue("@Brand_ID", BrandID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;
                k = Convert.ToInt32(cmd1.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return k;
        }

        public List<Brand> BrandList(int TenantId)
        {
            List<Brand> brands = new List<Brand>();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_BrandList", conn);

                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                 
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Brand brand = new Brand();
                        brand.BrandID = Convert.ToInt32(dt.Rows[i]["BrandID"]);
                        brand.TenantID = Convert.ToInt32(dt.Rows[i]["TenantID"]);
                        brand.BrandName = Convert.ToString(dt.Rows[i]["BrandName"]);
                        brand.BrandCode = Convert.ToString(dt.Rows[i]["BrandCode"]);
                        brand.Created_By = Convert.ToString(dt.Rows[i]["Created_By"]);
                        brand.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);
                        brand.CreatedDateFormat = brand.CreatedDate.ToString("dd/MMM/yyyy");
                        brand.Modify_By = Convert.ToString(dt.Rows[i]["Modified_By"]);
                        brand.ModifyDate = Convert.ToDateTime(dt.Rows[i]["ModifiedDate"]);
                        brand.ModifyDateFormat = brand.ModifyDate.ToString("dd/MMM/yyyy");
                        brand.Status = Convert.ToString(dt.Rows[i]["Status"]);


                        brands.Add(brand);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return brands;
        }

        public int AddBrand(Brand brand, int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn = Db.Connection;
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
           
            return k;

        }

    }
}
