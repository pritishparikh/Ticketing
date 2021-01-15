using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    public partial class CustomerChatController : ControllerBase
    {
        #region Custom Methods

        #region Customer Chat Profile APIs

        /// <summary>
        /// Get Chat Customer Profile Details
        /// <param name="CustomerID"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ChatCustomerProfileOrderDetails")]
        public async Task<ResponseModel> ChatCustomerProfileOrderDetails(int CustomerID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            ChatProfileOrder CustomerProfile = new ChatProfileOrder();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                CustomerProfile = await customerChatCaller.ChatCustomerProfileOrderDetails(new CustomerChatService(_connectionString, _chatbotBellHttpClientService), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID,authenticate.UserMasterID);
                statusCode = CustomerProfile != null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CustomerProfile;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("ChatProfileCustomerInsights")]
        public async Task<ResponseModel> ChatProfileCustomerInsights(string MobileNo)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<InsightsModel> CustomerInsights = new List<InsightsModel>();
            string ClientAPIResponse = string.Empty; string JsonRequest = string.Empty;
            int statusCode = 0;
            string statusMessage = "";
            ClientChatProfileModel ClientRequest = new ClientChatProfileModel();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                    ClientRequest.programCode = authenticate. ProgramCode;
                    ClientRequest .mobileNumber=  MobileNo;
                    JsonRequest = JsonConvert.SerializeObject(ClientRequest);
                   ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + _getKeyInsight, JsonRequest);
                 //  ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + "api/ChatbotBell/GetKeyInsight", JsonRequest);

                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    CustomerInsights = JsonConvert.DeserializeObject<List<InsightsModel>>(ClientAPIResponse);

                }
                statusCode = CustomerInsights.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CustomerInsights;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("GetChatCustomerProfile")]
        public async Task<ResponseModel> GetChatCustomerProfile(string MobileNo)
        {
            ResponseModel objResponseModel = new ResponseModel();
            CustomerpopupDetails CustomerATVDetails = null;
            string ClientAPIResponse = string.Empty; string JsonRequest = string.Empty;
            int statusCode = 0;
            string statusMessage = "";
            ClientChatProfileModel ClientRequest = new ClientChatProfileModel();
            ChatCustomerProfileModel CustomerProfile = new ChatCustomerProfileModel();

           
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                ClientRequest.programCode = authenticate.ProgramCode;
                ClientRequest.mobileNumber = MobileNo;
                JsonRequest = JsonConvert.SerializeObject(ClientRequest);

              

               // ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + "api/ChatbotBell/GetUserATVDetails", JsonRequest);

                ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + _getUserAtvDetails, JsonRequest);


                if (!string.IsNullOrEmpty(ClientAPIResponse))
                { 
                    CustomerATVDetails = JsonConvert.DeserializeObject<CustomerpopupDetails>(ClientAPIResponse);

                   
                    if (CustomerATVDetails != null)
                    {
                        CustomerATVDetails.email = string.IsNullOrEmpty(CustomerATVDetails.email) ? "" : CustomerATVDetails.email;
                        CustomerATVDetails.tiername = string.IsNullOrEmpty(CustomerATVDetails.tiername) ? "" : CustomerATVDetails.tiername;
                        CustomerATVDetails.lifeTimeValue = string.IsNullOrEmpty(CustomerATVDetails.lifeTimeValue) ? "0" : CustomerATVDetails.lifeTimeValue;
                        CustomerATVDetails.visitCount = string.IsNullOrEmpty(CustomerATVDetails.visitCount) ? "0" : CustomerATVDetails.visitCount;
                        CustomerATVDetails.availablePoints = string.IsNullOrEmpty(CustomerATVDetails.availablePoints) ? "0" : CustomerATVDetails.availablePoints;
                    }
                }
                statusCode = CustomerATVDetails != null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CustomerATVDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        [HttpPost]
        [Route("ChatProfileLastTransactionDetails")]
        public async Task<ResponseModel> ChatProfileLastTransactionDetails(string MobileNo)
        {
            ResponseModel objResponseModel = new ResponseModel();
            LastTransactionDetailsResponseModel LastTransaction = null;
            CustomerpopupDetails CustomerATVDetails = null;
            string ClientAPIResponse = string.Empty; string JsonRequest = string.Empty;
            int statusCode = 0;
            string statusMessage = "";
            ClientChatProfileModel ClientRequest = new ClientChatProfileModel();
            ChatCustomerProfileModel CustomerProfile = new ChatCustomerProfileModel();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                ClientRequest.programCode = authenticate.ProgramCode;
                ClientRequest.mobileNumber = MobileNo;
                JsonRequest = JsonConvert.SerializeObject(ClientRequest);


                ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + _getLastTransactionDetails, JsonRequest);
             //   ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + "api/ChatbotBell/GetLastTransactionDetails", JsonRequest);


                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    try
                    {
                        LastTransaction = JsonConvert.DeserializeObject<LastTransactionDetailsResponseModel>(ClientAPIResponse);

                    }
                    catch(Exception)
                    { }


                    if (LastTransaction != null)
                    {
                        CustomerProfile.BillNumber = string.IsNullOrEmpty(LastTransaction.billNo) ? "" : LastTransaction.billNo;
                        //CustomerProfile.BillAmount = string.IsNullOrEmpty(LastTransaction.amount) ? 0 : Convert.ToDecimal(CustomerATVDetails.lifeTimeValue);
                        CustomerProfile.StoreDetails = string.IsNullOrEmpty(LastTransaction.storeName) ? "" : LastTransaction.storeName;
                        CustomerProfile.TransactionDate = string.IsNullOrEmpty(LastTransaction.billDate) ? "" : Convert.ToDateTime(LastTransaction.billDate).ToString("dd MMM yyyy");
                        CustomerProfile.itemDetails = LastTransaction.itemDetails;

                        if(!string.IsNullOrEmpty(LastTransaction.amount))
                        {


                            //ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + "api/ChatbotBell/GetUserATVDetails", JsonRequest);
                            ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + _getUserAtvDetails, JsonRequest);

                            if (!string.IsNullOrEmpty(ClientAPIResponse))
                            {
                                CustomerATVDetails = JsonConvert.DeserializeObject<CustomerpopupDetails>(ClientAPIResponse);

                                if (CustomerATVDetails != null)
                                {
                                    CustomerProfile.BillAmount = string.IsNullOrEmpty(CustomerATVDetails.lifeTimeValue) ? 0 : Convert.ToDecimal(CustomerATVDetails.lifeTimeValue);
                                   
                                }

                            }
                        }
                        else
                        {
                            CustomerProfile.BillAmount = 0;
                        }

                    }


                }
                statusCode = LastTransaction != null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CustomerProfile;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #endregion

        /// <summary>
        /// Get Chat Customer Products Details
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatCustomerProducts")]
        public async Task<ResponseModel> GetChatCustomerProducts(int CustomerID, string MobileNo)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<CustomerChatProductModel> CustomerProducts = new List<CustomerChatProductModel>();

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                _ClientAPIUrl = _ClientAPIUrl + _getRecommendedList;

                CustomerProducts =await customerChatCaller.GetChatCustomerProducts(new CustomerChatService(_connectionString, _chatbotBellHttpClientService), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID, MobileNo,_ClientAPIUrl);
                statusCode = CustomerProducts.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CustomerProducts;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Remove Item from Wishlist /Shopping Bag
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCode"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveProduct")]
        public async Task<ResponseModel> RemoveProduct(int CustomerID, string MobileNo, string ItemCode, string RemoveFrom)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Result = await customerChatCaller.RemoveProduct(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID, MobileNo, ItemCode, RemoveFrom,authenticate.UserMasterID);
                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Add Item to Shopping Bag Or WishList
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCode"></param>
        /// <param name="Action"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProductsToBagOrWishlist")]
        public async  Task<ResponseModel> AddProductsToBagOrWishlist(int CustomerID, string MobileNo, string ItemCodes, string Action)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Result = await customerChatCaller.AddProductsToBagOrWishlist(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID, MobileNo, ItemCodes, Action, authenticate.UserMasterID);
                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Add From Recommendation To Wishlist
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCodes"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddRecommendationToWishlist")]
        public async Task<ResponseModel> AddRecommendationToWishlist(int CustomerID, string MobileNo, string ItemCodes)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                _ClientAPIUrl = _ClientAPIUrl + _getRecommendedList;
                Result = await customerChatCaller.AddRecommendationToWishlist(new CustomerChatService(_connectionString, _chatbotBellHttpClientService), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID, MobileNo, ItemCodes,_ClientAPIUrl,authenticate.UserMasterID);
                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Bu yProducts On Chat from shopping Bag/WishList
        /// <param name="ChatCustomerBuyModel"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BuyProductsOnChat")]
        public ResponseModel BuyProductsOnChat([FromBody] ChatCustomerBuyModel Buy)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                Buy.TenantID = authenticate.TenantId;
                Buy.ProgramCode = authenticate.ProgramCode;
                Buy.UserID = authenticate.UserMasterID;

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Result = customerChatCaller.BuyProductsOnChat(new CustomerChatService(_connectionString), Buy,_ClientAPIUrl);
                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Bu yProducts On Chat from shopping Bag/WishList
        /// <param name="ChatCustomerBuyModel"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BuyProductsOnChatNew")]
        public async Task<ResponseModel> BuyProductsOnChatNew([FromBody] ChatCustomerBuyModel Buy)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                Buy.TenantID = authenticate.TenantId;
                Buy.ProgramCode = authenticate.ProgramCode;
                Buy.UserID = authenticate.UserMasterID;

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Result = await customerChatCaller.BuyProductsOnChatNew(new CustomerChatService(_connectionString, _chatbotBellHttpClientService), 
                    Buy, _ClientAPIUrl, _getRecommendedList, _addOrderinPhygital);

                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Bu yProducts On Chat from shopping Bag/WishList
        /// <param name="ChatCustomerBuyModel"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("SendProductsOnChat")]
        public async Task<ResponseModel> SendProductsOnChat([FromBody]SendProductsToCustomer ProductDetails)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                ProductDetails.TenantID = authenticate.TenantId;
                ProductDetails.ProgramCode = authenticate.ProgramCode;
                ProductDetails.UserID = authenticate.UserMasterID;

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Result = await customerChatCaller.SendProductsOnChat(new CustomerChatService(_connectionString), ProductDetails, _ClientAPIUrl);
                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("SendProductsOnChatNew")]
        public async Task<ResponseModel> SendProductsOnChatNew([FromBody]SendProductsToCustomer ProductDetails)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                ProductDetails.TenantID = authenticate.TenantId;
                ProductDetails.ProgramCode = authenticate.ProgramCode;
                ProductDetails.UserID = authenticate.UserMasterID;

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

              

                Result =await customerChatCaller.SendProductsOnChatNew(new CustomerChatService(_connectionString, _chatbotBellHttpClientService),
                    ProductDetails, _ClientAPIUrl, _sendImageMessage,_getRecommendedList);
                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        #region Client Exposed API

        /// <summary>
        /// Add Products To ShoppingBag by Customer
        /// <param name="ClientChatAddProduct"></param>
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpPost]
        [Route("CustomerAddToShoppingBag")]
        public async Task<ResponseModel> CustomerAddToShoppingBag([FromBody] ClientChatAddProduct Item)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string connString = string.Empty;
            string statusMessage = "";
            try
            {

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                if (!string.IsNullOrEmpty(Item.ProgramCode))
                {

                    RedisCacheService cacheService = new RedisCacheService(_radisCacheServerAddress);
                    if (cacheService.Exists("Con" + Item.ProgramCode))
                    {
                        connString = cacheService.Get("Con" + Item.ProgramCode);
                        connString = JsonConvert.DeserializeObject<string>(connString);

                        Result = await customerChatCaller.CustomerAddToShoppingBag(new CustomerChatService(connString), Item);
                        statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                        objResponseModel.Status = true;
                        objResponseModel.StatusCode = statusCode;
                        objResponseModel.Message = statusMessage;
                        objResponseModel.ResponseData = Result;
                    }
                    else
                    {
                        objResponseModel.Status = false;
                        objResponseModel.StatusCode = statusCode;
                        objResponseModel.Message = "Invalid ProgramCode";
                        objResponseModel.ResponseData = Result;

                    }
                }
                else
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = "Please Provide ProgramCode";
                    objResponseModel.ResponseData = Result;

                }


            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Add Products To Wishlist by Customer
        /// <param name="ClientChatAddProduct"></param>
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("CustomerAddToWishlist")]
        public async Task<ResponseModel> CustomerAddToWishlist([FromBody] ClientChatAddProduct Item)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";
            string connString = string.Empty;
            try
            {


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                if (!string.IsNullOrEmpty(Item.ProgramCode))
                {

                    RedisCacheService cacheService = new RedisCacheService(_radisCacheServerAddress);
                    if (cacheService.Exists("Con" + Item.ProgramCode))
                    {
                        connString = cacheService.Get("Con" + Item.ProgramCode);
                        connString = JsonConvert.DeserializeObject<string>(connString);

                        Result = await customerChatCaller.CustomerAddToWishlist(new CustomerChatService(connString), Item);
                        statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                        objResponseModel.Status = true;
                        objResponseModel.StatusCode = statusCode;
                        objResponseModel.Message = statusMessage;
                        objResponseModel.ResponseData = Result;
                    }
                    else
                    {
                        objResponseModel.Status = false;
                        objResponseModel.StatusCode = statusCode;
                        objResponseModel.Message = "Invalid ProgramCode";
                        objResponseModel.ResponseData = Result;

                    }
                }
                else
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = "Please Provide ProgramCode";
                    objResponseModel.ResponseData = Result;

                }
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Remove Products To Wishlist/ShoppingBag by Customer
        /// <param name="ProgramCode"></param>
        /// <param name="CustomerMobile"></param>
        /// <param name="StoreCode"></param>
        /// <param name="ItemCode"></param>
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("CustomerRemoveProduct")]
        public async Task<ResponseModel> CustomerRemoveProduct(string ProgramCode, string CustomerMobile, string RemoveFrom, string ItemCode)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";
            string connString = string.Empty;
            try
            {


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                if (!string.IsNullOrEmpty(ProgramCode))
                {

                    RedisCacheService cacheService = new RedisCacheService(_radisCacheServerAddress);
                    if (cacheService.Exists("Con" + ProgramCode))
                    {
                        connString = cacheService.Get("Con" + ProgramCode);
                        connString = JsonConvert.DeserializeObject<string>(connString);

                        Result = await customerChatCaller.CustomerRemoveProduct(new CustomerChatService(connString), ProgramCode, CustomerMobile, RemoveFrom, ItemCode);
                        statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                        statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                        objResponseModel.Status = true;
                        objResponseModel.StatusCode = statusCode;
                        objResponseModel.Message = statusMessage;
                        objResponseModel.ResponseData = Result;
                    }
                    else
                    {
                        objResponseModel.Status = false;
                        objResponseModel.StatusCode = statusCode;
                        objResponseModel.Message = "Invalid ProgramCode";
                        objResponseModel.ResponseData = Result;

                    }
                }
                else
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = "Please Provide ProgramCode";
                    objResponseModel.ResponseData = Result;

                }

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #endregion


        #endregion
    }
}
