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

        /// <summary>
        /// Create SLA 
        /// </summary>
        public int InsertSLA(SLAModel SLA)
        {
            int InsertSLAID = 0; int SLATargetInsertCount = 0;
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertSLAMaster", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_tenantID", SLA.TenantID);
                cmd.Parameters.AddWithValue("@_createdBy", SLA.CreatedBy);
                cmd.Parameters.AddWithValue("@_issueType", SLA.IssueTypeID);
                cmd.Parameters.AddWithValue("@isSLAActive", Convert.ToInt16(SLA.isSLAActive));

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        InsertSLAID = ds.Tables[0].Rows[0]["InsertedSLA"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["InsertedSLA"]);

                    }

                    if(InsertSLAID > 0 )
                    {
                        if(SLA.SLATarget.Count > 0)
                        {
                            for(int i=0;i< SLA.SLATarget.Count;i++)
                            {
                                MySqlCommand Targetcmd = new MySqlCommand("SP_InsertSLATarget", conn);
                                Targetcmd.Connection = conn;
                                Targetcmd.Parameters.AddWithValue("@_slaID", InsertSLAID);
                                Targetcmd.Parameters.AddWithValue("@_priorityID", SLA.SLATarget[i].PriorityID);
                                Targetcmd.Parameters.AddWithValue("@_prioritySLABreach", SLA.SLATarget[i].SLABreachPercent);
                                Targetcmd.Parameters.AddWithValue("@_priorityRespondValue", SLA.SLATarget[i].PriorityRespondValue);
                                Targetcmd.Parameters.AddWithValue("@_priorityRespondDuraton", SLA.SLATarget[i].PriorityRespondDuration);
                                Targetcmd.Parameters.AddWithValue("@_priorityResolutionValue", SLA.SLATarget[i].PriorityResolutionValue);
                                Targetcmd.Parameters.AddWithValue("@_priorityResolutionDuraton", SLA.SLATarget[i].PriorityResolutionDuration);
                                Targetcmd.Parameters.AddWithValue("@_createdBy", SLA.CreatedBy);
                                Targetcmd.CommandType = CommandType.StoredProcedure;
                                SLATargetInsertCount += Targetcmd.ExecuteNonQuery();
                            }
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
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return SLATargetInsertCount;
        }

        /// <summary>
        /// Update SLA
        /// </summary>
        public int UpdateSLA(int SLAID, int tenantID, int IssuetypeID,  bool isActive, int modifiedBy)
        {
            int updatecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateSLA", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_slaID", SLAID);
                cmd.Parameters.AddWithValue("@_issueType", IssuetypeID);
               
                cmd.Parameters.AddWithValue("@isSLAActive", Convert.ToInt16(isActive));
                cmd.Parameters.AddWithValue("@_modifiedBy", modifiedBy);
                cmd.CommandType = CommandType.StoredProcedure;
                updatecount = cmd.ExecuteNonQuery();
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

            return updatecount;
        }

        /// <summary>
        /// Delete SLA
        /// </summary>
        public int DeleteSLA(int tenantID, int SLAID)
        {
            int deletecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteSLA", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_slaID", SLAID);


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



        /// <summary>
        /// GET SLA
        /// </summary>
        public List<SLAResponseModel> SLAList(int tenantID)
        {
            List<SLAResponseModel> objCRMLst = new List<SLAResponseModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetCRMRolesDetails", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@_tenantID", 1);
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

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


    }
}
