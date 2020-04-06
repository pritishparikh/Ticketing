using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class AttachmentSettingService : IAttachmentSetting
    {
        MySqlConnection conn = new MySqlConnection();

        public AttachmentSettingService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }


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
                            FileFormaName = r.Field<object>("FileFormaName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileFormaName")),

                        }).ToList();
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        arrachementsize = ds.Tables[1].AsEnumerable().Select(r => new ArrachementSize()
                        {
                            Numb = Convert.ToInt32(r.Field<object>("numb")),
                            NumbMB = r.Field<object>("numbMB") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("numbMB")),

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
    }
}
