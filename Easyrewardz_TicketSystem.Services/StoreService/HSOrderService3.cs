using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class HSOrderService
    {
        /// <summary>
        /// CreateShipmentAWB
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="itemIDs"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public ReturnShipmentDetails CreateShipmentAWB(int orderID, string itemIDs, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            ReturnShipmentDetails obj=null;
            Random generator = new Random();        
            var randomAWB= generator.Next(0, 1000000000).ToString("D10");
            string CourierPartner = "Blue Dart";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYCreateShipmentAWB", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@order_ID", orderID);
                cmd.Parameters.AddWithValue("@item_IDs", string.IsNullOrEmpty(itemIDs) ? "" :itemIDs.TrimEnd(','));
                cmd.Parameters.AddWithValue("@random_AWB", randomAWB); 
                cmd.Parameters.AddWithValue("@_CourierPartner", CourierPartner);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                         obj = new ReturnShipmentDetails
                        {
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            AWBNumber = ds.Tables[0].Rows[i]["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AWBNo"]),
                            ItemIDs = ds.Tables[0].Rows[i]["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemID"]),                            
                        };
                    }
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
            }

            return obj;
        }

        public OrdersItemDetails GetItemDetailByOrderID(int orderID, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<OrdersItem> lstOrdersItem = new List<OrdersItem>();
            OrdersItemDetails ordersItemDetails = new OrdersItemDetails();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetItemDetailByOrderID", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("order_ID", orderID);
                sqlcmd.Parameters.AddWithValue("tenant_ID", tenantID);
                sqlcmd.Parameters.AddWithValue("user_ID", userID);
     
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrdersItem ordersItems = new OrdersItem
                        {
                           ID= ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                           ItemID= ds.Tables[0].Rows[i]["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemID"]),
                           ItemName= ds.Tables[0].Rows[i]["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]),
                           ItemPrice= ds.Tables[0].Rows[i]["ItemPrice"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemPrice"]),
                           Quantity= ds.Tables[0].Rows[i]["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]),
                           OrderID= ds.Tables[0].Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"]),
                           Disable= ds.Tables[0].Rows[i]["Disable"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Disable"]),
                        };

                        lstOrdersItem.Add(ordersItems);
                    }
                    ordersItemDetails.OrdersItems = lstOrdersItem;             
                }
                if (ds != null && ds.Tables[1] != null)
                {
                    ordersItemDetails.InvoiceNumber = ds.Tables[1].Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
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
            }
            return ordersItemDetails;
        }

        public List<ReturnShipmentDetails> GetAWBInvoicenoDetails(int orderID, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<ReturnShipmentDetails> lstReturnShipmentDetails = new List<ReturnShipmentDetails>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetAWBInvoiceDetails", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("order_ID", orderID);
                sqlcmd.Parameters.AddWithValue("tenant_ID", tenantID);
                sqlcmd.Parameters.AddWithValue("user_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ReturnShipmentDetails returnShipmentDetails = new ReturnShipmentDetails
                        {
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            ItemIDs= ds.Tables[0].Rows[i]["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemID"]),
                            AWBNumber= ds.Tables[0].Rows[i]["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AWBNo"]),
                        };

                        lstReturnShipmentDetails.Add(returnShipmentDetails);
                    }
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
            }
            return lstReturnShipmentDetails;
        }
    }
}
