using Easyrewardz_TicketSystem.Interface;
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
    public class SLAServices : ISLA
    {
        MySqlConnection conn = new MySqlConnection();

        public SLAServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<SLAStatus> GetSLAStatusList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SLAStatus> slas = new List<SLAStatus>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetSLAStatusList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SLAStatus sla = new SLAStatus();
                        sla.SLAId = Convert.ToInt32(ds.Tables[0].Rows[i]["SlaId"]);
                        sla.SLATargetId = Convert.ToInt32(ds.Tables[0].Rows[i]["SLATargetId"]);
                        sla.TenatID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantId"]);
                        sla.SLARequestResponse = Convert.ToString(ds.Tables[0].Rows[i]["Respond"]) + "/" + Convert.ToString(ds.Tables[0].Rows[i]["Resolution"]);

                        slas.Add(sla);
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

            return slas;
        }

    }
}
