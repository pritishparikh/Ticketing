using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class DesignationService:IDesignation
    {
        MySqlConnection conn = new MySqlConnection();
        public DesignationService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Get Designations
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<DesignationMaster> GetDesignations(int TenantID,int hierarchyFor)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getDesignationMaster", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@hierarchy_For", hierarchyFor);
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
        /// Get Reportee Designation
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="HierarchyFor"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<DesignationMaster> GetReporteeDesignation(int DesignationID, int HierarchyFor, int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetReporteeDesignation", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                cmd1.Parameters.AddWithValue("@Designation_ID", DesignationID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@Hierarchy_For", HierarchyFor);        
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
        /// Get Report To User
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="IsStoreUser"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<CustomSearchTicketAgent> GetReportToUser(int DesignationID, int IsStoreUser, int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomSearchTicketAgent> Users = new List<CustomSearchTicketAgent>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetUserBasedonReportee", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                cmd1.Parameters.AddWithValue("@Designation_ID", DesignationID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@Is_StoreUser", IsStoreUser);
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
    }
}
