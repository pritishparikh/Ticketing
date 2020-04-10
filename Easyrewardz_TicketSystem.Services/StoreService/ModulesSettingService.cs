using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public class ModulesSettingService : IModulesSetting
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion

        MySqlConnection conn = new MySqlConnection();

        public ModulesSettingService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        #region Claim Attachment Setting

        /// <summary>
        /// Get Store Attachment Settings
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public AttachmentSettingResponseModel GetStoreAttachmentSettings(int TenantId, int CreatedBy)
        {
            AttachmentSettingResponseModel obj = new AttachmentSettingResponseModel();

            DataSet ds = new DataSet();
            try
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreAttachmentSettings", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantId", TenantId);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                List<StoreAttachmentFileFormat> storeattachmentfileformat = new List<StoreAttachmentFileFormat>();
                List<ArrachementSize> arrachementsize = new List<ArrachementSize>();
                List<AttachmentSettings> attachmentsettings = new List<AttachmentSettings>();

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        storeattachmentfileformat = ds.Tables[0].AsEnumerable().Select(r => new StoreAttachmentFileFormat()
                        {
                            FormatID = Convert.ToInt32(r.Field<object>("FormatID")),
                            FileFormaName = r.Field<object>("FileFormaName") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileFormaName")),

                        }).ToList();
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        arrachementsize = ds.Tables[1].AsEnumerable().Select(r => new ArrachementSize()
                        {
                            Numb = Convert.ToInt32(r.Field<object>("numb")),
                            NumbMB = r.Field<object>("numbMB") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("numbMB")),

                        }).ToList();
                    }
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        attachmentsettings = ds.Tables[2].AsEnumerable().Select(r => new AttachmentSettings()
                        {
                            SettingID = Convert.ToInt32(r.Field<object>("SettingID")),
                            AttachmentSize = Convert.ToInt32(r.Field<object>("AttachmentSize")),
                            FileFomatID = Convert.ToInt32(r.Field<object>("FileFomatID")),
                            CreatedBy = Convert.ToInt32(r.Field<object>("CreatedBy")),

                        }).ToList();
                    }
                }

                obj.StoreAttachmentFileFormatList = storeattachmentfileformat;
                obj.ArrachementSizeList = arrachementsize;
                obj.AttachmentSettingsList = attachmentsettings;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ds != null) ds.Dispose(); conn.Close();
            }


            return obj;
        }

        /// <summary>
        /// Modify Store Attachment Settings
        /// </summary>
        /// <param name="AttachmentSettings"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int ModifyStoreAttachmentSettings(int TenantId, int CreatedBy, AttachmentSettingsRequest AttachmentSettings)
        {
            int result = 0;
            try
            {
                if (AttachmentSettings.AttachmentSize > 0)
                {
                    if (AttachmentSettings.FileFomatID.Length > 0)
                    {

                        conn.Open();

                        MySqlCommand Targetcmd = new MySqlCommand("SP_InsertStoreAttachmentSettings", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        Targetcmd.Parameters.AddWithValue("@_AttachmentSize", AttachmentSettings.AttachmentSize);
                        Targetcmd.Parameters.AddWithValue("@_FileFomatID", AttachmentSettings.FileFomatID);
                        Targetcmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);
                        Targetcmd.Parameters.AddWithValue("@_TenantId", TenantId);

                        result = Convert.ToInt32(Targetcmd.ExecuteScalar());
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

            return result;
        }

        #endregion

        #region Campaign Script

        /// <summary>
        /// Get All Campaign Script / By ID
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="_CampaignID"></param>
        /// <returns></returns>
        public List<CampaignScriptDetails> GetCampaignScript(int TenantId, int CampaignID)
        {
            List<CampaignScriptDetails> obj = new List<CampaignScriptDetails>();

            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetCampaignScript", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_CampaignID", CampaignID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
              
                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        obj = ds.Tables[0].AsEnumerable().Select(r => new CampaignScriptDetails()
                        {
                            CampaignID = Convert.ToInt32(r.Field<object>("CampaignID")),
                            CampaignName = r.Field<object>("CampaignName") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CampaignName")),
                            CampaignNameID = Convert.ToInt32(r.Field<object>("CampaignNameID")),
                            CampaignScriptLess = r.Field<object>("CampaignScript") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CampaignScript")).Length < 15 ? Convert.ToString(r.Field<object>("CampaignScript")) : Convert.ToString(r.Field<object>("CampaignScript")).Substring(0, 15),
                            CampaignScript = r.Field<object>("CampaignScript") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CampaignScript")),
                            CreatedBy = r.Field<object>("createdBy") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("createdBy")),
                            CreatedOn = r.Field<object>("CreatedOn") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedOn")),
                            ModifiedBy = r.Field<object>("modifiedBy") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("modifiedBy")),
                            ModifiedOn = r.Field<object>("ModifiedOn") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModifiedOn")),
                            Status = r.Field<object>("Status") == DBNull.Value ? false : Convert.ToBoolean(r.Field<object>("Status")),
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


            return obj;
        }

        /// <summary>
        /// Get Campaign Name
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="_CampaignID"></param>
        /// <returns></returns>
        public List<CampaignScriptName> GetCampaignName(int TenantId)
        {
            List<CampaignScriptName> obj = new List<CampaignScriptName>();

            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetCampaignName", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.AddWithValue("@_TenantID", TenantId);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        obj = ds.Tables[0].AsEnumerable().Select(r => new CampaignScriptName()
                        {
                            CampaignNameID = Convert.ToInt32(r.Field<object>("ID")),
                            CampaignName = r.Field<object>("CampaignName") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CampaignName")),
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


            return obj;
        }

        /// <summary>
        /// Validate Campaign Name Exit
        /// </summary>
        /// <param name="CampaignNameID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public string ValidateCampaignNameExit(int CampaignNameID, int TenantID)
        {

            string Message = "";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ValidateCampaignNameExit", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@_CampaignNameID", CampaignNameID);
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
               
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

        /// <summary>
        /// Insert Campaign Script
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        public int InsertCampaignScript(int TenantId, int CreatedBy, CampaignScriptRequest Campaignscript)
        {
            int result = 0;
            try
            {
                if (Campaignscript.CampaignNameID.Length > 0)
                {
                    if (Campaignscript.CampaignScript.Length > 0)
                    {

                        conn.Open();

                        MySqlCommand Targetcmd = new MySqlCommand("SP_InsertCampaignScript", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        Targetcmd.Parameters.AddWithValue("@_CampaignNameID", Campaignscript.CampaignNameID);
                        Targetcmd.Parameters.AddWithValue("@_CampaignScript", Campaignscript.CampaignScript);
                        Targetcmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);
                        Targetcmd.Parameters.AddWithValue("@_Status", TenantId);
                        Targetcmd.Parameters.AddWithValue("@_TenantID", TenantId);

                        result = Targetcmd.ExecuteNonQuery();
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

            return result;
        }

        /// <summary>
        /// Insert Campaign Script
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        public int UpdateCampaignScript(int TenantId, int CreatedBy, CampaignScriptRequest Campaignscript)
        {
            int result = 0;
            try
            {
                if (Campaignscript.CampaignID > 0)
                {
                    if (Campaignscript.CampaignScript.Length > 0)
                    {

                        conn.Open();

                        MySqlCommand Targetcmd = new MySqlCommand("SP_UpdateCampaignScript", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        Targetcmd.Parameters.AddWithValue("@_CampaignID", Campaignscript.CampaignID);
                        Targetcmd.Parameters.AddWithValue("@_CampaignNameID", Campaignscript.CampaignNameID);
                        Targetcmd.Parameters.AddWithValue("@_CampaignScript", Campaignscript.CampaignScript);
                        Targetcmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);
                        Targetcmd.Parameters.AddWithValue("@_Status", Campaignscript.Status);
                        Targetcmd.Parameters.AddWithValue("@_TenantID", TenantId);

                        result = Targetcmd.ExecuteNonQuery();
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

            return result;
        }

        /// <summary>
        /// Delete Campaign Script
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="Campaignscript"></param>
        /// <returns></returns>
        public int DeleteCampaignScript(int TenantId, int CreatedBy, int CampaignID)
        {
            int result = 0;
            try
            {
                if (CampaignID > 0)
                {
                    conn.Open();

                    MySqlCommand Targetcmd = new MySqlCommand("SP_DeleteCampaignScirpt", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    Targetcmd.Parameters.AddWithValue("@_CampaignID", CampaignID);
                    result = Targetcmd.ExecuteNonQuery();
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

            return result;
        }

        /// <summary>
        /// Bulk Upload Campaign
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public List<string> CampaignBulkUpload(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            XmlDocument xmlDoc = new XmlDocument();
            DataSet Bulkds = new DataSet();
            List<string> csvLst = new List<string>();
            string SuccessFile = string.Empty; string ErrorFile = string.Empty;
            try
            {
                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {
                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        xmlDoc.LoadXml(DataSetCSV.GetXml());
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SP_BulkUploadStoreCampaign", conn);

                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);

                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
                        da.Fill(Bulkds);

                        if (Bulkds != null && Bulkds.Tables[0] != null && Bulkds.Tables[1] != null)
                        {

                            //for success file
                            SuccessFile = Bulkds.Tables[0].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[0]) : string.Empty;
                            csvLst.Add(SuccessFile);

                            //for error file
                            ErrorFile = Bulkds.Tables[1].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[1]) : string.Empty;
                            csvLst.Add(ErrorFile);


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
        #endregion
    }
}
