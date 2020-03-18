using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class SubCategoryService : ISubCategories
    {
        #region variable
        private readonly IDistributedCache _Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public SubCategoryService(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            _Cache = cache;
        }
        #endregion

        #region Custom Methods

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
                conn = Db.Connection;
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
            
            return objSubCategory;
        }

        /// <summary>
        /// Add Sub Category
        /// </summary>
        /// <param name="CategoryID">ID of the category </param>
        /// <param name="SubCategoryName">Name of the Sub-category</param>
        /// <param name="TenantID">Id of the Tenant</param>
        /// <param name="UserID">Id of the User</param>
        /// <returns></returns>
        public int AddSubCategory(int CategoryID, string SubCategoryName, int TenantID, int UserID)
        {
            int subCategoryId = 0;
            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_InsertSubCategory", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Category_ID", CategoryID);
                cmd.Parameters.AddWithValue("@SubCategory_Name", SubCategoryName);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Created_By", UserID);
                subCategoryId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return subCategoryId;
        }

        public List<SubCategory> GetSubCategoryByMultiCategoryID(string CategoryIDs)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SubCategory> objSubCategory = new List<SubCategory>();

            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetSubCategoryListByMultiCatagoryID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@CategoryIDs", CategoryIDs);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SubCategory SubCat = new SubCategory();
                        SubCat.SubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        SubCat.SubCategoryName = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        objSubCategory.Add(SubCat);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return objSubCategory;
        }
        #endregion
    }
}
