using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public partial class HSOrderController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionString;
        private readonly string _radisCacheServerAddress;
        private readonly string _ClientAPIUrl;
        #endregion

        #region Constructor
        public HSOrderController(IConfiguration iConfig)
        {
            configuration = iConfig;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _ClientAPIUrl = configuration.GetValue<string>("ClientAPIURL");
        }
        #endregion


        [HttpPost]
        [Route("GetModuleConfiguration")]
        public ResponseModel GetModuleConfiguration()
        {
            ModuleConfiguration moduleConfiguration = new ModuleConfiguration();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                moduleConfiguration = storecampaigncaller.GetModuleConfiguration(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   moduleConfiguration.ID.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = moduleConfiguration;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        [HttpPost]
        [Route("UpdateModuleConfiguration")]
        public ResponseModel UpdateModuleConfiguration([FromBody]ModuleConfiguration moduleConfiguration)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateModuleConfiguration(new HSOrderService(_connectionString),
                    moduleConfiguration, authenticate.UserMasterID);
                statusCode =
                   UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        [HttpPost]
        [Route("GetOrderConfiguration")]
        public ResponseModel GetOrderConfiguration()
        {
            OrderConfiguration orderConfiguration = new OrderConfiguration();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderConfiguration = hSOrderCaller.GetOrderConfiguration(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);
                statusCode =
                   orderConfiguration.ID.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderConfiguration;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("UpdateOrderConfiguration")]
        public ResponseModel UpdateOrderConfiguration([FromBody]OrderConfiguration orderConfiguration)
        {
            int UpdateCount = 0;
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = hSOrderCaller.UpdateOrderConfiguration(new HSOrderService(_connectionString),
                    orderConfiguration, authenticate.UserMasterID);
                statusCode =
                   UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("GetOrderDeliveredDetails")]
        public ResponseModel GetOrderDeliveredDetails(OrderDeliveredFilterRequest orderDeliveredFilter)
        {
            OrderDeliveredDetails orderDelivereds = new OrderDeliveredDetails();
            HSOrderCaller hSOrderCaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderDelivereds = hSOrderCaller.GetOrderDeliveredDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, orderDeliveredFilter);
                statusCode =
                   orderDelivereds.TotalCount == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderDelivereds;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

    }
}