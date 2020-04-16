using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class OrderController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string connectioSting;
        private readonly string radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public OrderController(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get OrderBy Number
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOrderByNumber")]
        public ResponseModel getOrderByNumber(string OrderNumber)
        {

            OrderMaster objorderMaster = null;
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objorderMaster = ordercaller.getOrderDetailsByNumber(new OrderService(connectioSting), OrderNumber, authenticate.TenantId);
                StatusCode =
                   objorderMaster == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
        /// <summary>
        /// Add Order Details
        /// </summary>
        /// <param name="orderMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createOrder")]
        public ResponseModel createOrder([FromBody]OrderMaster orderMaster)
        {
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            string OrderNo = string.Empty;
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                orderMaster.CreatedBy = authenticate.UserMasterID;
                OrderNo = ordercaller.addOrder(new OrderService(connectioSting), orderMaster, authenticate.TenantId);
              
                StatusCode =
               string.IsNullOrEmpty(OrderNo) ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = OrderNo;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Add Order Details
        /// </summary>
        /// <param name="OrderItem"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertOrderItems")]
        public ResponseModel InsertOrderItems([FromBody]List<OrderItem> itemMaster)
        {
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
               
                string itemOrderNos = ordercaller.AddOrderItem(new OrderService(connectioSting), itemMaster, authenticate.TenantId,authenticate.UserMasterID);

                StatusCode =
               string.IsNullOrEmpty(itemOrderNos) ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = itemOrderNos;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Orderdetail list 
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOrderListWithItemDetails")]
        public ResponseModel getOrderListWithItemDetails(string OrderNumber, int CustomerID)
        {
            List<CustomOrderMaster> objorderMaster = new List<CustomOrderMaster>();
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objorderMaster = ordercaller.GetOrderItemList(new OrderService(connectioSting), OrderNumber, CustomerID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                   objorderMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Order item list
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="OrderMasterID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="StoreCode"></param>
        /// <param name="InvoiceDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOrderItemDetailsList")]
        public ResponseModel getOrderItemDetailsList([FromBody]OrderMaster orders) //InvoiceDatedate format : yyyy-MM-dd
        {
            List<OrderItem> objitemMaster = new List<OrderItem>();
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objitemMaster = ordercaller.GetOrderItemDetailsList(new OrderService(connectioSting), authenticate.TenantId, orders);
                StatusCode =
                   objitemMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objitemMaster;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Orderdetail list with item
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getorderdetailsbycustomerid")]
        public ResponseModel getOrderDetailsByCustomerid(int CustomerID)
        {
            List<CustomOrderDetailsByCustomer> objorderMaster = new List<CustomOrderDetailsByCustomer>();
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objorderMaster = ordercaller.GetOrderDetailsByCustomerID(new OrderService(connectioSting), CustomerID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                   objorderMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// get order details by claim id
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="ClaimID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getorderdetailsbyclaimid")]
        public ResponseModel GetOrderDetailsByClaimid(int CustomerID, int ClaimID)
        {
            CustomOrderDetailsByClaim objorderMaster = new CustomOrderDetailsByClaim();
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objorderMaster = ordercaller.GetOrderListByClaimID(new OrderService(connectioSting), CustomerID, ClaimID, authenticate.TenantId);
                StatusCode =
                   objorderMaster == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Search Product
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="Searchtext"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SearchProduct")]
        public ResponseModel SearchProduct(int CustomerID, string Searchtext)
        {
            List<CustomSearchProduct> objorderMaster = new List<CustomSearchProduct>();
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objorderMaster = ordercaller.SearchProduct(new OrderService(connectioSting), CustomerID, Searchtext);
                StatusCode =
                   objorderMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #region attach order OLD APPROACH

        /// <summary>
        /// attach order
        /// </summary>
        /// <param name="OrderitemID"></param>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("attachorder")]
        //public ResponseModel AttachOrder(string OrderID, int TicketId)
        //{
        //    OrderCaller ordercaller = new OrderCaller();
        //    ResponseModel objResponseModel = new ResponseModel();
        //    int StatusCode = 0;
        //    string statusMessage = "";
        //    try
        //    {
        //        string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
        //        Authenticate authenticate = new Authenticate();
        //        authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

        //        int result = ordercaller.AttachOrder(new OrderService(connectioSting), OrderID, TicketId, authenticate.UserMasterID);
        //        StatusCode =
        //        result == 0 ?
        //               (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
        //        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
        //        objResponseModel.Status = true;
        //        objResponseModel.StatusCode = StatusCode;
        //        objResponseModel.Message = statusMessage;
        //        objResponseModel.ResponseData = result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return objResponseModel;
        //}

        #endregion

        /// <summary>
        /// attach order
        /// </summary>
        /// <param name="OrderitemID"></param>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("attachorder")]
        public ResponseModel AttachOrder(IFormFile File)
        {
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            OrderMaster orderDetails = new OrderMaster();
            List<OrderItem> OrderItemDetails = new List<OrderItem>();
            int StatusCode = 0;
            string statusMessage = "";
            string OrderID = string.Empty;
            int TicketId = 0;
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                var Keys = Request.Form;

                // get order details from form
                orderDetails = JsonConvert.DeserializeObject<OrderMaster>(Keys["orderDetails"]);
                OrderItemDetails = JsonConvert.DeserializeObject<List<OrderItem>>(Keys["orderItemDetails"]);

                OrderID = Convert.ToString(Keys["OrderID"]);
                TicketId = Convert.ToInt32(Keys["TicketId"]);

                if (!string.IsNullOrEmpty(OrderID) && TicketId > 0)
                {

                    #region check orderdetails and item details 

                    if (orderDetails != null)
                    {
                        if (orderDetails.OrderMasterID.Equals(0))
                        {
                            string OrderNumber = string.Empty;
                            string OrderItemsIds = string.Empty;
                            OrderMaster objorderMaster = null;

                            //call insert order
                            orderDetails.CreatedBy = authenticate.UserMasterID;
                            OrderNumber = ordercaller.addOrder(new OrderService(connectioSting), orderDetails, authenticate.TenantId);
                            if (!string.IsNullOrEmpty(OrderNumber))
                            {
                                objorderMaster = ordercaller.getOrderDetailsByNumber(new OrderService(connectioSting), OrderNumber, authenticate.TenantId);

                                if (objorderMaster != null)
                                {
                                    if (OrderItemDetails != null)
                                    {
                                        foreach (var item in OrderItemDetails)
                                        {
                                            item.OrderMasterID = objorderMaster.OrderMasterID;
                                            item.InvoiceDate = orderDetails.InvoiceDate;
                                        }

                                        OrderItemsIds = ordercaller.AddOrderItem(new OrderService(connectioSting), OrderItemDetails, authenticate.TenantId, authenticate.UserMasterID);
                                    }
                                    else
                                    {
                                        OrderItemsIds = Convert.ToString(objorderMaster.OrderMasterID) + "|0|1";
                                    }

                                    OrderID = OrderItemsIds;
                                }
                            }


                        }
                    }

                    #endregion

                    result = ordercaller.AttachOrder(new OrderService(connectioSting), OrderID, TicketId, authenticate.UserMasterID);

                }
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.InternalServerError : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Order Detail By TicketID
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOrderDetailByTicketID")]
        public ResponseModel getOrderDetailByTicketID(int TicketID)
        {
            List<CustomOrderMaster> objorderMaster = new List<CustomOrderMaster>();
            OrderCaller ordercaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objorderMaster = ordercaller.GetOrderDetailByticketID(new OrderService(connectioSting), TicketID, authenticate.TenantId);
                StatusCode =
                   objorderMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion
    }
}