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

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class GraphController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor
        public GraphController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        /// <summary>
        /// Get User List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserList")]
        public ResponseModel GetUserList()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            List<User> userdetails = new List<User>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                GraphCaller graphcaller = new GraphCaller();

                userdetails = graphcaller.GetUserList(new GraphService(_connectionSting), authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                userdetails.Count == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = userdetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Graph Count Data
        /// </summary>
        /// <param name="UserIds"></param>
        /// <param name="BrandIDs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetGraphCountData")]
        public ResponseModel GetGraphCountData([FromBody]GraphCountDataRequest GraphCountData)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            GraphModal graphmodalObj = new GraphModal();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                GraphCaller graphcaller = new GraphCaller();

                graphmodalObj = graphcaller.GetGraphCountData(new GraphService(_connectionSting), authenticate.TenantId, authenticate.UserMasterID, GraphCountData);
                StatusCode =(int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = graphmodalObj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Graph Data
        /// </summary>
        /// <param name="GraphCountData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetGraphData")]
        public ResponseModel GetGraphData([FromBody]GraphCountDataRequest GraphCountData)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            GraphData graphdataObj = new GraphData();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                GraphCaller graphcaller = new GraphCaller();

                graphdataObj = graphcaller.GetGraphData(new GraphService(_connectionSting), authenticate.TenantId, authenticate.UserMasterID, GraphCountData);
                StatusCode = (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = graphdataObj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
    }
}