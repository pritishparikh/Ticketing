using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

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
        public OrderMaster getOrderbyNumber(string OrderNumber,int TenantId)
        {
            OrderMaster orderMasters = new OrderMaster();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getOrderByNumber", conn);
                cmd1.Parameters.AddWithValue("@objOrderNumber", OrderNumber);
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantId);
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
        public int addOrderDetails(OrderMaster orderMaster,int TenantId)
        {
            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createOrder", conn);
                cmd1.Parameters.AddWithValue("@TenantID", TenantId);
                cmd1.Parameters.AddWithValue("@ProductBarCode", orderMaster.ProductBarCode);
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
                cmd1.Parameters.AddWithValue("@CreatedBy", orderMaster.CreatedBy);
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
        /// <summary>
        /// Get Order Detail with Item List
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<CustomOrderMaster> getOrderListwithItemDetail(string OrderNumber, int TenantID)
        {

            DataSet ds = new DataSet();
            List<CustomOrderMaster> objorderMaster = new List<CustomOrderMaster>();
            try
            {
                conn.Open();
                MySqlCommand cmd  = new MySqlCommand("SP_OrderDetails", conn);
                cmd.Connection = conn;        
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Order_ID", OrderNumber);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            
                if (ds != null && ds.Tables[0] != null)
                {              
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomOrderMaster customOrderMaster = new CustomOrderMaster();
                        customOrderMaster.OrderMasterID = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customOrderMaster.InvoiceNumber = Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNumber"]);
                        customOrderMaster.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]);
                        customOrderMaster.StoreCode = Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        customOrderMaster.StoreAddress = Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                        customOrderMaster.Discount = Convert.ToInt32(ds.Tables[0].Rows[i]["Discount"]);
                        int orderMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customOrderMaster.OrderItems = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("OrderMasterID")).
                        Equals(orderMasterId)).Select(x => new OrderItem()
                        {
                            OrderItemID= Convert.ToInt32(x.Field<int>("OrderItemID")),
                            OrderMasterID = Convert.ToInt32(x.Field<int>("OrderMasterID")),
                            InvoiceNo = Convert.ToString(x.Field<string>("InvoiceNo")),
                            Size = Convert.ToInt32(x.Field<int>("Size")),
                            ItemPrice = Convert.ToInt32(x.Field<decimal>("ItemPrice")),
                            PricePaid = Convert.ToInt32(x.Field<decimal>("PricePaid")),
                            Discount = Convert.ToInt32(x.Field<decimal>("Discount")),
                            RequireSize = Convert.ToInt32(x.Field<int>("RequireSize"))                       
                        }).ToList();
                        customOrderMaster.ItemCount = customOrderMaster.OrderItems.Count();
                        customOrderMaster.ItemPrice = customOrderMaster.OrderItems.Sum(item => item.ItemPrice);
                        customOrderMaster.PricePaid= customOrderMaster.OrderItems.Sum(item => item.PricePaid);
                        objorderMaster.Add(customOrderMaster);
                    }
                    
                }
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
            return objorderMaster;
        }


    }
}
