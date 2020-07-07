using Easyrewardz_TicketSystem.Interface.StoreInterface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class StorePayService : IStorePay
    {
        MySqlConnection conn = new MySqlConnection();

        public StorePayService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }


        /// <summary>
        /// Generate Store Pay Link
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        /// 
        public string GenerateStorePayLink(int TenantID, string ProgramCode, int UserID)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            string PaymentLink = string.Empty;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_GetTenantStorePayURL", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        PaymentLink = ds.Tables[0].Rows[0]["PaymentLink"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["PaymentLink"]);
                    }
                }

                if (!string.IsNullOrEmpty(PaymentLink))
                {
                    // add storepaylogic here

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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return PaymentLink;
        }
    }
}
