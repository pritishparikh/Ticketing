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

namespace Easyrewardz_TicketSystem.Services
{
    public class UserServices:IUser
    {
        MySqlConnection conn = new MySqlConnection();

        public UserServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public int AddUserPersonaldetail(UserModel userModel)
        {
            int UserID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertUserPeronalDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_Name", userModel.UserName);
                cmd.Parameters.AddWithValue("@Mobile_No", userModel.MobileNo);
                cmd.Parameters.AddWithValue("@First_Name", userModel.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", userModel.LastName);
                cmd.Parameters.AddWithValue("@Email_ID", userModel.EmailID);
                cmd.Parameters.AddWithValue("@Created_By", userModel.CreatedBy);
                cmd.Parameters.AddWithValue("@Is_StoreUser", userModel.IsStoreUser);
                cmd.Parameters.AddWithValue("@Tenant_ID", userModel.TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                UserID = Convert.ToInt32(cmd.ExecuteScalar());

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

            return UserID;
        }

        public int AddUserProfiledetail(int DesignationID, int ReportTo, int CreatedBy, int TenantID, int UserID,int IsStoreUser)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertUserProfileDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Designation_ID", DesignationID);
                cmd.Parameters.AddWithValue("@Reportee_ID", ReportTo);
                cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Is_StoreUser", IsStoreUser);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return success;
        }

        public int DeleteUser(int userID, int TenantID, int Modifyby,int IsStoreUser)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteUserData", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@user_ID", userID);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Modify_by", Modifyby);
                cmd.Parameters.AddWithValue("@Is_StoreUser", IsStoreUser);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return success;
        }

