using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class PaymentService : IPayment
    {
        MySqlConnection conn = new MySqlConnection();

        public PaymentService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public int InsertChequeDetails(OfflinePaymentModel offlinePaymentModel)
        {
            DataSet ds = new DataSet();
            int result = 0;
            try
            {
                conn.Open();
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
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
