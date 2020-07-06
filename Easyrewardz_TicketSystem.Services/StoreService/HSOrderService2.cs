using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
                            CountSendPaymentLink = ds.Tables[0].Rows[i]["CountSendPaymentLink"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CountSendPaymentLink"]),
                            StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]),
                            DisablePaymentlinkbutton = ds.Tables[0].Rows[i]["DisablePaymentlinkbutton"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["DisablePaymentlinkbutton"]),
                            ShowPaymentLinkPopup = ds.Tables[0].Rows[i]["ShowPaymentLinkPopup"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["ShowPaymentLinkPopup"]),
                            SourceOfOrder = ds.Tables[0].Rows[i]["SourceOfOrder"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SourceOfOrder"]),
                            PaymentBillDate= ds.Tables[0].Rows[i]["PaymentBillDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PaymentBillDate"]),
                            OrdersItemList = new List<OrdersItem>(),
                            ShoppingBagItemList = new List<ShoppingBagItem>()
                        };


                        obj.OrdersItemList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                        {
                            ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                            Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity")

                        }).ToList();

                        obj.ShoppingBagItemList = ds.Tables[2].AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
                        {
                            ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                            Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity")

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
                            ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                            Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity")

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
                            CourierPartner = ds.Tables[0].Rows[i]["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CourierPartner"]),
                            OrdersItemList = new List<OrdersItem>(),
                            ShoppingBagItemList = new List<ShoppingBagItem>()
                        };


                        obj.OrdersItemList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                        {
                            ID = x.Field<object>("ID") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<int>("ID")),
                            ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                            Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity")

                        }).ToList();

                        obj.ShoppingBagItemList = ds.Tables[2].AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
                        {
                            ID = x.Field<object>("ID") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<int>("ID")),
                            ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                            Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity")

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
        /// GetOrderTabSettingDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public OrderTabSetting GetOrderTabSettingDetails(int tenantId, int userId)
        {
            DataSet ds = new DataSet();
            OrderTabSetting objdetails = new OrderTabSetting();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderTabSettingDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    objdetails = new OrderTabSetting
                    {
                        PaymentVisible = ds.Tables[0].Rows[0]["Payment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Payment"]),
                        ShoppingBagVisible = ds.Tables[0].Rows[0]["ShoppingBag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["ShoppingBag"]),
                        ShipmentVisible = ds.Tables[0].Rows[0]["Shipment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Shipment"]),
                        StoreDelivery = ds.Tables[0].Rows[0]["StoreDelivery"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["StoreDelivery"]),
                        Exists = ds.Tables[0].Rows[0]["Exists"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["Exists"])
                    };
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
            return objdetails;
        }

        /// <summary>
        /// OrderHasBeenReturn
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int SetOrderHasBeenReturn(int tenantId, int userId, int orderID, string Returnby = "Order")
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYSetOrderHasBeenReturn", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_OrderID", orderID);
                cmd.Parameters.AddWithValue("@_Returnby", Returnby);

                cmd.CommandType = CommandType.StoredProcedure;
                UpdateCount = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return UpdateCount;
        }

        /// <summary>
        /// Sms Whats Up Data Send
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="orderId"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="sMSWhtappTemplate"></param>
        /// <returns></returns>
        public int SmsWhatsUpDataSend(int tenantId, int userId, string ProgramCode, int orderId, string ClientAPIURL, string sMSWhtappTemplate)
        {
            int result = 0;
            DataSet ds = new DataSet();
            OrdersSmsWhatsUpDataDetails ordersSmsWhatsUpDataDetails = new OrdersSmsWhatsUpDataDetails();
            //try
            //{

            //    GetWhatsappMessageDetailsResponse getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();

            //    string strpostionNumber = "";
            //    string strpostionName = "";
            //    string additionalInfo = "";
            //    try
            //    {
            //        GetWhatsappMessageDetailsModal getWhatsappMessageDetailsModal = new GetWhatsappMessageDetailsModal()
            //        {
            //            ProgramCode = ProgramCode
            //        };

            //        string apiBotReq = JsonConvert.SerializeObject(getWhatsappMessageDetailsModal);
            //        string apiBotResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetWhatsappMessageDetails", apiBotReq);

            //        if (!string.IsNullOrEmpty(apiBotResponse.Replace("[]", "").Replace("[", "").Replace("]", "")))
            //        {
            //            getWhatsappMessageDetailsResponse = JsonConvert.DeserializeObject<GetWhatsappMessageDetailsResponse>(apiBotResponse.Replace("[", "").Replace("]", ""));
            //        }

            //        if (getWhatsappMessageDetailsResponse != null)
            //        {
            //            if (getWhatsappMessageDetailsResponse.Remarks != null && getWhatsappMessageDetailsResponse.Remarks != "")
            //            {
            //                string ObjRemark = getWhatsappMessageDetailsResponse.Remarks.Replace("\r\n", "");
            //                string[] ObjSplitComma = ObjRemark.Split(',');

            //                if (ObjSplitComma.Length > 0)
            //                {
            //                    for (int i = 0; i < ObjSplitComma.Length; i++)
            //                    {
            //                        strpostionNumber += ObjSplitComma[i].Split('-')[0].Trim().Replace("{", "").Replace("}", "") + ",";
            //                        strpostionName += ObjSplitComma[i].Split('-')[1].Trim() + ",";
            //                    }
            //                }

            //                strpostionNumber = strpostionNumber.TrimEnd(',');
            //                strpostionName = strpostionName.TrimEnd(',');
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
            //    }

            //    MySqlCommand cmd = new MySqlCommand("SP_PHYGetSmsWhatsUpDataDetails", conn)
            //    {
            //        CommandType = CommandType.StoredProcedure
            //    };
            //    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
            //    cmd.Parameters.AddWithValue("@_UserID", userId);
            //    cmd.Parameters.AddWithValue("@_OrderID", orderId);
            //    cmd.Parameters.AddWithValue("@_strpostionNumber", strpostionNumber);
            //    cmd.Parameters.AddWithValue("@_strpostionName", strpostionName);
            //    cmd.Parameters.AddWithValue("@_sMSWhtappTemplate", sMSWhtappTemplate);

            //    MySqlDataAdapter da = new MySqlDataAdapter
            //    {
            //        SelectCommand = cmd
            //    };
            //    da.Fill(ds);

            //    if (ds != null && ds.Tables[0] != null)
            //    {
            //        ordersSmsWhatsUpDataDetails = new OrdersSmsWhatsUpDataDetails()
            //        {
            //            OderID = ds.Tables[0].Rows[0]["OderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["OderID"]),
            //            AlertCommunicationviaWhtsup = ds.Tables[0].Rows[0]["AlertCommunicationviaWhtsup"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["AlertCommunicationviaWhtsup"]),
            //            AlertCommunicationviaSMS = ds.Tables[0].Rows[0]["AlertCommunicationviaSMS"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["AlertCommunicationviaSMS"]),
            //            SMSSenderName = ds.Tables[0].Rows[0]["SMSSenderName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["SMSSenderName"]),
            //            IsSend = ds.Tables[0].Rows[0]["IsSend"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSend"]),
            //            MessageText = ds.Tables[0].Rows[0]["MessageText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MessageText"]),
            //            InvoiceNo = ds.Tables[0].Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]),
            //            MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"]),
            //            AdditionalInfo = ds.Tables[1].Rows[0]["additionalInfo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["additionalInfo"]),
            //        };
            //        // result = ds.Tables[0].Rows[0]["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ChatID"]);
            //        // Message = ds.Tables[0].Rows[0]["Message"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
            //        // additionalInfo = ds.Tables[0].Rows[0]["additionalInfo"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["additionalInfo"]);
            //    }



            //    if (ordersSmsWhatsUpDataDetails.AlertCommunicationviaWhtsup)
            //    {
            //        try
            //        {
            //            List<string> additionalList = new List<string>();
            //            if (additionalInfo != null)
            //            {
            //                additionalList = ordersSmsWhatsUpDataDetails.AdditionalInfo.Split(",").ToList();
            //            }
            //            SendFreeTextRequest sendFreeTextRequest = new SendFreeTextRequest
            //            {
            //                To = ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0').Length > 10 ? ordersSmsWhatsUpDataDetails.MobileNumber : "91" + ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0'),
            //                ProgramCode = ProgramCode,
            //                TemplateName = getWhatsappMessageDetailsResponse.TemplateName,
            //                AdditionalInfo = additionalList
            //            };

            //            string apiReq = JsonConvert.SerializeObject(sendFreeTextRequest);
            //            apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendCampaign", apiReq);

                       
            //            //if (apiResponse.Equals("true"))
            //            //{
            //            //    UpdateResponseShare(objRequest.CustomerID, "Contacted Via Chatbot");
            //            //}
            //        }
            //        catch (Exception)
            //        {
            //            throw;
            //        }
            //    }
            //    else if (ordersSmsWhatsUpDataDetails.AlertCommunicationviaSMS)
            //    {
            //        if(ordersSmsWhatsUpDataDetails.IsSend)
            //        {
            //            Message = ordersSmsWhatsUpDataDetails.MessageText;
            //        }
            //        //else if (sMSWhtappTemplate == "AWBAssigned" & ordersSmsWhatsUpDataDetails.AWBAssigned)
            //        //{
            //        //    Message = ordersSmsWhatsUpDataDetails.AWBAssignedText;
            //        //}
            //        //else if (sMSWhtappTemplate == "PickupScheduled" & ordersSmsWhatsUpDataDetails.PickupScheduled)
            //        //{
            //        //    Message = ordersSmsWhatsUpDataDetails.PickupScheduledText;
            //        //}
            //        //else if (sMSWhtappTemplate == "Shipped" & ordersSmsWhatsUpDataDetails.Shipped)
            //        //{
            //        //    Message = ordersSmsWhatsUpDataDetails.ShippedText;
            //        //}
            //        //else if (sMSWhtappTemplate == "Delivered" & ordersSmsWhatsUpDataDetails.Delivered)
            //        //{
            //        //    Message = ordersSmsWhatsUpDataDetails.DeliveredText;
            //        //}
                    

            //        ChatSendSMS chatSendSMS = new ChatSendSMS
            //        {
            //            MobileNumber = ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0').Length > 10 ? ordersSmsWhatsUpDataDetails.MobileNumber : "91" + ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0'),
            //            SenderId = ordersSmsWhatsUpDataDetails.SMSSenderName,
            //            SmsText = Message
            //        };

            //        string apiReq = JsonConvert.SerializeObject(chatSendSMS);
            //        apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendSMS", apiReq);

            //        ChatSendSMSResponse chatSendSMSResponse = new ChatSendSMSResponse();

            //        chatSendSMSResponse = JsonConvert.DeserializeObject<ChatSendSMSResponse>(apiResponse);

            //        if (chatSendSMSResponse != null)
            //        {
            //            result = chatSendSMSResponse.Id;
            //        }

            //        //if (result > 0)
            //        //{
            //        //    UpdateResponseShare(objRequest.CustomerID, "Contacted Via SMS");
            //        //}
            //    }

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    if (conn != null)
            //    {
            //        conn.Close();
            //    }
            //    if (ds != null)
            //    {
            //        ds.Dispose();
            //    }
            //}

            return result;
        }

        /// <summary>
        /// GetOrderShippingTemplate
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shippingTemplateRequest"></param>
        /// <returns></returns>
        public ShippingTemplateDetails GetOrderShippingTemplate(int tenantId, int userId, ShippingTemplateRequest shippingTemplateRequest)
        {
            DataSet ds = new DataSet();
            ShippingTemplateDetails objdetails = new ShippingTemplateDetails();

            List<ShippingTemplate> orderlist = new List<ShippingTemplate>();
            int TotalCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderShippingTemplate", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_pageno", shippingTemplateRequest.PageNo);
                cmd.Parameters.AddWithValue("@_pagesize", shippingTemplateRequest.PageSize);
                cmd.Parameters.AddWithValue("@_FilterStatus", shippingTemplateRequest.FilterStatus);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ShippingTemplate obj = new ShippingTemplate
                        {
                            ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            TemplateName = ds.Tables[0].Rows[i]["TemplateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TemplateName"]),
                            Height = ds.Tables[0].Rows[i]["Height"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Height"]),
                            Height_Unit = ds.Tables[0].Rows[i]["Height_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Height_Unit"]),
                            Length = ds.Tables[0].Rows[i]["Length"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Length"]),
                            Length_Unit = ds.Tables[0].Rows[i]["Length_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Length_Unit"]),
                            Breath = ds.Tables[0].Rows[i]["Breath"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Breath"]),
                            Breath_Unit = ds.Tables[0].Rows[i]["Breath_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Breath_Unit"]),
                            Weight = ds.Tables[0].Rows[i]["Weight"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Weight"]),
                            Weight_Unit = ds.Tables[0].Rows[i]["Weight_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Weight_Unit"]),
                            CreatedOn = ds.Tables[0].Rows[i]["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]),
                            ModifiedOn = ds.Tables[0].Rows[i]["ModifiedOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedOn"]),
                            Createdby = ds.Tables[0].Rows[i]["Createdby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Createdby"]),
                            Modifiedby = ds.Tables[0].Rows[i]["Modifiedby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Modifiedby"])
                        };
                        orderlist.Add(obj);
                    }
                }

                if (ds != null && ds.Tables[1] != null)
                {
                    TotalCount = ds.Tables[1].Rows[0]["TotalCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
                }

                objdetails.ShippingTemplateList = orderlist;
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
        /// InsertOrderShippingTemplate
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="addEditShippingTemplate"></param>
        /// <returns></returns>
        public int InsertUpdateOrderShippingTemplate(int tenantID, int userID, AddEditShippingTemplate addEditShippingTemplate)
        {
            int result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SP_PHYInsertOrderShippingTemplate", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                cmd.Parameters.AddWithValue("@_UserID", userID);
                cmd.Parameters.AddWithValue("@_ID", addEditShippingTemplate.ID);
                cmd.Parameters.AddWithValue("@_TemplateName", addEditShippingTemplate.TemplateName);
                cmd.Parameters.AddWithValue("@_Height", addEditShippingTemplate.Height);
                cmd.Parameters.AddWithValue("@_Height_Unit", addEditShippingTemplate.Height_Unit);
                cmd.Parameters.AddWithValue("@_Length", addEditShippingTemplate.Length);
                cmd.Parameters.AddWithValue("@_Length_Unit", addEditShippingTemplate.Length_Unit);
                cmd.Parameters.AddWithValue("@_Breath", addEditShippingTemplate.Breath);
                cmd.Parameters.AddWithValue("@_Breath_Unit", addEditShippingTemplate.Breath_Unit);
                cmd.Parameters.AddWithValue("@_Weight", addEditShippingTemplate.Weight);
                cmd.Parameters.AddWithValue("@_Weight_Unit", addEditShippingTemplate.Weight_Unit);
                result = Convert.ToInt32(cmd.ExecuteScalar());
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
            return result;
        }

        /// <summary>
        /// GetOrderShippingTemplateName
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ShippingTemplateDetails GetOrderShippingTemplateName(int tenantId, int userId)
        {
            DataSet ds = new DataSet();
            ShippingTemplateDetails objdetails = new ShippingTemplateDetails();

            List<ShippingTemplate> orderlist = new List<ShippingTemplate>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderShippingTemplateName", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ShippingTemplate obj = new ShippingTemplate
                        {
                            ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            TemplateName = ds.Tables[0].Rows[i]["TemplateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TemplateName"]),
                            Height = ds.Tables[0].Rows[i]["Height"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Height"]),
                            Height_Unit = ds.Tables[0].Rows[i]["Height_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Height_Unit"]),
                            Length = ds.Tables[0].Rows[i]["Length"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Length"]),
                            Length_Unit = ds.Tables[0].Rows[i]["Length_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Length_Unit"]),
                            Breath = ds.Tables[0].Rows[i]["Breath"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Breath"]),
                            Breath_Unit = ds.Tables[0].Rows[i]["Breath_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Breath_Unit"]),
                            Weight = ds.Tables[0].Rows[i]["Weight"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Weight"]),
                            Weight_Unit = ds.Tables[0].Rows[i]["Weight_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Weight_Unit"]),
                        };
                        orderlist.Add(obj);
                    }
                }
                objdetails.ShippingTemplateList = orderlist;
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