        public int EditUser(CustomEditUserModel customEditUserModel)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateUser", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@user_ID", customEditUserModel.UserID);
                cmd.Parameters.AddWithValue("@Designation_ID", customEditUserModel.DesignationID);
                cmd.Parameters.AddWithValue("@Reportee_ID", customEditUserModel.ReporteeID);
                cmd.Parameters.AddWithValue("@User_Name", customEditUserModel.UserName);
                cmd.Parameters.AddWithValue("@Email_ID", customEditUserModel.EmailID);
                cmd.Parameters.AddWithValue("@Mobile_No", customEditUserModel.MobileNo);
                cmd.Parameters.AddWithValue("@First_Name", customEditUserModel.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", customEditUserModel.LastName);
                cmd.Parameters.AddWithValue("@Brand_Ids", customEditUserModel.BrandIds);
                cmd.Parameters.AddWithValue("@category_Ids", customEditUserModel.categoryIds);
                cmd.Parameters.AddWithValue("@subCategory_Ids", customEditUserModel.subCategoryIds);
                cmd.Parameters.AddWithValue("@Issuetype_Ids", customEditUserModel.IssuetypeIds);
                cmd.Parameters.AddWithValue("@Role_ID", customEditUserModel.RoleID);
                cmd.Parameters.AddWithValue("@Is_CopyEscalation", customEditUserModel.IsCopyEscalation);
                cmd.Parameters.AddWithValue("@Is_AssignEscalation", customEditUserModel.IsAssignEscalation);
                cmd.Parameters.AddWithValue("@Is_Agent", customEditUserModel.IsAgent);
                cmd.Parameters.AddWithValue("@EscalateAssignTo_Id", customEditUserModel.EscalateAssignToId);
                cmd.Parameters.AddWithValue("@Is_Active", customEditUserModel.IsActive);
                cmd.Parameters.AddWithValue("@Created_By", customEditUserModel.CreatedBy);
                cmd.Parameters.AddWithValue("@Tenant_ID", customEditUserModel.TenantID);
                cmd.Parameters.AddWithValue("@Is_StoreUser", customEditUserModel.IsStoreUser);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return success;
        }

        public CustomUserList GetuserDetailsById(int UserID, int TenantID,int IsStoreUser)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            //CustomEditUserModel user = new CustomEditUserModel();
            CustomUserList customUserList = new CustomUserList();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetUserDetailById", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
                cmd1.Parameters.AddWithValue("@Is_StoreUser", IsStoreUser);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        customUserList.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        customUserList.RoleID= Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"]);
                        customUserList.DesignationID= Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationID"]);
                        customUserList.ReporteeID= Convert.ToInt32(ds.Tables[0].Rows[i]["ReporteeID"]);
                        customUserList.UserName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        customUserList.MobileNumber = ds.Tables[0].Rows[i]["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNo"]);
                        customUserList.EmailID = ds.Tables[0].Rows[i]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                        customUserList.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        customUserList.Is_CopyEscalation = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsCopyEscalation"]);
                        customUserList.Is_AssignEscalation = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsAssignEscalation"]);
                        customUserList.FirstName = ds.Tables[0].Rows[i]["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FirstName"]);
                        customUserList.LastName = ds.Tables[0].Rows[i]["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LastName"]);
                        customUserList.BrandIDs = ds.Tables[0].Rows[i]["BrandIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandIDs"]);
                        customUserList.BrandNames = ds.Tables[0].Rows[i]["BrandNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandNames"]);
                        customUserList.Brand_Names = ds.Tables[0].Rows[i]["Brand_Names"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Brand_Names"]);
                        customUserList.CategoryIDs = ds.Tables[0].Rows[i]["CategoryIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryIDs"]);
                        customUserList.CategoryNames = ds.Tables[0].Rows[i]["CategoryNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryNames"]);
                        customUserList.Category_Name = ds.Tables[0].Rows[i]["Category_Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Category_Name"]);
                        customUserList.SubCategoryIDs = ds.Tables[0].Rows[i]["SubCategoryIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryIDs"]);
                        customUserList.SubCategoryNames = ds.Tables[0].Rows[i]["SubCategoryNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryNames"]);
                        customUserList.SubCategory_Name = ds.Tables[0].Rows[i]["SubCategory_Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategory_Name"]);
                        customUserList.IssueTypeIDs = ds.Tables[0].Rows[i]["IssueTypeIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeIDs"]);
                        customUserList.IssueTypeNames = ds.Tables[0].Rows[i]["IssueTypeNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeNames"]);
                        customUserList.IssueType_Name = ds.Tables[0].Rows[i]["IssueType_Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueType_Name"]);
                        customUserList.AssignID = ds.Tables[0].Rows[i]["AssignID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AssignID"]);
                        customUserList.AssignEscalation= ds.Tables[0].Rows[i]["AssignEscalation"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignEscalation"]);
                        customUserList.AssignName= ds.Tables[0].Rows[i]["AssignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignName"]);
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

            return customUserList;
        }


        public List<User> GetUserList(int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<User> users = new List<User>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetUserFullName", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        User user = new User();
                        user.UserID= Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        user.FullName = Convert.ToString(ds.Tables[0].Rows[i]["FullName"]);
                        user.ReporteeID = Convert.ToInt32(ds.Tables[0].Rows[i]["ReporteeID"]);
                        user.RoleID= Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"]);
                        user.RoleName = Convert.ToString(ds.Tables[0].Rows[i]["RoleName"]);



                        users.Add(user);
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

            return users;
        }

        public int Mappedcategory(CustomUserModel customUserModel)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertMappedUsercategory", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Brand_Ids", customUserModel.BrandIds);
                cmd.Parameters.AddWithValue("@Category_Ids", customUserModel.categoryIds);
                cmd.Parameters.AddWithValue("@SubCategory_Ids", customUserModel.subCategoryIds);
                cmd.Parameters.AddWithValue("@Issuetype_Ids", customUserModel.IssuetypeIds);
                cmd.Parameters.AddWithValue("@Role_ID", customUserModel.RoleID);
                cmd.Parameters.AddWithValue("@Is_CopyEscalation", customUserModel.IsCopyEscalation);
                cmd.Parameters.AddWithValue("@Is_AssignEscalation", customUserModel.IsAssignEscalation);
                cmd.Parameters.AddWithValue("@Is_Agent", customUserModel.IsAgent);
                cmd.Parameters.AddWithValue("@Is_Active", customUserModel.IsActive);
                cmd.Parameters.AddWithValue("@Created_By", customUserModel.CreatedBy);
                cmd.Parameters.AddWithValue("@User_ID", customUserModel.UserId);
                cmd.Parameters.AddWithValue("@Tenant_ID", customUserModel.TenantID);
                cmd.Parameters.AddWithValue("@EscalateAssignTo_Id", customUserModel.EscalateAssignToId);
                cmd.Parameters.AddWithValue("@Is_StoreUser", customUserModel.IsStoreUser);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return success;
        }

        public List<CustomUserList> UserList(int TenantID,int IsStoreUser)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomUserList> users = new List<CustomUserList>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetUserListData", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@Is_StoreUser", IsStoreUser);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomUserList user = new CustomUserList();
                        user.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        user.UserName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        user.MobileNumber = ds.Tables[0].Rows[i]["MobileNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNo"]);
                        user.EmailID = ds.Tables[0].Rows[i]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                        user.Designation = ds.Tables[0].Rows[i]["DesignationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);
                        user.UpdatedBy = ds.Tables[0].Rows[i]["UpdatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UpdatedBy"]);
                        user.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]);
                        user.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        user.UpdatedDate = ds.Tables[0].Rows[i]["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]);
                        user.IsCopyEscalation= ds.Tables[0].Rows[i]["IsCopyEscalation"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsCopyEscalation"]);
                        //user.BrandIDs = ds.Tables[0].Rows[i]["BrandIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandIDs"]);
                        user.BrandNames= ds.Tables[0].Rows[i]["BrandNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandNames"]);
                        user.Brand_Names= ds.Tables[0].Rows[i]["Brand_Names"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Brand_Names"]);
                        //user.CategoryIDs = ds.Tables[0].Rows[i]["CategoryIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryIDs"]);
                        user.CategoryNames = ds.Tables[0].Rows[i]["CategoryNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryNames"]);
                        user.Category_Name = ds.Tables[0].Rows[i]["Category_Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Category_Name"]);
                       // user.SubCategoryIDs = ds.Tables[0].Rows[i]["SubCategoryIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryIDs"]);
                        user.SubCategoryNames = ds.Tables[0].Rows[i]["SubCategoryNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryNames"]);
                        user.SubCategory_Name = ds.Tables[0].Rows[i]["SubCategory_Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategory_Name"]);
                       // user.IssueTypeIDs = ds.Tables[0].Rows[i]["IssueTypeIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeIDs"]);
                        user.IssueTypeNames = ds.Tables[0].Rows[i]["IssueTypeNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeNames"]);
                        user.IssueType_Name = ds.Tables[0].Rows[i]["IssueType_Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueType_Name"]);
                        user.CrmRoleName= ds.Tables[0].Rows[i]["RoleName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RoleName"]);
                        user.ReportTo= ds.Tables[0].Rows[i]["ReportTo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReportTo"]);
                        user.AssignEscalation= ds.Tables[0].Rows[i]["AssignEscalation"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignEscalation"]);
                        user.AssignName = ds.Tables[0].Rows[i]["AssignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignName"]);
                        users.Add(user);
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

            return users;
        }
    }
}
