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

        public int AddUserPersonaldetail(UserModel userModel, int TenantID)
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
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
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

        public int AddUserProfiledetail(int DesignationID, int ReportTo, int CreatedBy, int TenantID, int UserID)
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

        public int DeleteUser(int userID, int TenantID, int Modifyby)
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

        public int EditUser(int userID, string DesignationName, int ReportTo, bool status, int TenantID, int Modifyby)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@user_ID", userID);
                cmd.Parameters.AddWithValue("@Designation_Name", DesignationName);
                cmd.Parameters.AddWithValue("@Report_To", ReportTo);
                cmd.Parameters.AddWithValue("@User_status", status);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Modify_by", Modifyby);
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
                cmd.Parameters.AddWithValue("@IsAgent", customUserModel.IsAgent);
                cmd.Parameters.AddWithValue("@Is_Active", customUserModel.IsActive);
                cmd.Parameters.AddWithValue("@Created_By", customUserModel.CreatedBy);
                cmd.Parameters.AddWithValue("@User_ID", customUserModel.UserId);
                cmd.Parameters.AddWithValue("@Tenant_ID", customUserModel.TenantID);
                cmd.Parameters.AddWithValue("@EscalateAssignTo_Id", customUserModel.EscalateAssignToId);
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

        public List<CustomUserList> UserList(int TenantID)
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
