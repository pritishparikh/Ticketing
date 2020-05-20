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
        /// <param name="StoreUserPersonalDetails"></param>
        /// </summary>
        
        /// 
        public int AddStoreUserPersonalDetails(StoreUserPersonalDetails personalDetails)
        {
            int UserID = 0;
            string RandomPassword = string.Empty;
            
            try
            {
                
                RandomPassword = SecurityService.Encrypt(CommonService.GeneratePassword());


                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertStoreUserPersonalDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_Name", personalDetails.UserName);
                cmd.Parameters.AddWithValue("@Mobile_No", personalDetails.MobileNo);
                cmd.Parameters.AddWithValue("@First_Name", string.IsNullOrEmpty(personalDetails.FirstName) ? "": personalDetails.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", string.IsNullOrEmpty(personalDetails.LastName) ? "" : personalDetails.LastName);
                cmd.Parameters.AddWithValue("@Email_ID", personalDetails.EmailID);
                cmd.Parameters.AddWithValue("@Created_By", personalDetails.CreatedBy);
                cmd.Parameters.AddWithValue("@Is_StoreUser", Convert.ToInt16(personalDetails.IsStoreUser));
                cmd.Parameters.AddWithValue("@Tenant_ID", personalDetails.TenantID);
                cmd.Parameters.AddWithValue("@User_ID", personalDetails.UserID);
                cmd.Parameters.AddWithValue("@_SecurePassword", RandomPassword);
                cmd.CommandType = CommandType.StoredProcedure;
                UserID = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception )
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
        public int AddStoreUserMappedCategory(StoreClaimCategory customStoreUser)
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
                cmd.Parameters.AddWithValue("@isClaimApprover", Convert.ToInt16(customStoreUser.isClaimApprover));
                cmd.Parameters.AddWithValue("@Is_Active", Convert.ToInt16(customStoreUser.isActive));
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
        /// Delete User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="Modifyby"></param>
        /// <param name="IsStoreUser"></param>
        public int DeleteStoreUser(int tenantID, int UserId, bool IsStoreUser, int ModifiedBy)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteStoreUserDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@user_ID", UserId);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@Modify_by", ModifiedBy);
                cmd.Parameters.AddWithValue("@Is_StoreUser", Convert.ToInt16(IsStoreUser));
                cmd.CommandType = CommandType.StoredProcedure; 
                success = Convert.ToInt32(cmd.ExecuteScalar());

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
        /// Update Store User
        /// </summary>
        /// <param name="StoreUserListing"></param>
        public int UpdateStoreUser(StoreUserDetailsModel customStoreUserEdit)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateStoreUser", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", customStoreUserEdit.UserID);
                cmd.Parameters.AddWithValue("@Brand_ID", customStoreUserEdit.BrandID);
                cmd.Parameters.AddWithValue("@Store_ID", customStoreUserEdit.StoreID);
                cmd.Parameters.AddWithValue("@Department_ID", customStoreUserEdit.DepartmentID);
                cmd.Parameters.AddWithValue("@Function_IDs", customStoreUserEdit.FunctionIDs);
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
                cmd.Parameters.AddWithValue("@Is_ClaimApprove", Convert.ToInt16(customStoreUserEdit.IsClaimApprove));
                cmd.Parameters.AddWithValue("@Is_Active", Convert.ToInt16(customStoreUserEdit.IsActive));
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
        /// Get  Store User List
        /// </summary>
        /// <param name="tenantID"></param>
        public List<StoreUserListing> GetStoreUserList(int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreUserListing> UsermasterList = new List<StoreUserListing>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreUserList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreUserListing Usermaster = new StoreUserListing();
                        Usermaster.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);

                        Usermaster.BrandID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        Usermaster.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        Usermaster.StoreID = ds.Tables[0].Rows[i]["StoreID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StoreID"]);
                        Usermaster.StoreName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        Usermaster.StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);

                        Usermaster.UserName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        Usermaster.FirstName = ds.Tables[0].Rows[i]["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FirstName"]);
                        Usermaster.LastName = ds.Tables[0].Rows[i]["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LastName"]);
                        Usermaster.MobileNo = ds.Tables[0].Rows[i]["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNo"]);
                        Usermaster.EmailID = ds.Tables[0].Rows[i]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                        Usermaster.RoleID = ds.Tables[0].Rows[i]["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"]);
                        Usermaster.RoleName = ds.Tables[0].Rows[i]["RoleName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RoleName"]);

                        Usermaster.BrandIDs = ds.Tables[0].Rows[i]["BrandIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandIDs"]);
                        Usermaster.MappedBrand = ds.Tables[0].Rows[i]["MappedBrand"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MappedBrand"]);

                        Usermaster.CategoryCount = ds.Tables[0].Rows[i]["CategoryCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryCount"]);
                        Usermaster.CategoryIDs = ds.Tables[0].Rows[i]["CategoryIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryIDs"]);
                        Usermaster.MappedCategory = ds.Tables[0].Rows[i]["MappedCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MappedCategory"]);

                        Usermaster.SubCategoryCount = ds.Tables[0].Rows[i]["SubCategoryCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryCount"]);
                        Usermaster.SubCategoryIDs = ds.Tables[0].Rows[i]["SubCategoryIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryIDs"]);
                        Usermaster.MappedSubCategory = ds.Tables[0].Rows[i]["MappedSubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MappedSubCategory"]);

                        Usermaster.IssueTypeCount = ds.Tables[0].Rows[i]["IssueTypeCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeCount"]);
                        Usermaster.IssueTypeIDs = ds.Tables[0].Rows[i]["IssueTypeIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeIDs"]);
                        Usermaster.MappedIssuetype = ds.Tables[0].Rows[i]["MappedIssuetype"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MappedIssuetype"]);

                        Usermaster.DesignationID = ds.Tables[0].Rows[i]["DesignationID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationID"]);
                        Usermaster.DesignationName = ds.Tables[0].Rows[i]["DesignationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);

                        Usermaster.ReporteeID = ds.Tables[0].Rows[i]["ReporteeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ReporteeID"]);
                        Usermaster.ReporteeName = ds.Tables[0].Rows[i]["ReporteeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReporteeName"]);
                        Usermaster.ReporteeDesignationID = ds.Tables[0].Rows[i]["ReporteeDesignationID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ReporteeDesignationID"]);
                        Usermaster.ReporteeDesignation = ds.Tables[0].Rows[i]["ReporteeDesignation"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReporteeDesignation"]);
                        Usermaster.DepartmentID = ds.Tables[0].Rows[i]["DepartmentID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentID"]);
                        Usermaster.DepartmentName = ds.Tables[0].Rows[i]["DepartmentName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);

                        Usermaster.FunctionIDs = ds.Tables[0].Rows[i]["FunctionIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FunctionIDs"]);
                        Usermaster.MappedFunctions = ds.Tables[0].Rows[i]["MappedFunction"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MappedFunction"]);

                        Usermaster.isActive = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsActive"]);
                        Usermaster.isClaimApprover = ds.Tables[0].Rows[i]["IsClaimApprover"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsClaimApprover"]);
                        Usermaster.CreatedBy = ds.Tables[0].Rows[i]["CreatedName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedName"]);
                        Usermaster.UpdatedBy = ds.Tables[0].Rows[i]["ModifyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyName"]);
                        Usermaster.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        Usermaster.UpdatedDate = ds.Tables[0].Rows[i]["UpdatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UpdatedDate"]);

                        UsermasterList.Add(Usermaster);
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

            return UsermasterList;
        }

        /// <summary>
        /// Get  Store User List on USerID
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        public StoreUserListing GetStoreUserOnUserID(int tenantID, int UserID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            StoreUserListing Usermaster = new StoreUserListing();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreUserListByUserID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("@_UserID", UserID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        Usermaster.UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);

                        Usermaster.BrandID = ds.Tables[0].Rows[0]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["BrandID"]);
                        Usermaster.BrandName = ds.Tables[0].Rows[0]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        Usermaster.StoreID = ds.Tables[0].Rows[0]["StoreID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["StoreID"]);
                        Usermaster.StoreName = ds.Tables[0].Rows[0]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]);
                        Usermaster.StoreCode = ds.Tables[0].Rows[0]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreCode"]);

                        Usermaster.UserName = ds.Tables[0].Rows[0]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                        Usermaster.FirstName = ds.Tables[0].Rows[0]["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                        Usermaster.LastName = ds.Tables[0].Rows[0]["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
                        Usermaster.MobileNo = ds.Tables[0].Rows[0]["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);
                        Usermaster.EmailID = ds.Tables[0].Rows[0]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                        Usermaster.RoleID = ds.Tables[0].Rows[0]["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["RoleID"]);
                        Usermaster.BrandIDs = ds.Tables[0].Rows[0]["BrandIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["BrandIDs"]);
                        Usermaster.MappedBrand = ds.Tables[0].Rows[0]["MappedBrand"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MappedBrand"]);

                        Usermaster.CategoryCount = ds.Tables[0].Rows[0]["CategoryCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryCount"]);
                        Usermaster.CategoryIDs = ds.Tables[0].Rows[0]["CategoryIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CategoryIDs"]);
                        Usermaster.MappedCategory = ds.Tables[0].Rows[0]["MappedCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MappedCategory"]);

                        Usermaster.SubCategoryCount = ds.Tables[0].Rows[0]["SubCategoryCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["SubCategoryCount"]);
                        Usermaster.SubCategoryIDs = ds.Tables[0].Rows[0]["SubCategoryIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["SubCategoryIDs"]);
                        Usermaster.MappedSubCategory = ds.Tables[0].Rows[0]["MappedSubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MappedSubCategory"]);

                        Usermaster.IssueTypeCount = ds.Tables[0].Rows[0]["IssueTypeCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["IssueTypeCount"]);
                        Usermaster.IssueTypeIDs = ds.Tables[0].Rows[0]["IssueTypeIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["IssueTypeIDs"]);
                        Usermaster.MappedIssuetype = ds.Tables[0].Rows[0]["MappedIssuetype"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MappedIssuetype"]);

                        Usermaster.DesignationID = ds.Tables[0].Rows[0]["DesignationID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["DesignationID"]);
                        Usermaster.DesignationName = ds.Tables[0].Rows[0]["DesignationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["DesignationName"]);

                        Usermaster.ReporteeID = ds.Tables[0].Rows[0]["ReporteeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ReporteeID"]);
                        Usermaster.ReporteeName = ds.Tables[0].Rows[0]["ReporteeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ReporteeName"]);
                        Usermaster.ReporteeDesignationID = ds.Tables[0].Rows[0]["ReporteeDesignationID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ReporteeDesignationID"]);
                        Usermaster.ReporteeDesignation = ds.Tables[0].Rows[0]["ReporteeDesignation"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ReporteeDesignation"]);
                        Usermaster.DepartmentID = ds.Tables[0].Rows[0]["DepartmentID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["DepartmentID"]);
                        Usermaster.DepartmentName = ds.Tables[0].Rows[0]["DepartmentName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["DepartmentName"]);

                        Usermaster.FunctionIDs = ds.Tables[0].Rows[0]["FunctionIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["FunctionIDs"]);
                        Usermaster.MappedFunctions = ds.Tables[0].Rows[0]["MappedFunction"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MappedFunction"]);

                        Usermaster.isActive = ds.Tables[0].Rows[0]["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["IsActive"]);
                        Usermaster.isClaimApprover = ds.Tables[0].Rows[0]["IsClaimApprover"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["IsClaimApprover"]);
                        Usermaster.CreatedBy = ds.Tables[0].Rows[0]["CreatedName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CreatedName"]);
                        Usermaster.UpdatedBy = ds.Tables[0].Rows[0]["ModifyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ModifyName"]);
                        Usermaster.CreatedDate = ds.Tables[0].Rows[0]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CreatedDate"]);
                        Usermaster.UpdatedDate = ds.Tables[0].Rows[0]["UpdatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["UpdatedDate"]);

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

            return Usermaster;
        }

        public int AddBrandStore(int tenantID, int brandID, int storeID, int UserMasterID)
        {
            int UserID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertUserBrandStore", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@brand_ID", brandID);
                cmd.Parameters.AddWithValue("@store_ID", storeID);
                cmd.Parameters.AddWithValue("@UserMaster_ID", UserMasterID);
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

        public int UpdateBrandStore(int tenantID, int brandID, int storeID, int userMasterID, int userID)
        {
            int UserID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateUserBrandStore", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@brand_ID", brandID);
                cmd.Parameters.AddWithValue("@store_ID", storeID);
                cmd.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                cmd.Parameters.AddWithValue("@user_ID", userID);
                cmd.CommandType = CommandType.StoredProcedure;
                UserID = cmd.ExecuteNonQuery();
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
        #region Profile Mapping


        /// <summary>
        /// Get  Department List by  brandID and store ID
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public List<StoreUserDepartmentList> BindDepartmentByBrandStore(int BrandID, int storeID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreUserDepartmentList> departmentMasters = new List<StoreUserDepartmentList>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("GetStoreDepartmentByBrandStore", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_brandID", BrandID);
                cmd1.Parameters.AddWithValue("@_storeID", storeID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreUserDepartmentList department = new StoreUserDepartmentList();
                        department.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentID"]);
                        department.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);
                        department.BrandID = Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        department.StoreID = Convert.ToInt32(ds.Tables[0].Rows[i]["StoreID"]);
                        department.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        departmentMasters.Add(department);
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
            return departmentMasters;
        }



        /// <summary>
        /// Get Reportee Designation
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="HierarchyFor"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<DesignationMaster> BindStoreReporteeDesignation(int DesignationID, int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreReporteeDesignation", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                cmd1.Parameters.AddWithValue("@Designation_ID", DesignationID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
               
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DesignationMaster designationMaster = new DesignationMaster();
                        designationMaster.DesignationID = Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationID"]);
                        designationMaster.DesignationName = Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);
                        designationMasters.Add(designationMaster);
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

            return designationMasters;
        }


        /// <summary>
        /// Get Store Report To User
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="IsStoreUser"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<CustomSearchTicketAgent> BindStoreReportToUser(int DesignationID,bool IsStoreUser,  int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomSearchTicketAgent> Users = new List<CustomSearchTicketAgent>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreUserBasedonReportee", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                cmd1.Parameters.AddWithValue("@Designation_ID", DesignationID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@Is_StoreUser", Convert.ToInt16(IsStoreUser));
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomSearchTicketAgent customSearchTicketAgent = new CustomSearchTicketAgent();
                        customSearchTicketAgent.User_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        customSearchTicketAgent.AgentName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        Users.Add(customSearchTicketAgent);
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

            return Users;
        }


        #endregion


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
                cmd.Parameters.AddWithValue("@_subcategoryIds", string.IsNullOrEmpty(SubCategoryIDs) ? "" : SubCategoryIDs.TrimEnd(','));
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
                                IssueTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]),
                                IssueTypeName = Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]),
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

        public List<UpdateUserProfiledetailsModel> GetUserProfileDetails(int UserMasterID, string url)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<UpdateUserProfiledetailsModel> UpdateUserProfiledetailsModel = new List<UpdateUserProfiledetailsModel>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreUserProfileDetails", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@User_ID", UserMasterID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        UpdateUserProfiledetailsModel model = new UpdateUserProfiledetailsModel();
                        model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        model.FirstName = ds.Tables[0].Rows[i]["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FirstName"]);
                        model.LastName = ds.Tables[0].Rows[i]["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LastName"]);
                        model.MobileNo = ds.Tables[0].Rows[i]["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNo"]);
                        model.EmailId = ds.Tables[0].Rows[i]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                        model.DesignationID = Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationID"]);
                        model.DesignationName = ds.Tables[0].Rows[i]["DesignationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);
                        model.ProfilePicture = ds.Tables[0].Rows[i]["ProfilePicture"] == DBNull.Value ? string.Empty : url + "/" + Convert.ToString(ds.Tables[0].Rows[i]["ProfilePicture"]);
                        UpdateUserProfiledetailsModel.Add(model);
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
            return UpdateUserProfiledetailsModel;
        }

        public int UpdateUserProfileDetail(UpdateUserProfiledetailsModel UpdateUserProfiledetailsModel)
        {
            int UserID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateStoreUserProfileDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", UpdateUserProfiledetailsModel.UserId);
                cmd.Parameters.AddWithValue("@First_Name", UpdateUserProfiledetailsModel.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", UpdateUserProfiledetailsModel.LastName);
                cmd.Parameters.AddWithValue("@Mobile_No", UpdateUserProfiledetailsModel.MobileNo);
                cmd.Parameters.AddWithValue("@Email_ID", UpdateUserProfiledetailsModel.EmailId);
                cmd.Parameters.AddWithValue("@Designation_ID", UpdateUserProfiledetailsModel.DesignationID);
                cmd.Parameters.AddWithValue("@Profile_Picture", UpdateUserProfiledetailsModel.ProfilePicture);

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

        #endregion


        public CustomChangePassword GetStoreUserCredentails(int userID, int TenantID, int IsStoreUser)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            CustomChangePassword customChangePassword = new CustomChangePassword();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreUserEmailandPassword", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@User_ID", userID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@Is_StoreUser", IsStoreUser);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        customChangePassword.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        customChangePassword.EmailID = ds.Tables[0].Rows[i]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                        customChangePassword.Password = ds.Tables[0].Rows[i]["SecurePassword"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SecurePassword"]);
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
            return customChangePassword;
        }
        /// <summary>
        /// Delete Profile Picture
        /// <param name="tenantID"></param>
        /// <param name="tenantID"></param>
        /// </summary>
        public int DeleteProfilePicture(int tenantID, int userID)
        {

            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteStoreUserProfile", conn)
                {
                    Connection = conn
                };
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", userID);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
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
    }
}
