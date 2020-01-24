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
        public List<FileUploadLogs> GetFileUploadLogs(int tenantId)
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

                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),


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


            return objFileUploadLogLst;
        }
    }
}
