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
    public class CategoryServices : ICategory
    {

        /// <summary>
        /// Get Brand list for drop down 
        /// </summary>
        /// <param name="EncptToken"></param>
        /// <returns></returns>


        MySqlConnection conn = new MySqlConnection();
        public CategoryServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        public List<Category> GetCategoryList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Category> categoryList = new List<Category>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetCategoryList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Category category = new Category();
                        category.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        category.CategoryName = Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        category.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);

                        categoryList.Add(category);
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
            return categoryList;
        }


        public int AddCategory(Category category)
        {

            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_InsertCategory", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", category.TenantID);
                cmd1.Parameters.AddWithValue("@Category_Name", category.CategoryName);
                cmd1.Parameters.AddWithValue("@Is_Active", category.IsActive);
                cmd1.Parameters.AddWithValue("@Created_By", category.CreatedBy);


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

        public int DeleteCategory(int CategoryID, int TenantId)
        {
            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_DeleteCategory", conn);
                cmd1.Parameters.AddWithValue("@Category_ID", CategoryID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;
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

        public int UpdateCategory(Category category)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_UpdateCategory", conn);
                cmd1.Parameters.AddWithValue("@Category_ID", category.CategoryID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", category.TenantID);
                cmd1.Parameters.AddWithValue("@Category_Name", category.CategoryName);
                cmd1.Parameters.AddWithValue("@Is_Active", category.IsActive);
                cmd1.Parameters.AddWithValue("@Modified_By", category.ModifyBy);


                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteNonQuery());
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

        public List<Category> CategoryList(int TenantId)
        {


            List<Category> categories = new List<Category>();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_CategoryList", conn);

                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();

                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Category category = new Category();
                        category.CategoryID = Convert.ToInt32(dt.Rows[i]["CategoryID"]);
                        category.TenantID = Convert.ToInt32(dt.Rows[i]["TenantID"]);
                        category.CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]);


                        category.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);

                        category.ModifyDate = Convert.ToDateTime(dt.Rows[i]["ModifiedDate"]);
                        category.Status = Convert.ToString(dt.Rows[i]["Status"]);


                        categories.Add(category);
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

            return categories;
        }
    }
}
