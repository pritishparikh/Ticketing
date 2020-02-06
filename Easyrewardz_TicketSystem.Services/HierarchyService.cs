using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;

namespace Easyrewardz_TicketSystem.Services
{
   public class HierarchyService : IHierarchy
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public HierarchyService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion
        public int CreateHierarchy(CustomHierarchymodel customHierarchymodel)
        {
            int Success = 0;
            if(customHierarchymodel.DesignationID==0)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_CreateHierarchy", conn);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Designation_Name", customHierarchymodel.DesignationName);
                    cmd.Parameters.AddWithValue("@Reportto_ID", customHierarchymodel.ReportToDesignation);
                    cmd.Parameters.AddWithValue("@Is_Active", customHierarchymodel.IsActive);
                    cmd.Parameters.AddWithValue("@Tenant_ID", customHierarchymodel.TenantID);
                    cmd.Parameters.AddWithValue("@User_ID", customHierarchymodel.CreatedBy);
                    cmd.Parameters.AddWithValue("@Hierarchy_For", customHierarchymodel.HierarchyFor);
                    Success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            }
            if (customHierarchymodel.DesignationID > 0)
            {

                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_UpdateHierarchy", conn);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Designation_ID", customHierarchymodel.DesignationID);
                    cmd.Parameters.AddWithValue("@Designation_Name", string.IsNullOrEmpty(customHierarchymodel.DesignationName) ? "" : customHierarchymodel.DesignationName);
                    cmd.Parameters.AddWithValue("@Reportto_ID", string.IsNullOrEmpty(Convert.ToString(customHierarchymodel.ReportToDesignation)) ? 0 : customHierarchymodel.ReportToDesignation);
                    cmd.Parameters.AddWithValue("@Is_Active", customHierarchymodel.IsActive);
                    cmd.Parameters.AddWithValue("@Tenant_ID", customHierarchymodel.TenantID);
                    cmd.Parameters.AddWithValue("@User_ID", customHierarchymodel.CreatedBy);
                    cmd.Parameters.AddWithValue("@Hierarchy_For", customHierarchymodel.HierarchyFor);
                    cmd.Parameters.AddWithValue("@Delete_flag", customHierarchymodel.Deleteflag);
                    Success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            }



                return Success;
        }

        public List<CustomHierarchymodel> ListHierarchy(int TenantID, int HierarchyFor)
        {
            DataSet ds = new DataSet();
            List<CustomHierarchymodel> listHierarchy = new List<CustomHierarchymodel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ListHierarchy", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Hierarchy_For", HierarchyFor);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++) 
                    {
                        CustomHierarchymodel hierarchymodel = new CustomHierarchymodel();
                        hierarchymodel.DesignationID= Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationID"]);
                        hierarchymodel.DesignationName= Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);
                        hierarchymodel.ReportTo= Convert.ToString(ds.Tables[0].Rows[i]["ReportTo"]);
                        hierarchymodel.Createdbyperson= Convert.ToString(ds.Tables[0].Rows[i]["Createdby"]);
                        hierarchymodel.Updatedbyperson = Convert.ToString(ds.Tables[0].Rows[i]["UpdatedBy"]);
                        hierarchymodel.Status= Convert.ToString(ds.Tables[0].Rows[i]["IsActive"]);
                        hierarchymodel.Createddate= Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                        hierarchymodel.Createdateformat= hierarchymodel.Createddate.ToString("dd MMMM yyyy");
                        hierarchymodel.Updateddate= Convert.ToDateTime(ds.Tables[0].Rows[i]["ModifiedDate"]);
                        hierarchymodel.Updateddateformat = hierarchymodel.Updateddate.ToString("dd MMMM yyyy");
                        hierarchymodel.ReportToDesignation = Convert.ToInt16(ds.Tables[0].Rows[i]["ReportToDesignation"]); 
                        listHierarchy.Add(hierarchymodel);
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
            return listHierarchy;
        }
    }
}
