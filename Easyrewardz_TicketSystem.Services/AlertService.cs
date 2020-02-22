using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public class AlertService: IAlerts
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion
        MySqlConnection conn = new MySqlConnection();

        public AlertService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Create Alert 
        /// </summary>
        public int InsertAlert(AlertInsertModel alertModel)
        {
            int InsertAlertID = 0; int AlertConfigInsertCount = 0;
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertAlert", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_tenantID", alertModel.TenantId);
                cmd.Parameters.AddWithValue("@_alertTypeName", alertModel.AlertTypeName);
                cmd.Parameters.AddWithValue("@_isAlertActive", Convert.ToInt16(alertModel.isAlertActive));
                cmd.Parameters.AddWithValue("@_createdBy", alertModel.CreatedBy);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        InsertAlertID = ds.Tables[0].Rows[0]["AlertId"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["AlertId"]);

                    }

                    if (InsertAlertID > 0)
                    {
                        if (alertModel.CommunicationModeDetails.Count > 0)
                        {
                            for (int i = 0; i < alertModel.CommunicationModeDetails.Count; i++)
                            {
                                MySqlCommand Targetcmd = new MySqlCommand("SP_InsertAlerMasterConfig", conn);
                                Targetcmd.Connection = conn;
                                Targetcmd.Parameters.AddWithValue("@_alertID", InsertAlertID);
                                Targetcmd.Parameters.AddWithValue("@_commMode", alertModel.CommunicationModeDetails[i].Communication_Mode);
                                Targetcmd.Parameters.AddWithValue("@_commFor", alertModel.CommunicationModeDetails[i].CommunicationFor);
                                Targetcmd.Parameters.AddWithValue("@_content", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].Content) ? string.Empty : alertModel.CommunicationModeDetails[i].Content);
                                Targetcmd.Parameters.AddWithValue("@_toemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].ToEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].ToEmailID);
                                Targetcmd.Parameters.AddWithValue("@_ccemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].CCEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].CCEmailID);
                                Targetcmd.Parameters.AddWithValue("@_bccemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].BCCEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].BCCEmailID);
                                Targetcmd.Parameters.AddWithValue("@_emailSubject", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].Subject) ? string.Empty : alertModel.CommunicationModeDetails[i].Subject);
                                Targetcmd.Parameters.AddWithValue("@_createdBy", alertModel.CreatedBy);
                                Targetcmd.Parameters.AddWithValue("@_isactive", Convert.ToInt16(alertModel.isAlertActive));
                                Targetcmd.CommandType = CommandType.StoredProcedure;
                                AlertConfigInsertCount += Targetcmd.ExecuteNonQuery();
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

            return AlertConfigInsertCount;
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
        public List<AlertList> BindAlerts(int tenantID)
        {
            List<AlertList> objAlertLst = new List<AlertList>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_BindAlerts", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objAlertLst = ds.Tables[0].AsEnumerable().Select(r => new AlertList()
                        {
                            AlertID = Convert.ToInt32(r.Field<object>("AlertID")),
                            AlertTypeName = r.Field<object>("AlertTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AlertTypeName")),
                            isAlertActive = r.Field<object>("IsActive") == System.DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(r.Field<object>("IsActive")))

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


            return objAlertLst;

        }

        /// <summary>
        /// Get Alert List
        /// </summary>
        public List<AlertModel> GetAlertList(int tenantID)
        {
            List<AlertModel> objAlertLst = new List<AlertModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetAlertList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objAlertLst = ds.Tables[0].AsEnumerable().Select(r => new AlertModel()
                        {
                            AlertID = Convert.ToInt32(r.Field<object>("AlertID")),
                            AlertTypeName = r.Field<object>("AlertTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AlertTypeName")),
                            ModeOfCommunication=new CommunicationModeBy {

                                isByEmail= r.Field<object>("EmailMode") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("EmailMode")) ==0 ? false : true,
                                isBySMS = r.Field<object>("SMSMode") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("SMSMode")) == 0 ? false : true,
                                isByNotification = r.Field<object>("NotificationMode") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("NotificationMode")) == 0 ? false : true,
                            },
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),
                            isAlertActive= r.Field<object>("AlertStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AlertStatus"))

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


            return objAlertLst;

        }

        public int BulkUploadAlert(int TenantID, int CreatedBy, DataSet DataSetCSV)
        {
            int uploadcount = 0;
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {
                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        xmlDoc.LoadXml(DataSetCSV.GetXml());

                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("", conn);
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        uploadcount = cmd.ExecuteNonQuery();
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
            return uploadcount;
        }


        #region Communication Mode Mapping



        #endregion

    }
}
