using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.WebAPI.Provider.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class HSSettingController : Controller
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        private readonly string rootPath;
        #endregion
        #region Constructor
        public HSSettingController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
        }
        #endregion

        #region Custom Methods
        // GET: api/<controller>
        [HttpPost]
        [Route("GetStoreAgentList")]
        public ResponseModel GetStoreAgentList(int BrandID, string StoreCode)
        {
            List<HSSettingModel> customHierarchymodels = new List<HSSettingModel>();
            HSSettingCaller HSSettingCaller = new HSSettingCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                customHierarchymodels = HSSettingCaller.GetStoreAgentList(new HSSettingService(_connectionSting), authenticate.TenantId, BrandID, StoreCode);
                statusCode =
                   customHierarchymodels.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customHierarchymodels;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("InsertUpdateAgentDetails")]
        public ResponseModel InsertUpdateAgentDetails([FromBody]HSSettingModel hSSettingModel)
        {
            HSSettingCaller hSSettingCaller = new HSSettingCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = hSSettingCaller.InsertUpdateAgentDetails(new HSSettingService(_connectionSting), hSSettingModel, authenticate.TenantId);
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

        [HttpPost]
        [Route("GetStoreAgentDetailsById")]
        public ResponseModel GetStoreAgentDetailsById(int AgentID)
        {
            List<HSSettingModel> customHierarchymodels = new List<HSSettingModel>();
            HSSettingCaller HSSettingCaller = new HSSettingCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                customHierarchymodels = HSSettingCaller.GetStoreAgentDetailsById(new HSSettingService(_connectionSting), authenticate.TenantId, AgentID);
                statusCode =
                   customHierarchymodels.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customHierarchymodels;
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
