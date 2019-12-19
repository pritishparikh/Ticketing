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
                cmd1.Parameters.AddWithValue("@TenantID", TenantID);
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
            return categoryList;
        }

    }
}
