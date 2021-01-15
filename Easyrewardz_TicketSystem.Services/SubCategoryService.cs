using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class SubCategoryService : ISubCategories
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public SubCategoryService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// Get Sub Category By Category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public List<SubCategory> GetSubCategoryByCategoryID(int CategoryID,int TypeId)
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
                cmd1.Parameters.AddWithValue("@Type_Id", TypeId);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
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
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertSubCategory", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Category_ID", CategoryID);
                cmd.Parameters.AddWithValue("@SubCategory_Name", SubCategoryName);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Created_By", UserID);
                subCategoryId = Convert.ToInt32(cmd.ExecuteScalar());
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

            return subCategoryId;
        }

        /// <summary>
        /// Get SubCategory By MultiCategoryID
        /// <param name="CategoryIDs"></param>
        /// </summary>
        public List<SubCategory> GetSubCategoryByMultiCategoryID(string CategoryIDs)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SubCategory> objSubCategory = new List<SubCategory>();

            try
            {
                conn.Open();
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return objSubCategory;
        }

        public List<SubCategory> GetSubCategoryByCategoryOnSearch(int tenantID, int CategoryID, string searchText)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SubCategory> objSubCategory = new List<SubCategory>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetSubCategoryByCategoryIdOnSearch", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd1.Parameters.AddWithValue("@Category_ID", CategoryID);
                cmd1.Parameters.AddWithValue("@search_Text", searchText);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return objSubCategory;
        }
        #endregion
    }
}
