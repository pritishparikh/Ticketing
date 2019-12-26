using System;
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
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CustomerController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        #endregion

        #region Cunstructor
        public CustomerController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
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
        [AllowAnonymous]
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
                    _objcustomerMaster = _customercaller.getCustomerDetailsById(new CustomerService(_connectioSting), CustomerID);
                    StatusCode =
                       _objcustomerMaster != null ?
                               (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        [AllowAnonymous]
        public ResponseModel searchCustomer(string SearchText)
        {

            List<CustomerMaster> _objcustomerMaster = new List<CustomerMaster>();
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                _objcustomerMaster = _customercaller.getCustomerDetailsByEmailIdandPhone(new CustomerService(_connectioSting), SearchText);

                StatusCode =
                      _objcustomerMaster == null ?
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
        //[AllowAnonymous]
        public ResponseModel createCustomer([FromBody]CustomerMaster customerMaster)
        {
            List<CustomerMaster> _objcustomerMaster = new List<CustomerMaster>();
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                int result = _customercaller.addCustomer(new CustomerService(_connectioSting), customerMaster);
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
        //[AllowAnonymous]
        public ResponseModel updateCustomer([FromBody]CustomerMaster customerMaster)
        {
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                int result = _customercaller.updateCustomer(new CustomerService(_connectioSting), customerMaster);
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
        #endregion
    }
}