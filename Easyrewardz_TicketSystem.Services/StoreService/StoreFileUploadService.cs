using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreFileUploadService : IStoreFileUpload
    {
        MySqlConnection conn = new MySqlConnection();
        private readonly string rootPath;

        public StoreFileUploadService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        public StoreFileUploadService(string _connectionString,string RootPath)
        {
            conn.ConnectionString = _connectionString;
            rootPath = RootPath;
        }
        public int CreateFileUploadLog(int tenantid, string filename, bool isuploaded, string errorlogfilename, string successlogfilename, int createdby, string filetype, string succesFilepath, string errorFilepath, int fileuploadFor)
        {
            int count = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertStoreFileUploadLogs", conn);
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

        public List<FileUploadLogs> GetFileUploadLogs(int tenantId, int fileuploadFor)
        {
            List<FileUploadLogs> objFileUploadLogLst = new List<FileUploadLogs>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreFileUpoadLogs", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd1.Parameters.AddWithValue("@_fileuploadfor", fileuploadFor);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                string rootUrl = rootPath + "/" + "bulkupload/downloadfiles/store/";
                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string successFilePath = string.Empty;
                            string errorFilePath = string.Empty;
                            FileUploadLogs objFileUploadLogs = new FileUploadLogs();
                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SucessFilePath"].ToString()))
                            {
                              
                                string successFile = ds.Tables[0].Rows[i]["SucessFilePath"].ToString();
                                int lastIndexOfBackSlash = successFile.LastIndexOf('\\');
                                int secondLastIndex = lastIndexOfBackSlash > 0 ? successFile.LastIndexOf('\\', lastIndexOfBackSlash - 1) : -1;
                                int thirdLastIndex = secondLastIndex > 0 ? successFile.LastIndexOf('\\', secondLastIndex - 1) : -1;
                                successFilePath = rootUrl + successFile.Substring(thirdLastIndex, successFile.Length - thirdLastIndex);
                            }
                            if(!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["ErrorFilePath"].ToString()))
                            {
                                string errorFile = ds.Tables[0].Rows[i]["ErrorFilePath"].ToString();
                                int lastIndexOfBackSlashError = errorFile.LastIndexOf('\\');
                                int secondLastIndexError = lastIndexOfBackSlashError > 0 ? errorFile.LastIndexOf('\\', lastIndexOfBackSlashError - 1) : -1;
                                int thirdLastIndexError = secondLastIndexError > 0 ? errorFile.LastIndexOf('\\', secondLastIndexError - 1) : -1;
                                errorFilePath = rootUrl + errorFile.Substring(thirdLastIndexError, errorFile.Length - thirdLastIndexError);
                            }
                            objFileUploadLogs.ErrorFilePath = errorFilePath;
                            objFileUploadLogs.SuccessFilePath = successFilePath;
                            objFileUploadLogs.FileUploadLogsID =Convert.ToInt32(ds.Tables[0].Rows[i]["FileUploadLogsID"]);
                            objFileUploadLogs.FileType =Convert.ToString(ds.Tables[0].Rows[i]["FileType"]);
                            objFileUploadLogs.FileName = Convert.ToString(ds.Tables[0].Rows[i]["FileName"]);
                            objFileUploadLogs.Date = Convert.ToString(ds.Tables[0].Rows[i]["FileUploadDate"]);
                            objFileUploadLogs.FileUploadStatus = Convert.ToString(ds.Tables[0].Rows[i]["FileUploadStatus"]);
                            objFileUploadLogs.CreatedBy = Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]);
                            objFileUploadLogs.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                            objFileUploadLogs.ModifiedBy = Convert.ToString(ds.Tables[0].Rows[i]["UpdatedBy"]);
                            objFileUploadLogs.ModifiedDate = Convert.ToString(ds.Tables[0].Rows[i]["UpdatedDate"]);
                            objFileUploadLogLst.Add(objFileUploadLogs);

                        }
                        //objFileUploadLogLst = ds.Tables[0].AsEnumerable().Select(r => new FileUploadLogs()
                        //{
                        //    FileUploadLogsID = Convert.ToInt32(r.Field<object>("FileUploadLogsID")),

                        //    FileType = r.Field<object>("FileType") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileType")),
                        //    FileName = r.Field<object>("FileName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileName")),
                        //    Date = r.Field<object>("FileUploadDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileUploadDate")),
                        //    FileUploadStatus = r.Field<object>("FileUploadStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FileUploadStatus")),
                          
                        //    SuccessFilePath = r.Field<object>("SucessFilePath") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SucessFilePath")),
                        //    ErrorFilePath = r.Field<object>("ErrorFilePath") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ErrorFilePath")),

                        //    CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                        //    CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                        //    ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                        //    ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),


                        //}).ToList();
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
