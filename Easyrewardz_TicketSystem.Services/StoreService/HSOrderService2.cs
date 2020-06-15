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
                            CourierPartner = ds.Tables[0].Rows[i]["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CourierPartner"]),
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
        /// 
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
            string Message = "";
            DataSet ds = new DataSet();
            OrdersSmsWhatsUpDataDetails ordersSmsWhatsUpDataDetails = new OrdersSmsWhatsUpDataDetails();
            try
            {

                GetWhatsappMessageDetailsResponse getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();

                string strpostionNumber = "";
                string strpostionName = "";
                string additionalInfo = "";
                try
                {
                    GetWhatsappMessageDetailsModal getWhatsappMessageDetailsModal = new GetWhatsappMessageDetailsModal()
                    {
                        ProgramCode = ProgramCode
                    };

                    string apiBotReq = JsonConvert.SerializeObject(getWhatsappMessageDetailsModal);
                    string apiBotResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetWhatsappMessageDetails", apiBotReq);

                    if (!string.IsNullOrEmpty(apiBotResponse.Replace("[]", "").Replace("[", "").Replace("]", "")))
                    {
                        getWhatsappMessageDetailsResponse = JsonConvert.DeserializeObject<GetWhatsappMessageDetailsResponse>(apiBotResponse.Replace("[", "").Replace("]", ""));
                    }

                    if (getWhatsappMessageDetailsResponse != null)
                    {
                        if (getWhatsappMessageDetailsResponse.Remarks != null)
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
                }
                catch (Exception)
                {
                    getWhatsappMessageDetailsResponse = new GetWhatsappMessageDetailsResponse();
                }



                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderTabSettingDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_OrderID", orderId);
                cmd.Parameters.AddWithValue("@_strpostionNumber", strpostionNumber);
                cmd.Parameters.AddWithValue("@_strpostionName", strpostionName);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    ordersSmsWhatsUpDataDetails = new OrdersSmsWhatsUpDataDetails()
                    {
                        OderID = ds.Tables[0].Rows[0]["OderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["OderID"]),
                        AlertCommunicationviaWhtsup = ds.Tables[0].Rows[0]["AlertCommunicationviaWhtsup"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["AlertCommunicationviaWhtsup"]),
                        AlertCommunicationviaSMS = ds.Tables[0].Rows[0]["AlertCommunicationviaSMS"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["AlertCommunicationviaSMS"]),
                        SMSSenderName = ds.Tables[0].Rows[0]["SMSSenderName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["SMSSenderName"]),
                        ShoppingBagConvertToOrder = ds.Tables[0].Rows[0]["ShoppingBagConvertToOrder"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["ShoppingBagConvertToOrder"]),
                        ShoppingBagConvertToOrderText = ds.Tables[0].Rows[0]["ShoppingBagConvertToOrderText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ShoppingBagConvertToOrderText"]),
                        AWBAssigned = ds.Tables[0].Rows[0]["AWBAssigned"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["AWBAssigned"]),
                        AWBAssignedText = ds.Tables[0].Rows[0]["AWBAssignedText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["AWBAssignedText"]),
                        PickupScheduled = ds.Tables[0].Rows[0]["PickupScheduled"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["PickupScheduled"]),
                        PickupScheduledText = ds.Tables[0].Rows[0]["PickupScheduledText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["PickupScheduledText"]),
                        Shipped = ds.Tables[0].Rows[0]["Shipped"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Shipped"]),
                        ShippedText = ds.Tables[0].Rows[0]["ShippedText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ShippedText"]),
                        Delivered = ds.Tables[0].Rows[0]["Delivered"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Delivered"]),
                        DeliveredText = ds.Tables[0].Rows[0]["DeliveredText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["DeliveredText"]),
                        InvoiceNo = ds.Tables[0].Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]),
                        MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"]),
                        AdditionalInfo = ds.Tables[1].Rows[0]["additionalInfo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["additionalInfo"]),
                    };
                    // result = ds.Tables[0].Rows[0]["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ChatID"]);
                    // Message = ds.Tables[0].Rows[0]["Message"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                    // additionalInfo = ds.Tables[0].Rows[0]["additionalInfo"] == DBNull.Value ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0]["additionalInfo"]);
                }



                if (ordersSmsWhatsUpDataDetails.AlertCommunicationviaWhtsup)
                {
                    try
                    {
                        List<string> additionalList = new List<string>();
                        if (additionalInfo != null)
                        {
                            additionalList = ordersSmsWhatsUpDataDetails.AdditionalInfo.Split(",").ToList();
                        }
                        SendFreeTextRequest sendFreeTextRequest = new SendFreeTextRequest
                        {
                            To = ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0').Length > 10 ? ordersSmsWhatsUpDataDetails.MobileNumber : "91" + ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0'),
                            ProgramCode = ProgramCode,
                            TemplateName = getWhatsappMessageDetailsResponse.TemplateName,
                            AdditionalInfo = additionalList
                        };

                        string apiReq = JsonConvert.SerializeObject(sendFreeTextRequest);
                        apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendCampaign", apiReq);

                       
                        //if (apiResponse.Equals("true"))
                        //{
                        //    UpdateResponseShare(objRequest.CustomerID, "Contacted Via Chatbot");
                        //}
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else if (ordersSmsWhatsUpDataDetails.AlertCommunicationviaSMS)
                {
                    if(sMSWhtappTemplate == "ShoppingBagConvertToOrder" & ordersSmsWhatsUpDataDetails.ShoppingBagConvertToOrder)
                    {
                        Message = ordersSmsWhatsUpDataDetails.ShoppingBagConvertToOrderText;
                    }
                    else if (sMSWhtappTemplate == "AWBAssigned" & ordersSmsWhatsUpDataDetails.AWBAssigned)
                    {
                        Message = ordersSmsWhatsUpDataDetails.AWBAssignedText;
                    }
                    else if (sMSWhtappTemplate == "PickupScheduled" & ordersSmsWhatsUpDataDetails.PickupScheduled)
                    {
                        Message = ordersSmsWhatsUpDataDetails.PickupScheduledText;
                    }
                    else if (sMSWhtappTemplate == "Shipped" & ordersSmsWhatsUpDataDetails.Shipped)
                    {
                        Message = ordersSmsWhatsUpDataDetails.ShippedText;
                    }
                    else if (sMSWhtappTemplate == "Delivered" & ordersSmsWhatsUpDataDetails.Delivered)
                    {
                        Message = ordersSmsWhatsUpDataDetails.DeliveredText;
                    }
                    

                    ChatSendSMS chatSendSMS = new ChatSendSMS
                    {
                        MobileNumber = ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0').Length > 10 ? ordersSmsWhatsUpDataDetails.MobileNumber : "91" + ordersSmsWhatsUpDataDetails.MobileNumber.TrimStart('0'),
                        SenderId = ordersSmsWhatsUpDataDetails.SMSSenderName,
                        SmsText = Message
                    };

                    string apiReq = JsonConvert.SerializeObject(chatSendSMS);
                   // apiResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendSMS", apiReq);

                    ChatSendSMSResponse chatSendSMSResponse = new ChatSendSMSResponse();

                    chatSendSMSResponse = JsonConvert.DeserializeObject<ChatSendSMSResponse>(apiResponse);

                    if (chatSendSMSResponse != null)
                    {
                        result = chatSendSMSResponse.Id;
                    }

                    //if (result > 0)
                    //{
                    //    UpdateResponseShare(objRequest.CustomerID, "Contacted Via SMS");
                    //}
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

            return result;
        }
    }
}
