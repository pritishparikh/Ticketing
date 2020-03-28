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
    public class PriorityController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string connectionString;
        private readonly string radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public PriorityController(IConfiguration iConfig)
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
        public ResponseModel GetPriorityList(int PriorityFor=1)
        {
            List<Priority> objPriority = new List<Priority>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller _newMasterSubCat = new MasterCaller();

                objPriority = _newMasterSubCat.GetPriorityList(new PriorityService(connectionString), authenticate.TenantId, PriorityFor);

                StatusCode =
                objPriority.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        public ResponseModel AddPriority(string PriorityName, int status,int PriorityFor=1)
        {
            MasterCaller MasterCaller = new MasterCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = MasterCaller.Addpriority(new PriorityService(connectionString), PriorityName, status, authenticate.TenantId, authenticate.UserMasterID, PriorityFor);
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
        public ResponseModel  UpdatePriority(int PriorityID, string PriorityName, int status, int PriorityFor=1)
        {
            MasterCaller MasterCaller = new MasterCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int result = MasterCaller.Updatepriority(new PriorityService(connectionString), PriorityID, PriorityName, status, authenticate.TenantId, authenticate.UserMasterID, PriorityFor);
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
        public ResponseModel DeletePriority(int PriorityID, int PriorityFor=1)
        {
            MasterCaller MasterCaller = new MasterCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                int result = MasterCaller.Deletepriority(new PriorityService(connectionString), PriorityID, authenticate.TenantId, authenticate.UserMasterID, PriorityFor);
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
        public ResponseModel PriorityList(int PriorityFor)
        {
            List<Priority> objPriority = new List<Priority>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller _newMasterSubCat = new MasterCaller();

                objPriority = _newMasterSubCat.PriorityList(new PriorityService(connectionString), authenticate.TenantId, PriorityFor);

                StatusCode =
                objPriority.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        public ResponseModel UpdatePriorityOrder(int selectedPriorityID, int currentPriorityID, int PriorityFor = 1)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMaster = new MasterCaller();

                bool iStatus = newMaster.UpdatePriorityOrder(new PriorityService(connectionString), authenticate.TenantId, selectedPriorityID, currentPriorityID, PriorityFor);

                StatusCode =
                iStatus ?
                     (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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