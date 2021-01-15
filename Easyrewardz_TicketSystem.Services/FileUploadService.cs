using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class FileUploadService: IFileUpload
    {


        MySqlConnection conn = new MySqlConnection();

        public FileUploadService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Create File Upload
        /// </summary>
        public int CreateFileUploadLog(int tenantid, string filename, bool isuploaded, string errorlogfilename, string successlogfilename,
            int createdby, string filetype, string succesFilepath, string errorFilepath, int fileuploadFor)
        {

            int count = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertFileUploadLogs", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_tenantid", tenantid);
                cmd.Parameters.AddWithValue("@_filename", filename);
                cmd.Parameters.AddWithValue("@_isuploaded", Convert.ToInt16(isuploaded));

                cmd.Parameters.AddWithValue("@_errorlogfilename", errorlogfilename);
                cmd.Parameters.AddWithValue("@_successlogfilename", successlogfilename);

                cmd.Parameters.AddWithValue("@_createdby", createdby);
                cmd.Parameters.AddWithValue("@filetype", filetype);

                cmd.Parameters.AddWithValue("@_errorfilepath", errorFilepath);
                cmd.Parameters.AddWithValue("@_sucessfilepath", succesFilepath);
                cmd.Parameters.AddWithValue("@_fileuploadfor", fileuploadFor);


                count = cmd.ExecuteNonQuery();

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

            return count;
        }


        public List<FileUploadLogs> GetFileUploadLogs(int tenantId,int fileuploadFor)
        {
            List<FileUploadLogs> objFileUploadLogLst = new List<FileUploadLogs>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetFileUpoadLogs", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd1.Parameters.AddWithValue("@_fileuploadfor", fileuploadFor);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objFileUploadLogLst = ds.Tables[0].AsEnumerable().Select(r => new FileUploadLogs()
                        {
                            FileUploadLogsID = Convert.ToInt32(r.Field<object>("FileUploadLogsID")),

                            FileType = r.Field<object>("FileType") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileType")),
                            FileName = r.Field<object>("FileName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileName")),
                            Date = r.Field<object>("FileUploadDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileUploadDate")),
                            FileUploadStatus = r.Field<object>("FileUploadStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileUploadStatus")),

                            SuccessFilePath = r.Field<object>("SucessFilePath") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SucessFilePath")),
                            ErrorFilePath = r.Field<object>("ErrorFilePath") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ErrorFilePath")),

                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),


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


            return objFileUploadLogLst;
        }


    }
}
