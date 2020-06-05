using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class HSOrderService : IHSOrder
    {
        /// <summary>
        /// GetOrdersDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public OrderResponseDetails GetOrdersDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            DataSet ds = new DataSet();
            OrderResponseDetails objdetails = new OrderResponseDetails();

            List<Orders> orderlist = new List<Orders>();
            int TotalCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrdersDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_SearchText", ordersDataRequest.SearchText);
                cmd.Parameters.AddWithValue("@_pageno", ordersDataRequest.PageNo);
                cmd.Parameters.AddWithValue("@_pagesize", ordersDataRequest.PageSize);
                cmd.Parameters.AddWithValue("@_FilterStatus", ordersDataRequest.FilterStatus);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Orders obj = new Orders
                        {
                            ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]),
                            MobileNumber = ds.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]),
                            Amount = ds.Tables[0].Rows[i]["Amount"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Amount"]),
                            Date = ds.Tables[0].Rows[i]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Date"]),
                            Time = ds.Tables[0].Rows[i]["Time"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Time"]),
                            StatusName = ds.Tables[0].Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StatusName"]),
                            ShippingAddress = ds.Tables[0].Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShippingAddress"]),
                            ActionTypeName = ds.Tables[0].Rows[i]["ActionTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ActionTypeName"]),
                            IsShoppingBagConverted = ds.Tables[0].Rows[i]["IsShoppingBagConverted"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsShoppingBagConverted"]),
                            ShoppingID = ds.Tables[0].Rows[i]["ShoppingID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ShoppingID"]),
                            ShoppingBagNo = ds.Tables[0].Rows[i]["ShoppingBagNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShoppingBagNo"]),
                            OrdersItemList = new List<OrdersItem>()
                        };


                        obj.OrdersItemList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                        {
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<double>("ItemPrice"),
                            Quantity = x.Field<int>("Quantity")

                        }).ToList();

                        obj.ShoppingBagItemList = ds.Tables[2].AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
                        {
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<double>("ItemPrice"),
                            Quantity = x.Field<int>("Quantity")

                        }).ToList();

                        orderlist.Add(obj);
                    }
                }

                if (ds != null && ds.Tables[3] != null)
                {
                    TotalCount = ds.Tables[3].Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[3].Rows[0]["TotalOrder"]);
                }

                objdetails.OrdersList = orderlist;
                objdetails.TotalCount = TotalCount;
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
            return objdetails;
        }

    }
}
