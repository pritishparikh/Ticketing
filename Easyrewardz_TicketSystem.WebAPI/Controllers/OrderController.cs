using System;
using System.Collections.Generic;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class OrderController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public OrderController(IConfiguration _iConfig,TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
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
            OrderCaller orderCaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objorderMaster = orderCaller.getOrderDetailsByNumber(new OrderService(Cache, Db), OrderNumber, authenticate.TenantId);
                statusCode =
                   objorderMaster == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
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
            OrderCaller orderCaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                orderMaster.CreatedBy = authenticate.UserMasterID;
                string OrderNumber = orderCaller.addOrder(new OrderService(Cache, Db), orderMaster, authenticate.TenantId);
                //OrderMaster order = orderCaller.getOrderDetailsByNumber(new OrderService(_connectioSting), orderMaster);
                statusCode =
               string.IsNullOrEmpty (OrderNumber) ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = OrderNumber;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Orderdetail list with item
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOrderListWithItemDetails")]
        public ResponseModel getOrderListWithItemDetails(string OrderNumber, int CustomerID)
        {
            List<CustomOrderMaster> objorderMaster = new List<CustomOrderMaster>();
            OrderCaller orderCaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objorderMaster = orderCaller.GetOrderItemList(new OrderService(Cache, Db), OrderNumber, CustomerID, authenticate.TenantId);
                statusCode =
                   objorderMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
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
        public ResponseModel getorderdetailsbycustomerid(int CustomerID)
        {
            List<CustomOrderDetailsByCustomer> objorderMaster = new List<CustomOrderDetailsByCustomer>();
            OrderCaller orderCaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objorderMaster = orderCaller.GetOrderDetailsByCustomerID(new OrderService(Cache, Db), CustomerID, authenticate.TenantId);
                statusCode =
                   objorderMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
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
        public ResponseModel getorderdetailsbyclaimid(int CustomerID, int ClaimID)
        {
            CustomOrderDetailsByClaim objorderMaster = new CustomOrderDetailsByClaim();
            OrderCaller orderCaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objorderMaster = orderCaller.GetOrderListByClaimID(new OrderService(Cache, Db), CustomerID, ClaimID, authenticate.TenantId);
                statusCode =
                   objorderMaster == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
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
            OrderCaller orderCaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objorderMaster = orderCaller.SearchProduct(new OrderService(Cache, Db), CustomerID, Searchtext);
                statusCode =
                   objorderMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// attach order
        /// </summary>
        /// <param name="OrderitemID"></param>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("attachorder")]
        public ResponseModel attachorder(string OrderID, int TicketId)
        {
            OrderCaller orderCaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = orderCaller.AttachOrder(new OrderService(Cache, Db), OrderID, TicketId, authenticate.UserMasterID);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
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
            OrderCaller orderCaller = new OrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objorderMaster = orderCaller.GetOrderDetailByticketID(new OrderService(Cache, Db), TicketID, authenticate.TenantId);
                statusCode =
                   objorderMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objorderMaster;

            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }
        #endregion
    }
}