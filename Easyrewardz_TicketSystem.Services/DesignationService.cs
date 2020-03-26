using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class DesignationService:IDesignation
    {
        #region variable
        private readonly IDistributedCache Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        MySqlConnection conn = new MySqlConnection();
        public DesignationService(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            Cache = cache;
        }
   
        /// <summary>
        /// Get Designation List
        /// </summary>
        /// <returns></returns>
        public List<DesignationMaster> GetDesignations(int TenantID)
        {
            DataSet ds = new DataSet();
            //MySqlCommand cmd = new MySqlCommand();
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();

            try
            {
                conn = Db.Connection;
                MySqlCommand cmd1 = new MySqlCommand("SP_getDesignationMaster", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
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
            catch (Exception ex)
            {

                throw ex;
            }         

            return designationMasters;
        }

        public List<DesignationMaster> GetReporteeDesignation(int DesignationID, int HierarchyFor, int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();

            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetReporteeDesignation", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
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
            catch (Exception ex)
            {

                throw ex;
            }
            
            return designationMasters;
        }

        public List<CustomSearchTicketAgent> GetReportToUser(int DesignationID, int IsStoreUser, int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomSearchTicketAgent> Users = new List<CustomSearchTicketAgent>();

            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetUserBasedonReportee", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
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
            catch (Exception ex)
            {

                throw ex;
            }
            
            return Users;
        }
    }
}
