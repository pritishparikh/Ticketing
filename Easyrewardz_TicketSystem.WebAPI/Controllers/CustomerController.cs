using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CustomerController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public CustomerController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
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
        public ResponseModel Getcustomerdetailsbyid(int CustomerID)
        {

            CustomerMaster objcustomerMaster = new CustomerMaster();
            Customercaller customercaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                if (CustomerID > 0)
                {

                    string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                    Authenticate authenticate = new Authenticate();
                    authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                    objcustomerMaster = customercaller.getCustomerDetailsById(new CustomerService(_connectioSting), CustomerID, authenticate.TenantId);
                    statusCode =
                       objcustomerMaster != null ?
                               (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                    objResponseModel.Status = true;
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = statusMessage;
                    objResponseModel.ResponseData = objcustomerMaster;
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
        public ResponseModel SearchCustomer(string SearchText)
        {

            List<CustomerMaster> objcustomerMaster = new List<CustomerMaster>();
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                objcustomerMaster = customerCaller.getCustomerDetailsByEmailIdandPhone(new CustomerService(_connectioSting), SearchText, authenticate.TenantId,authenticate.UserMasterID);

                statusCode =
                      objcustomerMaster.Count == 0 ?
                              (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objcustomerMaster;
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
        public ResponseModel CreateCustomer([FromBody]CustomerMaster customerMaster)
        {
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                customerMaster.CreatedBy = authenticate.UserMasterID;

                int result = customerCaller.addCustomer(new CustomerService(_connectioSting), customerMaster, authenticate.TenantId);
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
        public ResponseModel UpdateCustomer([FromBody]CustomerMaster customerMaster)
        {
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = customerCaller.updateCustomer(new CustomerService(_connectioSting), customerMaster, authenticate.TenantId);
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
        public ResponseModel ValidateCustomerExist(string Cust_EmailId, string Cust_PhoneNumber)
        {
            Customercaller customerCaller = new Customercaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                string result = customerCaller.validateCustomerExist(new CustomerService(_connectioSting), Cust_EmailId, Cust_PhoneNumber, authenticate.TenantId);
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