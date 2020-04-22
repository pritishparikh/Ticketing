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
    public class CategoryServices : ICategory
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion

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

        /// <summary>
        /// Get Category List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        public List<Category> GetCategoryList(int TenantID,int BrandID)
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
                cmd1.Parameters.AddWithValue("@Brand_ID", BrandID);
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
        /// AddCategory
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int AddCategory(string categoryName, int TenantID, int UserID, int BrandID)
        {

            MySqlCommand cmd = new MySqlCommand();
            int Success = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_InsertCategory", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@Category_Name", categoryName);
                cmd1.Parameters.AddWithValue("@Created_By", UserID);
                cmd1.Parameters.AddWithValue("@Brand_ID", BrandID);
                Success = Convert.ToInt32(cmd1.ExecuteScalar());
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
        /// Delete Category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
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
        /// Update Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
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
        /// Category List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
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

            return categories;
        }

        /// <summary>
        /// Create Category BrandMapping/update /soft delete 
        /// </summary>
        /// <param name="CustomCreateCategory"></param>
        /// <returns></returns>
        public int CreateCategoryBrandMapping(CustomCreateCategory customCreateCategory)
        {
            int Success = 0;
            if (customCreateCategory.BrandCategoryMappingID==0)
            {             
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_CreateCategoryBrandMapping", conn);
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
            if(customCreateCategory.BrandCategoryMappingID >0)
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
        /// List Category Brand Mapping
        /// </summary>
        /// <returns></returns>
        public List<CustomCreateCategory> ListCategoryBrandMapping()
        {
            DataSet ds = new DataSet();
            List<CustomCreateCategory> listCategoryMapping = new List<CustomCreateCategory>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ListCategoryBrandMapping", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomCreateCategory CreateCategory = new CustomCreateCategory();
                        CreateCategory.BrandCategoryMappingID = ds.Tables[0].Rows[i]["BrandCategoryMappingID"] == DBNull.Value ? 0: Convert.ToInt32(ds.Tables[0].Rows[i]["BrandCategoryMappingID"]);
                        CreateCategory.BraindID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        CreateCategory.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        CreateCategory.CategoryID = ds.Tables[0].Rows[i]["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        CreateCategory.CategoryName= ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        CreateCategory.SubCategoryID = ds.Tables[0].Rows[i]["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        CreateCategory.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        CreateCategory.IssueTypeID = ds.Tables[0].Rows[i]["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        CreateCategory.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        CreateCategory.StatusName = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsActive"]);
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
        /// Get Category List By Multi BrandID
        /// </summary>
        /// <param name="BrandIDs"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<Category> GetCategoryListByMultiBrandID(string BrandIDs, int TenantId)
        {
            List<Category> categories = new List<Category>();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetCategoryListByMultiBrandID", conn);
                cmd1.Parameters.AddWithValue("@Brand_IDs", BrandIDs);
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
                        category.CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]);
                        categories.Add(category);
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

            return categories;
        }

        /// <summary>
        /// Bulk Upload Category
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public List<string> BulkUploadCategory(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            XmlDocument xmlDoc = new XmlDocument();
            DataSet Bulkds = new DataSet();
            List<string> csvLst = new List<string>();
            string SuccessFile = string.Empty; string ErrorFile = string.Empty;
            try
            {
                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {
                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        xmlDoc.LoadXml(DataSetCSV.GetXml());
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SP_CategoryBulkUpload", conn);
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                      
                        cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(Bulkds);

                        if (Bulkds != null && Bulkds.Tables[0] != null && Bulkds.Tables[1] != null)
                        {

                            //for success file
                            SuccessFile = Bulkds.Tables[0].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[0]) : string.Empty;
                            csvLst.Add(SuccessFile);

                            //for error file
                            ErrorFile = Bulkds.Tables[1].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[1]) : string.Empty;
                            csvLst.Add(ErrorFile);
                           

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
                if (DataSetCSV != null)
                {
                    DataSetCSV.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return csvLst;
        }

        /// <summary>
        /// Create Claim Category
        /// </summary>
        /// <param name="claimCategory"></param>
        /// <returns></returns>
        public int CreateClaimCategory(ClaimCategory claimCategory)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_CreateDepartment", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@BrandID", claimCategory.BrandName);
                cmd.Parameters.AddWithValue("@StoreID", claimCategory.ClaimCategoryName);
                cmd.Parameters.AddWithValue("@DepartmentID", claimCategory.ClaimSubCategory);
                cmd.Parameters.AddWithValue("@FunctionID", claimCategory.ClaimIssueType);
                cmd.Parameters.AddWithValue("@Status", claimCategory.Status);
                cmd.Parameters.AddWithValue("@User_ID", claimCategory.CreatedBy);
                cmd.Parameters.AddWithValue("@Tenant_ID", claimCategory.TenantID);


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

        public List<Category> GetCategoryOnSearch(int TenantID, int brandID, string searchText)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Category> categoryList = new List<Category>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetCategoryOnSearch", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Brand_ID", brandID);
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);              
                cmd1.Parameters.AddWithValue("@search_Text", searchText);
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
    }
}
