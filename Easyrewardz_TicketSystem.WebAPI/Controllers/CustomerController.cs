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
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CustomerController : ControllerBase
    {
        private IConfiguration configuration;
        private readonly string _connectioSting;

        public CustomerController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
        }
        [HttpPost]
        [Route("getcustomerdetailsbyid")]
        public ResponseModel getcustomerdetailsbyid(int CustomerID)
        {
           
            CustomerMaster _objcustomerMaster = null;
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                if(CustomerID > 0)
                {
                    _objcustomerMaster = _customercaller.getCustomerDetailsById(new CustomerService(_connectioSting), CustomerID);
                    StatusCode =
                       _objcustomerMaster == null ?
                               (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                     statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = StatusCode;
                    _objResponseModel.Message = statusMessage;
                    _objResponseModel.ResponseData = _objcustomerMaster;                   
                }
                else
                {
                    StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
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
        [HttpPost]
        [Route("searchCustomer")]
        public ResponseModel searchCustomer(string Email, string Phoneno)
        {
            
            CustomerMaster _objcustomerMaster = null;
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                _objcustomerMaster = _customercaller.getCustomerDetailsByEmailIdandPhone(new CustomerService(_connectioSting), Email, Phoneno);

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
        [HttpPost]
        [Route("createCustomer")]
        [AllowAnonymous]
        public ResponseModel createCustomer([FromBody]CustomerMaster customerMaster)
        {
            Customercaller _customercaller = new Customercaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                int result = _customercaller.addCustomer(new CustomerService(_connectioSting), customerMaster);
                CustomerMaster customer = _customercaller.getCustomerDetailsById(new CustomerService(_connectioSting), result);
                StatusCode =
                result == 0?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                //_objResponseModel.ResponseData = _objcustomerMaster;
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
        [HttpPost]
        [Route("updateCustomer")]
        [AllowAnonymous]
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
    }
}