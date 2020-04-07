using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreUserService:IStoreUser
    {
        MySqlConnection conn = new MySqlConnection();
        public StoreUserService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// AddStoreUserPersonaldetail
        /// <param name="CustomStoreUserModel"></param>
        /// </summary>
        /// <param name="CustomStoreUserModel"></param>
        public int AddStoreUserPersonaldetail(CustomStoreUserModel storeUserModel)
        {
            int UserID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InserStoreUserPersonalDetail", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_Name", storeUserModel.UserName);
                cmd.Parameters.AddWithValue("@Mobile_No", storeUserModel.MobileNo);
                cmd.Parameters.AddWithValue("@First_Name", storeUserModel.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", storeUserModel.LastName);
                cmd.Parameters.AddWithValue("@Email_ID", storeUserModel.EmailID);
                cmd.Parameters.AddWithValue("@Created_By", storeUserModel.CreatedBy);
                //cmd.Parameters.AddWithValue("@Is_StoreUser", storeUserModel.IsStoreUser);
                cmd.Parameters.AddWithValue("@Tenant_ID", storeUserModel.TenantID);
                cmd.Parameters.AddWithValue("@Brand_IDs", storeUserModel.BrandIDs);
                cmd.Parameters.AddWithValue("@Store_IDs", storeUserModel.StoreIDs);
                cmd.CommandType = CommandType.StoredProcedure;
                UserID = Convert.ToInt32(cmd.ExecuteScalar());

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

            return UserID;
        }



        /// <summary>
        /// AddStoreUserPersonaldetail
        /// <param name="CustomStoreUserModel"></param>
        /// </summary>
        /// <param name="CustomStoreUserModel"></param>
        /// 
         public int AddStoreUserPersonalDetail(StoreUserPersonalDetails personalDetails)
        {
            int UserID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InserStoreUserPersonalDetail", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_Name", personalDetails.UserName);
                cmd.Parameters.AddWithValue("@Mobile_No", personalDetails.MobileNo);
                cmd.Parameters.AddWithValue("@First_Name", string.IsNullOrEmpty(personalDetails.FirstName) ? "": personalDetails.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", string.IsNullOrEmpty(personalDetails.LastName) ? "" : personalDetails.LastName);
                cmd.Parameters.AddWithValue("@Email_ID", personalDetails.EmailID);
                cmd.Parameters.AddWithValue("@Created_By", personalDetails.CreatedBy);
                cmd.Parameters.AddWithValue("@Is_StoreUser", Convert.ToInt16(personalDetails.IsStoreUser));
                cmd.Parameters.AddWithValue("@Tenant_ID", personalDetails.TenantID);

                cmd.CommandType = CommandType.StoredProcedure;
                UserID = Convert.ToInt32(cmd.ExecuteScalar());

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

            return UserID;
        }

        ///// <summary>
        ///// AddStoreUserProfiledetail
        ///// </summary>
        ///// <param name="customStoreUserModel"></param>
        //public int AddStoreUserProfiledetail(CustomStoreUserModel customStoreUserModel)
        //{

        //    int Success = 0;
        //    try
        //    {
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("SP_InsertStoreUserProfileDetails", conn);
        //        cmd.Connection = conn;
        //        cmd.Parameters.AddWithValue("@Department_Id", customStoreUserModel.DepartmentId);
        //        cmd.Parameters.AddWithValue("@Function_ID", customStoreUserModel.FunctionID);
        //        cmd.Parameters.AddWithValue("@Designation_ID", customStoreUserModel.DesignationID);
        //        cmd.Parameters.AddWithValue("@Reportee_ID", customStoreUserModel.ReporteeID);
        //        cmd.Parameters.AddWithValue("@Created_By", customStoreUserModel.CreatedBy);
        //        cmd.Parameters.AddWithValue("@Is_StoreUser", customStoreUserModel.IsStoreUser);
        //        cmd.Parameters.AddWithValue("@Tenant_ID", customStoreUserModel.TenantID);
        //        cmd.Parameters.AddWithValue("@User_ID", customStoreUserModel.UserID);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        Success = Convert.ToInt32(cmd.ExecuteNonQuery());

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (conn != null)
        //        {
        //            conn.Close();
        //        }
        //    }

        //    return Success;
        //}


        /// <summary>
        /// Insert User Profile Details
        /// </summary>
        /// <param name="BrandID"></param>
        ///  <param name="storeID"></param>
        ///   <param name="departmentId"></param>
        ///    <param name="functionIDs"></param>
        ///     <param name="designationID"></param>
        ///     <param name="reporteeID"></param>
        ///      <param name="CreatedBy"></param>
        public int AddStoreUserProfileDetails( int tenantID,int userID,int brandID, int storeID, int departmentId, string functionIDs, int designationID, int reporteeID, int CreatedBy)
        {
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertStoreUserProfileDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_userID", userID);
                cmd.Parameters.AddWithValue("@_BrandID", brandID);
                cmd.Parameters.AddWithValue("@_storeID", storeID);
                cmd.Parameters.AddWithValue("@_departmentId", departmentId);
                cmd.Parameters.AddWithValue("@_functionIDs", string.IsNullOrEmpty(functionIDs) ? "" : functionIDs);
                cmd.Parameters.AddWithValue("@_designationID", designationID);
                cmd.Parameters.AddWithValue("@_reporteeID", reporteeID);
                cmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);
                cmd.CommandType = CommandType.StoredProcedure;
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

            return Success;
        }

        /// <summary>
        /// Edit Store User
        /// </summary>
        /// <param name="CustomStoreUserEdit"></param>
        public int EditStoreUser(CustomStoreUserEdit customStoreUserEdit)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateStoreUser", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", customStoreUserEdit.UserID);
                cmd.Parameters.AddWithValue("@StoreBrand_IDs", customStoreUserEdit.StoreBrandIDs);
                cmd.Parameters.AddWithValue("@Store_IDs", customStoreUserEdit.StoreIDs);
                cmd.Parameters.AddWithValue("@Department_ID", customStoreUserEdit.DepartmentID);
                cmd.Parameters.AddWithValue("@Function_ID", customStoreUserEdit.FunctionID);
                cmd.Parameters.AddWithValue("@Designation_ID", customStoreUserEdit.DesignationID);
                cmd.Parameters.AddWithValue("@Reportee_ID", customStoreUserEdit.ReporteeID);
                cmd.Parameters.AddWithValue("@User_Name", customStoreUserEdit.UserName);
                cmd.Parameters.AddWithValue("@Email_ID", customStoreUserEdit.EmailID);
                cmd.Parameters.AddWithValue("@Mobile_No", customStoreUserEdit.MobileNo);
                cmd.Parameters.AddWithValue("@First_Name", customStoreUserEdit.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", customStoreUserEdit.LastName);
                cmd.Parameters.AddWithValue("@Brand_Ids", customStoreUserEdit.BrandIds);
                cmd.Parameters.AddWithValue("@Category_Ids", customStoreUserEdit.CategoryIds);
                cmd.Parameters.AddWithValue("@SubCategory_Ids", customStoreUserEdit.SubCategoryIds);
                cmd.Parameters.AddWithValue("@Issuetype_Ids", customStoreUserEdit.IssuetypeIds);
                cmd.Parameters.AddWithValue("@CRMRole_ID", customStoreUserEdit.CRMRoleID);
                cmd.Parameters.AddWithValue("@Is_ClaimApprove", customStoreUserEdit.IsClaimApprove);
                cmd.Parameters.AddWithValue("@Is_Active", customStoreUserEdit.IsActive);
                cmd.Parameters.AddWithValue("@Created_By", customStoreUserEdit.CreatedBy);
                cmd.Parameters.AddWithValue("@Tenant_ID", customStoreUserEdit.TenantID);
                cmd.Parameters.AddWithValue("@Is_StoreUser", customStoreUserEdit.IsStoreUser);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return success;
        }
        /// <summary>
        /// Store User Mapped Category
        /// </summary>
        /// <param name="customStoreUser"></param>
        public int StoreUserMappedCategory(CustomStoreUser customStoreUser)
        {

            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertStoreUserMappedCategory", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Brand_IDs", customStoreUser.BrandIDs);
                cmd.Parameters.AddWithValue("@Category_Ids", customStoreUser.CategoryIds);
                cmd.Parameters.AddWithValue("@SubCategory_Ids", customStoreUser.SubCategoryIds);
                cmd.Parameters.AddWithValue("@Issuetype_Ids", customStoreUser.IssuetypeIds);
                cmd.Parameters.AddWithValue("@Created_By", customStoreUser.CreatedBy);
                cmd.Parameters.AddWithValue("@Is_StoreUser", customStoreUser.IsStoreUser);
                cmd.Parameters.AddWithValue("@Tenant_ID", customStoreUser.TenantID);
                cmd.Parameters.AddWithValue("@User_ID", customStoreUser.UserID);
                cmd.Parameters.AddWithValue("@CRMRole_ID", customStoreUser.CRMRoleID);
                cmd.Parameters.AddWithValue("@ClaimApprover_ID", customStoreUser.ClaimApproverID);
                cmd.Parameters.AddWithValue("@Is_Active", customStoreUser.Status);
                cmd.CommandType = CommandType.StoredProcedure;
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

            return Success;
        }



        #region Claim CategoryMapping

        /// <summary>
        /// Get Claim Category List by muliptle brandID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        public List<StoreClaimCategoryModel> GetClaimCategoryListByBrandID(int TenantID, string BrandIDs)
        {

            DataSet ds = new DataSet();
            List<StoreClaimCategoryModel> categoryList = new List<StoreClaimCategoryModel>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimCategoryListByMultiBrandID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                cmd.Parameters.AddWithValue("@_brandIds", string.IsNullOrEmpty(BrandIDs) ? "":BrandIDs);
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
                            StoreClaimCategoryModel category = new StoreClaimCategoryModel
                            {
                                CategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]),
                                BrandID = Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]),
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
                if (ds != null)
                    ds.Dispose();
            }
            return categoryList;

        }


        /// <summary>
        /// Get Claim sub Category List by muliptle CategoryID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        public List<StoreClaimSubCategoryModel> GetClaimSubCategoryByCategoryID(int TenantID, string CategoryIDs)
        {

            DataSet ds = new DataSet();
            List<StoreClaimSubCategoryModel> subcategoryList = new List<StoreClaimSubCategoryModel>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimSubCategoriesByMultiCategoryId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                cmd.Parameters.AddWithValue("@_categoryIds", string.IsNullOrEmpty(CategoryIDs) ? "" : CategoryIDs);
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
                            StoreClaimSubCategoryModel category = new StoreClaimSubCategoryModel
                            {
                                SubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]),
                                CategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]),
                                SubCategoryName = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                                IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"])
                            };

                            subcategoryList.Add(category);
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
                if (ds != null)
                    ds.Dispose();
            }
            return subcategoryList;

        }


        /// <summary>
        /// Get Claim Issue Type List by multiple subcat Id
        /// </summary>
        /// <param name="TenantID">Tenant Id</param>
        /// <param name="SubCategoryID">SubCategory ID</param>
        /// <returns></returns>
        public List<StoreClaimIssueTypeModel> GetClaimIssueTypeListBySubCategoryID(int TenantID, string SubCategoryIDs)
        {
            DataSet ds = new DataSet();
            List<StoreClaimIssueTypeModel> issueTypeList = new List<StoreClaimIssueTypeModel>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimIssueTypeListByMultiSubCategoryId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                cmd.Parameters.AddWithValue("@_subcategoryIds", string.IsNullOrEmpty(SubCategoryIDs) ? "" : SubCategoryIDs);
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
                            StoreClaimIssueTypeModel issuetype = new StoreClaimIssueTypeModel
                            {
                                SubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]),
                                IssueTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]),
                                IssueTypeName = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                                IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"])
                            };

                            issueTypeList.Add(issuetype);
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
                if (ds != null)
                    ds.Dispose();
            }
            return issueTypeList;
        }


        #endregion
    }
}
