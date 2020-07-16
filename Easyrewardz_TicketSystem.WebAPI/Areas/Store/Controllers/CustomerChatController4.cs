using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    public partial class CustomerChatController : ControllerBase
    {
        #region Custom Methods

        /// <summary>
        /// Get Chat Customer Profile Details
        /// <param name="CustomerID"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatCustomerProfile")]
        public ResponseModel GetChatCustomerProfile(int CustomerID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            ChatCustomerProfileModel CustomerProfile = new ChatCustomerProfileModel();

            int statusCode = 0;
            string statusMessage = "";
            string SoundPhysicalFilePath = string.Empty;
            string SoundFilePath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                CustomerProfile = customerChatCaller.GetChatCustomerProfileDetails(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID,authenticate.UserMasterID,_ClientAPIUrl);
                statusCode = CustomerProfile.CustomerID > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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


        /// <summary>
        /// Get Chat Customer Products Details
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatCustomerProducts")]
        public ResponseModel GetChatCustomerProducts(int CustomerID, string MobileNo)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<CustomerChatProductModel> CustomerProducts = new List<CustomerChatProductModel>();

            int statusCode = 0;
            string statusMessage = "";
            string SoundPhysicalFilePath = string.Empty;
            string SoundFilePath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                CustomerProducts = customerChatCaller.GetChatCustomerProducts(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID, MobileNo);
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
        public ResponseModel RemoveProduct(int CustomerID, string MobileNo, string ItemCode)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";
            string SoundPhysicalFilePath = string.Empty;
            string SoundFilePath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Result = customerChatCaller.RemoveProduct(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID, MobileNo, ItemCode);
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
        /// Add Item to Shopping Bag
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCode"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProductsToShoppingBag")]
        public ResponseModel AddProductsToShoppingBag(int CustomerID, string MobileNo, string ItemCodes, bool IsFromRecommendation=false)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";
            string SoundPhysicalFilePath = string.Empty;
            string SoundFilePath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Result = customerChatCaller.AddProductsToShoppingBag(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID, MobileNo, ItemCodes, IsFromRecommendation);
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
        /// Add Products To Wishlist
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCode"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProductsToWishlist")]
        public ResponseModel AddProductsToWishlist(int CustomerID, string MobileNo, string ItemCodes, bool IsFromRecommendation = false)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int Result = 0;

            int statusCode = 0;
            string statusMessage = "";
            string SoundPhysicalFilePath = string.Empty;
            string SoundFilePath = string.Empty;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Result = customerChatCaller.AddProductsToWishlist(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, CustomerID, MobileNo, ItemCodes, IsFromRecommendation);
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

        #endregion
    }
}
