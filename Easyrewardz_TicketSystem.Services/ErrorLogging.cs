using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
   public class ErrorLogging : IErrorLogging
    {
        MySqlConnection conn = new MySqlConnection();

        #region Constructor
        public ErrorLogging(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Insert Error Log
        /// </summary>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        public int InsertErrorLog(ErrorLog errorLog)
        {
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ErrorLog", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@User_ID", errorLog.UserID);
                cmd.Parameters.AddWithValue("@Tenant_ID", errorLog.TenantID);
                cmd.Parameters.AddWithValue("@Controller_Name", errorLog.ControllerName);
                cmd.Parameters.AddWithValue("@Action_Name", errorLog.ActionName);
                cmd.Parameters.AddWithValue("@_Exceptions", errorLog.Exceptions);
                cmd.Parameters.AddWithValue("@_MessageException", errorLog.MessageException);
                cmd.Parameters.AddWithValue("@_IPAddress", errorLog.IPAddress);
                cmd.CommandType = CommandType.StoredProcedure;
                Success = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
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
           return Success;
        }


        /// <summary>
        /// Insert API request response Log
        /// </summary>
        /// <param name="Log"></param>
        /// <returns></returns>
        public async Task<int> InsertAPILog(APILogModel Log)
        {
            int Success = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SP_APIRequestResponseLog", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", Log.TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", string.IsNullOrEmpty(Log.ProgramCode) ? "" : Log.ProgramCode);
                cmd.Parameters.AddWithValue("@_RequestUrl", string.IsNullOrEmpty(Log.RequestUrl) ? "" : Log.RequestUrl);
                cmd.Parameters.AddWithValue("@_ActionName", string.IsNullOrEmpty(Log.RequestUrl) ? "" : Log.ActionName);
                cmd.Parameters.AddWithValue("@_Method", string.IsNullOrEmpty(Log.Method) ? "" : Log.Method);
                cmd.Parameters.AddWithValue("@_RequestToken", string.IsNullOrEmpty(Log.RequestToken) ? "" : Log.RequestToken);
                cmd.Parameters.AddWithValue("@_RequestBody", string.IsNullOrEmpty(Log.RequestBody) ? "" : Log.RequestBody);
                cmd.Parameters.AddWithValue("@_QueryString", string.IsNullOrEmpty(Log.QueryString) ? "" : Log.QueryString);
                cmd.Parameters.AddWithValue("@_Response", string.IsNullOrEmpty(Log.Response) ? "" : Log.Response);
                cmd.Parameters.AddWithValue("@_ResponseTimeTaken", Log.ResponseTimeTaken);
                cmd.Parameters.AddWithValue("@_IPAddress", string.IsNullOrEmpty(Log.IPAddress) ? "" : Log.IPAddress);
                cmd.Parameters.AddWithValue("@_IsClientAPI", Convert.ToInt16(Log.IsClientAPI));
                cmd.CommandType = CommandType.StoredProcedure;
                Success = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
            return Success;
        }
    }
}
