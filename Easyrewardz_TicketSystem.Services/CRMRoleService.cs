using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public  class CRMRoleService : ICRMRole
    {

        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion
        MySqlConnection conn = new MySqlConnection();
        #region Cunstructor

        public CRMRoleService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        #region CustomMethods
        /// <summary>
        /// Insert & Update CRMRole
        /// </summary>
        /// <param name="CRMRoleID"></param>
        /// <param name="tenantID"></param>
        /// <param name="RoleName"></param>
        /// <param name="RoleisActive"></param>
        /// <param name="UserID"></param>
        /// <param name="ModulesEnabled"></param>
        /// <param name="ModulesDisabled"></param>
        public int InsertUpdateCRMRole(int CRMRoleID,int tenantID, string RoleName, bool RoleisActive, int UserID, string ModulesEnabled, string ModulesDisabled)
        {

            int  count= 0;
 
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(CRMRoleID > 0 ? "SP_UpdateCRMRole" : "SP_InsertCRMRole", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                if(CRMRoleID> 0)
                {
                    cmd.Parameters.AddWithValue("@_Modify_By", UserID);
                    cmd.Parameters.AddWithValue("@_CRMRoles_ID", CRMRoleID); 
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_createdBy", UserID);
                }
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_RoleName", RoleName);
                cmd.Parameters.AddWithValue("@_isRoleActive", Convert.ToInt16(RoleisActive));

                cmd.Parameters.AddWithValue("@_ModulesIsEnabled", string.IsNullOrEmpty(ModulesEnabled) ? "" : ModulesEnabled.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_ModulesIsDisabled", string.IsNullOrEmpty(ModulesDisabled) ? "" : ModulesDisabled.TrimEnd(','));
                count = Convert.ToInt32(cmd.ExecuteScalar());

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

            return count;
        }


        /// <summary>
        /// Delete CRMRole
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="CRMRoleID"></param>
        public int DeleteCRMRole(int tenantID, int CRMRoleID)
        {
            int deletecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteCRMRole", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@CRMRoles_ID", CRMRoleID);
              
                cmd.CommandType = CommandType.StoredProcedure;
                deletecount = Convert.ToInt32(cmd.ExecuteScalar());
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
            return deletecount;

        }

        /// <summary>
        /// Get CRM Role List
        /// </summary>
        /// <param name="tenantID"></param>
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
                            isRoleActive = r.Field<object>("RoleStatus") == System.DBNull.Value ? string.Empty: Convert.ToString(r.Field<object>("RoleStatus")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),
                              }).ToList();
                    }

                    if(objCRMLst.Count> 0)
                    {
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < objCRMLst.Count; i++)
                            {
                                objCRMLst[i].Modules = ds.Tables[1].AsEnumerable().Where(r => r.Field<object>("CRMRolesID") != System.DBNull.Value &&
                                    objCRMLst[i].CRMRoleID == Convert.ToInt32(r.Field<object>("CRMRolesID"))).Select(r => new ModuleDetails()
                                    {
                                        CRMRoleID = Convert.ToInt32(r.Field<object>("CRMRolesID")),
                                        ModuleID= Convert.ToInt32(r.Field<object>("ModuleID")),
                                        ModuleName = r.Field<object>("ModuleName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModuleName")),
                                        Modulestatus = r.Field<object>("ModuleStatus") == System.DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(r.Field<object>("ModuleStatus"))),

                                     }).OrderBy(x=>x.ModuleID).ToList();
                        }

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
                if (ds != null) ds.Dispose(); conn.Close();
            }
            return objCRMLst;
        }

        /// <summary>
        /// Get CRM Role Dropdown
        /// </summary>
        /// <param name="tenantID"></param>
        public List<CRMRoleModel> GetCRMRoleDropdown(int tenantID)
        {

            List<CRMRoleModel> objCRMLst = new List<CRMRoleModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetCRMDropdown", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CRMRoleModel cRMRoleModel = new CRMRoleModel();
                        cRMRoleModel.CRMRoleID = Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"]);
                        cRMRoleModel.RoleName = Convert.ToString(ds.Tables[0].Rows[i]["RoleName"]);
                        objCRMLst.Add(cRMRoleModel);
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

            return objCRMLst;
        }

        /// <summary>
        /// Get CRM Role By UserID
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        public CRMRoleModel GetCRMRoleByUserID(int tenantID, int UserID)
        {
            CRMRoleModel cRMRoleModel = new CRMRoleModel();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("GetCRMRoleDetailsByUserID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("@_userID", UserID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                var test = ds.Tables.Count;

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        cRMRoleModel = ds.Tables[0].AsEnumerable().Select(r => new CRMRoleModel()
                        {
                            CRMRoleID = Convert.ToInt32(r.Field<object>("CRMRolesID")),

                            RoleName = r.Field<object>("RoleName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("RoleName")),
                            isRoleActive = r.Field<object>("RoleStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("RoleStatus")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),
                        }).FirstOrDefault();

                    }

                    if (cRMRoleModel != null)
                    {
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {

                                cRMRoleModel.Modules = ds.Tables[1].AsEnumerable().Where(r => r.Field<object>("CRMRolesID") != System.DBNull.Value &&
                                    cRMRoleModel.CRMRoleID.Equals(Convert.ToInt32(r.Field<object>("CRMRolesID")))).Select(r => new ModuleDetails()
                                    {
                                        CRMRoleID = Convert.ToInt32(r.Field<object>("CRMRolesID")),
                                        ModuleID = Convert.ToInt32(r.Field<object>("ModuleID")),
                                        ModuleName = r.Field<object>("ModuleName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModuleName")),
                                        Modulestatus = r.Field<object>("ModuleStatus") == System.DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(r.Field<object>("ModuleStatus"))),

                                    }).OrderBy(x => x.ModuleID).ToList();

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

            return cRMRoleModel;
        }

        /// <summary>
        /// Bulk Upload CRM Role
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        public List<string> BulkUploadCRMRole(int TenantID, int CreatedBy, int RoleFor, DataSet DataSetCSV)
        {
            XmlDocument xmlDoc = new XmlDocument();
            List<string> csvLst = new List<string>();
            string SuccesFile = string.Empty; string ErroFile = string.Empty;
            DataSet Bulkds = new DataSet();

            try
            {
                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {
                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        xmlDoc.LoadXml(DataSetCSV.GetXml());

                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SP_BulkUploadCRMRoles", conn);
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        cmd.Parameters.AddWithValue("@_roleFor", RoleFor);
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);

                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(Bulkds);

                        if (Bulkds != null && Bulkds.Tables[0] != null && Bulkds.Tables[1] != null)
                        {

                            if (Bulkds != null && Bulkds.Tables[0] != null && Bulkds.Tables[1] != null)
                            {

                                //for success file
                                SuccesFile = Bulkds.Tables[0].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[0]) : string.Empty;
                                csvLst.Add(SuccesFile);

                                //for error file
                                ErroFile = Bulkds.Tables[1].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[1]) : string.Empty;
                                csvLst.Add(ErroFile);

                            }

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

        #endregion
    }
}
