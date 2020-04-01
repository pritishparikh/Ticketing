using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class CategoryServices : ICategory
    {
        
        /// <summary>
        /// Claim Category List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<CustomCreateCategory> ClaimCategoryList(int TenantId)
        {
            DataSet ds = new DataSet();
            List<CustomCreateCategory> listCategoryMapping = new List<CustomCreateCategory>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ClaimCategoryList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomCreateCategory CreateCategory = new CustomCreateCategory
                        {
                            BrandCategoryMappingID = ds.Tables[0].Rows[i]["BrandCategoryMappingID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandCategoryMappingID"]),
                            BraindID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]),
                            BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]),
                            CategoryID = ds.Tables[0].Rows[i]["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]),
                            CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                            SubCategoryID = ds.Tables[0].Rows[i]["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]),
                            SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                            IssueTypeID = ds.Tables[0].Rows[i]["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]),
                            IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]),
                            StatusName = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsActive"])
                        };
                        listCategoryMapping.Add(CreateCategory);
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
            return listCategoryMapping;
        }

     
        /// <summary>
        /// Get Claim Category List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        public List<Category> GetClaimCategoryList(int TenantID, int BrandID)
        {

            DataSet ds = new DataSet();
            List<Category> categoryList = new List<Category>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimCategoryList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd.Parameters.AddWithValue("@Brand_ID", BrandID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0] != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Category category = new Category
                            {
                                CategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]),
                                CategoryName = Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                                IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"])
                            };

                            categoryList.Add(category);
                        }
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
            return categoryList;
        }


        /// <summary>
        /// Add Claim Category
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="BrandID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int AddClaimCategory(string CategoryName, int BrandID, int TenantID, int UserID)
        {
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertClaimCategory", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Category_Name", CategoryName);
                cmd.Parameters.AddWithValue("@Brand_ID", BrandID);
                cmd.Parameters.AddWithValue("@Created_By", UserID);
                
                Success = Convert.ToInt32(cmd.ExecuteScalar());
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

            return Success;

        }



        /// <summary>
        /// Create Category BrandMapping/update /soft delete 
        /// </summary>
        /// <param name="CustomCreateCategory"></param>
        /// <returns></returns>
        public int CreateClaimCategorybrandmapping(CustomCreateCategory customCreateCategory)
        {
            int Success = 0;
            if (customCreateCategory.BrandCategoryMappingID == 0)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_CreateClaimCategoryBrandMapping", conn);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Braind_ID", customCreateCategory.BraindID);
                    cmd.Parameters.AddWithValue("@Category_ID", customCreateCategory.CategoryID);
                    cmd.Parameters.AddWithValue("@SubCategory_ID", customCreateCategory.SubCategoryID);
                    cmd.Parameters.AddWithValue("@IssueType_ID", customCreateCategory.IssueTypeID);
                    cmd.Parameters.AddWithValue("@Is_Active", customCreateCategory.Status);
                    cmd.Parameters.AddWithValue("@Created_By", customCreateCategory.CreatedBy);
                    Success = Convert.ToInt32(cmd.ExecuteScalar());
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
            }
            if (customCreateCategory.BrandCategoryMappingID > 0)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_UpdateCategoryBrandMapping", conn);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BrandCategoryMapping_ID", customCreateCategory.BrandCategoryMappingID);
                    cmd.Parameters.AddWithValue("@Braind_ID", customCreateCategory.BraindID);
                    cmd.Parameters.AddWithValue("@Category_ID", customCreateCategory.CategoryID);
                    cmd.Parameters.AddWithValue("@SubCategory_ID", customCreateCategory.SubCategoryID);
                    cmd.Parameters.AddWithValue("@IssueType_ID", customCreateCategory.IssueTypeID);
                    cmd.Parameters.AddWithValue("@Is_Active", customCreateCategory.Status);
                    cmd.Parameters.AddWithValue("@Created_By", customCreateCategory.CreatedBy);
                    cmd.Parameters.AddWithValue("@Delete_flag", customCreateCategory.Deleteflag);
                    Success = Convert.ToInt32(cmd.ExecuteNonQuery());
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

            }
            return Success;
        }


        /// <summary>
        /// Delete Claim Category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int DeleteClaimCategory(int CategoryID, int TenantId)
        {
            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_DeleteClaimCategory", conn);
                cmd1.Parameters.AddWithValue("@Category_ID", CategoryID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;
                k = Convert.ToInt32(cmd1.ExecuteNonQuery());
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
        
    }
}
