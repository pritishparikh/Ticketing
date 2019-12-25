using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class OrderController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        #endregion

        #region Cunstructor
        public OrderController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
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
        [AllowAnonymous]
        public ResponseModel getOrderByNumber(string OrderNumber)
        {

            OrderMaster _objorderMaster = null;
            OrderCaller _ordercaller = new OrderCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                _objorderMaster = _ordercaller.getOrderDetailsByNumber(new OrderService(_connectioSting), OrderNumber);
                StatusCode =
                   _objorderMaster == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objorderMaster;

            }
            catch (Exception _ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }
        /// <summary>
        /// Add Order Details
        /// </summary>
        /// <param name="orderMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createOrder")]
        [AllowAnonymous]
        public ResponseModel createOrder([FromBody]OrderMaster orderMaster)
        {
            OrderCaller _ordercaller = new OrderCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                int result = _ordercaller.addOrder(new OrderService(_connectioSting), orderMaster);
                //OrderMaster order = _ordercaller.getOrderDetailsByNumber(new OrderService(_connectioSting), orderMaster);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }

        /// <summary>
        /// Get Orderdetail list with item
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOrderListWithItemDetails")]
        [AllowAnonymous]
        public ResponseModel getOrderListWithItemDetails(string OrderNumber,int TenantID)
        {

            List <OrderMaster> _objorderMaster = new List<OrderMaster>();
            OrderCaller _ordercaller = new OrderCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                _objorderMaster = _ordercaller.GetOrderItemList(new OrderService(_connectioSting), OrderNumber, TenantID);
                    StatusCode =
                       _objorderMaster.Count == 0 ?
                               (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = StatusCode;
                    _objResponseModel.Message = statusMessage;
                    _objResponseModel.ResponseData = _objorderMaster;
                
            }
            catch (Exception _ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }

        #endregion
    }
}