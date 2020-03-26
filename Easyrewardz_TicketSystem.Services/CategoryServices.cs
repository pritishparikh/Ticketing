﻿using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.CustomModel;
using System.Xml;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;

namespace Easyrewardz_TicketSystem.Services
{
    public class CategoryServices : ICategory
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        private readonly IDistributedCache Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        /// <summary>
        /// Get Brand list for drop down 
        /// </summary>
        /// <param name="EncptToken"></param>
        /// <returns></returns>


        MySqlConnection conn = new MySqlConnection();
        public CategoryServices(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            Cache = cache;
        }
        public List<Category> GetCategoryList(int TenantID,int BrandID)
        {

            DataSet ds = new DataSet();
           
            List<Category> categoryList = new List<Category>();

            try
            {
                conn = Db.Connection;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetCategoryListTemp", conn);
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
            catch (Exception ex)
            {

                throw ex;
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
            int success = 0;
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_InsertCategory", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@Category_Name", categoryName);
                cmd1.Parameters.AddWithValue("@Created_By", UserID);
                cmd1.Parameters.AddWithValue("@Brand_ID", BrandID);
                success = Convert.ToInt32(cmd1.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return success;

        }

        public int DeleteCategory(int CategoryID, int TenantId)
        {
            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn = Db.Connection;
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
            
            return k;
        }

        public int UpdateCategory(Category category)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_UpdateCategory", conn);
                cmd1.Parameters.AddWithValue("@Category_ID", category.CategoryID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", category.TenantID);
                cmd1.Parameters.AddWithValue("@Category_Name", category.CategoryName);
                cmd1.Parameters.AddWithValue("@Is_Active", category.IsActive);
                cmd1.Parameters.AddWithValue("@Modified_By", category.ModifyBy);


                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteNonQuery());
                

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            
            return i;
        }

        public List<Category> CategoryList(int TenantId)
        {


            List<Category> categories = new List<Category>();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn = Db.Connection;
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
           
            return categories;
        }
        /// <summary>
        /// Create Category BrandMapping/update /soft delete 
        /// </summary>
        /// <param name="CustomCreateCategory"></param>
        /// <returns></returns>
        public int CreateCategoryBrandMapping(CustomCreateCategory customCreateCategory)
        {
            int success = 0;
            if (customCreateCategory.BrandCategoryMappingID==0)
            {             
                try
                {
                    conn = Db.Connection;
                    MySqlCommand cmd = new MySqlCommand("SP_CreateCategoryBrandMapping", conn);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Braind_ID", customCreateCategory.BraindID);
                    cmd.Parameters.AddWithValue("@Category_ID", customCreateCategory.CategoryID);
                    cmd.Parameters.AddWithValue("@SubCategory_ID", customCreateCategory.SubCategoryID);
                    cmd.Parameters.AddWithValue("@IssueType_ID", customCreateCategory.IssueTypeID);
                    cmd.Parameters.AddWithValue("@Is_Active", customCreateCategory.Status);
                    cmd.Parameters.AddWithValue("@Created_By", customCreateCategory.CreatedBy);
                    success = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
                
            }
            if(customCreateCategory.BrandCategoryMappingID >0)
            {
                try
                {
                    conn = Db.Connection;
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
                    success = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }
            return success;
        }

        public List<CustomCreateCategory> ListCategoryBrandMapping()
        {
            DataSet ds = new DataSet();
            List<CustomCreateCategory> listCategoryMapping = new List<CustomCreateCategory>();
            try
            {
                conn = Db.Connection;
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
                        CreateCategory.BrandCategoryMappingID = Convert.ToInt32(ds.Tables[0].Rows[i]["BrandCategoryMappingID"]);
                        CreateCategory.BraindID = Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        CreateCategory.BrandName = Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        CreateCategory.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        CreateCategory.CategoryName= Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        CreateCategory.SubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        CreateCategory.SubCategoryName = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        CreateCategory.IssueTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        CreateCategory.IssueTypeName = Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        CreateCategory.StatusName = Convert.ToString(ds.Tables[0].Rows[i]["IsActive"]);
                        listCategoryMapping.Add(CreateCategory);
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
           
            return listCategoryMapping;
        }

        public List<Category> GetCategoryListByMultiBrandID(string BrandIDs, int TenantId)
        {
            List<Category> categories = new List<Category>();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn = Db.Connection;
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
            catch (Exception ex)
            {

                throw ex;
            }
            
            return categories;
        }

        public List<string> BulkUploadCategory(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            int insertCount = 0;
            XmlDocument xmlDoc = new XmlDocument();
            DataSet Bulkds = new DataSet();
            List<string> csvLst = new List<string>();
            string succesFile = string.Empty; string ErroFile = string.Empty;
            try
            {
                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {
                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        xmlDoc.LoadXml(DataSetCSV.GetXml());
                        conn = Db.Connection;
                        MySqlCommand cmd = new MySqlCommand("SP_CategoryBulkUpload", conn);
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        //cmd.Parameters.AddWithValue("@_CategoryFor", CategoryFor);
                        //cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                        cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(Bulkds);

                        if (Bulkds != null && Bulkds.Tables[0] != null && Bulkds.Tables[1] != null)
                        {

                            //for success file
                            succesFile = Bulkds.Tables[0].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[0]) : string.Empty;
                            csvLst.Add(succesFile);

                            //for error file
                            ErroFile = Bulkds.Tables[1].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[1]) : string.Empty;
                            csvLst.Add(ErroFile);

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (DataSetCSV != null)
                {
                    DataSetCSV.Dispose();
                }
               
            }
            return csvLst;
        }

        /// <summary>
        /// Create Claim Category
        /// </summary>
        /// <returns></returns>
        public int CreateClaimCategory(ClaimCategory claimCategory)
        {
            int result = 0;
            try
            {
                conn = Db.Connection;
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
            catch (Exception ex)
            {
                throw ex;
            }
            
            return result;
        }
    }
}
