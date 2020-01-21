using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public  class CRMRoleService : ICRMRole
    {
        MySqlConnection conn = new MySqlConnection();
        #region Cunstructor

        public CRMRoleService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        #region CustomMethods
        /// <summary>
        /// Create CRM Role
        /// </summary>
        public int InsertCRMRole(int tenantID, string RoleName, bool RoleisActive, int createdBy, string ModulesEnabled, string ModulesDisabled)
        {

            int insertCount = 0;

            try
            {
                conn.Open();
                
                MySqlCommand cmd = new MySqlCommand("SP_InsertCRMRole", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@_tenantID", 1);
                //cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_RoleName", RoleName);
                cmd.Parameters.AddWithValue("@_isRoleActive", Convert.ToInt16(RoleisActive));
                cmd.Parameters.AddWithValue("@_createdBy", 6);
                //cmd.Parameters.AddWithValue("@_createdBy", createdBy);
                cmd.Parameters.AddWithValue("@_ModulesIsEnabled", ModulesEnabled);
                cmd.Parameters.AddWithValue("@_ModulesIsDisabled", ModulesDisabled);

                insertCount = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return insertCount;
        }

        /// <summary>
        /// Update CRM Role
        /// </summary>
        public int UpdateCRMRole(int tenantID, int CRMRoleID, string RoleName, string Modules, bool RoleisActive, int modifiedBy)
        {
            int updatecount = 0; string[] ModuleArr = null;
            int Rolemappingcount = 0; 
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateCRMRole", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@CRMRoles_ID", CRMRoleID);
                //cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@Tenant_ID", 1);
                cmd.Parameters.AddWithValue("@Role_Name", RoleName);
                cmd.Parameters.AddWithValue("@Is_Active", Convert.ToInt16(RoleisActive));
                //cmd.Parameters.AddWithValue("@Modify_By", modifiedBy);
                cmd.Parameters.AddWithValue("@Modify_By", 6);
                cmd.CommandType = CommandType.StoredProcedure;
                updatecount = cmd.ExecuteNonQuery();


                //add crm rolemapping update 

                if (updatecount > 0 && !string.IsNullOrEmpty(Modules))
                {

                    ModuleArr = Modules.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ModuleArr.Length; i++)
                    {
                        string[] tmpArr = null;
                        tmpArr = ModuleArr[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        MySqlCommand cmd1 = new MySqlCommand("SP_UpdateCRMRoleMapping", conn);
                        cmd1.Connection = conn;
                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Parameters.AddWithValue("@_CRmRoleID", CRMRoleID);
                        cmd1.Parameters.AddWithValue("@_ModuleID", Convert.ToInt32(tmpArr[0]));

                        cmd1.Parameters.AddWithValue("@_ModuleisActive", Convert.ToInt16(Convert.ToBoolean(tmpArr[1])));
                        cmd1.Parameters.AddWithValue("@_ModifiedBy", 6);
                        //cmd1.Parameters.AddWithValue("@_ModifiedBy", modifiedBy);
                        Rolemappingcount += Convert.ToInt32(cmd1.ExecuteNonQuery());
                    }
                }

                //**
            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return Rolemappingcount;
        }

        /// <summary>
        /// Delete CRM Role
        /// </summary>
        public int DeleteCRMRole(int tenantID, int CRMRoleID)
        {
            int deletecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteCRMRole", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@CRMRoles_ID", tenantID);
                cmd.Parameters.AddWithValue("@Tenant_ID", CRMRoleID);


                cmd.CommandType = CommandType.StoredProcedure;
                deletecount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return deletecount;

        }

        public List<CRMRoleModel> GetCRMRoleList(int tenantID)
        {
            List<CRMRoleModel> objCRMLst = new List<CRMRoleModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetCRMRolesDetails", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objCRMLst = ds.Tables[0].AsEnumerable().Select(r => new CRMRoleModel()
                        {
                            CRMRoleID = Convert.ToInt32(r.Field<object>("CRMRolesID")),

                            RoleName = r.Field<object>("RoleName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("RoleName")),
                            isRoleActive = r.Field<object>("ModuleIsActive") == System.DBNull.Value ? false:Convert.ToBoolean(Convert.ToInt32(r.Field<object>("ModuleIsActive"))),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("ModifiedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModifiedBy")),
                            ModifiedDate = r.Field<object>("ModifiedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModifiedDate")),
                              }).ToList();
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
                if (ds != null) ds.Dispose(); conn.Close();
            }
            return objCRMLst;
        }

        #endregion
    }
}
