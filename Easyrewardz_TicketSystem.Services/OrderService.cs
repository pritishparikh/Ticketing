using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class OrderService : IOrder 
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public OrderService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Get OrderBy Number
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        public OrderMaster getOrderbyNumber(string OrderNumber)
        {
            OrderMaster orderMasters = new OrderMaster();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getOrderByNumber", conn);
                cmd1.Parameters.AddWithValue("@objOrderNumber", OrderNumber);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                conn.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    orderMasters.OrderMasterID = Convert.ToInt32(dt.Rows[0]["OrderMasterID"]);
                    orderMasters.OrderNumber = Convert.ToString(dt.Rows[0]["OrderNumber"]);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return orderMasters;
        } 

        /// <summary>
        /// Add Order Detail
        /// </summary>
        /// <param name="orderMaster"></param>
        /// <returns></returns>
        public int addOrderDetails(OrderMaster orderMaster)
        {
            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createOrder", conn);
                cmd1.Parameters.AddWithValue("@TenantID", orderMaster.TenantID);
                cmd1.Parameters.AddWithValue("@OrderNumber", orderMaster.OrderNumber);
                cmd1.Parameters.AddWithValue("@BillID", orderMaster.BillID);
                cmd1.Parameters.AddWithValue("@TicketSourceID", orderMaster.TicketSourceID);
                cmd1.Parameters.AddWithValue("@ModeOfPaymentID", orderMaster.ModeOfPaymentID);
                cmd1.Parameters.AddWithValue("@TransactionDate", orderMaster.TransactionDate);
                cmd1.Parameters.AddWithValue("@InvoiceNumber", orderMaster.InvoiceNumber);
                cmd1.Parameters.AddWithValue("@InvoiceDate", orderMaster.InvoiceDate);
                cmd1.Parameters.AddWithValue("@OrderPrice", orderMaster.OrderPrice);
                cmd1.Parameters.AddWithValue("@PricePaid", orderMaster.PricePaid);
                cmd1.Parameters.AddWithValue("@CustomerID", orderMaster.CustomerID);
                cmd1.Parameters.AddWithValue("@PurchaseFromStoreId", orderMaster.PurchaseFromStoreId);
                cmd1.Parameters.AddWithValue("@Discount", orderMaster.Discount);
                cmd1.Parameters.AddWithValue("@Size", orderMaster.Size);
                cmd1.Parameters.AddWithValue("@RequireSize", orderMaster.RequireSize);
                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return i;
        }
    }
}
