using ClientAPIServiceCall;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class HSOrderService : IHSOrder
    {
        #region Variable Declaration
        MySqlConnection conn = new MySqlConnection();
        string apiResponse = string.Empty;
        string apiResponse1 = string.Empty;
        string apiURL = string.Empty;
        string apiURLGetUserATVDetails = string.Empty;
        string _connectionStringClass = "";
        OrderURLList _OrderURLList;

        ClientHttpClientService _clienthttpclientservice;
        GeneratePaymentLinkHttpClientService _generatepaymentlinkhttpclientservice;

        PhygitalHttpClientService _phygitalhttpclientservice;

        ChatbotBellHttpClientService _APICall = null;
        WebBotHttpClientService _WebBot = null;
        MaxWebBotHttpClientService _MaxHSM = null;
        GenerateToken _generateToken = null;

        ILogger<object> _logger;
        #endregion

        #region Constructor
        public HSOrderService(string _connectionString, OrderURLList orderURLList = null, ClientHttpClientService clienthttpclientservice = null,
            GeneratePaymentLinkHttpClientService generatepaymentlinkhttpclientservice = null,
            PhygitalHttpClientService phygitalhttpclientservice = null,
            ChatbotBellHttpClientService APICall = null, WebBotHttpClientService WebBot = null, MaxWebBotHttpClientService MaxHSM = null,
            ILogger<object> logger = null, GenerateToken generateToken = null)
        {
            conn.ConnectionString = _connectionString;
            _connectionStringClass = _connectionString;
            _OrderURLList = orderURLList;

            _clienthttpclientservice = clienthttpclientservice;

            _generatepaymentlinkhttpclientservice = generatepaymentlinkhttpclientservice;

            _phygitalhttpclientservice = phygitalhttpclientservice;

            _APICall = APICall;
            _logger = logger;
            _WebBot = WebBot;
            _MaxHSM = MaxHSM;

            _generateToken = generateToken;
        }
        #endregion


        /// <summary>
        /// Get Order Configuration
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public async Task<OrderConfiguration> GetOrderConfiguration(int tenantId)
        {
            OrderConfiguration moduleConfiguration = new OrderConfiguration();
            List<PHYOrderMessageTemplate> pHYOrderMessages = new List<PHYOrderMessageTemplate>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderConfiguration", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_tenantID", tenantId);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            moduleConfiguration = new OrderConfiguration
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                IntegratedSystem = dr["IntegratedSystem"] == DBNull.Value ? false : Convert.ToBoolean(dr["IntegratedSystem"]),
                                Payment = dr["Payment"] == DBNull.Value ? false : Convert.ToBoolean(dr["Payment"]),
                                Shipment = dr["Shipment"] == DBNull.Value ? false : Convert.ToBoolean(dr["Shipment"]),
                                ShoppingBag = dr["ShoppingBag"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShoppingBag"]),
                                EnableClickAfterValue = dr["EnableClickAfterValue"] == DBNull.Value ? 0 : Convert.ToInt16(dr["EnableClickAfterValue"]),
                                EnableClickAfterDuration = dr["EnableClickAfterDuration"] == DBNull.Value ? "" : Convert.ToString(dr["EnableClickAfterDuration"]),
                                StoreDelivery = dr["StoreDelivery"] == DBNull.Value ? false : Convert.ToBoolean(dr["StoreDelivery"]),
                                AlertCommunicationviaWhtsup = dr["AlertCommunicationviaWhtsup"] == DBNull.Value ? false : Convert.ToBoolean(dr["AlertCommunicationviaWhtsup"]),
                                AlertCommunicationviaSMS = dr["AlertCommunicationviaSMS"] == DBNull.Value ? false : Convert.ToBoolean(dr["AlertCommunicationviaSMS"]),
                                AlertCommunicationSMSText = dr["AlertCommunicationSMSText"] == DBNull.Value ? "" : Convert.ToString(dr["AlertCommunicationSMSText"]),
                                PaymentTenantCodeText = dr["TenderPayRemainingText"] == DBNull.Value ? "" : Convert.ToString(dr["TenderPayRemainingText"]),
                                RetryCount = dr["RetryCount"] == DBNull.Value ? 0 : Convert.ToInt16(dr["RetryCount"]),
                                StateFlag = dr["StateFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["StateFlag"]),
                                CurrencyText = dr["Currency"] == DBNull.Value ? "" : Convert.ToString(dr["Currency"]),
                                CancelButtonInShipment = dr["CancelButtonInShipment"] == DBNull.Value ? false : Convert.ToBoolean(dr["CancelButtonInShipment"]),
                                TemplateFlag = dr["TemplateFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["TemplateFlag"]),
                                IsPushToPoss = dr["IsPushToPoss"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsPushToPoss"]),
                                IsPODAccept = dr["IsPODAccept"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsPODAccept"]),
                                EnableCheckService = dr["EnableCheckService"] == DBNull.Value ? false : Convert.ToBoolean(dr["EnableCheckService"]),
                                ShowShipmentCharges = dr["ShowShipmentCharges"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShowShipmentCharges"]),
                                ShowItemProperty = dr["ShowItemProperty"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShowItemProperty"]),
                                ShowSelfPickupTab = dr["ShowSelfPickupTab"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShowSelfPickupTab"]),
                                ShowAvailableQuantity = dr["ShowAvailableQuantity"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShowAvailableQuantity"])
                            };
                        }
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                PHYOrderMessageTemplate obj = new PHYOrderMessageTemplate
                                {
                                    ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                    MessageName = dr["MessageName"] == DBNull.Value ? "" : Convert.ToString(dr["MessageName"]),
                                    IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
                                    Description = dr["Description"] == DBNull.Value ? "" : Convert.ToString(dr["Description"]),
                                    StoreDeliveryIsActive = dr["StoreDeliveryIsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["StoreDeliveryIsActive"]),
                                    StoreDeliveryDescription = dr["StoreDeliveryDescription"] == DBNull.Value ? "" : Convert.ToString(dr["StoreDeliveryDescription"])
                                };
                                pHYOrderMessages.Add(obj);
                            }
                            moduleConfiguration.pHYOrderMessageTemplates = pHYOrderMessages;
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
            return moduleConfiguration;
        }

        /// <summary>
        /// Update Order Configuration
        /// </summary>
        /// <param name="orderConfiguration"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateOrderConfiguration(OrderConfiguration orderConfiguration, int modifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    cmd.Parameters.AddWithValue("@_StoreDeliveryText", orderConfiguration.StoreDeliveryText);
                    cmd.Parameters.AddWithValue("@_PaymentTenantCodeText", orderConfiguration.PaymentTenantCodeText);
                    cmd.Parameters.AddWithValue("@_RetryCount", orderConfiguration.RetryCount);
                    cmd.Parameters.AddWithValue("@_StateFlag", orderConfiguration.StateFlag);
                    cmd.Parameters.AddWithValue("@_CancelButtonInShipment", orderConfiguration.CancelButtonInShipment);
                    cmd.Parameters.AddWithValue("@_IsPushToPoss", orderConfiguration.IsPushToPoss);
                    cmd.Parameters.AddWithValue("@_CurrencyText", orderConfiguration.CurrencyText);
                    cmd.Parameters.AddWithValue("@_ModifiedBy", modifiedBy);
                    cmd.Parameters.AddWithValue("@_TemplateFlag", orderConfiguration.TemplateFlag);
                    cmd.Parameters.AddWithValue("@_IsPODAccept", orderConfiguration.IsPODAccept);
                    cmd.Parameters.AddWithValue("@_EnableCheckService", orderConfiguration.EnableCheckService);
                    cmd.Parameters.AddWithValue("@_ShowShipmentCharges", orderConfiguration.ShowShipmentCharges);
                    cmd.Parameters.AddWithValue("@_ShowItemProperty", orderConfiguration.ShowItemProperty);
                    cmd.Parameters.AddWithValue("@_ShowSelfPickupTab", orderConfiguration.ShowSelfPickupTab);
                    cmd.Parameters.AddWithValue("@_ShowAvailableQuantity", orderConfiguration.ShowAvailableQuantity);
                    cmd.CommandType = CommandType.StoredProcedure;
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
        /// Update Order Configuration Message Template
        /// </summary>
        /// <param name="pHYOrderMessageTemplates"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public async Task<int> UpdateOrderConfigurationMessageTemplate(List<PHYOrderMessageTemplate> pHYOrderMessageTemplates, int tenantID)
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
                    for (int i = 0; i < pHYOrderMessageTemplates.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateOrderMessageTemplate", conn)
                        {
                            Connection = conn
                        };
                        cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                        cmd.Parameters.AddWithValue("@_TemplateId", pHYOrderMessageTemplates[i].ID);
                        cmd.Parameters.AddWithValue("@_IsActive", pHYOrderMessageTemplates[i].IsActive);
                        cmd.Parameters.AddWithValue("@_Description", pHYOrderMessageTemplates[i].Description);
                        cmd.Parameters.AddWithValue("@_StoreDeliveryIsActive", pHYOrderMessageTemplates[i].StoreDeliveryIsActive);
                        cmd.Parameters.AddWithValue("@_StoreDeliveryDescription", pHYOrderMessageTemplates[i].StoreDeliveryDescription);
                        cmd.CommandType = CommandType.StoredProcedure;
                        updateCount += Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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

        /// <summary>
        /// Get Whatsapp Template
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="MessageName"></param>
        /// <returns></returns>
        public async Task<List<PHYWhatsAppTemplate>> GetWhatsappTemplate(int tenantID, int userID, string messageName)
        {
            List<PHYWhatsAppTemplate> pHYWhatsAppTemplates = new List<PHYWhatsAppTemplate>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetWhatsupTemplate", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantId", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_MessageName", messageName);
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            PHYWhatsAppTemplate obj = new PHYWhatsAppTemplate
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                MessageName = dr["MessageName"] == DBNull.Value ? "" : Convert.ToString(dr["MessageName"]),
                                TemplateName = dr["TemplateName"] == DBNull.Value ? "" : Convert.ToString(dr["TemplateName"]),
                                Status = dr["Status"] == DBNull.Value ? false : Convert.ToBoolean(dr["Status"])
                            };

                            pHYWhatsAppTemplates.Add(obj);
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
            return pHYWhatsAppTemplates;
        }

        /// <summary>
        /// Update Whatsapp Template
        /// </summary>
        /// <param name="pHYWhatsAppTemplates"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public async Task<int> UpdateWhatsappTemplate(List<PHYWhatsAppTemplate> pHYWhatsAppTemplates, int tenantID)
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
                    for (int i = 0; i < pHYWhatsAppTemplates.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateWhatsupTemplate", conn)
                        {
                            Connection = conn
                        };
                        cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                        cmd.Parameters.AddWithValue("@_TemplateId", pHYWhatsAppTemplates[i].ID);
                        cmd.Parameters.AddWithValue("@_TemplateName", pHYWhatsAppTemplates[i].TemplateName);

                        cmd.CommandType = CommandType.StoredProcedure;
                        updateCount += Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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

        /// <summary>
        /// Get Order Delivered Details
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="orderDeliveredFilter"></param>
        /// <returns></returns>
        public async Task<OrderDeliveredDetails> GetOrderDeliveredDetails(int tenantID, int userID, OrderDeliveredFilterRequest orderDeliveredFilter)
        {
            OrderDeliveredDetails objdetails = new OrderDeliveredDetails();
            List<OrderDelivered> orderDelivered = new List<OrderDelivered>();
            DataTable orderDeliveredTable = new DataTable();
            DataTable orderDeliveredItemsTable = new DataTable();
            DataTable totalTable = new DataTable();
            int TotalCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderDeliveredDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_SearchText", orderDeliveredFilter.SearchText);
                    cmd.Parameters.AddWithValue("@_pageno", orderDeliveredFilter.PageNo);
                    cmd.Parameters.AddWithValue("@_pagesize", orderDeliveredFilter.PageSize);
                    cmd.Parameters.AddWithValue("@_FilterStatus", orderDeliveredFilter.FilterStatus);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            orderDeliveredTable.Load(reader);
                            orderDeliveredItemsTable.Load(reader);
                            totalTable.Load(reader);
                        }
                    }

                    if (orderDeliveredTable != null)
                    {
                        for (int i = 0; i < orderDeliveredTable.Rows.Count; i++)
                        {
                            OrderDelivered obj = new OrderDelivered
                            {
                                ID = orderDeliveredTable.Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(orderDeliveredTable.Rows[i]["ID"]),
                                InvoiceNo = orderDeliveredTable.Rows[i]["InvoiceNo"] == DBNull.Value ? "" : Convert.ToString(orderDeliveredTable.Rows[i]["InvoiceNo"]),
                                CustomerName = orderDeliveredTable.Rows[i]["CustomerName"] == DBNull.Value ? "" : Convert.ToString(orderDeliveredTable.Rows[i]["CustomerName"]),
                                MobileNumber = orderDeliveredTable.Rows[i]["MobileNumber"] == DBNull.Value ? "" : Convert.ToString(orderDeliveredTable.Rows[i]["MobileNumber"]),
                                Date = orderDeliveredTable.Rows[i]["Date"] == DBNull.Value ? "" : Convert.ToString(orderDeliveredTable.Rows[i]["Date"]),
                                Time = orderDeliveredTable.Rows[i]["Time"] == DBNull.Value ? "" : Convert.ToString(orderDeliveredTable.Rows[i]["Time"]),
                                StatusName = orderDeliveredTable.Rows[i]["StatusName"] == DBNull.Value ? "" : Convert.ToString(orderDeliveredTable.Rows[i]["StatusName"]),
                                ActionTypeName = orderDeliveredTable.Rows[i]["ActionTypeName"] == DBNull.Value ? "" : Convert.ToString(orderDeliveredTable.Rows[i]["ActionTypeName"]),
                                orderDeliveredItems = new List<OrderDeliveredItem>()
                            };
                            obj.orderDeliveredItems = orderDeliveredItemsTable.AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrderDeliveredItem()
                            {
                                ItemID = Convert.ToString(x.Field<string>("ItemID")),
                                ItemName = Convert.ToString(x.Field<string>("ItemName")),
                                ItemPrice = x.Field<string>("ItemPrice"),
                                Quantity = x.Field<int>("Quantity")

                            }).ToList();
                            orderDelivered.Add(obj);
                        }
                    }

                    if (totalTable != null)
                    {
                        if (totalTable.Rows.Count > 0)
                        {
                            TotalCount = totalTable.Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(totalTable.Rows[0]["TotalOrder"]);
                        }
                    }
                    objdetails.orderDelivereds = orderDelivered;
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
                if (orderDeliveredTable != null)
                {
                    orderDeliveredTable.Dispose();
                }
                if (orderDeliveredItemsTable != null)
                {
                    orderDeliveredItemsTable.Dispose();
                }
                if (totalTable != null)
                {
                    totalTable.Dispose();
                }
            }
            return objdetails;
        }

        /// <summary>
        /// Get Order Status Filters
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="userID"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public async Task<List<OrderStatusFilter>> GetOrderStatusFilter(int tenantID, int userID, int pageID)
        {
            List<OrderStatusFilter> orderStatusFilter = new List<OrderStatusFilter>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrdersStatusFilter", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_PageID", pageID);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            OrderStatusFilter obj = new OrderStatusFilter
                            {
                                StatusID = Convert.ToInt32(dr["StatusID"]),
                                StatusName = Convert.ToString(dr["StatusName"])
                            };
                            orderStatusFilter.Add(obj);
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
            return orderStatusFilter;
        }

        /// <summary>
        /// Get Order Shipment Assigned Details
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="shipmentAssignedFilter"></param>
        /// <returns></returns>
        public async Task<ShipmentAssignedDetails> GetShipmentAssignedDetails(int tenantID, int userID, ShipmentAssignedFilterRequest shipmentAssignedFilter)
        {
            ShipmentAssignedDetails objdetails = new ShipmentAssignedDetails
            {
                shipmentAssigned = new List<ShipmentAssigned>(),
                TotalCount = 0
            };
            List<ShipmentAssigned> shipmentAssigned = new List<ShipmentAssigned>();
            int TotalCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderShipmentAssigned", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_SearchText", shipmentAssignedFilter.SearchText);
                    cmd.Parameters.AddWithValue("@_pageno", shipmentAssignedFilter.PageNo);
                    cmd.Parameters.AddWithValue("@_pagesize", shipmentAssignedFilter.PageSize);
                    cmd.Parameters.AddWithValue("@_FilterReferenceNo", shipmentAssignedFilter.FilterReferenceNo);
                    cmd.Parameters.AddWithValue("@_CourierPartner", shipmentAssignedFilter.CourierPartner);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            ShipmentAssigned obj = new ShipmentAssigned
                            {
                                OrderID = dr["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrderID"]),
                                AWBNo = dr["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AWBNo"]),
                                InvoiceNo = dr["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["InvoiceNo"]),
                                CourierPartner = dr["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CourierPartner"]),
                                CourierPartnerOrderID = dr["CourierPartnerOrderID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CourierPartnerOrderID"]),
                                CourierPartnerShipmentID = dr["CourierPartnerShipmentID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CourierPartnerShipmentID"]),
                                ReferenceNo = dr["ReferenceNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ReferenceNo"]),
                                StoreName = dr["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreName"]),
                                StaffName = dr["StaffName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StaffName"]),
                                MobileNumber = dr["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MobileNumber"]),
                                IsProceed = dr["IsProceed"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsProceed"]),
                                ShipmentAWBID = dr["ShipmentAWBID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ShipmentAWBID"]),
                                ShowOnlyPrintLabel = dr["ShowOnlyPrintLabel"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShowOnlyPrintLabel"])
                            };
                            shipmentAssigned.Add(obj);
                        }
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                TotalCount = dr["TotalAssignedShipment"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalAssignedShipment"]);
                            }
                        }
                        objdetails.shipmentAssigned = shipmentAssigned;
                        objdetails.TotalCount = TotalCount;
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
        /// Update Shipment Assigned Staff Details Of Store Delivery
        /// </summary>
        /// <param name="shipmentAssignedRequest"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedData(ShipmentAssignedRequest shipmentAssignedRequest, int tenantID, int userID, string programCode, string ClientAPIUrl, WebBotContentRequest webBotcontentRequest)
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
                    updateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
                    if (updateCount > 0)
                    {
                        await SmsWhatsUpDataSend(tenantID, userID, programCode, shipmentAssignedRequest.OrderID, ClientAPIUrl, "AWBAssignedStoreDelivery", webBotcontentRequest);
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

        /// <summary>
        /// Update Shopping Bag Cancel Data
        /// </summary>
        /// <param name="ShoppingID"></param>
        /// <param name="CancelComment"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentBagCancelData(int shoppingID, string cancelComment, int userID, bool sharewithCustomer, WebBotContentRequest webBotcontentRequest)
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
                    MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentBagCancel", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_ShoppingID", shoppingID);
                    cmd.Parameters.AddWithValue("@_CancelComment", cancelComment);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_sharewithCustomer", sharewithCustomer);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            updateCount = dr["rowcount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["rowcount"]);
                            if(webBotcontentRequest.webBotHSMSetting != null)
                            {
                                if (webBotcontentRequest.webBotHSMSetting.Programcode.ToLower().Equals(webBotcontentRequest.ProgramCode.ToLower()))
                                {
                                    webBotcontentRequest.MaxHSMRequest.body.from = dr["WabaNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["WabaNumber"]);
                                }
                            }
                        }
                    }
                    //updateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());

                    if (sharewithCustomer)
                    {
                        if (updateCount > 0)
                        {
                            try
                            {
                                HSWebBotService HSWebBotService = new HSWebBotService(_connectionStringClass, _APICall, _WebBot, _MaxHSM, _logger);
                                await HSWebBotService.SendWebBotHSM(webBotcontentRequest);
                            }
                            catch (Exception)
                            {
                                throw;
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

            return updateCount;
        }

        /// <summary>
        /// Update Shipment Pickup Pending Data
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentPickupPendingData(int orderID)
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
                    MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentPickupPending", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_OrderID", orderID);
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
        /// Insert Convert To Order Details
        /// </summary>
        /// <param name="convertToOrder"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        public async Task<int> InsertOrderDetails(ConvertToOrder convertToOrder, int tenantID, int userID, string programCode, string clientAPIUrl, WebBotContentRequest webBotcontentRequest)
        {
            int insertCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYInsertOrderDetails", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_ShoppingID", convertToOrder.ShoppingID);
                    cmd.Parameters.AddWithValue("@_InvoiceNo", convertToOrder.InvoiceNo);
                    cmd.Parameters.AddWithValue("@_Amount", convertToOrder.Amount);
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    insertCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    if (insertCount > 0)
                    {
                        await SmsWhatsUpDataSend(tenantID, userID, programCode, insertCount, clientAPIUrl, "ShoppingBagConvertToOrder", webBotcontentRequest);
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

            return insertCount;
        }

        /// <summary>
        /// Get Push Order To Poss
        /// </summary>
        /// <param name="convertToOrder"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <returns></returns>
        public async Task<MessageData> PushOrderToPoss(ConvertToOrder pushToPoss, int tenantId, int userID, string programCode, string phygitalClientAPIURL)
        {
            MessageData responseData = null;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetPushOrderToPoss", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_ShoppingID", pushToPoss.ShoppingID);
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    PushOrderRequest pushOrdRequest = new PushOrderRequest();
                    PushOrderResponse pushOrderResponse = new PushOrderResponse();
                    
                    DataTable ShoppingItems = new DataTable();
                    List<LineItem> lineItemList = new List<LineItem>();

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            pushOrdRequest = new PushOrderRequest
                            {
                                source = dr["Source"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Source"]),
                                ouCode = dr["OuCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OuCode"]),
                                storeCode = dr["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreCode"]),
                                mobileNumber = dr["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MobileNumber"]),
                                orderNumber = dr["OrderNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrderNumber"]),
                                orderStatus = dr["OrderStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrderStatus"]),
                                orderDate = dr["OrderDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrderDate"]),
                                orderTime = dr["OrderTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrderTime"]),
                                customerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                countryCode = dr["CountryCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CountryCode"]),
                                isCountryCodeRemove = dr["isCountryCodeRemove"] == DBNull.Value ? false : Convert.ToBoolean(dr["isCountryCodeRemove"]),
                                mobileNoLength = dr["MobileNoLength"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MobileNoLength"]),
                                tenantCode = dr["tenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["tenantCode"])
                            };
                            pushOrdRequest.customerAddress = new CustomerAddress
                            {
                                address = dr["Address"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Address"]),
                                city = dr["City"] == DBNull.Value ? string.Empty : Convert.ToString(dr["City"]),
                                state = dr["State"] == DBNull.Value ? string.Empty : Convert.ToString(dr["State"]),
                                pinCode = dr["PinCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PinCode"])
                            };

                        }
                        if (dr.NextResult())
                        {                        
                            if (dr.HasRows)
                            {
                                ShoppingItems.Load(dr);
                                foreach (DataRow reader in ShoppingItems.Rows)
                                {
                                    int quantity = reader["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Quantity"]);

                                    for (int i = 0; i < quantity; i++)
                                    {
                                        LineItem listObj = new LineItem()
                                        {
                                            itemCode = reader["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemCode"]),
                                            itemDesc = reader["ItemDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemDesc"]),
                                            itemSize = reader["ItemSize"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemSize"]),
                                            itemMRP = reader["ItemMRP"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemMRP"]),
                                            itemBrandDesc = reader["ItemBrandDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemBrandDesc"]),
                                            itemColor = reader["ItemColor"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemColor"]),
                                            itemSellingPrice = reader["ItemSellingPrice"] == DBNull.Value ? 0 : Convert.ToDouble(reader["ItemSellingPrice"]),
                                            itemExtSellingPrice = reader["ItemExtSellingPrice"] == DBNull.Value ? 0 : Convert.ToDouble(reader["ItemExtSellingPrice"]),
                                            itemDiscountPrice = reader["ItemDiscountPrice"] == DBNull.Value ? 0 : Convert.ToDouble(reader["ItemDiscountPrice"]),
                                            quantity = 1,
                                            promoCode = reader["PromoCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PromoCode"]),
                                            promoDesc = reader["PromoDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PromoDesc"])
                                        };
                                        lineItemList.Add(listObj);
                                    }

                                }
                                pushOrdRequest.lineItems = lineItemList;
                            }
                        }
                        string apiReq = JsonConvert.SerializeObject(pushOrdRequest);
                        apiResponse = await _phygitalhttpclientservice.SendApiRequest(phygitalClientAPIURL + _OrderURLList.GetPushOrderToPoss, apiReq);
                        pushOrderResponse = JsonConvert.DeserializeObject<PushOrderResponse>(apiResponse);
                        if (pushOrderResponse != null)
                        {
                            if (pushOrderResponse.data.resultCode == "SUCCESS")
                            {
                                responseData = new MessageData
                                {
                                    result= pushOrderResponse.data.result,
                                    resultCode = "SUCCESS",
                                    message = pushOrderResponse.data.message
                                };
                              int success=await UpdatePosPushed(pushToPoss.ShoppingID);
                            }
                            else
                            {
                                responseData = new MessageData
                                {
                                    result = "False",
                                    resultCode = "Falied",
                                    message = "Order Failed"
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
            }

            return responseData;
        }

        /// <summary>
        /// Update Address Pending
        /// </summary>
        /// <param name="addressPendingRequest"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<int> UpdateAddressPending(AddressPendingRequest addressPendingRequest, int tenantId, int userID)
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
                    cmd.Parameters.AddWithValue("@_CustomerName", addressPendingRequest.CustomerName);
                    cmd.Parameters.AddWithValue("@_MobileNumber", addressPendingRequest.MobileNumber);
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userID);

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
        /// Get Order Return Details
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="orderReturnsFilter"></param>
        /// <returns></returns>
        public async Task<OrderReturnsDetails> GetOrderReturnDetails(int tenantID, int userID, OrderReturnsFilterRequest orderReturnsFilter)
        {
            OrderReturnsDetails objdetails = new OrderReturnsDetails();
            List<OrderReturns> orderReturns = new List<OrderReturns>();
            DataTable orderReturnsTable = new DataTable();
            DataTable orderReturnsItemsTable = new DataTable();
            DataTable totalTable = new DataTable();
            DataTable retryCountTable = new DataTable();
            int totalCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderReturnsDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_SearchText", orderReturnsFilter.SearchText);
                    cmd.Parameters.AddWithValue("@_pageno", orderReturnsFilter.PageNo);
                    cmd.Parameters.AddWithValue("@_pagesize", orderReturnsFilter.PageSize);
                    cmd.Parameters.AddWithValue("@_FilterStatus", orderReturnsFilter.FilterStatus);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            orderReturnsTable.Load(reader);
                            orderReturnsItemsTable.Load(reader);
                            totalTable.Load(reader);
                            retryCountTable.Load(reader);
                        }
                    }

                    if (orderReturnsTable != null)
                    {
                        for (int i = 0; i < orderReturnsTable.Rows.Count; i++)
                        {
                            OrderReturns obj = new OrderReturns
                            {
                                ReturnID = orderReturnsTable.Rows[i]["ReturnID"] == DBNull.Value ? 0 : Convert.ToInt32(orderReturnsTable.Rows[i]["ReturnID"]),
                                OrderID = orderReturnsTable.Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(orderReturnsTable.Rows[i]["OrderID"]),
                                AWBNo = orderReturnsTable.Rows[i]["AWBNo"] == DBNull.Value ? "" : Convert.ToString(orderReturnsTable.Rows[i]["AWBNo"]),
                                InvoiceNo = orderReturnsTable.Rows[i]["InvoiceNo"] == DBNull.Value ? "" : Convert.ToString(orderReturnsTable.Rows[i]["InvoiceNo"]),
                                CustomerName = orderReturnsTable.Rows[i]["CustomerName"] == DBNull.Value ? "" : Convert.ToString(orderReturnsTable.Rows[i]["CustomerName"]),
                                MobileNumber = orderReturnsTable.Rows[i]["MobileNumber"] == DBNull.Value ? "" : Convert.ToString(orderReturnsTable.Rows[i]["MobileNumber"]),
                                Date = orderReturnsTable.Rows[i]["Date"] == DBNull.Value ? "" : Convert.ToString(orderReturnsTable.Rows[i]["Date"]),
                                Time = orderReturnsTable.Rows[i]["Time"] == DBNull.Value ? "" : Convert.ToString(orderReturnsTable.Rows[i]["Time"]),
                                StatusId = orderReturnsTable.Rows[i]["StatusID"] == DBNull.Value ? 0 : Convert.ToInt32(orderReturnsTable.Rows[i]["StatusID"]),
                                StatusName = orderReturnsTable.Rows[i]["StatusName"] == DBNull.Value ? "" : Convert.ToString(orderReturnsTable.Rows[i]["StatusName"]),
                                RetryCount = orderReturnsTable.Rows[i]["RetryCount"] == DBNull.Value ? 0 : Convert.ToInt32(orderReturnsTable.Rows[i]["RetryCount"]),
                                IsCancelled = orderReturnsTable.Rows[i]["IsCancelled"] == DBNull.Value ? false : Convert.ToBoolean(orderReturnsTable.Rows[i]["IsCancelled"]),
                                orderReturnsItems = new List<OrderReturnsItem>()
                            };

                            if ((retryCountTable.Rows[0]["RetryCount"] == DBNull.Value ? 0 : Convert.ToInt32(retryCountTable.Rows[0]["RetryCount"])) > 0)
                            {
                                if (obj.RetryCount == (retryCountTable.Rows[0]["RetryCount"] == DBNull.Value ? 0 : Convert.ToInt32(retryCountTable.Rows[0]["RetryCount"])))
                                {
                                    obj.IsRetry = false;
                                }
                                else
                                {
                                    obj.IsRetry = true;
                                }
                            }

                            obj.orderReturnsItems = orderReturnsItemsTable.AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.OrderID)).Select(x => new OrderReturnsItem()
                            {
                                ItemID = Convert.ToString(x.Field<string>("ItemID")),
                                ItemName = Convert.ToString(x.Field<string>("ItemName")),
                                ItemPrice = x.Field<string>("ItemPrice"),
                                Quantity = x.Field<int>("Quantity")

                            }).ToList();

                            orderReturns.Add(obj);
                        }
                    }

                    if (totalTable != null)
                    {
                        if (totalTable.Rows.Count > 0)
                        {
                            totalCount = totalTable.Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(totalTable.Rows[0]["TotalOrder"]);
                        }
                    }

                    objdetails.orderReturns = orderReturns;
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
                if (orderReturnsTable != null)
                {
                    orderReturnsTable.Dispose();
                }
                if (orderReturnsItemsTable != null)
                {
                    orderReturnsItemsTable.Dispose();
                }
                if (totalTable != null)
                {
                    totalTable.Dispose();
                }
                if (retryCountTable != null)
                {
                    retryCountTable.Dispose();
                }
            }
            return objdetails;
        }

        /// <summary>
        /// Update Shipment Assigned Delivered
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedDelivered(int orderID)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentAssignedDelivered", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_OrderID", orderID);

                    cmd.CommandType = CommandType.StoredProcedure;
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
        /// Update Shipment Assigned RTO
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public async Task<int> UpdateShipmentAssignedRTO(int orderID)
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
                    MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentAssignedRTO", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_OrderID", orderID);

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
        /// Shipment Assigned Print Manifest
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<PrintManifestResponse> ShipmentAssignedPrintManifest(int orderIds, string ClientAPIURL)
        {
            PrintManifestResponse printManifestResponse = new PrintManifestResponse();
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            string ClientAPIResponse = string.Empty;
            try
            {
                PrintManifestRequest printManifestRequest = new PrintManifestRequest
                {
                    orderIds = new List<int>
                    {
                        orderIds
                    }
                };
                string JsonRequest = JsonConvert.SerializeObject(printManifestRequest);
                ClientAPIResponse = await _phygitalhttpclientservice.SendApiRequest(ClientAPIURL + _OrderURLList.Printmanifest, JsonRequest);

                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    printManifestResponse = JsonConvert.DeserializeObject<PrintManifestResponse>(ClientAPIResponse);
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
            return printManifestResponse;
        }

        /// <summary>
        /// Shipment Assigned Print Label
        /// </summary>
        /// <param name="ShipmentId"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<PrintLabelResponse> ShipmentAssignedPrintLabel(int shipmentId, string ClientAPIURL)
        {
            PrintLabelResponse printLabelResponse = new PrintLabelResponse();
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            string ClientAPIResponse = string.Empty;
            try
            {
                PrintLabelRequest printLabelRequest = new PrintLabelRequest
                {
                    shipmentId = new List<int>
                    {
                        shipmentId
                    }
                };
                string JsonRequest = JsonConvert.SerializeObject(printLabelRequest);
                ClientAPIResponse = await _phygitalhttpclientservice.SendApiRequest(ClientAPIURL + _OrderURLList.Generatelabel, JsonRequest);
                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    printLabelResponse = JsonConvert.DeserializeObject<PrintLabelResponse>(ClientAPIResponse);
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
            return printLabelResponse;
        }

        /// <summary>
        /// ShipmentAssignedPrintInvoice
        /// </summary>
        /// <param name="OrderIds"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<PrintInvoiceResponse> ShipmentAssignedPrintInvoice(int orderIds, string ClientAPIURL)
        {
            PrintInvoiceResponse printInvoiceResponse = new PrintInvoiceResponse();
            string ClientAPIResponse = string.Empty;
            try
            {
                PrintInvoiceRequest printInvoiceRequest = new PrintInvoiceRequest
                {
                    ids = new List<int>
                    {
                        orderIds
                    }
                };
                string JsonRequest = JsonConvert.SerializeObject(printInvoiceRequest);
                ClientAPIResponse = await _phygitalhttpclientservice.SendApiRequest(ClientAPIURL + _OrderURLList.Printinvoice, JsonRequest);

                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    printInvoiceResponse = JsonConvert.DeserializeObject<PrintInvoiceResponse>(ClientAPIResponse);
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

            return printInvoiceResponse;
        }

        /// <summary>
        /// Send SMS and Whatsup On Return Cancel
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="OrderId"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        public async Task<int> SendSMSWhatsupOnReturnCancel(int tenantId, int userID, string programCode, int orderId, string ClientAPIURL, WebBotContentRequest webBotcontentRequest)
        {
            bool cancel = false;
            int result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetCancelSMSWhatupText", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            cancel = dr["Cancel"] == DBNull.Value ? false : Convert.ToBoolean(dr["Cancel"]);
                        }
                        if (cancel)
                        {
                            result = await SmsWhatsUpDataSend(tenantId, userID, programCode, orderId, ClientAPIURL, "Cancelled", webBotcontentRequest);

                            if (result > 0)
                            {
                                await UpdateCancelSMSWhatsAppSend(orderId);
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
            return result;
        }

        /// <summary>
        /// UpdateCancelSMSWhatsAppSend
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public async Task<int> UpdateCancelSMSWhatsAppSend(int orderID)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateCancelSMSWhatsAppSendFlag", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_OrderId", orderID);

                    cmd.CommandType = CommandType.StoredProcedure;
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
        /// Update On Return Retry
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="StatusId"></param>
        /// <param name="AWBNo"></param>
        /// <returns></returns>
        public async Task<int> UpdateOnReturnRetry(int orderID, int statusID, string AWBNo, int returnID)
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
                    MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateOnReturnTabRetry", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_OrderID", orderID);
                    cmd.Parameters.AddWithValue("@_StatusID", statusID);
                    cmd.Parameters.AddWithValue("@_AWBNo", AWBNo);
                    cmd.Parameters.AddWithValue("@_ReturnID", returnID);
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
        /// UpdatePosPushed
        /// </summary>
        /// <param name="shoppingBagID"></param>
        /// <returns></returns>
        public async Task<int> UpdatePosPushed(int shoppingBagID)
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
                    MySqlCommand cmd = new MySqlCommand("SP_PhyUpdateIsPosPushed", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_ShoppingID", shoppingBagID);
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
    }
}
