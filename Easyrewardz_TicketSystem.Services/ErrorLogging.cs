using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class ErrorLogging : IErrorLogging
    {
        MySqlConnection conn = new MySqlConnection();
        public ErrorLogging(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        public int InsertErrorLog(ErrorLog errorLog)
        {
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ErrorLog", conn);
                cmd.Connection = conn;
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
    }
}
