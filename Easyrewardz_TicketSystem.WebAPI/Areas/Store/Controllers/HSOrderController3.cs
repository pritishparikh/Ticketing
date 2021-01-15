using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
        public async Task<ResponseModel> CreateShipmentAWB(int orderID, string itemIDs, int templateID)
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

                returnShipmentDetails = await hSOrderCaller.InsertShipmentAWB(new HSOrderService(_connectionString), orderID, itemIDs, authenticate.TenantId, authenticate.UserMasterID,_ClientAPIUrl,authenticate.ProgramCode, templateID, _PhygitalClientAPIURL);
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
                    MessageException = ex.Message + " : : InnerException = " + ex.Message.ToString(),
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
        public async Task<ResponseModel> GetItemDetailByOrderID(int orderID)
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

                ordersItems = await hSOrderCaller.GetItemDetailByOrderID(new HSOrderService(_connectionString), orderID, authenticate.TenantId, authenticate.UserMasterID);
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
        public async Task<ResponseModel> GetAWBInvoiceDetails(int orderID)
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

                lstreturnShipmentDetails =await hSOrderCaller.GetAWBInvoicenoDetails(new HSOrderService(_connectionString), orderID, authenticate.TenantId, authenticate.UserMasterID);
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
        public async Task<ResponseModel> GeneratePaymentLink([FromBody] SentPaymentLink objRequest)
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
                
                obj = await hSOrderCaller.GeneratePaymentLink(new HSOrderService(_connectionString), objRequest, _ClientAPIUrlForGenerateToken, _ClientAPIUrlForGeneratePaymentLink, authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, _ClientAPIUrl, hSRequestGenerateToken);
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
        public async Task<ResponseModel> CheckCourierAvailibilty([FromBody] HSChkCourierAvailibilty hSChkCourierAvailibilty)
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

                responseCourierAvailibilty = await hSOrderCaller.CheckPinCodeForCourierAvailibilty(new HSOrderService(_connectionString), hSChkCourierAvailibilty, authenticate.TenantId, authenticate.UserMasterID, _ClientAPIUrl, _PhygitalClientAPIURL);
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
        public async Task<ResponseModel> GetStorePinCodeByUserID()
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

                string pinCode =await hSOrderCaller.GetStorePinCodeByUserID(new HSOrderService(_connectionString),authenticate.TenantId, authenticate.UserMasterID);
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

        /// <summary>
        /// MakeShipmentAsStoreDelivery
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="itemIDs"></param>
        /// <param name="templateID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MakeShipmentAsStoreDelivery")]
        public async Task<ResponseModel> MakeShipmentAsStoreDelivery(int orderID, string itemIDs, int templateID)
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

                returnShipmentDetails =await hSOrderCaller.MakeShipmentAsStoreDelivery(new HSOrderService(_connectionString), orderID, itemIDs, authenticate.TenantId, authenticate.UserMasterID, templateID);
                statusCode =
                 string.IsNullOrEmpty(returnShipmentDetails.ItemIDs) ?
                          (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = returnShipmentDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// CancelOrder
        /// </summary>
        /// <param name="orderCancelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CancelOrder")]
        public async Task<ResponseModel> CancelOrder(OrderCancelRequest orderCancelRequest)
        {
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            OrderCancelResponse orderCancelResponse = new OrderCancelResponse();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderCancelRequest.tenantID = authenticate.TenantId;
                orderCancelRequest.userID = authenticate.UserMasterID;
                orderCancelRequest.phygitalClientAPIURL = _PhygitalClientAPIURL;
                orderCancelRequest.clientAPIUrl = _ClientAPIUrl;

                orderCancelResponse = await hSOrderCaller.CancelOrder(new HSOrderService(_connectionString), orderCancelRequest);
                statusCode =
                 orderCancelResponse.StatusCode != "200" ?
                          (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderCancelResponse;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetPODDetails
        /// </summary>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPODdetails")]
        public async Task<ResponseModel> GetPODdetails(OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails podResponseDetails = new OrderResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                podResponseDetails = await storecampaigncaller.PODDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, ordersDataRequest);
                statusCode =
                   podResponseDetails.TotalCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = podResponseDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Payment Comment
        /// </summary>
        /// <param name="PaymentCommentModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PaymentComment")]
        public async Task<ResponseModel> PaymentComment(PaymentCommentModel paymentCommentModel)
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

                paymentCommentModel.TenantID = authenticate.TenantId;
                paymentCommentModel.UserID = authenticate.UserMasterID;

                int result = await hSOrderCaller.PaymentComment(new HSOrderService(_connectionString), paymentCommentModel);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// DownLoadReport
        /// </summary>
        /// <param name="DownloadReportRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownLoadReport")]
        public async Task<ResponseModel> DownLoadReport(DownloadReportRequest downloadReportRequest)
        {
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            List<DownloadReportResponse> lstdownloadReportResponse = new List<DownloadReportResponse>();
            int statusCode = 0;
            string statusMessage = "";
            string CSVReport = string.Empty;
            string appRoot = string.Empty;
            string Folderpath = string.Empty;
            string URLPath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                downloadReportRequest.TenantID = authenticate.TenantId;
                downloadReportRequest.UserID = authenticate.UserMasterID;
                lstdownloadReportResponse = await storecampaigncaller.DownloadReport(new HSOrderService(_connectionString), downloadReportRequest);
                CSVReport = lstdownloadReportResponse != null && lstdownloadReportResponse.Count > 0 ? CommonService.ListToCSV(lstdownloadReportResponse) : string.Empty;

                appRoot = Directory.GetCurrentDirectory();

                string CSVFileName = "OrderPODReport_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".csv";

                Folderpath = Path.Combine(appRoot, "ReportDownload");
                if (!Directory.Exists(Folderpath))
                {
                    Directory.CreateDirectory(Folderpath);
                }


                if (!string.IsNullOrEmpty(CSVReport))
                {
                    URLPath = rootPath + "ReportDownload" + "/" + CSVFileName;
                    Folderpath = Path.Combine(Folderpath, CSVFileName);
                    CommonService.SaveFile(Folderpath, CSVReport);
                }


                statusCode = !string.IsNullOrEmpty(CSVReport) ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = !string.IsNullOrEmpty(CSVReport) ? URLPath : string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
    }
}