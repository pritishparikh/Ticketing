using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    public partial class HSOrderController : ControllerBase
    {
        /// <summary>
        /// CreateShipmentAWB
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="itemIDs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateShipmentAWB")]
        public ResponseModel CreateShipmentAWB(int orderID, string itemIDs)
        {
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            ReturnShipmentDetails returnShipmentDetails = new ReturnShipmentDetails();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                returnShipmentDetails = hSOrderCaller.InsertShipmentAWB(new HSOrderService(_connectionString), orderID, itemIDs, authenticate.TenantId, authenticate.UserMasterID,_ClientAPIUrl,authenticate.ProgramCode);
                statusCode =
                  string.IsNullOrEmpty (returnShipmentDetails.ItemIDs) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = returnShipmentDetails;
            }
            catch (Exception ex)
            {
                objResponseModel.Status = false;
                objResponseModel.StatusCode = 503;
                objResponseModel.Message = "Server Unavailable";
                objResponseModel.ResponseData = "";
                CustomExceptionFilter customExceptionFilter = new CustomExceptionFilter(configuration);
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                ErrorLog errorLogs = new ErrorLog
                {
                    ActionName = ControllerContext.ActionDescriptor.ActionName,
                    ControllerName = ControllerContext.ActionDescriptor.ControllerName,
                    TenantID = authenticate.TenantId,
                    UserID = authenticate.UserMasterID,
                    Exceptions = ex.StackTrace,
                    MessageException = ex.Message,
                    IPAddress = ""
                };
                customExceptionFilter.OnExceptioninanyclass(errorLogs);
                //throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetItemDetailByOrderID
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetItemDetailByOrderID")]
        public ResponseModel GetItemDetailByOrderID(int orderID)
        {
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            OrdersItemDetails ordersItems = new OrdersItemDetails();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                ordersItems = hSOrderCaller.GetItemDetailByOrderID(new HSOrderService(_connectionString), orderID, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                ordersItems.OrdersItems.Count>0 ?
                           (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ordersItems;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetItemDetailByOrderID
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAWBInvoiceDetails")]
        public ResponseModel GetAWBInvoiceDetails(int orderID)
        {
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            List<ReturnShipmentDetails> lstreturnShipmentDetails = new List<ReturnShipmentDetails>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                lstreturnShipmentDetails = hSOrderCaller.GetAWBInvoicenoDetails(new HSOrderService(_connectionString), orderID, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                lstreturnShipmentDetails.Count > 0 ?
                           (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = lstreturnShipmentDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Generate Payment Link
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GeneratePaymentLink")]
        public ResponseModel GeneratePaymentLink([FromBody] SentPaymentLink objRequest)
        {
            int obj = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                HSRequestGenerateToken hSRequestGenerateToken = new HSRequestGenerateToken()
                {
                    Client_Id = _Client_Id,
                    Client_Secret = _Client_Secret,
                    Grant_Type = _Grant_Type,
                    Scope = _Scope,
                };
                
                obj = hSOrderCaller.GeneratePaymentLink(new HSOrderService(_connectionString), objRequest, _ClientAPIUrlForGenerateToken, _ClientAPIUrlForGeneratePaymentLink, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, _ClientAPIUrl, hSRequestGenerateToken);
                statusCode =
                   obj == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// CheckCourierAvailibilty
        /// </summary>
        /// <param name="HSChkCourierAvailibilty"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckCourierAvailibilty")]
        public ResponseModel CheckCourierAvailibilty([FromBody] HSChkCourierAvailibilty hSChkCourierAvailibilty)
        {
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            ResponseCourierAvailibilty responseCourierAvailibilty = new ResponseCourierAvailibilty();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                responseCourierAvailibilty = hSOrderCaller.CheckPinCodeForCourierAvailibilty(new HSOrderService(_connectionString), hSChkCourierAvailibilty, authenticate.TenantId, authenticate.UserMasterID, _ClientAPIUrl);
                statusCode =
                responseCourierAvailibilty.StatusCode !="" ? 
                           (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = responseCourierAvailibilty;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetStorePinCodeByUserID
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStorePinCodeByUserID")]
        public ResponseModel GetStorePinCodeByUserID()
        {
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                string pinCode = hSOrderCaller.GetStorePinCodeByUserID(new HSOrderService(_connectionString),authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                pinCode!=""?
                           (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = pinCode;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
    }
}