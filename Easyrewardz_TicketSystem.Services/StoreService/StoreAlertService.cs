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
    public class StoreAlertService : IStoreAlerts
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion
        MySqlConnection conn = new MySqlConnection();

        public StoreAlertService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Create Alert 
        /// </summary>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        public int InsertStoreAlert(AlertInsertModel alertModel)
        {
            int AlertConfigInsertCount = 0;
            try
            {
                if (alertModel.AlertID > 0)
                {
                    if (alertModel.CommunicationModeDetails.Count > 0)
                    {
                        conn.Open();
                        
                        for (int i = 0; i < alertModel.CommunicationModeDetails.Count; i++)
                        {
                            MySqlCommand Targetcmd = new MySqlCommand("SP_InsertStoreAlertMasterConfig", conn)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            Targetcmd.Parameters.AddWithValue("@_alertID", alertModel.AlertID);
                            Targetcmd.Parameters.AddWithValue("@_commMode", alertModel.CommunicationModeDetails[i].Communication_Mode);
                            Targetcmd.Parameters.AddWithValue("@_commFor", alertModel.CommunicationModeDetails[i].CommunicationFor);
                            Targetcmd.Parameters.AddWithValue("@_content", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].Content) ? string.Empty : alertModel.CommunicationModeDetails[i].Content);
                            Targetcmd.Parameters.AddWithValue("@_toemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].ToEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].ToEmailID);
                            Targetcmd.Parameters.AddWithValue("@_ccemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].CCEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].CCEmailID);
                            Targetcmd.Parameters.AddWithValue("@_bccemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].BCCEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].BCCEmailID);
                            Targetcmd.Parameters.AddWithValue("@_emailSubject", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].Subject) ? string.Empty : alertModel.CommunicationModeDetails[i].Subject);
                            Targetcmd.Parameters.AddWithValue("@_createdBy", alertModel.CreatedBy);
                            Targetcmd.Parameters.AddWithValue("@_isactive", Convert.ToInt16(alertModel.isAlertActive));
                            
                            AlertConfigInsertCount += Targetcmd.ExecuteNonQuery();

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
            }

            return AlertConfigInsertCount;
        }

        /// <summary>
        /// Update Alert 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="ModifiedBy"></param>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        public int UpdateStoreAlert(int tenantId, int ModifiedBy, AlertUpdateModel alertModel)
        {
            int AlertConfigUpdateCount = 0;
            try
            {             
                if (alertModel.AlertId > 0)
                {
                    if (alertModel.CommunicationModeDetails.Count > 0)
                    {

                        conn.Open();
                        int DeleteAlertConfig = 1;
                        for (int i = 0; i < alertModel.CommunicationModeDetails.Count; i++)
                        {
                            MySqlCommand Targetcmd = new MySqlCommand("SP_UpdateStoreAlertMasterConfig", conn)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            Targetcmd.Parameters.AddWithValue("@_alertID", alertModel.AlertId);
                            Targetcmd.Parameters.AddWithValue("@_commMode", alertModel.CommunicationModeDetails[i].Communication_Mode);
                            Targetcmd.Parameters.AddWithValue("@_commFor", alertModel.CommunicationModeDetails[i].CommunicationFor);
                            Targetcmd.Parameters.AddWithValue("@_content", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].Content) ? string.Empty : alertModel.CommunicationModeDetails[i].Content);
                            Targetcmd.Parameters.AddWithValue("@_toemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].ToEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].ToEmailID);
                            Targetcmd.Parameters.AddWithValue("@_ccemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].CCEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].CCEmailID);
                            Targetcmd.Parameters.AddWithValue("@_bccemail", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].BCCEmailID) ? string.Empty : alertModel.CommunicationModeDetails[i].BCCEmailID);
                            Targetcmd.Parameters.AddWithValue("@_emailSubject", string.IsNullOrEmpty(alertModel.CommunicationModeDetails[i].Subject) ? string.Empty : alertModel.CommunicationModeDetails[i].Subject);
                            Targetcmd.Parameters.AddWithValue("@_modifiedBy", alertModel.CreatedBy);
                            Targetcmd.Parameters.AddWithValue("@_isactive", Convert.ToInt16(alertModel.isAlertActive));
                            Targetcmd.Parameters.AddWithValue("@_alertTypeID", Convert.ToInt32(alertModel.CommunicationModeDetails[i].AlertTypeID));
                            Targetcmd.Parameters.AddWithValue("@_DeleteAlertConfig", DeleteAlertConfig);
                            AlertConfigUpdateCount += Targetcmd.ExecuteNonQuery();

                            DeleteAlertConfig = 0;
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
            }

            return AlertConfigUpdateCount;
        }

        /// <summary>
        /// Delete Alert 
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="AlertID"></param>
        /// <returns></returns>
        public int DeleteStoreAlert(int tenantID, int AlertID)
        {
            int deletecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteStoreAlert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_alertID", AlertID);

                
                deletecount = cmd.ExecuteNonQuery();
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
        /// Bind Alert 
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<AlertList> BindStoreAlerts(int tenantID)
        {
            List<AlertList> objAlertLst = new List<AlertList>();
            DataSet ds = new DataSet();
            try
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_BindStoreAlerts", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
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
            catch (Exception)
            {

                throw;
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
        /// <param name="tenantID"></param>
        /// <param name="alertID"></param>
        /// <returns></returns>
        public List<AlertModel> GetStoreAlertList(int tenantID, int alertID)
        {
            List<AlertModel> objAlertLst = new List<AlertModel>();
            DataSet ds = new DataSet();
            try
            {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_GetStoreAlertList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@_alertID", alertID);
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objAlertLst = ds.Tables[0].AsEnumerable().Select(r => new AlertModel()
                        {
                            AlertID = Convert.ToInt32(r.Field<object>("AlertID")),
                            AlertTypeName = r.Field<object>("AlertTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AlertTypeName")),
                            isAlertActive = r.Field<object>("AlertStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("AlertStatus")),

                            isByEmail = r.Field<object>("EmailMode") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("EmailMode")) == 0 ? false : true,
                            isBySMS = r.Field<object>("SMSMode") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("SMSMode")) == 0 ? false : true,
                            isByNotification = r.Field<object>("NotificationMode") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("NotificationMode")) == 0 ? false : true,

                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),


                        }).ToList();

                    }

                    if(objAlertLst.Count > 0)
                    {
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            foreach(AlertModel am in objAlertLst)
                            {
                                am.AlertContent = ds.Tables[1].AsEnumerable().Where(r => r.Field<object>("AlertID") != System.DBNull.Value &&
                                    am.AlertID == Convert.ToInt32(r.Field<object>("AlertID"))).Select(r=> new AlertConfigModel()
                                    {
                                        AlertID = Convert.ToInt32(r.Field<object>("AlertID")),
                                        AlertTypeID = Convert.ToInt32(r.Field<object>("AlertTypeID")),

                                        IsEmailCustomer = r.Field<object>("IsEmailCustomer") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("IsEmailCustomer")) == 0 ? false : true,
                                        IsEmailInternal = r.Field<object>("IsEmailInternal") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("IsEmailInternal")) == 0 ? false : true,
                                        IsEmailTicketing = r.Field<object>("IsEmailTicketing") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("IsEmailTicketing")) == 0 ? false : true,

                                        IsSMSCustomer = r.Field<object>("IsSMSCustomer") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("IsSMSCustomer")) == 0 ? false : true,
                                        IsNotificationInternal = r.Field<object>("IsNotificationInternal") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("IsNotificationInternal")) == 0 ? false : true,
                                        IsNotificationTicketing = r.Field<object>("IsNotificationTicketing") == System.DBNull.Value || Convert.ToInt32(r.Field<object>("IsNotificationTicketing")) == 0 ? false : true,

                                        MailContent = r.Field<object>("MailContent") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("MailContent")),
                                        Subject = r.Field<object>("Subject") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("Subject")),
                                        SMSContent = r.Field<object>("SMSContent") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SMSContent")),
                                        NotificationContent = r.Field<object>("NotificationContent") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("NotificationContent"))
                                        
                                    }).ToList();
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


            return objAlertLst;

        }

        /// <summary>
        /// Bulk Upload Alert
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public int BulkStoreUploadAlert(int TenantID, int CreatedBy, DataSet DataSetCSV)
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
                        MySqlCommand cmd = new MySqlCommand("", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);
                        
                        uploadcount = cmd.ExecuteNonQuery();
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
            return uploadcount;
        }

        /// <summary>
        /// Validate Alert
        /// </summary>
        /// <param name="AlertID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public string ValidateStoreAlert(int AlertID, int TenantID)
        {

            string Message = "";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Sp_ValidateStoreAlertExists", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Alert_ID", AlertID);
                           
                Message = Convert.ToString(cmd.ExecuteScalar());

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
            return Message;
        }


        #region Communication Mode Mapping



        #endregion

    }
}
