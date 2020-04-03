using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using System;
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
        /// AddStoreUserProfiledetail
        /// </summary>
        /// <param name="customStoreUserModel"></param>
        public int AddStoreUserProfiledetail(CustomStoreUserModel customStoreUserModel)
        {

            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertStoreUserProfileDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Department_Id", customStoreUserModel.DepartmentId);
                cmd.Parameters.AddWithValue("@Function_ID", customStoreUserModel.FunctionID);
                cmd.Parameters.AddWithValue("@Designation_ID", customStoreUserModel.DesignationID);
                cmd.Parameters.AddWithValue("@Reportee_ID", customStoreUserModel.ReporteeID);
                cmd.Parameters.AddWithValue("@Created_By", customStoreUserModel.CreatedBy);
                cmd.Parameters.AddWithValue("@Is_StoreUser", customStoreUserModel.IsStoreUser);
                cmd.Parameters.AddWithValue("@Tenant_ID", customStoreUserModel.TenantID);
                cmd.Parameters.AddWithValue("@User_ID", customStoreUserModel.UserID);
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
    }
}
