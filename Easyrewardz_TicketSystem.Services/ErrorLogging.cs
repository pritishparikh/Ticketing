using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class ErrorLogging : IErrorLogging
    {
        #region variable
        private readonly IDistributedCache Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        MySqlConnection conn = new MySqlConnection();
        public ErrorLogging(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            Cache = cache;
        }
        public int InsertErrorLog(ErrorLog errorLog)
        {
            int success = 0;
            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_ErrorLog", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", errorLog.UserID);
                cmd.Parameters.AddWithValue("@Tenant_ID", errorLog.TenantID);
                cmd.Parameters.AddWithValue("@Controller_Name", errorLog.ControllerName );
                cmd.Parameters.AddWithValue("@Action_Name", errorLog.ActionName);
                cmd.Parameters.AddWithValue("@_Exceptions", errorLog.Exceptions);
                cmd.Parameters.AddWithValue("@_MessageException", errorLog.MessageException);
                cmd.Parameters.AddWithValue("@_IPAddress", errorLog.IPAddress);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());
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
           return success;
        }
    }
}
