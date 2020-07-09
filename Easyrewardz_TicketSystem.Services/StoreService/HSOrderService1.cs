using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class HSOrderService : IHSOrder
    {
        #region Variable Declaration
        MySqlConnection conn = new MySqlConnection();
        string apiResponse = string.Empty;
        string apiResponse1 = string.Empty;
        string apisecurityToken = string.Empty;
        string apiURL = string.Empty;
        string apiURLGetUserATVDetails = string.Empty;
        #endregion

        #region Constructor
        public HSOrderService(string _connectionString)
        {

            conn.ConnectionString = _connectionString;
            apisecurityToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJQcm9ncmFtQ29kZSI6IkJhdGEiLCJVc2VySUQiOiIzIiwiQXBwSUQiOiI3IiwiRGF5IjoiMjgiLCJNb250aCI6IjMiLCJZZWFyIjoiMjAyMSIsIlJvbGUiOiJBZG1pbiIsImlzcyI6IkF1dGhTZWN1cml0eUlzc3VlciIsImF1ZCI6IkF1dGhTZWN1cml0eUF1ZGllbmNlIn0.0XeF7V5LWfQn0NlSlG7Rb-Qq1hUCtUYRDg6dMGIMvg0";
            //apiURLGetUserATVDetails = configuration.GetValue<string>("apiURLGetUserATVDetails");
        }
        #endregion


        /// <summary>
        /// Get Order Configuration
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public OrderConfiguration GetOrderConfiguration(int TenantId, int UserId, string ProgramCode)
        {
            DataSet ds = new DataSet();
            OrderConfiguration moduleConfiguration = new OrderConfiguration();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderConfiguration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", TenantId);
                cmd.Parameters.AddWithValue("@_prgramCode", ProgramCode);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        moduleConfiguration.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                        moduleConfiguration.IntegratedSystem = ds.Tables[0].Rows[0]["IntegratedSystem"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["IntegratedSystem"]);
                        moduleConfiguration.Payment = ds.Tables[0].Rows[0]["Payment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Payment"]);
                        moduleConfiguration.Shipment = ds.Tables[0].Rows[0]["Shipment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Shipment"]);
                        moduleConfiguration.ShoppingBag = ds.Tables[0].Rows[0]["ShoppingBag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["ShoppingBag"]);
                        moduleConfiguration.EnableClickAfterValue = ds.Tables[0].Rows[0]["EnableClickAfterValue"] == DBNull.Value ? 0 : Convert.ToInt16(ds.Tables[0].Rows[0]["EnableClickAfterValue"]);
                        moduleConfiguration.EnableClickAfterDuration = ds.Tables[0].Rows[0]["EnableClickAfterDuration"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["EnableClickAfterDuration"]);
                        moduleConfiguration.StoreDelivery= ds.Tables[0].Rows[0]["StoreDelivery"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["StoreDelivery"]);
                        moduleConfiguration.AlertCommunicationviaWhtsup= ds.Tables[0].Rows[0]["AlertCommunicationviaWhtsup"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["AlertCommunicationviaWhtsup"]);
                        moduleConfiguration.AlertCommunicationviaSMS= ds.Tables[0].Rows[0]["AlertCommunicationviaSMS"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["AlertCommunicationviaSMS"]);
                        moduleConfiguration.AlertCommunicationSMSText = ds.Tables[0].Rows[0]["AlertCommunicationSMSText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["AlertCommunicationSMSText"]);
                        moduleConfiguration.ShoppingBagConvertToOrder = ds.Tables[0].Rows[0]["ShoppingBagConvertToOrder"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["ShoppingBagConvertToOrder"]);
                        moduleConfiguration.ShoppingBagConvertToOrderText = ds.Tables[0].Rows[0]["ShoppingBagConvertToOrderText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["ShoppingBagConvertToOrderText"]);
                        moduleConfiguration.AWBAssigned = ds.Tables[0].Rows[0]["AWBAssigned"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["AWBAssigned"]);
                        moduleConfiguration.AWBAssignedText = ds.Tables[0].Rows[0]["AWBAssignedText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["AWBAssignedText"]);
                        moduleConfiguration.PickupScheduled = ds.Tables[0].Rows[0]["PickupScheduled"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["PickupScheduled"]);
                        moduleConfiguration.PickupScheduledText = ds.Tables[0].Rows[0]["PickupScheduledText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["PickupScheduledText"]);
                        moduleConfiguration.Shipped = ds.Tables[0].Rows[0]["Shipped"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Shipped"]);
                        moduleConfiguration.ShippedText = ds.Tables[0].Rows[0]["ShippedText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["ShippedText"]);
                        moduleConfiguration.Delivered = ds.Tables[0].Rows[0]["Delivered"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Delivered"]);
                        moduleConfiguration.DeliveredText = ds.Tables[0].Rows[0]["DeliveredText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["DeliveredText"]);
                        moduleConfiguration.Cancel = ds.Tables[0].Rows[0]["Cancel"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Cancel"]);
                        moduleConfiguration.CancelText = ds.Tables[0].Rows[0]["CancelText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["CancelText"]);
                        moduleConfiguration.UnDeliverable = ds.Tables[0].Rows[0]["UnDeliverable"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["UnDeliverable"]);
                        moduleConfiguration.UnDeliverableText = ds.Tables[0].Rows[0]["UnDeliverableText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["UnDeliverableText"]);
                        moduleConfiguration.StoreDeliveryText = ds.Tables[0].Rows[0]["StoreDeliveryText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["StoreDeliveryText"]);
                        moduleConfiguration.PaymentTenantCodeText = ds.Tables[0].Rows[0]["TenderPayRemainingText"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["TenderPayRemainingText"]);
                        moduleConfiguration.RetryCount = ds.Tables[0].Rows[0]["RetryCount"] == DBNull.Value ? 0 : Convert.ToInt16(ds.Tables[0].Rows[0]["RetryCount"]);
                        moduleConfiguration.StateFlag = ds.Tables[0].Rows[0]["StateFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["StateFlag"]);
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
            return moduleConfiguration;
        }

        /// <summary>
        /// Update Order Configuration
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int UpdateOrderConfiguration(OrderConfiguration orderConfiguration, int ModifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateOrderConfiguration", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ID", orderConfiguration.ID);
                cmd.Parameters.AddWithValue("@_IntegratedSystem", Convert.ToInt16(orderConfiguration.IntegratedSystem));
                cmd.Parameters.AddWithValue("@_Payment", Convert.ToInt16(orderConfiguration.Payment));
                cmd.Parameters.AddWithValue("@_Shipment", Convert.ToInt16(orderConfiguration.Shipment));
                cmd.Parameters.AddWithValue("@_ShoppingBag", Convert.ToInt16(orderConfiguration.ShoppingBag));
                cmd.Parameters.AddWithValue("@_StoreDelivery", Convert.ToInt16(orderConfiguration.StoreDelivery));
                cmd.Parameters.AddWithValue("@_AlertCommunicationviaWhtsup", Convert.ToInt16(orderConfiguration.AlertCommunicationviaWhtsup));
                cmd.Parameters.AddWithValue("@_AlertCommunicationviaSMS", Convert.ToInt16(orderConfiguration.AlertCommunicationviaSMS));
                cmd.Parameters.AddWithValue("@_AlertCommunicationviaSMSText", orderConfiguration.AlertCommunicationSMSText);
                cmd.Parameters.AddWithValue("@_EnableClickAfterValue", Convert.ToInt16(orderConfiguration.EnableClickAfterValue));
                cmd.Parameters.AddWithValue("@_EnableClickAfterDuration", orderConfiguration.EnableClickAfterDuration);
                cmd.Parameters.AddWithValue("@_ShoppingBagConvertToOrder", Convert.ToInt16(orderConfiguration.ShoppingBagConvertToOrder));
                cmd.Parameters.AddWithValue("@_ShoppingBagConvertToOrderText", orderConfiguration.ShoppingBagConvertToOrderText);
                cmd.Parameters.AddWithValue("@_AWBAssigned", Convert.ToInt16(orderConfiguration.AWBAssigned));
                cmd.Parameters.AddWithValue("@_AWBAssignedText", orderConfiguration.AWBAssignedText);
                cmd.Parameters.AddWithValue("@_PickupScheduled", Convert.ToInt16(orderConfiguration.PickupScheduled));
                cmd.Parameters.AddWithValue("@_PickupScheduledText", orderConfiguration.PickupScheduledText);
                cmd.Parameters.AddWithValue("@_Shipped", Convert.ToInt16(orderConfiguration.Shipped));
                cmd.Parameters.AddWithValue("@_ShippedText", orderConfiguration.ShippedText);
                cmd.Parameters.AddWithValue("@_Delivered", Convert.ToInt16(orderConfiguration.Delivered));
                cmd.Parameters.AddWithValue("@_DeliveredText", orderConfiguration.DeliveredText);
                cmd.Parameters.AddWithValue("@_Cancel", Convert.ToInt16(orderConfiguration.Cancel));
                cmd.Parameters.AddWithValue("@_CancelText", orderConfiguration.CancelText);
                cmd.Parameters.AddWithValue("@_UnDeliverable", Convert.ToInt16(orderConfiguration.UnDeliverable));
                cmd.Parameters.AddWithValue("@_UnDeliverableText", orderConfiguration.UnDeliverableText);
                cmd.Parameters.AddWithValue("@_StoreDeliveryText", orderConfiguration.StoreDeliveryText);
                cmd.Parameters.AddWithValue("@_PaymentTenantCodeText", orderConfiguration.PaymentTenantCodeText);
                cmd.Parameters.AddWithValue("@_RetryCount", orderConfiguration.RetryCount);
                cmd.Parameters.AddWithValue("@_StateFlag", orderConfiguration.StateFlag);
                cmd.Parameters.AddWithValue("@_ModifiedBy", ModifiedBy);

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
        /// Get Order Delivered Details
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="orderDeliveredFilter"></param>
        /// <returns></returns>
        public OrderDeliveredDetails GetOrderDeliveredDetails(int TenantId, int UserId, OrderDeliveredFilterRequest orderDeliveredFilter)
        {
            DataSet ds = new DataSet();
            OrderDeliveredDetails objdetails = new OrderDeliveredDetails();

            List<OrderDelivered> orderDelivered = new List<OrderDelivered>();
            int TotalCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderDeliveredDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", TenantId);
                cmd.Parameters.AddWithValue("@_UserID", UserId);
                cmd.Parameters.AddWithValue("@_SearchText", orderDeliveredFilter.SearchText);
                cmd.Parameters.AddWithValue("@_pageno", orderDeliveredFilter.PageNo);
                cmd.Parameters.AddWithValue("@_pagesize", orderDeliveredFilter.PageSize);
                cmd.Parameters.AddWithValue("@_FilterStatus", orderDeliveredFilter.FilterStatus);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrderDelivered obj = new OrderDelivered
                        {
                            ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]),
                            MobileNumber = ds.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]),
                            Date = ds.Tables[0].Rows[i]["Date"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["Date"]),
                            Time = ds.Tables[0].Rows[i]["Time"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["Time"]),
                            StatusName = ds.Tables[0].Rows[i]["StatusName"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["StatusName"]),
                            ActionTypeName = ds.Tables[0].Rows[i]["ActionTypeName"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["ActionTypeName"]),
                            orderDeliveredItems = new List<OrderDeliveredItem>()
                        };


                        obj.orderDeliveredItems = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrderDeliveredItem()
                        {
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<double>("ItemPrice"),
                            Quantity = x.Field<int>("Quantity")

                        }).ToList();

                        orderDelivered.Add(obj);
                    }
                }

                if (ds != null && ds.Tables[2] != null)
                {
                    TotalCount = ds.Tables[2].Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[2].Rows[0]["TotalOrder"]);
                }

                objdetails.orderDelivereds = orderDelivered;
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
        /// Get Order Status Filters
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="PageID"></param>
        /// <returns></returns>
        public List<OrderStatusFilter> GetOrderStatusFilter(int TenantId, int UserId, int PageID)
        {
            DataSet ds = new DataSet();
            List<OrderStatusFilter> orderStatusFilter = new List<OrderStatusFilter>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrdersStatusFilter", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_UserID", UserId);
                cmd.Parameters.AddWithValue("@_PageID", PageID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrderStatusFilter obj = new OrderStatusFilter
                        {
                            StatusID = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]),
                            StatusName = Convert.ToString(ds.Tables[0].Rows[i]["StatusName"])
                        };


                        orderStatusFilter.Add(obj);
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
            return orderStatusFilter;
        }

        /// <summary>
        /// Get Order Shipment Assigned Details
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="shipmentAssignedFilter"></param>
        /// <returns></returns>
        public ShipmentAssignedDetails GetShipmentAssignedDetails(int TenantId, int UserId, ShipmentAssignedFilterRequest shipmentAssignedFilter)
        {
            DataSet ds = new DataSet();
            ShipmentAssignedDetails objdetails = new ShipmentAssignedDetails();
            objdetails.shipmentAssigned = new List<ShipmentAssigned>();
            objdetails.TotalCount = 0;
            List<ShipmentAssigned> shipmentAssigned = new List<ShipmentAssigned>();
            int TotalCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderShipmentAssigned", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", TenantId);
                cmd.Parameters.AddWithValue("@_UserID", UserId);
                cmd.Parameters.AddWithValue("@_SearchText", shipmentAssignedFilter.SearchText);
                cmd.Parameters.AddWithValue("@_pageno", shipmentAssignedFilter.PageNo);
                cmd.Parameters.AddWithValue("@_pagesize", shipmentAssignedFilter.PageSize);
                cmd.Parameters.AddWithValue("@_FilterReferenceNo", shipmentAssignedFilter.FilterReferenceNo);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ShipmentAssigned obj = new ShipmentAssigned
                        {
                            OrderID = ds.Tables[0].Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"]),
                            AWBNo = ds.Tables[0].Rows[i]["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AWBNo"]),
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            CourierPartner = ds.Tables[0].Rows[i]["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CourierPartner"]),
                            CourierPartnerOrderID = ds.Tables[0].Rows[i]["CourierPartnerOrderID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CourierPartnerOrderID"]),
                            CourierPartnerShipmentID = ds.Tables[0].Rows[i]["CourierPartnerShipmentID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CourierPartnerShipmentID"]),
                            ReferenceNo = ds.Tables[0].Rows[i]["ReferenceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReferenceNo"]),
                            StoreName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]),
                            StaffName = ds.Tables[0].Rows[i]["StaffName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StaffName"]),
                            MobileNumber = ds.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]),
                            IsProceed = ds.Tables[0].Rows[i]["IsProceed"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsProceed"]),
                            ShipmentAWBID = ds.Tables[0].Rows[i]["ShipmentAWBID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShipmentAWBID"])
                        };

                        shipmentAssigned.Add(obj);
                    }
                }

                if (ds != null && ds.Tables[1] != null)
                {
                    TotalCount = ds.Tables[1].Rows[0]["TotalAssignedShipment"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["TotalAssignedShipment"]);
                }

                objdetails.shipmentAssigned = shipmentAssigned;
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
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="shipmentAssignedRequest"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        public int UpdateShipmentAssignedData(ShipmentAssignedRequest shipmentAssignedRequest, int TenantId, int UserId, string ProgramCode, string ClientAPIUrl)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentAssignedData", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ShipmentAWBID", shipmentAssignedRequest.ShipmentAWBID);
                cmd.Parameters.AddWithValue("@_ReferenceNo", shipmentAssignedRequest.ReferenceNo);
                cmd.Parameters.AddWithValue("@_StoreName", shipmentAssignedRequest.StoreName);
                cmd.Parameters.AddWithValue("@_StaffName", shipmentAssignedRequest.StaffName);
                cmd.Parameters.AddWithValue("@_MobileNumber", shipmentAssignedRequest.MobileNumber);
                cmd.Parameters.AddWithValue("@_IsProceed", shipmentAssignedRequest.IsProceed);

                cmd.CommandType = CommandType.StoredProcedure;
                UpdateCount = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (UpdateCount > 0)
                {
                    SmsWhatsUpDataSend(TenantId, UserId, ProgramCode, shipmentAssignedRequest.OrderID, ClientAPIUrl, "AWBAssignedStoreDelivery");
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

            return UpdateCount;
        }

        /// <summary>
        /// Update Shopping Bag Cancel Data
        /// </summary>
        /// <param name="ShoppingID"></param>
        /// <param name="CancelComment"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int UpdateShipmentBagCancelData(int ShoppingID, string CancelComment, int UserId)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentBagCancel", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ShoppingID", ShoppingID);
                cmd.Parameters.AddWithValue("@_CancelComment", CancelComment);
                cmd.Parameters.AddWithValue("@_UserID", UserId);

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
        /// Update Shipment Pickup Pending Data
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public int UpdateShipmentPickupPendingData(int OrderID)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentPickupPending", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_OrderID", OrderID);

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
        /// Insert Convert To Order Details
        /// </summary>
        /// <param name="convertToOrder"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        public int InsertOrderDetails(ConvertToOrder convertToOrder, int TenantId, int UserId, string ProgramCode, string ClientAPIUrl)
        {
            int InsertCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYInsertOrderDetails", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ShoppingID", convertToOrder.ShoppingID);
                cmd.Parameters.AddWithValue("@_InvoiceNo", convertToOrder.InvoiceNo);
                cmd.Parameters.AddWithValue("@_Amount", convertToOrder.Amount);
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_UserID", UserId);

                cmd.CommandType = CommandType.StoredProcedure;
                InsertCount = Convert.ToInt32(cmd.ExecuteScalar());
                if(InsertCount > 0)
                {
                    SmsWhatsUpDataSend(TenantId, UserId, ProgramCode, InsertCount, ClientAPIUrl, "ShoppingBagConvertToOrder");
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

            return InsertCount;
        }

        /// <summary>
        /// Update Address Pending
        /// </summary>
        /// <param name="addressPendingRequest"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int UpdateAddressPending(AddressPendingRequest addressPendingRequest, int TenantId, int UserId)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateAddressPending", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_OrderID", addressPendingRequest.OrderID);
                cmd.Parameters.AddWithValue("@_ShipmentAddress", addressPendingRequest.ShipmentAddress);
                cmd.Parameters.AddWithValue("@_Landmark", addressPendingRequest.Landmark);
                cmd.Parameters.AddWithValue("@_PinCode", addressPendingRequest.PinCode);
                cmd.Parameters.AddWithValue("@_City", addressPendingRequest.City);
                cmd.Parameters.AddWithValue("@_State", addressPendingRequest.State);
                cmd.Parameters.AddWithValue("@_Country", addressPendingRequest.Country);
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_UserID", UserId);

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
        /// Get Order Return Details
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="orderReturnsFilter"></param>
        /// <returns></returns>
        public OrderReturnsDetails GetOrderReturnDetails(int TenantId, int UserId, OrderReturnsFilterRequest orderReturnsFilter)
        {
            DataSet ds = new DataSet();
            OrderReturnsDetails objdetails = new OrderReturnsDetails();

            List<OrderReturns> orderReturns = new List<OrderReturns>();
            int TotalCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderReturnsDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", TenantId);
                cmd.Parameters.AddWithValue("@_UserID", UserId);
                cmd.Parameters.AddWithValue("@_SearchText", orderReturnsFilter.SearchText);
                cmd.Parameters.AddWithValue("@_pageno", orderReturnsFilter.PageNo);
                cmd.Parameters.AddWithValue("@_pagesize", orderReturnsFilter.PageSize);
                cmd.Parameters.AddWithValue("@_FilterStatus", orderReturnsFilter.FilterStatus);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrderReturns obj = new OrderReturns
                        {
                            ReturnID = ds.Tables[0].Rows[i]["ReturnID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ReturnID"]),
                            OrderID = ds.Tables[0].Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"]),
                            AWBNo = ds.Tables[0].Rows[i]["AWBNo"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["AWBNo"]),
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]),
                            MobileNumber = ds.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]),
                            Date = ds.Tables[0].Rows[i]["Date"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["Date"]),
                            Time = ds.Tables[0].Rows[i]["Time"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["Time"]),
                            StatusName = ds.Tables[0].Rows[i]["StatusName"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["StatusName"]),
                            orderReturnsItems = new List<OrderReturnsItem>()
                        };


                        obj.orderReturnsItems = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.OrderID)).Select(x => new OrderReturnsItem()
                        {
                            ItemID = Convert.ToString(x.Field<string>("ItemID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            ItemPrice = x.Field<double>("ItemPrice"),
                            Quantity = x.Field<int>("Quantity")

                        }).ToList();

                        orderReturns.Add(obj);
                    }
                }

                if (ds != null && ds.Tables[2] != null)
                {
                    TotalCount = ds.Tables[2].Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[2].Rows[0]["TotalOrder"]);
                }

                objdetails.orderReturns = orderReturns;
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
        /// Update Shipment Assigned Delivered
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateShipmentAssignedDelivered(int orderID)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentAssignedDelivered", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_OrderID", orderID);

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
        /// Update Shipment Assigned RTO
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateShipmentAssignedRTO(int orderID)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentAssignedRTO", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_OrderID", orderID);

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
        /// Shipment Assigned Print Manifest
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public PrintManifestResponse ShipmentAssignedPrintManifest(Int64 OrderIds, string ClientAPIURL)
        {

            PrintManifestResponse printManifestResponse = new PrintManifestResponse();

            

            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            string ClientAPIResponse = string.Empty;
            try
            {

                #region call client api for print manifest

                PrintManifestRequest printManifestRequest = new PrintManifestRequest();
                printManifestRequest.orderIds = new List<long>();
                printManifestRequest.orderIds.Add(OrderIds);
               

                try
                {
                    string JsonRequest = JsonConvert.SerializeObject(printManifestRequest);

                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ShoppingBag/PrintManifest", JsonRequest);

                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                    {
                        printManifestResponse = JsonConvert.DeserializeObject<PrintManifestResponse>(ClientAPIResponse);
                    }

                }
                catch (Exception)
                {
                    throw;
                }



                #endregion

            }
            catch (Exception)
            {
                throw;
            }

            return printManifestResponse;
        }


        /// <summary>
        /// Shipment Assigned Print Label
        /// </summary>
        /// <param name="ShipmentId"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public PrintLabelResponse ShipmentAssignedPrintLabel(Int64 ShipmentId, string ClientAPIURL)
        {

            PrintLabelResponse printLabelResponse = new PrintLabelResponse();

            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            string ClientAPIResponse = string.Empty;
            try
            {

                #region call client api for print manifest

                PrintLabelRequest printLabelRequest = new PrintLabelRequest();
                printLabelRequest.shipmentId = new List<long>();
                printLabelRequest.shipmentId.Add(ShipmentId);


                try
                {
                    string JsonRequest = JsonConvert.SerializeObject(printLabelRequest);

                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ShoppingBag/GenerateLabel", JsonRequest);

                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                    {
                        printLabelResponse = JsonConvert.DeserializeObject<PrintLabelResponse>(ClientAPIResponse);
                    }

                }
                catch (Exception)
                {
                    throw;
                }



                #endregion

            }
            catch (Exception)
            {
                throw;
            }

            return printLabelResponse;
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public PrintInvoiceResponse ShipmentAssignedPrintInvoice(Int64 OrderIds, string ClientAPIURL)
        {
            PrintInvoiceResponse printInvoiceResponse = new PrintInvoiceResponse();
            string ClientAPIResponse = string.Empty;
            try
            {
                PrintInvoiceRequest printInvoiceRequest = new PrintInvoiceRequest
                {
                    ids = new List<long>
                    {
                        OrderIds
                    }
                };
                try
                {
                    string JsonRequest = JsonConvert.SerializeObject(printInvoiceRequest);

                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ShoppingBag/PrintInvoice", JsonRequest);

                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                    {
                        printInvoiceResponse = JsonConvert.DeserializeObject<PrintInvoiceResponse>(ClientAPIResponse);
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return printInvoiceResponse;
        }
    }
}
