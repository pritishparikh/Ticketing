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

        /// <returns></returns>
        /// 
        public List<StoreUserListing> GetStoreUserList(int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreUserListing> UsermasterList = new List<StoreUserListing>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("GetStoreDepartmentByBrandStore", conn);
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

                        Usermaster.UserName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        Usermaster.FirstName = ds.Tables[0].Rows[i]["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FirstName"]);
                        Usermaster.LastName = ds.Tables[0].Rows[i]["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LastName"]);
                        Usermaster.MobileNo = ds.Tables[0].Rows[i]["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNo"]);
                        Usermaster.EmailID = ds.Tables[0].Rows[i]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                        Usermaster.RoleID = ds.Tables[0].Rows[i]["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"]);
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
                        Usermaster.isActive = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsActive"]);
                        Usermaster.CreatedBy = ds.Tables[0].Rows[i]["CreatedName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedName"]);
                        Usermaster.UpdatedBy = ds.Tables[0].Rows[i]["ModifyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyName"]);
                        Usermaster.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        Usermaster.UpdatedDate = ds.Tables[0].Rows[i]["UpdatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UpdatedDate"]);

                        UsermasterList.Add(Usermaster);
                    }
                }

                //if(UsermasterList.Count > 0)
                //{
                //    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                //    {
                //        for (int k = 0; k < UsermasterList.Count; k++)
                //        {
                //            UsermasterList[k].BrandID= ds.Tables[1].AsEnumerable().Where(r => r.Field<object>("UserID").Equals(UsermasterList[k].UserID)).
                //                Select(r => Convert.ToInt32( r.Field<object>("BrandID")))
                            
                //        }


                //    }
                //}

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
