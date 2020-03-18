using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class PaymentService : IPayment
    {

        #region variable
        private readonly IDistributedCache _Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        MySqlConnection conn = new MySqlConnection();

        public PaymentService(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            _Cache = cache;
        }

        public int InsertChequeDetails(OfflinePaymentModel offlinePaymentModel)
        {
            DataSet ds = new DataSet();
            int result = 0;
            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_InsertChequeDetails", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ChequeNumber", offlinePaymentModel.ChequeNumber);
                cmd.Parameters.AddWithValue("@ChequeAmount", offlinePaymentModel.ChequeAmount);
                cmd.Parameters.AddWithValue("@ChequeDate", offlinePaymentModel.ChequeDate);
                cmd.Parameters.AddWithValue("@FromCompanyName", offlinePaymentModel.FromCompanyName);
                cmd.Parameters.AddWithValue("@PaidToname", offlinePaymentModel.PaidToname);
                cmd.Parameters.AddWithValue("@TenantID", offlinePaymentModel.TenantID);
                cmd.Parameters.AddWithValue("@PaymentModeID", offlinePaymentModel.PaymentModeID);

                result = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return result;
        }
    }
}
