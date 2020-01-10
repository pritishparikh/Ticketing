using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class SubCategoryService : ISubCategories
    {
        #region
        MySqlConnection conn = new MySqlConnection();
        public SubCategoryService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Get Sub Category By Category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public List<SubCategory> GetSubCategoryByCategoryID(int CategoryID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SubCategory> objSubCategory = new List<SubCategory>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetListofSubCategoriesByCategoryId", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Category_ID", CategoryID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SubCategory SubCat = new SubCategory();
                        SubCat.SubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        SubCat.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        SubCat.SubCategoryName = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        SubCat.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

                        objSubCategory.Add(SubCat);
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
            return objSubCategory;
        }
    }
}
