using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;

namespace Easyrewardz_TicketSystem.Services
{
   public class HierarchyService : IHierarchy
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion

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
                    Success = Convert.ToInt32(cmd.ExecuteScalar());

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
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (customHierarchymodel.Deleteflag != 1)
                    {
                        cmd.CommandText = "SP_UpdateHierarchy";
                        ////Update
                        cmd.Parameters.AddWithValue("@Designation_ID", customHierarchymodel.DesignationID);
                        cmd.Parameters.AddWithValue("@Designation_Name", string.IsNullOrEmpty(customHierarchymodel.DesignationName) ? "" : customHierarchymodel.DesignationName);
                        cmd.Parameters.AddWithValue("@Reportto_ID", string.IsNullOrEmpty(Convert.ToString(customHierarchymodel.ReportToDesignation)) ? 0 : customHierarchymodel.ReportToDesignation);
                        cmd.Parameters.AddWithValue("@Is_Active", customHierarchymodel.IsActive);
                        cmd.Parameters.AddWithValue("@Tenant_ID", customHierarchymodel.TenantID);
                        cmd.Parameters.AddWithValue("@User_ID", customHierarchymodel.CreatedBy);
                        cmd.Parameters.AddWithValue("@Hierarchy_For", customHierarchymodel.HierarchyFor);
                        cmd.Parameters.AddWithValue("@Delete_flag", customHierarchymodel.Deleteflag);
                    }
                    else
                    {
                        ////Delete the record
                        cmd.CommandText = "SP_DeleteHierarchy";
                        cmd.Parameters.AddWithValue("@Hierarchy_ID", customHierarchymodel.DesignationID);
                        cmd.Parameters.AddWithValue("@Tenant_ID", customHierarchymodel.TenantID);
                    }
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

        public List<string> BulkUploadHierarchy(int TenantID, int CreatedBy, int HierarchyFor, DataSet DataSetCSV)
        {

            XmlDocument xmlDoc = new XmlDocument();
            DataSet Bulkds = new DataSet();
            List<string> csvLst = new List<string>();
            string SuccesFile = string.Empty; string ErroFile = string.Empty;
            try
            {
                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {
                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        xmlDoc.LoadXml(DataSetCSV.GetXml());

                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SP_BulkUploadHierarchy", conn);
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        cmd.Parameters.AddWithValue("@_hierarchyFor", HierarchyFor);
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(Bulkds);

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
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
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
    }
}
