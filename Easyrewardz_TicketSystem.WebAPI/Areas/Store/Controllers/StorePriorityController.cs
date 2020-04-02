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
    public class StorePriorityController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string connectionString;
        private readonly string radisCacheServerAddress;
        #endregion
        #region Cunstructor
        public StorePriorityController(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectionString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get PriorityList
        /// </summary>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPriorityList")]
        public ResponseModel GetPriorityList()
        {
            List<Priority> objPriority = new List<Priority>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StorePriorityCaller storePriorityCaller = new StorePriorityCaller();

                objPriority = storePriorityCaller.GetPriorityList(new StorePriorityService(connectionString), authenticate.TenantId);

                statusCode =
                objPriority.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objPriority;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Add Priority
        /// </summary>
        /// <param name="PriorityName"></param>
        /// <param name="status"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPriority")]
        public ResponseModel AddPriority(string PriorityName, int status)
        {
            StorePriorityCaller storePriorityCaller = new StorePriorityCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = storePriorityCaller.Addpriority(new StorePriorityService(connectionString), PriorityName, status, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Update Priority
        /// </summary>
        /// <param name="PriorityID"></param>
        /// <param name="PriorityName"></param>
        /// <param name="status"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePriority")]
        public ResponseModel UpdatePriority(int PriorityID, string PriorityName, int status)
        {
            StorePriorityCaller storePriorityCaller = new StorePriorityCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = storePriorityCaller.Updatepriority(new StorePriorityService(connectionString), PriorityID, PriorityName, status, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        ///Delete Priority
        /// </summary>
        /// <param name="PriorityID"></param>
        /// // <param name="PriorityFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeletePriority")]
        public ResponseModel DeletePriority(int PriorityID)
        {
            StorePriorityCaller storePriorityCaller = new StorePriorityCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                int result = storePriorityCaller.Deletepriority(new StorePriorityService(connectionString), PriorityID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Priority List
        /// </summary>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PriorityList")]
        public ResponseModel PriorityList()
        {
            List<Priority> objPriority = new List<Priority>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                StorePriorityCaller storePriorityCaller = new StorePriorityCaller();
                objPriority = storePriorityCaller.PriorityList(new StorePriorityService(connectionString), authenticate.TenantId);

                statusCode =
                objPriority.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objPriority;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Update Priority Order 
        /// </summary>
        /// <param name="selectedPriorityID"></param>
        /// <param name="currentPriorityID"></param>
        /// <param name="PriorityFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePriorityOrder")]
        public ResponseModel UpdatePriorityOrder(int selectedPriorityID, int currentPriorityID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StorePriorityCaller storePriorityCaller = new StorePriorityCaller();
                bool iStatus = storePriorityCaller.UpdatePriorityOrder(new StorePriorityService(connectionString), authenticate.TenantId, selectedPriorityID, currentPriorityID);
                statusCode =
                iStatus ?
                     (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = iStatus;

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