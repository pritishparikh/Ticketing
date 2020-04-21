using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreReportService: IStoreReport
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public StoreReportService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion


        /// <summary>
        /// Search the Report
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        public int GetStoreReportSearch(StoreReportModel searchModel)
        {

            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0; 
            
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;

                resultCount = 10;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return resultCount;
        }
    }
}
