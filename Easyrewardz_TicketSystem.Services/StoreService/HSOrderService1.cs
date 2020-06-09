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
    public partial class HSOrderService : IHSOrder
    {
        MySqlConnection conn = new MySqlConnection();
        private IConfiguration configuration;
        CustomResponse ApiResponse = null;
        string apiResponse = string.Empty;
        string apiResponse1 = string.Empty;
        string apisecurityToken = string.Empty;
        string apiURL = string.Empty;
        string apiURLGetUserATVDetails = string.Empty;

        public HSOrderService(string _connectionString)
        {

            conn.ConnectionString = _connectionString;
            apisecurityToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJQcm9ncmFtQ29kZSI6IkJhdGEiLCJVc2VySUQiOiIzIiwiQXBwSUQiOiI3IiwiRGF5IjoiMjgiLCJNb250aCI6IjMiLCJZZWFyIjoiMjAyMSIsIlJvbGUiOiJBZG1pbiIsImlzcyI6IkF1dGhTZWN1cml0eUlzc3VlciIsImF1ZCI6IkF1dGhTZWN1cml0eUF1ZGllbmNlIn0.0XeF7V5LWfQn0NlSlG7Rb-Qq1hUCtUYRDg6dMGIMvg0";
            //apiURLGetUserATVDetails = configuration.GetValue<string>("apiURLGetUserATVDetails");
        }

        public ModuleConfiguration GetModuleConfiguration(int tenantId, int userId, string programCode)
        {
            DataSet ds = new DataSet();
            ModuleConfiguration moduleConfiguration = new ModuleConfiguration();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetModuleConfiguration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@_prgramCode", programCode);

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
                        moduleConfiguration.ShoppingBag = ds.Tables[0].Rows[0]["ShoppingBag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["ShoppingBag"]);
                        moduleConfiguration.Payment = ds.Tables[0].Rows[0]["Payment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Payment"]);
                        moduleConfiguration.Shipment = ds.Tables[0].Rows[0]["Shipment"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["Shipment"]);
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

        public int UpdateModuleConfiguration(ModuleConfiguration moduleConfiguration, int modifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateModuleConfiguration", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ID", moduleConfiguration.ID);
                cmd.Parameters.AddWithValue("@_ShoppingBag", Convert.ToInt16(moduleConfiguration.ShoppingBag));
                cmd.Parameters.AddWithValue("@_Payment", Convert.ToInt16(moduleConfiguration.Payment));
                cmd.Parameters.AddWithValue("@_Shipment", Convert.ToInt16(moduleConfiguration.Shipment));
                cmd.Parameters.AddWithValue("@_ModifiedBy", modifiedBy);

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

        public OrderConfiguration GetOrderConfiguration(int tenantId, int userId, string programCode)
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
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@_prgramCode", programCode);

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

        public int UpdateOrderConfiguration(OrderConfiguration orderConfiguration, int modifiedBy)
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
                cmd.Parameters.AddWithValue("@_EnableClickAfterValue", Convert.ToInt16(orderConfiguration.EnableClickAfterValue));
                cmd.Parameters.AddWithValue("@_EnableClickAfterDuration", orderConfiguration.EnableClickAfterDuration);
                cmd.Parameters.AddWithValue("@_ModifiedBy", modifiedBy);

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

        public OrderDeliveredDetails GetOrderDeliveredDetails(int tenantId, int userId, OrderDeliveredFilterRequest orderDeliveredFilter)
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
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
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
                            ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            InvoiceNo = Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            CustomerName = Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]),
                            MobileNumber = Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]),
                            Date = Convert.ToString(ds.Tables[0].Rows[i]["Date"]),
                            Time = Convert.ToString(ds.Tables[0].Rows[i]["Time"]),
                            StatusName = Convert.ToString(ds.Tables[0].Rows[i]["StatusName"]),
                            ActionTypeName = Convert.ToString(ds.Tables[0].Rows[i]["ActionTypeName"]),
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

        public List<OrderStatusFilter> GetOrderStatusFilter(int tenantId, int userId, int pageID)
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

        public ShipmentAssignedDetails GetShipmentAssignedDetails(int tenantId, int userId, ShipmentAssignedFilterRequest shipmentAssignedFilter)
        {
            DataSet ds = new DataSet();
            ShipmentAssignedDetails objdetails = new ShipmentAssignedDetails();

            List<ShipmentAssigned> shipmentAssigned = new List<ShipmentAssigned>();
            int TotalCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderShipmentAssigned", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
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
                            AWBNo = Convert.ToString(ds.Tables[0].Rows[i]["AWBNo"]),
                            InvoiceNo = Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            CourierPartner = Convert.ToString(ds.Tables[0].Rows[i]["CourierPartner"]),
                            ReferenceNo = Convert.ToString(ds.Tables[0].Rows[i]["ReferenceNo"]),
                            StoreName = Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]),
                            StaffName = Convert.ToString(ds.Tables[0].Rows[i]["StaffName"]),
                            MobileNumber = Convert.ToString(ds.Tables[0].Rows[i]["MobileNumber"]),
                            IsProceed = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsProceed"]),
                            ShipmentAWBID = Convert.ToString(ds.Tables[0].Rows[i]["ShipmentAWBID"])
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

        public int UpdateMarkAsDelivered(int tenantId, int userId, int orderID)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateMarkAsDelivered", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
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

        public int UpdateShipmentAssignedData(ShipmentAssignedRequest shipmentAssignedRequest)
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

        public int UpdateShipmentBagCancelData(int shoppingID, string cancelComment, int userId)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateShipmentBagCancel", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ShoppingID", shoppingID);
                cmd.Parameters.AddWithValue("@_CancelComment", cancelComment);
                cmd.Parameters.AddWithValue("@_UserID", userId);

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

        public int InsertOrderDetails(ConvertToOrder convertToOrder)
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

                cmd.CommandType = CommandType.StoredProcedure;
                InsertCount = Convert.ToInt32(cmd.ExecuteNonQuery());

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


        public int UpdateAddressPending(AddressPendingRequest addressPendingRequest)
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
    }
}
