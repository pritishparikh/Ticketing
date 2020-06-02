using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
                MySqlCommand cmd = new MySqlCommand("SP_HSGetModuleConfiguration", conn)
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
                MySqlCommand cmd = new MySqlCommand("SP_HSUpdateModuleConfiguration", conn)
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
                MySqlCommand cmd = new MySqlCommand("SP_HSGetOrderConfiguration", conn)
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
                MySqlCommand cmd = new MySqlCommand("SP_HSUpdateOrderConfiguration", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ID", orderConfiguration.ID);
                cmd.Parameters.AddWithValue("@_IntegratedSystem", Convert.ToInt16(orderConfiguration.IntegratedSystem));
                cmd.Parameters.AddWithValue("@_Payment", Convert.ToInt16(orderConfiguration.Payment));
                cmd.Parameters.AddWithValue("@_Shipment", Convert.ToInt16(orderConfiguration.Shipment));
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
    }
}
