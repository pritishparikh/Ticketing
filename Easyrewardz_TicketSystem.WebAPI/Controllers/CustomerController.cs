﻿using System;
using System.Collections.Generic;
using System.Data;
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
using Newtonsoft.Json;

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
        public ResponseModel getcustomerdetailsbyid(int CustomerID)
        {

            CustomerMaster _objcustomerMaster = new CustomerMaster();
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                if (CustomerID > 0)
                {

                    string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                    Authenticate authenticate = new Authenticate();
                    authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                    _objcustomerMaster = _customercaller.getCustomerDetailsById(new CustomerService(_connectioSting), CustomerID, authenticate.TenantId);
                    StatusCode =
                       _objcustomerMaster != null ?
                               (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = StatusCode;
                    _objResponseModel.Message = statusMessage;
                    _objResponseModel.ResponseData = _objcustomerMaster;
                }
                else
                {
                    StatusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = StatusCode;
                    _objResponseModel.Message = statusMessage;
                    _objResponseModel.ResponseData = null;
                }
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
        ///  Get Customer Details By ID/Contact No
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchCustomer")]
        public ResponseModel searchCustomer(string SearchText)
        {

            List<CustomerMaster> _objcustomerMaster = new List<CustomerMaster>();
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));


                _objcustomerMaster = _customercaller.getCustomerDetailsByEmailIdandPhone(new CustomerService(_connectioSting), SearchText, authenticate.TenantId);

                StatusCode =
                      _objcustomerMaster.Count == 0 ?
                              (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objcustomerMaster;
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
        /// Add customer
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createCustomer")]
        public ResponseModel createCustomer([FromBody]CustomerMaster customerMaster)
        {
            List<CustomerMaster> _objcustomerMaster = new List<CustomerMaster>();
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                int result = _customercaller.addCustomer(new CustomerService(_connectioSting), customerMaster, authenticate.TenantId);
                StatusCode =
                result == 0?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
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
        /// Update Customer
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateCustomer")]
        public ResponseModel updateCustomer([FromBody]CustomerMaster customerMaster)
        {
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                int result = _customercaller.updateCustomer(new CustomerService(_connectioSting), customerMaster, authenticate.TenantId);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
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
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                string result = _customercaller.validateCustomerExist(new CustomerService(_connectioSting), Cust_EmailId, Cust_PhoneNumber, authenticate.TenantId);
                StatusCode =
                string.IsNullOrEmpty(result) ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
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