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
                            PaymentLink = ds.Tables[0].Rows[i]["PaymentLink"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PaymentLink"]),
                            ModeOfPayment = ds.Tables[0].Rows[i]["ModeOfPayment"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModeOfPayment"]),
                            PaymentVia = ds.Tables[0].Rows[i]["PaymentVia"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PaymentVia"]),
                            TotalAmount = ds.Tables[0].Rows[i]["TotalAmount"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TotalAmount"]),
                            OrdersItemList = new List<OrdersItem>(),
                            ShoppingBagItemList = new List<ShoppingBagItem>()
                        };


                        obj.OrdersItemList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                        {
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = Convert.ToString(x.Field<string>("ItemPrice")),
                            Quantity = x.Field<int>("Quantity")

                        }).ToList();

                        obj.ShoppingBagItemList = ds.Tables[2].AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
                        {
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = Convert.ToString(x.Field<string>("ItemPrice")),
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

        /// <summary>
        /// GetShoppingBagDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public ShoppingBagResponseDetails GetShoppingBagDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            DataSet ds = new DataSet();
            ShoppingBagResponseDetails objdetails = new ShoppingBagResponseDetails();

            List<ShoppingBag> ShoppingBaglist = new List<ShoppingBag>();
            int TotalCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetShoppingBagDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_SearchText", ordersDataRequest.SearchText);
                cmd.Parameters.AddWithValue("@_pageno", ordersDataRequest.PageNo);
                cmd.Parameters.AddWithValue("@_pagesize", ordersDataRequest.PageSize);
                cmd.Parameters.AddWithValue("@_FilterStatus", ordersDataRequest.FilterStatus);
                cmd.Parameters.AddWithValue("@_FilterDeliveryType", ordersDataRequest.FilterDelivery);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ShoppingBag obj = new ShoppingBag
                        {
                            ShoppingID = ds.Tables[0].Rows[i]["ShoppingID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ShoppingID"]),
                            ShoppingBagNo = ds.Tables[0].Rows[i]["ShoppingBagNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShoppingBagNo"]),
                            Date = ds.Tables[0].Rows[i]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Date"]),
                            Time = ds.Tables[0].Rows[i]["Time"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Time"]),
                            CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]),
                            MobileNumber = ds.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]),
                            Status = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Status"]),
                            StatusName = ds.Tables[0].Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StatusName"]),
                            DeliveryTypeName = ds.Tables[0].Rows[i]["DeliveryTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DeliveryTypeName"]),
                            PickupDate = ds.Tables[0].Rows[i]["PickupDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PickupDate"]),
                            PickupTime = ds.Tables[0].Rows[i]["PickupTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PickupTime"]),
                            Address = ds.Tables[0].Rows[i]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Address"]),
                            ActionTypeName = ds.Tables[0].Rows[i]["ActionTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ActionTypeName"]),
                            Action = ds.Tables[0].Rows[i]["Action"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Action"]),
                            IsCanceled = ds.Tables[0].Rows[i]["IsCanceled"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsCanceled"]),
                            CanceledOn = ds.Tables[0].Rows[i]["CanceledOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CanceledOn"]),
                            UserName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserName"]),
                            CanceledComment = ds.Tables[0].Rows[i]["CanceledComment"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CanceledComment"]),
                            ShoppingBagItemList = new List<ShoppingBagItem>()
                        };


                        obj.ShoppingBagItemList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
                        {
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = Convert.ToString(x.Field<string>("ItemPrice")),
                            Quantity = x.Field<int>("Quantity")

                        }).ToList();

                        ShoppingBaglist.Add(obj);
                    }
                }

                if (ds != null && ds.Tables[2] != null)
                {
                    TotalCount = ds.Tables[2].Rows[0]["TotalShoppingBag"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[2].Rows[0]["TotalShoppingBag"]);
                }

                objdetails.ShoppingBagList = ShoppingBaglist;
                objdetails.TotalShoppingBag = TotalCount;
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

        /// <summary>
        /// GetShoppingBagDeliveryType
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public List<ShoppingBagDeliveryFilter> GetShoppingBagDeliveryType(int tenantId, int userId, int pageID)
        {
            DataSet ds = new DataSet();
            List<ShoppingBagDeliveryFilter> shoppingBagDeliveryFilter = new List<ShoppingBagDeliveryFilter>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYShoppingBagDeliveryType", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_PageID", pageID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ShoppingBagDeliveryFilter obj = new ShoppingBagDeliveryFilter
                        {
                            DeliveryTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["DeliveryTypeID"]),
                            DeliveryTypeName = Convert.ToString(ds.Tables[0].Rows[i]["DeliveryTypeName"])
                        };
                        shoppingBagDeliveryFilter.Add(obj);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return shoppingBagDeliveryFilter;
        }

        /// <summary>
        /// GetShipmentDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public OrderResponseDetails GetShipmentDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            DataSet ds = new DataSet();
            OrderResponseDetails objdetails = new OrderResponseDetails();

            List<Orders> orderlist = new List<Orders>();
            int TotalCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetShipmentDetails", conn)
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
                            DeliveryType = ds.Tables[0].Rows[i]["DeliveryType"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DeliveryType"]),
                            DeliveryTypeName = ds.Tables[0].Rows[i]["DeliveryTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DeliveryTypeName"]),
                            PickupDate = ds.Tables[0].Rows[i]["PickupDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PickupDate"]),
                            PickupTime = ds.Tables[0].Rows[i]["PickupTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PickupTime"]),
                            OrdersItemList = new List<OrdersItem>(),
                            ShoppingBagItemList = new List<ShoppingBagItem>()
                        };


                        obj.OrdersItemList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                        {
                            ID = Convert.ToInt32(x.Field<int>("ID")),
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = Convert.ToString(x.Field<string>("ItemPrice")),
                            Quantity = x.Field<int>("Quantity")

                        }).ToList();

                        obj.ShoppingBagItemList = ds.Tables[2].AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
                        {
                            ID = Convert.ToInt32(x.Field<int>("ID")),
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = Convert.ToString(x.Field<string>("ItemPrice")),
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
