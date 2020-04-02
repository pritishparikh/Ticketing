using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreHierarchyService : IStoreHierarchy
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion

        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public StoreHierarchyService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion
        public List<string> BulkUploadStoreHierarchy(int TenantID, int CreatedBy, DataSet DataSetCSV)
        {
            throw new NotImplementedException();
        }

        public int CreateStoreHierarchy(CustomHierarchymodel customHierarchymodel)
        {
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_CreateStoreHierarchy", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Designation_Name", customHierarchymodel.DesignationName);
                cmd.Parameters.AddWithValue("@Reportto_ID", customHierarchymodel.ReportToDesignation);
                cmd.Parameters.AddWithValue("@Is_Active", customHierarchymodel.IsActive);
                cmd.Parameters.AddWithValue("@Tenant_ID", customHierarchymodel.TenantID);
                cmd.Parameters.AddWithValue("@User_ID", customHierarchymodel.CreatedBy);
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

        public int DeleteStoreHierarchy(int designationID, int userMasterID, int tenantID)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteStoreHierarchy", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Hierarchy_ID", designationID);
                cmd.Parameters.AddWithValue("@Created_By", userMasterID);
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

        public List<DesignationMaster> GetDesignations(int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();

            try
            {

                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getStoreDesignationMaster", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
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

        public List<CustomHierarchymodel> ListStoreHierarchy(int TenantID)
        {
            DataSet ds = new DataSet();
            List<CustomHierarchymodel> listHierarchy = new List<CustomHierarchymodel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ListStoreHierarchy", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                       
                        CustomHierarchymodel hierarchymodel = new CustomHierarchymodel();
                        hierarchymodel.DesignationID = ds.Tables[0].Rows[i]["DesignationID"] == DBNull.Value ? 0: Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationID"]);
                        hierarchymodel.DesignationName = ds.Tables[0].Rows[i]["DesignationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);
                        hierarchymodel.ReportTo = ds.Tables[0].Rows[i]["ReportTo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReportTo"]);
                        hierarchymodel.Createdbyperson = ds.Tables[0].Rows[i]["Createdby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Createdby"]);
                        hierarchymodel.Updatedbyperson = ds.Tables[0].Rows[i]["UpdatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UpdatedBy"]);
                        hierarchymodel.Status = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsActive"]);
                        hierarchymodel.Createddate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                        hierarchymodel.Createdateformat = hierarchymodel.Createddate.ToString("dd MMMM yyyy");
                        hierarchymodel.Updateddate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ModifiedDate"]);
                        hierarchymodel.Updateddateformat = hierarchymodel.Updateddate.ToString("dd MMMM yyyy");
                        hierarchymodel.ReportToDesignation = ds.Tables[0].Rows[i]["ReportToDesignation"] == DBNull.Value ? 0 : Convert.ToInt16(ds.Tables[0].Rows[i]["ReportToDesignation"]);
                        listHierarchy.Add(hierarchymodel);
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
            return listHierarchy;
        }

        public int UpdateStoreHierarchy(CustomHierarchymodel customHierarchymodel)
        {
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_UpdateStoreHierarchy"
                };
                cmd.Parameters.AddWithValue("@Designation_ID", customHierarchymodel.DesignationID);
                cmd.Parameters.AddWithValue("@Designation_Name", string.IsNullOrEmpty(customHierarchymodel.DesignationName) ? "" : customHierarchymodel.DesignationName);
                cmd.Parameters.AddWithValue("@Reportto_ID", string.IsNullOrEmpty(Convert.ToString(customHierarchymodel.ReportToDesignation)) ? 0 : customHierarchymodel.ReportToDesignation);
                cmd.Parameters.AddWithValue("@Is_Active", customHierarchymodel.IsActive);
                cmd.Parameters.AddWithValue("@Tenant_ID", customHierarchymodel.TenantID);
                cmd.Parameters.AddWithValue("@User_ID", customHierarchymodel.CreatedBy);
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

