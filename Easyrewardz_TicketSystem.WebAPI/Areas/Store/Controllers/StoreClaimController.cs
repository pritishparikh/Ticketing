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
using System.IO;
using System.Text.RegularExpressions;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreClaimController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        private readonly string _ClaimProductImage;
        #endregion
        #region Constructor
        public StoreClaimController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _ClaimProductImage = configuration.GetValue<string>("RaiseClaimProductImage");
        }
        #endregion
        #region Custom Methods
        /// <summary>
        /// Raise Claim
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RaiseClaim")]
        public ResponseModel RaiseClaim(IFormFile File)
        {
            StoreClaimMaster storeClaimMaster = new StoreClaimMaster();
            OrderMaster orderDetails = new OrderMaster();
            List<OrderItem> OrderItemDetails = new List<OrderItem>();
            var files = Request.Form.Files;
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            string fileName = "";
            string finalAttchment = "";

            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                }
                finalAttchment = fileName.TrimEnd(',');
            }
            var Keys = Request.Form;
            storeClaimMaster = JsonConvert.DeserializeObject<StoreClaimMaster>(Keys["storeClaimMaster"]);
            // get order details from form
            orderDetails = JsonConvert.DeserializeObject<OrderMaster>(Keys["orderDetails"]);
            OrderItemDetails = JsonConvert.DeserializeObject<List<OrderItem>>(Keys["orderItemDetails"]);

            var exePath = Path.GetDirectoryName(System.Reflection
                    .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            string Folderpath = appRoot + "\\" + _ClaimProductImage;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                #region check orderdetails and item details 

                if (orderDetails != null)
                {

                    if (orderDetails.OrderMasterID.Equals(0))
                    {

                        string OrderNumber = string.Empty;
                        string OrderItemsIds = string.Empty;
                        OrderMaster objorderMaster = null;

                        OrderCaller ordercaller = new OrderCaller();
                        //call insert order
                        orderDetails.CreatedBy = authenticate.UserMasterID;
                        OrderNumber = ordercaller.addOrder(new OrderService(_connectionSting), orderDetails, authenticate.TenantId);
                        if (!string.IsNullOrEmpty(OrderNumber))
                        {
                            objorderMaster = ordercaller.getOrderDetailsByNumber(new OrderService(_connectionSting), OrderNumber, authenticate.TenantId);


                            if (objorderMaster != null)
                            {
                                if (OrderItemDetails != null)
                                {
                                    foreach (var item in OrderItemDetails)
                                    {
                                        item.OrderMasterID = objorderMaster.OrderMasterID;
                                        item.InvoiceDate = orderDetails.InvoiceDate;
                                    }

                                    OrderItemsIds = ordercaller.AddOrderItem(new OrderService(_connectionSting), OrderItemDetails, authenticate.TenantId, authenticate.UserMasterID);

                                }
                                else
                                {
                                    OrderItemsIds = Convert.ToString(objorderMaster.OrderMasterID) + "|0|1";
                                    //OrderItemsIds = Convert.ToString(objorderMaster.OrderMasterID) + "|1";
                                }

                            }

                            storeClaimMaster.OrderMasterID = objorderMaster.OrderMasterID;
                            storeClaimMaster.OrderItemID = OrderItemsIds;
                        }

                    }


                }
                #endregion
                StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
                storeClaimMaster.TenantID = authenticate.TenantId;
                storeClaimMaster.CreatedBy = authenticate.UserMasterID;
                int result = storeClaimCaller.InsertRaiseClaim(new StoreClaimService(_connectionSting), storeClaimMaster, finalAttchment);
                if (result > 0)
                {
                    if (files.Count > 0)
                    {
                        string[] filesName = finalAttchment.Split(",");
                        for (int i = 0; i < files.Count; i++)
                        {
                            using (var ms = new MemoryStream())
                            {
                                files[i].CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                MemoryStream msfile = new MemoryStream(fileBytes);
                                FileStream docFile = new FileStream(Folderpath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                                msfile.WriteTo(docFile);
                                docFile.Close();
                                ms.Close();
                                msfile.Close();
                                string s = Convert.ToBase64String(fileBytes);
                                byte[] a = Convert.FromBase64String(s);
                                // act on the Base64 data

                            }
                        }
                    }
                }
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
        /// Add Store Claim Comment
        /// </summary>
        /// <param name="CommentForId"></param>
        ///    <param name="ID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStoreClaimComment")]
        public ResponseModel AddStoreClaimComment(int ClaimID, string Comment)
        {
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = storeClaimCaller.AddClaimComment(new StoreClaimService(_connectionSting), ClaimID, Comment, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Claim Comment By ClaimID
        /// </summary>
        /// <param name="TaskId"></param>
        [HttpPost]
        [Route("GetClaimCommentByClaimID")]
        public ResponseModel GetClaimCommentByClaimID(int ClaimID)
        {
            List<UserComment> objClaimComment= new List<UserComment>();
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                objClaimComment = storeClaimCaller.GetClaimComment(new StoreClaimService(_connectionSting), ClaimID);
                statusCode =
                   objClaimComment.Count==0?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objClaimComment;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Claim List
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClaimList")]
        public ResponseModel GetClaimList(int tab_For)
        {
            List<CustomClaimList> objClaimMaster = new List<CustomClaimList>();
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                objClaimMaster = storeClaimCaller.GetClaimList(new StoreClaimService(_connectionSting), tab_For, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                   objClaimMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objClaimMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Claim By ID
        /// </summary>
        /// <param name="ClaimID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClaimByID")]
        public ResponseModel GetClaimByID(int ClaimID)
        {
            CustomClaimByID objClaimMaster = new CustomClaimByID();
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                string url = configuration.GetValue<string>("APIURL") + _ClaimProductImage;
                objClaimMaster = storeClaimCaller.GetClaimByID(new StoreClaimService(_connectionSting), ClaimID, authenticate.TenantId, authenticate.UserMasterID, url);
                StatusCode =
                   objClaimMaster == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objClaimMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Store Claim Comment By Approvel
        /// </summary>
        /// <param name="CommentForId"></param>
        ///    <param name="ID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StoreClaimCommentByApprovel")]
        public ResponseModel StoreClaimCommentByApprovel(int ClaimID, string Comment)
        {
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = storeClaimCaller.AddClaimCommentByApprovel(new StoreClaimService(_connectionSting), ClaimID, Comment, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Claim Comment For Approvel
        /// </summary>
        /// <param name="TaskId"></param>
        [HttpPost]
        [Route("GetClaimCommentForApprovel")]
        public ResponseModel GetClaimCommentForApprovel(int ClaimID)
        {
            List<CommentByApprovel> objClaimComment = new List<CommentByApprovel>();
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                objClaimComment = storeClaimCaller.GetClaimCommentForApprovel(new StoreClaimService(_connectionSting), ClaimID);
                statusCode =
                   objClaimComment.Count==0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objClaimComment;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Claim Approve Or Reject
        /// </summary>
        /// <param name="claimID"></param>
        ///    <param name="finalClaimAsked"></param>
        ///   <param name="IsApprove"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("IsClaimApprove")]
        public ResponseModel IsClaimApprove(int claimID,double finalClaimAsked ,bool IsApprove)
        {
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = storeClaimCaller.ClaimApprove(new StoreClaimService(_connectionSting), claimID, finalClaimAsked, IsApprove, authenticate.UserMasterID, authenticate.TenantId);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Claim Re Assign
        /// </summary>
        /// <param name=""></param>
        ///    <param name="claimID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ClaimReAssign")]
        public ResponseModel ClaimReAssign(int claimID, int assigneeID)
        {
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = storeClaimCaller.AssignClaim(new StoreClaimService(_connectionSting), claimID, assigneeID,  authenticate.UserMasterID, authenticate.TenantId);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Order and Customer Detail By TicketID
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderwithCustomerDetailByTicketID")]
        public ResponseModel GetOrderandCustomerDetailByTicketID(int ticketID)
        {
            List<CustomOrderwithCustomerDetails> objorderMaster = new List<CustomOrderwithCustomerDetails>();
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objorderMaster = storeClaimCaller.GetOrderDetailByticketID(new StoreClaimService(_connectionSting), ticketID, authenticate.TenantId);
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