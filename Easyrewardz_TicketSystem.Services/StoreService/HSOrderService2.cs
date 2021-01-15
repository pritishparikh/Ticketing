using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class HSOrderService : IHSOrder
    {
        public static string Xpath = "//NewDataSet//Table1";

        /// <summary>
        /// GetOrdersDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public async Task<OrderResponseDetails> GetOrdersDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails objdetails = new OrderResponseDetails();
            List<Orders> orderlist = new List<Orders>();
            int totalCount = 0;
            DataTable ordersTable = new DataTable();
            DataTable ordersItemTable = new DataTable();
            DataTable shoppingBagItemTable = new DataTable();
            DataTable totalTable = new DataTable();
            DataTable showItemPropertyTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            ordersTable.Load(reader);
                            ordersItemTable.Load(reader);
                            shoppingBagItemTable.Load(reader);
                            totalTable.Load(reader);
                            showItemPropertyTable.Load(reader);
                        }
                    }
                    if (ordersTable != null)
                    {
                        for (int i = 0; i < ordersTable.Rows.Count; i++)
                        {
                            Orders obj = new Orders
                            {
                                ID = ordersTable.Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["ID"]),
                                InvoiceNo = ordersTable.Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["InvoiceNo"]),
                                CustomerName = ordersTable.Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["CustomerName"]),
                                MobileNumber = ordersTable.Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["MobileNumber"]),
                                Amount = ordersTable.Rows[i]["Amount"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Amount"]),
                                Date = ordersTable.Rows[i]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Date"]),
                                Time = ordersTable.Rows[i]["Time"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Time"]),
                                StatusName = ordersTable.Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["StatusName"]),
                                ShippingAddress = ordersTable.Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShippingAddress"]),
                                ActionTypeName = ordersTable.Rows[i]["ActionTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ActionTypeName"]),
                                IsShoppingBagConverted = ordersTable.Rows[i]["IsShoppingBagConverted"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["IsShoppingBagConverted"]),
                                ShoppingID = ordersTable.Rows[i]["ShoppingID"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["ShoppingID"]),
                                ShoppingBagNo = ordersTable.Rows[i]["ShoppingBagNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShoppingBagNo"]),
                                POSGenratedInvoiceNo = ordersTable.Rows[i]["POSGenratedInvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["POSGenratedInvoiceNo"]),
                                PaymentLink = ordersTable.Rows[i]["PaymentLink"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PaymentLink"]),
                                ModeOfPayment = ordersTable.Rows[i]["ModeOfPayment"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ModeOfPayment"]),
                                PaymentVia = ordersTable.Rows[i]["PaymentVia"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PaymentVia"]),
                                TotalAmount = ordersTable.Rows[i]["TotalAmount"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["TotalAmount"]),
                                CountSendPaymentLink = ordersTable.Rows[i]["CountSendPaymentLink"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["CountSendPaymentLink"]),
                                StoreCode = ordersTable.Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["StoreCode"]),
                                DisablePaymentlinkbutton = ordersTable.Rows[i]["DisablePaymentlinkbutton"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["DisablePaymentlinkbutton"]),
                                ShowPaymentLinkPopup = ordersTable.Rows[i]["ShowPaymentLinkPopup"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["ShowPaymentLinkPopup"]),
                                SourceOfOrder = ordersTable.Rows[i]["SourceOfOrder"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["SourceOfOrder"]),
                                PaymentBillDate = ordersTable.Rows[i]["PaymentBillDate"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PaymentBillDate"]),
                                OrdersItemList = new List<OrdersItem>(),
                                ShoppingBagItemList = new List<ShoppingBagItem>()
                            };

                            obj.OrdersItemList = ordersItemTable.AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                            {
                                ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                                ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                                ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                                Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity"),
                                ShowItemProperty = x.Field<object>("ShowItemProperty") == DBNull.Value ? false : Convert.ToBoolean(x.Field<bool>("ShowItemProperty"))

                            }).ToList();

                            obj.ShoppingBagItemList = shoppingBagItemTable.AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
                            {
                                ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                                ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                                ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                                Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity")

                            }).ToList();

                            obj.ShowItemProperty = showItemPropertyTable.Rows[0]["ShowItemProperty"] == DBNull.Value ? false : Convert.ToBoolean(showItemPropertyTable.Rows[0]["ShowItemProperty"]);

                            orderlist.Add(obj);
                        }
                    }
                    if (totalTable != null)
                    {
                        if (totalTable.Rows.Count > 0)
                        {
                            totalCount = totalTable.Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(totalTable.Rows[0]["TotalOrder"]);
                        }
                    }

                    objdetails.OrdersList = orderlist;
                    objdetails.TotalCount = totalCount;
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
                if (ordersTable != null)
                {
                    ordersTable.Dispose();
                }
                if (ordersItemTable != null)
                {
                    ordersItemTable.Dispose();
                }
                if (shoppingBagItemTable != null)
                {
                    shoppingBagItemTable.Dispose();
                }
                if (totalTable != null)
                {
                    totalTable.Dispose();
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
        public async Task<ShoppingBagResponseDetails> GetShoppingBagDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            ShoppingBagResponseDetails objdetails = new ShoppingBagResponseDetails();
            List<ShoppingBag> ShoppingBaglist = new List<ShoppingBag>();
            int totalCount = 0;
            DataTable shoppingBagTable = new DataTable();
            DataTable shoppingBagItemTable = new DataTable();
            DataTable posShoppingBagStatusTable = new DataTable();
            DataTable totalCountTable = new DataTable();
            DataTable showSettingTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            shoppingBagTable.Load(reader);
                            shoppingBagItemTable.Load(reader);
                            posShoppingBagStatusTable.Load(reader);
                            totalCountTable.Load(reader);
                            showSettingTable.Load(reader);
                        }
                    }

                    if (shoppingBagTable != null)
                    {
                        if (shoppingBagTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < shoppingBagTable.Rows.Count; i++)
                            {
                                ShoppingBag obj = new ShoppingBag
                                {
                                    ShoppingID = shoppingBagTable.Rows[i]["ShoppingID"] == DBNull.Value ? 0 : Convert.ToInt32(shoppingBagTable.Rows[i]["ShoppingID"]),
                                    ShoppingBagNo = shoppingBagTable.Rows[i]["ShoppingBagNo"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["ShoppingBagNo"]),
                                    Date = shoppingBagTable.Rows[i]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["Date"]),
                                    Time = shoppingBagTable.Rows[i]["Time"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["Time"]),
                                    CustomerName = shoppingBagTable.Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["CustomerName"]),
                                    MobileNumber = shoppingBagTable.Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["MobileNumber"]),
                                    Status = shoppingBagTable.Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["Status"]),
                                    StatusName = shoppingBagTable.Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["StatusName"]),
                                    DeliveryTypeName = shoppingBagTable.Rows[i]["DeliveryTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["DeliveryTypeName"]),
                                    PickupDate = shoppingBagTable.Rows[i]["PickupDate"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["PickupDate"]),
                                    PickupTime = shoppingBagTable.Rows[i]["PickupTime"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["PickupTime"]),
                                    Address = shoppingBagTable.Rows[i]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["Address"]),
                                    ActionTypeName = shoppingBagTable.Rows[i]["ActionTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["ActionTypeName"]),
                                    Action = shoppingBagTable.Rows[i]["Action"] == DBNull.Value ? 0 : Convert.ToInt32(shoppingBagTable.Rows[i]["Action"]),
                                    IsCanceled = shoppingBagTable.Rows[i]["IsCanceled"] == DBNull.Value ? false : Convert.ToBoolean(shoppingBagTable.Rows[i]["IsCanceled"]),
                                    CanceledOn = shoppingBagTable.Rows[i]["CanceledOn"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["CanceledOn"]),
                                    UserName = shoppingBagTable.Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["UserName"]),
                                    CanceledComment = shoppingBagTable.Rows[i]["CanceledComment"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["CanceledComment"]),
                                    IsPushToPoss = shoppingBagTable.Rows[i]["IsPushToPoss"] == DBNull.Value ? false : Convert.ToBoolean(shoppingBagTable.Rows[i]["IsPushToPoss"]),
                                    IsPosPushed = shoppingBagTable.Rows[i]["IsPosPushed"] == DBNull.Value ? false : Convert.ToBoolean(shoppingBagTable.Rows[i]["IsPosPushed"]),
                                    StoreCode = shoppingBagTable.Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(shoppingBagTable.Rows[i]["StoreCode"]),
                                    ShoppingBagItemList = new List<ShoppingBagItem>(),
                                    PosShoppingBagStatusList = new List<PosShoppingBagStatus>()
                                };


                                obj.ShoppingBagItemList = shoppingBagItemTable.AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
                                {
                                    ID = x.Field<object>("ID") == DBNull.Value ? 0 : x.Field<int>("ID"),
                                    ItemID = x.Field<object>("ItemID") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                                    ItemName = x.Field<object>("ItemName") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                                    ItemPrice = x.Field<object>("ItemPrice") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                                    Quantity = x.Field<object>("Quantity") == DBNull.Value ? 0 : x.Field<int>("Quantity"),
                                    ShowItemProperty = x.Field<object>("ShowItemProperty") == DBNull.Value ? false : x.Field<bool>("ShowItemProperty"),
                                    Availableqty = GetAvailableqty(x.Field<object>("Quantity") == DBNull.Value ? 0 : x.Field<int>("Quantity")),
                                    SelectAvailableqty = x.Field<object>("SelectAvailableqty") == DBNull.Value ? 0 : x.Field<int>("SelectAvailableqty"),
                                    ShowAvailableQuantity = x.Field<object>("ShowAvailableQuantity") == DBNull.Value ? false : x.Field<bool>("ShowAvailableQuantity"),
                                }).ToList();

                                obj.PosShoppingBagStatusList = posShoppingBagStatusTable.AsEnumerable().Where(x => (x.Field<int>("ShoppingBagID")).Equals(obj.ShoppingID)).Select(x => new PosShoppingBagStatus()
                                {
                                    Status = x.Field<object>("Status") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("Status")),
                                    Date = x.Field<object>("Date") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("Date"))
                                }).ToList();

                                obj.ShowItemProperty = showSettingTable.Rows[0]["ShowItemProperty"] == DBNull.Value ? false : Convert.ToBoolean(showSettingTable.Rows[0]["ShowItemProperty"]);
                                obj.ShowAvailableQuantity = showSettingTable.Rows[0]["ShowAvailableQuantity"] == DBNull.Value ? false : Convert.ToBoolean(showSettingTable.Rows[0]["ShowAvailableQuantity"]);

                                ShoppingBaglist.Add(obj);
                            }
                        }
                    }

                    if (totalCountTable != null)
                    {
                        if (totalCountTable.Rows.Count > 0)
                        {
                            totalCount = totalCountTable.Rows[0]["TotalShoppingBag"] == DBNull.Value ? 0 : Convert.ToInt32(totalCountTable.Rows[0]["TotalShoppingBag"]);
                        }
                    }

                    objdetails.ShoppingBagList = ShoppingBaglist;
                    objdetails.TotalShoppingBag = totalCount;
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
                if (shoppingBagTable != null)
                {
                    shoppingBagTable.Dispose();
                }
                if (shoppingBagItemTable != null)
                {
                    shoppingBagItemTable.Dispose();
                }
                if (totalCountTable != null)
                {
                    totalCountTable.Dispose();
                }
                if (showSettingTable != null)
                {
                    showSettingTable.Dispose();
                }
            }
            return objdetails;
        }

        public List<int> GetAvailableqty(int qty)
        {
            List<int> Availableqty = new List<int>();
            try
            {
                for (int i = qty; i >= 0 ; i--)
                {
                    Availableqty.Add(i);
                }
            }
            catch(Exception)
            {
                throw;
            }

            return Availableqty;
        }

        /// <summary>
        /// GetShoppingBagDeliveryType
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public async Task<List<ShoppingBagDeliveryFilter>> GetShoppingBagDeliveryType(int tenantId, int userId, int pageID)
        {
            List<ShoppingBagDeliveryFilter> shoppingBagDeliveryFilter = new List<ShoppingBagDeliveryFilter>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYShoppingBagDeliveryType", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_PageID", pageID);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {

                        while (dr.Read())
                        {
                           ShoppingBagDeliveryFilter obj = new ShoppingBagDeliveryFilter
                           {
                               DeliveryTypeID = Convert.ToInt32(dr["DeliveryTypeID"]),
                               DeliveryTypeName = Convert.ToString(dr["DeliveryTypeName"])
                           };
                           shoppingBagDeliveryFilter.Add(obj);
                        }
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
            return shoppingBagDeliveryFilter;
        }

        /// <summary>
        /// GetShipmentDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public async Task<OrderResponseDetails> GetShipmentDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails objdetails = new OrderResponseDetails();
            List<Orders> orderlist = new List<Orders>();
            int TotalCount = 0;
            DataTable ordersTable = new DataTable();
            DataTable ordersItemTable = new DataTable();
            DataTable shoppingBagItemTable = new DataTable();
            DataTable totalTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    cmd.Parameters.AddWithValue("@_FilterDelivery", ordersDataRequest.FilterDelivery);
                    cmd.Parameters.AddWithValue("@_CourierPartner", ordersDataRequest.CourierPartner);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            ordersTable.Load(reader);
                            ordersItemTable.Load(reader);
                            shoppingBagItemTable.Load(reader);
                            totalTable.Load(reader);
                        }
                    }

                    if (ordersTable != null)
                    {
                        for (int i = 0; i < ordersTable.Rows.Count; i++)
                        {
                            Orders obj = new Orders
                            {
                                ID = ordersTable.Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["ID"]),
                                InvoiceNo = ordersTable.Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["InvoiceNo"]),
                                CustomerName = ordersTable.Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["CustomerName"]),
                                MobileNumber = ordersTable.Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["MobileNumber"]),
                                Amount = ordersTable.Rows[i]["Amount"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Amount"]),
                                Date = ordersTable.Rows[i]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Date"]),
                                Time = ordersTable.Rows[i]["Time"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Time"]),
                                StatusName = ordersTable.Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["StatusName"]),
                                ShippingAddress = ordersTable.Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShippingAddress"]),
                                ActionTypeName = ordersTable.Rows[i]["ActionTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ActionTypeName"]),
                                IsShoppingBagConverted = ordersTable.Rows[i]["IsShoppingBagConverted"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["IsShoppingBagConverted"]),
                                ShoppingID = ordersTable.Rows[i]["ShoppingID"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["ShoppingID"]),
                                ShoppingBagNo = ordersTable.Rows[i]["ShoppingBagNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShoppingBagNo"]),
                                DeliveryType = ordersTable.Rows[i]["DeliveryType"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["DeliveryType"]),
                                DeliveryTypeName = ordersTable.Rows[i]["DeliveryTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["DeliveryTypeName"]),
                                PickupDate = ordersTable.Rows[i]["PickupDate"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PickupDate"]),
                                PickupTime = ordersTable.Rows[i]["PickupTime"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PickupTime"]),
                                CourierPartner = ordersTable.Rows[i]["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["CourierPartner"]),
                                ShippingCharges = ordersTable.Rows[i]["ShippingCharges"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShippingCharges"]),
                                EstimatedDeliveryDate = ordersTable.Rows[i]["EstimatedDeliveryDate"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["EstimatedDeliveryDate"]),
                                PickupScheduledDate = ordersTable.Rows[i]["PickupScheduledDate"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PickupScheduledDate"]),
                                CancelButtonInShipment = ordersTable.Rows[i]["CancelButtonInShipment"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["CancelButtonInShipment"]),
                                ShowShipmentCharges = ordersTable.Rows[i]["ShowShipmentCharges"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["ShowShipmentCharges"]),
                                OrdersItemList = new List<OrdersItem>(),
                                ShoppingBagItemList = new List<ShoppingBagItem>() 
                            };


                            obj.OrdersItemList = ordersItemTable.AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                            {
                                ID = x.Field<object>("ID") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<int>("ID")),
                                ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                                ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                                ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                                Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity")

                            }).ToList();

                            obj.ShoppingBagItemList = shoppingBagItemTable.AsEnumerable().Where(x => (x.Field<int>("ShoppingID")).Equals(obj.ShoppingID)).Select(x => new ShoppingBagItem()
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

                    if (totalTable != null)
                    {
                        if (totalTable.Rows.Count > 0)
                        {
                            TotalCount = totalTable.Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(totalTable.Rows[0]["TotalOrder"]);
                        }
                    }

                    objdetails.OrdersList = orderlist;
                    objdetails.TotalCount = TotalCount;
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
                if (ordersTable != null)
                {
                    ordersTable.Dispose();
                }
                if (ordersItemTable != null)
                {
                    ordersItemTable.Dispose();
                }
                if (shoppingBagItemTable != null)
                {
                    shoppingBagItemTable.Dispose();
                }
                if (totalTable != null)
                {
                    totalTable.Dispose();
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
        public async Task<OrderTabSetting> GetOrderTabSettingDetails(int tenantId, int userId)
        {
            DataTable schemaTable = new DataTable();
            OrderTabSetting objdetails = new OrderTabSetting();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderTabSettingDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            if (schemaTable != null)
                            {
                                objdetails = new OrderTabSetting
                                {
                                    PaymentVisible = schemaTable.Rows[0]["Payment"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["Payment"]),
                                    ShoppingBagVisible = schemaTable.Rows[0]["ShoppingBag"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["ShoppingBag"]),
                                    ShipmentVisible = schemaTable.Rows[0]["Shipment"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["Shipment"]),
                                    StoreDelivery = schemaTable.Rows[0]["StoreDelivery"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["StoreDelivery"]),
                                    PODVisible = schemaTable.Rows[0]["PODVisible"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["PODVisible"]),
                                    Exists = schemaTable.Rows[0]["Exists"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["Exists"]),
                                    EnableCheckService = schemaTable.Rows[0]["EnableCheckService"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["EnableCheckService"]),
                                    ShowShipmentCharges = schemaTable.Rows[0]["ShowShipmentCharges"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["ShowShipmentCharges"]),
                                    ShowSelfPickupTab = schemaTable.Rows[0]["ShowSelfPickupTab"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["ShowSelfPickupTab"])
                                };
                            }
                        }
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
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
        public async Task<int> SetOrderHasBeenReturn(int tenantId, int userId, int orderID, string Returnby = "Order")
        {
            int updateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYSetOrderHasBeenReturn", conn)
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_OrderID", orderID);
                    cmd.Parameters.AddWithValue("@_Returnby", Returnby);

                    updateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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

            return updateCount;
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
        public async Task<int> SmsWhatsUpDataSend(int tenantId, int userId, string programCode, int orderId, string clientAPIURL, string sMSWhtappTemplate, WebBotContentRequest webBotcontentRequest)
        {
            int result = 0;
            string Message = "";
            OrdersSmsWhatsUpDataDetails ordersSmsWhatsUpDataDetails = new OrdersSmsWhatsUpDataDetails();
            DataTable schemaTable = new DataTable();
            MaxWebBotHSMResponse MaxResponse = new MaxWebBotHSMResponse();
            try
            {
                StoreCampaignService storeCampaign = new StoreCampaignService(_connectionStringClass);
                GetWhatsappMessageDetailsResponse getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
                List<GetWhatsappMessageDetailsResponse> getWhatsappMessageDetailsResponseList = new List<GetWhatsappMessageDetailsResponse>();

                string whatsapptemplate =await storeCampaign.GetWhatsupTemplateName(tenantId, userId, sMSWhtappTemplate);

                string strpostionNumber = "";
                string strpostionName = "";
                string additionalInfo = "";
                try
                {
                    GetWhatsappMessageDetailsModal getWhatsappMessageDetailsModal = new GetWhatsappMessageDetailsModal()
                    {
                        ProgramCode = programCode
                    };

                    string apiBotReq = JsonConvert.SerializeObject(getWhatsappMessageDetailsModal);
              
                    string apiBotResponse = await _clienthttpclientservice.SendApiRequest(clientAPIURL + _OrderURLList.Getwhatsappmessagedetails, apiBotReq);

                    if (!string.IsNullOrEmpty(apiBotResponse.Replace("[]", "").Replace("[", "").Replace("]", "")))
                    {
                        getWhatsappMessageDetailsResponseList = JsonConvert.DeserializeObject<List<GetWhatsappMessageDetailsResponse>>(apiBotResponse);
                    }

                    if (getWhatsappMessageDetailsResponseList != null)
                    {
                        if (getWhatsappMessageDetailsResponseList.Count > 0)
                        {
                            getWhatsappMessageDetailsResponse = getWhatsappMessageDetailsResponseList.Where(x => x.TemplateName == whatsapptemplate).FirstOrDefault();
                        }
                    }

                    if (getWhatsappMessageDetailsResponse != null)
                    {
                        if (getWhatsappMessageDetailsResponse.Remarks != null && getWhatsappMessageDetailsResponse.Remarks != "")
                        {
                            string ObjRemark = getWhatsappMessageDetailsResponse.Remarks.Replace("\r\n", "");
                            string[] ObjSplitComma = ObjRemark.Split(',');

                            if (ObjSplitComma.Length > 0)
                            {
                                for (int i = 0; i < ObjSplitComma.Length; i++)
                                {
                                    strpostionNumber += ObjSplitComma[i].Split('-')[0].Trim().Replace("{", "").Replace("}", "") + ",";
                                    strpostionName += ObjSplitComma[i].Split('-')[1].Trim() + ",";
                                }
                            }
                            strpostionNumber = strpostionNumber.TrimEnd(',');
                            strpostionName = strpostionName.TrimEnd(',');
                        }
                    }
                    else
                    {
                        getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
                    }
                }
                catch (Exception)
                {
                    getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
                }

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetSmsWhatsUpDataDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_OrderID", orderId);
                    cmd.Parameters.AddWithValue("@_strpostionNumber", strpostionNumber);
                    cmd.Parameters.AddWithValue("@_strpostionName", strpostionName);
                    cmd.Parameters.AddWithValue("@_sMSWhtappTemplate", sMSWhtappTemplate);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            if (schemaTable != null)
                            {
                                ordersSmsWhatsUpDataDetails = new OrdersSmsWhatsUpDataDetails()
                                {
                                    OderID = schemaTable.Rows[0]["OderID"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["OderID"]),
                                    AlertCommunicationviaWhtsup = schemaTable.Rows[0]["AlertCommunicationviaWhtsup"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["AlertCommunicationviaWhtsup"]),
                                    AlertCommunicationviaSMS = schemaTable.Rows[0]["AlertCommunicationviaSMS"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["AlertCommunicationviaSMS"]),
                                    SMSSenderName = schemaTable.Rows[0]["SMSSenderName"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["SMSSenderName"]),
                                    IsSend = schemaTable.Rows[0]["IsSend"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["IsSend"]),
                                    MessageText = schemaTable.Rows[0]["MessageText"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["MessageText"]),
                                    InvoiceNo = schemaTable.Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["InvoiceNo"]),
                                    MobileNumber = schemaTable.Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["MobileNumber"]),
                                    WabaNumber = schemaTable.Rows[0]["WabaNumber"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["WabaNumber"]),
                                };
                            }

                            schemaTable = new DataTable();
                            schemaTable.Load(reader);
                            if (schemaTable != null)
                            {
                                ordersSmsWhatsUpDataDetails.AdditionalInfo = schemaTable.Rows[0]["additionalInfo"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["additionalInfo"]);
                            }

                        }
                    }
                }

                if (ordersSmsWhatsUpDataDetails.IsSend)
                {
                    if (ordersSmsWhatsUpDataDetails.AlertCommunicationviaWhtsup)
                    {
                        try
                        {
                            List<string> additionalList = new List<string>();
                            if (additionalInfo != null)
                            {
                                additionalList = ordersSmsWhatsUpDataDetails.AdditionalInfo.Split(",").ToList();
                            }

                            if (webBotcontentRequest.webBotHSMSetting != null)
                            {
                                if (webBotcontentRequest.webBotHSMSetting.Programcode.ToLower().Equals(webBotcontentRequest.ProgramCode.ToLower()))
                                {
                                    webBotcontentRequest.WABANo = ordersSmsWhatsUpDataDetails.WabaNumber;
                                    webBotcontentRequest.MaxHSMRequest.body.to = ordersSmsWhatsUpDataDetails.MobileNumber;
                                    webBotcontentRequest.MaxHSMRequest.body.from = ordersSmsWhatsUpDataDetails.WabaNumber;
                                    webBotcontentRequest.MaxHSMRequest.body.hsm.element_name = getWhatsappMessageDetailsResponse.TemplateName;
                                    webBotcontentRequest.TenantID = tenantId;
                                    webBotcontentRequest.ProgramCode = programCode;
                                    webBotcontentRequest.UserID = userId;


                                    if (additionalList.Count > 0)
                                    {
                                        List<LocalizableParam> list = new List<LocalizableParam>();

                                        foreach (string str in additionalList)
                                        {
                                            list.Add(new LocalizableParam() { @default = str });
                                        }
                                        webBotcontentRequest.MaxHSMRequest.body.hsm.localizable_params = list;
                                    }

                                    string JsonRequest = JsonConvert.SerializeObject(webBotcontentRequest.MaxHSMRequest);
                                    string ClientAPIResponse = await _MaxHSM.SendApiRequest(webBotcontentRequest.MaxWebBotHSMURL, JsonRequest);

                                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                                    {
                                        MaxResponse = JsonConvert.DeserializeObject<MaxWebBotHSMResponse>(ClientAPIResponse);
                                        result = MaxResponse.success ? 1 : 0;
                                    }
                                    else
                                    {
                                        result = 0;
                                    }
                                }
                            }
                            else
                            {
                                SendFreeTextRequest sendFreeTextRequest = new SendFreeTextRequest
                                {
                                    //To = ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0').Length > 10 ? ordersSmsWhatsUpDataDetails.MobileNumber : "91" + ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0'),
                                    To = ordersSmsWhatsUpDataDetails.MobileNumber,
                                    ProgramCode = programCode,
                                    TemplateName = getWhatsappMessageDetailsResponse.TemplateName,
                                    AdditionalInfo = additionalList,
                                    language = getWhatsappMessageDetailsResponse.TemplateLanguage,
                                    whatsAppNumber = ordersSmsWhatsUpDataDetails.WabaNumber
                                };

                                string apiReq = JsonConvert.SerializeObject(sendFreeTextRequest);

                                apiResponse = await _clienthttpclientservice.SendApiRequest(clientAPIURL + _OrderURLList.Sendcampaign, apiReq);
                                if (apiResponse.Equals("true"))
                                {
                                    result = 1;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else if (ordersSmsWhatsUpDataDetails.AlertCommunicationviaSMS)
                    {

                        Message = ordersSmsWhatsUpDataDetails.MessageText;                     

                        ChatSendSMS chatSendSMS = new ChatSendSMS
                        {
                            //MobileNumber = ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0').Length > 10 ? ordersSmsWhatsUpDataDetails.MobileNumber : "91" + ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0'),
                            MobileNumber = ordersSmsWhatsUpDataDetails.MobileNumber,
                            SenderId = ordersSmsWhatsUpDataDetails.SMSSenderName,
                            SmsText = Message
                        };

                        string apiReq = JsonConvert.SerializeObject(chatSendSMS);
                        //apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendSMS", apiReq);
                        apiResponse = await _clienthttpclientservice.SendApiRequest(clientAPIURL + _OrderURLList.Sendsms, apiReq);

                        ChatSendSMSResponse chatSendSMSResponse = new ChatSendSMSResponse();

                        chatSendSMSResponse = JsonConvert.DeserializeObject<ChatSendSMSResponse>(apiResponse);

                        if (chatSendSMSResponse != null)
                        {
                            result = chatSendSMSResponse.Id;
                        }
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }

            return result;
        }

        /// <summary>
        /// GetOrderShippingTemplate
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shippingTemplateRequest"></param>
        /// <returns></returns>
        public async Task<ShippingTemplateDetails> GetOrderShippingTemplate(int tenantId, int userId, ShippingTemplateRequest shippingTemplateRequest)
        {
            ShippingTemplateDetails objdetails = new ShippingTemplateDetails();

            List<ShippingTemplate> orderlist = new List<ShippingTemplate>();
            int totalCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderShippingTemplate", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_pageno", shippingTemplateRequest.PageNo);
                    cmd.Parameters.AddWithValue("@_pagesize", shippingTemplateRequest.PageSize);
                    cmd.Parameters.AddWithValue("@_FilterStatus", shippingTemplateRequest.FilterStatus);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {

                        while (dr.Read())
                        {
                            ShippingTemplate obj = new ShippingTemplate
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                TemplateName = dr["TemplateName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TemplateName"]),
                                Height = dr["Height"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Height"]),
                                Height_Unit = dr["Height_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Height_Unit"]),
                                Length = dr["Length"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Length"]),
                                Length_Unit = dr["Length_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Length_Unit"]),
                                Breath = dr["Breath"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Breath"]),
                                Breath_Unit = dr["Breath_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Breath_Unit"]),
                                Weight = dr["Weight"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Weight"]),
                                Weight_Unit = dr["Weight_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Weight_Unit"]),
                                CreatedOn = dr["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedOn"]),
                                ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedOn"]),
                                Createdby = dr["Createdby"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Createdby"]),
                                Modifiedby = dr["Modifiedby"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Modifiedby"])
                            };
                            orderlist.Add(obj);
                        }
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                totalCount = dr["TotalCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalCount"]);
                            }
                        }
                        objdetails.ShippingTemplateList = orderlist;
                        objdetails.TotalCount = totalCount;
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
            return objdetails;
        }

        /// <summary>
        /// InsertOrderShippingTemplate
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="addEditShippingTemplate"></param>
        /// <returns></returns>
        public async Task<int> InsertUpdateOrderShippingTemplate(int tenantID, int userID, AddEditShippingTemplate addEditShippingTemplate)
        {
            int result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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
            return result;
        }

        /// <summary>
        /// GetOrderShippingTemplateName
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ShippingTemplateDetails> GetOrderShippingTemplateName(int tenantId, int userId)
        {
            ShippingTemplateDetails objdetails = new ShippingTemplateDetails();

            List<ShippingTemplate> orderlist = new List<ShippingTemplate>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderShippingTemplateName", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {

                        while (dr.Read())
                        {
                            ShippingTemplate obj = new ShippingTemplate
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                TemplateName = dr["TemplateName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TemplateName"]),
                                Height = dr["Height"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Height"]),
                                Height_Unit = dr["Height_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Height_Unit"]),
                                Length = dr["Length"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Length"]),
                                Length_Unit = dr["Length_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Length_Unit"]),
                                Breath = dr["Breath"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Breath"]),
                                Breath_Unit = dr["Breath_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Breath_Unit"]),
                                Weight = dr["Weight"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Weight"]),
                                Weight_Unit = dr["Weight_Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Weight_Unit"]),
                            };
                            orderlist.Add(obj);
                        }
                        objdetails.ShippingTemplateList = orderlist;
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
            return objdetails;
        }

        /// <summary>
        /// CheckPincodeExists
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="Delivery_postcode"></param>
        /// <returns></returns>
        public async Task<PincodeCheck> CheckPincodeExists(int tenantID, int userID, string delivery_postcode)
        {
            PincodeCheck pincodeCheck = new PincodeCheck();
            DataTable schemaTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYCheckPincodeExists", conn)
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_Pincode", delivery_postcode);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);

                            if (schemaTable != null)
                            {
                                pincodeCheck = new PincodeCheck()
                                {
                                    PincodeAvailable = schemaTable.Rows[0]["IsPincodeValid"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["IsPincodeValid"]),
                                    PincodeState = schemaTable.Rows[0]["statename"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["statename"])
                                };
                            }
                        }
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }
            return pincodeCheck;
        }

        /// <summary>
        /// SetOrderHasBeenSelfPickUp
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderID"></param>
        /// <param name="PickupDate"></param>
        /// <param name="PickupTime"></param>
        /// <returns></returns>
        public async Task<int> SetOrderHasBeenSelfPickUp(int tenantId, int userId, OrderSelfPickUp orderSelfPickUp)
        {
            int updateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYSetOrderHasBeenSelfPickUp", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_OrderID", orderSelfPickUp.OrderID);
                    cmd.Parameters.AddWithValue("@_PickupDate", orderSelfPickUp.PickupDate);
                    cmd.Parameters.AddWithValue("@_PickupTime", orderSelfPickUp.PickupTime);

                    cmd.CommandType = CommandType.StoredProcedure;
                    updateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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

            return updateCount;
        }

        /// <summary>
        /// GetCourierPartnerFilter
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public async Task<List<string>> GetCourierPartnerFilter(int tenantId, int userId, int pageID)
        {
            List<string> CourierPartnerFilter = new List<string>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetCourierPartnerFilter", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_PageID", pageID);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {

                        while (dr.Read())
                        {
                            string obj = Convert.ToString(dr["CourierPartner"]);

                            CourierPartnerFilter.Add(obj);
                        }
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
            return CourierPartnerFilter;
        }

        /// <summary>
        /// BulkUploadOrderTemplate 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="UserFor"></param>
        /// <param name="DataSetCSV"></param>
        public async Task<List<string>> BulkUploadOrderTemplate(int tenantID, int createdBy, int userFor, DataSet dataSetCSV)
        {
            XmlDocument xmlDoc = new XmlDocument();
            List<string> csvLst = new List<string>();
            MySqlCommand cmd = null;
            string succesFile = string.Empty; string ErroFile = string.Empty;
            DataTable succesFileTable = new DataTable();
            DataTable erroFileTable = new DataTable();
            try
            {
                if (dataSetCSV != null && dataSetCSV.Tables.Count > 0)
                {
                    if (dataSetCSV.Tables[0] != null && dataSetCSV.Tables[0].Rows.Count > 0)
                    {
                        xmlDoc.LoadXml(dataSetCSV.GetXml());
                        if (conn != null && conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        using (conn)
                        {
                            cmd = new MySqlCommand("SP_PHYBulkUploadOrderTemplate", conn)
                            {
                                CommandType = CommandType.StoredProcedure,
                                Connection = conn
                            };
                            cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                            cmd.Parameters.AddWithValue("@_UserFor", userFor);
                            cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                            cmd.Parameters.AddWithValue("@_node", Xpath);
                            cmd.Parameters.AddWithValue("@_createdBy", createdBy);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (reader.HasRows)
                                {
                                    succesFileTable.Load(reader);
                                    erroFileTable.Load(reader);
                                }
                            }
                            if (succesFileTable != null)
                            {
                                //for success file
                                succesFile = succesFileTable.Rows.Count > 0 ? CommonService.DataTableToCsv(succesFileTable) : string.Empty;
                                csvLst.Add(succesFile);
                            }
                            if (erroFileTable != null)
                            {
                                //for error file
                                ErroFile = erroFileTable.Rows.Count > 0 ? CommonService.DataTableToCsv(erroFileTable) : string.Empty;
                                csvLst.Add(ErroFile);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dataSetCSV != null)
                {
                    dataSetCSV.Dispose();
                }
                if (succesFileTable != null)
                {
                    succesFileTable.Dispose();
                }
                if (erroFileTable != null)
                {
                    erroFileTable.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return csvLst;
        }

        /// <summary>
        /// GetOrderCountry
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<OrderCountry>> GetOrderCountry(int tenantId, int userId)
        {
            List<OrderCountry> listOrderCountries = new List<OrderCountry>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderCountry", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            OrderCountry obj = new OrderCountry()
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                Country = dr["Country"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Country"]),
                                CreatedOn = dr["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedOn"]),
                                CreatedBy = dr["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedBy"]),
                                ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedOn"]),
                                ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedBy"])
                            };

                            listOrderCountries.Add(obj);
                        }
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
            return listOrderCountries;
        }

        /// <summary>
        /// InsertModifyOrderCountry
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="modifyOrderCountryRequest"></param>
        /// <returns></returns>
        public async Task<int> InsertModifyOrderCountry(int tenantId, int userId, ModifyOrderCountryRequest modifyOrderCountryRequest)
        {
            int updateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYInsertModifyOrderCountry", conn)
                    {
                        Connection = conn
                    };
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_ID", modifyOrderCountryRequest.ID);
                    cmd.Parameters.AddWithValue("@_Countryname", modifyOrderCountryRequest.Country);
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_IsDelete", modifyOrderCountryRequest.IsDelete);
                    updateCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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

            return updateCount;
        }

        /// <summary>
        /// GetCustAddressDetails
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<CustAddressDetails> GetCustAddressDetails(int tenantID, int userID, int orderId)
        {
            CustAddressDetails custAddressDetails = new CustAddressDetails();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrdersCustAddress", conn)
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_OrderID", orderId);
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            custAddressDetails = new CustAddressDetails()
                            {
                                CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                MobileNumber = dr["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MobileNumber"]),
                                ShippingAddress = dr["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ShippingAddress"]),
                                City = dr["City"] == DBNull.Value ? string.Empty : Convert.ToString(dr["City"]),
                                PinCode = dr["PinCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PinCode"]),
                                State = dr["State"] == DBNull.Value ? string.Empty : Convert.ToString(dr["State"]),
                                Country = dr["Country"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Country"]),
                                Landmark = dr["Landmark"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Landmark"])
                            };
                        }
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

            return custAddressDetails;
        }

        /// <summary>
        /// GetPrintLabelDetails
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<PrintLabelDetails> GetPrintLabelDetails(int tenantID, int userID, int orderId)
        {
            PrintLabelDetails printLabelDetails = new PrintLabelDetails();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetPrintLabelDetails", conn)
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_OrderID", orderId);
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                printLabelDetails.OrderCustDetails = new OrderCustDetails()
                                {
                                    OrderCreatedOn = reader["OrderCreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderCreatedOn"]),
                                    CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                                    MobileNumber = reader["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MobileNumber"]),
                                    ShippingAddress = reader["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ShippingAddress"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    PinCode = reader["PinCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PinCode"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                    Landmark = reader["Landmark"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Landmark"]),
                                    Latitude = reader["Latitude"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Latitude"]),
                                    Longitude = reader["Longitude"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Longitude"]),
                                    StoreName = reader["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["StoreName"]),
                                    Address = reader["Address"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address"]),
                                    CityName = reader["CityName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CityName"]),
                                    StateName = reader["StateName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["StateName"]),
                                    CountryName = reader["CountryName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CountryName"]),
                                    PincodeID = reader["PincodeID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PincodeID"]),
                                    StoreEmailID = reader["StoreEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["StoreEmailID"]),
                                    StorePhoneNo = reader["StorePhoneNo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["StorePhoneNo"]),
                                };
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    printLabelDetails.OrderLabelDetails = new OrderLabelDetails()
                                    {
                                        OrderID = reader["OrderID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderID"]),
                                        RequestID = reader["RequestID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestID"]),
                                    };
                                }
                            }

                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    printLabelDetails.OrderTemplateDetails = new OrderTemplateDetails()
                                    {
                                        Weight = reader["Weight"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Weight"]),
                                        Dimensions = reader["Dimensions"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Dimensions"]),
                                    };
                                }
                            }

                            if (reader.NextResult())
                            {
                                printLabelDetails.OrderLabelItemsDetails = new List<OrderLabelItemsDetails>();
                                while (reader.Read())
                                {
                                    OrderLabelItemsDetails obj = new OrderLabelItemsDetails()
                                    {
                                        SKU = reader["SKU"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SKU"]),
                                        ItemName = reader["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemName"]),
                                        ItemPrice = reader["ItemPrice"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemPrice"]),
                                        Quantity = reader["Quantity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Quantity"])
                                    };
                                    printLabelDetails.OrderLabelItemsDetails.Add(obj);
                                }
                            }

                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    printLabelDetails.TotalPrice = reader["TotalPrice"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TotalPrice"]);
                                }
                            }
                        }
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
            return printLabelDetails;
        }

        /// <summary>
        /// GetSelfPickUpOrdersDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="selfPickupOrdersDataRequest"></param>
        /// <returns></returns>
        public async Task<SelfPickupOrderResponseDetails> GetSelfPickUpOrdersDetails(int tenantId, int userId, SelfPickupOrdersDataRequest selfPickupOrdersDataRequest)
        {
            SelfPickupOrderResponseDetails objdetails = new SelfPickupOrderResponseDetails();
            List<Orders> orderlist = new List<Orders>();
            int totalCount = 0;
            DataTable ordersTable = new DataTable();
            DataTable ordersItemTable = new DataTable();
            DataTable shoppingBagItemTable = new DataTable();
            DataTable totalTable = new DataTable();
            DataTable showItemPropertyTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetSelfPickUpOrdersDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_SearchText", selfPickupOrdersDataRequest.SearchText);
                    cmd.Parameters.AddWithValue("@_pageno", selfPickupOrdersDataRequest.PageNo);
                    cmd.Parameters.AddWithValue("@_pagesize", selfPickupOrdersDataRequest.PageSize);
                    cmd.Parameters.AddWithValue("@_Filterdate", selfPickupOrdersDataRequest.Filterdate);
                    cmd.Parameters.AddWithValue("@_FilterTimeSlot", selfPickupOrdersDataRequest.FilterTimeSlot);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            ordersTable.Load(reader);
                            ordersItemTable.Load(reader);
                            totalTable.Load(reader);
                            showItemPropertyTable.Load(reader);
                        }
                    }
                    if (ordersTable != null)
                    {
                        for (int i = 0; i < ordersTable.Rows.Count; i++)
                        {
                            Orders obj = new Orders
                            {
                                ID = ordersTable.Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["ID"]),
                                InvoiceNo = ordersTable.Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["InvoiceNo"]),
                                CustomerName = ordersTable.Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["CustomerName"]),
                                MobileNumber = ordersTable.Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["MobileNumber"]),
                                Amount = ordersTable.Rows[i]["Amount"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Amount"]),
                                Date = ordersTable.Rows[i]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Date"]),
                                Time = ordersTable.Rows[i]["Time"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Time"]),
                                DeliveryTypeName = ordersTable.Rows[i]["DeliveryTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["DeliveryTypeName"]),
                                ShippingAddress = ordersTable.Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShippingAddress"]),
                                POSGenratedInvoiceNo = ordersTable.Rows[i]["POSGenratedInvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["POSGenratedInvoiceNo"]),
                                TotalAmount = ordersTable.Rows[i]["TotalAmount"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["TotalAmount"]),
                                PickupDate = ordersTable.Rows[i]["PickupDate"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PickupDate"]),
                                PickupTime = ordersTable.Rows[i]["PickupTime"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PickupTime"]),
                                StoreCode = ordersTable.Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["StoreCode"]),
                                SourceOfOrder = ordersTable.Rows[i]["SourceOfOrder"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["SourceOfOrder"]),
                                OrdersItemList = new List<OrdersItem>(),
                            };

                            obj.OrdersItemList = ordersItemTable.AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                            {
                                ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                                ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                                ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                                Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity"),
                                ShowItemProperty = x.Field<object>("ShowItemProperty") == DBNull.Value ? false : Convert.ToBoolean(x.Field<bool>("ShowItemProperty"))

                            }).ToList();


                            obj.ShowItemProperty = showItemPropertyTable.Rows[0]["ShowItemProperty"] == DBNull.Value ? false : Convert.ToBoolean(showItemPropertyTable.Rows[0]["ShowItemProperty"]);

                            orderlist.Add(obj);
                        }
                    }
                    if (totalTable != null)
                    {
                        if (totalTable.Rows.Count > 0)
                        {
                            totalCount = totalTable.Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(totalTable.Rows[0]["TotalOrder"]);
                        }
                    }

                    objdetails.OrdersList = orderlist;
                    objdetails.TotalCount = totalCount;
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
                if (ordersTable != null)
                {
                    ordersTable.Dispose();
                }
                if (ordersItemTable != null)
                {
                    ordersItemTable.Dispose();
                }
                if (shoppingBagItemTable != null)
                {
                    shoppingBagItemTable.Dispose();
                }
                if (totalTable != null)
                {
                    totalTable.Dispose();
                }
            }
            return objdetails;
        }

        /// <summary>
        /// UpdateShoppingAvailableQty
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shoppingAvailableQty"></param>
        /// <returns></returns>
        public async Task<int> UpdateShoppingAvailableQty(int tenantId, int userId, List<ShoppingAvailableQty> shoppingAvailableQty, WebBotContentRequest webBotContentRequest)
        {
            int updateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {

                    MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShoppingAvailableQty", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_ID", string.Join(", ", shoppingAvailableQty.Select(z => z.ID)));
                    cmd.Parameters.AddWithValue("@_ItemID", string.Join(", ", shoppingAvailableQty.Select(z => z.ItemID)));
                    cmd.Parameters.AddWithValue("@_Availableqty", string.Join(", ", shoppingAvailableQty.Select(z => z.Availableqty)));
                    cmd.Parameters.AddWithValue("@_ShoppingID", shoppingAvailableQty[0].ShoppingID);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            updateCount = dr["rowcount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["rowcount"]);
                            if (webBotContentRequest.webBotHSMSetting != null)
                            {
                                if (webBotContentRequest.webBotHSMSetting.Programcode.ToLower().Equals(webBotContentRequest.ProgramCode.ToLower()))
                                {
                                    webBotContentRequest.MaxHSMRequest.body.from = dr["WabaNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["WabaNumber"]);
                                    webBotContentRequest.CustomerName = shoppingAvailableQty[0].CustomerName;
                                    webBotContentRequest.MobileNo = shoppingAvailableQty[0].CustomerMob;
                                    webBotContentRequest.ShopingBagNo = shoppingAvailableQty[0].ShoppingID;
                                }
                            }
                        }
                    }


                    if (updateCount >= 0)
                    {
                        try
                        {
                            HSWebBotService HSWebBotService = new HSWebBotService(_connectionStringClass, _APICall, _WebBot, _MaxHSM, _logger);
                            await HSWebBotService.SendWebBotHSM(webBotContentRequest);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
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
            return updateCount;
        }
    }
}
