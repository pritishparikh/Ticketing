using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class AlertService: IAlerts
    {

        MySqlConnection conn = new MySqlConnection();

        public AlertService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }


        /// <summary>
        /// Update Alert
        /// </summary>
        public int UpdateAlert(int tenantId, int AlertID, string AlertTypeName, bool isAlertActive, int ModifiedBy)
        {
            int updatecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateAlert", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@_alertID", AlertID);
                cmd.Parameters.AddWithValue("@_alertTypeName", AlertTypeName);

                cmd.Parameters.AddWithValue("@_isAlertActive", Convert.ToInt16(isAlertActive));
                cmd.Parameters.AddWithValue("@_modifiedBy", ModifiedBy);
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
        /// Delete Alert
        /// </summary>
        public int DeleteAlert(int tenantID, int AlertID)
        {
            int deletecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteAlert", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_alertID", AlertID);


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
        /// Get Alert List
        /// </summary>
        //public List<AlertModel> AlertList(int tenantID)
        //{
        //    List<AlertModel> objAlertLst = new List<AlertModel>();
        //    DataSet ds = new DataSet();
        //    MySqlCommand cmd = new MySqlCommand();
        //    try
        //    {

        //        //int rowStart = (pageNo - 1) * PageSize;
        //        conn.Open();
        //        cmd.Connection = conn;

        //        MySqlCommand cmd1 = new MySqlCommand("SP_GetSLAList", conn);
        //        cmd1.CommandType = CommandType.StoredProcedure;
        //        cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
        //        MySqlDataAdapter da = new MySqlDataAdapter();
        //        da.SelectCommand = cmd1;
        //        da.Fill(ds);

        //        if (ds != null && ds.Tables != null)
        //        {
        //            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                objAlertLst = ds.Tables[0].AsEnumerable().Select(r => new AlertModel()
        //                {
        //                    SLAID = Convert.ToInt32(r.Field<object>("SlaId")),

        //                    IssueTpeID = r.Field<object>("IssueTypeID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IssueTypeID")),
        //                    IssueTpeName = r.Field<object>("IssueTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IssueTypeName")),
        //                    isSLAActive = r.Field<object>("SLAStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SLAStatus")),
        //                    CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
        //                    CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
        //                    ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
        //                    ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),
        //                }).ToList();
        //            }

        //            if (objAlertLst.Count > 0)
        //            {
        //                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < objSLALst.Count; i++)
        //                    {
        //                        objSLALst[i].SLATarget = ds.Tables[1].AsEnumerable().Where(r => r.Field<object>("SlaID") != System.DBNull.Value &&
        //                            objSLALst[i].SLAID == Convert.ToInt32(r.Field<object>("SlaID"))).Select(r => new SLATargetResponseModel()
        //                            {
        //                                SLATargetID = Convert.ToInt32(r.Field<object>("SLATargetID")),
        //                                PriorityID = Convert.ToInt32(r.Field<object>("PriorityID")),
        //                                PriorityName = r.Field<object>("PriortyName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriortyName")),
        //                                SLABreachPercent = r.Field<object>("PriorityBreach") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriorityBreach")),
        //                                PriorityRespond = r.Field<object>("PriorityRespond") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriorityRespond")),
        //                                PriorityResolution = r.Field<object>("PriorityResolve") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriorityResolve")),

        //                            }).ToList();
        //                    }

        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string message = Convert.ToString(ex.InnerException);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (ds != null) ds.Dispose(); conn.Close();
        //    }


        //    return objSLALst;

        //}
    }
}
