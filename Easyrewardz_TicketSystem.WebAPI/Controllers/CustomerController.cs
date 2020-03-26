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
    public class CustomerController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public CustomerController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
        #endregion
      
        #region Custom Methods
        /// <summary>
        /// Get Customer Details By ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getcustomerdetailsbyid")]
        public ResponseModel getcustomerdetailsbyid(int CustomerID)
        {

            CustomerMaster objCustomerMaster = new CustomerMaster();
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                if (CustomerID > 0)
                {

                    string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                    Authenticate authenticate = new Authenticate();
                    authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                    objCustomerMaster = customerCaller.getCustomerDetailsById(new CustomerService(Cache, Db), CustomerID, authenticate.TenantId);
                    statusCode =
                       objCustomerMaster != null ?
                               (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                    objResponseModel.Status = true;
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = statusMessage;
                    objResponseModel.ResponseData = objCustomerMaster;
                }
                else
                {
                    statusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                    objResponseModel.Status = true;
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = statusMessage;
                    objResponseModel.ResponseData = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        ///  Get Customer Details By ID/Contact No
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchCustomer")]
        public ResponseModel searchCustomer(string SearchText)
        {

            List<CustomerMaster> objCustomerMaster = new List<CustomerMaster>();
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));


                objCustomerMaster = customerCaller.getCustomerDetailsByEmailIdandPhone(new CustomerService(Cache, Db), SearchText, authenticate.TenantId);

                statusCode =
                      objCustomerMaster.Count == 0 ?
                              (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCustomerMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
        /// <summary>
        /// Add customer
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createCustomer")]
        public ResponseModel createCustomer([FromBody]CustomerMaster customerMaster)
        {
            List<CustomerMaster> objCustomerMaster = new List<CustomerMaster>();
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = customerCaller.addCustomer(new CustomerService(Cache, Db), customerMaster, authenticate.TenantId);
                statusCode =
                result == 0?
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
        /// Update Customer
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateCustomer")]
        public ResponseModel updateCustomer([FromBody]CustomerMaster customerMaster)
        {
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = customerCaller.updateCustomer(new CustomerService(Cache, Db), customerMaster, authenticate.TenantId);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// validate Customer Exist
        /// </summary>
        /// <param name="Cust_EmailId"></param>
        /// <param name="Cust_PhoneNumber"></param>
        /// <param name="TenantId"></param>
        /// <returns>Message</returns>
        [HttpPost]
        [Route("validateCustomerExist")]
        public ResponseModel validateCustomerExist(string Cust_EmailId, string Cust_PhoneNumber)
        {
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                string result = customerCaller.validateCustomerExist(new CustomerService(Cache, Db), Cust_EmailId, Cust_PhoneNumber, authenticate.TenantId);
                statusCode =
                string.IsNullOrEmpty(result) ?
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

        #endregion
    }
}