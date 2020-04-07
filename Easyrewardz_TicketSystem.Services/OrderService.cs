using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class OrderService : IOrder
    {
        CultureInfo culture = CultureInfo.InvariantCulture;
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        CustomResponse ApiResponse = null;
        string apiResponse = string.Empty;
        string apisecurityToken = string.Empty;
        string apiURL = string.Empty;

        public OrderService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
            apisecurityToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJQcm9ncmFtQ29kZSI6IkJhdGEiLCJVc2VySUQiOiIzIiwiQXBwSUQiOiI3IiwiRGF5IjoiMjgiLCJNb250aCI6IjMiLCJZZWFyIjoiMjAyMSIsIlJvbGUiOiJBZG1pbiIsImlzcyI6IkF1dGhTZWN1cml0eUlzc3VlciIsImF1ZCI6IkF1dGhTZWN1cml0eUF1ZGllbmNlIn0.0XeF7V5LWfQn0NlSlG7Rb-Qq1hUCtUYRDg6dMGIMvg0";
            apiURL = "http://searchapi.ercx.co/api/Search/";
         

        }
        #endregion
        /// <summary>
        /// Get OrderBy Number
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        public OrderMaster getOrderbyNumber(string orderNumber, int tenantID)
        {
            OrderMaster orderMasters = new OrderMaster();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getOrderByNumber", conn);
                cmd1.Parameters.AddWithValue("@objOrderNumber", orderNumber);
                cmd1.Parameters.AddWithValue("@Tenant_Id", tenantID);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                conn.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    orderMasters.OrderMasterID = Convert.ToInt32(dt.Rows[0]["OrderMasterID"]);
                    orderMasters.OrderNumber = Convert.ToString(dt.Rows[0]["OrderNumber"]);
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
            return orderMasters;
        }
        /// <summary>
        /// Add Order Detail
        /// </summary>
        /// <param name="orderMaster"></param>
        /// <returns></returns>
        public string addOrderDetails(OrderMaster orderMaster, int tenantID)
        {
            MySqlCommand cmd = new MySqlCommand();
            string OrderNumber="";
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createOrder", conn);
                cmd1.Parameters.AddWithValue("@TenantID", tenantID);
                cmd1.Parameters.AddWithValue("@ProductBarCode", orderMaster.ProductBarCode);
                cmd1.Parameters.AddWithValue("@OrderNumber", orderMaster.OrderNumber);
                cmd1.Parameters.AddWithValue("@BillID", orderMaster.BillID);
                cmd1.Parameters.AddWithValue("@TicketSourceID", orderMaster.TicketSourceID);
                cmd1.Parameters.AddWithValue("@ModeOfPaymentID", orderMaster.ModeOfPaymentID);
                cmd1.Parameters.AddWithValue("@TransactionDate", orderMaster.TransactionDate);
               // cmd1.Parameters.AddWithValue("@InvoiceNumber", orderMaster.InvoiceNumber);
                cmd1.Parameters.AddWithValue("@InvoiceDate", orderMaster.InvoiceDate);
                cmd1.Parameters.AddWithValue("@OrderPrice", orderMaster.OrderPrice);
                cmd1.Parameters.AddWithValue("@PricePaid", orderMaster.PricePaid);
                cmd1.Parameters.AddWithValue("@CustomerID", orderMaster.CustomerID);
                cmd1.Parameters.AddWithValue("@PurchaseFromStoreId", orderMaster.PurchaseFromStoreId);
                cmd1.Parameters.AddWithValue("@Discount", orderMaster.Discount);
                cmd1.Parameters.AddWithValue("@Size", string.IsNullOrEmpty(orderMaster.Size) ? "" :orderMaster.Size);
                cmd1.Parameters.AddWithValue("@RequireSize", string.IsNullOrEmpty(orderMaster.RequireSize) ? "" :orderMaster.RequireSize);
                cmd1.Parameters.AddWithValue("@CreatedBy", orderMaster.CreatedBy);
                cmd1.CommandType = CommandType.StoredProcedure;
                OrderNumber = Convert.ToString(cmd1.ExecuteScalar());
                conn.Close();
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
            return OrderNumber;
        }
        /// <summary>
        /// Get Order Detail with Item List
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<CustomOrderMaster> getOrderListwithItemDetail(string orderNumber, int customerID, int tenantID,int CreatedBy)
        {

            DataSet ds = new DataSet();
            List<CustomOrderMaster> objorderMaster = new List<CustomOrderMaster>();
            CustomerService CustService = new CustomerService(conn.ConnectionString);
            CustomerMaster customerMaster = new CustomerMaster();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SP_OrderDetails", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Order_ID", orderNumber);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@Customer_ID", customerID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        
                           CustomOrderMaster customOrderMaster = new CustomOrderMaster();
                        customOrderMaster.OrderMasterID = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        //customOrderMaster.InvoiceNumber = Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNumber"]);
                        // customOrderMaster.InvoiceNumber = ds.Tables[0].Rows[i]["BillID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BillID"]);
                        customOrderMaster.InvoiceNumber = ds.Tables[0].Rows[i]["OrderNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrderNumber"]);
                        customOrderMaster.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]);
                        customOrderMaster.OrdeItemPrice = ds.Tables[0].Rows[i]["OrderPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderPrice"]);
                        customOrderMaster.OrderPricePaid = ds.Tables[0].Rows[i]["PricePaid"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["PricePaid"]);
                        customOrderMaster.DateFormat = customOrderMaster.InvoiceDate.ToString("dd/MMM/yyyy");
                        customOrderMaster.StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        customOrderMaster.StoreAddress = ds.Tables[0].Rows[i]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                        customOrderMaster.Discount = ds.Tables[0].Rows[i]["Discount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Discount"]);
                        int orderMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customOrderMaster.OrderItems = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("OrderMasterID")).
                        Equals(orderMasterId)).Select(x => new OrderItem()
                        {
                            OrderItemID = Convert.ToInt32(x.Field<int>("OrderItemID")),
                            OrderMasterID = Convert.ToInt32(x.Field<int>("OrderMasterID")),
                            ArticleNumber = x.Field<object>("SKUNumber") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("SKUNumber")),
                            ArticleName = x.Field<object>("SKUName") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("SKUName")),
                            ItemPrice = x.Field<object>("ItemPrice") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("ItemPrice")),
                            PricePaid = x.Field<object>("PricePaid") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("PricePaid")),
                            Discount = x.Field<object>("Discount") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("Discount")),
                            RequireSize = x.Field<object>("RequireSize") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("RequireSize"))
                        }).ToList();
                        customOrderMaster.ItemCount = customOrderMaster.OrderItems.Count();
                        customOrderMaster.ItemPrice = customOrderMaster.OrderItems.Sum(item => item.ItemPrice);
                        customOrderMaster.PricePaid = customOrderMaster.OrderItems.Sum(item => item.PricePaid);
                        objorderMaster.Add(customOrderMaster);
                    }

                    }
                    else
                    {
                        // get customer details
                        customerMaster=CustService.getCustomerbyId(customerID, tenantID);
                       
                        objorderMaster = getOrderDetailsfromAPI(orderNumber, customerID, customerMaster.CustomerPhoneNumber, tenantID, CreatedBy);
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
            return objorderMaster;
        }
        /// <summary>
        /// Get OrderList By CustomerID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<CustomOrderDetailsByCustomer> getOrderListByCustomerID(int customerID, int tenantID,int CreatedBy)
        {
            DataSet ds = new DataSet();
            List<CustomOrderDetailsByCustomer> objorderMaster = new List<CustomOrderDetailsByCustomer>();
            List<CustomOrderMaster> customOrderdetail = new List<CustomOrderMaster>();
            CustomerService CustService = new CustomerService(conn.ConnectionString);
            CustomerMaster customerMaster = new CustomerMaster();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_OrderDetailsByCustomerID", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Customer_ID", customerID);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CustomOrderDetailsByCustomer customOrderMaster = new CustomOrderDetailsByCustomer();
                            customOrderMaster.OrderMasterID = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                            customOrderMaster.CusotmerID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"]);
                            customOrderMaster.CusotmerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                            customOrderMaster.MobileNumber = ds.Tables[0].Rows[i]["CustomerPhoneNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerPhoneNumber"]);
                            customOrderMaster.EmailID = ds.Tables[0].Rows[i]["CustomerEmailId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerEmailId"]);
                            customOrderMaster.OrderNumber = ds.Tables[0].Rows[i]["OrderNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrderNumber"]);
                            customOrderMaster.InvoiceNumber = ds.Tables[0].Rows[i]["InvoiceNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNumber"]);
                            customOrderMaster.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]);
                            customOrderMaster.DateFormat = customOrderMaster.InvoiceDate.ToString("dd/MMM/yyyy");
                            customOrderMaster.StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                            customOrderMaster.StoreAddress = ds.Tables[0].Rows[i]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                            customOrderMaster.PaymentModename = ds.Tables[0].Rows[i]["PaymentModename"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PaymentModename"]);
                            int orderMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                            customOrderMaster.OrderItems = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("OrderMasterID")).
                            Equals(orderMasterId)).Select(x => new OrderItem()
                            {
                                OrderItemID = Convert.ToInt32(x.Field<int>("OrderItemID")),
                                OrderMasterID = Convert.ToInt32(x.Field<int>("OrderMasterID")),
                                ItemName = x.Field<object>("ItemName") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("ItemName")),
                                InvoiceNo = x.Field<object>("InvoiceNo") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("InvoiceNo")),
                                ItemPrice = x.Field<object>("ItemPrice") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("ItemPrice")),
                                PricePaid = x.Field<object>("PricePaid") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("PricePaid")),
                            }).ToList();
                            customOrderMaster.ItemCount = customOrderMaster.OrderItems.Count();
                            customOrderMaster.ItemPrice = customOrderMaster.OrderItems.Sum(item => item.ItemPrice);
                            customOrderMaster.PricePaid = customOrderMaster.OrderItems.Sum(item => item.PricePaid);
                            objorderMaster.Add(customOrderMaster);
                        }
                    }
                    else
                    {
                        // get customer details
                        customerMaster = CustService.getCustomerbyId(customerID, tenantID);

                        customOrderdetail = getOrderDetailsfromAPI("", customerID, customerMaster.CustomerPhoneNumber, tenantID, CreatedBy);

                        if(customOrderdetail.Count > 0)
                        {
                            for(int k=0; k < customOrderdetail.Count; k++)
                            {
                                CustomOrderDetailsByCustomer customOrderMaster = new CustomOrderDetailsByCustomer();
                                customOrderMaster.OrderMasterID = customOrderdetail[k].OrderMasterID;
                                customOrderMaster.CusotmerID = customerMaster.CustomerID;
                                customOrderMaster.CusotmerName = customerMaster.CustomerName;
                                customOrderMaster.MobileNumber = customerMaster.CustomerPhoneNumber;
                                customOrderMaster.EmailID = customerMaster.CustomerEmailId;
                                customOrderMaster.OrderNumber = customOrderdetail[k].InvoiceNumber;
                                customOrderMaster.InvoiceNumber = customOrderdetail[k].InvoiceNumber;
                                customOrderMaster.InvoiceDate = customOrderdetail[k].InvoiceDate;
                                customOrderMaster.DateFormat = customOrderdetail[k].InvoiceDate.ToString("dd/MMM/yyyy");
                                customOrderMaster.StoreCode = customOrderdetail[k].StoreCode;
                                customOrderMaster.StoreAddress = customOrderdetail[k].StoreAddress;
                                customOrderMaster.PaymentModename = "";
                                // int orderMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                                customOrderMaster.OrderItems = customOrderdetail[k].OrderItems;
                                customOrderMaster.ItemCount = customOrderMaster.OrderItems.Count();
                                customOrderMaster.ItemPrice = customOrderMaster.OrderItems.Sum(item => item.ItemPrice);
                                customOrderMaster.PricePaid = customOrderMaster.OrderItems.Sum(item => item.PricePaid);
                                objorderMaster.Add(customOrderMaster);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return objorderMaster;
        }
        /// <summary>
        /// Get Order List By ClaimID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="ClaimID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public CustomOrderDetailsByClaim getOrderListByClaimID(int customerID, int claimID, int tenantID)
        {
            DataSet ds = new DataSet();
            CustomOrderDetailsByClaim customClaimMaster = new CustomOrderDetailsByClaim();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetOrderDetailByClaimID", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Claim_ID", claimID);
                cmd.Parameters.AddWithValue("@Customer_ID", customerID);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //CustomOrderDetailsByClaim customClaimMaster = new CustomOrderDetailsByClaim();
                        customClaimMaster.TicketClaimID = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketClaimID"]);
                        customClaimMaster.ClaimCategory = Convert.ToString(ds.Tables[0].Rows[i]["ClaimCategory"]);
                        customClaimMaster.ClaimSubCategory = Convert.ToString(ds.Tables[0].Rows[i]["ClaimSubCategory"]);
                        customClaimMaster.ClaimType = Convert.ToString(ds.Tables[0].Rows[i]["ClaimType"]);
                        customClaimMaster.ClaimAskedFor = Convert.ToString(ds.Tables[0].Rows[i]["ClaimAskedFor"]);
                        customClaimMaster.StatusID = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]);

                        customClaimMaster.CusotmerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"]);
                        customClaimMaster.CusotmerName = Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        customClaimMaster.MobileNumber = Convert.ToString(ds.Tables[0].Rows[i]["CustomerPhoneNumber"]);
                        customClaimMaster.EmailID = Convert.ToString(ds.Tables[0].Rows[i]["CustomerEmailId"]);
                        customClaimMaster.Gender = Convert.ToInt32(ds.Tables[0].Rows[i]["GenderID"]);
                        customClaimMaster.OrderMasterID = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customClaimMaster.OrderNumber = Convert.ToString(ds.Tables[0].Rows[i]["OrderNumber"]);
                        customClaimMaster.InvoiceNumber = Convert.ToString(ds.Tables[0].Rows[i]["BillID"]);
                        customClaimMaster.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]);
                        customClaimMaster.DateFormat = customClaimMaster.InvoiceDate.ToString("dd/MMM/yyyy");
                        customClaimMaster.StoreCode = Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        customClaimMaster.StoreAddress = Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                        customClaimMaster.PaymentModename = Convert.ToString(ds.Tables[0].Rows[i]["PaymentModename"]);
                        int orderMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customClaimMaster.OrderItems = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("OrderMasterID")).
                        Equals(orderMasterId)).Select(x => new OrderItem()
                        {
                            OrderItemID = Convert.ToInt32(x.Field<int>("OrderItemID")),
                            OrderMasterID = Convert.ToInt32(x.Field<int>("OrderMasterID")),
                            ItemName = Convert.ToString(x.Field<string>("ItemName")),
                            InvoiceNo = Convert.ToString(x.Field<string>("InvoiceNo")),
                            ItemPrice = Convert.ToInt32(x.Field<decimal>("ItemPrice")),
                            PricePaid = Convert.ToInt32(x.Field<decimal>("PricePaid")),
                            Discount = Convert.ToInt32(x.Field<decimal>("Discount")),
                        }).ToList();
                        customClaimMaster.ItemCount = customClaimMaster.OrderItems.Count();
                        customClaimMaster.ItemPrice = customClaimMaster.OrderItems.Sum(item => item.ItemPrice);
                        customClaimMaster.PricePaid = customClaimMaster.OrderItems.Sum(item => item.PricePaid);

                        int MasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketClaimID"]);
                        customClaimMaster.Comments = ds.Tables[2].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("TicketClaimID")).
                        Equals(MasterId)).Select(x => new UserComment()
                        {
                            Name = Convert.ToString(x.Field<string>("Name")),
                            Comment = Convert.ToString(x.Field<string>("ClaimComment")),
                            datetime = Convert.ToString(x.Field<string>("CommentAt"))
                        }).ToList();
                        //objorderMaster.Add(customClaimMaster);
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
            return customClaimMaster;
        }
        /// <summary>
        /// Search Product
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public List<CustomSearchProduct> SearchProduct(int customerID, string productName)
        {
            List<CustomSearchProduct> productlist = new List<CustomSearchProduct>();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_SearchProduct", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Customer_ID", customerID);
                cmd.Parameters.AddWithValue("@product_Name", productName);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomSearchProduct product = new CustomSearchProduct();
                        product.ItemID = Convert.ToString(ds.Tables[0].Rows[i]["OrderItemID"]);
                        product.ItemName = Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]);
                        product.Department = Convert.ToString(ds.Tables[0].Rows[i]["Department"]);
                        product.Description = Convert.ToString(ds.Tables[0].Rows[i]["Description"]);
                        productlist.Add(product);
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
            return productlist;
        }
        /// <summary>
        /// Attach Order of ticket
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="TicketId"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int AttachOrder(string orderID, int ticketID, int createdBy)
        {
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_BulkTicketOrderAttachMapping", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Ticket_Id", ticketID);
                cmd.Parameters.AddWithValue("@OrderIDs", orderID);
                cmd.Parameters.AddWithValue("@Created_By", createdBy);
                cmd.CommandType = CommandType.StoredProcedure;
                Success = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
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
            return Success;
        }

        public List<CustomOrderMaster> getOrderDetailByTicketID(int ticketID, int tenantID)
        {
            DataSet ds = new DataSet();
            List<CustomOrderMaster> objorderMaster = new List<CustomOrderMaster>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetOrderDetailByTicketID", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ticket_ID", ticketID);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomOrderMaster customOrderMaster = new CustomOrderMaster();
                        customOrderMaster.OrderMasterID = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customOrderMaster.InvoiceNumber = ds.Tables[0].Rows[i]["InvoiceNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNumber"]);
                        customOrderMaster.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]);
                        customOrderMaster.OrdeItemPrice = ds.Tables[0].Rows[i]["OrderPrice"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderPrice"]);
                        customOrderMaster.OrderPricePaid = ds.Tables[0].Rows[i]["PricePaid"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PricePaid"]);
                        customOrderMaster.DateFormat = customOrderMaster.InvoiceDate.ToString("dd/MMM/yyyy");
                        customOrderMaster.StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        customOrderMaster.StoreAddress = ds.Tables[0].Rows[i]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                        customOrderMaster.Discount = ds.Tables[0].Rows[i]["Discount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Discount"]);
                        int orderMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customOrderMaster.OrderItems = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("OrderMasterID")).
                        Equals(orderMasterId)).Select(x => new OrderItem()
                        {
                            OrderItemID = Convert.ToInt32(x.Field<int>("OrderItemID")),
                            OrderMasterID = Convert.ToInt32(x.Field<int>("OrderMasterID")),
                            ArticleNumber = x.Field<object>("SKUNumber") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("SKUNumber")),
                            ArticleName = x.Field<object>("SKUName") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("SKUName")),
                            ItemPrice = x.Field<object>("ItemPrice") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("ItemPrice")),
                            PricePaid = x.Field<object>("PricePaid") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("PricePaid")),
                            Discount = x.Field<object>("Discount") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("Discount")),
                            RequireSize = x.Field<object>("RequireSize") == DBNull.Value ? string.Empty: Convert.ToString(x.Field<object>("RequireSize"))
                        }).ToList();
                        customOrderMaster.ItemCount = customOrderMaster.OrderItems.Count();
                        customOrderMaster.ItemPrice = customOrderMaster.OrderItems.Sum(item => item.ItemPrice);
                        customOrderMaster.PricePaid = customOrderMaster.OrderItems.Sum(item => item.PricePaid);
                        objorderMaster.Add(customOrderMaster);
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
            return objorderMaster;
        }

        public List<CustomOrderMaster> getOrderDetailsfromAPI(string orderNumber, int customerID,string MobNo, int tenantID, int CreatedBy)
        {
            List<CustomOrderMaster> objorderMaster = new List<CustomOrderMaster>();
            CustomSearchOrder objOrderSearch = new CustomSearchOrder();
            List<CustomOrderDetails> objOrderDetails = new List<CustomOrderDetails>();
            
            string InsertOrderNo = string.Empty;
            int InsertOrderID = 0;

            try
            {
                objOrderSearch.programCode = "bata";
                objOrderSearch.mobileNumber = MobNo;
                objOrderSearch.invoiceNumber = orderNumber;
                objOrderSearch.securityToken = apisecurityToken;
                objOrderSearch.userID = customerID;// 3;
                objOrderSearch.appID = 7;

                string apiReq = JsonConvert.SerializeObject(objOrderSearch);
                apiResponse = CommonService.SendApiRequest(apiURL+ "CustomerOrderDetails", apiReq);
                if (!string.IsNullOrEmpty(apiResponse))
                {
                    ApiResponse = JsonConvert.DeserializeObject<CustomResponse>(apiResponse);
                    if (ApiResponse != null)
                    {
                        objOrderDetails= JsonConvert.DeserializeObject<List<CustomOrderDetails>>(Convert.ToString((ApiResponse.Responce)));
                        if (objOrderDetails != null)
                        {
                            if(objOrderDetails.Count > 0)
                            {
                                for (int k = 0; k < objOrderDetails.Count; k++)
                                {
                                    OrderMaster OrderReq = new OrderMaster();
                                    OrderReq.ProductBarCode ="";
                                    OrderReq.OrderNumber = objOrderDetails[k].InvoiceNumber;
                                    OrderReq.BillID ="";
                                    OrderReq.TicketSourceID = 30; // 29-offline; 30- web ; 31- mobile channel of purchase
                                    OrderReq.ModeOfPaymentID =1;
                                    OrderReq.TransactionDate = DateTime.ParseExact(objOrderDetails[k].InvoiceDate, "M/d/yy h:mm:ss tt", culture);
                                    OrderReq.InvoiceNumber =objOrderDetails[k].InvoiceNumber;
                                    OrderReq.InvoiceDate = DateTime.ParseExact(objOrderDetails[k].InvoiceDate, "M/d/yy h:mm:ss tt", culture);
                                    OrderReq.StoreCode = 0;// objOrderDetails[k].StoreCode
                                    OrderReq.OrderPrice =0;
                                    OrderReq.PricePaid = objOrderDetails[k].PricePaid;
                                    OrderReq.CustomerID = customerID;
                                    OrderReq.PurchaseFromStoreId =Convert.ToInt16(!string.IsNullOrEmpty(objOrderDetails[k].StoreCode));
                                    OrderReq.Discount = string.IsNullOrEmpty(objOrderDetails[k].Discount) ? 0: Convert.ToDecimal(objOrderDetails[k].Discount);
                                    OrderReq.Size ="";
                                    OrderReq.RequireSize ="";
                                    OrderReq.ModeOfPaymentID = 1;
                                    OrderReq.CreatedBy = CreatedBy;
                                    OrderReq.TenantID = tenantID;

                                    //insert order into table
                                    InsertOrderNo = addOrderDetails(OrderReq, tenantID);

                                    if(!string.IsNullOrEmpty(InsertOrderNo))
                                    {
                                        InsertOrderID = getOrderbyNumber(InsertOrderNo, tenantID).OrderMasterID;
                                        if(InsertOrderID > 0)
                                        {

                                            CustomOrderMaster orderDetails = new CustomOrderMaster();
                                            orderDetails.OrderMasterID = InsertOrderID;
                                            orderDetails.InvoiceNumber = InsertOrderNo;
                                            orderDetails.InvoiceDate = OrderReq.InvoiceDate;
                                            orderDetails.OrdeItemPrice = OrderReq.OrderPrice;
                                            orderDetails.OrderPricePaid = OrderReq.PricePaid;
                                            orderDetails.DateFormat = OrderReq.InvoiceDate.ToString("dd/MMM/yyyy");
                                            orderDetails.StoreCode = objOrderDetails[k].StoreCode;
                                            orderDetails.StoreAddress = objOrderDetails[k].StoreAddress;
                                            orderDetails.Discount = OrderReq.Discount;
                                            orderDetails.OrderItems = GetItemdetailsfromAPI(InsertOrderNo, InsertOrderID, objOrderDetails[k], CreatedBy);
                                            orderDetails.ItemCount = orderDetails.OrderItems.Count();
                                            orderDetails.ItemPrice = orderDetails.OrderItems.Sum(item => item.ItemPrice);
                                            orderDetails.PricePaid = orderDetails.OrderItems.Sum(item => item.PricePaid);


                                            objorderMaster.Add(orderDetails);
                                        }
                                        
                                    }

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

            return objorderMaster;
        }

        public List<OrderItem> GetItemdetailsfromAPI(string orderno,int OrderMasterId, CustomOrderDetails Orders, int CreatedBy)
        {
            CustomItemSearch objItemSearch = new CustomItemSearch();
            List<CustomItemDetails> objItemDetails = new List<CustomItemDetails>();
            List<OrderItem> objOrderItems = new List<OrderItem>();
            int InsertedItemID = 0;
            try
            {

                objItemSearch.programCode = "bata";
                objItemSearch.invoiceNumber = orderno;
                objItemSearch.storeCode ="";// Orders.StoreCode;
                objItemSearch.invoiceDate = "";// Orders.InvoiceDate;
                objItemSearch.securityToken = apisecurityToken;
                objItemSearch.userID = 3;
                objItemSearch.appID = 7;

                string apiReq = JsonConvert.SerializeObject(objItemSearch);
                apiResponse = CommonService.SendApiRequest(apiURL + "ItemDetails", apiReq);

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    ApiResponse = JsonConvert.DeserializeObject<CustomResponse>(apiResponse);
                    if (ApiResponse != null)
                    {
                        objItemDetails = JsonConvert.DeserializeObject<List<CustomItemDetails>>(Convert.ToString((ApiResponse.Responce)));

                        if (objItemDetails != null && objItemDetails.Count > 0)
                        {
                            for (int k = 0; k < objItemDetails.Count; k++)
                            {
                                if (conn != null && conn.State == ConnectionState.Closed)
                                {
                                    conn.Open();
                                }

                                MySqlCommand cmd = new MySqlCommand("SP_InsertOrderItem", conn);
                                cmd.Connection = conn;
                                cmd.Parameters.AddWithValue("@_OrderMasterID", OrderMasterId);
                                cmd.Parameters.AddWithValue("@_InvoiceNo", Orders.InvoiceNumber);
                                cmd.Parameters.AddWithValue("@_InvoiceDate", DateTime.ParseExact(Orders.InvoiceDate, "M/d/yy h:mm:ss tt", culture));
                                cmd.Parameters.AddWithValue("@_ItemCount", Orders.ItemCount);
                                cmd.Parameters.AddWithValue("@_PricePaid", string.IsNullOrEmpty(objItemDetails[k].PricePaid) ? 0: Convert.ToDecimal(objItemDetails[k].PricePaid));
                                cmd.Parameters.AddWithValue("@_SKUNumber", objItemDetails[k].ArticleNumber);
                                cmd.Parameters.AddWithValue("@_SKUName", objItemDetails[k].ArticleSize);
                                cmd.Parameters.AddWithValue("@_ItemPrice", string.IsNullOrEmpty(objItemDetails[k].ArticleMrp) ? 0 : Convert.ToDecimal(objItemDetails[k].ArticleMrp));
                                cmd.Parameters.AddWithValue("@_Discount", string.IsNullOrEmpty(objItemDetails[k].Discount) ? 0 : Convert.ToDecimal(objItemDetails[k].Discount));
                                cmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);

                                cmd.CommandType = CommandType.StoredProcedure;
                                InsertedItemID = Convert.ToInt32(cmd.ExecuteScalar());

                                objOrderItems.Add(new OrderItem
                                {
                                    OrderItemID = InsertedItemID,
                                    OrderMasterID = OrderMasterId,
                                    ArticleNumber = objItemDetails[k].ArticleNumber,
                                    ArticleName = objItemDetails[k].ArticleSize, // we are getting skuname in ArticleSize (API issue)
                                    ItemPrice = string.IsNullOrEmpty(objItemDetails[k].ArticleMrp) ? 0 : Convert.ToDecimal(objItemDetails[k].ArticleMrp),
                                  
                                    PricePaid = string.IsNullOrEmpty(objItemDetails[k].PricePaid) ? 0 : Convert.ToDecimal(objItemDetails[k].PricePaid),
                                   
                                    Discount = string.IsNullOrEmpty(objItemDetails[k].Discount) ? 0 : Convert.ToDecimal(objItemDetails[k].Discount), 
                                    RequireSize = ""
                                });


                            }

                        }
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return objOrderItems;
        }
    }
}
